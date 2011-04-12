using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using ECollegeAPI.Util;
using System.IO.IsolatedStorage;
using Path = System.IO.Path;

// Based on this approach http://blog.humann.info/post/2010/09/03/WP7-Do-you-need-to-cache-data-.aspx

namespace eCollegeWP7.Util
{
    public class IsolatedStorageResponseCache : ECollegeResponseCache
    {
        private IsolatedStorageFile _storage;
        private string _sessionKey;

        public IsolatedStorageResponseCache(string session)
        {
            if (session == null) throw new ArgumentNullException("session");
            this._sessionKey = HashUtil.ToSHA1(session);

            //create cache dir
            _storage = IsolatedStorageFile.GetUserStoreForApplication();
            if (!_storage.DirectoryExists("ECollegeCache"))
            {
                _storage.CreateDirectory("ECollegeCache");
            }

            //create session dir
            string thisSessionDirectory = string.Format("ECollegeCache\\{0}", _sessionKey);
            if (!_storage.DirectoryExists(thisSessionDirectory))
            {
                _storage.CreateDirectory(thisSessionDirectory);
            }
        }

        public void PurgeOldSessions() {
            string[] otherSessionKeys = _storage.GetDirectoryNames("ECollegeCache\\*");
            foreach (string otherSessionKey in otherSessionKeys)
            {
                if (!_sessionKey.Equals(otherSessionKey))
                {
                    RecursiveDeleteDirectory(string.Format("ECollegeCache\\{0}",otherSessionKey));
                }
            }
        }

        protected void RecursiveDeleteDirectory(string dirToDelete)
        {
            string dirToDeleteTrailingSlash = string.Format("{0}\\", dirToDelete);

            var files = _storage.GetFileNames(dirToDeleteTrailingSlash);
            foreach (var file in files)
            {
                _storage.DeleteFile(string.Format("{0}\\{1}",dirToDelete,file));
            }
            var directories = _storage.GetDirectoryNames(dirToDeleteTrailingSlash);
            foreach (var directory in directories)
            {
                RecursiveDeleteDirectory(string.Format("{0}\\{1}", dirToDelete, directory));
            }
            _storage.DeleteDirectory(dirToDelete);
        }

        protected void DeleteFiles(string dir)
        {
            var files = _storage.GetFileNames(string.Format("{0}\\",dir));
            foreach (var file in files)
            {
                _storage.DeleteFile(string.Format("{0}\\{1}", dir, file));
            }
        }

        //Directory:  ECollegeCache\{sessionKey}\{scope}\{cacheKey}\{createdAtTimeStamp}.cache
        protected string GetFileGlobForCacheEntry(string scope, string cacheKey)
        {
            return string.Format("ECollegeCache\\{0}\\{1}\\{2}\\*.cache", _sessionKey,scope,cacheKey);
        }

        protected string GetFileForCacheEntry(string scope, string cacheKey)
        {
            return string.Format("ECollegeCache\\{0}\\{1}\\{2}\\{3}.cache", _sessionKey, scope, cacheKey, DateTime.UtcNow.ToFileTimeUtc());
        }

        protected string GetDirectoryForCacheEntry(string scope, string cacheKey)
        {
            return string.Format("ECollegeCache\\{0}\\{1}\\{2}", _sessionKey, scope, cacheKey);
        }

        protected string GetDirectoryForScope(string scope)
        {
            return string.Format("ECollegeCache\\{0}\\{1}", _sessionKey, scope);
        }

        protected string GetLatestFileForCacheEntry(string scope, string cacheKey, TimeSpan? expiration)
        {
            string fileGlob = GetFileGlobForCacheEntry(scope, cacheKey);
            var fileNames = _storage.GetFileNames(fileGlob); //probably only ever 1 filename, but checks multiple in case strategy changes

            DateTime? bestMatchCreatedAt = null;
            string bestMatchFileName = null;

            foreach (var fileName in fileNames)
            {
                var createdAt =
                    DateTime.FromFileTimeUtc(long.Parse(Path.GetFileNameWithoutExtension(fileName)));
                if (bestMatchCreatedAt == null || createdAt > bestMatchCreatedAt)
                {
                    if (expiration == null || (createdAt + expiration) > DateTime.UtcNow)
                    {
                        bestMatchCreatedAt = createdAt;
                        bestMatchFileName = fileName;
                    }
                }
            }
            return bestMatchFileName;
        }

        public ECollegeResponseCacheEntry Get(string scope, string cacheKey, TimeSpan? expiration)
        {
            ECollegeResponseCacheEntry result = null;

            string scopePath = GetDirectoryForScope(scope);

            lock (scopePath)
            {
                if (_storage.DirectoryExists(scopePath))
                {
                    string cacheEntryPath = GetDirectoryForCacheEntry(scope, cacheKey);

                    lock (cacheEntryPath)
                    {
                        if (_storage.DirectoryExists(cacheEntryPath))
                        {
                            string matchingCacheFileName = GetLatestFileForCacheEntry(scope, cacheKey,expiration);

                            if (matchingCacheFileName != null)
                            {
                                var matchingCacheFilePath = string.Format("{0}\\{1}", cacheEntryPath, matchingCacheFileName);

                                using (var f = new IsolatedStorageFileStream(matchingCacheFilePath, FileMode.Open, FileAccess.Read, _storage))
                                {
                                    using (var sr = new StreamReader(f))
                                    {
                                        result = new ECollegeResponseCacheEntry();
                                        result.CachedAt = DateTime.Now;
                                        result.Data = sr.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void Put(string scope, string cacheKey, string responseContent)
        {
            string scopePath = GetDirectoryForScope(scope);

            lock (scopePath)
            {
                if (_storage.DirectoryExists(scopePath))
                {
                    _storage.CreateDirectory(scopePath);
                }

                string cacheEntryPath = GetDirectoryForCacheEntry(scope, cacheKey);

                lock (cacheEntryPath)
                {
                    if (!_storage.DirectoryExists(cacheEntryPath))
                    {
                        _storage.CreateDirectory(cacheEntryPath);
                    } else
                    {
                        DeleteFiles(cacheEntryPath);
                    }

                    string cacheFilePath = GetFileForCacheEntry(scope, cacheKey);
                    using (var f = new IsolatedStorageFileStream(cacheFilePath, FileMode.Create, FileAccess.Write, _storage))
                    {
                        using (var sw = new StreamWriter(f))
                        {
                            sw.Write(responseContent);
                        }
                    }
                }
            }
        }
        
        public void Invalidate(string scope)
        {
            string scopePath = GetDirectoryForScope(scope);

            lock (scopePath)
            {
                if (_storage.DirectoryExists(scopePath))
                {
                    RecursiveDeleteDirectory(scopePath);
                }
            }
        }

        public void Invalidate(string scope, string cacheKey)
        {
            string scopePath = GetDirectoryForScope(scope);

            lock (scopePath)
            {
                if (_storage.DirectoryExists(scopePath))
                {
                    string cacheEntryPath = GetDirectoryForCacheEntry(scope, cacheKey);

                    lock (cacheEntryPath)
                    {
                        if (_storage.DirectoryExists(cacheEntryPath))
                        {
                            RecursiveDeleteDirectory(cacheEntryPath);
                        }
                    }
                }
            }
        }
    }
}

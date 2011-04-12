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
        private TimeSpan _expiresIn;
        private string _sessionKey;

        public IsolatedStorageResponseCache(string session, TimeSpan expiresIn)
        {
            this._expiresIn = expiresIn;
            if (session == null) throw new ArgumentNullException("session");
            this._sessionKey = HashUtil.ToSHA1(session);

            //create cache dir
            _storage = IsolatedStorageFile.GetUserStoreForApplication();
            if (!_storage.DirectoryExists("Cache"))
            {
                _storage.CreateDirectory("Cache");
            }

            //create session dir
            string thisSessionDirectory = string.Format("Cache\\{0}", _sessionKey);
            if (!_storage.DirectoryExists(thisSessionDirectory))
            {
                _storage.CreateDirectory(thisSessionDirectory);
            }
        }

        public void PurgeOldSessions() {
            string[] otherSessionKeys = _storage.GetDirectoryNames("Cache\\*");
            foreach (string otherSessionKey in otherSessionKeys)
            {
                if (!_sessionKey.Equals(otherSessionKey))
                {
                    RecursiveDeleteDirectory(string.Format("Cache\\{0}",otherSessionKey));
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

        //Directory:  Cache\{sessionKey}\{scope}\{cacheKey}\{expirationTimeStamp}.cache
        protected string GetFileGlobForCacheEntry(string scope, string cacheKey)
        {
            return string.Format("Cache\\{0}\\{1}\\{2}\\*.cache", _sessionKey,scope,cacheKey);
        }

        protected string GetFileForCacheEntry(string scope, string cacheKey)
        {
            DateTime expiration = DateTime.UtcNow + _expiresIn;
            return string.Format("Cache\\{0}\\{1}\\{2}\\{3}.cache",_sessionKey,scope,cacheKey,expiration.ToFileTimeUtc());
        }

        protected string GetDirectoryForCacheEntry(string scope, string cacheKey)
        {
            return string.Format("Cache\\{0}\\{1}\\{2}", _sessionKey, scope, cacheKey);
        }

        protected string GetDirectoryForScope(string scope)
        {
            return string.Format("Cache\\{0}\\{1}", _sessionKey, scope);
        }

        public ECollegeResponseCacheEntry Get(string scope, string cacheKey)
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
                            string existingCacheFileName = _storage.GetFileNames(GetFileGlobForCacheEntry(scope, cacheKey)).FirstOrDefault();

                            if (existingCacheFileName != null)
                            {
                                var existingCacheFilePath = string.Format("{0}\\{1}", cacheEntryPath, existingCacheFileName);

                                var expirationDate =
                                    DateTime.FromFileTimeUtc(long.Parse(Path.GetFileNameWithoutExtension(existingCacheFileName)));

                                if (expirationDate >= DateTime.UtcNow)
                                {
                                    using (var f = new IsolatedStorageFileStream(existingCacheFilePath, FileMode.Open, FileAccess.Read, _storage))
                                    {
                                        using (var sr = new StreamReader(f))
                                        {
                                            result = new ECollegeResponseCacheEntry();
                                            result.CachedAt = DateTime.Now;
                                            result.Data = sr.ReadToEnd();
                                        }
                                    }
                                }
                                else
                                {
                                    _storage.DeleteFile(existingCacheFilePath);
                                    _storage.DeleteDirectory(cacheEntryPath);
                                }
                            }
                            else
                            {
                                _storage.DeleteDirectory(cacheEntryPath);
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
                    if (_storage.DirectoryExists(cacheEntryPath))
                    {
                        string existingCacheFilePath = _storage.GetFileNames(GetFileGlobForCacheEntry(scope, cacheKey)).FirstOrDefault();
                        if (existingCacheFilePath != null)
                        {
                            _storage.DeleteFile(existingCacheFilePath);
                        }
                    }
                    else
                    {
                        _storage.CreateDirectory(cacheEntryPath);
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

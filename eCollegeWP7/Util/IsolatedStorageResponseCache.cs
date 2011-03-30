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
        private IsolatedStorageFile storage;
        private TimeSpan expiresIn;

        public IsolatedStorageResponseCache(TimeSpan expiresIn)
        {
            this.expiresIn = expiresIn;
            storage = IsolatedStorageFile.GetUserStoreForApplication();
            if (!storage.DirectoryExists("Cache"))
            {
                storage.CreateDirectory("Cache");
            }
        }
        
        protected string GetFileGlobForCacheKey(string cacheKey)
        {
            return string.Format("Cache\\{0}\\*.cache", cacheKey);
        }

        protected string GetFileForCacheKey(string cacheKey)
        {
            DateTime expiration = DateTime.UtcNow + expiresIn;
            return string.Format("Cache\\{0}\\{1}.cache",cacheKey,expiration.ToFileTimeUtc());
        }

        protected string GetDirectoryForCacheKey(string cacheKey)
        {
            return string.Format("Cache\\{0}", cacheKey);
        }

        public ECollegeResponseCacheEntry Get(string cacheKey)
        {
            string dirPath = GetDirectoryForCacheKey(cacheKey);

            ECollegeResponseCacheEntry result = null;

            lock (dirPath)
            {
                if (storage.DirectoryExists(dirPath))
                {

                    string existingCacheFileName = storage.GetFileNames(GetFileGlobForCacheKey(cacheKey)).FirstOrDefault();

                    if (existingCacheFileName != null)
                    {
                        var existingCacheFilePath = string.Format("{0}\\{1}", dirPath, existingCacheFileName);

                        var expirationDate =
                            DateTime.FromFileTimeUtc(long.Parse(Path.GetFileNameWithoutExtension(existingCacheFileName)));

                        if (expirationDate >= DateTime.UtcNow)
                        {
                            using (var f = new IsolatedStorageFileStream(existingCacheFilePath, FileMode.Open, FileAccess.Read, storage))
                            {
                                using (var sr = new StreamReader(f))
                                {
                                    result = new ECollegeResponseCacheEntry();
                                    result.CachedAt = DateTime.Now;
                                    result.Data = sr.ReadToEnd();
                                }
                            }
                        } else
                        {
                            storage.DeleteFile(existingCacheFilePath);
                            storage.DeleteDirectory(dirPath);
                        }
                    } else
                    {
                        storage.DeleteDirectory(dirPath);
                    }
                }
            }

            return result;

        }

        public void Put(string cacheKey, string responseContent)
        {
            string dirPath = GetDirectoryForCacheKey(cacheKey);

            lock (dirPath)
            {
                if (storage.DirectoryExists(dirPath))
                {
                    string existingCacheFilePath = storage.GetFileNames(GetFileGlobForCacheKey(cacheKey)).FirstOrDefault();
                    if (existingCacheFilePath != null)
                    {
                        storage.DeleteFile(existingCacheFilePath);
                    }
                } else
                {
                    storage.CreateDirectory(dirPath);
                }

                string cacheFilePath = GetFileForCacheKey(cacheKey);
                using (var f = new IsolatedStorageFileStream(cacheFilePath, FileMode.Create, FileAccess.Write, storage))
                {
                    using (var sw = new StreamWriter(f))
                    {
                        sw.Write(responseContent);
                    }
                }
            }
        }
    }
}

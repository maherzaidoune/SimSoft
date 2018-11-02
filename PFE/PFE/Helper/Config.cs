using System;
using System.Reactive.Linq;
using Akavache;
using Plugin.SecureStorage;

namespace PFE.Helper
{
    public class Config
    {

        public static string URL
        {
            get
            {
                try
                {
                    string url = BlobCache.UserAccount.GetObject<String>("URL").Wait();
                    if (string.IsNullOrEmpty(url)){
                        return "http://localhost";
                    }else{
                        if(url.Contains("http://")){
                            return url.ToString();
                        }else{
                            return "http://" + url;
                        }
                    }


                }
                catch
                {
                    return "http://localhost";
                }
            }

            set
            {
                 BlobCache.UserAccount.Invalidate("port");
                BlobCache.UserAccount.InsertObject("URL", value);
            }
        }

        public static string port
        {
            get
            {
                try
                {
                    string url = BlobCache.UserAccount.GetObject<string>("port").Wait();
                    //working :)
                    if (string.IsNullOrEmpty(url)){
                        return "3000";
                    }
                    return url;
                }
                catch
                {
                    return "3000";
                }
            }

            set
            {
                BlobCache.UserAccount.Invalidate("port");
                BlobCache.UserAccount.InsertObject("port", value);
            }
        }
    }
}

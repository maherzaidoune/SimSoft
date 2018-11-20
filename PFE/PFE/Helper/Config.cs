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
                        return "localhost";
                    }else{
                        return url.ToString();
                    }


                }
                catch
                {
                    return "localhost";
                }
            }

            set
            {
                 BlobCache.UserAccount.Invalidate("URL");
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

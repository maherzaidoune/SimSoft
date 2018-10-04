using System;
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
                    //working :)
                    if(string.IsNullOrEmpty(CrossSecureStorage.Current.GetValue("URL"))){
                        return "http://localhost";
                    }else{
                        if(CrossSecureStorage.Current.GetValue("URL").Contains("http://")){
                            return CrossSecureStorage.Current.GetValue("URL");
                        }else{
                            return "http://" + CrossSecureStorage.Current.GetValue("URL");
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
                CrossSecureStorage.Current.SetValue("URL", value);
            }
        }

        public static string port
        {
            get
            {
                try
                {
                    //working :)
                    if(string.IsNullOrEmpty(CrossSecureStorage.Current.GetValue("port"))){
                        return "3000";
                    }
                    return CrossSecureStorage.Current.GetValue("port");
                }
                catch
                {
                    return "3000";
                }
            }

            set
            {
                CrossSecureStorage.Current.SetValue("port", value);
            }
        }
    }
}

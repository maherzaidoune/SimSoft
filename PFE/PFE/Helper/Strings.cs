using System;
namespace PFE.Helper
{
    public class Strings
    {
        public static string getNum(int PCDID,string type)
        {
            var num = type + "_";
            int lenth = 8 - PCDID.ToString().Length;
            for (int i = 0;i < lenth ;i++){
                num += "0";
            }
            return num + PCDID.ToString();
        }
    }
}

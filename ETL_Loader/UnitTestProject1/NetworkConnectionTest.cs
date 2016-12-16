using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL_Loader.Util;
using System.IO;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class NetworkConnectionTest
    {
        [TestMethod]
        public void DownloadFileTest()
        {
            //using (new NetworkConnection(@"\\172.16.99.135", new NetworkCredential(@"c3-1-7-O-10347\wncmis5600", "back__space26495")))
            using (new NetworkConnection(@"\\172.16.99.135", new NetworkCredential(@"WNEWEB\10509014", "pony0110")))
            {
                string[] dirs = Directory.GetDirectories(@"\\172.16.99.135\Pony_Test");
                Console.WriteLine(dirs.Length);
            }

            //NetworkCredential theNetworkCredential = new NetworkCredential(@"EDSAAPTEST\Administrator", "p@ssw0rd");
            //CredentialCache theNetCache = new CredentialCache();
            //theNetCache.Add(new Uri(@"\\172.16.99.135"), "Basic", theNetworkCredential);
            //string[] theFolders = Directory.GetDirectories(@"\\172.16.99.135\Pony_Test");

            //using (NetworkShareAccesser.Access("172.16.99.135", "EDSAAPTEST", "Administrator", "p@ssw0rd"))
            //{
            //    string[] dirs = Directory.GetDirectories(@"\\172.16.99.135\Pony_Test");
            //    Console.WriteLine(dirs.Length);
            //}



            Console.WriteLine("");
        }
    }
}

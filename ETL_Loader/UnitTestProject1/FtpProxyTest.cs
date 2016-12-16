using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL_Loader.Proxy;
using System.IO;
namespace UnitTestProject1
{
    [TestClass]
    public class FtpProxyTest
    {
        FtpProxy ftpPxy = null;

        [TestInitialize]
        public void Init()
        {
            ftpPxy = new FtpProxy("t1test.wneweb.com.tw", 21, "edsaadmin", "wncedsa3.0");
            //ftpPxy.Connect("t1test.wneweb.com.tw", 21, "edsaadmin", "wncedsa3.0");
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    ftpPxy.Close();
        //}

        //[TestMethod]
        //public void TestConnect()
        //{
        //    ftpPxy = new FtpProxy();
        //    ftpPxy.Connect("t1test.wneweb.com.tw", 21, "edsaadmin", "wncedsa3.0");
        //    ftpPxy.Close();
        //}
        //[TestMethod]
        //public void TestConnectMethod()
        //{
        //    bool isConnected = ftpPxy.Connected();
        //    Assert.IsTrue(isConnected, "connect fail.");
        //}

        [TestMethod]
        public void TestDownloadFile()
        {

            ftpPxy.Download("/EDSA/Json_UB2GG4200370C01_20161014212733.txt", "Temp/Json_UB2GG4200370C01_20161014212733.txt");
            Assert.IsTrue(File.Exists("Temp/Json_UB2GG4200370C01_20161014212733.txt"));
            File.Delete("Temp/Json_UB2GG4200370C01_20161014212733.txt");
        }

        [TestMethod]
        public void TestUploadFile()
        {
            byte[] data = System.Text.Encoding.Default.GetBytes("abc");
            ftpPxy.Upload("/EDSA/Test.txt", data);

        }

        //[TestMethod]
        //public void TestList()
        //{

        //    ftpPxy.ChangeFolder("/");
        //    int count = ftpPxy.List().Length;
        //    Assert.AreNotSame(0, count, "List fail.");

        //}

        //[TestMethod]
        //public void TestDownloadFile()
        //{
        //    StreamReader reader = ftpPxy.DownloadFile("/EDSA/Json_UB2GG4200370C01_20161014212733.txt");
        //    Assert.IsNotNull(reader);
        //    string line = reader.ReadLine();

        //    Assert.IsFalse(string.IsNullOrEmpty(line),"Line : [" + line + "]");
        //    reader.Close();
        //}

        //[TestMethod]
        //public void TestChangeFolder()
        //{
        //    try
        //    {

        //        ftpPxy.ChangeFolder("/");
        //        int count = ftpPxy.List().Length;
        //        Assert.AreNotSame(0, count,"Change folder fail.");
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("FTP change folder test fail. {0}", e);
        //    }
        //}

        //[TestMethod]
        //public void TestFileExists()
        //{
        //    bool exists = ftpPxy.FileExists("/EDSA/Json_UB2GG4200370C01_20161014212733.txt");
        //    Assert.IsTrue(exists);

        //    exists = ftpPxy.FileExists("/EDSA/abc.txt");
        //    Assert.IsFalse(exists);
        //}
    }
}

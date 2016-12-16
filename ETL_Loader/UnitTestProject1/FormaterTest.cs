using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL_Loader.Formater;
using System.Data;

namespace UnitTestProject1
{
    /// <summary>
    /// FormaterTest 的摘要描述
    /// </summary>
    [TestClass]
    public class FormaterTest
    {
        public FormaterTest()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合
        ///相關資訊與功能的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫測試: 
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void JsonFormaterTest()
        {
            string str = "[{\"id\":\"1\",\"name\":\"id1\",\"Time\":\"2016-10-10 00:00:00\"},{\"id\":\"2\",\"name\":\"id2\",\"Time\":\"2016-10-10 00:00:00\"}]";
            DataTable dt = JsonFormater.ConvertToDataTable(str,"Time","yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(dt.Columns[2].DataType);
            Assert.AreEqual(dt.Columns[2].DataType, typeof(DateTime));
            //if (dt.Columns[2].DataType == typeof(DateTime)
            Assert.AreEqual(dt.Columns.Count,3,"Column count no match");
            Assert.AreEqual(dt.Rows.Count, 2, "Row count no match");

            string str2 = JsonFormater.ConvertToString(dt);
            Assert.AreEqual<string>(str, str2, "DataTable to string data error.");
        }
    }
}

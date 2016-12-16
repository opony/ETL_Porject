using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLib.Database;
using ETL_Loader.Dao;
using System.Data;

namespace UnitTestProject1
{
    [TestClass]
    public class CommonDaoTest
    {
        CommonDao cmmDao = null;
        [TestInitialize]
        public void init()
        {
            //var connection = System.Configuration.ConfigurationManager.ConnectionStrings["SFCS"].ConnectionString;

            OracleDatabase db = DatabaseFactory.GetOracleDatabaseByConfigEncrypt("SFCS");

            cmmDao = new CommonDao(db);

        }

        private DataTable GetSampleData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("TIME_STAMP",typeof(DateTime));
            

            DataRow row = dt.NewRow();
            row["ID"] = 1;
            row["NAME"] = "Test";
            row["TIME_STAMP"] = DateTime.Now ;
            dt.Rows.Add(row);

            return dt;
        }

        [TestMethod]
        public void InsertTest()
        {
            DataTable dt = GetSampleData();
            cmmDao.Insert(dt, "ETL_TEST", null);

            DataTable result = cmmDao.Query(new string[] { "ID", "NAME"}, "ETL_TEST");
            Assert.AreEqual(dt.Rows.Count, result.Rows.Count);
        }
    }
}

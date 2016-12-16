using ETL_Interface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ETL_Loader.Vo;
using DatabaseLib.Database;
using ETL_Loader.Dao;
using log4net;

namespace ETL_Loader.Source
{
    class OracleDbSource : ADbSource
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        CommonDao comDao = null;
        public OracleDbSource(DateTime jobExecTime, FsETLJob etlJob, FsETLSetting setting) : base(jobExecTime, etlJob, setting)
        {
            logger.InfoFormat("Connection to db . IP: [{0}], Port: [{1}], Service Name: [{2}], user: [{3}]", setting.IP, setting.Port, setting.DbServiceName, setting.UserName);
            
            OracleDatabase db = DatabaseFactory.GetOracleDatabase(setting.IP, setting.Port, setting.DbServiceName, setting.UserName, setting.Pwd);
            comDao = new CommonDao(db);
        }

        public override DataTable GetData()
        {
            logger.Info("----- OracleDbSource.GetData start -----");
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(setting.DbColumn))
                {
                    throw new Exception("Query [DbColumn] no setting. can't to query db.");
                }

                if (string.IsNullOrEmpty(setting.DbTable))
                {
                    throw new Exception("Query DB [Table] no setting. can't to query db.");
                }

                if (stTime.HasValue)
                {
                    logger.DebugFormat("Column : [{0}], Table : [{1}], Time Column : [{2}], Start : [{3}], End : [{4}]"
                        , setting.DbColumn, setting.DbTable, setting.DbTimeColumn, stTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                    dt = comDao.Query(setting.DbColumn.Split(','), setting.DbTable, setting.DbTimeColumn, stTime.Value, endTime.Value);
                }
                else
                {
                    logger.DebugFormat("Column : [{0}], Table : [{1}]"
                        , setting.DbColumn, setting.DbTable);

                    dt = comDao.Query(setting.DbColumn.Split(','), setting.DbTable);
                }

                logger.InfoFormat("Data count : {0}", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query db fail.", ex);
                throw ex;
            }

            logger.Info("----- OracleDbSource.GetData end -----");
            return dt;
        }
    }
}

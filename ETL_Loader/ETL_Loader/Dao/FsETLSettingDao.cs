using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.Database;
using ETL_Loader.Vo;
using Oracle.ManagedDataAccess.Client;
using DatabaseLib.Util;
using log4net;

namespace ETL_Loader.Dao
{
    class FsETLSettingDao : ADaoBasic
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FsETLSettingDao(OracleDatabase db) : base(db)
        {
        }

        public List<FsETLSetting> QueryJobSetting(bool enabled)
        {
            logger.Info("----- QueryJobSetting Start -----");
            List<FsETLSetting> setList = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT JOB_ID, GROUP_ID, SOURCE, JOB_TYPE, DB_TYPE, DB_TABLE, DB_TIME_COLUMN, DB_COLUMN, DB_SERVICE_NAME, IP, PORT, USER_NAME, PWD, FILE_FORMAT, FILE_LOCAL ")
                    .AppendLine("FROM FS_ETL_SETTING")
                    .AppendLine("WHERE JOB_ID in (")
                    .AppendLine("   SELECT JOB_ID FROM FS_ETL_JOB")
                    .AppendLine("   WHERE ENABLED ='" + (enabled? "1":"0")+ "')");

                logger.DebugFormat("SQL : {0}", sql.ToString());
                OracleCommand cmd = new OracleCommand(sql.ToString());
                db.GetConnection();
                setList = db.ExecuteResult(cmd).ConvertToList<FsETLSetting>();
                logger.InfoFormat("Query data count : {0}", setList.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.",ex);
                throw ex;
            }
            finally
            {
                this.db.Close();
            }

            logger.Info("----- QueryJobSetting End -----");
            return setList;
        }
    }
}

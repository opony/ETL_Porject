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
    public class FsETLJobDao : ADaoBasic
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FsETLJobDao(OracleDatabase db) : base(db)
        {
        }

        public List<FsETLJob> Query(bool enabled)
        {
            logger.Info("----- Query Start -----");
            List<FsETLJob> jobList = null;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT JOB_ID, DESCRIPTION, LAST_EXEC_TIME, ENABLED, STATUS, PERIOD, PERIOD_TYPE, TIME_OUT, MAIL_TO, CREATED_BY, CREATED_TIME ")
                    .AppendLine("FROM FS_ETL_JOB")
                    .AppendLine("WHERE ENABLED = '" + (enabled ? "1":"0") + "'");

                logger.DebugFormat("SQL : {0}", sql.ToString());
                OracleCommand cmd = new OracleCommand(sql.ToString());
                
                this.db.GetConnection();
                jobList = db.ExecuteResult(cmd).ConvertToList<FsETLJob>();
                logger.InfoFormat("Query data count : {0}", jobList.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.", ex);
                throw ex;
            }
            finally
            {
                this.db.Close();
            }
            logger.Info("----- Query End -----");
            return jobList;
        }
    }
}

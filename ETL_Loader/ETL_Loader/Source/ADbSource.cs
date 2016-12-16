using ETL_Interface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ETL_Loader.Vo;
using DatabaseLib.DataBase;
using DatabaseLib.Database;
using ETL_Loader.Dao;

namespace ETL_Loader.Source
{
    public abstract class ADbSource : ISource
    {
        protected FsETLSetting setting;
        protected FsETLJob etlJob;
        protected DateTime? stTime;
        protected DateTime? endTime;
        public ADbSource(DateTime jobExecTime, FsETLJob etlJob, FsETLSetting setting)
        {
            this.etlJob = etlJob;
            this.setting = setting;
            if (string.IsNullOrEmpty(setting.DbTimeColumn) == false)
            {
                endTime = DateTime.Parse(jobExecTime.ToString("yyyy-MM-dd HH:mm:00"));
                if (etlJob.LastExecTime != null)
                {
                    stTime = etlJob.LastExecTime;
                }
            }
        }


        public abstract DataTable GetData();

        //public abstract DataTable GetData()
        //{
        //    ADatabase db = null;

        //    if (setting.DbType.ToUpper() == "ORACLE")
        //    {
        //        db = DatabaseFactory.GetOracleDatabase(setting.IP, setting.Port, setting.DbServiceName, setting.UserName, setting.Pwd);
        //        CommonDao comDao = new CommonDao((OracleDatabase)db);
                

        //    }

        //    return null;
        //}
    }
}

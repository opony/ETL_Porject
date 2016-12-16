using ETL_Interface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ETL_Loader.Vo;
using ETL_Loader.Proxy;
using ETL_Loader.Formater;
using log4net;

namespace ETL_Loader.Source
{
    class FileSource : ISource
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FsETLSetting etlSetting = null;

        public FileSource(FsETLSetting setting)
        {
            this.etlSetting = setting;
        }

        DataTable ISource.GetData()
        {
            logger.Info("----- GetData Start -----");
            
            DataTable dt = null;
            if (etlSetting.IP.StartsWith("ftp:"))
            {
                dt = GetFromFtp();
            }
            logger.InfoFormat("Query data count : {0}", dt.Rows.Count);
            logger.Info("----- GetData End -----");
            return dt;
        }

        private DataTable GetFromFtp()
        {
            DataTable dt = null;
            logger.DebugFormat("IP: [{0}], Port: [{1}], User: [{2}]",etlSetting.IP, etlSetting.Port, etlSetting.UserName);

            FtpProxy ftPxy = new FtpProxy(etlSetting.IP, etlSetting.Port, etlSetting.UserName, etlSetting.Pwd);
            logger.InfoFormat("Download file : {0}", etlSetting.FileLocal);

            byte[] datas = ftPxy.Download(etlSetting.FileLocal);
            logger.DebugFormat("Downloaded file size : {0}", datas.Length);

            string str = System.Text.Encoding.Default.GetString(datas);
            string fiFor = etlSetting.FileFormat.ToUpper();
            logger.InfoFormat("Source file format : {0}", fiFor);
            try
            {
                if (fiFor == "JSON")
                {
                    dt = JsonFormater.ConvertToDataTable(str, etlSetting.DbTimeColumn);

                }
                else if (fiFor == "CSV")
                {
                    dt = CsvFormater.ConvertToDataTable(str, etlSetting.DbTimeColumn);
                }

                if (dt == null)
                    throw new Exception("Convert the DataTable is null.");
            }
            catch (Exception ex)
            {

                throw new Exception("Source file convert to DataTable fail.",ex);
            }
            

            logger.DebugFormat("Convert to DataTable row count : {0}", dt.Rows.Count);

            return dt;
        }
    }
}

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

namespace ETL_Loader.Target
{
    class FileTarget : ITarget
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FsETLSetting etlSetting = null;

        public FileTarget(FsETLSetting etlSetting)
        {
            this.etlSetting = etlSetting;
        }


        public void Input(DataTable srcTb)
        {
            logger.Info("----- FileTarget.Input Start -----");
            string data = "";
            logger.DebugFormat("Target file format : [{0}]", etlSetting.FileFormat);
            if (etlSetting.FileFormat.ToUpper() == "JSON")
            {
                data = JsonFormater.ConvertToString(srcTb);
            }

            logger.DebugFormat("Target IP: [{0}], Port: [{1}], User: [{2}]", etlSetting.IP, etlSetting.Port, etlSetting.UserName);
            if (etlSetting.IP.StartsWith("ftp:"))
            {
                FtpProxy ftpPxy = new FtpProxy(etlSetting.IP, etlSetting.Port, etlSetting.UserName, etlSetting.Pwd);
                byte[] databytes = System.Text.Encoding.Default.GetBytes(data);
                logger.InfoFormat("Upload data length : {0}", databytes.Length);
                ftpPxy.Upload(etlSetting.FileLocal, databytes);
            }

            logger.Info("----- FileTarget.Input End -----");
        }
    }
}

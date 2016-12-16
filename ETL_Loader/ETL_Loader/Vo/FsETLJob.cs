using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.Attribute;
namespace ETL_Loader.Vo
{
    public class FsETLJob
    {
        [TableColumn(Name = "JOB_ID")]
        public string JobID
        { get; set; }

        [TableColumn(Name = "DESC")]
        public string Desc
        { get; set; }

        [TableColumn(Name = "LAST_EXEC_TIME")]
        public DateTime? LastExecTime
        { get; set; }

        [TableColumn(Name = "ENABLED")]
        public int Enabled
        { get; set; }

        [TableColumn(Name = "STATUS")]
        public string status
        { get; set; }

        [TableColumn(Name = "PERIOD")]
        public int Period
        { get; set; }

        /// <summary>
        /// [Month, Week, Day, Min]
        /// </summary>
        [TableColumn(Name = "PERIOD_TYPE")]
        public string PeriodType
        { get; set; }

        /// <summary>
        /// Job timeout limited (mins)
        /// </summary>
        [TableColumn(Name = "TIME_OUT")]
        public int Timeout
        { get; set; }

        /// <summary>
        /// While the job had error then mail to 
        /// </summary>
        [TableColumn(Name = "MAIL_TO")]
        public string MailTo
        { get; set; }

        [TableColumn(Name = "CREATED_BY")]
        public string CreatedBy
        { get; set; }

        [TableColumn(Name = "CREATED_TIME")]
        public DateTime CreatedTime
        { get; set; }


        public List<FsETLSetting> ETLSetList;
    }
}

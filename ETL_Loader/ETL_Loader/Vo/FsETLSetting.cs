using DatabaseLib.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Vo
{
    public class FsETLSetting
    {
        [TableColumn(Name = "JOB_ID")]
        public string JobID
        { get; set; }

        [TableColumn(Name = "GROUP")]
        public int Group
        { get; set; }

        [TableColumn(Name = "SOURCE")]
        public string Source
        { get; set; }

        /// <summary>
        /// [DB, FILE, SQL]
        /// </summary>
        [TableColumn(Name = "JOB_TYPE")]
        public string JobType
        { get; set; }

        /// <summary>
        /// [Oracle, MySql, MS SQL]
        /// </summary>
        [TableColumn(Name = "DB_TYPE")]
        public string DbType
        { get; set; }

        /// <summary>
        /// DB table name or Sql statement
        /// </summary>
        [TableColumn(Name = "DB_TABLE")]
        public string DbTable
        { get; set; }

        /// <summary>
        /// timestamp column name
        /// if the value is null, then it do full table scan
        /// </summary>
        [TableColumn(Name = "DB_TIME_COLUMN")]
        public string DbTimeColumn
        { get; set; }

        /// <summary>
        /// Query columns, 用逗號(,)隔開
        /// </summary>
        [TableColumn(Name = "DB_COLUMN")]
        public string DbColumn
        { get; set; }

        [TableColumn(Name = "DB_SERVICE_NAME")]
        public string DbServiceName
        { get; set; }

        [TableColumn(Name = "IP")]
        public string IP
        { get; set; }

        [TableColumn(Name = "PORT")]
        public int Port
        { get; set; }

        [TableColumn(Name = "USER_NAME")]
        public string UserName
        { get; set; }

        [TableColumn(Name = "PWD")]
        public string Pwd
        { get; set; }

        [TableColumn(Name = "FILE_FORMAT")]
        public string FileFormat
        { get; set; }

        [TableColumn(Name = "FILE_LOCAL")]
        public string FileLocal
        { get; set; }
    }
}

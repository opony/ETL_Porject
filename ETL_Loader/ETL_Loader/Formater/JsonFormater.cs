using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Formater
{
    public class JsonFormater
    {
        public static DataTable ConvertToDataTable(string str)
        {
           return (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
        }


        public static DataTable ConvertToDataTable(string str, string dateTimeColumn, string dateTimeFormat = "yyyy-mm-dd HH:mi:ss")
        {
            DataTable srcDt = (DataTable)JsonConvert.DeserializeObject(str, (typeof(DataTable)));
            DataTable targDt = new DataTable();
            string[] columnNames = srcDt.Columns.Cast<DataColumn>()
                                  .Select(x => x.ColumnName)
                                  .ToArray();

            foreach (string colName in columnNames)
            {
                DataColumn col = new DataColumn(colName);
                if (colName == dateTimeColumn)
                    col.DataType = typeof(DateTime);
                else
                    col.DataType = System.Type.GetType("System.String");

                targDt.Columns.Add(col);
                
            }
            DataRow newRow = null;
            DateTime time;
            CultureInfo provider = CultureInfo.InvariantCulture;
            foreach (DataRow srcRow in srcDt.Rows)
            {
                newRow = targDt.NewRow();
                foreach (DataColumn col in srcDt.Columns)
                {
                    if (col.ColumnName == dateTimeColumn)
                    {
                        
                        if (DateTime.TryParseExact(srcRow[col].ToString(), dateTimeFormat, provider,DateTimeStyles.None, out time))
                        {
                            newRow[col.ColumnName] = time;
                        }
                    }
                    else
                    {
                        newRow[col.ColumnName] = srcRow[col.ColumnName];
                    }
                }

                targDt.Rows.Add(newRow);
            }
            

            return targDt;
        }

        public static string ConvertToString(DataTable dt, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            List<Dictionary<string, object>> rowList = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        if (dr[col] != DBNull.Value)
                        {
                            row.Add(col.ColumnName, ((DateTime)dr[col]).ToString(dateTimeFormat));
                        }
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    
                }

                rowList.Add(row);
            }

            return JsonConvert.SerializeObject(rowList);
        }
    }
}

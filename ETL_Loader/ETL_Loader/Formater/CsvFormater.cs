using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Formater
{
    class CsvFormater
    {
        public static DataTable ConvertToDataTable(string str, string dateTimeColumn, string dateTimeFormat = "yyyy-mm-dd HH:mm:ss")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("Convert to datatable fail, file is empty.");
            }
            byte[] byteArray = Encoding.Default.GetBytes(str);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);
            string line = reader.ReadLine();
            string[] columns = GetCsvDatas(line);

            DataTable dt = new DataTable();
            foreach (string column in columns)
            {
                dt.Columns.Add(column);
            }
            string[] datas;
            DataRow row = null;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string value;
            DateTime time;
            while ((line = reader.ReadLine()) != null)
            {
                datas = GetCsvDatas(line);
                row = dt.NewRow();
                foreach (string column in columns)
                {
                    value = datas[Array.IndexOf(columns, column)];
                    if (column == dateTimeColumn)
                    {
                        if (DateTime.TryParseExact(value, dateTimeFormat, provider, DateTimeStyles.None, out time))
                        {
                            row[column] = time;
                        }
                    }
                    else
                    {
                        row[column] = value;
                    }
                }

                dt.Rows.Add(row);
            }


            return dt;

        }


        private static string[] GetCsvDatas(string line)
        {
            string tempLine;
            string[] columns;
            if (line.StartsWith("\""))
            {
                tempLine = line.Substring(1).Substring(0, line.LastIndexOf("\"") - 1);
                columns = tempLine.Split(new string[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);

            }
            else
            {
                columns = line.Split(',');
            }

            return columns;
        }
        public static string ConvertToString(DataTable dt, string dateTimeColumn, string dataTimeFormat = "yyyy-mm-dd HH:mm:ss")
        {
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                  .Select(x => "\"" + x.ColumnName + "\"")
                                  .ToArray();
            StringBuilder csvStr = new StringBuilder();
            csvStr.AppendLine(string.Join(",",columnNames));

            List<string> rowDataList = null;
            foreach (DataRow row in dt.Rows)
            {
                rowDataList = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == dateTimeColumn && row[col] != null)
                    {
                        rowDataList.Add("\"" + ((DateTime)row[col]).ToString(dataTimeFormat) + "\"");
                    }
                    else
                    {
                        rowDataList.Add("\"" + row[col] + "\"");
                    }
                }

                csvStr.AppendLine(string.Join(",", rowDataList));

            }

            return csvStr.ToString();

        }
    }
}

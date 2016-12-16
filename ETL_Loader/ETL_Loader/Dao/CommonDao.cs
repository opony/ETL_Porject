using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.Database;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using log4net;

namespace ETL_Loader.Dao
{
    public class CommonDao : ADaoBasic
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Dictionary<Type, OracleDbType> oraTypeMap = new Dictionary<Type, OracleDbType>();
        public CommonDao(OracleDatabase db) : base(db)
        {
            oraTypeMap[typeof(short)] = OracleDbType.Int16;
            oraTypeMap[typeof(int)] = OracleDbType.Int32;
            oraTypeMap[typeof(long)] = OracleDbType.Int64;
            oraTypeMap[typeof(double)] = OracleDbType.Double;
            oraTypeMap[typeof(decimal)] = OracleDbType.Decimal;
            oraTypeMap[typeof(string)] = OracleDbType.Varchar2;
            oraTypeMap[typeof(DateTime)] = OracleDbType.TimeStamp;
            oraTypeMap[typeof(byte?)] = OracleDbType.Byte;
            oraTypeMap[typeof(short?)] = OracleDbType.Int16;
            oraTypeMap[typeof(int?)] = OracleDbType.Int32;
            oraTypeMap[typeof(long?)] = OracleDbType.Int64;
            oraTypeMap[typeof(float?)] = OracleDbType.Single;
            oraTypeMap[typeof(double?)] = OracleDbType.Double;
            oraTypeMap[typeof(decimal?)] = OracleDbType.Decimal;
            oraTypeMap[typeof(DateTime?)] = OracleDbType.TimeStamp;
            

        }

        public DataTable Query(string[] columns, string table)
        {
            logger.Info("----- Query Start -----");
            DataTable dt;
            try
            {
                string sql = "SELECT " + string.Join(", ", columns) + " FROM " + table;
                logger.DebugFormat("SQL : {0}",sql);

                OracleCommand cmd = new OracleCommand(sql);
                db.GetConnection();
                dt = db.ExecuteResult(cmd);

                logger.InfoFormat("Query data count : {0}", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.",ex);
                throw ex;
            }
            finally
            {
                db.Close();
            }
            logger.Info("----- Query End -----");
            return dt;
        }


        public DataTable Query(string[] columns, string table, string timeColumn, DateTime stTime, DateTime endTime)
        {
            logger.Info("----- Query Start -----");
            DataTable dt;
            try
            {
                string sql = "SELECT " + string.Join(", ", columns) + " FROM " + table
                    + " WHERE " + timeColumn + " between " + stTime.ToString("yyyy-MM-dd HH:mm:ss")
                    + " AND " + endTime.ToString("yyyy-MM-dd HH:mm:ss");

                logger.DebugFormat("SQL : {0}", sql);

                OracleCommand cmd = new OracleCommand(sql);
                db.GetConnection();
                dt = db.ExecuteResult(cmd);
                logger.InfoFormat("Query data count : {0}", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.", ex);
                throw ex;
            }
            finally
            {
                db.Close();
            }
            logger.Info("----- Query End -----");
            return dt;
        }


        public DataTable QueryBySQLStmt(string subSql)
        {
            logger.Info("----- QueryBySQLStmt Start -----");
            DataTable dt;
            try
            {
                string sql = "SELECT * FROM (" + subSql + ")";

                logger.DebugFormat("SQL : {0}", sql);

                OracleCommand cmd = new OracleCommand(sql);
                db.GetConnection();
                dt = db.ExecuteResult(cmd);
                logger.InfoFormat("Query data count : {0}", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.", ex);
                throw ex;
            }
            finally
            {
                db.Close();
            }
            logger.Info("----- QueryBySQLStmt End -----");
            return dt;
        }


        public DataTable QueryBySQLStmt(string[] columns, string subSql, string timeColumn, DateTime stTime, DateTime endTime)
        {
            logger.Info("----- QueryBySQLStmt Start -----");

            DataTable dt;
            try
            {
                string sql = "SELECT " + string.Join(", ", columns) + " FROM (" + subSql + ") "
                    + " WHERE " + timeColumn + " between " + stTime.ToString("yyyy-MM-dd HH:mm:ss")
                    + " AND " + endTime.ToString("yyyy-MM-dd HH:mm:ss");

                logger.DebugFormat("SQL : {0}", sql);

                OracleCommand cmd = new OracleCommand(sql);
                db.GetConnection();
                dt = db.ExecuteResult(cmd);
                logger.InfoFormat("Query data count : {0}", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                logger.Error("Query fail.", ex);
                throw ex;
            }
            finally
            {
                db.Close();
            }

            logger.Info("----- QueryBySQLStmt End -----");

            return dt;
        }

        public void Insert(DataTable srcTb, string tarTableName, string[] tarColumns)
        {
            logger.Info("----- Insert Start -----");
            try
            {
                if (tarColumns == null || tarColumns.Length == 0)
                {
                    tarColumns = srcTb.Columns.Cast<DataColumn>()
                                    .Select(x => x.ColumnName)
                                    .ToArray();
                }

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO ").Append(tarTableName).Append("(").Append(string.Join(",", tarColumns)).Append(")")
                    .Append("VALUES(:").Append(string.Join(",:", tarColumns)).Append(")");

                this.db.GetConnection();
                db.BeginTrans();
                OracleCommand cmd = new OracleCommand(sql.ToString());
                foreach (DataRow row in srcTb.Rows)
                {
                    foreach (string col in tarColumns)
                    {
                        cmd.Parameters.Add(col, oraTypeMap[row[col].GetType()], row[col], ParameterDirection.Input);
                    }
                    db.ExecuteNonQuery(cmd);
                    cmd.Parameters.Clear();
                }

                db.CommitTrans();
            }
            catch (Exception ex)
            {
                db.RollBackTrans();
                logger.Error("Inesrt fail.", ex);
                throw ex;
            }

            logger.Info("----- Insert End -----");
        }

        //private OracleDbType GetType(object value)
        //{
        //    Type valType = typeof(value)
        //}


        
    }
}

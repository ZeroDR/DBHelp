using System;
using System.Collections;
using System.Collections.Generic;        
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using MySql.Data;
using System.Dynamic;
using System.Text;  
using System.Text.RegularExpressions;

namespace DbHelper
{
    /// <summary>
    /// \brief   数据库访问类。
    /// \author  RLM
    /// \date    2014.09.05
    /// </summary>
    public class DBHelper
    {
        private string connectionString;
        public string ConntionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public DBHelper(string ConntionStr, string dataType)
        {
            this.ConntionString = ConntionStr;
            this.DbType = dataType;
        }

        /// <summary>
        /// 数据库类型 
        /// </summary>
        private string dbType;
        public string DbType
        {
            get
            {
                if (dbType == string.Empty || dbType == null)
                {
                    return "Access";
                }
                else
                {
                    return dbType;
                }
            }
            set
            {
                if (value != string.Empty && value != null)
                {
                    dbType = value;
                }
                if (dbType == string.Empty || dbType == null)
                {
                    dbType = ConfigurationSettings.AppSettings["DataType"];
                }
                if (dbType == string.Empty || dbType == null)
                {
                    dbType = "Access";
                }
            }
        }

        #region 转换参数
        private System.Data.IDbDataParameter iDbPara(string ParaName, string DataType)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return GetSqlPara(ParaName, DataType);

                case "Oracle":
                    return GetOleDbPara(ParaName, DataType);

                case "Access":
                    return GetOleDbPara(ParaName, DataType);

                case "SQLite":
                    return GetSQLitePara(ParaName, DataType);

                case "MySQL":
                    return GetMySqlPara(ParaName, DataType);

                default:
                    return GetSqlPara(ParaName, DataType);
            }
        }

        private System.Data.SqlClient.SqlParameter GetSqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Decimal);
                case "Varchar":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.VarChar);
                case "DateTime":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.DateTime);
                case "Iamge":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Image);
                case "Int":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.Int);
                case "Text":
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.NText);
                default:
                    return new System.Data.SqlClient.SqlParameter(ParaName, System.Data.SqlDbType.VarChar);
            }
        }

        private System.Data.OracleClient.OracleParameter GetOraclePara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.Double);

                case "Varchar":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.VarChar);

                case "DateTime":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.DateTime);

                case "Iamge":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.BFile);

                case "Int":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.Int32);

                case "Text":
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.LongVarChar);

                default:
                    return new System.Data.OracleClient.OracleParameter(ParaName, System.Data.OracleClient.OracleType.VarChar);
            }
        }

        private System.Data.OleDb.OleDbParameter GetOleDbPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.Decimal);

                case "Varchar":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.String);

                case "DateTime":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.DateTime);

                case "Iamge":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.Binary);

                case "Int":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.Int32);

                case "Text":
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.String);

                default:
                    return new System.Data.OleDb.OleDbParameter(ParaName, System.Data.DbType.String);
            }
        }

        private System.Data.SQLite.SQLiteParameter GetSQLitePara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.Decimal);

                case "Varchar":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.String);

                case "DateTime":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.DateTime);

                case "Iamge":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.Binary);

                case "Int":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.Int32);

                case "Text":
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.String);

                default:
                    return new System.Data.SQLite.SQLiteParameter(ParaName, System.Data.DbType.String);
            }
        }


        private MySql.Data.MySqlClient.MySqlParameter GetMySqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.Decimal);

                case "Varchar":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.String);

                case "DateTime":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.DateTime);

                case "Iamge":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.Binary);

                case "Int":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.Int32);

                case "Text":
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.String);

                default:
                    return new MySql.Data.MySqlClient.MySqlParameter(ParaName, System.Data.DbType.String);
            }
        }

        #endregion

        #region 创建 Connection 和 Command

        private IDbConnection GetConnection()
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlConnection(this.ConntionString);

                case "Oracle":
                    return new System.Data.OracleClient.OracleConnection(this.ConntionString);
                case "Access":
                    return new System.Data.OleDb.OleDbConnection(this.ConntionString);
                case "SQLite":
                    return new System.Data.SQLite.SQLiteConnection(this.connectionString);
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlConnection(this.connectionString);
                default:
                    return new System.Data.SqlClient.SqlConnection(this.ConntionString);
            }
        }


        private IDbCommand GetCommand(string Sql, IDbConnection iConn)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlCommand(Sql, (SqlConnection)iConn);

                case "Oracle":
                    return new System.Data.OracleClient.OracleCommand(Sql, (OracleConnection)iConn);
                case "Access":
                    return new System.Data.OleDb.OleDbCommand(Sql, (OleDbConnection)iConn);
                case "SQLite":
                    return new System.Data.SQLite.SQLiteCommand(Sql, (SQLiteConnection)iConn);
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlCommand(Sql,(MySql.Data.MySqlClient.MySqlConnection)iConn);
                default:
                    return new System.Data.SqlClient.SqlCommand(Sql, (SqlConnection)iConn);
            }
        }

        private IDbCommand GetCommand()
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlCommand();

                case "Oracle":
                    return new System.Data.OracleClient.OracleCommand();    
                case "Access":
                    return new System.Data.OleDb.OleDbCommand();
                case "SQLite":
                    return new System.Data.SQLite.SQLiteCommand();
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlCommand();
                default:
                    return new System.Data.SqlClient.SqlCommand();
            }
        }

        private IDataAdapter GetAdapater(string Sql, IDbConnection iConn)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlDataAdapter(Sql, (SqlConnection)iConn);
                case "Oracle":
                    return new System.Data.OracleClient.OracleDataAdapter(Sql, (OracleConnection)iConn);
                case "Access":
                    return new System.Data.OleDb.OleDbDataAdapter(Sql, (OleDbConnection)iConn);
                case "SQLite":
                    return new System.Data.SQLite.SQLiteDataAdapter(Sql, (SQLiteConnection)iConn);
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlDataAdapter(Sql,(MySql.Data.MySqlClient.MySqlConnection)iConn);
                default:
                    return new System.Data.SqlClient.SqlDataAdapter(Sql, (SqlConnection)iConn); ;
            }

        }

        private IDataAdapter GetAdapater()
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlDataAdapter();

                case "Oracle":
                    return new System.Data.OracleClient.OracleDataAdapter();
                case "Access":
                    return new System.Data.OleDb.OleDbDataAdapter();
                case "SQLite":
                    return new System.Data.SQLite.SQLiteDataAdapter();
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlDataAdapter();
                default:
                    return new System.Data.SqlClient.SqlDataAdapter();
            }
        }

        private IDataAdapter GetAdapater(IDbCommand iCmd)
        {
            switch (this.DbType)
            {
                case "SqlServer":
                    return new System.Data.SqlClient.SqlDataAdapter((SqlCommand)iCmd);

                case "Oracle":
                    return new System.Data.OracleClient.OracleDataAdapter((OracleCommand)iCmd);

                case "Access":
                    return new System.Data.OleDb.OleDbDataAdapter((OleDbCommand)iCmd);
                case "SQLite":
                    return new System.Data.SQLite.SQLiteDataAdapter((SQLiteCommand)iCmd);
                case "MySQL":
                    return new MySql.Data.MySqlClient.MySqlDataAdapter((MySql.Data.MySqlClient.MySqlCommand)iCmd);
                default:
                    return new System.Data.SqlClient.SqlDataAdapter((SqlCommand)iCmd);
            }
        }
        #endregion

        #region  执行简单SQL语句
        //// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SqlString)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public int ExecuteScalar(string SqlString)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        int rows = (int)iCmd.ExecuteScalar();
                        return rows;
                    }
                    catch (System.Exception E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        //// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>        
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (System.Data.IDbCommand iCmd = GetCommand())
                {
                    iCmd.Connection = iConn;
                    using (System.Data.IDbTransaction iDbTran = iConn.BeginTransaction())
                    {
                        iCmd.Transaction = iDbTran;
                        try
                        {
                            for (int n = 0; n < SQLStringList.Count; n++)
                            {
                                string strsql = SQLStringList[n].ToString();
                                if (strsql.Trim().Length > 1)
                                {
                                    iCmd.CommandText = strsql;
                                    iCmd.ExecuteNonQuery();
                                }
                            }
                            iDbTran.Commit();
                        }
                        catch (System.Exception E)
                        {
                            iDbTran.Rollback();
                            throw new Exception(E.Message);
                        }
                        finally
                        {
                            if (iConn.State != ConnectionState.Closed)
                            {
                                iConn.Close();
                            }
                        }
                    }

                }

            }
        }

        //// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SqlString, string content)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    System.Data.IDataParameter myParameter = this.iDbPara("@content", "Text");
                    myParameter.Value = content;
                    iCmd.Parameters.Add(myParameter);
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }


        /**/
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string SqlString, byte[] fs)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    System.Data.IDataParameter myParameter = this.iDbPara("@content", "Image");
                    myParameter.Value = fs;
                    iCmd.Parameters.Add(myParameter);
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /**/
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SqlString)
        {
            using (System.Data.IDbConnection iConn = GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        object obj = iCmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /**/
        /// <summary>
        /// 执行查询语句，返回IDataAdapter
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>IDataAdapter</returns>
        public IDataAdapter ExecuteReader(string strSQL)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                try
                {
                    System.Data.IDataAdapter iAdapter = this.GetAdapater(strSQL, iConn);
                    return iAdapter;
                }
                catch (System.Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
            }
        }
        /**/
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(sqlString, iConn))
                {
                    DataSet ds = new DataSet();
                    iConn.Open();
                    try
                    {
                        System.Data.IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                        iAdapter.Fill(ds);
                        return ds;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /**/
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="dataSet">要填充的DataSet</param>
        /// <param name="tableName">要填充的表名</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString, DataSet dataSet, string tableName)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(sqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        System.Data.IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                        ((OleDbDataAdapter)iAdapter).Fill(dataSet, tableName);
                        return dataSet;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }


        /**/
        /// <summary>
        /// 执行SQL语句 返回存储过程
        /// </summary>
        /// <param name="sqlString">Sql语句</param>
        /// <param name="dataSet">要填充的DataSet</param>
        /// <param name="startIndex">开始记录</param>
        /// <param name="pageSize">页面记录大小</param>
        /// <param name="tableName">表名称</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString, DataSet dataSet, int startIndex, int pageSize, string tableName)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                try
                {
                    System.Data.IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);

                    ((OleDbDataAdapter)iAdapter).Fill(dataSet, startIndex, pageSize, tableName);

                    return dataSet;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
            }
        }


        /**/
        /// <summary>
        /// 执行查询语句，向XML文件写入数据
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="xmlPath">XML文件路径</param>
        public void WriteToXml(string sqlString, string xmlPath)
        {
            Query(sqlString).WriteXml(xmlPath);
        }

        /**/
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="SqlString">查询语句</param>
        /// <returns>DataTable </returns>
        public DataTable ExecuteQuery(string sqlString)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                //System.Data.IDbCommand iCmd  =  GetCommand(sqlString,iConn);
                DataSet ds = new DataSet();
                try
                {
                    System.Data.IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                    iAdapter.Fill(ds);
                }
                catch (System.Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
                return ds.Tables[0];
            }
        }

        /**/
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="SqlString">查询语句</param>
        /// <returns>DataTable </returns>
        public DataTable ExecuteQuery(string SqlString, string Proc)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iCmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    try
                    {
                        System.Data.IDataAdapter iDataAdapter = this.GetAdapater(SqlString, iConn);
                        iDataAdapter.Fill(ds);
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                    return ds.Tables[0];
                }


            }
        }

        /**/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public DataView ExeceuteDataView(string Sql)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                using (System.Data.IDbCommand iCmd = GetCommand(Sql, iConn))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        System.Data.IDataAdapter iDataAdapter = this.GetAdapater(Sql, iConn);
                        iDataAdapter.Fill(ds);
                        return ds.Tables[0].DefaultView;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        #endregion

        #region 执行带参数的SQL语句
        /**/
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params IDataParameter[] iParms)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                System.Data.IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        int rows = iCmd.ExecuteNonQuery();
                        iCmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Exception E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }


        /**/
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (IDbTransaction iTrans = iConn.BeginTransaction())
                {
                    System.Data.IDbCommand iCmd = GetCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            IDataParameter[] iParms = (IDataParameter[])myDE.Value;
                            PrepareCommand(out iCmd, iConn, iTrans, cmdText, iParms);
                            int val = iCmd.ExecuteNonQuery();
                            iCmd.Parameters.Clear();
                        }
                        iTrans.Commit();
                    }
                    catch
                    {
                        iTrans.Rollback();
                        throw;
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }

                }
            }
        }


        /**/
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, params IDataParameter[] iParms)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                System.Data.IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        object obj = iCmd.ExecuteScalar();
                        iCmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /**/
        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns> IDataReader </returns>
        public IDataReader ExecuteReader(string SQLString, params IDataParameter[] iParms)
        {
            System.Data.IDbConnection iConn = this.GetConnection();
            {
                System.Data.IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        System.Data.IDataReader iReader = iCmd.ExecuteReader();
                        iCmd.Parameters.Clear();
                        return iReader;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /**/
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString, params IDataParameter[] iParms)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                IDbCommand iCmd = GetCommand();
                {
                    PrepareCommand(out iCmd, iConn, null, sqlString, iParms);
                    try
                    {
                        IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                        DataSet ds = new DataSet();
                        iAdapter.Fill(ds);
                        iCmd.Parameters.Clear();
                        return ds;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }


        /**/
        /// <summary>
        /// 初始化Command
        /// </summary>
        /// <param name="iCmd"></param>
        /// <param name="iConn"></param>
        /// <param name="iTrans"></param>
        /// <param name="cmdText"></param>
        /// <param name="iParms"></param>
        private void PrepareCommand(out IDbCommand iCmd, IDbConnection iConn, System.Data.IDbTransaction iTrans, string cmdText, IDataParameter[] iParms)
        {
            if (iConn.State != ConnectionState.Open)
                iConn.Open();
            iCmd = this.GetCommand();
            iCmd.Connection = iConn;
            iCmd.CommandText = cmdText;
            if (iTrans != null)
                iCmd.Transaction = iTrans;
            iCmd.CommandType = CommandType.Text;//cmdType;
            if (iParms != null)
            {
                foreach (IDataParameter parm in iParms)
                    iCmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作

        /**/
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            System.Data.IDbConnection iConn = this.GetConnection();
            {
                iConn.Open();

                using (SqlCommand sqlCmd = BuildQueryCommand(iConn, storedProcName, parameters))
                {
                    return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }

        /**/
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {

            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                DataSet dataSet = new DataSet();
                iConn.Open();
                System.Data.IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storedProcName, parameters));

                ((SqlDataAdapter)iDA).Fill(dataSet, tableName);
                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="startIndex">开始记录索引</param>
        /// <param name="pageSize">页面记录大小</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, int startIndex, int pageSize, string tableName)
        {

            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                DataSet dataSet = new DataSet();
                iConn.Open();
                System.Data.IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storedProcName, parameters));

                ((SqlDataAdapter)iDA).Fill(dataSet, startIndex, pageSize, tableName);
                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程 填充已经存在的DataSet数据集 
        /// </summary>
        /// <param name="storeProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="dataSet">要填充的数据集</param>
        /// <param name="tablename">要填充的表名</param>
        /// <returns></returns>
        public DataSet RunProcedure(string storeProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                System.Data.IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storeProcName, parameters));

                ((SqlDataAdapter)iDA).Fill(dataSet, tableName);

                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }

                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程并返回受影响的行数
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int RunProcedureNoQuery(string storedProcName, IDataParameter[] parameters)
        {

            int result = 0;
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (SqlCommand scmd = BuildQueryCommand(iConn, storedProcName, parameters))
                {
                    result = scmd.ExecuteNonQuery();
                }

                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
            }

            return result;
        }

        public string RunProcedureExecuteScalar(string storeProcName, IDataParameter[] parameters)
        {
            string result = string.Empty;
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (SqlCommand scmd = BuildQueryCommand(iConn, storeProcName, parameters))
                {
                    object obj = scmd.ExecuteScalar();
                    if (obj == null)
                        result = null;
                    else
                        result = obj.ToString();
                }

                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(IDbConnection iConn, string storedProcName, IDataParameter[] parameters)
        {
            IDbCommand iCmd = GetCommand(storedProcName, iConn);
            iCmd.CommandType = CommandType.StoredProcedure;
            if (parameters == null)
            {
                return (SqlCommand)iCmd;
            }
            foreach (IDataParameter parameter in parameters)
            {
                iCmd.Parameters.Add(parameter);
            }
            return (SqlCommand)iCmd;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数        
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (System.Data.IDbConnection iConn = this.GetConnection())
            {
                int result;
                iConn.Open();
                using (SqlCommand sqlCmd = BuildIntCommand(iConn, storedProcName, parameters))
                {
                    rowsAffected = sqlCmd.ExecuteNonQuery();
                    result = (int)sqlCmd.Parameters["ReturnValue"].Value;

                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                    return result;
                }
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)    
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(IDbConnection iConn, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand sqlCmd = BuildQueryCommand(iConn, storedProcName, parameters);
            sqlCmd.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return sqlCmd;
        }
        #endregion

        #region 数据转换
        /// <summary>
        /// 转换字符串为bool。 字符串 "1" 或者 "true" 映射为布尔值true，其他为布尔值false。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetBool(string value)
        {
            if (value == string.Empty)
                return false;

            else if (value.Trim().Equals("1") || value.ToLower().Trim().Equals("true"))
                return true;

            else
                return false;
        }

        /// <summary>
        /// 转换对象为string类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetStringValue(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return "";
            else return obj.ToString();
        }

        /// <summary>
        /// 转换对象为string类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBooleanValue(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return false;
            string value = obj.ToString();

            if (value == string.Empty)
                return false;

            else if (value.Trim().Equals("1") || value.ToLower().Trim().Equals("true"))
                return true;
            else
                return false;

        }

        /// <summary>
        /// 转换对象为int类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt32Value(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return -1;
            else
            {
                try
                {
                    return Convert.ToInt32(obj);
                }
                catch //(Exception ex)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 转换对象为int64类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 GetInt64Value(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return -1;
            else
            {
                try
                {
                    return Convert.ToInt64(obj);
                }
                catch //(Exception ex)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 转换对象为double类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDoubleValue(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return -1;
            else
            {
                try
                {
                    return Convert.ToDouble(obj);
                }
                catch //(Exception ex)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 转换对象为byte[]类型数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetBytesValue(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return null;
            else
            {
                try
                {
                    return obj as byte[];
                }
                catch// (Exception ex)
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// 通过字符串获取枚举成员实例。
        /// </summary>
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        /// <param name="obj">枚举成员的常量名或常量值,
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"或"0"获取 Enum1.A 枚举类型</param>
        public static T GetEnumValue<T>(object obj)
        {
            if (obj == null || Convert.IsDBNull(obj)) return default(T);
            else
            {
                try
                {
                    return EnumHelp.Instance.GetInstance<T>(obj.ToString());
                }
                catch //(Exception ex)
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 获取数据表第一列的字符数组表示。
        /// </summary>
        /// <param name="dt">需要转换的数据表。</param>
        /// <returns>dt为空时，返回0长数组。</returns>
        public static string[] GetStrings(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return new string[0];
            else
            {
                string[] values = new string[dt.Rows.Count];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = GetStringValue(dt.Rows[i][0]);
                }
                return values;
            }
        }

        /// <summary>
        /// 获取数据表第一列的Int数组表示。
        /// </summary>
        /// <param name="dt">需要转换的数据表。</param>
        /// <returns>dt为空时，返回0长数组。</returns>
        public static int[] GetInts(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return new int[0];
            else
            {
                int[] values = new int[dt.Rows.Count];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = GetInt32Value(dt.Rows[i][0]);
                }
                return values;
            }
        }

        /// <summary>
        /// 创建由字符串分隔符连接的字符数组。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetJoin(string[] values, string segment)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0) builder.Append(segment);
                builder.Append("'" + values[i] + "'");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 创建由字符串分隔符连接的数字数组。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetJoin(int[] values, string segment)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0) builder.Append(segment);
                builder.Append("'" + values[i] + "'");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 创建由字符串分隔符连接的数字数组(不带单引号)。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetJoinNoQuotation(int[] values, string segment)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0) builder.Append(segment);
                builder.Append("" + values[i] + "");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 创建由字符串分隔符连接的数字数组(不带单引号)。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetJoinNoQuotation(string[] values, string segment)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0) builder.Append(segment);
                builder.Append("" + values[i] + "");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 创建由字符串分隔符连接的对象数组。
        /// </summary>
        /// <param name="values"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetJoin(object[] values, string segment)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0) builder.Append(segment);
                builder.Append("'" + values[i].ToString() + "'");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 合并两个表（上下合并）。
        /// </summary>
        /// <param name="dt1">需要合并的表。</param>
        /// <param name="dt2">需要合并的表。</param>
        /// <returns>合并后的表。</returns>
        public static DataTable CombineDataTables(DataTable dt1, DataTable dt2)
        {
            if (dt1 == null || dt2 == null)
            {
                if (dt1 != null)
                    return dt1.Copy();
                else if (dt2 != null)
                    return dt2.Copy();
                else
                    return null;
            }
            else
            {
                DataTable dt;

                if (dt1.Rows.Count > dt2.Rows.Count)
                {
                    dt = dt1.Copy();
                    foreach (DataRow dr in dt2.Rows)
                        dt.ImportRow(dr);
                }
                else
                {
                    dt = dt2.Copy();
                    foreach (DataRow dr in dt1.Rows)
                        dt.ImportRow(dr);
                }

                return dt;
            }
        }

        /// <summary>
        /// 转换字符串到整型。
        /// </summary>
        /// <param name="value">需要转换的字符串。</param>
        /// <returns>转换后的数据。</returns>
        public static int ToInteger(string value)
        {
            int result;
            bool success = int.TryParse(value, out result);

            if (success)
            {
                return result;
            }

            return 0;
        }

        /// <summary>
        /// 转换字符串到布尔类型。
        /// </summary>
        /// <param name="value">需要转换的字符串。</param>
        /// <returns>转换后的数据。</returns>
        public static bool ToBoolean(string value)
        {
            if (value.Equals("0", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (value.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                bool result;
                bool success = bool.TryParse(value, out result);

                if (success)
                {
                    return result;
                }

                return false;
            }
        }

        /// <summary>
        /// 转换字符串到时间类型。
        /// </summary>
        /// <param name="value">需要转换的字符串。</param>
        /// <returns>转换后的数据。</returns>
        public static DateTime ToDateTime(string value)
        {
            DateTime result;
            bool success = DateTime.TryParse(value, out result);

            if (success)
            {
                return result;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// 转换字符串到decimal。
        /// </summary>
        /// <param name="value">需要转换的字符串。</param>
        /// <returns>转换后的数据。</returns>
        public static decimal ToDecimal(string value)
        {
            decimal result;
            bool success = decimal.TryParse(value, out result);

            if (success)
            {
                return result;
            }

            return 0;
        }

        /// <summary>
        /// 转换DataTable对象为dynamic对象。
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(DataTable dataTable)
        {
            List<dynamic> list = new List<dynamic>();
            foreach (DataRow dr in dataTable.Rows)
            {
                dynamic row = new ExpandoObject();
                var d = row as IDictionary<string, object>;
                foreach (DataColumn dc in dataTable.Columns)
                {
                    d[dc.ColumnName] = dr[dc.ColumnName];
                }
                list.Add(row);
            }
            return list;
        }

        /// <summary>
        /// 转换object对象为dynamic对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(object obj)
        {
            dynamic model = new ExpandoObject();
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                try
                {
                    ((IDictionary<string, object>)model)[property.Name] = property.GetValue(obj, null);
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }
            }
            return model;
        }
        #endregion

        public static bool CheckSql(string sql)
        {
            if (sql.IndexOf("DELETE ", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            if (sql.IndexOf("EXEC ", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            if (sql.IndexOf("UPDATE ", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            if (sql.IndexOf("INSERT ", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            if (sql.IndexOf("CREATE ", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            if (sql.IndexOf("--", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                return false;
            }
            return true;
        }

        public static string FilterWhereInfo(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            source = Regex.Replace(source, "insert", "[insert]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "delete", "[delete]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "drop", "[drop]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "update", "[update]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "truncate", "[truncate]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "exec", "[exec]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, ";", "[；]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "-", "[-]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "execute", "[execute]", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "create", "[create]", RegexOptions.IgnoreCase);
            return source;
        }

        public static string FilterSql(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            source = source.Replace("\b", "");
            source = source.Replace("'", "''");
            return source;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_newsupdate_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_newsupdate_db()
        {
            string name = string.Empty;
            string conname = string.Empty;
            ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
            if (connections.Count != 0)
            {
                foreach (ConnectionStringSettings connection in connections)
                {
                    name = connection.Name;
                }
                conname = "" + name + "";
            }
            ConnectionString.ConnectionString = ConfigurationManager.ConnectionStrings[conname].ConnectionString;
        }
        #endregion

        #region Public Methods
        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }
        public DataTable newsupdate_WSSelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_WSSelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }

        public newsupdate SelectById(Int64 newsupdateid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            newsupdate objnewsupdate = new newsupdate();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@newsupdateid", newsupdateid);
                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                {
                                    objnewsupdate.newsupdateid = Convert.ToInt64(ds.Tables[0].Rows[0]["newsupdateid"]);
                                    objnewsupdate.title = Convert.ToString(ds.Tables[0].Rows[0]["title"]);
                                    objnewsupdate.imagename = Convert.ToString(ds.Tables[0].Rows[0]["imagename"]);
                                    objnewsupdate.shortdescp = Convert.ToString(ds.Tables[0].Rows[0]["shortdescp"]);
                                    objnewsupdate.longdescp = Convert.ToString(ds.Tables[0].Rows[0]["longdescp"]);
                                    objnewsupdate.newsdate = Convert.ToString(ds.Tables[0].Rows[0]["newsdate"]);
                                    objnewsupdate.imagename2 = Convert.ToString(ds.Tables[0].Rows[0]["imagename2"]);
                                    objnewsupdate.imagename3 = Convert.ToString(ds.Tables[0].Rows[0]["imagename3"]);
                                    objnewsupdate.imagename4 = Convert.ToString(ds.Tables[0].Rows[0]["imagename4"]);
                                    objnewsupdate.imagename5 = Convert.ToString(ds.Tables[0].Rows[0]["imagename5"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return objnewsupdate;
        }

        public Int64 Insert(newsupdate objnewsupdate)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@newsupdateid";
                param.Value = objnewsupdate.newsupdateid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@title", objnewsupdate.title);
                cmd.Parameters.AddWithValue("@imagename", objnewsupdate.imagename);
                cmd.Parameters.AddWithValue("@shortdescp", objnewsupdate.shortdescp);
                cmd.Parameters.AddWithValue("@longdescp", objnewsupdate.longdescp);
                cmd.Parameters.AddWithValue("@newsdate", objnewsupdate.newsdate);
                cmd.Parameters.AddWithValue("@imagename2", objnewsupdate.imagename2);
                cmd.Parameters.AddWithValue("@imagename3", objnewsupdate.imagename3);
                cmd.Parameters.AddWithValue("@imagename4", objnewsupdate.imagename4);
                cmd.Parameters.AddWithValue("@imagename5", objnewsupdate.imagename5);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt64(param.Value);
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
            finally
            {
                ConnectionString.Close();
            }
            return result;
        }

        public Int64 Update(newsupdate objnewsupdate)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@newsupdateid";
                param.Value = objnewsupdate.newsupdateid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@title", objnewsupdate.title);
                cmd.Parameters.AddWithValue("@imagename", objnewsupdate.imagename);
                cmd.Parameters.AddWithValue("@shortdescp", objnewsupdate.shortdescp);
                cmd.Parameters.AddWithValue("@longdescp", objnewsupdate.longdescp);
                cmd.Parameters.AddWithValue("@newsdate", objnewsupdate.newsdate);
                cmd.Parameters.AddWithValue("@imagename2", objnewsupdate.imagename2);
                cmd.Parameters.AddWithValue("@imagename3", objnewsupdate.imagename3);
                cmd.Parameters.AddWithValue("@imagename4", objnewsupdate.imagename4);
                cmd.Parameters.AddWithValue("@imagename5", objnewsupdate.imagename5);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt64(param.Value);
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
            finally
            {
                ConnectionString.Close();
            }
            return result;
        }

        public bool Delete(Int64 newsupdateid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "newsupdate_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@newsupdateid", newsupdateid);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return false;
            }
            finally
            {
                ConnectionString.Close();
            }
            return true;
        }

        #endregion


    }

}

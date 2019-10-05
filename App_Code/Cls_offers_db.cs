using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_offers_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_offers_db()
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
                cmd.CommandText = "offers_SelectAll";
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

        public DataTable offers_WSSelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "offers_WSSelectAll";
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


        public offers SelectById(Int64 offerid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            offers objoffers = new offers();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "offers_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@offerid", offerid);
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
                                    objoffers.offerid = Convert.ToInt64(ds.Tables[0].Rows[0]["offerid"]);
                                    objoffers.title = Convert.ToString(ds.Tables[0].Rows[0]["title"]);
                                    objoffers.descp = Convert.ToString(ds.Tables[0].Rows[0]["descp"]);
                                    objoffers.imagename = Convert.ToString(ds.Tables[0].Rows[0]["imagename"]);
                                    objoffers.validfrom = Convert.ToString(ds.Tables[0].Rows[0]["validfrom"]);
                                    objoffers.validto = Convert.ToString(ds.Tables[0].Rows[0]["validto"]);
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
            return objoffers;
        }
        public Int64 Insert(offers objoffers)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "offers_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@offerid";
                param.Value = objoffers.offerid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@title", objoffers.title);
                cmd.Parameters.AddWithValue("@descp", objoffers.descp);
                cmd.Parameters.AddWithValue("@imagename", objoffers.imagename);
                cmd.Parameters.AddWithValue("@validfrom", objoffers.validfrom);
                cmd.Parameters.AddWithValue("@validto", objoffers.validto);

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
        public Int64 Update(offers objoffers)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "offers_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@offerid";
                param.Value = objoffers.offerid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@title", objoffers.title);
                cmd.Parameters.AddWithValue("@descp", objoffers.descp);
                cmd.Parameters.AddWithValue("@imagename", objoffers.imagename);
                cmd.Parameters.AddWithValue("@validfrom", objoffers.validfrom);
                cmd.Parameters.AddWithValue("@validto", objoffers.validto);

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
        public bool Delete(Int64 offerid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "offers_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@offerid", offerid);
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

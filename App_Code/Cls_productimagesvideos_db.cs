using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_productimagesvideos_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_productimagesvideos_db()
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
        public DataTable SelectAll(productimagesvideos objproductimagesvideos)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "productimagesvideos_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;


                #region Null checking
                if (objproductimagesvideos.piid != 0 && objproductimagesvideos.piid != null)
                {
                    cmd.Parameters.AddWithValue("@piid", objproductimagesvideos.piid);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@piid", DBNull.Value);
                }

                if (objproductimagesvideos.pid != 0 && objproductimagesvideos.pid != null)
                {
                    cmd.Parameters.AddWithValue("@pid", objproductimagesvideos.pid);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pid", DBNull.Value);
                }

                if (objproductimagesvideos.type != 0 && objproductimagesvideos.type != null)
                {
                    cmd.Parameters.AddWithValue("@type", objproductimagesvideos.type);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@type", DBNull.Value);
                }

                if (objproductimagesvideos.imagevideoname != "" && objproductimagesvideos.imagevideoname != null)
                {
                    cmd.Parameters.AddWithValue("@imagevideoname", objproductimagesvideos.imagevideoname);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@imagevideoname", DBNull.Value);
                }

                if (objproductimagesvideos.imagevideopath != "" && objproductimagesvideos.imagevideopath != null)
                {
                    cmd.Parameters.AddWithValue("@imagevideopath", objproductimagesvideos.imagevideopath);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@imagevideopath", DBNull.Value);
                }

                if (objproductimagesvideos.isdelete != null)
                {
                    cmd.Parameters.AddWithValue("@isdelete", objproductimagesvideos.isdelete);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isdelete", DBNull.Value);
                }
                #endregion

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
        public productimagesvideos SelectById(Int64 piid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            productimagesvideos objproductimagesvideos = new productimagesvideos();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "productimagesvideos_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@piid", piid);
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
                                    objproductimagesvideos.piid = Convert.ToInt64(ds.Tables[0].Rows[0]["piid"]);
                                    objproductimagesvideos.pid = Convert.ToInt64(ds.Tables[0].Rows[0]["pid"]);
                                    objproductimagesvideos.type = Convert.ToInt32(ds.Tables[0].Rows[0]["type"]);
                                    objproductimagesvideos.imagevideoname = Convert.ToString(ds.Tables[0].Rows[0]["imagevideoname"]);
                                    objproductimagesvideos.imagevideopath = Convert.ToString(ds.Tables[0].Rows[0]["imagevideopath"]);
                                    objproductimagesvideos.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
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
            return objproductimagesvideos;
        }
        public Int64 Insert(productimagesvideos objproductimagesvideos)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "productimagesvideos_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@piid";
                param.Value = objproductimagesvideos.piid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@pid", objproductimagesvideos.pid);
                cmd.Parameters.AddWithValue("@type", objproductimagesvideos.type);
                cmd.Parameters.AddWithValue("@imagevideoname", objproductimagesvideos.imagevideoname);
                cmd.Parameters.AddWithValue("@imagevideopath", objproductimagesvideos.imagevideopath);

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
        public Int64 Update(productimagesvideos objproductimagesvideos)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "productimagesvideos_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@piid";
                param.Value = objproductimagesvideos.piid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@pid", objproductimagesvideos.pid);
                cmd.Parameters.AddWithValue("@type", objproductimagesvideos.type);
                cmd.Parameters.AddWithValue("@imagevideoname", objproductimagesvideos.imagevideoname);
                cmd.Parameters.AddWithValue("@imagevideopath", objproductimagesvideos.imagevideopath);
                cmd.Parameters.AddWithValue("@isdelete", objproductimagesvideos.isdelete);

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
        public bool Delete(Int64 piid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "productimagesvideos_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@piid", piid);

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

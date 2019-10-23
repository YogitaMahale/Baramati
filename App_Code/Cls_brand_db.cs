using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_brand_db
/// </summary>

namespace DatabaseLayer
{
    public class Cls_brand_db
    {
        SqlConnection ConnectionString = new SqlConnection();
        public Cls_brand_db()
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

        #region Public Methods


        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "brand_SelectAll",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };
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




        public brandMaster SelectById(Int64 cid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            brandMaster objcategory = new brandMaster();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "brand_SelectById",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };
                cmd.Parameters.AddWithValue("@bid", cid);
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
                                    objcategory.bid = Convert.ToInt64(ds.Tables[0].Rows[0]["bid"]);
                                    objcategory.brandName = Convert.ToString(ds.Tables[0].Rows[0]["brandName"]);
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
            return objcategory;
        }


        public Int64 Insert(brandMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "brand_Insert",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@bid",
                    Value = objcategory.bid,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@brandName", objcategory.brandName);


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

        public Int64 Update(brandMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "brand_Update",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@bid",
                    Value = objcategory.bid,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@brandName", objcategory.brandName);

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
        public bool Delete(Int64 cid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "Brand_Delete",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                cmd.Parameters.AddWithValue("@bid", cid);

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
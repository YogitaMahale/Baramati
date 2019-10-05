using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_category_db
    {
        SqlConnection ConnectionString = new SqlConnection();
        #region Constructor
        public Cls_category_db()
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

        public DataTable SelectAllAdmin()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_SelectAllAdmin";
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

        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_SelectAll";
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

        public DataTable category_WSSelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_WSSelectAll";
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



        public category SelectById(Int64 cid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            category objcategory = new category();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", cid);
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
                                    objcategory.cid = Convert.ToInt64(ds.Tables[0].Rows[0]["cid"]);
                                    objcategory.categoryname = Convert.ToString(ds.Tables[0].Rows[0]["categoryname"]);
                                    objcategory.imagename = Convert.ToString(ds.Tables[0].Rows[0]["imagename"]);
                                    objcategory.actualprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["actualprice"]);
                                    objcategory.discountprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["discountprice"]);
                                    objcategory.shortdesc = Convert.ToString(ds.Tables[0].Rows[0]["shortdesc"]);
                                    objcategory.longdescp = Convert.ToString(ds.Tables[0].Rows[0]["longdescp"]);
                                    objcategory.bankid = Convert.ToInt32(ds.Tables[0].Rows[0]["bankid"]);
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

        public DataTable category_WSSelectById(Int64 cid)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_WSSelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", cid);
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

        public Int64 Insert(category objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@cid";
                param.Value = objcategory.cid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@categoryname", objcategory.categoryname);
                cmd.Parameters.AddWithValue("@imagename", objcategory.imagename);
                cmd.Parameters.AddWithValue("@actualprice", objcategory.actualprice);
                cmd.Parameters.AddWithValue("@discountprice", objcategory.discountprice);
                cmd.Parameters.AddWithValue("@shortdesc", objcategory.shortdesc);
                cmd.Parameters.AddWithValue("@longdescp", objcategory.longdescp);
               // cmd.Parameters.AddWithValue("@bankid", objcategory.bankid);

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

        public Int64 Update(category objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@cid";
                param.Value = objcategory.cid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@categoryname", objcategory.categoryname);
                cmd.Parameters.AddWithValue("@imagename", objcategory.imagename);
                cmd.Parameters.AddWithValue("@actualprice", objcategory.actualprice);
                cmd.Parameters.AddWithValue("@discountprice", objcategory.discountprice);
                cmd.Parameters.AddWithValue("@shortdesc", objcategory.shortdesc);
                cmd.Parameters.AddWithValue("@longdescp", objcategory.longdescp);
                //cmd.Parameters.AddWithValue("@bankid", objcategory.bankid);                

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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@cid", cid);

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

        public bool Category_IsActive(Int64 CategoryId, Boolean IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "category_IsActive";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", CategoryId);
                cmd.Parameters.AddWithValue("@isactive", IsActive);
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
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

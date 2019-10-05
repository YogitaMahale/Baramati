using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_orders_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_orders_db()
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
                cmd.CommandText = "orders_SelectAll";
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
        public orders SelectById(Int64 oid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            orders objorders = new orders();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orders_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@oid", oid);
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
                                    objorders.oid = Convert.ToInt64(ds.Tables[0].Rows[0]["oid"]);
                                    objorders.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                    objorders.productquantites = Convert.ToInt32(ds.Tables[0].Rows[0]["productquantites"]);
                                    objorders.billpaidornot = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["billpaidornot"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["billpaidornot"]);
                                    objorders.amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]);
                                    objorders.discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["discount"]);
                                    objorders.tax = Convert.ToDecimal(ds.Tables[0].Rows[0]["tax"]);
                                    objorders.totalamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["totalamount"]);
                                    objorders.orderdate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderdate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["orderdate"]);
                                    objorders.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
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
            return objorders;
        }
        public Int64 Insert(orders objorders)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orders_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@oid";
                param.Value = objorders.oid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@uid", objorders.uid);
                cmd.Parameters.AddWithValue("@productquantites", objorders.productquantites);
                cmd.Parameters.AddWithValue("@billpaidornot", objorders.billpaidornot);
                cmd.Parameters.AddWithValue("@amount", objorders.amount);
                cmd.Parameters.AddWithValue("@discount", objorders.discount);
                cmd.Parameters.AddWithValue("@tax", objorders.tax);
                cmd.Parameters.AddWithValue("@totalamount", objorders.totalamount);
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@isdelete", objorders.isdelete);
                cmd.Parameters.AddWithValue("@usertype", objorders.usertype);
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
        public Int64 Update(orders objorders)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orders_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@oid";
                param.Value = objorders.oid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@uid", objorders.uid);
                cmd.Parameters.AddWithValue("@productquantites", objorders.productquantites);
                cmd.Parameters.AddWithValue("@billpaidornot", objorders.billpaidornot);
                cmd.Parameters.AddWithValue("@amount", objorders.amount);
                cmd.Parameters.AddWithValue("@discount", objorders.discount);
                cmd.Parameters.AddWithValue("@tax", objorders.tax);
                cmd.Parameters.AddWithValue("@totalamount", objorders.totalamount);
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@isdelete", objorders.isdelete);

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
        public bool Delete(Int64 oid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orders_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@oid", oid);
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

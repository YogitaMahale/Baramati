using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_creditnotes_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_creditnotes_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_creditnotes_db()
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

        /*
        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "creditnotes_SelectAll";
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
        

        public creditnotes SelectById(Int64 oid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            creditnotes objcreditnotes = new creditnotes();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "creditnotes_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@o" +
                    "id", oid);
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
                                    objcreditnotes.oid = Convert.ToInt64(ds.Tables[0].Rows[0]["oid"]);
                                    objcreditnotes.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                    objcreditnotes.productquantites = Convert.ToInt32(ds.Tables[0].Rows[0]["productquantites"]);
                                    objcreditnotes.billpaidornot = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["billpaidornot"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["billpaidornot"]);
                                    objcreditnotes.amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]);
                                    objcreditnotes.discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["discount"]);
                                    objcreditnotes.tax = Convert.ToDecimal(ds.Tables[0].Rows[0]["tax"]);
                                    objcreditnotes.totalamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["totalamount"]);
                                    objcreditnotes.orderdate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderdate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["orderdate"]);
                                    objcreditnotes.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
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
            return objcreditnotes;
        }
        */


        public Int64 Insert(creditnotes objcreditnotes)
        {
            //id, customerid, invoiceid, reason, disctypepercentage, amount, freightdiscount, createddate, isdeleted

            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "creditnotes_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objcreditnotes.id;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@customerid", objcreditnotes.customerid);
                cmd.Parameters.AddWithValue("@invoiceid", objcreditnotes.invoiceid);
                cmd.Parameters.AddWithValue("@reason", objcreditnotes.reason);
                cmd.Parameters.AddWithValue("@disctypepercentage", objcreditnotes.disctypepercentage);
                cmd.Parameters.AddWithValue("@amount", objcreditnotes.amount);
                cmd.Parameters.AddWithValue("@freightdiscount", objcreditnotes.freightdiscount);
                cmd.Parameters.AddWithValue("@otheramount", objcreditnotes.otheramount);
                
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

        /*
        public Int64 Update(creditnotes objcreditnotes)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "creditnotes_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@oid";
                param.Value = objcreditnotes.oid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@uid", objcreditnotes.uid);
                cmd.Parameters.AddWithValue("@productquantites", objcreditnotes.productquantites);
                cmd.Parameters.AddWithValue("@billpaidornot", objcreditnotes.billpaidornot);
                cmd.Parameters.AddWithValue("@amount", objcreditnotes.amount);
                cmd.Parameters.AddWithValue("@discount", objcreditnotes.discount);
                cmd.Parameters.AddWithValue("@tax", objcreditnotes.tax);
                cmd.Parameters.AddWithValue("@totalamount", objcreditnotes.totalamount);
                cmd.Parameters.AddWithValue("@orderdate", objcreditnotes.orderdate);
                cmd.Parameters.AddWithValue("@isdelete", objcreditnotes.isdelete);

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
                cmd.CommandText = "creditnotes_Delete";
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

        */
        #endregion

    }

}

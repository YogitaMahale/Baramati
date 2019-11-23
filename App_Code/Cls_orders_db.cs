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
      
                           
                                objorders.oid = Convert.ToInt64(ds.Tables[0].Rows[0]["oid"]);
                                objorders.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                objorders.paymentType = Convert.ToString(ds.Tables[0].Rows[0]["paymentType"]);
                                objorders.orderno = Convert.ToString(ds.Tables[0].Rows[0]["orderno"]);
                                objorders.invoicetype = Convert.ToString(ds.Tables[0].Rows[0]["invoicetype"]);
                                objorders.paymentMode = Convert.ToString(ds.Tables[0].Rows[0]["paymentMode"]);
                                objorders.orderdate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderdate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["orderdate"]);
                                objorders.subamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["subamount"]);
                                objorders.totalGSTAmount = Convert.ToDecimal (ds.Tables[0].Rows[0]["totalGSTAmount"]);
                                objorders.per_tradeDisandScheme = Convert.ToDecimal(ds.Tables[0].Rows[0]["per_tradeDisandScheme"]);
                                objorders.amt_tradeDisandScheme = Convert.ToDecimal(ds.Tables[0].Rows[0]["amt_tradeDisandScheme"]);
                                objorders.per_taxableDiscount = Convert.ToDecimal(ds.Tables[0].Rows[0]["per_taxableDiscount"]);
                                objorders.amt_taxableDiscount = Convert.ToDecimal(ds.Tables[0].Rows[0]["amt_taxableDiscount"]);
                                objorders.TaxableAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["TaxableAmount"]);
                                objorders.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalAmount"]);
                                objorders.CGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["CGSTamt"]);
                                objorders.SGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGSTamt"]);
                                objorders.IGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGSTamt"]);
                                objorders.otheramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["otheramt"]);                         
                                   objorders.freightDiscount = Convert.ToDecimal(ds.Tables[0].Rows[0]["freightDiscount"]);
                                objorders.duedate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["duedate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["duedate"]);
                                objorders.grandTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["grandTotal"]);
                                objorders.Referenceby = Convert.ToString(ds.Tables[0].Rows[0]["Referenceby"]);
                                objorders.DeliveredThrough = Convert.ToString(ds.Tables[0].Rows[0]["DeliveredThrough"]);
                                objorders.DeliveredDetails = Convert.ToString(ds.Tables[0].Rows[0]["DeliveredDetails"]);
                                objorders.OrderStatus = Convert.ToInt64(ds.Tables[0].Rows[0]["OrderStatus"]);
                                objorders.ordertype = Convert.ToString (ds.Tables[0].Rows[0]["ordertype"]);
                                objorders.pendingAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["pendingAmt"]);
         
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
                cmd.Parameters.AddWithValue("@paymentType", objorders.paymentType);
                cmd.Parameters.AddWithValue("@invoicetype", objorders.invoicetype);
                cmd.Parameters.AddWithValue("@orderno", objorders.orderno);
                cmd.Parameters.AddWithValue("@paymentMode", objorders.paymentMode);
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@subamount", objorders.subamount);
                cmd.Parameters.AddWithValue("@totalGSTAmount", objorders.totalGSTAmount);
                cmd.Parameters.AddWithValue("@per_tradeDisandScheme", objorders.per_tradeDisandScheme);
                cmd.Parameters.AddWithValue("@amt_tradeDisandScheme", objorders.amt_tradeDisandScheme);
                cmd.Parameters.AddWithValue("@per_taxableDiscount", objorders.per_taxableDiscount);
                cmd.Parameters.AddWithValue("@amt_taxableDiscount", objorders.amt_taxableDiscount);
                cmd.Parameters.AddWithValue("@TaxableAmount", objorders.TaxableAmount);
                cmd.Parameters.AddWithValue("@TotalAmount", objorders.TotalAmount);
                cmd.Parameters.AddWithValue("@CGSTamt", objorders.CGSTamt);
                cmd.Parameters.AddWithValue("@SGSTamt", objorders.SGSTamt);
                cmd.Parameters.AddWithValue("@IGSTamt", objorders.IGSTamt);
                cmd.Parameters.AddWithValue("@otheramt", objorders.otheramt);
                cmd.Parameters.AddWithValue("@freightDiscount", objorders.freightDiscount);
                cmd.Parameters.AddWithValue("@duedate", objorders.duedate);
                cmd.Parameters.AddWithValue("@grandTotal", objorders.grandTotal);
                cmd.Parameters.AddWithValue("@Referenceby", objorders.Referenceby);
                cmd.Parameters.AddWithValue("@DeliveredThrough", objorders.DeliveredThrough);
                cmd.Parameters.AddWithValue("@DeliveredDetails", objorders.DeliveredDetails);
                cmd.Parameters.AddWithValue("@OrderStatus", objorders.OrderStatus);
                cmd.Parameters.AddWithValue("@ordertype", objorders.ordertype);
                cmd.Parameters.AddWithValue("@pendingAmt", objorders.pendingAmt);
                cmd.Parameters.AddWithValue("@isconfirmed", objorders.isconfirmed );
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
                cmd.Parameters.AddWithValue("@paymentType", objorders.paymentType);
                cmd.Parameters.AddWithValue("@invoicetype", objorders.invoicetype);
                cmd.Parameters.AddWithValue("@orderno", objorders.orderno);
                cmd.Parameters.AddWithValue("@paymentMode", objorders.paymentMode);
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@subamount", objorders.subamount);
                cmd.Parameters.AddWithValue("@totalGSTAmount", objorders.totalGSTAmount);
                cmd.Parameters.AddWithValue("@per_tradeDisandScheme", objorders.per_tradeDisandScheme);
                cmd.Parameters.AddWithValue("@amt_tradeDisandScheme", objorders.amt_tradeDisandScheme);
                cmd.Parameters.AddWithValue("@per_taxableDiscount", objorders.per_taxableDiscount);
                cmd.Parameters.AddWithValue("@amt_taxableDiscount", objorders.amt_taxableDiscount);
                cmd.Parameters.AddWithValue("@TaxableAmount", objorders.TaxableAmount);
                cmd.Parameters.AddWithValue("@TotalAmount", objorders.TotalAmount);
                cmd.Parameters.AddWithValue("@CGSTamt", objorders.CGSTamt);
                cmd.Parameters.AddWithValue("@SGSTamt", objorders.SGSTamt);
                cmd.Parameters.AddWithValue("@IGSTamt", objorders.IGSTamt);
                cmd.Parameters.AddWithValue("@otheramt", objorders.otheramt);
                cmd.Parameters.AddWithValue("@freightDiscount", objorders.freightDiscount);
                cmd.Parameters.AddWithValue("@duedate", objorders.duedate );
                cmd.Parameters.AddWithValue("@grandTotal", objorders.grandTotal);
                cmd.Parameters.AddWithValue("@Referenceby", objorders.Referenceby);
                cmd.Parameters.AddWithValue("@DeliveredThrough", objorders.DeliveredThrough);
                cmd.Parameters.AddWithValue("@DeliveredDetails", objorders.DeliveredDetails);
                cmd.Parameters.AddWithValue("@OrderStatus", objorders.OrderStatus);
                cmd.Parameters.AddWithValue("@ordertype", objorders.ordertype);
                cmd.Parameters.AddWithValue("@pendingAmt", objorders.pendingAmt);
       
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
        public DataTable Selectorderdetailsbydealerid(Int64 dealerId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Selectorderdetailsbydealerid";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@uid", dealerId);
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


        #endregion

    }

}

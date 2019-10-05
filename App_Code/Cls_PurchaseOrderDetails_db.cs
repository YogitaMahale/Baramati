using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_PurchaseOrderDetails_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_PurchaseOrderDetails_db
    {

        SqlConnection ConnectionString = new SqlConnection();
        public Cls_PurchaseOrderDetails_db()
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

        #region SelectAll and SelectById not in use
        /*
        public DataTable SelectAll(PurchaseOrderDetails objPurchaseOrderDetails)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }
        public PurchaseOrderDetails SelectById(Int64 id)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            PurchaseOrderDetails objPurchaseOrderDetails = new PurchaseOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@PurchaseOrderId", id);
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

                                //PurchaseOrderId, VendorId, isdeleted, OrderDate
                                objPurchaseOrderDetails.PurchaseOrderDetailsId = Convert.ToInt64(ds.Tables[0].Rows[0]["PurchaseOrderDetailsId"]);

                                objPurchaseOrderDetails.PurchaseOrderId = Convert.ToInt64(ds.Tables[0].Rows[0]["PurchaseOrderId"]);
                                objPurchaseOrderDetails.ProdId = Convert.ToInt64(ds.Tables[0].Rows[0]["ProdId"]);
                                objPurchaseOrderDetails.CategoryId = Convert.ToInt64(ds.Tables[0].Rows[0]["CategoryId"]);
                                objPurchaseOrderDetails.isdeleted = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdeleted"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdeleted"]);
                                objPurchaseOrderDetails.Quantity1 = Convert.ToInt64(ds.Tables[0].Rows[0]["Quantity1"]);
                                objPurchaseOrderDetails.Quantity = Convert.ToInt64(ds.Tables[0].Rows[0]["Quantity"]);





                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return objPurchaseOrderDetails;
        }
        */
        #endregion


        public Int64 Insert(PurchaseOrderDetails objPurchaseOrderDetails)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PurchaseOrderDetailsId";
                param.Value = objPurchaseOrderDetails.PurchaseOrderDetailsId;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                //PurchaseOrderId, ProdId, CategoryId, Quantity, isdeleted, Quantity1

                cmd.Parameters.AddWithValue("@PurchaseOrderId", objPurchaseOrderDetails.PurchaseOrderId);
                cmd.Parameters.AddWithValue("@ProdId", objPurchaseOrderDetails.ProdId);
                cmd.Parameters.AddWithValue("@CategoryId", objPurchaseOrderDetails.CategoryId);
                cmd.Parameters.AddWithValue("@Quantity", objPurchaseOrderDetails.Quantity);
                //cmd.Parameters.AddWithValue("@isdeleted", objPurchaseOrderDetails.isdeleted);
                cmd.Parameters.AddWithValue("@Quantity1", objPurchaseOrderDetails.Quantity1);




                ConnectionString.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt64(param.Value);
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
            finally
            {
                ConnectionString.Close();
            }
            return result;
        }

        #region Update Not in use
        /*
        public Int64 Update(PurchaseOrderDetails objPurchaseOrderDetails)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PurchaseOrderId";
                param.Value = objPurchaseOrderDetails.PurchaseOrderId;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                //PurchaseOrderId, VendorId, isdeleted

                cmd.Parameters.AddWithValue("@PONo", objPurchaseOrderDetails.PONo);
                cmd.Parameters.AddWithValue("@VendorId", objPurchaseOrderDetails.VendorId);

                cmd.Parameters.AddWithValue("@isdeleted", objPurchaseOrderDetails.isdeleted);
                cmd.Parameters.AddWithValue("@OrderDate", objPurchaseOrderDetails.OrderDate);
                cmd.Parameters.AddWithValue("@orderstatus", objPurchaseOrderDetails.orderstatus);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt64(param.Value);
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
            finally
            {
                ConnectionString.Close();
            }
            return result;
        }
        */
        #endregion



        #region Delete Not in use
/*
        public bool Delete(Int64 ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@PurchaseOrderId", ID);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
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



        #endregion


    }
}

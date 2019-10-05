using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_PurchaseOrderHeader_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_PurchaseOrderHeader_db
    {

        SqlConnection ConnectionString = new SqlConnection();
        public Cls_PurchaseOrderHeader_db()
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
        public DataTable SelectAll(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_SelectAll";
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
        public PurchaseOrderHeader SelectById(Int64 id)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            PurchaseOrderHeader objPurchaseOrderHeader = new PurchaseOrderHeader();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_SelectById";
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

                                objPurchaseOrderHeader.PurchaseOrderId = Convert.ToInt64(ds.Tables[0].Rows[0]["PurchaseOrderId"]);
                                objPurchaseOrderHeader.PONo = Convert.ToString(ds.Tables[0].Rows[0]["PONo"]);
                                objPurchaseOrderHeader.VendorId = Convert.ToInt64(ds.Tables[0].Rows[0]["VendorId"]);
                                objPurchaseOrderHeader.isdeleted = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdeleted"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdeleted"]);
                                objPurchaseOrderHeader.OrderDate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OrderDate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"]);
                                objPurchaseOrderHeader.orderstatus = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderstatus"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["orderstatus"]);





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
            return objPurchaseOrderHeader;
        }
        public Int64 Insert(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PurchaseOrderId";
                param.Value = objPurchaseOrderHeader.PurchaseOrderId;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                //PurchaseOrderId, VendorId, isdeleted

                cmd.Parameters.AddWithValue("@PONo", objPurchaseOrderHeader.PONo);
                cmd.Parameters.AddWithValue("@VendorId", objPurchaseOrderHeader.VendorId);

                cmd.Parameters.AddWithValue("@isdeleted", objPurchaseOrderHeader.isdeleted);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@orderstatus", objPurchaseOrderHeader.orderstatus);




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
        public Int64 Update(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PurchaseOrderId";
                param.Value = objPurchaseOrderHeader.PurchaseOrderId;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                //PurchaseOrderId, VendorId, isdeleted

                cmd.Parameters.AddWithValue("@PONo", objPurchaseOrderHeader.PONo);
                cmd.Parameters.AddWithValue("@VendorId", objPurchaseOrderHeader.VendorId);

                cmd.Parameters.AddWithValue("@isdeleted", objPurchaseOrderHeader.isdeleted);
                cmd.Parameters.AddWithValue("@OrderDate", objPurchaseOrderHeader.OrderDate);
                cmd.Parameters.AddWithValue("@orderstatus", objPurchaseOrderHeader.orderstatus);

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
        public bool Delete(Int64 ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_Delete";
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
        #endregion


    }
}

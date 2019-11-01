using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_worksheetdetails_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_worksheetdetails_db
    {

        SqlConnection ConnectionString = new SqlConnection();
        public Cls_worksheetdetails_db()
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
        public DataTable SelectAll(worksheetdetails objworksheetdetails)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "worksheetdetails_SelectAll";
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
        public worksheetdetails SelectById(Int64 id)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            worksheetdetails objworksheetdetails = new worksheetdetails();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "worksheetdetails_SelectById";
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
                                objworksheetdetails.worksheetdetailsId = Convert.ToInt64(ds.Tables[0].Rows[0]["worksheetdetailsId"]);

                                objworksheetdetails.PurchaseOrderId = Convert.ToInt64(ds.Tables[0].Rows[0]["PurchaseOrderId"]);
                                objworksheetdetails.ProdId = Convert.ToInt64(ds.Tables[0].Rows[0]["ProdId"]);
                                objworksheetdetails.CategoryId = Convert.ToInt64(ds.Tables[0].Rows[0]["CategoryId"]);
                                objworksheetdetails.isdeleted = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdeleted"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdeleted"]);
                                objworksheetdetails.Quantity1 = Convert.ToInt64(ds.Tables[0].Rows[0]["Quantity1"]);
                                objworksheetdetails.Quantity = Convert.ToInt64(ds.Tables[0].Rows[0]["Quantity"]);





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
            return objworksheetdetails;
        }
        */
        #endregion


        public Int64 Insert(worksheetdetails objworksheetdetails)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetdetails_Insert",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = objworksheetdetails.Id,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);

                //id,worksheetid,particularsid,employeeid,quantity,workdate,remark

                //cmd.Parameters.AddWithValue("@id", objworksheetdetails.Id);
                cmd.Parameters.AddWithValue("@worksheetid", objworksheetdetails.Worksheetid);
                cmd.Parameters.AddWithValue("@particularsid", objworksheetdetails.Particularsid);
                cmd.Parameters.AddWithValue("@employeeid", objworksheetdetails.Employeeid);
                cmd.Parameters.AddWithValue("@quantity", objworksheetdetails.Quantity);
                cmd.Parameters.AddWithValue("@wages", objworksheetdetails.Wages);
                cmd.Parameters.AddWithValue("@workdate", objworksheetdetails.Workdate);
                cmd.Parameters.AddWithValue("@remark", objworksheetdetails.Remark);

                //cmd.Parameters.AddWithValue("@isdeleted", objworksheetdetails.isdeleted);
                //cmd.Parameters.AddWithValue("@Quantity1", objworksheetdetails.Quantity1);




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
        public Int64 Update(worksheetdetails objworksheetdetails)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "worksheetdetails_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PurchaseOrderId";
                param.Value = objworksheetdetails.PurchaseOrderId;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                //PurchaseOrderId, VendorId, isdeleted

                cmd.Parameters.AddWithValue("@PONo", objworksheetdetails.PONo);
                cmd.Parameters.AddWithValue("@VendorId", objworksheetdetails.VendorId);

                cmd.Parameters.AddWithValue("@isdeleted", objworksheetdetails.isdeleted);
                cmd.Parameters.AddWithValue("@OrderDate", objworksheetdetails.OrderDate);
                cmd.Parameters.AddWithValue("@orderstatus", objworksheetdetails.orderstatus);

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
                        cmd.CommandText = "worksheetdetails_Delete";
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

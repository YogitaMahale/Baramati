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
        public DataTable SelectAll()
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
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }
        public PurchaseOrderDetails  SelectById(Int64 opid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            PurchaseOrderDetails objorderproducts = new PurchaseOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@opid", opid);
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
                                

                                    objorderproducts.opid = Convert.ToInt64(ds.Tables[0].Rows[0]["opid"]);
                                    objorderproducts.oid = Convert.ToInt64(ds.Tables[0].Rows[0]["oid"]);
                                    objorderproducts.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                    objorderproducts.pid = Convert.ToInt64(ds.Tables[0].Rows[0]["pid"]);
                                    objorderproducts.qty = Convert.ToInt64(ds.Tables[0].Rows[0]["qty"]);

                                    objorderproducts.rate = Convert.ToDecimal(ds.Tables[0].Rows[0]["rate"]);
                                    objorderproducts.subtotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["subtotal"]);
                                    objorderproducts.discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Tables[0].Rows[0]["scheme"]);
                                    objorderproducts.frieghtamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["frieghtamt"]);

                                    objorderproducts.igstper = Convert.ToDecimal(ds.Tables[0].Rows[0]["igstper"]);
                                    objorderproducts.gstamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["gstamt"]);
                                    objorderproducts.total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"]);
                                    objorderproducts.netrate = Convert.ToDecimal(ds.Tables[0].Rows[0]["netrate"]);
                                     
                                 
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
            return objorderproducts;
        }
        public Int64 Insert(PurchaseOrderDetails  objorderproducts)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@opid";
                param.Value = objorderproducts.opid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@oid", objorderproducts.oid);
                cmd.Parameters.AddWithValue("@uid", objorderproducts.uid);
                cmd.Parameters.AddWithValue("@pid", objorderproducts.pid);
                cmd.Parameters.AddWithValue("@qty", objorderproducts.qty );
                cmd.Parameters.AddWithValue("@rate", objorderproducts.rate );

               
                cmd.Parameters.AddWithValue("@subtotal", objorderproducts.subtotal );
                cmd.Parameters.AddWithValue("@discount", objorderproducts.discount );
                cmd.Parameters.AddWithValue("@scheme", objorderproducts.scheme );

                cmd.Parameters.AddWithValue("@frieghtamt", objorderproducts.frieghtamt );
                cmd.Parameters.AddWithValue("@taxableamt", objorderproducts.taxableamt );
                cmd.Parameters.AddWithValue("@csgtper", objorderproducts.csgtper );
                cmd.Parameters.AddWithValue("@sgstper", objorderproducts.sgstper );
                cmd.Parameters.AddWithValue("@igstper", objorderproducts.igstper );
                cmd.Parameters.AddWithValue("@netrate", objorderproducts.netrate );
                cmd.Parameters.AddWithValue("@gstamt", objorderproducts.gstamt );
                cmd.Parameters.AddWithValue("@total", objorderproducts.total );


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
        public Int64 Update(PurchaseOrderDetails  objorderproducts)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@opid";
                param.Value = objorderproducts.opid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@oid", objorderproducts.oid);
                cmd.Parameters.AddWithValue("@oid", objorderproducts.oid);
                cmd.Parameters.AddWithValue("@uid", objorderproducts.uid);
                cmd.Parameters.AddWithValue("@pid", objorderproducts.pid);
                cmd.Parameters.AddWithValue("@qty", objorderproducts.qty);
                cmd.Parameters.AddWithValue("@rate", objorderproducts.rate);


                cmd.Parameters.AddWithValue("@subtotal", objorderproducts.subtotal);
                cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
                cmd.Parameters.AddWithValue("@scheme", objorderproducts.scheme);

                cmd.Parameters.AddWithValue("@frieghtamt", objorderproducts.frieghtamt);
                cmd.Parameters.AddWithValue("@taxableamt", objorderproducts.taxableamt);
                cmd.Parameters.AddWithValue("@csgtper", objorderproducts.csgtper);
                cmd.Parameters.AddWithValue("@sgstper", objorderproducts.sgstper);
                cmd.Parameters.AddWithValue("@igstper", objorderproducts.igstper);
                cmd.Parameters.AddWithValue("@netrate", objorderproducts.netrate);
                cmd.Parameters.AddWithValue("@gstamt", objorderproducts.gstamt);
                cmd.Parameters.AddWithValue("@total", objorderproducts.total);
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
        public bool Delete(Int64 opid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderDetails_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@opid", opid);

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

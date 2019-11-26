using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

 
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
        public DataSet SelectAll()
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
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds;
        }
        public PurchaseOrderHeader SelectById(Int64 oid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            PurchaseOrderHeader objorders = new PurchaseOrderHeader();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_SelectById";
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
                                objorders.invoiceno = Convert.ToString(ds.Tables[0].Rows[0]["invoiceno"]);
                                objorders.invoicetype = Convert.ToString(ds.Tables[0].Rows[0]["invoicetype"]);
                                objorders.accountYear = Convert.ToString(ds.Tables[0].Rows[0]["accountYear"]);
                                objorders.orderdate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderdate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["orderdate"]);
                                objorders.subtotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["subtotal"]);
                                objorders.discandScheme = Convert.ToDecimal(ds.Tables[0].Rows[0]["discandScheme"]);
                                objorders.frieghtamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["frieghtamount"]);
                                objorders.taxableamount = Convert.ToDecimal(ds.Tables[0].Rows[0]["taxableamount"]);
                                objorders.CGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGSTamt"]);
                                objorders.SGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGSTamt"]);
                                objorders.IGSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGSTamt"]);
                               objorders.totalAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["totalAmt"]);
                                objorders.transportamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["transportamt"]);
                                objorders.packingamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["packingamt"]);
                                 objorders.otheramt = Convert.ToDecimal(ds.Tables[0].Rows[0]["otheramt"]);
                                objorders.dicountamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["dicountamt"]);
                                objorders.grandtotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["grandtotal"]); 
                                objorders.pendingAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["pendingAmt"]);
                                objorders.stockdate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stockdate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["stockdate"]);

                                



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
        public Int64 Insert(PurchaseOrderHeader  objorders)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@oid";
                param.Value = objorders.oid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@uid", objorders.uid);
                 
                cmd.Parameters.AddWithValue("@invoicetype", objorders.invoicetype);
                cmd.Parameters.AddWithValue("@invoiceno", objorders.invoiceno );              
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@accountYear", objorders.accountYear );
                cmd.Parameters.AddWithValue("@subtotal", objorders.subtotal);
                cmd.Parameters.AddWithValue("@discandScheme", objorders.discandScheme );    
               cmd.Parameters.AddWithValue("@frieghtamount", objorders.frieghtamount);
                cmd.Parameters.AddWithValue("@taxableamount", objorders.taxableamount);
                cmd.Parameters.AddWithValue("@CGSTamt", objorders.CGSTamt);
                cmd.Parameters.AddWithValue("@SGSTamt", objorders.SGSTamt);
                cmd.Parameters.AddWithValue("@IGSTamt", objorders.IGSTamt);
                cmd.Parameters.AddWithValue("@totalAmt", objorders.totalAmt);
                cmd.Parameters.AddWithValue("@transportamt", objorders.transportamt);
                cmd.Parameters.AddWithValue("@packingamt", objorders.packingamt);
                cmd.Parameters.AddWithValue("@otheramt", objorders.otheramt);
                cmd.Parameters.AddWithValue("@grandtotal", objorders.grandtotal);
                cmd.Parameters.AddWithValue("@pendingAmt", objorders.pendingAmt);
                cmd.Parameters.AddWithValue("@stockdate", objorders.stockdate);
                
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
        public Int64 Update(PurchaseOrderHeader  objorders)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@oid";
                param.Value = objorders.oid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@uid", objorders.uid);

                cmd.Parameters.AddWithValue("@invoicetype", objorders.invoicetype);
                cmd.Parameters.AddWithValue("@invoiceno", objorders.invoiceno);
                cmd.Parameters.AddWithValue("@orderdate", objorders.orderdate);
                cmd.Parameters.AddWithValue("@accountYear", objorders.accountYear);
                cmd.Parameters.AddWithValue("@subtotal", objorders.subtotal);
                cmd.Parameters.AddWithValue("@discandScheme", objorders.discandScheme);
                cmd.Parameters.AddWithValue("@frieghtamount", objorders.frieghtamount);
                cmd.Parameters.AddWithValue("@taxableamount", objorders.taxableamount);
                cmd.Parameters.AddWithValue("@CGSTamt", objorders.CGSTamt);
                cmd.Parameters.AddWithValue("@SGSTamt", objorders.SGSTamt);
                cmd.Parameters.AddWithValue("@IGSTamt", objorders.IGSTamt);
                cmd.Parameters.AddWithValue("@totalAmt", objorders.totalAmt);
                cmd.Parameters.AddWithValue("@transportamt", objorders.transportamt);
                cmd.Parameters.AddWithValue("@packingamt", objorders.packingamt);
                cmd.Parameters.AddWithValue("@otheramt", objorders.otheramt);
                cmd.Parameters.AddWithValue("@grandtotal", objorders.grandtotal);
                cmd.Parameters.AddWithValue("@pendingAmt", objorders.pendingAmt);
                cmd.Parameters.AddWithValue("@stockdate", objorders.stockdate);

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
                cmd.CommandText = "PurchaseOrderHeader_Delete";
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

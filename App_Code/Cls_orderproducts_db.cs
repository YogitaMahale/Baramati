using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_orderproducts_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_orderproducts_db()
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
                cmd.CommandText = "orderproducts_SelectAll";
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
        public orderproducts SelectById(Int64 opid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            orderproducts objorderproducts = new orderproducts();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orderproducts_SelectById";
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
                                {
                                    objorderproducts.opid = Convert.ToInt64(ds.Tables[0].Rows[0]["opid"]);
                                    objorderproducts.oid = Convert.ToInt64(ds.Tables[0].Rows[0]["oid"]);
                                    objorderproducts.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                    objorderproducts.pid = Convert.ToInt64(ds.Tables[0].Rows[0]["pid"]);
                                    objorderproducts.brandid = Convert.ToString(ds.Tables[0].Rows[0]["brandid"]);

                                    objorderproducts.sizeid = Convert.ToString(ds.Tables[0].Rows[0]["sizeid"]);
                                    objorderproducts.colorid = Convert.ToString(ds.Tables[0].Rows[0]["colorid"]);
                                    objorderproducts.cart = Convert.ToDecimal(ds.Tables[0].Rows[0]["cart"]);
                                    objorderproducts.pack = Convert.ToString (ds.Tables[0].Rows[0]["pack"]);
                                    objorderproducts.qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["qty"]);

                                    objorderproducts.mrp = Convert.ToDecimal(ds.Tables[0].Rows[0]["mrp"]);
                                     objorderproducts.unitRate = Convert.ToDecimal(ds.Tables[0].Rows[0]["unitRate"]);
                                    objorderproducts.subTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["subTotal"]);
                                    objorderproducts.discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Tables[0].Rows[0]["scheme"]);



                                    objorderproducts.taxableamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["taxableamt"]);
                                    objorderproducts.CGSTper = Convert.ToDecimal(ds.Tables[0].Rows[0]["CGSTper"]);
                                    objorderproducts.SGSTper = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGSTper"]);
                                    objorderproducts.IGSTper = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGSTper"]);
                                    objorderproducts.GSTamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["GSTamt"]);
                                      objorderproducts.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalAmount"]);
  
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
            return objorderproducts;
        }
        public Int64 Insert(orderproducts objorderproducts)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orderproducts_Insert";
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
                cmd.Parameters.AddWithValue("@brandid", objorderproducts.brandid);
                cmd.Parameters.AddWithValue("@sizeid", objorderproducts.sizeid);
                cmd.Parameters.AddWithValue("@colorid", objorderproducts.colorid);
                cmd.Parameters.AddWithValue("@cart", objorderproducts.cart);
                cmd.Parameters.AddWithValue("@pack", objorderproducts.pack);

                cmd.Parameters.AddWithValue("@qty", objorderproducts.qty);
                cmd.Parameters.AddWithValue("@mrp", objorderproducts.mrp);
                cmd.Parameters.AddWithValue("@unitRate", objorderproducts.unitRate);
                cmd.Parameters.AddWithValue("@subTotal", objorderproducts.subTotal);
                cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
                cmd.Parameters.AddWithValue("@scheme", objorderproducts.scheme);
                cmd.Parameters.AddWithValue("@taxableamt", objorderproducts.taxableamt);
                cmd.Parameters.AddWithValue("@CGSTper", objorderproducts.CGSTper);

                cmd.Parameters.AddWithValue("@SGSTper", objorderproducts.SGSTper);
                cmd.Parameters.AddWithValue("@IGSTper", objorderproducts.IGSTper);
                cmd.Parameters.AddWithValue("@GSTamt", objorderproducts.GSTamt);
                cmd.Parameters.AddWithValue("@TotalAmount", objorderproducts.TotalAmount);
            

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
        public Int64 Update(orderproducts objorderproducts)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "orderproducts_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@opid";
                param.Value = objorderproducts.opid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                //cmd.Parameters.AddWithValue("@oid", objorderproducts.oid);
                //cmd.Parameters.AddWithValue("@uid", objorderproducts.uid);
                //cmd.Parameters.AddWithValue("@pid", objorderproducts.pid);
                //cmd.Parameters.AddWithValue("@productprice", objorderproducts.productprice);
                //cmd.Parameters.AddWithValue("@gst", objorderproducts.gst);
                //cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
                //cmd.Parameters.AddWithValue("@productafterdiscountprice", objorderproducts.discount);
                //cmd.Parameters.AddWithValue("@quantites", objorderproducts.quantites);
                cmd.Parameters.AddWithValue("@oid", objorderproducts.oid);
                cmd.Parameters.AddWithValue("@uid", objorderproducts.uid);
                cmd.Parameters.AddWithValue("@pid", objorderproducts.pid);
                cmd.Parameters.AddWithValue("@brandid", objorderproducts.brandid);
                cmd.Parameters.AddWithValue("@sizeid", objorderproducts.sizeid);
                cmd.Parameters.AddWithValue("@colorid", objorderproducts.colorid);
                cmd.Parameters.AddWithValue("@cart", objorderproducts.cart);
                cmd.Parameters.AddWithValue("@pack", objorderproducts.pack);

                cmd.Parameters.AddWithValue("@qty", objorderproducts.qty);
                cmd.Parameters.AddWithValue("@mrp", objorderproducts.mrp);
                cmd.Parameters.AddWithValue("@unitRate", objorderproducts.unitRate);
                cmd.Parameters.AddWithValue("@subTotal", objorderproducts.subTotal);
                cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
                cmd.Parameters.AddWithValue("@scheme", objorderproducts.scheme);
                cmd.Parameters.AddWithValue("@taxableamt", objorderproducts.taxableamt);
                cmd.Parameters.AddWithValue("@CGSTper", objorderproducts.CGSTper);

                cmd.Parameters.AddWithValue("@SGSTper", objorderproducts.SGSTper);
                cmd.Parameters.AddWithValue("@IGSTper", objorderproducts.IGSTper);
                cmd.Parameters.AddWithValue("@GSTamt", objorderproducts.GSTamt);
                cmd.Parameters.AddWithValue("@TotalAmount", objorderproducts.TotalAmount);
            

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
                cmd.CommandText = "orderproducts_Delete";
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

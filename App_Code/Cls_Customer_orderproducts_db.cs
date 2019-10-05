using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_Customer_orderproducts_db
{
       SqlConnection ConnectionString = new SqlConnection();
	public Cls_Customer_orderproducts_db()
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
            cmd.CommandText = "Customer_orderproducts_SelectAll";
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
    public Customer_orderproducts SelectById(Int64 opid)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        Customer_orderproducts objorderproducts = new Customer_orderproducts();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Customer_orderproducts_SelectById";
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
                                objorderproducts.productprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["productprice"]);
                                objorderproducts.gst = Convert.ToDecimal(ds.Tables[0].Rows[0]["gst"]);
                                objorderproducts.discount = Convert.ToInt32(ds.Tables[0].Rows[0]["discount"]);
                                objorderproducts.productafterdiscountprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["productafterdiscountprice"]);
                                objorderproducts.quantites = Convert.ToInt32(ds.Tables[0].Rows[0]["quantites"]);
                                objorderproducts.producttotalprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["producttotalprice"]);

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
    public Int64 Insert(Customer_orderproducts objorderproducts)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Customer_orderproducts_Insert";
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
            cmd.Parameters.AddWithValue("@productprice", objorderproducts.productprice);
            cmd.Parameters.AddWithValue("@gst", objorderproducts.gst);
            cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
            cmd.Parameters.AddWithValue("@productafterdiscountprice", objorderproducts.productafterdiscountprice);
            cmd.Parameters.AddWithValue("@quantites", objorderproducts.quantites);
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
    public Int64 Update(Customer_orderproducts objorderproducts)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Customer_orderproducts_Update";
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
            cmd.Parameters.AddWithValue("@productprice", objorderproducts.productprice);
            cmd.Parameters.AddWithValue("@gst", objorderproducts.gst);
            cmd.Parameters.AddWithValue("@discount", objorderproducts.discount);
            cmd.Parameters.AddWithValue("@productafterdiscountprice", objorderproducts.discount);
            cmd.Parameters.AddWithValue("@quantites", objorderproducts.quantites);

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
            cmd.CommandText = "Customer_orderproducts_Delete";
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

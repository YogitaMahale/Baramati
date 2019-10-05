using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_OrderStatusmaster_db
{
       SqlConnection ConnectionString = new SqlConnection();
	public Cls_OrderStatusmaster_db()
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

    //public DataTable SelectAll_Admin()
    //{
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "bankmaster_SelectAllAdmin";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        ConnectionString.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //        return null;
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }
    //    return ds.Tables[0];
    //}

    public DataTable SelectAll()
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderStatus_SelectAll";
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

    public OrderStatus SelectById(Int64 bankid)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        OrderStatus objOrderStatus = new OrderStatus();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderStatus_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@id", bankid);
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
                                objOrderStatus.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                                objOrderStatus.type = Convert.ToString(ds.Tables[0].Rows[0]["type"]);
                                objOrderStatus.NotificationMsg = Convert.ToString(ds.Tables[0].Rows[0]["NotificationMsg"]);
                               
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
        return objOrderStatus;
    }

    public Int64 Insert(OrderStatus objOrderStatus)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderStatus_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objOrderStatus.id;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@type", objOrderStatus.type);
            cmd.Parameters.AddWithValue("@NotificationMsg", objOrderStatus.NotificationMsg);
          

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

    public Int64 Update(OrderStatus objOrderStatus)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderStatus_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objOrderStatus.id;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@type", objOrderStatus.type);
            cmd.Parameters.AddWithValue("@NotificationMsg", objOrderStatus.NotificationMsg);
          
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

    public bool Delete(Int32 oid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "orderStatus_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@id", oid);

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

    //public bool IsActive(Int32 bankid, bool isactive)
    //{
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "bankmaster_IsActive";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        cmd.Parameters.AddWithValue("@bankid", bankid);
    //        cmd.Parameters.AddWithValue("@isactive", isactive);

    //        ConnectionString.Open();
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //        return false;
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }
    //    return true;
    //}

    #endregion


}

}

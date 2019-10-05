using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_ExpenseMaster_db
{
    SqlConnection ConnectionString = new SqlConnection();

	public Cls_ExpenseMaster_db()
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
    public Int64 Update(ExpenseMaster objcategory)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Expense_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@eid";
            param.Value = objcategory.eid;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@Expensename", objcategory.Expensename);

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
    public ExpenseMaster SelectById(Int64 id)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        ExpenseMaster objcategory = new ExpenseMaster();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Expense_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@eid", id);
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
                                objcategory.eid = Convert.ToInt64(ds.Tables[0].Rows[0]["eid"]);
                                objcategory.Expensename = Convert.ToString(ds.Tables[0].Rows[0]["Expensename"]);

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return null;
        }
        finally
        {
            ConnectionString.Close();
        }
        return objcategory;
    }
    public bool Delete(Int64 id)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Expense_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@eid", id);

            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //   ErrHandler.writeError(ex.Message, ex.StackTrace);
            return false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return true;
    }
    public Int64 Insert(ExpenseMaster objcategory)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Expense_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@eid";
            param.Value = objcategory.eid;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@Expensename", objcategory.Expensename);

            ConnectionString.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
        finally
        {
            ConnectionString.Close();
        }
        return result;
    }
    public DataTable SelectAll()
    {
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "selectAll_Expense";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            //cmd.Parameters.AddWithValue("@eid", id);
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);


        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return null;
        }
        finally
        {
            ConnectionString.Close();
        }
        return dt;
    }
}
}
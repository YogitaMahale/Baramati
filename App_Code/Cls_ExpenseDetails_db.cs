using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_ExpenseDetails_db
{
    SqlConnection ConnectionString = new SqlConnection();
	public Cls_ExpenseDetails_db()
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
    public Int64 Update(ExpenseDetails objExpenseDetails)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExpenseDetails_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objExpenseDetails.id;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@FK_ExpenseID", objExpenseDetails.FK_ExpenseID);
            cmd.Parameters.AddWithValue("@amount", objExpenseDetails.amount);
            cmd.Parameters.AddWithValue("@Description", objExpenseDetails.Description);
            cmd.Parameters.AddWithValue("@bankid", objExpenseDetails.bankid);

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
    public ExpenseDetails SelectById(Int64 id)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        ExpenseDetails objExpenseDetails = new ExpenseDetails();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExpenseDetails_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@id", id);
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
                                objExpenseDetails.id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                 objExpenseDetails.FK_ExpenseID = Convert.ToInt64(ds.Tables[0].Rows[0]["FK_ExpenseID"]);
                                 objExpenseDetails.amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"]);
                                objExpenseDetails.Description = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
                                objExpenseDetails.bankid = Convert.ToInt64(ds.Tables[0].Rows[0]["bankid"]);

                                
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
        return objExpenseDetails;
    }
    public bool Delete(Int64 id)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExpenseDetails_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@id", id);

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
    public Int64 Insert(ExpenseDetails objExpenseDetails)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExpenseDetails_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objExpenseDetails.id;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@FK_ExpenseID", objExpenseDetails.FK_ExpenseID);
            cmd.Parameters.AddWithValue("@amount", objExpenseDetails.amount);
            cmd.Parameters.AddWithValue("@Description", objExpenseDetails.Description);
            cmd.Parameters.AddWithValue("@bankid", objExpenseDetails.bankid);
            

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
            cmd.CommandText = "selectAll_ExpenseDetails";
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
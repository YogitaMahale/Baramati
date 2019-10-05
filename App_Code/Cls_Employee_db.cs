using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_Employee_db
{
     SqlConnection ConnectionString = new SqlConnection();
	public Cls_Employee_db()
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
            cmd.CommandText = "employee_SelectAll";
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

    public employeeMaster  SelectById(Int64 id)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        employeeMaster objemployeeMaster = new employeeMaster();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "employee_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@id", id);
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            //vid, vendorName, Address1, Address2, MobileNo1, MobileNo2, email, landline, fk_countryId, fk_stateId, fk_cityId, createddate, isdelete, isactive

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            
                                objemployeeMaster.id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                
                                objemployeeMaster.employeeName = Convert.ToString(ds.Tables[0].Rows[0]["employeeName"]);
                                objemployeeMaster.img = Convert.ToString(ds.Tables[0].Rows[0]["img"]);
                                objemployeeMaster.Address1 = Convert.ToString(ds.Tables[0].Rows[0]["Address1"]);
                                objemployeeMaster.MobileNo1 = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo1"]);
                                 
                                objemployeeMaster.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);

                                objemployeeMaster.landline = Convert.ToString(ds.Tables[0].Rows[0]["landline"]);
                               
                                objemployeeMaster.createddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                               
                                
                            
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
        return objemployeeMaster;
    }

    public Int64 Insert(employeeMaster  objemployeeMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "employee_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;



            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objemployeeMaster.id;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@employeeName", objemployeeMaster.employeeName);
            cmd.Parameters.AddWithValue("@img", objemployeeMaster.img);
            cmd.Parameters.AddWithValue("@Address1", objemployeeMaster.Address1 );
            cmd.Parameters.AddWithValue("@MobileNo1", objemployeeMaster.MobileNo1);
             
            cmd.Parameters.AddWithValue("@email", objemployeeMaster.email);
            cmd.Parameters.AddWithValue("@landline", objemployeeMaster.landline);

             


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

    public Int64 Update(employeeMaster objemployeeMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "employee_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;


            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objemployeeMaster.id;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@employeeName", objemployeeMaster.employeeName);
            cmd.Parameters.AddWithValue("@img", objemployeeMaster.img);
            cmd.Parameters.AddWithValue("@Address1", objemployeeMaster.Address1);
            cmd.Parameters.AddWithValue("@MobileNo1", objemployeeMaster.MobileNo1);

            cmd.Parameters.AddWithValue("@email", objemployeeMaster.email);
            cmd.Parameters.AddWithValue("@landline", objemployeeMaster.landline);

             

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

    public bool Delete(Int32 id)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "employee_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@id", id);

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
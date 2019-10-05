using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_agentmaster_db
{
      SqlConnection ConnectionString = new SqlConnection();
	public Cls_agentmaster_db()
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
            cmd.CommandText = "agentmaster_SelectAll";
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

    public AgentMaster  SelectById(Int64 bankid)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        AgentMaster objAgentMaster = new AgentMaster();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "agentmaster_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@aid", bankid);
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
                                objAgentMaster.aid = Convert.ToInt32(ds.Tables[0].Rows[0]["aid"]);
                                objAgentMaster.Agentname = Convert.ToString(ds.Tables[0].Rows[0]["Agentname"]);
                                objAgentMaster.Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                                objAgentMaster.MobileNo = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                                objAgentMaster.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                                objAgentMaster.createddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                                objAgentMaster.img = Convert.ToString(ds.Tables[0].Rows[0]["img"]);

                              
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
        return objAgentMaster;
    }

    public Int64 Insert(AgentMaster objAgentMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "agentmaster_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

             

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@aid";
            param.Value = objAgentMaster.aid;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@Agentname", objAgentMaster.Agentname);
            cmd.Parameters.AddWithValue("@Address", objAgentMaster.Address);
            cmd.Parameters.AddWithValue("@MobileNo", objAgentMaster.MobileNo);
            cmd.Parameters.AddWithValue("@email", objAgentMaster.email);
            cmd.Parameters.AddWithValue("@img", objAgentMaster.img);


             

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

    public Int64 Update(AgentMaster objAgentMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "agentmaster_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;


            SqlParameter param = new SqlParameter();
            param.ParameterName = "@aid";
            param.Value = objAgentMaster.aid;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@Agentname", objAgentMaster.Agentname);
            cmd.Parameters.AddWithValue("@Address", objAgentMaster.Address);
            cmd.Parameters.AddWithValue("@MobileNo", objAgentMaster.MobileNo);
            cmd.Parameters.AddWithValue("@email", objAgentMaster.email);
            cmd.Parameters.AddWithValue("@img", objAgentMaster.img);

 
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

    public bool Delete(Int32 agentid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "agentmaster_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@aid", agentid);

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

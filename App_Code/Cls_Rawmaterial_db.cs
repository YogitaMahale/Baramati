using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_Rawmaterial_db
{
      SqlConnection ConnectionString = new SqlConnection();
	public Cls_Rawmaterial_db()
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
            cmd.CommandText = "rawMaterial_SelectAll";
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

    public rawMaterialMaster SelectById(Int64 pid)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        rawMaterialMaster objrawMaterialMaster = new rawMaterialMaster();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "raw_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@id", pid);
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
                            
 
                                objrawMaterialMaster.id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);                               
                                objrawMaterialMaster.productname = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                                objrawMaterialMaster.mainimage = Convert.ToString(ds.Tables[0].Rows[0]["mainimage"]);                                
                                objrawMaterialMaster.price = Convert.ToDecimal(ds.Tables[0].Rows[0]["price"]);
                                objrawMaterialMaster.hsncode = Convert.ToString(ds.Tables[0].Rows[0]["hsncode"]);
                                objrawMaterialMaster.gstno= Convert.ToString(ds.Tables[0].Rows[0]["gstno"]);

                                objrawMaterialMaster.quantity = Convert.ToInt32(ds.Tables[0].Rows[0]["quantity"]);
                                objrawMaterialMaster.unitid = Convert.ToInt32(ds.Tables[0].Rows[0]["unitid"]);
                                objrawMaterialMaster.alertquantites = Convert.ToInt32(ds.Tables[0].Rows[0]["alertquantites"]);
                                objrawMaterialMaster.isstock = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isstock"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isstock"]);
                                objrawMaterialMaster.shortdescp = Convert.ToString(ds.Tables[0].Rows[0]["shortdescp"]);
                                objrawMaterialMaster.longdescp = Convert.ToString(ds.Tables[0].Rows[0]["longdescp"]);
                                
                                objrawMaterialMaster.isactive = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isactive"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isactive"]);
                                objrawMaterialMaster.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
                                objrawMaterialMaster.createddate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["createddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                                objrawMaterialMaster.modifieddate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["modifieddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["modifieddate"]);
                                 
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
        return objrawMaterialMaster;
    }

    public Int64 Insert(rawMaterialMaster objrawMaterialMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rawMaterial_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objrawMaterialMaster.id;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
          
            cmd.Parameters.AddWithValue("@productname", objrawMaterialMaster.productname);
            cmd.Parameters.AddWithValue("@mainimage", objrawMaterialMaster.mainimage);            
            cmd.Parameters.AddWithValue("@price", objrawMaterialMaster.price);
            cmd.Parameters.AddWithValue("@quantity", objrawMaterialMaster.quantity);
            cmd.Parameters.AddWithValue("@alertquantites", objrawMaterialMaster.alertquantites);
            cmd.Parameters.AddWithValue("@isstock", objrawMaterialMaster.isstock);
            cmd.Parameters.AddWithValue("@shortdescp", objrawMaterialMaster.shortdescp);
            cmd.Parameters.AddWithValue("@longdescp", objrawMaterialMaster.longdescp);
            cmd.Parameters.AddWithValue("@hsncode", objrawMaterialMaster.hsncode);
            cmd.Parameters.AddWithValue("@gstno", objrawMaterialMaster.gstno);
            cmd.Parameters.AddWithValue("@unitid", objrawMaterialMaster.unitid);


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

    public Int64 Update(rawMaterialMaster  objrawMaterialMaster)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rawMaterial_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value = objrawMaterialMaster.id;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);

            cmd.Parameters.AddWithValue("@productname", objrawMaterialMaster.productname);
            cmd.Parameters.AddWithValue("@mainimage", objrawMaterialMaster.mainimage);
            cmd.Parameters.AddWithValue("@price", objrawMaterialMaster.price);
            cmd.Parameters.AddWithValue("@quantity", objrawMaterialMaster.quantity);
            cmd.Parameters.AddWithValue("@alertquantites", objrawMaterialMaster.alertquantites);
            cmd.Parameters.AddWithValue("@isstock", objrawMaterialMaster.isstock);
            cmd.Parameters.AddWithValue("@shortdescp", objrawMaterialMaster.shortdescp);
            cmd.Parameters.AddWithValue("@longdescp", objrawMaterialMaster.longdescp);
            cmd.Parameters.AddWithValue("@hsncode", objrawMaterialMaster.hsncode);
            cmd.Parameters.AddWithValue("@gstno", objrawMaterialMaster.gstno);
            cmd.Parameters.AddWithValue("@unitid", objrawMaterialMaster.unitid);


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

    public bool Delete(Int64 pid )
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "rawMaterial_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@id", pid);
           
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

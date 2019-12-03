using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
public class Cls_size_db
{
    SqlConnection ConnectionString = new SqlConnection();
	public Cls_size_db()
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
            cmd.CommandText = "size_SelectAll";
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

        //public DataTable SelectAllByGroupId()
        //{
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "size_SelectAllByGroupId";
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




        public sizeMaster  SelectById(Int64 cid)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        sizeMaster objcategory = new sizeMaster();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "size_SelectById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@cid", cid);
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
                                objcategory.cid = Convert.ToInt64(ds.Tables[0].Rows[0]["cid"]);
                                objcategory.sizeName = Convert.ToString(ds.Tables[0].Rows[0]["sizeName"]);
                                objcategory.groupid = Convert.ToInt64(ds.Tables[0].Rows[0]["groupid"]);
                                    //objcategory.imagename = Convert.ToString(ds.Tables[0].Rows[0]["imagename"]);
                                    //objcategory.actualprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["actualprice"]);
                                    //objcategory.discountprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["discountprice"]);
                                    //objcategory.shortdesc = Convert.ToString(ds.Tables[0].Rows[0]["shortdesc"]);
                                    //objcategory.longdescp = Convert.ToString(ds.Tables[0].Rows[0]["longdescp"]);
                                    //objcategory.bankid = Convert.ToInt32(ds.Tables[0].Rows[0]["bankid"]);
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
        return objcategory;
    }


    public Int64 Insert(sizeMaster  objcategory)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "size_Insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@cid";
            param.Value = objcategory.cid;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@sizeName", objcategory.sizeName);
                cmd.Parameters.AddWithValue("@groupid", objcategory.groupid);


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

    public Int64 Update(sizeMaster  objcategory)
    {
        Int64 result = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "size_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@cid";
            param.Value = objcategory.cid;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@sizeName", objcategory.sizeName);
            cmd.Parameters.AddWithValue("@groupid", objcategory.groupid);

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
    public bool Delete(Int64 cid)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Size_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            cmd.Parameters.AddWithValue("@cid", cid);

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

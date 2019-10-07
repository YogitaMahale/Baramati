using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_article_db
{
        SqlConnection ConnectionString = new SqlConnection();
        public Cls_article_db()
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
                cmd.CommandText = "Article_SelectAll";
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

        //public VendorMaster SelectById(Int64 id)
        //{
        //    SqlDataAdapter da;
        //    DataSet ds = new DataSet();
        //    VendorMaster objVendorMaster = new VendorMaster();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "vendorMaster_SelectById";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = ConnectionString;
        //        cmd.Parameters.AddWithValue("@vid", id);
        //        ConnectionString.Open();
        //        da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        //vid, vendorName, Address1, Address2, MobileNo1, MobileNo2, email, landline, fk_countryId, fk_stateId, fk_cityId, createddate, isdelete, isactive

        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0] != null)
        //                {
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        {
        //                            objVendorMaster.vid = Convert.ToInt64(ds.Tables[0].Rows[0]["vid"]);
        //                            objVendorMaster.fk_agentId = Convert.ToInt64(ds.Tables[0].Rows[0]["fk_agentId"]);
        //                            objVendorMaster.vendorName = Convert.ToString(ds.Tables[0].Rows[0]["vendorName"]);
        //                            objVendorMaster.Address1 = Convert.ToString(ds.Tables[0].Rows[0]["Address1"]);
        //                            objVendorMaster.Address2 = Convert.ToString(ds.Tables[0].Rows[0]["Address2"]);
        //                            objVendorMaster.MobileNo1 = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo1"]);
        //                            objVendorMaster.MobileNo2 = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo2"]);
        //                            objVendorMaster.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);

        //                            objVendorMaster.landline = Convert.ToString(ds.Tables[0].Rows[0]["landline"]);
        //                            //objVendorMaster.fk_agentId = Convert.ToInt64(ds.Tables[0].Rows[0]["fk_agentId"]);
        //                            objVendorMaster.fk_countryId = Convert.ToString(ds.Tables[0].Rows[0]["fk_countryId"]);
        //                            objVendorMaster.fk_stateId = Convert.ToString(ds.Tables[0].Rows[0]["fk_stateId"]);
        //                            objVendorMaster.fk_cityId = Convert.ToString(ds.Tables[0].Rows[0]["fk_cityId"]);

        //                            objVendorMaster.createddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
        //                            objVendorMaster.country = Convert.ToString(ds.Tables[0].Rows[0]["countryname"]);
        //                            objVendorMaster.state = Convert.ToString(ds.Tables[0].Rows[0]["statename"]);
        //                            objVendorMaster.city = Convert.ToString(ds.Tables[0].Rows[0]["cityname"]);
        //                            //objVendorMaster.img = Convert.ToString(ds.Tables[0].Rows[0]["img"]);


        //                        }
        //                    }
        //                }
        //            }
        //        }
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
        //    return objVendorMaster;
        //}

        //public Int64 Insert(Article objArticle)
        //{
        //    Int64 result = 0;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "vendorMaster_Insert";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = ConnectionString;



        //        SqlParameter param = new SqlParameter();
        //        param.ParameterName = "@vid";
        //        param.Value = objVendorMaster.vid;
        //        param.SqlDbType = SqlDbType.Int;
        //        param.Direction = ParameterDirection.InputOutput;
        //        cmd.Parameters.Add(param);
        //        cmd.Parameters.AddWithValue("@vendorName", objVendorMaster.vendorName);
        //        cmd.Parameters.AddWithValue("@Address1", objVendorMaster.Address1);
        //        cmd.Parameters.AddWithValue("@Address2", objVendorMaster.Address2);
        //        cmd.Parameters.AddWithValue("@MobileNo1", objVendorMaster.MobileNo1);
        //        cmd.Parameters.AddWithValue("@MobileNo2", objVendorMaster.MobileNo2);
        //        cmd.Parameters.AddWithValue("@email", objVendorMaster.email);
        //        cmd.Parameters.AddWithValue("@landline", objVendorMaster.landline);

        //        cmd.Parameters.AddWithValue("@fk_agentId", objVendorMaster.fk_agentId);
        //        cmd.Parameters.AddWithValue("@fk_countryId", objVendorMaster.fk_countryId);
        //        cmd.Parameters.AddWithValue("@fk_stateId", objVendorMaster.fk_stateId);
        //        cmd.Parameters.AddWithValue("@fk_cityId", objVendorMaster.fk_cityId);
        //        cmd.Parameters.AddWithValue("@img", objVendorMaster.img);


        //        ConnectionString.Open();
        //        cmd.ExecuteNonQuery();
        //        result = Convert.ToInt64(param.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return result;
        //    }
        //    finally
        //    {
        //        ConnectionString.Close();
        //    }
        //    return result;
        //}

        //public Int64 Update(VendorMaster objVendorMaster)
        //{
        //    Int64 result = 0;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = "vendorMaster_Update";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = ConnectionString;


        //        SqlParameter param = new SqlParameter();
        //        param.ParameterName = "@vid";
        //        param.Value = objVendorMaster.vid;
        //        param.SqlDbType = SqlDbType.Int;
        //        param.Direction = ParameterDirection.InputOutput;
        //        cmd.Parameters.Add(param);
        //        cmd.Parameters.AddWithValue("@vendorName", objVendorMaster.vendorName);
        //        cmd.Parameters.AddWithValue("@Address1", objVendorMaster.Address1);
        //        cmd.Parameters.AddWithValue("@Address2", objVendorMaster.Address2);
        //        cmd.Parameters.AddWithValue("@MobileNo1", objVendorMaster.MobileNo1);
        //        cmd.Parameters.AddWithValue("@MobileNo2", objVendorMaster.MobileNo2);
        //        cmd.Parameters.AddWithValue("@email", objVendorMaster.email);
        //        cmd.Parameters.AddWithValue("@landline", objVendorMaster.landline);

        //        //cmd.Parameters.AddWithValue("@fk_agentId", objVendorMaster.fk_agentId);
        //        cmd.Parameters.AddWithValue("@fk_countryId", objVendorMaster.fk_countryId);
        //        cmd.Parameters.AddWithValue("@fk_stateId", objVendorMaster.fk_stateId);
        //        cmd.Parameters.AddWithValue("@fk_cityId", objVendorMaster.fk_cityId);
        //        cmd.Parameters.AddWithValue("@img", objVendorMaster.img);


        //        ConnectionString.Open();
        //        cmd.ExecuteNonQuery();
        //        result = Convert.ToInt64(param.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return result;
        //    }
        //    finally
        //    {
        //        ConnectionString.Close();
        //    }
        //    return result;
        //}

        public bool Delete(Int32 pid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Article_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@productid", pid);

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
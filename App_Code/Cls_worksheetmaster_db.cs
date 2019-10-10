using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_worksheetmaster_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_worksheetmaster_db
    {

        SqlConnection ConnectionString = new SqlConnection();
        public Cls_worksheetmaster_db()
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
        public DataTable SelectAll(worksheetmaster objworksheetmaster)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetmaster_SelectAll",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }
        public worksheetmaster SelectById(Int64 id)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            worksheetmaster objworksheetmaster = new worksheetmaster();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetmaster_SelectById",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };
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
                                //[id],[productid],[sizeid],[colorid],[createdby],[createddate],[isdeleted]

                                objworksheetmaster.Id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                objworksheetmaster.Productid = Convert.ToInt64(ds.Tables[0].Rows[0]["productid"]);
                                objworksheetmaster.Sizeid = Convert.ToInt64(ds.Tables[0].Rows[0]["sizeid"]);
                                objworksheetmaster.Colorid = Convert.ToInt64(ds.Tables[0].Rows[0]["colorid"]);
                                objworksheetmaster.Createdby = Convert.ToInt64(ds.Tables[0].Rows[0]["createdby"]);
                                //objworksheetmaster.isdeleted = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdeleted"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdeleted"]);
                                objworksheetmaster.Createddate= string.IsNullOrEmpty(ds.Tables[0].Rows[0]["createddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                                //objworksheetmaster.orderstatus = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderstatus"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["orderstatus"]);





                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return objworksheetmaster;
        }
        public Int64 Insert(worksheetmaster objworksheetmaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetmaster_Insert",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = 0,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);

                //[id],[productid],[sizeid],[colorid],[createdby],[createddate],[isdeleted]

                //cmd.Parameters.AddWithValue("@id", objworksheetmaster.Id);
                cmd.Parameters.AddWithValue("@productid", objworksheetmaster.Productid);
                cmd.Parameters.AddWithValue("@sizeid", objworksheetmaster.Sizeid);
                cmd.Parameters.AddWithValue("@colorid", objworksheetmaster.Colorid);
                cmd.Parameters.AddWithValue("@createdby", objworksheetmaster.Createdby);

                //cmd.Parameters.AddWithValue("@isdeleted", objworksheetmaster.isdeleted);
                cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@orderstatus", objworksheetmaster.orderstatus);




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
        public Int64 Update(worksheetmaster objworksheetmaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetmaster_Update",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = objworksheetmaster.Id,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);

                //[id],[productid],[sizeid],[colorid],[createdby],[createddate],[isdeleted]

                cmd.Parameters.AddWithValue("@id", objworksheetmaster.Id);
                cmd.Parameters.AddWithValue("@productid", objworksheetmaster.Productid);
                cmd.Parameters.AddWithValue("@sizeid", objworksheetmaster.Sizeid);
                cmd.Parameters.AddWithValue("@colorid", objworksheetmaster.Colorid);
                cmd.Parameters.AddWithValue("@createdby", objworksheetmaster.Createdby);

                //cmd.Parameters.AddWithValue("@isdeleted", objworksheetmaster.isdeleted);
                cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@orderstatus", objworksheetmaster.orderstatus);

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
        public bool Delete(Int64 ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "worksheetmaster_Delete",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                cmd.Parameters.AddWithValue("@id", ID);

                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
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
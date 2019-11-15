using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_invoicetransport_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_invoicetransport_db
    {

        SqlConnection ConnectionString = new SqlConnection();
        public Cls_invoicetransport_db()
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

        /*

        public DataTable SelectAll(invoicetransport objinvoicetransport)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "invoicetransport_SelectAll",
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
        public invoicetransport SelectById(Int64 id)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            invoicetransport objinvoicetransport = new invoicetransport();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "invoicetransport_SelectById",
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

                                objinvoicetransport.Id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                objinvoicetransport.Productid = Convert.ToInt64(ds.Tables[0].Rows[0]["productid"]);
                                //objinvoicetransport.Sizeid = Convert.ToInt64(ds.Tables[0].Rows[0]["sizeid"]);
                                objinvoicetransport.Sizeid = ds.Tables[0].Rows[0]["sizeid"].ToString();
                                objinvoicetransport.Colorid = ds.Tables[0].Rows[0]["colorid"].ToString();
                                objinvoicetransport.Createdby = Convert.ToInt64(ds.Tables[0].Rows[0]["createdby"]);
                                //objinvoicetransport.isdeleted = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdeleted"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdeleted"]);
                                objinvoicetransport.Createddate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["createddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                                //objinvoicetransport.orderstatus = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["orderstatus"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["orderstatus"]);





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
            return objinvoicetransport;
        }



        */


        public Int64 Insert(invoicetransport objinvoicetransport)
        {
            Int64 result = 0;
            try
            {

                // id, transporterid, customerid, lrno, lrdate, invoiceno, freightamount, total, createddate, isdeleted

                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "invoicetransport_Insert",
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
                
                cmd.Parameters.AddWithValue("@transporterid", objinvoicetransport.transporterid);
                cmd.Parameters.AddWithValue("@customerid", objinvoicetransport.customerid);
                cmd.Parameters.AddWithValue("@lrno", objinvoicetransport.lrno);
                cmd.Parameters.AddWithValue("@lrdate", objinvoicetransport.lrdate);

                cmd.Parameters.AddWithValue("@invoiceno", objinvoicetransport.invoiceno);
                cmd.Parameters.AddWithValue("@freightamount", objinvoicetransport.freightamount);
                cmd.Parameters.AddWithValue("@total", objinvoicetransport.total);

                //cmd.Parameters.AddWithValue("@createddate", DateTime.Now);




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


        /*
        public Int64 Update(invoicetransport objinvoicetransport)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "invoicetransport_Update",
                    CommandType = CommandType.StoredProcedure,
                    Connection = ConnectionString
                };

                SqlParameter param = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = objinvoicetransport.Id,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.InputOutput
                };
                cmd.Parameters.Add(param);

                //[id],[productid],[sizeid],[colorid],[createdby],[createddate],[isdeleted]

                cmd.Parameters.AddWithValue("@id", objinvoicetransport.Id);
                cmd.Parameters.AddWithValue("@productid", objinvoicetransport.Productid);
                cmd.Parameters.AddWithValue("@sizeid", objinvoicetransport.Sizeid);
                cmd.Parameters.AddWithValue("@colorid", objinvoicetransport.Colorid);
                cmd.Parameters.AddWithValue("@createdby", objinvoicetransport.Createdby);

                //cmd.Parameters.AddWithValue("@isdeleted", objinvoicetransport.isdeleted);
                cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@orderstatus", objinvoicetransport.orderstatus);

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
                    CommandText = "invoicetransport_Delete",
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


        */
        #endregion


    }
}
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_transporter_db
/// </summary>

namespace DatabaseLayer
{
    public class Cls_transporter_db
    {
        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor

        public Cls_transporter_db()
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

        #endregion Constructor

        public transporter SelectById(Int64 did)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            transporter objtransporter = new transporter();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transporter_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@id", did);
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
                                    objtransporter.id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                    objtransporter.name = Convert.ToString(ds.Tables[0].Rows[0]["name"]);
                                    objtransporter.mobileno = Convert.ToString(ds.Tables[0].Rows[0]["mobileno"]);
                                    objtransporter.phoneno = Convert.ToString(ds.Tables[0].Rows[0]["phoneno"]);
                                    objtransporter.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                                    objtransporter.gstno = Convert.ToString(ds.Tables[0].Rows[0]["gstno"]);
                                    objtransporter.gsttype = Convert.ToString(ds.Tables[0].Rows[0]["gsttype"]);
                                    objtransporter.aadharno = Convert.ToString(ds.Tables[0].Rows[0]["aadharno"]);
                                    objtransporter.panno = Convert.ToString(ds.Tables[0].Rows[0]["panno"]);
                                    objtransporter.address = Convert.ToString(ds.Tables[0].Rows[0]["address"]);
                                    objtransporter.remark = Convert.ToString(ds.Tables[0].Rows[0]["remark"]);
                                    
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
            return objtransporter;
        }



        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transporter_SelectAll";
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




        public Int64 Insert(transporter objtransporter)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transporter_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objtransporter.id;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@name", objtransporter.name);
                cmd.Parameters.AddWithValue("@mobileno", objtransporter.mobileno);
                cmd.Parameters.AddWithValue("@phoneno", objtransporter.phoneno);
                cmd.Parameters.AddWithValue("@email", objtransporter.email);
                cmd.Parameters.AddWithValue("@gstno", objtransporter.gstno);
                cmd.Parameters.AddWithValue("@gsttype", objtransporter.gsttype);
                cmd.Parameters.AddWithValue("@aadharno", objtransporter.aadharno);
                cmd.Parameters.AddWithValue("@panno", objtransporter.panno);
                cmd.Parameters.AddWithValue("@address", objtransporter.address);
                cmd.Parameters.AddWithValue("@remark", objtransporter.remark);


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

        public Int64 Update(transporter objtransporter)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "transporter_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objtransporter.id;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@name", objtransporter.name);
                cmd.Parameters.AddWithValue("@mobileno", objtransporter.mobileno);
                cmd.Parameters.AddWithValue("@phoneno", objtransporter.phoneno);
                cmd.Parameters.AddWithValue("@email", objtransporter.email);
                cmd.Parameters.AddWithValue("@gstno", objtransporter.gstno);
                cmd.Parameters.AddWithValue("@gsttype", objtransporter.gsttype);
                cmd.Parameters.AddWithValue("@aadharno", objtransporter.aadharno);
                cmd.Parameters.AddWithValue("@panno", objtransporter.panno);
                cmd.Parameters.AddWithValue("@address", objtransporter.address);
                cmd.Parameters.AddWithValue("@remark", objtransporter.remark);

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
                cmd.CommandText = "transporter_Delete";
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



        /*
        public DataTable SelectDealerDetails_usingId(Int64 did)
        {
            SqlDataAdapter da;
            DataTable ds = new DataTable();
            transporter objtransporter = new transporter();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "getDealerDetails_usingID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@did", did);
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
            return ds;
        }
        */
    }
}
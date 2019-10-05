using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_userregistration_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_userregistration_db()
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
        #endregion

        #region Public Methods

        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "userregistration_SelectAll";
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







        public userregistration SelectById(Int64 uid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            userregistration objuserregistration = new userregistration();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "userregistration_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@uid", uid);
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
                                    objuserregistration.uid = Convert.ToInt64(ds.Tables[0].Rows[0]["uid"]);
                                    objuserregistration.fname = Convert.ToString(ds.Tables[0].Rows[0]["fname"]);
                                    objuserregistration.mname = Convert.ToString(ds.Tables[0].Rows[0]["mname"]);
                                    objuserregistration.lname = Convert.ToString(ds.Tables[0].Rows[0]["lname"]);
                                    objuserregistration.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                                    objuserregistration.phone = Convert.ToString(ds.Tables[0].Rows[0]["phone"]);
                                    objuserregistration.password = Convert.ToString(ds.Tables[0].Rows[0]["password"]);
                                    objuserregistration.dob = Convert.ToString(ds.Tables[0].Rows[0]["dob"]);
                                    objuserregistration.address1 = Convert.ToString(ds.Tables[0].Rows[0]["address1"]);
                                    objuserregistration.address2 = Convert.ToString(ds.Tables[0].Rows[0]["address2"]);
                                    objuserregistration.isguest = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isguest"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isguest"]);
                                    objuserregistration.registrationdate = Convert.ToString(ds.Tables[0].Rows[0]["registrationdate"]);
                                    objuserregistration.uguid = Convert.ToString(ds.Tables[0].Rows[0]["uguid"]);
                                    objuserregistration.isactive = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isactive"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isactive"]);
                                    objuserregistration.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
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
            return objuserregistration;
        }

        public Int64 Insert(userregistration objuserregistration)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "userregistration_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@uid";
                param.Value = objuserregistration.uid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@fname", objuserregistration.fname);
                cmd.Parameters.AddWithValue("@mname", objuserregistration.mname);
                cmd.Parameters.AddWithValue("@lname", objuserregistration.lname);
                cmd.Parameters.AddWithValue("@email", objuserregistration.email);
                cmd.Parameters.AddWithValue("@phone", objuserregistration.phone);
                cmd.Parameters.AddWithValue("@password", objuserregistration.password);
                cmd.Parameters.AddWithValue("@dob", objuserregistration.dob);
                cmd.Parameters.AddWithValue("@address1", objuserregistration.address1);
                cmd.Parameters.AddWithValue("@address2", objuserregistration.address2);
                cmd.Parameters.AddWithValue("@isguest", objuserregistration.isguest);
                cmd.Parameters.AddWithValue("@registrationdate", objuserregistration.registrationdate);
                cmd.Parameters.AddWithValue("@uguid", objuserregistration.uguid);
                cmd.Parameters.AddWithValue("@isactive", objuserregistration.isactive);
                cmd.Parameters.AddWithValue("@isdelete", objuserregistration.isdelete);

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

        public Int64 Update(userregistration objuserregistration)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "userregistration_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@uid";
                param.Value = objuserregistration.uid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@fname", objuserregistration.fname);
                cmd.Parameters.AddWithValue("@mname", objuserregistration.mname);
                cmd.Parameters.AddWithValue("@lname", objuserregistration.lname);
                cmd.Parameters.AddWithValue("@email", objuserregistration.email);
                //cmd.Parameters.AddWithValue("@phone", objuserregistration.phone);
                //cmd.Parameters.AddWithValue("@password", objuserregistration.password);
                cmd.Parameters.AddWithValue("@dob", objuserregistration.dob);
                cmd.Parameters.AddWithValue("@address1", objuserregistration.address1);
                cmd.Parameters.AddWithValue("@address2", objuserregistration.address2);
                //cmd.Parameters.AddWithValue("@isguest", objuserregistration.isguest);
                //cmd.Parameters.AddWithValue("@registrationdate", objuserregistration.registrationdate);
                //cmd.Parameters.AddWithValue("@uguid", objuserregistration.uguid);
                cmd.Parameters.AddWithValue("@isactive", objuserregistration.isactive);
                cmd.Parameters.AddWithValue("@isdelete", objuserregistration.isdelete);

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
        public Int64 WebsiteUser_Status(string Username, string password, Boolean IsActive)
        {
            Int64 result = 0;
            try
            {
                Int64 Result = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "WebsiteUser_Status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@isactive", IsActive);

                ConnectionString.Open();
                result = cmd.ExecuteNonQuery();
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

        #endregion


    }

}

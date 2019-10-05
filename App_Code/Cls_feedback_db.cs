using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_feedback_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_feedback_db()
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
        public DataTable SelectAll(feedback objfeedback)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "feedback_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;


                #region Null checking
                if (objfeedback.feedbackid != 0 && objfeedback.feedbackid != null)
                {
                    cmd.Parameters.AddWithValue("@feedbackid", objfeedback.feedbackid);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@feedbackid", DBNull.Value);
                }

                if (objfeedback.name != "" && objfeedback.name != null)
                {
                    cmd.Parameters.AddWithValue("@name", objfeedback.name);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@name", DBNull.Value);
                }

                if (objfeedback.phone != "" && objfeedback.phone != null)
                {
                    cmd.Parameters.AddWithValue("@phone", objfeedback.phone);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@phone", DBNull.Value);
                }

                if (objfeedback.feedbackmessage != null)
                {
                    cmd.Parameters.AddWithValue("@feedbackmessage", objfeedback.feedbackmessage);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@feedbackmessage", DBNull.Value);
                }

                if (objfeedback.feedbackdt != null)
                {
                    if (objfeedback.feedbackdt == DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue("@feedbackdt", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@feedbackdt", objfeedback.feedbackdt);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@feedbackdt", DBNull.Value);
                }
                #endregion

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
        public feedback SelectById(Int64 feedbackid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            feedback objfeedback = new feedback();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "feedback_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@feedbackid", feedbackid);
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
                                    objfeedback.feedbackid = Convert.ToInt64(ds.Tables[0].Rows[0]["feedbackid"]);
                                    objfeedback.name = Convert.ToString(ds.Tables[0].Rows[0]["name"]);
                                    objfeedback.phone = Convert.ToString(ds.Tables[0].Rows[0]["phone"]);
                                    objfeedback.feedbackmessage = Convert.ToString(ds.Tables[0].Rows[0]["feedbackmessage"]);
                                    objfeedback.feedbackdt = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["feedbackdt"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["feedbackdt"]);
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
            return objfeedback;
        }
        public Int64 Insert(feedback objfeedback)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "feedback_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@feedbackid";
                param.Value = objfeedback.feedbackid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@name", objfeedback.name);
                cmd.Parameters.AddWithValue("@phone", objfeedback.phone);
                cmd.Parameters.AddWithValue("@feedback", objfeedback.feedbackmessage);
                cmd.Parameters.AddWithValue("@feedbackdt", objfeedback.feedbackdt);

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
        public Int64 Update(feedback objfeedback)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "feedback_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@feedbackid";
                param.Value = objfeedback.feedbackid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@name", objfeedback.name);
                cmd.Parameters.AddWithValue("@phone", objfeedback.phone);
                cmd.Parameters.AddWithValue("@feedbackmessage", objfeedback.feedbackmessage);
                cmd.Parameters.AddWithValue("@feedbackdt", objfeedback.feedbackdt);

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
        public bool Delete(Int64 feedbackid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "feedback_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@feedbackid", feedbackid);

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

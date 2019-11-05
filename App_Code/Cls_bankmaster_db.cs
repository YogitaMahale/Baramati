using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_bankmaster_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_bankmaster_db()
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

        public DataTable SelectAll_Admin()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_SelectAllAdmin";
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

        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_SelectAll";
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

        public bankmaster SelectById(Int64 bankid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            bankmaster objbankmaster = new bankmaster();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@bankid", bankid);
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
                                    objbankmaster.bankid = Convert.ToInt32(ds.Tables[0].Rows[0]["bankid"]);
                                    objbankmaster.bankname = Convert.ToString(ds.Tables[0].Rows[0]["bankname"]);
                                    objbankmaster.bankifsccode = Convert.ToString(ds.Tables[0].Rows[0]["bankifsccode"]);
                                    objbankmaster.bankbranch = Convert.ToString(ds.Tables[0].Rows[0]["bankbranch"]);
                                    objbankmaster.accountno = Convert.ToString(ds.Tables[0].Rows[0]["accountno"]);
                                    objbankmaster.accountholdername = Convert.ToString(ds.Tables[0].Rows[0]["accountholdername"]);
                                    objbankmaster.balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["balance"]);

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
            return objbankmaster;
        }

        public Int64 Insert(bankmaster objbankmaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@bankid";
                param.Value = objbankmaster.bankid;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@bankname", objbankmaster.bankname);
                cmd.Parameters.AddWithValue("@bankifsccode", objbankmaster.bankifsccode);
                cmd.Parameters.AddWithValue("@bankbranch", objbankmaster.bankbranch);
                cmd.Parameters.AddWithValue("@accountno", objbankmaster.accountno);
                cmd.Parameters.AddWithValue("@accountholdername", objbankmaster.accountholdername);
                cmd.Parameters.AddWithValue("@balance", objbankmaster.balance);

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

        public Int64 Update(bankmaster objbankmaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@bankid";
                param.Value = objbankmaster.bankid;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@bankname", objbankmaster.bankname);
                cmd.Parameters.AddWithValue("@bankifsccode", objbankmaster.bankifsccode);
                cmd.Parameters.AddWithValue("@bankbranch", objbankmaster.bankbranch);
                cmd.Parameters.AddWithValue("@accountno", objbankmaster.accountno);
                cmd.Parameters.AddWithValue("@accountholdername", objbankmaster.accountholdername);
                cmd.Parameters.AddWithValue("@balance", objbankmaster.balance);

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

        public bool Delete(Int32 bankid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@bankid", bankid);

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

        public bool IsActive(Int32 bankid, bool isactive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "bankmaster_IsActive";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@bankid", bankid);
                cmd.Parameters.AddWithValue("@isactive", isactive);

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

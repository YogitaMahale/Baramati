using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_articleloosepairs_db
/// </summary>
namespace DatabaseLayer
{
    public class Cls_articleloosepairs_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_articleloosepairs_db()
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
        /*
        public DataTable SelectAll_Admin()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleloosepairs_SelectAllAdmin";
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
                cmd.CommandText = "articleloosepairs_SelectAll";
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

        public articleloosepairs SelectById(Int64 bankid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            articleloosepairs objarticleloosepairs = new articleloosepairs();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleloosepairs_SelectById";
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
                                    
                                    objarticleloosepairs.bankid = Convert.ToInt32(ds.Tables[0].Rows[0]["bankid"]);
                                    objarticleloosepairs.bankname = Convert.ToString(ds.Tables[0].Rows[0]["bankname"]);
                                    objarticleloosepairs.bankifsccode = Convert.ToString(ds.Tables[0].Rows[0]["bankifsccode"]);
                                    objarticleloosepairs.bankbranch = Convert.ToString(ds.Tables[0].Rows[0]["bankbranch"]);
                                    objarticleloosepairs.accountno = Convert.ToString(ds.Tables[0].Rows[0]["accountno"]);
                                    objarticleloosepairs.accountholdername = Convert.ToString(ds.Tables[0].Rows[0]["accountholdername"]);
                                    objarticleloosepairs.balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["balance"]);

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
            return objarticleloosepairs;
        }

        */

        public Int64 InsertUpdate(articleloosepairs objarticleloosepairs)
        {
            //id, pid, sizeid, colorid, quantity

            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleloosepairs_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objarticleloosepairs.id;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@pid", objarticleloosepairs.pid);
                cmd.Parameters.AddWithValue("@sizegroupid", objarticleloosepairs.sizegroupid);
                cmd.Parameters.AddWithValue("@colorid", objarticleloosepairs.colorid);
                cmd.Parameters.AddWithValue("@quantity", objarticleloosepairs.quantity);

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

        /*
        public Int64 Update(articleloosepairs objarticleloosepairs)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleloosepairs_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@bankid";
                param.Value = objarticleloosepairs.bankid;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@bankname", objarticleloosepairs.bankname);
                cmd.Parameters.AddWithValue("@bankifsccode", objarticleloosepairs.bankifsccode);
                cmd.Parameters.AddWithValue("@bankbranch", objarticleloosepairs.bankbranch);
                cmd.Parameters.AddWithValue("@accountno", objarticleloosepairs.accountno);
                cmd.Parameters.AddWithValue("@accountholdername", objarticleloosepairs.accountholdername);
                cmd.Parameters.AddWithValue("@balance", objarticleloosepairs.balance);

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
                cmd.CommandText = "articleloosepairs_Delete";
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
                cmd.CommandText = "articleloosepairs_IsActive";
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
      
        */
        #endregion


    }

}

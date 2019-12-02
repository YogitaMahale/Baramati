using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_articleproduction_db
/// </summary>

namespace DatabaseLayer
{
    public class Cls_articleproduction_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_articleproduction_db()
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
                cmd.CommandText = "articleproduction_SelectAllAdmin";
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
                cmd.CommandText = "articleproduction_SelectAll";
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

        public articleproduction SelectById(Int64 bankid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            articleproduction objarticleproduction = new articleproduction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleproduction_SelectById";
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
                                    
                                    objarticleproduction.bankid = Convert.ToInt32(ds.Tables[0].Rows[0]["bankid"]);
                                    objarticleproduction.bankname = Convert.ToString(ds.Tables[0].Rows[0]["bankname"]);
                                    objarticleproduction.bankifsccode = Convert.ToString(ds.Tables[0].Rows[0]["bankifsccode"]);
                                    objarticleproduction.bankbranch = Convert.ToString(ds.Tables[0].Rows[0]["bankbranch"]);
                                    objarticleproduction.accountno = Convert.ToString(ds.Tables[0].Rows[0]["accountno"]);
                                    objarticleproduction.accountholdername = Convert.ToString(ds.Tables[0].Rows[0]["accountholdername"]);
                                    objarticleproduction.balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["balance"]);

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
            return objarticleproduction;
        }

        */

        public Int64 Insert(articleproduction objarticleproduction)
        {
            //id, worksheetno, employeeid, totalpairs, vshape, silai, factorysecond, isdeleted, createddate

            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleproduction_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objarticleproduction.id;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@worksheetno", objarticleproduction.worksheetno);
                cmd.Parameters.AddWithValue("@employeeid", objarticleproduction.employeeid);
                cmd.Parameters.AddWithValue("@totalpairs", objarticleproduction.totalpairs);
                cmd.Parameters.AddWithValue("@vshape", objarticleproduction.vshape);
                cmd.Parameters.AddWithValue("@silai", objarticleproduction.silai);
                cmd.Parameters.AddWithValue("@factorysecond", objarticleproduction.factorysecond);

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
        public Int64 Update(articleproduction objarticleproduction)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articleproduction_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@bankid";
                param.Value = objarticleproduction.bankid;
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@bankname", objarticleproduction.bankname);
                cmd.Parameters.AddWithValue("@bankifsccode", objarticleproduction.bankifsccode);
                cmd.Parameters.AddWithValue("@bankbranch", objarticleproduction.bankbranch);
                cmd.Parameters.AddWithValue("@accountno", objarticleproduction.accountno);
                cmd.Parameters.AddWithValue("@accountholdername", objarticleproduction.accountholdername);
                cmd.Parameters.AddWithValue("@balance", objarticleproduction.balance);

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
                cmd.CommandText = "articleproduction_Delete";
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
                cmd.CommandText = "articleproduction_IsActive";
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

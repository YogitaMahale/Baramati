using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_employeewages_db
/// </summary>

namespace DatabaseLayer
{
    public class Cls_employeewages_db
    {
        SqlConnection ConnectionString = new SqlConnection();
        public Cls_employeewages_db()
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
                cmd.CommandText = "employeewages_SelectAll";
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




        public employeewagesMaster SelectById(Int64 cid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            employeewagesMaster objcategory = new employeewagesMaster();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "employeewages_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@id", cid);
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
                                    objcategory.id = Convert.ToInt64(ds.Tables[0].Rows[0]["id"]);
                                    objcategory.eid = Convert.ToInt64(ds.Tables[0].Rows[0]["empid"]);
                                    //objcategory.employeename = Convert.ToString(ds.Tables[0].Rows[0]["employeename"]);
                                    objcategory.createddate = Convert.ToString(ds.Tables[0].Rows[0]["createddate"]);
                                    objcategory.firstcount = Convert.ToInt32(ds.Tables[0].Rows[0]["firstcount"]);
                                    objcategory.firstrate = Convert.ToDecimal(ds.Tables[0].Rows[0]["firstrate"]);
                                    objcategory.secondrate = Convert.ToDecimal(ds.Tables[0].Rows[0]["secondrate"]);
                                    objcategory.vshaperate = Convert.ToDecimal(ds.Tables[0].Rows[0]["vshaperate"]);
                                    objcategory.silairate = Convert.ToDecimal(ds.Tables[0].Rows[0]["silairate"]);
                                    objcategory.currentfirstrate = Convert.ToDecimal(ds.Tables[0].Rows[0]["currentfirstrate"]);
                                    objcategory.currentsecondrate = Convert.ToDecimal(ds.Tables[0].Rows[0]["currentsecondrate"]);
                                    objcategory.currentvshaperate = Convert.ToDecimal(ds.Tables[0].Rows[0]["currentvshaperate"]);
                                    objcategory.currentsilairate = Convert.ToDecimal(ds.Tables[0].Rows[0]["currentsilairate"]);


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


        public Int64 Insert(employeewagesMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "employeewages_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                //param.Value = objcategory.id;
                param.Value = 0;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@empid", objcategory.eid);
                cmd.Parameters.AddWithValue("@firstcount", objcategory.firstcount);
                cmd.Parameters.AddWithValue("@firstrate", objcategory.firstrate);
                cmd.Parameters.AddWithValue("@secondrate", objcategory.secondrate);
                cmd.Parameters.AddWithValue("@vshaperate", objcategory.vshaperate);
                cmd.Parameters.AddWithValue("@silairate", objcategory.silairate);
                cmd.Parameters.AddWithValue("@currentfirstrate", objcategory.currentfirstrate);
                cmd.Parameters.AddWithValue("@currentsecondrate", objcategory.currentsecondrate);
                cmd.Parameters.AddWithValue("@currentvshaperate", objcategory.currentvshaperate);
                cmd.Parameters.AddWithValue("@currentsilairate", objcategory.currentsilairate);


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

        public Int64 Update(employeewagesMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "employeewages_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id";
                param.Value = objcategory.id;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@firstcount", objcategory.firstcount);
                cmd.Parameters.AddWithValue("@firstrate", objcategory.firstrate);
                cmd.Parameters.AddWithValue("@secondrate", objcategory.secondrate);
                cmd.Parameters.AddWithValue("@vshaperate", objcategory.vshaperate);
                cmd.Parameters.AddWithValue("@silairate", objcategory.silairate);
                cmd.Parameters.AddWithValue("@currentfirstrate", objcategory.currentfirstrate);
                cmd.Parameters.AddWithValue("@currentsecondrate", objcategory.currentsecondrate);
                cmd.Parameters.AddWithValue("@currentvshaperate", objcategory.currentvshaperate);
                cmd.Parameters.AddWithValue("@currentsilairate", objcategory.currentsilairate);

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
                cmd.CommandText = "employeewages_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@id", cid);

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

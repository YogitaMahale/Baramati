using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_dealermaster_db
    {
        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor

        public Cls_dealermaster_db()
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




        public DataTable SelectAll()
        {
            DataSet ds = new DataSet();
           
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "dealer_SelectAllAdmin";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                ConnectionString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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



        public dealermaster SelectById(Int64 did)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            dealermaster objdealermaster = new dealermaster();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "dealermaster_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@did", did);
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
                                    objdealermaster.did = Convert.ToInt64(ds.Tables[0].Rows[0]["did"]);
                                    objdealermaster.name = Convert.ToString(ds.Tables[0].Rows[0]["name"]);
                                    objdealermaster.userloginmobileno = Convert.ToString(ds.Tables[0].Rows[0]["userloginmobileno"]);
                                    objdealermaster.password = Convert.ToString(ds.Tables[0].Rows[0]["password"]);
                                    objdealermaster.whatappno = Convert.ToString(ds.Tables[0].Rows[0]["whatappno"]);
                                    objdealermaster.email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);
                                    objdealermaster.gstno = Convert.ToString(ds.Tables[0].Rows[0]["gstno"]);
                                    objdealermaster.aadharno = Convert.ToString(ds.Tables[0].Rows[0]["aadharno"]);
                                    objdealermaster.panno = Convert.ToString(ds.Tables[0].Rows[0]["panno"]);
                                    objdealermaster.address1 = Convert.ToString(ds.Tables[0].Rows[0]["address1"]);
                                    objdealermaster.address2 = Convert.ToString(ds.Tables[0].Rows[0]["address2"]);
                                    objdealermaster.city = Convert.ToString(ds.Tables[0].Rows[0]["city"]);
                                    objdealermaster.state = Convert.ToString(ds.Tables[0].Rows[0]["state"]);
                                    objdealermaster.guid = Convert.ToString(ds.Tables[0].Rows[0]["guid"]);
                                    objdealermaster.agentid = Convert.ToInt64(ds.Tables[0].Rows[0]["FK_agentId"]);
                                    objdealermaster.district = Convert.ToInt64(ds.Tables[0].Rows[0]["district"]);
                                    objdealermaster.balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["balance"]);

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
            return objdealermaster;
        }

        public Int64 Insert(dealermaster objdealermaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "dealermaster_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@did";
                param.Value = objdealermaster.did;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@name", objdealermaster.name);
                cmd.Parameters.AddWithValue("@userloginmobileno", objdealermaster.userloginmobileno);
                cmd.Parameters.AddWithValue("@password", objdealermaster.password);
                cmd.Parameters.AddWithValue("@whatappno", objdealermaster.whatappno);
                cmd.Parameters.AddWithValue("@email", objdealermaster.email);
                cmd.Parameters.AddWithValue("@gstno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@aadharno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@panno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@address1", objdealermaster.address1);
                cmd.Parameters.AddWithValue("@address2", objdealermaster.address2);
                cmd.Parameters.AddWithValue("@city", objdealermaster.city);
                cmd.Parameters.AddWithValue("@state", objdealermaster.state);
                cmd.Parameters.AddWithValue("@guid", objdealermaster.guid);
                cmd.Parameters.AddWithValue("@district", objdealermaster.district);
                cmd.Parameters.AddWithValue("@balance", objdealermaster.balance);



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

        public Int64 Update(dealermaster objdealermaster)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "dealermaster_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@did";
                param.Value = objdealermaster.did;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@name", objdealermaster.name);
                cmd.Parameters.AddWithValue("@whatappno", objdealermaster.whatappno);
                cmd.Parameters.AddWithValue("@email", objdealermaster.email);
                cmd.Parameters.AddWithValue("@gstno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@aadharno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@panno", objdealermaster.gstno);
                cmd.Parameters.AddWithValue("@address1", objdealermaster.address1);
                cmd.Parameters.AddWithValue("@address2", objdealermaster.address2);
                cmd.Parameters.AddWithValue("@city", objdealermaster.city);
                cmd.Parameters.AddWithValue("@state", objdealermaster.state);
                cmd.Parameters.AddWithValue("@Img", objdealermaster.Img);
                //cmd.Parameters.AddWithValue("@FK_agentId", objdealermaster.agentid);
                cmd.Parameters.AddWithValue("@district", objdealermaster.district);
                ConnectionString.Close();
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


        public DataTable SelectDealerDetails_usingId(Int64 did)
        {
            SqlDataAdapter da;
            DataTable ds = new DataTable();
            dealermaster objdealermaster = new dealermaster();
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

    }
}
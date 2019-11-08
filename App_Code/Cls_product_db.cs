using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;

namespace DatabaseLayer
{
    public class Cls_product_db
    {

        SqlConnection ConnectionString = new SqlConnection();

        #region Constructor
        public Cls_product_db()
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
                cmd.CommandText = "product_SelectAll";
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

        public DataTable SelectAllAdmin()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_SelectAllAdmin";
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

        public DataTable SelectAllProductUsingCategoryIdAdmin(Int64 CategoryId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_SelectAllProductUsingCategoryIdAdmin";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", CategoryId);
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

        public DataTable SelectAllProductUsingCategoryId(Int64 CategoryId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_SelectAllProductUsingCategoryId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", CategoryId);
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

        public DataTable Product_WSSelectAllProductUsingCategoryId(Int64 CategoryId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_WSSelectAllProductUsingCategoryId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", CategoryId);
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

        public DataTable Product_WSSelectAllProductUsingCategoryIdandBrandId(Int64 CategoryId, Int64 brandId)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_WSSelectAllProductUsingCategoryIdandBrandId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@cid", CategoryId);
                     cmd.Parameters.AddWithValue("@brandid", brandId);
                
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


        
        public product SelectById(Int64 pid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            product objproduct = new product();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", pid);
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
                                    objproduct.pid = Convert.ToInt64(ds.Tables[0].Rows[0]["pid"]);
                                    objproduct.cid = Convert.ToInt64(ds.Tables[0].Rows[0]["cid"]);
                                    objproduct.productname = Convert.ToString(ds.Tables[0].Rows[0]["productname"]);
                                    objproduct.mainimage = Convert.ToString(ds.Tables[0].Rows[0]["mainimage"]);
                                    objproduct.sku = Convert.ToString(ds.Tables[0].Rows[0]["sku"]);
                                    objproduct.customerprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["customerprice"]);
                                    objproduct.dealerprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["dealerprice"]);
                                    objproduct.discountprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["discountprice"]);
                                    objproduct.quantites = Convert.ToInt32(ds.Tables[0].Rows[0]["quantites"]);
                                    objproduct.alertquantites = Convert.ToInt32(ds.Tables[0].Rows[0]["alertquantites"]);
                                    objproduct.isstock = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isstock"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isstock"]);
                                    objproduct.shortdescp = Convert.ToString(ds.Tables[0].Rows[0]["shortdescp"]);
                                    objproduct.longdescp = Convert.ToString(ds.Tables[0].Rows[0]["longdescp"]);
                                    objproduct.image = Convert.ToString(ds.Tables[0].Rows[0]["image"]);
                                    objproduct.isactive = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isactive"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isactive"]);
                                    objproduct.isdelete = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isdelete"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isdelete"]);
                                    objproduct.createddate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["createddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["createddate"]);
                                    objproduct.modifieddate = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["modifieddate"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0]["modifieddate"]);
                                    objproduct.video1 = Convert.ToString(ds.Tables[0].Rows[0]["video1"]);
                                    objproduct.video2 = Convert.ToString(ds.Tables[0].Rows[0]["video2"]);
                                    objproduct.video3 = Convert.ToString(ds.Tables[0].Rows[0]["video3"]);
                                    objproduct.video4 = Convert.ToString(ds.Tables[0].Rows[0]["video4"]);
                                    objproduct.gst = Convert.ToDecimal(ds.Tables[0].Rows[0]["gst"]);
                                    objproduct.video_name_1 = Convert.ToString(ds.Tables[0].Rows[0]["video_name_1"]);
                                    objproduct.video_name_2 = Convert.ToString(ds.Tables[0].Rows[0]["video_name_2"]);
                                    objproduct.video_name_3 = Convert.ToString(ds.Tables[0].Rows[0]["video_name_3"]);
                                    objproduct.video_name_4 = Convert.ToString(ds.Tables[0].Rows[0]["video_name_4"]);
                                    objproduct.wholesaleprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["wholesaleprice"]);
                                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["superwholesaleprice"].ToString()))
                                        objproduct.superwholesaleprice = Convert.ToDecimal(ds.Tables[0].Rows[0]["superwholesaleprice"]);

                                    objproduct.HSNCode = Convert.ToString(ds.Tables[0].Rows[0]["HSNCode"]);
                                    objproduct.RealStock = Convert.ToInt16(ds.Tables[0].Rows[0]["RealStock"]);
                                    objproduct.LandingPrice = Convert.ToDecimal(ds.Tables[0].Rows[0]["LandingPrice"]);
                                    objproduct.isHotproduct = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isHotproduct"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["isHotproduct"]);

                                    objproduct.fk_colorId = Convert.ToInt64(ds.Tables[0].Rows[0]["fk_colorId"]);
                                    objproduct.fk_sizeId = Convert.ToInt64(ds.Tables[0].Rows[0]["fk_sizeId"]);
                                    objproduct.packing = Convert.ToInt32(ds.Tables[0].Rows[0]["packing"]);
                                    objproduct.brandid= Convert.ToInt64(ds.Tables[0].Rows[0]["brandid"]);
                                    objproduct.maincategoryid = Convert.ToInt64(ds.Tables[0].Rows[0]["maincategoryid"]);
                                    objproduct.gsttype = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["gsttype"].ToString()) ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["gsttype"]);
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
            return objproduct;
        }

        public DataTable SearchProductUsingProductName(string Productname)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SearchSelectProductUsingProductName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@productname", Productname);
                ConnectionString.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ConnectionString.Close();
            }
            return ds.Tables[0];
        }

        public DataTable SelectProductDetailsUsingProductId(Int64 pid)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            product objproduct = new product();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SelectProductDetailsUsingProductId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", pid);
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

        public Int64 Insert(product objproduct)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@pid";
                param.Value = objproduct.pid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@cid", objproduct.cid);
                cmd.Parameters.AddWithValue("@productname", objproduct.productname);
                cmd.Parameters.AddWithValue("@mainimage", objproduct.mainimage);
                cmd.Parameters.AddWithValue("@sku", objproduct.sku);
                cmd.Parameters.AddWithValue("@customerprice", objproduct.customerprice);
                cmd.Parameters.AddWithValue("@dealerprice", objproduct.dealerprice);
                cmd.Parameters.AddWithValue("@discountprice", objproduct.discountprice);
                cmd.Parameters.AddWithValue("@gst", objproduct.gst);
                cmd.Parameters.AddWithValue("@quantites", objproduct.quantites);
                cmd.Parameters.AddWithValue("@alertquantites", objproduct.alertquantites);
                cmd.Parameters.AddWithValue("@isstock", objproduct.isstock);
                cmd.Parameters.AddWithValue("@shortdescp", objproduct.shortdescp);
                cmd.Parameters.AddWithValue("@longdescp", objproduct.longdescp);
                cmd.Parameters.AddWithValue("@image", objproduct.image);
                cmd.Parameters.AddWithValue("@video1", objproduct.video1);
                cmd.Parameters.AddWithValue("@video2", objproduct.video2);
                cmd.Parameters.AddWithValue("@video3", objproduct.video3);
                cmd.Parameters.AddWithValue("@video4", objproduct.video4);
                cmd.Parameters.AddWithValue("@wholesaleprice", objproduct.wholesaleprice);
                cmd.Parameters.AddWithValue("@superwholesaleprice", objproduct.superwholesaleprice);
                cmd.Parameters.AddWithValue("@video_name_1", objproduct.video_name_1);
                cmd.Parameters.AddWithValue("@video_name_2", objproduct.video_name_2);
                cmd.Parameters.AddWithValue("@video_name_3", objproduct.video_name_3);
                cmd.Parameters.AddWithValue("@video_name_4", objproduct.video_name_4);

                cmd.Parameters.AddWithValue("@HSNCode", objproduct.HSNCode);
                cmd.Parameters.AddWithValue("@RealStock", objproduct.RealStock);
                cmd.Parameters.AddWithValue("@LandingPrice", objproduct.LandingPrice);
                cmd.Parameters.AddWithValue("@isHotproduct", objproduct.isHotproduct);

                cmd.Parameters.AddWithValue("@fk_sizeId", objproduct.fk_sizeId );
                cmd.Parameters.AddWithValue("@fk_colorId", objproduct.fk_colorId );
                cmd.Parameters.AddWithValue("@packing", objproduct.packing);
                cmd.Parameters.AddWithValue("@brandid", objproduct.brandid);

                cmd.Parameters.AddWithValue("@fk_mainCategoryId", objproduct.maincategoryid);
                cmd.Parameters.AddWithValue("@gsttype", objproduct.gsttype);
                
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

        public Int64 Update(product objproduct)
        {
            Int64 result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@pid";
                param.Value = objproduct.pid;
                param.SqlDbType = SqlDbType.BigInt;
                param.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@cid", objproduct.cid);
                cmd.Parameters.AddWithValue("@productname", objproduct.productname);
                cmd.Parameters.AddWithValue("@mainimage", objproduct.mainimage);
                cmd.Parameters.AddWithValue("@sku", objproduct.sku);
                cmd.Parameters.AddWithValue("@customerprice", objproduct.customerprice);
                cmd.Parameters.AddWithValue("@dealerprice", objproduct.dealerprice);
                cmd.Parameters.AddWithValue("@discountprice", objproduct.discountprice);
                cmd.Parameters.AddWithValue("@gst", objproduct.gst);
                cmd.Parameters.AddWithValue("@quantites", objproduct.quantites);
                cmd.Parameters.AddWithValue("@alertquantites", objproduct.alertquantites);
                cmd.Parameters.AddWithValue("@isstock", objproduct.isstock);
                cmd.Parameters.AddWithValue("@shortdescp", objproduct.shortdescp);
                cmd.Parameters.AddWithValue("@longdescp", objproduct.longdescp);
                cmd.Parameters.AddWithValue("@image", objproduct.image);
                cmd.Parameters.AddWithValue("@video1", objproduct.video1);
                cmd.Parameters.AddWithValue("@video2", objproduct.video2);
                cmd.Parameters.AddWithValue("@video3", objproduct.video3);
                cmd.Parameters.AddWithValue("@video4", objproduct.video4);
                cmd.Parameters.AddWithValue("@wholesaleprice", objproduct.wholesaleprice);
                cmd.Parameters.AddWithValue("@superwholesaleprice", objproduct.superwholesaleprice);
                cmd.Parameters.AddWithValue("@video_name_1", objproduct.video_name_1);
                cmd.Parameters.AddWithValue("@video_name_2", objproduct.video_name_2);
                cmd.Parameters.AddWithValue("@video_name_3", objproduct.video_name_3);
                cmd.Parameters.AddWithValue("@video_name_4", objproduct.video_name_4);

                cmd.Parameters.AddWithValue("@HSNCode", objproduct.HSNCode);
                cmd.Parameters.AddWithValue("@RealStock", objproduct.RealStock);
                cmd.Parameters.AddWithValue("@LandingPrice", objproduct.LandingPrice);
                cmd.Parameters.AddWithValue("@isHotproduct", objproduct.isHotproduct);
                cmd.Parameters.AddWithValue("@fk_sizeId", objproduct.fk_sizeId);
                cmd.Parameters.AddWithValue("@fk_colorId", objproduct.fk_colorId);
                cmd.Parameters.AddWithValue("@packing", objproduct.packing);
                cmd.Parameters.AddWithValue("@brandid", objproduct.brandid);

                cmd.Parameters.AddWithValue("@fk_mainCategoryId", objproduct.maincategoryid);
                cmd.Parameters.AddWithValue("@gsttype", objproduct.gsttype);

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

        public bool Delete(Int64 pid, Int64 cid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;

                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@cid", cid);

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

        public bool Product_UpdatePrice(Int64 ProductId, decimal CustomerPrice, decimal DealerPrice, decimal DiscountPrice, decimal GST, Int32 StockQuantites, bool IsStock, bool IsActive, decimal Wholesaleprice, decimal SuperWholesaleprice, int StockAlertQuantites, bool isHotproduct)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_UpdatePrice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", ProductId);
                cmd.Parameters.AddWithValue("@customerprice", CustomerPrice);
                cmd.Parameters.AddWithValue("@dealerprice", DealerPrice);
                cmd.Parameters.AddWithValue("@wholesaleprice", Wholesaleprice);
                cmd.Parameters.AddWithValue("@superwholesaleprice", SuperWholesaleprice);
                cmd.Parameters.AddWithValue("@discountprice", DiscountPrice);
                cmd.Parameters.AddWithValue("@gst", GST);
                cmd.Parameters.AddWithValue("@quantites", StockQuantites);
                cmd.Parameters.AddWithValue("@stockalertquantites", StockAlertQuantites);
                cmd.Parameters.AddWithValue("@isstock", IsStock);
                cmd.Parameters.AddWithValue("@isactive", IsActive);
                cmd.Parameters.AddWithValue("@isHotproduct", isHotproduct);
                
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConnectionString.Close();
            }
            return true;
        }

        public bool Product_IsActive(Int64 ProductId, Boolean IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_IsActive";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", ProductId);
                cmd.Parameters.AddWithValue("@isactive", IsActive);
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConnectionString.Close();
            }
            return true;
        }
        public bool Product_IsHotProduct(Int64 ProductId, Boolean IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Product_IsHotProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", ProductId);
                cmd.Parameters.AddWithValue("@isactive", IsActive);
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConnectionString.Close();
            }
            return true;
        }

        public bool Product_IsStock(Int64 ProductId, Boolean IsStock)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "product_IsStock";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = ConnectionString;
                cmd.Parameters.AddWithValue("@pid", ProductId);
                cmd.Parameters.AddWithValue("@isstock", IsStock);
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
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

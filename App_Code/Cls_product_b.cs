using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_product_b
    {

        #region Constructor
        public Cls_product_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable SelectAllProductUsingCategoryIdAdmin(Int64 CategoryId)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.SelectAllProductUsingCategoryIdAdmin(CategoryId);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable SelectAllProductUsingCategoryId(Int64 CategoryId)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.SelectAllProductUsingCategoryId(CategoryId);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public DataTable Product_WSSelectAllProductUsingCategoryId(Int64 CategoryId)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.Product_WSSelectAllProductUsingCategoryId(CategoryId);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable Product_WSSelectAllProductUsingCategoryIdandBrandId(Int64 CategoryId, Int64 brandId)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.Product_WSSelectAllProductUsingCategoryIdandBrandId(CategoryId, brandId);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public DataTable SelectProductDetailsUsingProductId(Int64 pid)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.SelectProductDetailsUsingProductId(pid);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public DataTable SearchProductUsingProductName(string Productname)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                dt = objCls_product_db.SearchProductUsingProductName(Productname);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public product SelectById(Int64 pid)
        {
            product objproduct = new product();
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                objproduct = objCls_product_db.SelectById(pid);
                return objproduct;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objproduct;
            }
        }
        public Int64 Insert(product objproduct)
        {
            Int64 result = 0;
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                result = Convert.ToInt64(objCls_product_db.Insert(objproduct));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(product objproduct)
        {
            Int64 result = 0;
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                result = Convert.ToInt64(objCls_product_db.Update(objproduct));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 pid, Int64 cid)
        {
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                if (objCls_product_db.Delete(pid, cid))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Product_UpdatePrice(Int64 ProductId, decimal CustomerPrice, decimal DealerPrice, decimal DiscountPrice, decimal GST, Int32 StockQuantites, bool IsStock, bool IsActive, decimal Wholesaleprice, decimal SuperWholesaleprice, int StockAlertQuantites,bool isHotproduct)
        {
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                if (objCls_product_db.Product_UpdatePrice(ProductId, CustomerPrice, DealerPrice, DiscountPrice, GST, StockQuantites, IsStock, IsActive, Wholesaleprice, SuperWholesaleprice, StockAlertQuantites, isHotproduct))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return false;
            }
        }
        public bool Product_IsActive(Int64 ProductId, Boolean IsActive)
        {
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                if (objCls_product_db.Product_IsActive(ProductId, IsActive))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Product_IsStock(Int64 ProductId, Boolean IsStock)
        {
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                if (objCls_product_db.Product_IsStock(ProductId, IsStock))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Product_IsHotProduct(Int64 ProductId, Boolean IsActive)
        {
            try
            {
                Cls_product_db objCls_product_db = new Cls_product_db();
                if (objCls_product_db.Product_IsHotProduct(ProductId, IsActive))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
    public class product
    {
        public product()
        { }

        #region Private Variables
        private Int64 _pid;
        private Int64 _cid;
        private String _productname;
        private String _mainimage;
        private String _sku;
        private Decimal _customerprice;
        private Decimal _dealerprice;
        private Decimal _discountprice;
        private Decimal _gst;
        private Int32 _quantites;
        private Int32 _alertquantites;
        private Boolean _isstock;
        private String _shortdescp;
        private String _longdescp;
        private String _image;
        private Boolean _isactive;
        private Boolean _isdelete;
        private System.DateTime _createddate;
        private System.DateTime _modifieddate;
        private String _video1;
        private String _video2;
        private String _video3;
        private String _video4;
        private String _video_name_1;
        private String _video_name_2;
        private String _video_name_3;
        private String _video_name_4;
        private Decimal _wholesaleprice;
        private Decimal _superwholesaleprice;

         private String _HSNCode;
        private int _RealStock;
        private decimal _LandingPrice;

        private Boolean _isHotproduct;

        private Int64 _fk_colorId;
        private Int64 _fk_sizeId;
        private Int32 _packing;
        private Int64 _brandid;
        private Int64 _maincategoryid;
        private Boolean _gsttype;
        #endregion


        #region Public Properties
        public Int64 pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        public Int64 cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public Int64 brandid
        {
            get { return _brandid; }
            set { _brandid = value; }
        }
        public String productname
        {
            get { return _productname; }
            set { _productname = value; }
        }
        public String mainimage
        {
            get { return _mainimage; }
            set { _mainimage = value; }
        }
        public String sku
        {
            get { return _sku; }
            set { _sku = value; }
        }
        public Decimal customerprice
        {
            get { return _customerprice; }
            set { _customerprice = value; }
        }
        public Decimal dealerprice
        {
            get { return _dealerprice; }
            set { _dealerprice = value; }
        }
        public Decimal discountprice
        {
            get { return _discountprice; }
            set { _discountprice = value; }
        }
        public Decimal gst
        {
            get { return _gst; }
            set { _gst = value; }
        }
        public Int32 quantites
        {
            get { return _quantites; }
            set { _quantites = value; }
        }
        public Int32 alertquantites
        {
            get { return _alertquantites; }
            set { _alertquantites = value; }
        }
        public Boolean isstock
        {
            get { return _isstock; }
            set { _isstock = value; }
        }
        public String shortdescp
        {
            get { return _shortdescp; }
            set { _shortdescp = value; }
        }
        public String longdescp
        {
            get { return _longdescp; }
            set { _longdescp = value; }
        }
        public String image
        {
            get { return _image; }
            set { _image = value; }
        }
        public Boolean isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
        public System.DateTime createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }
        public System.DateTime modifieddate
        {
            get { return _modifieddate; }
            set { _modifieddate = value; }
        }
        public String video1
        {
            get { return _video1; }
            set { _video1 = value; }
        }
        public String video2
        {
            get { return _video2; }
            set { _video2 = value; }
        }
        public String video3
        {
            get { return _video3; }
            set { _video3 = value; }
        }
        public String video4
        {
            get { return _video4; }
            set { _video4 = value; }
        }
        public Decimal wholesaleprice
        {
            get { return _wholesaleprice; }
            set { _wholesaleprice = value; }
        }

        public Decimal superwholesaleprice
        {
            get { return _superwholesaleprice; }
            set { _superwholesaleprice = value; }
        }

        public String video_name_1
        {
            get { return _video_name_1; }
            set { _video_name_1 = value; }
        }
        public String video_name_2
        {
            get { return _video_name_2; }
            set { _video_name_2 = value; }
        }
        public String video_name_3
        {
            get { return _video_name_3; }
            set { _video_name_3 = value; }
        }
        public String video_name_4
        {
            get { return _video_name_4; }
            set { _video_name_4 = value; }
        }


          public String HSNCode
        {
            get { return _HSNCode; }
            set { _HSNCode = value; }
        }
        public int RealStock
        {
            get { return _RealStock; }
            set { _RealStock = value; }
        }
        public decimal  LandingPrice
        {
            get { return _LandingPrice; }
            set { _LandingPrice = value; }
        }


        public Boolean isHotproduct
        {
            get { return _isHotproduct; }
            set { _isHotproduct = value; }
        }

        public Int64 fk_colorId
        {
            get { return _fk_colorId; }
            set { _fk_colorId = value; }
        }
        public Int64  fk_sizeId
        {
            get { return _fk_sizeId; }
            set { _fk_sizeId = value; }
        }
        public Int32 packing
        {
            get { return _packing; }
            set { _packing = value; }
        }
        public Int64 maincategoryid
        {
            get { return _maincategoryid; }
            set { _maincategoryid = value; }
        }
        public Boolean gsttype
        {
            get { return _gsttype; }
            set { _gsttype = value; }



        }

       // gsttype
        #endregion
    }

}

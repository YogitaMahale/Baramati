using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_category_b
    {

        #region Constructor
        public Cls_category_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAllAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                dt = objCls_category_db.SelectAllAdmin();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                dt = objCls_category_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable category_WSSelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                dt = objCls_category_db.category_WSSelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public category SelectById(Int64 cid)
        {
            category objcategory = new category();
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                objcategory = objCls_category_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }
        public DataTable category_WSSelectById(Int64 cid)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                dt = objCls_category_db.category_WSSelectById(cid);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public Int64 Insert(category objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                result = Convert.ToInt64(objCls_category_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(category objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                result = Convert.ToInt64(objCls_category_db.Update(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 cid)
        {
            bool result = false;
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                if (objCls_category_db.Delete(cid))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                ErrHandler.writeError(ex.Message, ex.StackTrace);
            }
            return result;
        }
        public bool Category_IsActive(Int64 CategoryId, Boolean IsActive)
        {
            try
            {
                Cls_category_db objCls_category_db = new Cls_category_db();
                if (objCls_category_db.Category_IsActive(CategoryId, IsActive))
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
    public class category
    {
        public category()
        { }

        #region Private Variables
        private Int64 _cid;
        private String _categoryname;
        private String _imagename;
        private Decimal _actualprice;
        private Decimal _discountprice;
        private String _shortdesc;
        private String _longdescp;
        private Boolean _isdelete;
        private System.DateTime _createddate;
        private System.DateTime _modifieddate;
        private String _field1;
        private String _field2;
        private Int32 _bankid;
        private Int32 _maincategoryid;
        #endregion


        #region Public Properties
        public Int64 cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public Int32 bankid
        {
            get { return _bankid; }
            set { _bankid = value; }
        }
        public Int32 maincategoryid
        {
            get { return _maincategoryid; }
            set { _maincategoryid = value; }
        }
        public String categoryname
        {
            get { return _categoryname; }
            set { _categoryname = value; }
        }
        public String imagename
        {
            get { return _imagename; }
            set { _imagename = value; }
        }
        public Decimal actualprice
        {
            get { return _actualprice; }
            set { _actualprice = value; }
        }
        public Decimal discountprice
        {
            get { return _discountprice; }
            set { _discountprice = value; }
        }
        public String shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; }
        }
        public String longdescp
        {
            get { return _longdescp; }
            set { _longdescp = value; }
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
        public String field1
        {
            get { return _field1; }
            set { _field1 = value; }
        }
        public String field2
        {
            get { return _field2; }
            set { _field2 = value; }
        }
        #endregion
    }

}

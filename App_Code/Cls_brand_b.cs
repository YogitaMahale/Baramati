using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_brand_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_brand_b
    {
        public Cls_brand_b()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Public Methods

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_brand_db objCls_brand_db = new Cls_brand_db();
                dt = objCls_brand_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public brandMaster SelectById(Int64 cid)
        {
            brandMaster objcategory = new brandMaster();
            try
            {
                Cls_brand_db objCls_brand_db = new Cls_brand_db();
                objcategory = objCls_brand_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }

        public Int64 Insert(brandMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_brand_db objCls_brand_db = new Cls_brand_db();
                result = Convert.ToInt64(objCls_brand_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(brandMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_brand_db objCls_brand_db = new Cls_brand_db();
                result = Convert.ToInt64(objCls_brand_db.Update(objcategory));
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
                Cls_brand_db objCls_brand_db = new Cls_brand_db();
                if (objCls_brand_db.Delete(cid))
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

        #endregion


    }
    public class brandMaster
    {
        public brandMaster()
        { }

        #region Private Variables
        private Int64 _bid;
        private String _brandName;
        

        #endregion


        #region Public Properties
        public Int64 bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public String brandName
        {
            get { return _brandName; }
            set { _brandName = value; }
        }

        #endregion
    }

}
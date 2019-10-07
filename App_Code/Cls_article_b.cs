using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_article_b
{
    public Cls_article_b()
    {
        //
        // TODO: Add constructor logic here
        //
    }

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_article_db objCls_article_db = new Cls_article_db();
                dt = objCls_article_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }


        //public VendorMaster SelectById(Int64 id)
        //{
        //    VendorMaster objVendorMaster = new VendorMaster();
        //    try
        //    {
        //        Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

        //        objVendorMaster = objCls_VendorMaster_db.SelectById(id);
        //        return objVendorMaster;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return objVendorMaster;
        //    }
        //}

        //public Int64 Insert(VendorMaster objVendorMaster)
        //{
        //    Int64 result = 0;
        //    try
        //    {
        //        Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

        //        result = Convert.ToInt64(objCls_VendorMaster_db.Insert(objVendorMaster));
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return result;
        //    }
        //}

        //public Int64 Update(VendorMaster objVendorMaster)
        //{
        //    Int64 result = 0;
        //    try
        //    {
        //        Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

        //        result = Convert.ToInt64(objCls_VendorMaster_db.Update(objVendorMaster));
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return result;
        //    }
        //}

        public bool Delete(Int32 id)
        {
            try
            {
                Cls_article_db objCls_article_db = new Cls_article_db();

                if (objCls_article_db.Delete(id))
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



    }
    public class Article
    {
        public Article()
        { }
        //vid, vendorName, Address1, Address2, MobileNo1, MobileNo2, email, landline, fk_countryId, fk_stateId, fk_cityId, createddate, isdelete, isactive
        #region Private Variables
        private Int64 _id;
        private Int64 _productid;
        private String _particulars;
        private decimal  _wages;
        private Int64 _quantity;
        
        
        #endregion
       

        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 productid
        {
            get { return _productid; }
            set { _productid = value; }
        }
        public String particulars
        {
            get { return _particulars; }
            set { _particulars = value; }
        }
        public decimal  wages
        {
            get { return _wages; }
            set { _wages = value; }
        }
        public Int64 quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
         

        #endregion
    }

}
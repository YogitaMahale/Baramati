using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_maincategory_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_maincategory_b
    {

        #region Constructor
        public Cls_maincategory_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAllAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                dt = objCls_maincategory_db.SelectAllAdmin();
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
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                dt = objCls_maincategory_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        //public DataTable category_WSSelectAll()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
        //        dt = objCls_maincategory_db.category_WSSelectAll();
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return dt;
        //    }
        //}
        public maincategory SelectById(Int64 cid)
        {
            maincategory objcategory = new maincategory();
            try
            {
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                objcategory = objCls_maincategory_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }
        //public DataTable category_WSSelectById(Int64 cid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
        //        dt = objCls_maincategory_db.category_WSSelectById(cid);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrHandler.writeError(ex.Message, ex.StackTrace);
        //        return dt;
        //    }
        //}
        public Int64 Insert(maincategory objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                result = Convert.ToInt64(objCls_maincategory_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(maincategory objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                result = Convert.ToInt64(objCls_maincategory_db.Update(objcategory));
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
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                if (objCls_maincategory_db.Delete(cid))
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
                Cls_maincategory_db objCls_maincategory_db = new Cls_maincategory_db();
                if (objCls_maincategory_db.Category_IsActive(CategoryId, IsActive))
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
    public class maincategory
    {
        public maincategory()
        { }

        #region Private Variables
        private Int64 _id;
        private String _name;
        private String _imagename;
        private Boolean _isdelete;
        
        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String imagename
        {
            get { return _imagename; }
            set { _imagename = value; }
        }

        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }

        #endregion
    }

}

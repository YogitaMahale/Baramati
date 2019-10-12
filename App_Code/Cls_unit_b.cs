using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_unit_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_unit_b
    {
        public Cls_unit_b()
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
                Cls_unit_db objCls_unit_db = new Cls_unit_db();
                dt = objCls_unit_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public unitMaster SelectById(Int64 cid)
        {
            unitMaster objcategory = new unitMaster();
            try
            {
                Cls_unit_db objCls_unit_db = new Cls_unit_db();
                objcategory = objCls_unit_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }

        public Int64 Insert(unitMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_unit_db objCls_unit_db = new Cls_unit_db();
                result = Convert.ToInt64(objCls_unit_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(unitMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_unit_db objCls_unit_db = new Cls_unit_db();
                result = Convert.ToInt64(objCls_unit_db.Update(objcategory));
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
                Cls_unit_db objCls_unit_db = new Cls_unit_db();
                if (objCls_unit_db.Delete(cid))
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
    public class unitMaster
    {
        public unitMaster()
        { }

        #region Private Variables
        private Int64 _id;
        private String _unitname;
        private Boolean _isdeleted;



        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String unitname
        {
            get { return _unitname; }
            set { _unitname = value; }
        }
        public Boolean isdeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        
        #endregion
    }

}
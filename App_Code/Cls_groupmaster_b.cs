using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_groupmaster_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_groupmaster_b
    {

        #region Constructor
        public Cls_groupmaster_b()
        { }
        #endregion

        #region Public Methods

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();
                dt = objCls_groupmaster_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public DataTable SelectAllAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();
                dt = objCls_groupmaster_db.SelectAll_Admin();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public groupmaster SelectById(Int64 id)
        {
            groupmaster objgroupmaster = new groupmaster();
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();

                objgroupmaster = objCls_groupmaster_db.SelectById(id);
                return objgroupmaster;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objgroupmaster;
            }
        }

        public Int64 Insert(groupmaster objgroupmaster)
        {
            Int64 result = 0;
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();

                result = Convert.ToInt64(objCls_groupmaster_db.Insert(objgroupmaster));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }

        public Int64 Update(groupmaster objgroupmaster)
        {
            Int64 result = 0;
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();

                result = Convert.ToInt64(objCls_groupmaster_db.Update(objgroupmaster));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }

        public bool Delete(Int32 bankid)
        {
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();

                if (objCls_groupmaster_db.Delete(bankid))
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

        public bool IsActive(Int32 bankid, bool isActive)
        {
            try
            {
                Cls_groupmaster_db objCls_groupmaster_db = new Cls_groupmaster_db();
                if (objCls_groupmaster_db.IsActive(bankid, isActive))
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
    public class groupmaster
    {
        public groupmaster()
        { }

        #region Private Variables
        private Int32 _id;
        private String _groupname;
        private Boolean _isactive;
        private Boolean _isdelete;
        #endregion


        #region Public Properties
        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String groupname
        {
            get { return _groupname; }
            set { _groupname = value; }
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
        #endregion
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_size_b
    {
        public Cls_size_b()
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
                Cls_size_db objCls_size_db = new Cls_size_db();
                dt = objCls_size_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public sizeMaster  SelectById(Int64 cid)
        {
            sizeMaster objcategory = new sizeMaster();
            try
            {
                Cls_size_db objCls_size_db = new Cls_size_db();
                objcategory = objCls_size_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }

        public Int64 Insert(sizeMaster  objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_size_db objCls_size_db = new Cls_size_db();
                result = Convert.ToInt64(objCls_size_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(sizeMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_size_db objCls_size_db = new Cls_size_db();
                result = Convert.ToInt64(objCls_size_db.Update(objcategory));
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
                Cls_size_db objCls_size_db = new Cls_size_db();
                if (objCls_size_db.Delete(cid))
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
    public class sizeMaster
    {
        public sizeMaster()
        { }

        #region Private Variables
        private Int64 _cid;
        private String _sizeName;
        private String _imagename;

        private String _shortdesc;
        private String _longdescp;
        private Boolean _isdelete;

        private String _field1;
        private String _field2;
        private Int64 _groupid;


        #endregion


        #region Public Properties
        public Int64 cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public Int64 groupid
        {
            get { return _groupid; }
            set { _groupid = value; }
        }

        public String sizeName
        {
            get { return _sizeName; }
            set { _sizeName = value; }
        }
        public String imagename
        {
            get { return _imagename; }
            set { _imagename = value; }
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
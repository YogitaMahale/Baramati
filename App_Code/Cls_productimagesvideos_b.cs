using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_productimagesvideos_b
    {

        #region Constructor
        public Cls_productimagesvideos_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll(productimagesvideos objproductimagesvideos)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_productimagesvideos_db objCls_productimagesvideos_db = new Cls_productimagesvideos_db();

                dt = objCls_productimagesvideos_db.SelectAll(objproductimagesvideos);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public productimagesvideos SelectById(Int64 piid)
        {
            productimagesvideos objproductimagesvideos = new productimagesvideos();
            try
            {
                Cls_productimagesvideos_db objCls_productimagesvideos_db = new Cls_productimagesvideos_db();

                objproductimagesvideos = objCls_productimagesvideos_db.SelectById(piid);
                return objproductimagesvideos;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objproductimagesvideos;
            }
        }
        public Int64 Insert(productimagesvideos objproductimagesvideos)
        {
            Int64 result = 0;
            try
            {
                Cls_productimagesvideos_db objCls_productimagesvideos_db = new Cls_productimagesvideos_db();

                result = Convert.ToInt64(objCls_productimagesvideos_db.Insert(objproductimagesvideos));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(productimagesvideos objproductimagesvideos)
        {
            Int64 result = 0;
            try
            {
                Cls_productimagesvideos_db objCls_productimagesvideos_db = new Cls_productimagesvideos_db();

                result = Convert.ToInt64(objCls_productimagesvideos_db.Update(objproductimagesvideos));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 piid)
        {
            try
            {
                Cls_productimagesvideos_db objCls_productimagesvideos_db = new Cls_productimagesvideos_db();

                if (objCls_productimagesvideos_db.Delete(piid))
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
    public class productimagesvideos
    {
        public productimagesvideos()
        { }

        #region Private Variables
        private Int64 _piid;
        private Int64 _pid;
        private Int32 _type;
        private String _imagevideoname;
        private String _imagevideopath;
        private Boolean _isdelete;
        #endregion


        #region Public Properties
        public Int64 piid
        {
            get { return _piid; }
            set { _piid = value; }
        }
        public Int64 pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        public Int32 type
        {
            get { return _type; }
            set { _type = value; }
        }
        public String imagevideoname
        {
            get { return _imagevideoname; }
            set { _imagevideoname = value; }
        }
        public String imagevideopath
        {
            get { return _imagevideopath; }
            set { _imagevideopath = value; }
        }
        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
        #endregion
    }

}

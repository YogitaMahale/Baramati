using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_offers_b
    {

        #region Constructor
        public Cls_offers_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                dt = objCls_offers_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public DataTable offers_WSSelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                dt = objCls_offers_db.offers_WSSelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public offers SelectById(Int64 offerid)
        {
            offers objoffers = new offers();
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                objoffers = objCls_offers_db.SelectById(offerid);
                return objoffers;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objoffers;
            }
        }
        public Int64 Insert(offers objoffers)
        {
            Int64 result = 0;
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                result = Convert.ToInt64(objCls_offers_db.Insert(objoffers));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(offers objoffers)
        {
            Int64 result = 0;
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                result = Convert.ToInt64(objCls_offers_db.Update(objoffers));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 offerid)
        {
            try
            {
                Cls_offers_db objCls_offers_db = new Cls_offers_db();
                if (objCls_offers_db.Delete(offerid))
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
    public class offers
    {
        public offers()
        { }

        #region Private Variables
        private Int64 _offerid;
        private String _title;
        private String _descp;
        private String _imagename;
        private String _validfrom;
        private String _validto;
        private Boolean _isdelete;
        #endregion


        #region Public Properties
        public Int64 offerid
        {
            get { return _offerid; }
            set { _offerid = value; }
        }
        public String title
        {
            get { return _title; }
            set { _title = value; }
        }
        public String descp
        {
            get { return _descp; }
            set { _descp = value; }
        }
        public String imagename
        {
            get { return _imagename; }
            set { _imagename = value; }
        }
        public String validfrom
        {
            get { return _validfrom; }
            set { _validfrom = value; }
        }
        public String validto
        {
            get { return _validto; }
            set { _validto = value; }
        }
        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
        #endregion
    }

}

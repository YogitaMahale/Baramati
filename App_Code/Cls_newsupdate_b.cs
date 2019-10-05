using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_newsupdate_b
    {

        #region Constructor
        public Cls_newsupdate_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();
                dt = objCls_newsupdate_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public DataTable newsupdate_WSSelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();
                dt = objCls_newsupdate_db.newsupdate_WSSelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public newsupdate SelectById(Int64 newsupdateid)
        {
            newsupdate objnewsupdate = new newsupdate();
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();

                objnewsupdate = objCls_newsupdate_db.SelectById(newsupdateid);
                return objnewsupdate;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objnewsupdate;
            }
        }
        public Int64 Insert(newsupdate objnewsupdate)
        {
            Int64 result = 0;
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();

                result = Convert.ToInt64(objCls_newsupdate_db.Insert(objnewsupdate));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(newsupdate objnewsupdate)
        {
            Int64 result = 0;
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();

                result = Convert.ToInt64(objCls_newsupdate_db.Update(objnewsupdate));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 newsupdateid)
        {
            try
            {
                Cls_newsupdate_db objCls_newsupdate_db = new Cls_newsupdate_db();

                if (objCls_newsupdate_db.Delete(newsupdateid))
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
    public class newsupdate
    {
        public newsupdate()
        { }

        #region Private Variables
        private Int64 _newsupdateid;
        private String _title;
        private String _imagename;
        private String _shortdescp;
        private String _longdescp;
        private String _newsdate;
        private Boolean _isdelete;
        private String _imagename2;
        private String _imagename3;
        private String _imagename4;
        private String _imagename5;
        #endregion


        #region Public Properties
        public Int64 newsupdateid
        {
            get { return _newsupdateid; }
            set { _newsupdateid = value; }
        }
        public String title
        {
            get { return _title; }
            set { _title = value; }
        }
        public String imagename
        {
            get { return _imagename; }
            set { _imagename = value; }
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
        public String newsdate
        {
            get { return _newsdate; }
            set { _newsdate = value; }
        }
        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
        public String imagename2
        {
            get { return _imagename2; }
            set { _imagename2 = value; }
        }
        public String imagename3
        {
            get { return _imagename3; }
            set { _imagename3 = value; }
        }
        public String imagename4
        {
            get { return _imagename4; }
            set { _imagename4 = value; }
        }
        public String imagename5
        {
            get { return _imagename5; }
            set { _imagename5 = value; }
        }
        #endregion
    }

}

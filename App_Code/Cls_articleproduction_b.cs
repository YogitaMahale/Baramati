using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_articleproduction_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_articleproduction_b
    {

        #region Constructor
        public Cls_articleproduction_b()
        { }
        #endregion

        #region Public Methods

        /*
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();
                dt = objCls_articleproduction_db.SelectAll();
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
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();
                dt = objCls_articleproduction_db.SelectAll_Admin();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public articleproduction SelectById(Int64 bankid)
        {
            articleproduction objarticleproduction = new articleproduction();
            try
            {
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();

                objarticleproduction = objCls_articleproduction_db.SelectById(bankid);
                return objarticleproduction;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objarticleproduction;
            }
        }

        */

        public Int64 Insert(articleproduction objarticleproduction)
        {
            Int64 result = 0;
            try
            {
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();

                result = Convert.ToInt64(objCls_articleproduction_db.Insert(objarticleproduction));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }


        /*
        public Int64 Update(articleproduction objarticleproduction)
        {
            Int64 result = 0;
            try
            {
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();

                result = Convert.ToInt64(objCls_articleproduction_db.Update(objarticleproduction));
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
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();

                if (objCls_articleproduction_db.Delete(bankid))
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
                Cls_articleproduction_db objCls_articleproduction_db = new Cls_articleproduction_db();
                if (objCls_articleproduction_db.IsActive(bankid, isActive))
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


        */
        #endregion


    }
    public class articleproduction
    {
        public articleproduction()
        { }
        //id, worksheetno, employeeid, totalpairs, vshape, silai, factorysecond, isdeleted, createddate
        #region Private Variables
        private Int64 _id;
        private Int64 _worksheetno;
        private Int64 _employeeid;
        private Decimal _totalpairs;
        private Decimal _vshape;
        private Decimal _silai;
        private Decimal _factorysecond;
        //private Boolean _isactive;
        private Boolean _isdeleted;
        //private Decimal _balance;
        private DateTime _createddate;
        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 worksheetno
        {
            get { return _worksheetno; }
            set { _worksheetno = value; }
        }
        public Int64 employeeid
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }


        public Decimal totalpairs
        {
            get { return _totalpairs; }
            set { _totalpairs = value; }
        }
        public Decimal vshape
        {
            get { return _vshape; }
            set { _vshape = value; }
        }
        public Decimal silai
        {
            get { return _silai; }
            set { _silai = value; }
        }
        public Decimal factorysecond
        {
            get { return _factorysecond; }
            set { _factorysecond = value; }
        }

        /*
        public Boolean isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        */

        public Boolean isdeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }

        public DateTime createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }
        #endregion
    }

}

using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_employeewages_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_employeewages_b
    {
        public Cls_employeewages_b()
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
                Cls_employeewages_db objCls_employeewages_db = new Cls_employeewages_db();
                dt = objCls_employeewages_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public employeewagesMaster SelectById(Int64 cid)
        {
            employeewagesMaster objcategory = new employeewagesMaster();
            try
            {
                Cls_employeewages_db objCls_employeewages_db = new Cls_employeewages_db();
                objcategory = objCls_employeewages_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }

        public Int64 Insert(employeewagesMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_employeewages_db objCls_employeewages_db = new Cls_employeewages_db();
                result = Convert.ToInt64(objCls_employeewages_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(employeewagesMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_employeewages_db objCls_employeewages_db = new Cls_employeewages_db();
                result = Convert.ToInt64(objCls_employeewages_db.Update(objcategory));
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
                Cls_employeewages_db objCls_employeewages_db = new Cls_employeewages_db();
                if (objCls_employeewages_db.Delete(cid))
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
    public class employeewagesMaster
    {

        //id, empid, firstcount, firstrate, secondrate, vshaperate, silairate, currentfirstpairate, currentsecondrate, currentvshaperate, currentsilairate, isdeleted, createddate

        public employeewagesMaster()
        { }

        #region Private Variables
        private Int64 _id;
        private Int64 _eid;
        private String _createddate;
        private String _employeename;
        private Int32 _firstcount;
        private Decimal _firstrate;
        private Decimal _secondrate;
        private Decimal _vshaperate;
        private Decimal _silairate;
        private Decimal _currentfirstrate;
        private Decimal _currentsecondrate;
        private Decimal _currentvshaperate;
        private Decimal _currentsilairate;


        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 eid
        {
            get { return _eid; }
            set { _eid = value; }
        }

        public String createddate
        {
            get { return _createddate; }
            set { _createddate = value; }


        }

        public String employeename
        {
            get { return _employeename; }
            set { _employeename = value; }
        }

        public Int32 firstcount
        {
            get { return _firstcount; }
            set { _firstcount = value; }
        }

        public Decimal firstrate {
            get { return _firstrate; }
            set { _firstrate = value; }
        }
        public Decimal secondrate
        {
            get { return _secondrate; }
            set { _secondrate = value; }
        }
        public Decimal vshaperate
        {
            get { return _vshaperate; }
            set { _vshaperate = value; }
        }
        public Decimal silairate
        {
            get { return _silairate; }
            set { _silairate = value; }
        }
        public Decimal currentfirstrate
        {
            get { return _currentfirstrate; }
            set { _currentfirstrate = value; }
        }
        public Decimal currentsecondrate
        {
            get { return _currentsecondrate; }
            set { _currentsecondrate = value; }
        }
        public Decimal currentvshaperate
        {
            get { return _currentvshaperate; }
            set { _currentvshaperate = value; }
        }
        public Decimal currentsilairate
        {
            get { return _currentsilairate; }
            set { _currentsilairate = value; }
        }




        #endregion
    }

}

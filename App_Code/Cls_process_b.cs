using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_process_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_process_b
    {
        public Cls_process_b()
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
                Cls_process_db objCls_process_db = new Cls_process_db();
                dt = objCls_process_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public ProcessMaster SelectById(Int64 cid)
        {
            ProcessMaster objcategory = new ProcessMaster();
            try
            {
                Cls_process_db objCls_process_db = new Cls_process_db();
                objcategory = objCls_process_db.SelectById(cid);
                return objcategory;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcategory;
            }
        }

        public Int64 Insert(ProcessMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_process_db objCls_process_db = new Cls_process_db();
                result = Convert.ToInt64(objCls_process_db.Insert(objcategory));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(ProcessMaster objcategory)
        {
            Int64 result = 0;
            try
            {
                Cls_process_db objCls_process_db = new Cls_process_db();
                result = Convert.ToInt64(objCls_process_db.Update(objcategory));
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
                Cls_process_db objCls_process_db = new Cls_process_db();
                if (objCls_process_db.Delete(cid))
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
    public class ProcessMaster
    {
        public ProcessMaster()
        { }

        #region Private Variables
        private Int64 _id;
        private String _processname;


        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String processname
        {
            get { return _processname; }
            set { _processname = value; }
        }

        #endregion
    }

}

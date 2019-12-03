using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_articlestock_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_articlestock_b
    {

        #region Constructor
        public Cls_articlestock_b()
        { }
        #endregion

        #region Public Methods

        /*
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();
                dt = objCls_articlestock_db.SelectAll();
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
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();
                dt = objCls_articlestock_db.SelectAll_Admin();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public articlestock SelectById(Int64 bankid)
        {
            articlestock objarticlestock = new articlestock();
            try
            {
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();

                objarticlestock = objCls_articlestock_db.SelectById(bankid);
                return objarticlestock;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objarticlestock;
            }
        }

        */

        public Int64 InsertUpdate(articlestock objarticlestock)
        {
            Int64 result = 0;
            try
            {
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();

                result = Convert.ToInt64(objCls_articlestock_db.InsertUpdate(objarticlestock));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }


        /*
        public Int64 Update(articlestock objarticlestock)
        {
            Int64 result = 0;
            try
            {
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();

                result = Convert.ToInt64(objCls_articlestock_db.Update(objarticlestock));
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
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();

                if (objCls_articlestock_db.Delete(bankid))
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
                Cls_articlestock_db objCls_articlestock_db = new Cls_articlestock_db();
                if (objCls_articlestock_db.IsActive(bankid, isActive))
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
    public class articlestock
    {
        public articlestock()
        { }

        //id, pid, sizeid, colorid, quantity

        #region Private Variables
        private Int64 _id;
        private Int64 _pid;
        private Int64 _sizeid;
        private Int64 _colorid;
        private Decimal _quantity;
        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        public Int64 sizeid
        {
            get { return _sizeid; }
            set { _sizeid = value; }
        }
        public Int64 colorid
        {
            get { return _colorid; }
            set { _colorid = value; }
        }


        public Decimal quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        
        #endregion
    }

}

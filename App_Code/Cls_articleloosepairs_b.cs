using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_articleloosepairs_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_articleloosepairs_b
    {

        #region Constructor
        public Cls_articleloosepairs_b()
        { }
        #endregion

        #region Public Methods

        /*
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();
                dt = objCls_articleloosepairs_db.SelectAll();
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
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();
                dt = objCls_articleloosepairs_db.SelectAll_Admin();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public articleloosepairs SelectById(Int64 bankid)
        {
            articleloosepairs objarticleloosepairs = new articleloosepairs();
            try
            {
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();

                objarticleloosepairs = objCls_articleloosepairs_db.SelectById(bankid);
                return objarticleloosepairs;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objarticleloosepairs;
            }
        }

        */

        public Int64 InsertUpdate(articleloosepairs objarticleloosepairs)
        {
            Int64 result = 0;
            try
            {
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();

                result = Convert.ToInt64(objCls_articleloosepairs_db.InsertUpdate(objarticleloosepairs));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }


        /*
        public Int64 Update(articleloosepairs objarticleloosepairs)
        {
            Int64 result = 0;
            try
            {
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();

                result = Convert.ToInt64(objCls_articleloosepairs_db.Update(objarticleloosepairs));
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
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();

                if (objCls_articleloosepairs_db.Delete(bankid))
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
                Cls_articleloosepairs_db objCls_articleloosepairs_db = new Cls_articleloosepairs_db();
                if (objCls_articleloosepairs_db.IsActive(bankid, isActive))
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
    public class articleloosepairs
    {
        public articleloosepairs()
        { }

        //id, pid, colorid, sizegroupid, quantity

        #region Private Variables
        private Int64 _id;
        private Int64 _pid;
        private Int64 _sizegroupid;
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
        public Int64 sizegroupid
        {
            get { return _sizegroupid; }
            set { _sizegroupid = value; }
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

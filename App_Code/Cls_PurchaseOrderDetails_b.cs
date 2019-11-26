using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_PurchaseOrderDetails_b
/// </summary>
/// 
namespace BusinessLayer
{
    public class Cls_PurchaseOrderDetails_b
    {
        public Cls_PurchaseOrderDetails_b()
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
                Cls_PurchaseOrderDetails_db objCls_orderproducts_db = new Cls_PurchaseOrderDetails_db();

                dt = objCls_orderproducts_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public PurchaseOrderDetails SelectById(Int64 opid)
        {
            PurchaseOrderDetails objorderproducts = new PurchaseOrderDetails();
            try
            {
                Cls_PurchaseOrderDetails_db objCls_orderproducts_db = new Cls_PurchaseOrderDetails_db();

                objorderproducts = objCls_orderproducts_db.SelectById(opid);
                return objorderproducts;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objorderproducts;
            }
        }
        public Int64 Insert(PurchaseOrderDetails objorderproducts)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderDetails_db objCls_orderproducts_db = new Cls_PurchaseOrderDetails_db();

                result = Convert.ToInt64(objCls_orderproducts_db.Insert(objorderproducts));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(PurchaseOrderDetails objorderproducts)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderDetails_db objCls_orderproducts_db = new Cls_PurchaseOrderDetails_db();

                result = Convert.ToInt64(objCls_orderproducts_db.Update(objorderproducts));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 opid)
        {
            try
            {
                Cls_PurchaseOrderDetails_db objCls_orderproducts_db = new Cls_PurchaseOrderDetails_db();

                if (objCls_orderproducts_db.Delete(opid))
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
    public class PurchaseOrderDetails
    {
        public PurchaseOrderDetails()
        { }

        #region Private Variables
        private Int64 _opid;
        private Int64 _oid;
        private Int64 _uid;
        private Int64 _pid;
        private Int64 _qty;
        private decimal _rate;
        private decimal _subtotal;
        private Decimal _discount;
        private decimal _scheme;
        private Decimal _frieghtamt;
        private Decimal _taxableamt;
        private Decimal _csgtper;
        private Decimal _sgstper;
        private Decimal _igstper;
        private Decimal _gstamt;
        private Decimal _total;
        private Decimal _netrate;



        public Int64 opid
        {
            get { return _opid; }
            set { _opid = value; }
        }
        public Int64 oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        public Int64 uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        public Int64 pid
        {
            get { return _pid; }
            set { _pid = value; }
        }


        public Int64 qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        public decimal rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        public decimal subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }



        public decimal discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        public decimal scheme
        {
            get { return _scheme; }
            set { _scheme = value; }
        }
        public decimal frieghtamt
        {
            get { return _frieghtamt; }
            set { _frieghtamt = value; }
        }





        public decimal taxableamt
        {
            get { return _taxableamt; }
            set { _taxableamt = value; }
        }


        public decimal csgtper
        {
            get { return _csgtper; }
            set { _csgtper = value; }
        }

        public decimal sgstper
        {
            get { return _sgstper; }
            set { _sgstper = value; }
        }
        public decimal igstper
        {
            get { return _igstper; }
            set { _igstper = value; }
        }
        public decimal gstamt
        {
            get { return _gstamt; }
            set { _gstamt = value; }
        }


        public decimal total
        {
            get { return _total; }
            set { _total = value; }
        }



        public decimal  netrate
        {
            get { return _netrate; }
            set { _netrate = value; }
        }
        #endregion
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class clsPurchaseOrderHeader_b
    {

        #region Constructor
        public clsPurchaseOrderHeader_b()
        { }
        #endregion

        #region Public Methods
        public DataSet SelectAll()
        {
            DataSet dt = new DataSet();
            try
            {
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();
                dt = objCls_PurchaseOrderHeader_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public PurchaseOrderHeader SelectById(Int64 oid)
        {
            PurchaseOrderHeader objorders = new PurchaseOrderHeader();
            try
            {
                Cls_PurchaseOrderHeader_db objCls_orders_db = new Cls_PurchaseOrderHeader_db();

                objorders = objCls_orders_db.SelectById(oid);
                return objorders;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objorders;
            }
        }
        public Int64 Insert(PurchaseOrderHeader objorders)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderHeader_db objCls_orders_db = new Cls_PurchaseOrderHeader_db();

                result = Convert.ToInt64(objCls_orders_db.Insert(objorders));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(PurchaseOrderHeader objorders)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderHeader_db objCls_orders_db = new Cls_PurchaseOrderHeader_db();

                result = Convert.ToInt64(objCls_orders_db.Update(objorders));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 oid)
        {
            try
            {
                Cls_PurchaseOrderHeader_db objCls_orders_db = new Cls_PurchaseOrderHeader_db();

                if (objCls_orders_db.Delete(oid))
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
    public class PurchaseOrderHeader
    {
        public PurchaseOrderHeader()
        { }

        #region Private Variables        
        private Int64 _oid;
        private Int64 _uid;
        private string _invoicetype;
        private string _invoiceno;
        private DateTime _orderdate;
        private string _accountYear;
        private decimal _subtotal;
        private decimal _discandScheme;
        private decimal _frieghtamount;
        private decimal _taxableamount;
        private decimal _CGSTamt;
        private decimal _SGSTamt;
        private decimal _IGSTamt;
        private decimal _totalAmt;
        private decimal _transportamt;
        private decimal _packingamt;
        private decimal _otheramt;
        private decimal _dicountamt;
        private decimal _grandtotal;
        private decimal _pendingAmt;

        private DateTime _stockdate;
        #endregion
        #region Public Properties
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


        public string invoicetype
        {
            get { return _invoicetype; }
            set { _invoicetype = value; }
        }
        public string invoiceno
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        
        public DateTime orderdate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }
        public string accountYear
        {
            get { return _accountYear; }
            set { _accountYear = value; }
        }

       


        public decimal subtotal
        {
            get { return _subtotal; }
            set { _subtotal = value; }
        }
        public decimal discandScheme
        {
            get { return _discandScheme; }
            set { _discandScheme = value; }
        }
        public decimal frieghtamount
        {
            get { return _frieghtamount; }
            set { _frieghtamount = value; }
        }
        public decimal taxableamount
        {
            get { return _taxableamount; }
            set { _taxableamount = value; }
        }

      

        public decimal CGSTamt
        {
            get { return _CGSTamt; }
            set { _CGSTamt = value; }
        }
        public decimal SGSTamt
        {
            get { return _SGSTamt; }
            set { _SGSTamt = value; }
        }
        public decimal IGSTamt
        {
            get { return _IGSTamt; }
            set { _IGSTamt = value; }
        }
        public decimal totalAmt
        {
            get { return _totalAmt; }
            set { _totalAmt = value; }
        }
        public decimal transportamt
        {
            get { return _transportamt; }
            set { _transportamt = value; }
        }
       

        public decimal packingamt
        {
            get { return _packingamt; }
            set { _packingamt = value; }
        }
        public decimal otheramt
        {
            get { return _otheramt; }
            set { _otheramt = value; }
        }
        public decimal dicountamt
        {
            get { return _dicountamt; }
            set { _dicountamt = value; }
        }
        public decimal grandtotal
        {
            get { return _grandtotal; }
            set { _grandtotal = value; }
        }
        public decimal pendingAmt
        {
            get { return _pendingAmt; }
            set { _pendingAmt = value; }
        }
        public DateTime stockdate
        {
            get { return _stockdate; }
            set { _stockdate = value; }
        }
        
        #endregion
    }

}
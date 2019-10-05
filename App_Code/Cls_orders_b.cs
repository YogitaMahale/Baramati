using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_orders_b
    {

        #region Constructor
        public Cls_orders_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_orders_db objCls_orders_db = new Cls_orders_db();
                dt = objCls_orders_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public orders SelectById(Int64 oid)
        {
            orders objorders = new orders();
            try
            {
                Cls_orders_db objCls_orders_db = new Cls_orders_db();

                objorders = objCls_orders_db.SelectById(oid);
                return objorders;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objorders;
            }
        }
        public Int64 Insert(orders objorders)
        {
            Int64 result = 0;
            try
            {
                Cls_orders_db objCls_orders_db = new Cls_orders_db();

                result = Convert.ToInt64(objCls_orders_db.Insert(objorders));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(orders objorders)
        {
            Int64 result = 0;
            try
            {
                Cls_orders_db objCls_orders_db = new Cls_orders_db();

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
                Cls_orders_db objCls_orders_db = new Cls_orders_db();

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
    public class orders
    {
        public orders()
        { }

        #region Private Variables
        private Int64 _oid;
        private Int64 _uid;
        private Int32 _productquantites;
        private Boolean _billpaidornot;
        private Decimal _amount;
        private Decimal _discount;
        private Decimal _tax;
        private Decimal _totalamount;
        private System.DateTime _orderdate;
        private Boolean _isdelete;
        private String _usertype;
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
        public Int32 productquantites
        {
            get { return _productquantites; }
            set { _productquantites = value; }
        }
        public Boolean billpaidornot
        {
            get { return _billpaidornot; }
            set { _billpaidornot = value; }
        }
        public Decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public Decimal discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        public Decimal tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        public Decimal totalamount
        {
            get { return _totalamount; }
            set { _totalamount = value; }
        }
        public System.DateTime orderdate
        {
            get { return _orderdate; }
            set { _orderdate = value; }
        }
        public Boolean isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
        public String usertype
        {
            get { return _usertype; }
            set { _usertype = value; }
        }
        #endregion
    }

}

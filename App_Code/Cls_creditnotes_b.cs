using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_creditnotes_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_creditnotes_b
    {

        #region Constructor
        public Cls_creditnotes_b()
        { }
        #endregion

        #region Public Methods

        /*
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_creditnotes_db objCls_creditnotes_db = new Cls_creditnotes_db();
                dt = objCls_creditnotes_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        
        public creditnotes SelectById(Int64 oid)
        {
            creditnotes objcreditnotes = new creditnotes();
            try
            {
                Cls_creditnotes_db objCls_creditnotes_db = new Cls_creditnotes_db();

                objcreditnotes = objCls_creditnotes_db.SelectById(oid);
                return objcreditnotes;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objcreditnotes;
            }
        }
        */
        public Int64 Insert(creditnotes objcreditnotes)
        {
            Int64 result = 0;
            try
            {
                Cls_creditnotes_db objCls_creditnotes_db = new Cls_creditnotes_db();

                result = Convert.ToInt64(objCls_creditnotes_db.Insert(objcreditnotes));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }

        /*
        public Int64 Update(creditnotes objcreditnotes)
        {
            Int64 result = 0;
            try
            {
                Cls_creditnotes_db objCls_creditnotes_db = new Cls_creditnotes_db();

                result = Convert.ToInt64(objCls_creditnotes_db.Update(objcreditnotes));
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
                Cls_creditnotes_db objCls_creditnotes_db = new Cls_creditnotes_db();

                if (objCls_creditnotes_db.Delete(oid))
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
    public class creditnotes
    {
        public creditnotes()
        { }
        
        //id, customerid, invoiceid, reason, disctypepercentage, amount, freightdiscount, createddate, isdeleted
        
        #region Private Variables
        private Int64 _id;
        private Int64 _customerid;
        private Int32 _invoiceid;
        private String _reason;
        private String _disctypepercentage;
        private Decimal _amount;
        private Decimal _freightdiscount;
        private System.DateTime _createddate;
        private Boolean _isdeleted;
        private Decimal _otheramount;
        

        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 customerid
        {
            get { return _customerid; }
            set { _customerid = value; }
        }
        public Int32 invoiceid
        {
            get { return _invoiceid; }
            set { _invoiceid = value; }
        }
        public String reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        public String disctypepercentage
        {
            get { return _disctypepercentage; }
            set { _disctypepercentage = value; }
        }
        public Decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public Decimal freightdiscount
        {
            get { return _freightdiscount; }
            set { _freightdiscount = value; }
        }

        public System.DateTime createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }
        public Boolean isdeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        public Decimal otheramount
        {
            get { return _otheramount; }
            set { _otheramount = value; }
        }

        
        #endregion
    }

}

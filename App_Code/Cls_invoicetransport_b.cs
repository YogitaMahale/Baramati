using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_invoicetransport_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_invoicetransport_b
    {
        public Cls_invoicetransport_b()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Public Methods

        /*

        public DataTable SelectAll(invoicetransport objinvoicetransport)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_invoicetransport_db objCls_invoicetransport_db = new Cls_invoicetransport_db();

                dt = objCls_invoicetransport_db.SelectAll(objinvoicetransport);
                return dt;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public invoicetransport SelectById(Int64 id)
        {
            invoicetransport objinvoicetransport = new invoicetransport();
            try
            {
                Cls_invoicetransport_db objCls_invoicetransport_db = new Cls_invoicetransport_db();

                objinvoicetransport = objCls_invoicetransport_db.SelectById(id);
                return objinvoicetransport;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objinvoicetransport;
            }
        }


        */

        
        public Int64 Insert(invoicetransport objinvoicetransport)
        {
            Int64 result = 0;
            try
            {
                Cls_invoicetransport_db objCls_invoicetransport_db = new Cls_invoicetransport_db();

                result = Convert.ToInt64(objCls_invoicetransport_db.Insert(objinvoicetransport));
                return result;
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }


        /*
        public Int64 Update(invoicetransport objinvoicetransport)
        {
            Int64 result = 0;
            try
            {
                Cls_invoicetransport_db objCls_invoicetransport_db = new Cls_invoicetransport_db();

                result = Convert.ToInt64(objCls_invoicetransport_db.Update(objinvoicetransport));
                return result;
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 id)
        {
            try
            {
                Cls_invoicetransport_db objCls_invoicetransport_db = new Cls_invoicetransport_db();

                if (objCls_invoicetransport_db.Delete(id))
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
    public partial class invoicetransport
    {
        public invoicetransport()
        { }

        // id, transporterid, customerid, lrno, lrdate, invoiceno, freightamount, total, createddate, isdeleted

        #region Private Variables
        private Int64 _id;
        private Int64 _transporterid;
        private Int64 _customerid;
        private String _lrno;
        private DateTime _lrdate;
        private String _invoiceno;
        private Decimal _freightamount;
        private Decimal _total;
        private DateTime _createddate;

        //private Boolean _isdeleted;
        //private Boolean _orderstatus;

        #endregion


        #region Public Properties

        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Int64 transporterid
        {
            get { return _transporterid; }
            set { _transporterid = value; }
        }
        public Int64 customerid
        {
            get { return _customerid; }
            set { _customerid = value; }
        }

        public String lrno
        {
            get { return _lrno; }
            set { _lrno = value; }
        }

        public DateTime lrdate
        {
            get { return _lrdate; }
            set { _lrdate = value; }
        }
        public String invoiceno
        {
            get { return _invoiceno; }
            set { _invoiceno = value; }
        }
        public Decimal freightamount
        {
            get { return _freightamount; }
            set { _freightamount = value; }
        }
        public Decimal total
        {
            get { return _total; }
            set { _total = value; }
        }

        public DateTime Createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }

        

        #endregion
    }
}
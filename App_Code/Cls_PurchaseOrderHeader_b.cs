using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_PurchaseOrderHeader_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_PurchaseOrderHeader_b
    {
        public Cls_PurchaseOrderHeader_b()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Public Methods
        public DataTable SelectAll(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();

                dt = objCls_PurchaseOrderHeader_db.SelectAll(objPurchaseOrderHeader);
                return dt;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public PurchaseOrderHeader SelectById(Int64 id)
        {
            PurchaseOrderHeader objPurchaseOrderHeader = new PurchaseOrderHeader();
            try
            {
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();

                objPurchaseOrderHeader = objCls_PurchaseOrderHeader_db.SelectById(id);
                return objPurchaseOrderHeader;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objPurchaseOrderHeader;
            }
        }
        public Int64 Insert(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();

                result = Convert.ToInt64(objCls_PurchaseOrderHeader_db.Insert(objPurchaseOrderHeader));
                return result;
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(PurchaseOrderHeader objPurchaseOrderHeader)
        {
            Int64 result = 0;
            try
            {
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();

                result = Convert.ToInt64(objCls_PurchaseOrderHeader_db.Update(objPurchaseOrderHeader));
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
                Cls_PurchaseOrderHeader_db objCls_PurchaseOrderHeader_db = new Cls_PurchaseOrderHeader_db();

                if (objCls_PurchaseOrderHeader_db.Delete(id))
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
    public partial class PurchaseOrderHeader
    {
        public PurchaseOrderHeader()
        { }

        //PurchaseOrderId, VendorId, isdeleted

        #region Private Variables
        private Int64 _PurchaseOrderId;
        private String _PONo;
        private Int64 _VendorId;
        private Boolean _isdeleted;
        private DateTime _OrderDate;
        private Boolean _orderstatus;

        #endregion


        #region Public Properties

        public Int64 PurchaseOrderId
        {
            get { return _PurchaseOrderId; }
            set { _PurchaseOrderId = value; }
        }

        public String PONo
        {
            get { return _PONo; }
            set { _PONo = value; }
        }

        public Int64 VendorId
        {
            get { return _VendorId; }
            set { _VendorId = value; }
        }

        public Boolean isdeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        public Boolean orderstatus
        {
            get { return _orderstatus; }
            set { _orderstatus = value; }
        }

        #endregion
    }
}
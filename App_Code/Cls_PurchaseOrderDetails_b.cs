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
    /*
    public DataTable SelectAll(PurchaseOrderDetails objPurchaseOrderDetails)
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_PurchaseOrderDetails_db objCls_PurchaseOrderDetails_db = new Cls_PurchaseOrderDetails_db();

            dt = objCls_PurchaseOrderDetails_db.SelectAll(objPurchaseOrderDetails);
            return dt;
        }
        catch (Exception ex)
        {
            //ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public PurchaseOrderDetails SelectById(Int64 id)
    {
        PurchaseOrderDetails objPurchaseOrderDetails = new PurchaseOrderDetails();
        try
        {
            Cls_PurchaseOrderDetails_db objCls_PurchaseOrderDetails_db = new Cls_PurchaseOrderDetails_db();

            objPurchaseOrderDetails = objCls_PurchaseOrderDetails_db.SelectById(id);
            return objPurchaseOrderDetails;
        }
        catch (Exception ex)
        {
            //ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objPurchaseOrderDetails;
        }
    }
     * /
     * */
    public Int64 Insert(PurchaseOrderDetails objPurchaseOrderDetails)
    {
        Int64 result = 0;
        try
        {
            Cls_PurchaseOrderDetails_db objCls_PurchaseOrderDetails_db = new Cls_PurchaseOrderDetails_db();

            result = Convert.ToInt64(objCls_PurchaseOrderDetails_db.Insert(objPurchaseOrderDetails));
            return result;
        }
        catch (Exception ex)
        {
            ////ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    /*
    public Int64 Update(PurchaseOrderDetails objPurchaseOrderDetails)
    {
        Int64 result = 0;
        try
        {
            Cls_PurchaseOrderDetails_db objCls_PurchaseOrderDetails_db = new Cls_PurchaseOrderDetails_db();

            result = Convert.ToInt64(objCls_PurchaseOrderDetails_db.Update(objPurchaseOrderDetails));
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
            Cls_PurchaseOrderDetails_db objCls_PurchaseOrderDetails_db = new Cls_PurchaseOrderDetails_db();

            if (objCls_PurchaseOrderDetails_db.Delete(id))
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
     * 
     * */
    #endregion

}
 public partial class PurchaseOrderDetails
    {
        public PurchaseOrderDetails()
        { }

        //PurchaseOrderDetailsId, PurchaseOrderId, ProdId, CategoryId, Quantity, isdeleted, Quantity1

        #region Private Variables
        private Int64 _PurchaseOrderDetailsId;
        private Int64 _PurchaseOrderId;
        private Int64 _ProdId;
        private Int64 _CategoryId;
        private Int64 _Quantity;
        private Int64 _Quantity1;
        private Boolean _isdeleted;

        #endregion


        #region Public Properties

        public Int64 PurchaseOrderDetailsId
        {
            get { return _PurchaseOrderDetailsId; }
            set { _PurchaseOrderDetailsId = value; }
        }
        public Int64 PurchaseOrderId
        {
            get { return _PurchaseOrderId; }
            set { _PurchaseOrderId = value; }
        }
        public Int64 ProdId
        {
            get { return _ProdId; }
            set { _ProdId = value; }
        }

        public Int64 CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        public Boolean isdeleted
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        public Int64 Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public Int64 Quantity1
        {
            get { return _Quantity1; }
            set { _Quantity1 = value; }
        }
        

        #endregion
    }
}
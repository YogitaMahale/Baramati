using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_Customer_order_b
{
	public Cls_Customer_order_b()
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
            Cls_Customer_order_db objCls_orders_db = new Cls_Customer_order_db();
            dt = objCls_orders_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public Customer_orders SelectById(Int64 oid)
    {
        Customer_orders objorders = new Customer_orders();
        try
        {
            Cls_Customer_order_db objCls_orders_db = new Cls_Customer_order_db();

            objorders = objCls_orders_db.SelectById(oid);
            return objorders;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objorders;
        }
    }
    public Int64 Insert(Customer_orders objorders)
    {
        Int64 result = 0;
        try
        {
            Cls_Customer_order_db objCls_orders_db = new Cls_Customer_order_db();

            result = Convert.ToInt64(objCls_orders_db.Insert(objorders));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(Customer_orders objorders)
    {
        Int64 result = 0;
        try
        {
            Cls_Customer_order_db objCls_orders_db = new Cls_Customer_order_db();

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
            Cls_Customer_order_db objCls_orders_db = new Cls_Customer_order_db();

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
public class Customer_orders
{
    public Customer_orders()
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
    private Boolean _isCreateInvoice;
      private String _orderno;
      private String _ordertype;


   
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

    public Boolean isCreateInvoice
    {
        get { return _isCreateInvoice; }
        set { _isCreateInvoice = value; }
    }
    public string orderno
    {
        get{return _orderno;}
        set {_orderno=value;}
    }
    public string ordertype
    {
        get { return _ordertype; }
        set { _ordertype = value; }
    }    
    
    #endregion
}

}

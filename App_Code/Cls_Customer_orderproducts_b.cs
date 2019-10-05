using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_Customer_orderproducts_b
{
	public Cls_Customer_orderproducts_b()
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
            Cls_Customer_orderproducts_db objCls_orderproducts_db = new Cls_Customer_orderproducts_db();

            dt = objCls_orderproducts_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public Customer_orderproducts SelectById(Int64 opid)
    {
        Customer_orderproducts objorderproducts = new Customer_orderproducts();
        try
        {
            Cls_Customer_orderproducts_db objCls_orderproducts_db = new Cls_Customer_orderproducts_db();

            objorderproducts = objCls_orderproducts_db.SelectById(opid);
            return objorderproducts;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objorderproducts;
        }
    }
    public Int64 Insert(Customer_orderproducts objorderproducts)
    {
        Int64 result = 0;
        try
        {
            Cls_Customer_orderproducts_db objCls_orderproducts_db = new Cls_Customer_orderproducts_db();

            result = Convert.ToInt64(objCls_orderproducts_db.Insert(objorderproducts));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(Customer_orderproducts objorderproducts)
    {
        Int64 result = 0;
        try
        {
            Cls_Customer_orderproducts_db objCls_orderproducts_db = new Cls_Customer_orderproducts_db();

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
            Cls_Customer_orderproducts_db objCls_orderproducts_db = new Cls_Customer_orderproducts_db();

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
public class Customer_orderproducts
{
    public Customer_orderproducts()
    { }

    #region Private Variables
    private Int64 _opid;
    private Int64 _oid;
    private Int64 _uid;
    private Int64 _pid;
    private Decimal _productprice;
    private Decimal _discount;
    private Decimal _gst;
    private Decimal _productafterdiscountprice;
    private Int32 _quantites;
    private Decimal _producttotalprice;
    private Boolean _isdelete;
    #endregion


    #region Public Properties
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
    public Decimal productprice
    {
        get { return _productprice; }
        set { _productprice = value; }
    }
    public Decimal gst
    {
        get { return _gst; }
        set { _gst = value; }
    }
    public Decimal discount
    {
        get { return _discount; }
        set { _discount = value; }
    }
    public Decimal productafterdiscountprice
    {
        get { return _productafterdiscountprice; }
        set { _productafterdiscountprice = value; }
    }
    public Int32 quantites
    {
        get { return _quantites; }
        set { _quantites = value; }
    }
    public Decimal producttotalprice
    {
        get { return _producttotalprice; }
        set { _producttotalprice = value; }
    }
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
    #endregion
}

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_OrderStatusmaster_b
{
	public Cls_OrderStatusmaster_b()
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
            Cls_OrderStatusmaster_db objCls_OrderStatusmaster_db = new Cls_OrderStatusmaster_db();
            dt = objCls_OrderStatusmaster_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }

    //public DataTable SelectAllAdmin()
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        Cls_bankmaster_db objCls_bankmaster_db = new Cls_bankmaster_db();
    //        dt = objCls_bankmaster_db.SelectAll_Admin();
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //        return dt;
    //    }
    //}

    public OrderStatus SelectById(Int64 bankid)
    {
        OrderStatus objbankmaster = new OrderStatus();
        try
        {
            Cls_OrderStatusmaster_db objCls_OrderStatusmaster_db = new Cls_OrderStatusmaster_db();

            objbankmaster = objCls_OrderStatusmaster_db.SelectById(bankid);
            return objbankmaster;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objbankmaster;
        }
    }

    public Int64 Insert(OrderStatus objbankmaster)
    {
        Int64 result = 0;
        try
        {
            Cls_OrderStatusmaster_db objCls_OrderStatusmaster_db = new Cls_OrderStatusmaster_db();

            result = Convert.ToInt64(objCls_OrderStatusmaster_db.Insert(objbankmaster));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public Int64 Update(OrderStatus objOrderStatus)
    {
        Int64 result = 0;
        try
        {
            Cls_OrderStatusmaster_db objCls_OrderStatusmaster_db = new Cls_OrderStatusmaster_db();

            result = Convert.ToInt64(objCls_OrderStatusmaster_db.Update(objOrderStatus));
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
            Cls_OrderStatusmaster_db objCls_OrderStatusmaster_db = new Cls_OrderStatusmaster_db();

            if (objCls_OrderStatusmaster_db.Delete(bankid))
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

    //public bool IsActive(Int32 bankid, bool isActive)
    //{
    //    try
    //    {
    //        Cls_bankmaster_db objCls_bankmaster_db = new Cls_bankmaster_db();
    //        if (objCls_bankmaster_db.IsActive(bankid, isActive))
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    #endregion


}
public class OrderStatus
{
    public OrderStatus()
    { }

    #region Private Variables
    private Int32 _id;
    private String _type;
    private String _NotificationMsg;
   
    private Boolean _isdelete;

      
    #endregion


    #region Public Properties
    public Int32 id
    {
        get { return _id; }
        set { _id = value; }
    }
    public String type
    {
        get { return _type; }
        set { _type = value; }
    }
    public String NotificationMsg
    {
        get { return _NotificationMsg; }
        set { _NotificationMsg = value; }
    }
    
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
    #endregion
}

}

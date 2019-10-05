using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_ExpenseMaster_b
{
	public Cls_ExpenseMaster_b()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool Delete(Int64 id)
    {
        bool result = false;
        try
        {
            Cls_ExpenseMaster_db objCls_category_db = new Cls_ExpenseMaster_db();
            if (objCls_category_db.Delete(id))
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        catch (Exception ex)
        {
            result = false;
            // ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        return result;
    }
    public Int64 Update(ExpenseMaster objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_ExpenseMaster_db objCls_category_db = new Cls_ExpenseMaster_db();
            result = Convert.ToInt64(objCls_category_db.Update(objcategory));
            return result;
        }
        catch (Exception ex)
        {
            // ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public ExpenseMaster SelectById(Int64 id)
    {
        ExpenseMaster objcategory = new ExpenseMaster();
        try
        {
            Cls_ExpenseMaster_db objCls_category_db = new Cls_ExpenseMaster_db();
            objcategory = objCls_category_db.SelectById(id);
            return objcategory;
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objcategory;
        }
    }
    public Int64 Insert(ExpenseMaster objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_ExpenseMaster_db objCls_category_db = new Cls_ExpenseMaster_db();
            result = Convert.ToInt64(objCls_category_db.Insert(objcategory));
            return result;
        }
        catch (Exception ex)
        {
            //ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public DataTable SelectAll()
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_ExpenseMaster_db objCls_category_db = new Cls_ExpenseMaster_db();
            dt = objCls_category_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
}
public class ExpenseMaster
{
    public ExpenseMaster()
    { }

    #region Private Variables
    private Int64 _eid;
    private String _Expensename;

    #endregion


    #region Public Properties
    public Int64 eid
    {
        get { return _eid; }
        set { _eid = value; }
    }
    public String Expensename
    {
        get { return _Expensename; }
        set { _Expensename = value; }
    }


    #endregion
}
}
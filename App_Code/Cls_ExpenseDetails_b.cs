using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_ExpenseDetails_b
{
	public Cls_ExpenseDetails_b()
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
            Cls_ExpenseDetails_db objCls_ExpenseDetails_db = new Cls_ExpenseDetails_db();
            if (objCls_ExpenseDetails_db.Delete(id))
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
    public Int64 Update(ExpenseDetails objExpenseDetails)
    {
        Int64 result = 0;
        try
        {
            Cls_ExpenseDetails_db objCls_ExpenseDetails_db = new Cls_ExpenseDetails_db();
            result = Convert.ToInt64(objCls_ExpenseDetails_db.Update(objExpenseDetails));
            return result;
        }
        catch (Exception ex)
        {
            // ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public ExpenseDetails  SelectById(Int64 id)
    {
        ExpenseDetails objcategory = new ExpenseDetails();
        try
        {
            Cls_ExpenseDetails_db objCls_ExpenseDetails_db = new Cls_ExpenseDetails_db();
            objcategory = objCls_ExpenseDetails_db.SelectById(id);
            return objcategory;
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objcategory;
        }
    }
    public Int64 Insert(ExpenseDetails  objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_ExpenseDetails_db objCls_ExpenseDetails_db = new Cls_ExpenseDetails_db();
            result = Convert.ToInt64(objCls_ExpenseDetails_db.Insert(objcategory));
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
            Cls_ExpenseDetails_db objCls_ExpenseDetails_db = new Cls_ExpenseDetails_db();
            dt = objCls_ExpenseDetails_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
}
public class ExpenseDetails
{
    public ExpenseDetails()
    { }

    #region Private Variables
    private Int64 _id;  
    private Int64 _FK_ExpenseID;
    private decimal _amount;
    private String _Description;
    private Int64 _bankid;

    #endregion


    #region Public Properties
    public Int64 id
    {
        get { return _id; }
        set { _id = value; }
    }
    public Int64 bankid
    {
        get { return _bankid; }
        set { _bankid = value; }
    }

     public Int64 FK_ExpenseID
    {
        get { return _FK_ExpenseID; }
        set { _FK_ExpenseID = value; }
    }
    
     public decimal  amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

     public String Description
    {
        get { return _Description; }
        set { _Description = value; }
    }


    #endregion
}
}
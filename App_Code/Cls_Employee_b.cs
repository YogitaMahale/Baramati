using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_Employee_b
{
	public Cls_Employee_b()
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
            Cls_Employee_db objCls_Employee_db = new Cls_Employee_db();
            dt = objCls_Employee_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }


    public employeeMaster  SelectById(Int64 id)
    {
        employeeMaster objemployeeMaster = new employeeMaster();
        try
        {
            Cls_Employee_db objCls_Employee_db = new Cls_Employee_db();

            objemployeeMaster = objCls_Employee_db.SelectById(id);
            return objemployeeMaster;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objemployeeMaster;
        }
    }

    public Int64 Insert(employeeMaster objemployeeMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_Employee_db objCls_Employee_db = new Cls_Employee_db();

            result = Convert.ToInt64(objCls_Employee_db.Insert(objemployeeMaster));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public Int64 Update(employeeMaster objemployeeMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_Employee_db objCls_Employee_db = new Cls_Employee_db();

            result = Convert.ToInt64(objCls_Employee_db.Update(objemployeeMaster));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public bool Delete(Int32 id)
    {
        try
        {
            Cls_Employee_db objCls_Employee_db = new Cls_Employee_db();

            if (objCls_Employee_db.Delete(id))
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
public class employeeMaster
{
    public employeeMaster()
    { }
    
    #region Private Variables
    private Int64 _id;
    private String _employeeName;   
    private String _Address1; 
    private String _MobileNo1;   
    private String _img;
    private String _email;
    private String _landline;    
    
    private DateTime _createddate;
    private Boolean _isdelete;
    private Boolean _isactive;
    #endregion
    

    #region Public Properties
    public Int64 id
    {
        get { return _id; }
        set { _id = value; }
    }    
    public String employeeName
    {
        get { return _employeeName; }
        set { _employeeName = value; }
    }
    public String Address1
    {
        get { return _Address1; }
        set { _Address1 = value; }
    }    
    public String MobileNo1
    {
        get { return _MobileNo1; }
        set { _MobileNo1 = value; }
    }   
    public String img
    {
        get { return _img; }
        set { _img = value; }
    }
    public String email
    {
        get { return _email; }
        set { _email = value; }
    }
    public String landline
    {
        get { return _landline; }
        set { _landline = value; }
    }  
   
    
    public DateTime createddate
    {
        get { return _createddate; }
        set { _createddate = value; }
    }
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
    public Boolean isactive
    {
        get { return _isactive; }
        set { _isactive = value; }
    }




    #endregion
}

}

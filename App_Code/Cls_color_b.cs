using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_color_b
{
	public Cls_color_b()
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
            Cls_color_db objCls_color_db = new Cls_color_db();
            dt = objCls_color_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    
    public ColorMaster  SelectById(Int64 cid)
    {
        ColorMaster  objcategory = new ColorMaster ();
        try
        {
            Cls_color_db objCls_color_db = new Cls_color_db();
            objcategory = objCls_color_db.SelectById(cid);
            return objcategory;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objcategory;
        }
    }
    
    public Int64 Insert(ColorMaster  objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_color_db objCls_color_db = new Cls_color_db();
            result = Convert.ToInt64(objCls_color_db.Insert(objcategory));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(ColorMaster  objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_color_db objCls_color_db = new Cls_color_db();
            result = Convert.ToInt64(objCls_color_db.Update(objcategory));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public bool Delete(Int64 cid)
    {
        bool result = false;
        try
        {
            Cls_color_db objCls_color_db = new Cls_color_db();
            if (objCls_color_db.Delete(cid))
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
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        return result;
    }
    
    #endregion


}
public class ColorMaster
{
    public ColorMaster()
    { }

    #region Private Variables
    private Int64 _cid;
    private String _colorname;
    private String _imagename;
    
    private String _shortdesc;
    private String _longdescp;
    private Boolean _isdelete;
     
    private String _field1;
    private String _field2;
  
     
    #endregion

    
    #region Public Properties
    public Int64 cid
    {
        get { return _cid; }
        set { _cid = value; }
    }
  
    public String colorname
    {
        get { return _colorname; }
        set { _colorname = value; }
    }
    public String imagename
    {
        get { return _imagename; }
        set { _imagename = value; }
    }
  
    public String shortdesc
    {
        get { return _shortdesc; }
        set { _shortdesc = value; }
    }
    public String longdescp
    {
        get { return _longdescp; }
        set { _longdescp = value; }
    }
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
   
    public String field1
    {
        get { return _field1; }
        set { _field1 = value; }
    }
    public String field2
    {
        get { return _field2; }
        set { _field2 = value; }
    }
    #endregion
}

}

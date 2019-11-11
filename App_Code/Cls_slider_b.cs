using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_slider_b
{
	public Cls_slider_b()
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
            Cls_slider_db objCls_category_db = new Cls_slider_db();
            dt = objCls_category_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    
    public slider  SelectById(Int64 cid)
    {
        slider  objcategory = new slider ();
        try
        {
            Cls_slider_db objCls_category_db = new Cls_slider_db();
            objcategory = objCls_category_db.SelectById(cid);
            return objcategory;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objcategory;
        }
    }
   
    public Int64 Insert(slider  objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_slider_db objCls_category_db = new Cls_slider_db();
            result = Convert.ToInt64(objCls_category_db.Insert(objcategory));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(slider  objcategory)
    {
        Int64 result = 0;
        try
        {
            Cls_slider_db objCls_category_db = new Cls_slider_db();
            result = Convert.ToInt64(objCls_category_db.Update(objcategory));
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
            Cls_slider_db objCls_category_db = new Cls_slider_db();
            if (objCls_category_db.Delete(cid))
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
public class slider
{
    public slider()
    { }

    #region Private Variables
    private Int64 _cid;
     
    private String _imagename;
   
    private Boolean _isdelete;
     
    #endregion


    #region Public Properties
    public Int64 cid
    {
        get { return _cid; }
        set { _cid = value; }
    }
    
    
    public String imagename
    {
        get { return _imagename; }
        set { _imagename = value; }
    }
    
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
    
    #endregion
}

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_Rawmaterial_b
{
	public Cls_Rawmaterial_b()
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
            Cls_Rawmaterial_db objCls_Rawmaterial_b = new Cls_Rawmaterial_db();
            dt = objCls_Rawmaterial_b.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    
    public rawMaterialMaster  SelectById(Int64 pid)
    {
        rawMaterialMaster objrawMaterialMaster = new rawMaterialMaster();
        try
        {
                Cls_Rawmaterial_db objCls_Rawmaterial_b = new Cls_Rawmaterial_db();
            objrawMaterialMaster = objCls_Rawmaterial_b.SelectById(pid);
            return objrawMaterialMaster;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objrawMaterialMaster;
        }
    }
    public Int64 Insert(rawMaterialMaster  objproduct)
    {
        Int64 result = 0;
        try
        {
                Cls_Rawmaterial_db objCls_Rawmaterial_b = new Cls_Rawmaterial_db();
            result = Convert.ToInt64(objCls_Rawmaterial_b.Insert(objproduct));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(rawMaterialMaster  objproduct)
    {
        Int64 result = 0;
        try
        {
                Cls_Rawmaterial_db objCls_Rawmaterial_b = new Cls_Rawmaterial_db();
            result = Convert.ToInt64(objCls_Rawmaterial_b.Update(objproduct));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public bool Delete(Int64 pid)
    {
        try
        {
                Cls_Rawmaterial_db objCls_Rawmaterial_b = new Cls_Rawmaterial_db();
            if (objCls_Rawmaterial_b.Delete(pid))
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
public class rawMaterialMaster
{
    public rawMaterialMaster()
    { }
  
    #region Private Variables
    private Int64 _id;   
    private String _productname;
    private String _mainimage;     
    private Decimal _price;    
    private Int32 _quantity;
    private Int32 _alertquantites;
    private Boolean _isstock;
    private String _shortdescp;
    private String _longdescp;     
    private Boolean _isactive;
    private Boolean _isdelete;
    private System.DateTime _createddate;
    private System.DateTime _modifieddate;    
    private String _hsncode;
    private String _gstno;
    private Int32 _unitid;
        #endregion
        #region Public Properties
        public Int64 id
    {
        get { return _id; }
        set { _id = value; }
    }    
    public String productname
    {
        get { return _productname; }
        set { _productname = value; }
    }
    public String hsncode
    {
        get { return _hsncode; }
        set { _hsncode = value; }
    }
    public String gstno
    {
        get { return _gstno; }
        set { _gstno = value; }
    }
    public String mainimage
    {
        get { return _mainimage; }
        set { _mainimage = value; }
    }
    public Decimal price
    {
        get { return _price; }
        set { _price = value; }
    }
    public Int32 quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
    public Int32 unitid
    {
        get { return _unitid; }
        set { _unitid = value; }
    }
        public Int32 alertquantites
    {
        get { return _alertquantites; }
        set { _alertquantites = value; }
    }
    public Boolean isstock
    {
        get { return _isstock; }
        set { _isstock = value; }
    }
    public String shortdescp
    {
        get { return _shortdescp; }
        set { _shortdescp = value; }
    }
    public String longdescp
    {
        get { return _longdescp; }
        set { _longdescp = value; }
    }    
    public Boolean isactive
    {
        get { return _isactive; }
        set { _isactive = value; }
    }
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }
    public System.DateTime createddate
    {
        get { return _createddate; }
        set { _createddate = value; }
    }
    public System.DateTime modifieddate
    {
        get { return _modifieddate; }
        set { _modifieddate = value; }
    }
    

    #endregion
}

}

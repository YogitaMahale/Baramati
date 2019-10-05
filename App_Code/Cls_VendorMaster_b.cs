using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_VendorMaster_b
{
	public Cls_VendorMaster_b()
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
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();
            dt = objCls_VendorMaster_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }


    public VendorMaster SelectById(Int64 id)
    {
        VendorMaster objVendorMaster = new VendorMaster();
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

            objVendorMaster = objCls_VendorMaster_db.SelectById(id);
            return objVendorMaster;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objVendorMaster;
        }
    }

    public Int64 Insert(VendorMaster objVendorMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

            result = Convert.ToInt64(objCls_VendorMaster_db.Insert(objVendorMaster));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public Int64 Update(VendorMaster objVendorMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

            result = Convert.ToInt64(objCls_VendorMaster_db.Update(objVendorMaster));
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
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();

            if (objCls_VendorMaster_db.Delete(id))
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

    public DataTable Country_SelectAll()
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();
            dt = objCls_VendorMaster_db.Country_SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public DataTable getState_byCountryId(Int64 CountryId)
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();
            dt = objCls_VendorMaster_db.getState_byCountryId(CountryId);
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public DataTable getCity_byStateId(Int64 stateId)
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_VendorMaster_db objCls_VendorMaster_db = new Cls_VendorMaster_db();
            dt = objCls_VendorMaster_db.getCity_byStateId(stateId);
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    #endregion


}
public class VendorMaster
{
    public VendorMaster()
    { }
    //vid, vendorName, Address1, Address2, MobileNo1, MobileNo2, email, landline, fk_countryId, fk_stateId, fk_cityId, createddate, isdelete, isactive
    #region Private Variables
    private Int64 _vid;
    private Int64 _fk_agentId;
    private String _vendorName;
    private String _Address1;
    private String _Address2;
    private String _MobileNo1;
    private String _MobileNo2;
    private String _img;
    private String _email;
    private String _landline;
    //private Int64 _fk_agentId;
    private String _fk_countryId;
    private String _fk_stateId;
    private String _fk_cityId;
    private String _country;
    private String _state;
    private String _city;

    private DateTime _createddate;
    private Boolean _isdelete;
    private Boolean _isactive;
    #endregion

    

    #region Public Properties
     public Int64 vid
    {
        get { return _vid; }
        set { _vid = value; }
    }
     public Int64 fk_agentId
     {
         get { return _fk_agentId; }
         set { _fk_agentId = value; }
     }
     public String vendorName
    {
        get { return _vendorName; }
        set { _vendorName = value; }
    }
     public String Address1
    {
        get { return _Address1; }
        set { _Address1 = value; }
    }
     public String Address2
     {
         get { return _Address2; }
         set { _Address2 = value; }
     }
     public String MobileNo1
    {
        get { return _MobileNo1; }
        set { _MobileNo1 = value; }
    }
     public String MobileNo2
     {
         get { return _MobileNo2; }
         set { _MobileNo2 = value; }
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

    //public Int64 fk_agentId
    //{
    //    get { return _fk_agentId; }
    //    set { _fk_agentId = value; }
    //}
    public String country
    {
        get { return _country; }
        set { _country = value; }
    }
    public String state
    {
        get { return _state; }
        set { _state = value; }
    }
    public String city
    {
        get { return _city; }
        set { _city = value; }
    }
    public String fk_countryId
    {
        get { return _fk_countryId; }
        set { _fk_countryId = value; }
    }
    public String fk_stateId
    {
        get { return _fk_stateId; }
        set { _fk_stateId = value; }
    }
    public String fk_cityId
    {
        get { return _fk_cityId; }
        set { _fk_cityId = value; }
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

using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_worksheetmaster_b
/// </summary>
 namespace BusinessLayer
{
    public class Cls_worksheetmaster_b
{
    public Cls_worksheetmaster_b()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Public Methods
    public DataTable SelectAll(worksheetmaster objworksheetmaster)
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_worksheetmaster_db objCls_worksheetmaster_db = new Cls_worksheetmaster_db();

            dt = objCls_worksheetmaster_db.SelectAll(objworksheetmaster);
            return dt;
        }
        catch (Exception ex)
        {
            //ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }
    public worksheetmaster SelectById(Int64 id)
    {
        worksheetmaster objworksheetmaster = new worksheetmaster();
        try
        {
            Cls_worksheetmaster_db objCls_worksheetmaster_db = new Cls_worksheetmaster_db();

            objworksheetmaster = objCls_worksheetmaster_db.SelectById(id);
            return objworksheetmaster;
        }
        catch (Exception ex)
        {
            //ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objworksheetmaster;
        }
    }
    public Int64 Insert(worksheetmaster objworksheetmaster)
    {
        Int64 result = 0;
        try
        {
            Cls_worksheetmaster_db objCls_worksheetmaster_db = new Cls_worksheetmaster_db();

            result = Convert.ToInt64(objCls_worksheetmaster_db.Insert(objworksheetmaster));
            return result;
        }
        catch (Exception ex)
        {
            ////ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public Int64 Update(worksheetmaster objworksheetmaster)
    {
        Int64 result = 0;
        try
        {
            Cls_worksheetmaster_db objCls_worksheetmaster_db = new Cls_worksheetmaster_db();

            result = Convert.ToInt64(objCls_worksheetmaster_db.Update(objworksheetmaster));
            return result;
        }
        catch (Exception ex)
        {
            ////ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
    public bool Delete(Int64 id)
    {
        try
        {
            Cls_worksheetmaster_db objCls_worksheetmaster_db = new Cls_worksheetmaster_db();

            if (objCls_worksheetmaster_db.Delete(id))
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
public partial class worksheetmaster
{
    public worksheetmaster()
    { }

    //PurchaseOrderId, VendorId, isdeleted

    //   [id] [bigint] IDENTITY(1,1) NOT NULL,

    //   [worksheetid] [bigint] NULL,
    //[particularsid] [bigint] NULL,
    //--[sizeid] [bigint] NULL,
    //--[colorid] [bigint] NULL,
    //[employeeid] [bigint] NULL,
    //[quantity] [bigint] NULL,
    //[workdate] [datetime] NULL,
    //--[createdby] [bigint] NULL,
    //--[createddate] [datetime] NULL,
    //--[isdeleted] [bit] NULL,
    //[remark]
    //   [nvarchar]
    //   (max) NULL
    //[id],[productid],[sizeid],[colorid],[createdby],[createddate],[isdeleted]
    
    #region Private Variables
    private Int64 _id;
    private Int64 _productid;
    private String _sizeid;
    private String _colorid;
    private Int64 _createdby;
    private DateTime _createddate;

    //private Boolean _isdeleted;
    //private Boolean _orderstatus;

    #endregion


    #region Public Properties

    public Int64 Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public Int64 Productid
    {
        get { return _productid; }
        set { _productid = value; }
    }

    public String Sizeid
    {
        get { return _sizeid; }
        set { _sizeid = value; }
    }
    public String Colorid
    {
        get { return _colorid; }
        set { _colorid = value; }
    }
    public Int64 Createdby
    {
        get { return _createdby; }
        set { _createdby = value; }
    }
    //public Boolean isdeleted
    //{
    //    get { return _isdeleted; }
    //    set { _isdeleted = value; }
    //}
    public DateTime Createddate
    {
        get { return _createddate; }
        set { _createddate = value; }
    }

    //public Boolean orderstatus
    //{
    //    get { return _orderstatus; }
    //    set { _orderstatus = value; }
    //}

    #endregion
}
}
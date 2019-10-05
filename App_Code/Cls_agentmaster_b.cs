using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
public class Cls_agentmaster_b
{
	public Cls_agentmaster_b()
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
            Cls_agentmaster_db objCls_agentmaster_db = new Cls_agentmaster_db();
            dt = objCls_agentmaster_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }

    
    public AgentMaster SelectById(Int64 id)
    {
        AgentMaster objAgentMaster = new AgentMaster();
        try
        {
            Cls_agentmaster_db objCls_agentmaster_db = new Cls_agentmaster_db();

            objAgentMaster = objCls_agentmaster_db.SelectById(id);
            return objAgentMaster;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return objAgentMaster;
        }
    }

    public Int64 Insert(AgentMaster objAgentMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_agentmaster_db objCls_agentmaster_db = new Cls_agentmaster_db();

            result = Convert.ToInt64(objCls_agentmaster_db.Insert(objAgentMaster));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }

    public Int64 Update(AgentMaster objAgentMaster)
    {
        Int64 result = 0;
        try
        {
            Cls_agentmaster_db objCls_agentmaster_db = new Cls_agentmaster_db();

            result = Convert.ToInt64(objCls_agentmaster_db.Update(objAgentMaster));
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
            Cls_agentmaster_db objCls_agentmaster_db = new Cls_agentmaster_db();

            if (objCls_agentmaster_db.Delete(bankid))
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
public class AgentMaster
{
    public AgentMaster()
    { }

    #region Private Variables
    private Int32 _aid;
    private String _Agentname;
    private String _Address;
    private String _MobileNo;
    private String _email;
    private String _img;
  private DateTime _createddate;
    private Boolean _isdelete;
    #endregion


    #region Public Properties
    public Int32 aid
    {
        get { return _aid; }
        set { _aid = value; }
    }
    public String Agentname
    {
        get { return _Agentname; }
        set { _Agentname = value; }
    }
    public String Address
    {
        get { return _Address; }
        set { _Address = value; }
    }
    public String MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }
    public String email
    {
        get { return _email; }
        set { _email = value; }
    }
    public String img
    {
        get { return _img; }
        set { _img = value; }
    }
    public DateTime  createddate
    {
        get { return _createddate; }
        set { _createddate = value; }
    }
    public Boolean isdelete
    {
        get { return _isdelete; }
        set { _isdelete = value; }
    }


     
    #endregion
}

}

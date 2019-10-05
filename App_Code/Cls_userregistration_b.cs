using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
	public class Cls_userregistration_b
	{

	#region Constructor
	public Cls_userregistration_b()
	{}
	#endregion

	#region Public Methods

    public DataTable SelectAll()
    {
        DataTable dt = new DataTable();
        try
        {
            Cls_userregistration_db objCls_userregistration_db = new Cls_userregistration_db();
            dt = objCls_userregistration_db.SelectAll();
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return dt;
        }
    }

	

    public userregistration SelectById(Int64 uid)
	{
			userregistration objuserregistration = new userregistration();
		try
		{
			Cls_userregistration_db objCls_userregistration_db = new Cls_userregistration_db();
		
			objuserregistration = objCls_userregistration_db.SelectById(uid);
			return objuserregistration;
		}
		catch(Exception ex)
		{
			ErrHandler.writeError(ex.Message, ex.StackTrace);
			return objuserregistration;
		}
	}
	public Int64 Insert(userregistration objuserregistration)
	{
	Int64 result = 0;
		try
		{
			Cls_userregistration_db objCls_userregistration_db = new Cls_userregistration_db ();
		
		result =Convert.ToInt64(objCls_userregistration_db.Insert(objuserregistration));
			return result;
		}
		catch(Exception ex)
		{
			ErrHandler.writeError(ex.Message, ex.StackTrace);
			return result;
		}
	}
	public Int64 Update(userregistration objuserregistration)
	{
	Int64 result = 0;
		try
		{
			Cls_userregistration_db objCls_userregistration_db = new Cls_userregistration_db ();
		
			result =Convert.ToInt64(objCls_userregistration_db.Update(objuserregistration));
			return result;
		}
		catch(Exception ex)
		{
			ErrHandler.writeError(ex.Message, ex.StackTrace);
			return result;
		}
	}

    public Int64 WebsiteUser_Status(string Username, string password, Boolean IsActive)
    {
        Int64 result = 0;
        try
        {
            Cls_userregistration_db objCls_userregistration_db = new Cls_userregistration_db();

            result = Convert.ToInt64(objCls_userregistration_db.WebsiteUser_Status(Username,password,IsActive));
            return result;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return result;
        }
    }
	#endregion

	
	}
	public class userregistration
	{
		public userregistration()
		{}

		#region Private Variables
		private Int64 _uid;
		private String _fname;
		private String _mname;
		private String _lname;
		private String _email;
		private String _phone;
		private String _password;
		private String _dob;
		private String _address1;
		private String _address2;
		private Boolean _isguest;
		private String _registrationdate;
		private String _uguid;
		private Boolean _isactive;
		private Boolean _isdelete;
		#endregion

		
		#region Public Properties
		public Int64 uid
		{ 
			get { return _uid; }
			set { _uid = value; }
		}
		public String fname
		{ 
			get { return _fname; }
			set { _fname = value; }
		}
		public String mname
		{ 
			get { return _mname; }
			set { _mname = value; }
		}
		public String lname
		{ 
			get { return _lname; }
			set { _lname = value; }
		}
		public String email
		{ 
			get { return _email; }
			set { _email = value; }
		}
		public String phone
		{ 
			get { return _phone; }
			set { _phone = value; }
		}
		public String password
		{ 
			get { return _password; }
			set { _password = value; }
		}
		public String dob
		{ 
			get { return _dob; }
			set { _dob = value; }
		}
		public String address1
		{ 
			get { return _address1; }
			set { _address1 = value; }
		}
		public String address2
		{ 
			get { return _address2; }
			set { _address2 = value; }
		}
		public Boolean isguest
		{ 
			get { return _isguest; }
			set { _isguest = value; }
		}
		public String registrationdate
		{ 
			get { return _registrationdate; }
			set { _registrationdate = value; }
		}
		public String uguid
		{ 
			get { return _uguid; }
			set { _uguid = value; }
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
			#endregion
	}

	}

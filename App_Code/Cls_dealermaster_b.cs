using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_dealermaster_b
    {
        public Cls_dealermaster_b()
        {

        }


        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_dealermaster_db objCls_dealermaster_db = new Cls_dealermaster_db();
                dt = objCls_dealermaster_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }

        public dealermaster SelectById(Int64 did)
        {
            dealermaster objdealermaster = (new Cls_dealermaster_db().SelectById(did));
            return objdealermaster;
        }
        public Int64 Insert(dealermaster objdealermaster)
        {
            Int64 result = (new Cls_dealermaster_db().Insert(objdealermaster));
            return result;
        }
        public Int64 Update(dealermaster objdealermaster)
        {
            Int64 result = (new Cls_dealermaster_db().Update(objdealermaster));
            return result;
        }
        public DataTable  SelectAllDetails_usingID(Int64 did)
        {
            DataTable dt = new DataTable();

            dt = (new Cls_dealermaster_db().SelectDealerDetails_usingId(did));
            return dt;
        }
    }

    public class dealermaster
    {
        public dealermaster()
        { }

        #region Private Variables
        private Int64 _did;
        private Int64 _agentid;
        private String _name;
        private String _userloginmobileno;
        private String _password;
        private String _whatappno;
        private String _email;
        private String _gstno;
        private String _aadharno;
        private String _panno;
        private String _address1;
        private String _address2;
        private String _city;
        private String _state;
        private String _guid;
        private Boolean _isactive;
        private Boolean _isdeleted;
        private System.DateTime _createddate;
        private String _Img;
        private Int64 _district;
        private Decimal _balance;
        #endregion


        #region Public Properties
        public Int64 did
        {
            get { return _did; }
            set { _did = value; }
        }
        public Decimal balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        public Int64 agentid
        {
            get { return _agentid; }
            set { _agentid = value; }
        }
        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String userloginmobileno
        {
            get { return _userloginmobileno; }
            set { _userloginmobileno = value; }
        }
        public String password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string whatappno
        {
            get { return _whatappno; }
            set { _whatappno = value; }
        }
        public String email
        {
            get { return _email; }
            set { _email = value; }
        }
        public String gstno
        {
            get { return _gstno; }
            set { _gstno = value; }
        }
        public String aadharno
        {
            get { return _aadharno; }
            set { _aadharno = value; }
        }
        public String panno
        {
            get { return _panno; }
            set { _panno = value; }
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
        public String city
        {
            get { return _city; }
            set { _city = value; }
        }
        public String state
        {
            get { return _state; }
            set { _state = value; }
        }
        public String guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public Boolean isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public Boolean isdelete
        {
            get { return _isdeleted; }
            set { _isdeleted = value; }
        }
        public System.DateTime createddate
        {
            get { return _createddate; }
            set { _createddate = value; }
        }
        public String Img
        {
            get { return _Img; }
            set { _Img = value; }
        }
        public Int64 district
        {
            get { return _district; }
            set { _district = value; }
        }
        #endregion
    }
}
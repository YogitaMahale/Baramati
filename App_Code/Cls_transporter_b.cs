using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_transporter_b
/// </summary>

namespace BusinessLayer
{
    public class Cls_transporter_b
    {
        public Cls_transporter_b()
        {

        }

        public transporter SelectById(Int64 did)
        {
            transporter objtransporter = (new Cls_transporter_db().SelectById(did));
            return objtransporter;
        }

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_transporter_db objCls_transporter_db = new Cls_transporter_db();
                dt = objCls_transporter_db.SelectAll();
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public Int64 Insert(transporter objtransporter)
        {
            Int64 result = (new Cls_transporter_db().Insert(objtransporter));
            return result;
        }
        public Int64 Update(transporter objtransporter)
        {
            Int64 result = (new Cls_transporter_db().Update(objtransporter));
            return result;
        }


        public bool Delete(Int32 id)
        {
            try
            {
                Cls_transporter_db objCls_transporter_db = new Cls_transporter_db();

                if (objCls_transporter_db.Delete(id))
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


        //public DataTable SelectAllDetails_usingID(Int64 did)
        //{
        //    DataTable dt = new DataTable();

        //    dt = (new Cls_transporter_db().SelectDealerDetails_usingId(did));
        //    return dt;
        //}
    }

    public class transporter
    {
        public transporter()
        { }


        #region Private Variables
        private Int64 _id;
        private String _name;
        private String _mobileno;
        private String _phoneno;
        private String _email;
        private String _gstno;
        private String _gsttype;
        private String _aadharno;
        private String _panno;
        private String _address;
        private String _remark;

        #endregion


        #region Public Properties
        public Int64 id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String mobileno
        {
            get { return _mobileno; }
            set { _mobileno = value; }
        }
        public String phoneno
        {
            get { return _phoneno; }
            set { _phoneno = value; }
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
        public String gsttype
        {
            get { return _gsttype; }
            set { _gsttype = value; }
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
        public String address
        {
            get { return _address; }
            set { _address = value; }
        }
        public String remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        #endregion
    }
}
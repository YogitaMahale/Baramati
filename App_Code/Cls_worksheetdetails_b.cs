using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_worksheetdetails_b
/// </summary>
namespace BusinessLayer
{
    public class Cls_worksheetdetails_b
    {
        public Cls_worksheetdetails_b()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Public Methods
        /*
        public DataTable SelectAll(worksheetdetails objworksheetdetails)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_worksheetdetails_db objCls_worksheetdetails_db = new Cls_worksheetdetails_db();

                dt = objCls_worksheetdetails_db.SelectAll(objworksheetdetails);
                return dt;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public worksheetdetails SelectById(Int64 id)
        {
            worksheetdetails objworksheetdetails = new worksheetdetails();
            try
            {
                Cls_worksheetdetails_db objCls_worksheetdetails_db = new Cls_worksheetdetails_db();

                objworksheetdetails = objCls_worksheetdetails_db.SelectById(id);
                return objworksheetdetails;
            }
            catch (Exception ex)
            {
                //ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objworksheetdetails;
            }
        }
         * /
         * */
        public Int64 Insert(worksheetdetails objworksheetdetails)
        {
            Int64 result = 0;
            try
            {
                Cls_worksheetdetails_db objCls_worksheetdetails_db = new Cls_worksheetdetails_db();

                result = Convert.ToInt64(objCls_worksheetdetails_db.Insert(objworksheetdetails));
                return result;
            }
            catch (Exception ex)
            {
                ////ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }

        /*
        public Int64 Update(worksheetdetails objworksheetdetails)
        {
            Int64 result = 0;
            try
            {
                Cls_worksheetdetails_db objCls_worksheetdetails_db = new Cls_worksheetdetails_db();

                result = Convert.ToInt64(objCls_worksheetdetails_db.Update(objworksheetdetails));
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
                Cls_worksheetdetails_db objCls_worksheetdetails_db = new Cls_worksheetdetails_db();

                if (objCls_worksheetdetails_db.Delete(id))
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
         * 
         * */
        #endregion

    }
    public partial class worksheetdetails
    {
        public worksheetdetails()
        { }

        //id,worksheetid,particularsid,employeeid,quantity,workdate,remark
        
        
        #region Private Variables
        private Int64 _id;
        private Int64 _worksheetid;
        private Int64 _particularsid;
        private Int64 _employeeid;
        private Int64 _quantity;
        private Double _wages;
        private DateTime _workdate;
        private String _remark;
        //private Int64 _Quantity1;
        //private Boolean _isdeleted;

        #endregion


        #region Public Properties

        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Int64 Worksheetid
        {
            get { return _worksheetid; }
            set { _worksheetid = value; }
        }
        public Int64 Particularsid
        {
            get { return _particularsid; }
            set { _particularsid = value; }
        }

        public Int64 Employeeid
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }

        //public Boolean isdeleted
        //{
        //    get { return _isdeleted; }
        //    set { _isdeleted = value; }
        //}

        public Int64 Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public Double Wages
        {
            get { return _wages; }
            set { _wages = value; }
        }

        public String Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public DateTime Workdate
        {
            get { return _workdate; }
            set { _workdate = value; }
        }
        //public Int64 Quantity1
        //{
        //    get { return _Quantity1; }
        //    set { _Quantity1 = value; }
        //}


        #endregion
    }
}
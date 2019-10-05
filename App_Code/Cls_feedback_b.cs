using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DatabaseLayer;

namespace BusinessLayer
{
    public class Cls_feedback_b
    {

        #region Constructor
        public Cls_feedback_b()
        { }
        #endregion

        #region Public Methods
        public DataTable SelectAll(feedback objfeedback)
        {
            DataTable dt = new DataTable();
            try
            {
                Cls_feedback_db objCls_feedback_db = new Cls_feedback_db();

                dt = objCls_feedback_db.SelectAll(objfeedback);
                return dt;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return dt;
            }
        }
        public feedback SelectById(Int64 feedbackid)
        {
            feedback objfeedback = new feedback();
            try
            {
                Cls_feedback_db objCls_feedback_db = new Cls_feedback_db();

                objfeedback = objCls_feedback_db.SelectById(feedbackid);
                return objfeedback;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return objfeedback;
            }
        }
        public Int64 Insert(feedback objfeedback)
        {
            Int64 result = 0;
            try
            {
                Cls_feedback_db objCls_feedback_db = new Cls_feedback_db();

                result = Convert.ToInt64(objCls_feedback_db.Insert(objfeedback));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public Int64 Update(feedback objfeedback)
        {
            Int64 result = 0;
            try
            {
                Cls_feedback_db objCls_feedback_db = new Cls_feedback_db();

                result = Convert.ToInt64(objCls_feedback_db.Update(objfeedback));
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                return result;
            }
        }
        public bool Delete(Int64 feedbackid)
        {
            try
            {
                Cls_feedback_db objCls_feedback_db = new Cls_feedback_db();

                if (objCls_feedback_db.Delete(feedbackid))
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
    public class feedback
    {
        public feedback()
        { }

        #region Private Variables
        private Int64 _feedbackid;
        private String _name;
        private String _phone;
        private String _feedback;
        private System.DateTime _feedbackdt;
        #endregion


        #region Public Properties
        public Int64 feedbackid
        {
            get { return _feedbackid; }
            set { _feedbackid = value; }
        }
        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public String feedbackmessage
        {
            get { return _feedback; }
            set { _feedback = value; }
        }
        public System.DateTime feedbackdt
        {
            get { return _feedbackdt; }
            set { _feedbackdt = value; }
        }
        #endregion
    }

}

using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class frm_Payment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {

        txtpaymentDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!Page.IsPostBack)
        {
            bindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Payment";
            Page.Title = "Payment";
            BindPaymentDetails(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
        }

    }
    public void BindPaymentDetails(Int64 orderId)
    {
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getOrdersDetails_byOrderId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@oid", orderId);
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt != null)
            {
                if (dt.Rows[0][0].ToString().Trim() == "")
                {
                }
                else
                {
                    Int64 oid = Convert.ToInt64(dt.Rows[0]["oid"].ToString().Trim());
                    Int64 uid = Convert.ToInt64(dt.Rows[0]["uid"].ToString().Trim());
                    decimal totalamount = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString().Trim());
                    string UserType = Convert.ToString(dt.Rows[0]["UserType"].ToString().Trim());
                    decimal PaidAmount = Convert.ToDecimal(dt.Rows[0]["PaidAmount"].ToString().Trim());
                    decimal RemainingAmt = Convert.ToDecimal(dt.Rows[0]["RemainingAmt"].ToString().Trim());

                    if (UserType.ToUpper().Trim() == "D".ToUpper().Trim())
                    {
                        ddlUserType.SelectedIndex = 1;
                    }
                    else
                    {
                        ddlUserType.SelectedIndex = 2;
                    }
                    ddlUserType_SelectedIndexChanged(null, null);
                    ddlname.SelectedValue = uid.ToString();
                    ddlname_SelectedIndexChanged(null, null);
                    ddlInvoiceNo.SelectedValue = oid.ToString();
                    //ddlInvoiceNo_SelectedIndexChanged(null, null);
                    txtTotalamt.Text = totalamount.ToString();

                    txtRemaningamt.Text = RemainingAmt.ToString();
                    txtPaidamt.Text = "0";
                    txtbalenceamt.Text = RemainingAmt.ToString();

                }
            }
        }
        catch { }
        finally { con.Close(); }
    }
    public void bindBank()
    {
        try
        {
            DataTable dt = new DataTable();
            Cls_bankmaster_b obj = new Cls_bankmaster_b();
            dt = obj.SelectAllAdmin();
            ddlBank.DataSource = null;
            ddlBank.DataBind();
            if (dt.Rows.Count > 0)
            {
                ddlBank.DataSource = dt;
                ddlBank.DataTextField = "bankname";
                ddlBank.DataValueField = "bankid";
                ddlBank.DataBind();
                ddlBank.Items.Insert(0, "---select Bank---");
            }
            else
            {

            }
        }
        catch { }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserType.SelectedItem.Value == "Dealer")
            {
                string Getalldealer = "select distinct did,name from dealermaster  where isactive=1 and isdeleted=0 ";
                SqlDataAdapter dadealer = new SqlDataAdapter(Getalldealer, con);
                DataTable dtdealer = new DataTable();
                dadealer.Fill(dtdealer);
                ddlname.DataSource = null;
                ddlname.DataBind();


                if (dtdealer.Rows.Count > 0)
                {
                    ddlname.DataSource = dtdealer;
                    ddlname.DataTextField = "name";
                    ddlname.DataValueField = "did";
                    ddlname.DataBind();
                    ddlname.Items.Insert(0, "---select---");
                }
                else
                {

                }

            }
            else if (ddlUserType.SelectedItem.Value == "Customer")
            {
                string Getallcusotmer = "select distinct uid,fname+''+mname+''+lname as name from userregistration where isactive=1 and isdelete=0 ";
                SqlDataAdapter dacust = new SqlDataAdapter(Getallcusotmer, con);
                DataTable dtcustoemr = new DataTable();
                dacust.Fill(dtcustoemr);
                ddlname.DataSource = null;
                ddlname.DataBind();

                if (dtcustoemr.Rows.Count > 0)
                {
                    ddlname.DataSource = dtcustoemr;
                    ddlname.DataTextField = "name";
                    ddlname.DataValueField = "uid";
                    ddlname.DataBind();
                    ddlname.Items.Insert(0, "---select---");
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
        finally { con.Close(); }
    }
    protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CustmerState = "";

            DataTable dtdealer = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "selectInvoiceNo_byUserTypeandid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@uid", Convert.ToInt64(ddlname.SelectedValue.ToString()));
            if (ddlUserType.SelectedItem.Value == "Dealer")
            {
                cmd.Parameters.Add("@UserType", "D");
            }
            else
            {
                cmd.Parameters.Add("@UserType", "U");
            }
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtdealer);
            ddlInvoiceNo.DataSource = null;
            ddlInvoiceNo.DataBind();
            if (dtdealer.Rows.Count > 0)
            {
                ddlInvoiceNo.DataSource = dtdealer;
                ddlInvoiceNo.DataTextField = "oid";
                ddlInvoiceNo.DataValueField = "oid";
                ddlInvoiceNo.DataBind();
                ddlInvoiceNo.Items.Insert(0, "---select---");
            }
            else
            {

            }


        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
        finally { con.Close(); }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToDouble(txtPaidamt.Text) > Convert.ToDouble(txtRemaningamt.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Enter proper Paid Amount')", true);
        }
        else
        {
            #region
            try
            {
                con.Open();
                if (txtPaidamt.Text == string.Empty && ddlUserType.SelectedIndex == 0 && ddlname.SelectedIndex == 0 && ddlInvoiceNo.SelectedIndex == 0||ddlBank.SelectedIndex==0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Enter all Fields')", true);
                }
                else
                {


                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "PaymentTransaction_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@Payid";
                    param.Value = 0;
                    param.SqlDbType = SqlDbType.BigInt;
                    param.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(param);
                    if (ddlUserType.SelectedItem.Value == "Dealer")
                    {
                        cmd.Parameters.Add("@UserType", "D");
                    }
                    else
                    {
                        cmd.Parameters.Add("@UserType", "U");
                    }

                    cmd.Parameters.AddWithValue("@FK_uid", Convert.ToInt64(ddlname.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@FK_bankId", Convert.ToInt64(ddlBank.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@dt", DateTime.Parse(txtpaymentDate.Text));
                    cmd.Parameters.AddWithValue("@TotalFee", Convert.ToDecimal(txtTotalamt.Text));
                    cmd.Parameters.AddWithValue("@PendingFee", Convert.ToDecimal(txtRemaningamt.Text));
                    cmd.Parameters.AddWithValue("@PaidFee", Convert.ToDecimal(txtPaidamt.Text));
                    cmd.Parameters.AddWithValue("@BalenceFee", Convert.ToDecimal(txtbalenceamt.Text));
                    cmd.Parameters.AddWithValue("@comment", txtComment.Text);
                    cmd.Parameters.AddWithValue("@FK_oid", Convert.ToInt64(ddlInvoiceNo.SelectedValue.ToString()));


                    int t = cmd.ExecuteNonQuery();
                    Int64 result = Convert.ToInt64(param.Value);
                    if (t > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Saved Successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Not Saved')", true);
                    }
                    Response.Redirect(Page.ResolveUrl("~/manageorders.aspx?mode=i"));
                    clear();
                }

            }
            catch { }
            finally { con.Close(); }
            #endregion
           
        }
    }
    public void clear()
    {
        ddlUserType.SelectedIndex = 0;
        ddlname.SelectedIndex = 0;
        ddlInvoiceNo.SelectedIndex = 0;
        txtpaymentDate.Text = string.Empty;
        txtTotalamt.Text = string.Empty;
        txtRemaningamt.Text = string.Empty;
        txtPaidamt.Text = string.Empty;
        txtbalenceamt.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        txtComment.Text = string.Empty;



    }
    //public void bindTransaction()
    //{
    //    try
    //    {
    //        con.Open();

    //        DataTable d = new DataTable();
    //        SqlCommand cmdrep = new SqlCommand();
    //        cmdrep.CommandText = "PaymentTransaction_byInvoiceId";
    //        cmdrep.CommandType = CommandType.StoredProcedure;
    //        cmdrep.Parameters.Add("@FK_oid", Convert.ToInt64(ddlInvoiceNo.SelectedValue.ToString()));
    //        cmdrep.Connection = con;
    //        SqlDataAdapter darep = new SqlDataAdapter(cmdrep);
    //        darep.Fill(d);
    //        if (d != null)
    //        {
    //            if (d.Rows.Count > 0)
    //            {
    //                repTransaction.DataSource = d;
    //                repTransaction.DataBind();
    //            }
    //            else
    //            {
    //                repTransaction.DataSource = null;
    //                repTransaction.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            repTransaction.DataSource = null;
    //            repTransaction.DataBind();
    //        }
    //    }
    //    catch { }
    //    finally { con.Close(); }
    //}
    protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //        try
        //        {
        //            con.Open();



        //txtTotalamt.Text=string.Empty;
        //txtRemaningamt.Text=string.Empty;
        //txtPaidamt.Text=string.Empty;
        //txtbalenceamt.Text=string.Empty;
        //ddlBank.SelectedIndex = 0;
        //txtComment.Text = string.Empty;


        //            string userType = "";
        //            if (ddlUserType.SelectedItem.Value == "Dealer")
        //            {
        //                userType = "D";

        //            }
        //            else
        //            {
        //                userType = "U";
        //            }
        //            double total = 0, paid = 0, remaining = 0;
        //            SqlCommand cmd = new SqlCommand("SELECT oid, uid, totalamount, orderdate, isdelete, UserType FROM orders where UserType='" +userType+"' and oid="+ddlInvoiceNo.SelectedValue.ToString()+"  and uid="+ddlname.SelectedValue.ToString()+"",con);
        //            SqlDataAdapter sda = new SqlDataAdapter();
        //            DataTable dt = new DataTable();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            sda.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtTotalamt.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["totalamount"]), 2).ToString();
        //                total = Convert.ToDouble(dt.Rows[0]["totalamount"].ToString());
        //            }

        //            //----------------------------

        //            SqlCommand cmd1 = new SqlCommand("SELECT SUM(PaidFee) as PaidFee   FROM tbl_PaymentTransaction where FK_oid=" + ddlInvoiceNo.SelectedValue + " and UserType='" + userType + "' and FK_uid=" + ddlname.SelectedValue + " and isdelete=0");
        //            SqlDataAdapter sda1 = new SqlDataAdapter();
        //            DataTable dt1 = new DataTable();
        //            cmd1.Connection = con;
        //            sda1.SelectCommand = cmd1;
        //            sda1.Fill(dt1);
        //            if (dt1==null || dt1.Rows[0]["PaidFee"].ToString().Trim() == "".ToString())
        //            {
        //                txtRemaningamt.Text = txtTotalamt.Text;
        //            }
        //            else
        //            {
        //                if (dt1.Rows.Count > 0)
        //                {

        //                    paid = Convert.ToDouble(dt1.Rows[0]["PaidFee"].ToString());
        //                    remaining = total - paid;
        //                    txtRemaningamt.Text = Math.Round(remaining, 2).ToString();
        //                }
        //                else
        //                {
        //                    txtRemaningamt.Text = txtTotalamt.Text;
        //                }
        //            }

        //        }
        //        catch { }
        //        finally { con.Close(); }
        //bindTransaction();
    }
    protected void txtPaidamt_TextChanged(object sender, EventArgs e)
    {
        if (txtRemaningamt.Text != string.Empty && txtPaidamt.Text != string.Empty)
        {

            decimal reman = Convert.ToDecimal(txtRemaningamt.Text.ToString());
            decimal paid = Convert.ToDecimal(txtPaidamt.Text.ToString());

            if (paid > reman)
            {
                txtPaidamt.BackColor = Color.Red;
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Enter Proper Amount')", true);
            }
            else
            {
                txtPaidamt.BackColor = Color.White;
                decimal balence = reman - paid;
                txtbalenceamt.Text = Convert.ToString(balence);
            }
        }
        else
        {
            txtbalenceamt.Text = string.Empty;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("manageorders.aspx");
    }
    //protected void lnkDelete_Click(object sender, EventArgs e)
    //{
    //    RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
    //    Int64 TransactionId = int.Parse((item.FindControl("lblTransactionId") as Label).Text);
    //    try
    //    {

    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "PaymentTransactionDelete_byTransactionId";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = con;

    //        cmd.Parameters.AddWithValue("@Payid", TransactionId);

    //        con.Open();
    //        int t=cmd.ExecuteNonQuery();
    //        if (t>0)
    //        {
    //            //spnMessage.Style.Add("color", "green");
    //            //spnMessage.InnerText = "Record Deleted Successfully";

    //        }
    //        else
    //        {
    //            //spnMessage.Style.Add("color", "red");
    //            //spnMessage.InnerText = "Record Not Deleted";

    //        }
    //    }
    //    catch { }
    //    finally { con.Close(); }

    //    ddlInvoiceNo.SelectedIndex = 0;
    //    txtTotalamt.Text = string.Empty;
    //    txtRemaningamt.Text = string.Empty;
    //    txtPaidamt.Text = string.Empty;
    //    txtbalenceamt.Text = string.Empty;
    //    ddlBank.SelectedIndex = 0;
    //    txtComment.Text = string.Empty;
    //    repTransaction.DataSource = null;
    //    repTransaction.DataBind();
    //    //bindTransaction();



    //}

}
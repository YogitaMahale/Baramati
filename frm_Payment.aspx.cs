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
    public void bindDelar()
    {
        try
        {
            string Getalldealer = "select distinct did,name from dealermaster  where  isdeleted=0 ";
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
        catch { }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        txtpaymentDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!Page.IsPostBack)
        {
            bindDelar();
         //   bindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Payment";
            Page.Title = "Payment";
          //  BindPaymentDetails(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
        }

    }
     

    protected void btnSave_Click(object sender, EventArgs e)
    {
       
            #region
            try
            {
                con.Open();
                if (txtPaidamt.Text == string.Empty  && ddlname.SelectedIndex == 0 &&ddlPaymentType.SelectedIndex==0 )
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
               
                    cmd.Parameters.AddWithValue("@FK_uid", Convert.ToInt64(ddlname.SelectedValue.ToString()));
                    
                    cmd.Parameters.AddWithValue("@date1", DateTime.Parse(txtpaymentDate.Text));
                    cmd.Parameters.AddWithValue("@Paidamt", Convert.ToDecimal(txtPaidamt.Text));
                   
                    cmd.Parameters.AddWithValue("@Note", txtComment.Text);
                     
                    cmd.Parameters.AddWithValue("@paymentType", Convert.ToString(ddlPaymentType.SelectedItem.ToString()));
                    cmd.Parameters.AddWithValue("@chequeno", Convert.ToString(txtChequeNo.Text));
                if(ddlPaymentType.SelectedItem.ToString()== "Cash")
                {
                    cmd.Parameters.AddWithValue("@paymentType1", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@paymentType1", Convert.ToString(ddlPaymentType1.SelectedItem.ToString()));
                }
                //cmd.Parameters.AddWithValue("@paymentType1", Convert.ToString(ddlPaymentType1.SelectedItem.ToString()));
                cmd.Parameters.AddWithValue("@bankName", Convert.ToString(txtbankName .Text));
                 

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
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Saved Successfully')", true);
                    clear();
                }

            }
            catch(Exception p)
        {
        }
            finally { con.Close(); }
            #endregion

         
    }
    public void clear()
    {
        
        ddlname.SelectedIndex = 0;
        
        txtpaymentDate.Text = string.Empty;
        
        txtPaidamt.Text = string.Empty;
        
        txtComment.Text = string.Empty;



    }
     
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("manageorders.aspx");
    }
    

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentType.SelectedItem.ToString() == "Cash".ToString())
        {
            ddlPaymentType1.Visible = false;
            txtChequeNo.Visible = false;
            txtbankName.Visible = false;
        }
        else if (ddlPaymentType.SelectedItem.ToString() == "Bank".ToString())
        {
            ddlPaymentType1.Visible = true  ;
            txtChequeNo.Visible = false;
            txtbankName.Visible = false;

        }
    }

    protected void ddlPaymentType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbankName.Visible = true;
        txtChequeNo.Visible = true;
    }
}
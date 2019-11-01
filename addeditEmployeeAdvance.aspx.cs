using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class addeditEmployeeAdvance : System.Web.UI.Page
{
   
    common ocommon = new common();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ToString());
    private void BindEmployee()
    {

        DataTable dt = new DataTable();
        
        try
        {
            Cls_Employee_b obj = new Cls_Employee_b();
            dt = obj.SelectAll();
             if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlemp .DataSource = dt;
                    ddlemp.DataTextField = "employeeName";
                    ddlemp.DataValueField = "id";
                    ddlemp.DataBind();
                    ListItem objListItem = new ListItem("--Select Employee--", "0");
                    ddlemp.Items.Insert(0, objListItem);
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindEmployee();
            //BindBank();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                 
                //btnSave.Text = "Update";
                //hPageTitle.InnerText = "Employee Advanced";
                //Page.Title = "Employee Advanced";
            }
            else
            {
                hPageTitle.InnerText = "Employee Advanced";
                Page.Title = "Employee Advanced";
                btnSave.Text = "Add";

            }
        }
    }

    private void Clear()
    {
        


    }



    //public void BindColor(Int64 CategoryId)
    //{
    //    sizeMaster objcategory = (new Cls_size_b().SelectById(CategoryId));
    //    if (objcategory != null)
    //    {
    //        //ddlBank.SelectedValue = objcategory.bankid.ToString();
    //        txtSizeName.Text = objcategory.sizeName;

    //    }
    //}

    protected override void Render(HtmlTextWriter writer)
    {
        string validatorOverrideScripts = "<script src=\"" + Page.ResolveUrl("~") + "js/validators.js\" type=\"text/javascript\"></script>";
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
        base.Render(writer);
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlemp.SelectedIndex == 0 || ddlType.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Select All Fields')", true);

        }
        else
        {

            Int64 Result = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "empAdvanced_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@id";
                p.DbType = DbType.Int64;
                p.Value = 0;
                p.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(p);
                cmd.Parameters.AddWithValue("@FK_eid", Convert.ToInt64(ddlemp.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@type", Convert.ToString(ddlType.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@amt", Convert.ToDecimal(txtamt.Text));
                cmd.Parameters.AddWithValue("@date1", Convert.ToDateTime(txt_Date.Text));

                cmd.ExecuteNonQuery();
                Result = Convert.ToInt64(p.Value);
                if (Result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Saved Successfully')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Not Saved')", true);
                    // spnMessgae.InnerText = "Size Not Updated";

                }
                ddlemp.SelectedIndex = 0;
                ddlType.SelectedIndex = 0;
                txt_Date.Text = string.Empty;
                txtamt.Text = string.Empty;

                con.Close();
            }
            catch { }
            finally { con.Close(); }
        }
      
    }



     
}
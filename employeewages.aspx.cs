using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class employeewages : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProduct = new DataTable();

    DataTable dtEmployee = new DataTable();
    Int64 Result = 0;

    Boolean update = false;
   

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    String usermail = String.Empty;
    String createdby = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Session["update"] = null;

            BindEmployee();
            BindWages();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Transport";
            txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);




        }

        if (Page.IsPostBack)
        {
            //Bind();

            // get a reference to ScriptManager and check if we have a partial postback
            if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                // partial (asynchronous) postback occured
                // insert Ajax custom logic here
                Console.Write("partial");
            }
            else
            {
                Console.Write("full");
                // regular full page postback occured
                // custom logic accordingly    

            }
        }




    }



    private void Clear()
    {
        lstCustomer.SelectedIndex = -1;
        txtfirstcount.Text = "0";
        txtfirstrate.Text = "0";
        txtcurrentfirstrate.Text = "0";
        txtsecondrate.Text = "0";
        txtcurrentsecondrate.Text = "0";
        txtvshaperate.Text = "0";
        txtcurrentvshaperate.Text = "0";
        txtsilairate.Text = "0";
        txtcurrentsilairate.Text = "0";
        txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);


    }


    private void BindEmployee()
    {
        try
        {
            Cls_Employee_b clsEmployee = new Cls_Employee_b();
            dtEmployee = clsEmployee.SelectAll();

            if (dtEmployee != null)
            {
                if (dtEmployee.Rows.Count > 0)
                {
                    lstCustomer.DataSource = dtEmployee;
                    lstCustomer.DataTextField = "employeename";
                    lstCustomer.DataValueField = "id";
                    lstCustomer.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Employee--", "0");
                    lstCustomer.Items.Insert(0, objListItem);



                }
                else
                {
                    lstCustomer.DataSource = dtEmployee;
                    lstCustomer.DataTextField = "employeename";
                    lstCustomer.DataValueField = "id";
                    lstCustomer.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Employee--", "0");
                    lstCustomer.Items.Insert(0, objListItem);



                }
            }
            else
            {
                lstCustomer.DataSource = dtEmployee;
                lstCustomer.DataTextField = "employeename";
                lstCustomer.DataValueField = "id";
                lstCustomer.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Employee--", "0");
                lstCustomer.Items.Insert(0, objListItem);



            }




        }
        catch { }
        finally { con.Close(); }




    }


    private void BindWages()
    {
        try
        {
            Cls_employeewages_b clsEmployee = new Cls_employeewages_b();
            DataTable dtEmployeeWages = clsEmployee.SelectAll();

            if (dtEmployeeWages != null)
            {
                if (dtEmployeeWages.Rows.Count > 0)
                {
                    repCategory.DataSource = dtEmployeeWages;
                    repCategory.DataBind();
                }
                else
                {
                    repCategory.DataSource = null;
                    repCategory.DataBind();
                }
            }
            else
            {
                repCategory.DataSource = null;
                repCategory.DataBind();
            }




        }
        catch { }
        finally { con.Close(); }


       

    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        employeewagesMaster objemployee = new employeewagesMaster();

        objemployee.eid = Convert.ToInt64(hfcustomer.Value);
        objemployee.firstcount = Convert.ToInt32(txtfirstcount.Text);
        objemployee.firstrate = Convert.ToDecimal( txtfirstrate.Text);
        objemployee.secondrate = Convert.ToDecimal( txtsecondrate.Text);
        objemployee.vshaperate = Convert.ToDecimal( txtvshaperate.Text);
        objemployee.silairate = Convert.ToDecimal( txtsilairate.Text);
        objemployee.currentfirstrate = Convert.ToDecimal( txtcurrentfirstrate.Text);
        objemployee.currentsecondrate = Convert.ToDecimal( txtcurrentsecondrate.Text);
        objemployee.currentsilairate = Convert.ToDecimal( txtcurrentsilairate.Text);
        objemployee.currentvshaperate = Convert.ToDecimal( txtcurrentvshaperate.Text);

        if (Session["update"] != null)
        {
            if (Convert.ToBoolean(Session["update"]))
            {
                if (Session["id"] != null)
                    objemployee.id = Convert.ToInt64(Session["id"]);
                Result = new Cls_employeewages_b().Update(objemployee);
                Session["update"] = false;
                if (Result > 0)
                {
                    //update = false;
                    Clear();
                    spnMessage.InnerText = "Employee Wages Updated Successfully !!!";
                    BindWages();
                    //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));

                }
                else
                {
                    Clear();
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "Employee Wages Not Updated...";

                }
            }
        }
        else
        {
            Result = new Cls_employeewages_b().Insert(objemployee);

            if (Result > 0)
            {
                Clear();
                spnMessage.InnerText = "Employee Wages Saved Successfully !!!";
                BindWages();
                //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));

            }
            else
            {
                Clear();
                spnMessage.Style.Add("color", "red");
                spnMessage.InnerText = "Employee Wages Not Saved...";

            }
        }

    }

    



    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        spnMessage.Visible = true;
        
        Int64 Id = int.Parse((item.FindControl("lblId") as Label).Text);
        bool yes = (new Cls_employeewages_b().Delete(Id));

        if (yes)
        {
            BindWages();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Employee Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Employee Not Deleted";
        }
        

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }



    protected void hlEdit_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as Button).Parent as RepeaterItem;

        Int64 Id = int.Parse((item.FindControl("lblId") as Label).Text);

        employeewagesMaster objemp = new Cls_employeewages_b().SelectById(Id);

        if (objemp != null)
        {
            lstCustomer.SelectedValue = objemp.eid.ToString();
            hfcustomer.Value = objemp.eid.ToString();
            txtfirstcount.Text = objemp.firstcount.ToString();
            txtfirstrate.Text = objemp.firstrate.ToString();
            txtsecondrate.Text = objemp.secondrate.ToString();
            txtvshaperate.Text = objemp.vshaperate.ToString();
            txtsilairate.Text = objemp.silairate.ToString();
            txtcurrentfirstrate.Text = objemp.currentfirstrate.ToString();
            txtcurrentsecondrate.Text = objemp.currentsecondrate.ToString();
            txtcurrentvshaperate.Text = objemp.currentvshaperate.ToString();
            txtcurrentsilairate.Text = objemp.currentsilairate.ToString();
            txt_Date.Text = objemp.createddate;

        }
        update = true;
        Session["update"] = true;
        Session["id"] = Id;
        btnSave.Text = "Update";
        spnMessage.InnerText = "";

    }
}
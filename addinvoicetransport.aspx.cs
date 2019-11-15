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

public partial class addinvoicetransport : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProduct = new DataTable();

    DataTable dtTransporter = new DataTable();
    DataTable dtDealer = new DataTable();
    DataTable dtInvoice = new DataTable();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    String usermail = String.Empty;
    String createdby = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Transport";
            txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
            txtinvoicedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);




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
        lstTransporter.SelectedIndex = -1;
        lstinvoiceno.SelectedIndex = -1;
        txtlrno.Text = String.Empty;
        txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtinvoicedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtfreightamount.Text = "0";
        txttotal.Text = "0";

    }


    private void Bind()
    {
        //List<SelectListItem> list = new List<SelectListItem>();
        
        //DataTable dtEmployee = new DataTable();
        

        try
        {

            Cls_transporter_b clsTransporter = new Cls_transporter_b();
            dtTransporter = clsTransporter.SelectAll();

            Cls_dealermaster_b clsdealermaster = new Cls_dealermaster_b();
            dtDealer = clsdealermaster.SelectAll();
            

            /*
            Cls_Employee_b clsEmployee = new Cls_Employee_b();
            dtEmployee = clsEmployee.SelectAll();
            */


        }
        catch { }
        finally { con.Close(); }

        if (dtTransporter != null)
        {
            if (dtTransporter.Rows.Count > 0)
            {
                lstTransporter.DataSource = dtTransporter;
                lstTransporter.DataTextField = "name";
                lstTransporter.DataValueField = "id";
                lstTransporter.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Transporter--", "0");
                lstTransporter.Items.Insert(0, objListItem);

            }
            else {
                lstTransporter.DataSource = dtTransporter;
                lstTransporter.DataTextField = "name";
                lstTransporter.DataValueField = "id";
                lstTransporter.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Transporter--", "0");
                lstTransporter.Items.Insert(0, objListItem);


            }

        }
        else
        {
            lstTransporter.DataSource = dtTransporter;
            lstTransporter.DataTextField = "name";
            lstTransporter.DataValueField = "id";
            lstTransporter.DataBind();
            System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Transporter--", "0");
            lstTransporter.Items.Insert(0, objListItem);


        }

        if (dtDealer != null)
        {
            if (dtDealer.Rows.Count > 0)
            {
                lstCustomer.DataSource = dtDealer;
                lstCustomer.DataTextField = "name";
                lstCustomer.DataValueField = "did";
                lstCustomer.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
                lstCustomer.Items.Insert(0, objListItem);



            }
            else
            {
                lstCustomer.DataSource = dtDealer;
                lstCustomer.DataTextField = "name";
                lstCustomer.DataValueField = "did";
                lstCustomer.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
                lstCustomer.Items.Insert(0, objListItem);



            }
        }
        else
        {
            lstCustomer.DataSource = dtDealer;
            lstCustomer.DataTextField = "name";
            lstCustomer.DataValueField = "did";
            lstCustomer.DataBind();
            System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Customer--", "0");
            lstCustomer.Items.Insert(0, objListItem);



        }


    }



    protected void lstCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        // DataSet ds = new DataSet();

        String customerid = hfcustomer.Value;
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT o.oid AS oid, c.orderno AS orderno FROM orders o, customer_orders c WHERE o.oid = c.ConfirmedInvoiceId AND o.isdelivered = 0 AND o.uid = @uid";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@uid", customerid);

            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtInvoice);

            if (dtInvoice != null)
            {
                if (dtInvoice.Rows.Count > 0)
                {

                    lstinvoiceno.DataSource = dtInvoice;
                    lstinvoiceno.DataTextField = "orderno";
                    lstinvoiceno.DataValueField = "oid";
                    lstinvoiceno.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Invoice--", "0");
                    lstinvoiceno.Items.Insert(0, objListItem);


                }
                else
                {
                    lstinvoiceno.DataSource = dtInvoice;
                    lstinvoiceno.DataTextField = "orderno";
                    lstinvoiceno.DataValueField = "oid";
                    lstinvoiceno.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Invoice--", "0");
                    lstinvoiceno.Items.Insert(0, objListItem);


                }
            }
            else
            {
                lstinvoiceno.DataSource = dtInvoice;
                lstinvoiceno.DataTextField = "orderno";
                lstinvoiceno.DataValueField = "oid";
                lstinvoiceno.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Invoice--", "0");
                lstinvoiceno.Items.Insert(0, objListItem);


            }

        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            
        }
        finally
        {
            con.Close();
        }



        

    }

    protected void lstinvoiceno_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 oid = Convert.ToInt64(hfinvoiceno.Value);
        DataTable dtOrder = new DataTable();

        orders objOrder = new Cls_orders_b().SelectById(oid);
        if (objOrder != null)
        {
            txtinvoicedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", objOrder.orderdate);

        }



    }





    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        invoicetransport objinvoicetransport = new invoicetransport();
        objinvoicetransport.transporterid = Convert.ToInt64(hftransporter.Value);
        objinvoicetransport.customerid = Convert.ToInt64(hfcustomer.Value);
        objinvoicetransport.lrno = txtlrno.Text.Trim();
        objinvoicetransport.lrdate = Convert.ToDateTime(txt_Date.Text.Trim());
        objinvoicetransport.invoiceno = hfinvoiceno.Value;
        objinvoicetransport.freightamount = Convert.ToDecimal(txtfreightamount.Text.Trim());
        objinvoicetransport.total = Convert.ToDecimal(txttotal.Text.Trim());
        
        
            Result = (new Cls_invoicetransport_b().Insert(objinvoicetransport));
            if (Result > 0)
            {
                Clear();
                spnMessgae.InnerText = "Invoice Transport Saved Successfully !!!";

            //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));

        }
        else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Invoice Transport Not Saved...";

            }
        
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/dashboard.aspx");
    }
    
    
}
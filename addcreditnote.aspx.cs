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

public partial class addcreditnote : System.Web.UI.Page
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
            hPageTitle.InnerText = "Credit Note";
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
        //lstTransporter.SelectedIndex = -1;
        lstinvoiceno.SelectedIndex = -1;
        //txtlrno.Text = String.Empty;
        txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtinvoicedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtfreightamount.Text = "0";
        txttotal.Text = "0";
        lstreason.SelectedIndex = -1;

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
            cmd.CommandText = "SELECT o.oid AS oid, c.orderno AS orderno FROM orders o, customer_orders c WHERE o.oid = c.ConfirmedInvoiceId AND o.uid = @uid";
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

        DataSet dsOrder = new DataSet();

        try
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "getOrderDetailsById",
                CommandType = CommandType.StoredProcedure,
                Connection = con

            };
            cmd.Parameters.AddWithValue("@oid", oid);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsOrder);

            if (dsOrder.Tables[0] != null)
            {
                if (dsOrder.Tables[0].Rows.Count > 0)
                {
                    txt_Date.Text = dsOrder.Tables[0].Rows[0]["orderdate"].ToString();
                    
                }
            }

            if (dsOrder.Tables[1] != null)
            {
                if (dsOrder.Tables[1].Rows.Count > 0)
                {
                    txttransporter.Text = dsOrder.Tables[1].Rows[0]["transportername"].ToString();
                    txtdeliverydetails.Text = dsOrder.Tables[1].Rows[0]["deliverydetails"].ToString();
                    
                }
            }



            if (dsOrder.Tables[2] != null)
            {
                if (dsOrder.Tables[2].Rows.Count > 0)
                {
                    
                }
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





    protected void btnSave_Click(object sender, EventArgs e)
    {
        //id, customerid, invoiceid, reason, disctypepercentage, amount, freightdiscount, createddate, isdeleted
        Decimal amount = 0;
        String disctypepercentage = String.Empty;
        Int64 Result = 0;

        if (!String.Equals("0", txttradedisc.Text)) { 
            disctypepercentage = "Trade Discount "+ txttradedisc.Text+"%";
            amount = Convert.ToDecimal(txttradeamount.Text);
            

        }
        else if (!String.Equals("0", txttaxabledisc.Text)) { 
            disctypepercentage = "Taxable Discount "+ txttaxabledisc.Text + "%";
            amount = Convert.ToDecimal(txttaxableamount.Text);

        }

        creditnotes objcreditnotes = new creditnotes();

        objcreditnotes.customerid = Convert.ToInt64(hfcustomer.Value);
        objcreditnotes.invoiceid = Convert.ToInt32(hfinvoiceno.Value);
        objcreditnotes.reason = hfreason.Value;
        objcreditnotes.disctypepercentage = disctypepercentage;
        objcreditnotes.amount = amount;
        objcreditnotes.freightdiscount = Convert.ToDecimal(txtfreightdiscount.Text);

        Result = (new Cls_creditnotes_b().Insert(objcreditnotes));
        if (Result > 0)
        {
            Clear();
            spnMessgae.InnerText = "Credit Note Saved Successfully !!!";

            //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));

        }
        else
        {
            Clear();
            spnMessgae.Style.Add("color", "red");
            spnMessgae.InnerText = "Credit Note Not Saved...";

        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //Response.Redirect("~/dashboard.aspx");
    }




    protected void txttradedisc_TextChanged(object sender, EventArgs e)
    {
        //string s = string.Format("{0:N2}%", x);
        if (String.IsNullOrEmpty(txttradedisc.Text))
            txttradedisc.Text = "0";

        Double tradedisc = Convert.ToDouble(txttradedisc.Text);
        Double subtotal = Convert.ToDouble(txtsubtotal.Text);
        Double tradeamount = (subtotal * tradedisc) / 100;
        txttradeamount.Text = tradeamount.ToString();
        Double total = subtotal - tradeamount;
        txttotal.Text = total.ToString();
        Double taxable = ((total / 105) * 100);
        txttaxable.Text = taxable.ToString("0.##");
        Double totalgst = total - taxable;
        txttotalgst.Text = totalgst.ToString("0.##");
        Double cgsgst = totalgst / 2;
        txtcgst.Text = cgsgst.ToString("0.##");
        txtsgst.Text = cgsgst.ToString("0.##");

    }

    protected void txttaxabledisc_TextChanged(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txttaxabledisc.Text))
            txttaxabledisc.Text = "0";

        Double taxabledisc = Convert.ToDouble(txttaxabledisc.Text);
        Double subtotal = Convert.ToDouble(txtsubtotal.Text);
        Double taxable = Convert.ToDouble(txttaxable.Text);
        Double taxableamount = (taxable * taxabledisc) / 100;
        txttaxableamount.Text = taxableamount.ToString("0.##");
        Double total = subtotal - taxableamount;
        txttotal.Text = total.ToString("0.##");


       


    }
}
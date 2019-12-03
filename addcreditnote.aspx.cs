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
       
        lstreason.SelectedIndex = -1;

        try
        {


            txt_Subtotal.Text = "0";
            txtTotalGstAmt.Text = "0";
            txttradDis.Text = "0";
            txttradAmt.Text = "0";
            txttaxableDis.Text = "0";
            txttaxableDisamt.Text = "0";
            txttaxableAmt.Text = "0";
            txttotalAmt.Text = "0";
            txtcsgtfinal.Text = "0";
            txtsgstfinal.Text = "0";
            txtIgstfinal.Text = "0";
            txtotherAmt.Text = "0";
            txtfreightdis.Text = "0";
            txttotalAmtfinal.Text = "0";
            txtduedate.Text = "0";
            txtreferenceby.Text = "0";
            txtdeliveredthrough.Text = "0";
            txtdelivereddetails.Text = "0";
            txttransporter.Text = "0";
            txtdeliverydetails.Text = "0";

            DataTable dt = new DataTable();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            ViewState["dtprod"] = dt;
        }
        catch { }
        finally { }
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
            Cls_orders_b obj = new Cls_orders_b();

            dtInvoice = obj.Selectorderdetailsbydealerid(Convert.ToInt64(lstCustomer.SelectedValue.ToString()));

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
                    txt_Subtotal.Text = dsOrder.Tables[0].Rows[0]["subamount"].ToString();
                    txtTotalGstAmt.Text = dsOrder.Tables[0].Rows[0]["totalGSTAmount"].ToString();
                    txttradDis.Text = dsOrder.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString();
                    txttradAmt.Text = dsOrder.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString();
                    txttaxableDis.Text = dsOrder.Tables[0].Rows[0]["per_taxableDiscount"].ToString();
                    txttaxableDisamt.Text = dsOrder.Tables[0].Rows[0]["amt_taxableDiscount"].ToString();
                    txttaxableAmt.Text = dsOrder.Tables[0].Rows[0]["TaxableAmount"].ToString();
                    txttotalAmt.Text = dsOrder.Tables[0].Rows[0]["TotalAmount"].ToString();
                    txtcsgtfinal.Text = dsOrder.Tables[0].Rows[0]["CGSTamt"].ToString();
                    txtsgstfinal.Text = dsOrder.Tables[0].Rows[0]["SGSTamt"].ToString();
                    txtIgstfinal.Text = dsOrder.Tables[0].Rows[0]["IGSTamt"].ToString();
                    txtotherAmt.Text = dsOrder.Tables[0].Rows[0]["otheramt"].ToString();
                    txtfreightdis.Text = dsOrder.Tables[0].Rows[0]["freightDiscount"].ToString();
                    txttotalAmtfinal.Text = dsOrder.Tables[0].Rows[0]["grandTotal"].ToString();
                    txtduedate.Text = dsOrder.Tables[0].Rows[0]["duedate"].ToString();
                    txtreferenceby.Text = dsOrder.Tables[0].Rows[0]["Referenceby"].ToString();
                    txtdeliveredthrough.Text = dsOrder.Tables[0].Rows[0]["DeliveredThrough"].ToString();
                    txtdelivereddetails.Text = dsOrder.Tables[0].Rows[0]["DeliveredDetails"].ToString();

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
                   

                    Repeater1.DataSource = dsOrder.Tables[2];
                    Repeater1.DataBind();
                    ViewState["dtprod"] = dsOrder.Tables[2];

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

        if (Convert.ToDecimal (txttradDis.Text)>0)
        {
            disctypepercentage = "Trade Discount " + txttradDis.Text + "%";
            amount = Convert.ToDecimal(txttradAmt .Text);


        }
        else if (Convert.ToDecimal(txttaxableDis.Text) > 0)   
        {
            disctypepercentage = "Taxable Discount " + txttaxableDis.Text + "%";
            amount = Convert.ToDecimal(txttaxableDisamt.Text);

        }

        creditnotes objcreditnotes = new creditnotes();

        objcreditnotes.customerid = Convert.ToInt64(hfcustomer.Value);
        objcreditnotes.invoiceid = Convert.ToInt32(hfinvoiceno.Value);
        objcreditnotes.reason = hfreason.Value;
        objcreditnotes.disctypepercentage = disctypepercentage;
        objcreditnotes.amount = amount;
        objcreditnotes.freightdiscount = Convert.ToDecimal(txtfreightdis.Text);
        objcreditnotes.otheramount  = Convert.ToDecimal(txtotherAmt .Text);
        Result = (new Cls_creditnotes_b().Insert(objcreditnotes));
        if (Result > 0)
        {
         Clear();
            spnMessgae.InnerText = "Credit Note Saved Successfully !!!";

            //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));
            Response.Redirect("addCreditNoteInvoice.aspx?id=" + Result);
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

        Response.Redirect("~/dashboard.aspx");
    }


    //public void gridTotal()
    //{


    //    //-------------------------
    //    try
    //    {
    //        DataTable dtprodn = new DataTable();
    //        dtprodn = (DataTable)ViewState["dtprod"];
    //        double t_subtotal = 0, t_totalGSTamt = 0, t_totalGSTamt1 = 0, t_taxableamt = 0, t_CGST = 0, t_SGST = 0, t_IGST = 0, t_grandTotal = 0;
    //        if (txttradDis.Text == "")
    //        {
    //            txttradDis.Text = "0";
    //        }
    //        double discount = Convert.ToDouble(txttradDis.Text);

    //        for (int i = 0; i < dtprodn.Rows.Count; i++)
    //        {
    //            double subTotal1 = Convert.ToDouble(dtprodn.Rows[i]["subTotal"].ToString());
    //            double disamt1 = (subTotal1 * discount) / 100;

    //            double grandtot = subTotal1 - disamt1;
    //            dtprodn.Rows[i]["discount"] = txttradDis.Text;
    //            dtprodn.Rows[i]["TotalAmount"] = grandtot.ToString();

    //            string t_subtotal1 = dtprodn.Rows[i]["subTotal"].ToString();
    //            string t_totalGSTamt11 = dtprodn.Rows[i]["GSTamt"].ToString();
    //            string t_taxableamt1 = dtprodn.Rows[i]["taxableamt"].ToString();
    //            string t_CGST1 = dtprodn.Rows[i]["CGSTper"].ToString();
    //            string t_SGST1 = dtprodn.Rows[i]["SGSTper"].ToString();
    //            string t_IGST1 = dtprodn.Rows[i]["IGSTper"].ToString();
    //            string t_grandTotal1 = dtprodn.Rows[i]["TotalAmount"].ToString();

    //            t_subtotal += Convert.ToDouble(t_subtotal1);
    //            t_totalGSTamt += Convert.ToDouble(t_totalGSTamt11);
    //            t_taxableamt += Convert.ToDouble(t_taxableamt1);
    //            t_CGST += Convert.ToDouble(t_CGST1);
    //            t_SGST += Convert.ToDouble(t_SGST1);
    //            t_IGST += Convert.ToDouble(t_IGST1);
    //            t_grandTotal += Convert.ToDouble(t_grandTotal1);


    //        }

    //        txt_Subtotal.Text = t_subtotal.ToString();
    //        txtTotalGstAmt.Text = t_totalGSTamt.ToString();
    //        //  txttradDis.Text = "0";
    //        //  txttaxableDis.Text = "0";
    //        txttaxableAmt.Text = t_taxableamt.ToString();

    //        //double subtot1 = t_subtotal - t_taxableamt;
    //        //txttotalAmt.Text = subtot1.ToString();

    //        txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
    //        txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
    //        txtIgstfinal.Text = t_totalGSTamt.ToString();
    //        if (t_CGST == 0)
    //        {
    //            txtcsgtfinal.Text = "0";

    //        }
    //        else if (t_SGST == 0)
    //        {

    //            txtsgstfinal.Text = "0";

    //        }
    //        else if (t_IGST == 0)
    //        {

    //            txtIgstfinal.Text = "0";
    //        }

    //        //txtotherAmt.Text = "0";
    //        // txtfreightdis.Text = "0";
    //        //txttotalAmtfinal.Text = t_grandTotal.ToString();
    //        //---------Taxable dis(%) Amount -------------------------------------------
    //        if (txttaxableAmt.Text == "")
    //        {
    //            txttaxableAmt.Text = "0";
    //        }
    //        if (txttaxableDis.Text == "")
    //        {
    //            txttaxableDis.Text = "0";
    //        }

    //        //-----------Trad Dis(%) And Scheme -------------
    //        if (txttradDis.Text == "")
    //        {
    //            txttradDis.Text = "0";
    //        }
    //        if (txt_Subtotal.Text == "")
    //        {
    //            txt_Subtotal.Text = "0";
    //        }
    //        if (txtotherAmt.Text == "")
    //        {
    //            txtotherAmt.Text = "0";
    //        }
    //        if (txtfreightdis.Text == "")
    //        {
    //            txtfreightdis.Text = "0";
    //        }
    //        if (txttaxableDis.Text == "")
    //        {
    //            txttaxableDis.Text = "0";
    //        }

    //        //decimal tradAmt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
    //        // txttradAmt.Text = tradAmt.ToString();
    //        //------------------------
    //        decimal totalamt1 = 0;
    //        txttradAmt.Text = "0";
    //        txttaxableDisamt.Text = "0";
    //        totalamt1=(Convert.ToDecimal(txt_Subtotal.Text));
    //        txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
    //        if (Convert.ToDecimal(txttradDis.Text)>0)
    //        {
    //            decimal tradDisamt =( (Convert.ToDecimal(txt_Subtotal.Text)) * Convert.ToDecimal(txttradDis.Text))/100;
    //            txttradAmt.Text = tradDisamt.ToString();
    //              totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - tradDisamt;
    //            txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
    //        }
    //        else if (Convert.ToDecimal(txttaxableDis.Text) > 0)
    //        {
    //            decimal disamt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
    //            txttaxableDisamt.Text = disamt.ToString();

    //              totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - Convert.ToDecimal(txttaxableDisamt.Text);
    //            txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
    //        }


    //        //------------------------------------------------------------------

    //        //------------------------------------------------------------------




    //        decimal lastTotal = totalamt1 - Convert.ToDecimal(txtotherAmt.Text) - Convert.ToDecimal(txtfreightdis.Text);
    //        txttotalAmtfinal.Text = System.Math.Round(lastTotal, 2).ToString();


    //        //-------------update in table
    //        dtprodn.AcceptChanges();
    //        ViewState["dtprod"] = dtprodn;
    //        Repeater1.DataSource = dtprodn;
    //        Repeater1.DataBind();
    //        //            txttradDis
    //        //txttradAmt
    //        //txt_Subtotal
    //        //txttotalAmt
    //        //txttotalAmtfinal
    //        //txtotherAmt
    //        //txtfreightdis


    //    }
    //    catch { }


    //}

    public void gridTotal()
    {


        //-------------------------
        try
        {
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            double t_subtotal = 0, t_totalGSTamt = 0, t_totalGSTamt1 = 0, t_taxableamt = 0, t_CGST = 0, t_SGST = 0, t_IGST = 0, t_grandTotal = 0;
            if (txttradDis.Text == "")
            {
                txttradDis.Text = "0";
            }
            double discount = Convert.ToDouble(txttradDis.Text);

            for (int i = 0; i < dtprodn.Rows.Count; i++)
            {
                //double subTotal1 = Convert.ToDouble(dtprodn.Rows[i]["subTotal"].ToString());
                //double disamt1 = (subTotal1 * discount) / 100;

                //double grandtot = subTotal1 - disamt1;
                //dtprodn.Rows[i]["discount"] = txttradDis.Text;
                //dtprodn.Rows[i]["TotalAmount"] = grandtot.ToString();

                string t_subtotal1 = dtprodn.Rows[i]["subTotal"].ToString();
                string t_totalGSTamt11 = dtprodn.Rows[i]["GSTamt"].ToString();
                string t_taxableamt1 = dtprodn.Rows[i]["taxableamt"].ToString();
                string t_CGST1 = dtprodn.Rows[i]["CGSTper"].ToString();
                string t_SGST1 = dtprodn.Rows[i]["SGSTper"].ToString();
                string t_IGST1 = dtprodn.Rows[i]["IGSTper"].ToString();
                string t_grandTotal1 = dtprodn.Rows[i]["TotalAmount"].ToString();

                t_subtotal += Convert.ToDouble(t_subtotal1);
                t_totalGSTamt += Convert.ToDouble(t_totalGSTamt11);
                t_taxableamt += Convert.ToDouble(t_taxableamt1);
                t_CGST += Convert.ToDouble(t_CGST1);
                t_SGST += Convert.ToDouble(t_SGST1);
                t_IGST += Convert.ToDouble(t_IGST1);
                t_grandTotal += Convert.ToDouble(t_grandTotal1);


            }

            txt_Subtotal.Text = t_subtotal.ToString();
            txtTotalGstAmt.Text = t_totalGSTamt.ToString();
            //  txttradDis.Text = "0";
            //  txttaxableDis.Text = "0";
            txttaxableAmt.Text = t_taxableamt.ToString();

            //double subtot1 = t_subtotal - t_taxableamt;
            //txttotalAmt.Text = subtot1.ToString();

            txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
            txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
            txtIgstfinal.Text = t_totalGSTamt.ToString();
            if (t_CGST == 0)
            {
                txtcsgtfinal.Text = "0";

            }
            if (t_SGST == 0)
            {

                txtsgstfinal.Text = "0";

            }
            if (t_IGST == 0)
            {

                txtIgstfinal.Text = "0";
            }

            //txtotherAmt.Text = "0";
            // txtfreightdis.Text = "0";
            //txttotalAmtfinal.Text = t_grandTotal.ToString();
            //---------Taxable dis(%) Amount -------------------------------------------
            if (txttaxableAmt.Text == "")
            {
                txttaxableAmt.Text = "0";
            }
            if (txttaxableDis.Text == "")
            {
                txttaxableDis.Text = "0";
            }

            //-----------Trad Dis(%) And Scheme -------------
            if (txttradDis.Text == "")
            {
                txttradDis.Text = "0";
            }
            if (txt_Subtotal.Text == "")
            {
                txt_Subtotal.Text = "0";
            }
            if (txtotherAmt.Text == "")
            {
                txtotherAmt.Text = "0";
            }
            if (txtfreightdis.Text == "")
            {
                txtfreightdis.Text = "0";
            }
            if (txttaxableDis.Text == "")
            {
                txttaxableDis.Text = "0";
            }

            //decimal tradAmt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
            // txttradAmt.Text = tradAmt.ToString();
            //------------------------
            decimal totalamt1 = 0;
            txttradAmt.Text = "0";
            txttaxableDisamt.Text = "0";
            //totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text));
            totalamt1 = Convert.ToDecimal(t_grandTotal.ToString());
            txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();


            decimal disamt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
            txttaxableDisamt.Text = disamt.ToString();


            //if (Convert.ToDecimal(txttradDis.Text) > 0)
            //{
            //    decimal tradDisamt = ((Convert.ToDecimal(txt_Subtotal.Text)) * Convert.ToDecimal(txttradDis.Text)) / 100;
            //    txttradAmt.Text = tradDisamt.ToString();
            //    totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - tradDisamt;
            //    txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
            //}
            //else if (Convert.ToDecimal(txttaxableDis.Text) > 0)
            //{
            //    decimal disamt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
            //    txttaxableDisamt.Text = disamt.ToString();

            //    totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - Convert.ToDecimal(txttaxableDisamt.Text);
            //    txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
            //}


            //------------------------------------------------------------------

            //------------------------------------------------------------------




            decimal lastTotal = totalamt1 - Convert.ToDecimal(txtotherAmt.Text) - Convert.ToDecimal(txtfreightdis.Text);
            txttotalAmtfinal.Text = System.Math.Round(lastTotal, 2).ToString();


            //-------------update in table
            dtprodn.AcceptChanges();
            ViewState["dtprod"] = dtprodn;
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();
            //            txttradDis
            //txttradAmt
            //txt_Subtotal
            //txttotalAmt
            //txttotalAmtfinal
            //txtotherAmt
            //txtfreightdis


        }
        catch { }


    }



    //public void BindOrders(Int64 Oid)
    //{
    //    try
    //    {
    //        con.Open();
    //        DataSet ds = new DataSet();
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "Display_CustomerOrder";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = con;
    //        cmd.Parameters.AddWithValue("@oid", Oid);
    //        //con.Open();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);

    //        if (ds.Tables[0] != null)
    //        {
    //            ddlname.SelectedValue = ds.Tables[0].Rows[0]["uid"].ToString();
    //            ddlname_SelectedIndexChanged(null, null);
    //            ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["paymentType"].ToString();
    //            ddlinvoiceType.SelectedValue = ds.Tables[0].Rows[0]["invoicetype"].ToString();
    //            txt_InvoieNo.Text = ds.Tables[0].Rows[0]["orderno"].ToString();
    //            if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Cash")
    //            {
    //                rdoCash.Checked = true;
    //                rdoCredit.Checked = false;

    //            }
    //            else if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Credit")
    //            {
    //                rdoCash.Checked = false;
    //                rdoCredit.Checked = true;
    //            }

    //            txt_Date.Text = ds.Tables[0].Rows[0]["orderdate"].ToString();
    //            txt_Subtotal.Text = ds.Tables[0].Rows[0]["subamount"].ToString();
    //            txtTotalGstAmt.Text = ds.Tables[0].Rows[0]["totalGSTAmount"].ToString();
    //            txttradDis.Text = ds.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString();
    //            txttradAmt.Text = ds.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString();
    //            txttaxableDis.Text = ds.Tables[0].Rows[0]["per_taxableDiscount"].ToString();
    //            txttaxableDisamt.Text = ds.Tables[0].Rows[0]["amt_taxableDiscount"].ToString();
    //            txttaxableAmt.Text = ds.Tables[0].Rows[0]["TaxableAmount"].ToString();
    //            txttotalAmt.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
    //            txtcsgtfinal.Text = ds.Tables[0].Rows[0]["CGSTamt"].ToString();
    //            txtsgstfinal.Text = ds.Tables[0].Rows[0]["SGSTamt"].ToString();
    //            txtIgstfinal.Text = ds.Tables[0].Rows[0]["IGSTamt"].ToString();
    //            txtotherAmt.Text = ds.Tables[0].Rows[0]["otheramt"].ToString();
    //            txtfreightdis.Text = ds.Tables[0].Rows[0]["freightDiscount"].ToString();
    //            txttotalAmtfinal.Text = ds.Tables[0].Rows[0]["grandTotal"].ToString();
    //            txtduedate.Text = ds.Tables[0].Rows[0]["duedate"].ToString();
    //            txtreferenceby.Text = ds.Tables[0].Rows[0]["Referenceby"].ToString();
    //            txtdeliveredthrough.Text = ds.Tables[0].Rows[0]["DeliveredThrough"].ToString();
    //            txtdelivereddetails.Text = ds.Tables[0].Rows[0]["DeliveredDetails"].ToString();

    //            DataTable dtprodn = new DataTable();
    //            dtprodn = (DataTable)ViewState["dtprod"];
    //            dtprodn.Rows.Clear();
    //            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
    //            {
    //                DataRow dr = dtprodn.NewRow();
    //                dr["sr"] = (i + 1).ToString();
    //                dr["productName"] = ds.Tables[1].Rows[i]["productName"].ToString();
    //                dr["pid"] = Convert.ToInt64(ds.Tables[1].Rows[i]["pid"].ToString());
    //                dr["brandid"] = Convert.ToString(ds.Tables[1].Rows[i]["brandid"].ToString());
    //                dr["sizeid"] = Convert.ToString(ds.Tables[1].Rows[i]["sizeid"].ToString());
    //                dr["colorid"] = Convert.ToString(ds.Tables[1].Rows[i]["colorid"].ToString());
    //                dr["cart"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["cart"].ToString());
    //                dr["pack"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["pack"].ToString());
    //                dr["qty"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["qty"].ToString());
    //                dr["mrp"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["mrp"].ToString());
    //                dr["unitRate"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["unitRate"].ToString());
    //                dr["subTotal"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["subTotal"].ToString());
    //                dr["discount"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["discount"].ToString());
    //                dr["scheme"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["scheme"].ToString());
    //                dr["taxableamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["taxableamt"].ToString());
    //                dr["CGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTper"].ToString());
    //                dr["SGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTper"].ToString());
    //                dr["IGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTper"].ToString());
    //                dr["GSTamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["GSTamt"].ToString());
    //                dr["TotalAmount"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalAmount"].ToString());

    //                dtprodn.Rows.Add(dr);
    //            }

    //            Repeater1.DataSource = dtprodn;
    //            Repeater1.DataBind();
    //            ViewState["dtprod"] = dtprodn;



    //        }


    //        con.Close();
    //    }
    //    catch { }
    //    finally { con.Close(); }

    //}

    protected void txttradDis_TextChanged(object sender, EventArgs e)
    {
        if(txttradDis.Text=="")
        {
            txttradDis.Text = "0";

        }
        gridTotal();
    }

    protected void txttaxableDis_TextChanged(object sender, EventArgs e)
    {
        if (txttaxableDis.Text == "")
        {
            txttaxableDis.Text = "0";

        }
        gridTotal();
    }

    protected void txtotherAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtotherAmt.Text == "")
        {
            txtotherAmt.Text = "0";

        }
        gridTotal();
    }

    protected void txtfreightdis_TextChanged(object sender, EventArgs e)
    {
        if (txtfreightdis.Text == "")
        {
            txtfreightdis.Text = "0";

        }
        gridTotal();
    }
}
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

public partial class adddebitnote : System.Web.UI.Page
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
            Bindsupplier();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Debit Note";
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
        try
        {
            lstCustomer.SelectedIndex = -1;
            lstreason.SelectedIndex = -1;
            lstinvoiceno.SelectedIndex = -1;
            txt_Subtotal.Text = string.Empty;
            txttradDis.Text = string.Empty;
            txtFriegtAmt.Text = string.Empty;
            txttaxableAmt.Text = string.Empty;
            txtcsgtfinal.Text = string.Empty;
            txtsgstfinal.Text = string.Empty;
            txtIgstfinal.Text = string.Empty;
            txttotalAmt.Text = string.Empty;
            txttransport.Text = string.Empty;
            txtpacking.Text = string.Empty;
            txtotherAmt.Text = string.Empty;
            txtdiscountamt.Text = string.Empty;
            txttotalAmtfinal.Text = string.Empty;
        }
        catch { }
       
    }


    private void Bindsupplier()
    {
        try
        {
            Cls_VendorMaster_b obj = new Cls_VendorMaster_b();
            DataTable dtVendor = new DataTable();
            dtVendor = obj.SelectAll();
            if (dtVendor.Rows.Count > 0)
            {
                lstCustomer.DataSource = dtVendor;
                lstCustomer.DataTextField = "vendorName";
                lstCustomer.DataValueField = "vid";
                lstCustomer.DataBind();
                lstCustomer.Items.Insert(0, "---select---");
            }
            else
            {

            }


        }
        catch { }
        finally { }

    }



    protected void lstCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        // DataSet ds = new DataSet();

        String customerid = hfcustomer.Value;
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT oid , invoiceno FROM PurchaseOrderHeader WHERE uid = @uid";
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
                    lstinvoiceno.DataTextField = "invoiceno";
                    lstinvoiceno.DataValueField = "oid";
                    lstinvoiceno.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Invoice--", "0");
                    lstinvoiceno.Items.Insert(0, objListItem);


                }
                else
                {
                    lstinvoiceno.DataSource = dtInvoice;
                    lstinvoiceno.DataTextField = "invoiceno";
                    lstinvoiceno.DataValueField = "oid";
                    lstinvoiceno.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Invoice--", "0");
                    lstinvoiceno.Items.Insert(0, objListItem);


                }
            }
            else
            {
                lstinvoiceno.DataSource = dtInvoice;
                lstinvoiceno.DataTextField = "invoiceno";
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
        try
        {
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Display_PurchaseOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@oid", oid);
            //con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0] != null)
            {
                //ddlname.SelectedValue = ds.Tables[0].Rows[0]["uid"].ToString();
                //ddlname_SelectedIndexChanged(null, null);

                //ddlinvoiceType.SelectedValue = ds.Tables[0].Rows[0]["invoicetype"].ToString();
                //txt_InvoieNo.Text = ds.Tables[0].Rows[0]["invoiceno"].ToString();
              
                //ddlaccountYear.SelectedValue = ds.Tables[0].Rows[0]["accountYear"].ToString();

                txt_Date.Text = ds.Tables[0].Rows[0]["orderdate"].ToString();

                txt_Subtotal.Text = ds.Tables[0].Rows[0]["subtotal"].ToString();
                txttradDis.Text = ds.Tables[0].Rows[0]["discandScheme"].ToString();
                txtFriegtAmt.Text = ds.Tables[0].Rows[0]["frieghtamount"].ToString();

                txttaxableAmt.Text = ds.Tables[0].Rows[0]["taxableamount"].ToString();
                txtcsgtfinal.Text = ds.Tables[0].Rows[0]["CGSTamt"].ToString();
                txtsgstfinal.Text = ds.Tables[0].Rows[0]["SGSTamt"].ToString();
                txtIgstfinal.Text = ds.Tables[0].Rows[0]["IGSTamt"].ToString();
                txttotalAmt.Text = ds.Tables[0].Rows[0]["totalAmt"].ToString();

                txttransport.Text = ds.Tables[0].Rows[0]["transportamt"].ToString();
                txtpacking.Text = ds.Tables[0].Rows[0]["packingamt"].ToString();
                txtotherAmt.Text = ds.Tables[0].Rows[0]["otheramt"].ToString();
                txtdiscountamt.Text = ds.Tables[0].Rows[0]["dicountamt"].ToString();
                txttotalAmtfinal.Text = ds.Tables[0].Rows[0]["grandtotal"].ToString();
                //txtStockDate.Text = ds.Tables[0].Rows[0]["stockdate"].ToString();


                DataTable dtprodn = new DataTable();
                dtprodn.Columns.Add("sr", typeof(String));
                dtprodn.Columns.Add("productName", typeof(String));
                dtprodn.Columns.Add("pid", typeof(String));
                dtprodn.Columns.Add("qty", typeof(String));
                dtprodn.Columns.Add("rate", typeof(String));
                dtprodn.Columns.Add("subtotal", typeof(String));
                dtprodn.Columns.Add("discount", typeof(String));
                dtprodn.Columns.Add("scheme", typeof(String));
                dtprodn.Columns.Add("frieghtamt", typeof(String));
                dtprodn.Columns.Add("taxableamt", typeof(String));
                dtprodn.Columns.Add("csgtper", typeof(String));

                dtprodn.Columns.Add("sgstper", typeof(String));
                dtprodn.Columns.Add("igstper", typeof(String));
                dtprodn.Columns.Add("gstamt", typeof(String));
                dtprodn.Columns.Add("total", typeof(String));

                dtprodn.Columns.Add("netrate", typeof(String));


                //dtprodn = (DataTable)ViewState["dtprod"];
                dtprodn.Rows.Clear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtprodn.NewRow();
                    dr["sr"] = (i + 1).ToString();
                    dr["productName"] = ds.Tables[1].Rows[i]["productName"].ToString();
                    dr["pid"] = Convert.ToInt64(ds.Tables[1].Rows[i]["pid"].ToString());
                    //  qty,rate,subtotal,discount,scheme,frieghtamt,taxableamt,csgtper,sgstper,igstper,gstamt,total,netrate

                    dr["qty"] = Convert.ToInt64(ds.Tables[1].Rows[i]["qty"].ToString());
                    dr["rate"] = Convert.ToString(ds.Tables[1].Rows[i]["rate"].ToString());
                    dr["subtotal"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["subtotal"].ToString());
                    dr["discount"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["discount"].ToString());
                    dr["scheme"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["scheme"].ToString());
                    dr["frieghtamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["frieghtamt"].ToString());
                    dr["taxableamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["taxableamt"].ToString());
                    dr["csgtper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["csgtper"].ToString());
                    dr["sgstper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["sgstper"].ToString());
                    dr["igstper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["igstper"].ToString());
                    dr["gstamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["gstamt"].ToString());
                    dr["total"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["total"].ToString());
                    dr["netrate"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["netrate"].ToString());


                    //qty,rate,subtotal,discount,scheme,frieghtamt,taxableamt,csgtper,sgstper,igstper,gstamt,total,netrate

                    dtprodn.Rows.Add(dr);
                }

                Repeater1.DataSource = dtprodn;
                Repeater1.DataBind();
                ViewState["dtprod"] = dtprodn;



            }


            con.Close();
        }
        catch { }
        finally { con.Close(); }





    }


   


    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        Decimal amount = 0;
        String disctypepercentage = String.Empty;
        Int64 Result = 0;        
        try
        {
            decimal subtotal = Convert.ToDecimal(txt_Subtotal.Text);
            decimal discount = Convert.ToDecimal(txttradDis.Text);
            decimal disamt = (subtotal * discount) / 100;  
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "debitnotes_insert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con ;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@id";
            param.Value =0;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@vendorid", Convert.ToInt64(hfcustomer.Value));
            cmd.Parameters.AddWithValue("@invoiceid", Convert.ToInt64(hfinvoiceno.Value));
            cmd.Parameters.AddWithValue("@reason", hfreason.Value);
            cmd.Parameters.AddWithValue("@disctypepercentage", txttradDis.Text);
            cmd.Parameters.AddWithValue("@amount", disamt);
            cmd.Parameters.AddWithValue("@freightamt", Convert.ToDecimal(txtFriegtAmt.Text));
            cmd.Parameters.AddWithValue("@transportamt", Convert.ToDecimal(txttransport.Text));
            cmd.Parameters.AddWithValue("@packingamt",Convert.ToDecimal(txtpacking.Text ));
            cmd.Parameters.AddWithValue("@otheramt", Convert.ToDecimal(txtotherAmt.Text ));
           
            cmd.ExecuteNonQuery();
            Result  = Convert.ToInt64(param.Value);
            if (Result > 0)
            {
                Clear();
                spnMessgae.InnerText = "Debit Note Saved Successfully !!!";

                //Response.Redirect(Page.ResolveUrl("~/manageEmployee.aspx?mode=i"));

            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Debit Note Not Saved...";

            }
            con.Close();
        }
        catch (Exception p)
        {

        }
        finally
        {
            con.Close();
        }
        

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        Response.Redirect("~/dashboard.aspx");
    }




    protected void txttradedisc_TextChanged(object sender, EventArgs e)
    {
        //string s = string.Format("{0:N2}%", x);
        /*
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
        */
    }

    protected void txttaxabledisc_TextChanged(object sender, EventArgs e)
    {
        /*
        if (String.IsNullOrEmpty(txttaxabledisc.Text))
            txttaxabledisc.Text = "0";

        Double taxabledisc = Convert.ToDouble(txttaxabledisc.Text);
        Double subtotal = Convert.ToDouble(txtsubtotal.Text);
        Double taxable = Convert.ToDouble(txttaxable.Text);
        Double taxableamount = (taxable * taxabledisc) / 100;
        txttaxableamount.Text = taxableamount.ToString("0.##");
        Double total = subtotal - taxableamount;
        txttotal.Text = total.ToString("0.##");
        */




    }

    protected void txttransport_TextChanged(object sender, EventArgs e)
    {
        if (txttransport.Text == "")
        {
            txttransport.Text = "0";
        }
        gridTotal();
    }

    protected void txtpacking_TextChanged(object sender, EventArgs e)
    {
        if (txtpacking .Text == "")
        {
            txtpacking .Text = "0";
        }
        gridTotal();
    }

    protected void txtotherAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtotherAmt .Text == "")
        {
            txtotherAmt .Text = "0";
        }
        gridTotal();
    }

    protected void txttradDis_TextChanged(object sender, EventArgs e)
    {
        if (txttradDis.Text == "")
        {
            txttradDis.Text = "0";
        }
        gridTotal();
    }
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
            if (txtFriegtAmt.Text == "")
            {
                txtFriegtAmt.Text = "0";
            }
            double discount = Convert.ToDouble(txttradDis.Text);
            double frieghtamt = Convert.ToDouble(txtFriegtAmt.Text);

            for (int i = 0; i < dtprodn.Rows.Count; i++)
            {
                //double subTotal1 = Convert.ToDouble(dtprodn.Rows[i]["subtotal"].ToString());
                //double disamt1 = (subTotal1 * discount) / 100;
                //double gst = Convert.ToDouble(dtprodn.Rows[i]["gstamt"].ToString());

                //double grandtot = subTotal1 - disamt1;
                //double final = grandtot - frieghtamt;
                //dtprodn.Rows[i]["discount"] = txttradDis.Text;
                //dtprodn.Rows[i]["frieghtamt"] = txtFriegtAmt.Text;
                //dtprodn.Rows[i]["total"] = (final + gst).ToString();

                string t_subtotal1 = dtprodn.Rows[i]["subtotal"].ToString();
                string t_totalGSTamt11 = dtprodn.Rows[i]["gstamt"].ToString();
                string t_taxableamt1 = dtprodn.Rows[i]["taxableamt"].ToString();
                string t_CGST1 = dtprodn.Rows[i]["csgtper"].ToString();
                string t_SGST1 = dtprodn.Rows[i]["sgstper"].ToString();
                string t_IGST1 = dtprodn.Rows[i]["igstper"].ToString();
                string t_grandTotal1 = dtprodn.Rows[i]["total"].ToString();

                t_subtotal += Convert.ToDouble(t_subtotal1);
                t_totalGSTamt += Convert.ToDouble(t_totalGSTamt11);
                t_taxableamt += Convert.ToDouble(t_taxableamt1);
                t_CGST += Convert.ToDouble(t_CGST1);
                t_SGST += Convert.ToDouble(t_SGST1);
                t_IGST += Convert.ToDouble(t_IGST1);
                t_grandTotal += Convert.ToDouble(t_grandTotal1);


            }
           
            txt_Subtotal.Text = (t_subtotal).ToString();

            txttaxableAmt.Text = t_taxableamt.ToString();

            if (t_CGST > 0 && t_SGST > 0)
            {
                txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
                txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
                txtIgstfinal.Text = "0";
            }
            else if (t_IGST > 0)
            {
                txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
                txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
                txtIgstfinal.Text = "0";
            }


            double discountamt = (t_subtotal * discount) / 100;

            txttotalAmt.Text = (t_subtotal - discountamt).ToString();

            if (txtFriegtAmt.Text == "")
            {
                txtFriegtAmt.Text = "0";
            }
            if (txttransport.Text == "")
            {
                txttransport.Text = "0";
            }
            if (txtpacking.Text == "")
            {
                txtpacking.Text = "0";
            }
            if (txtotherAmt.Text == "")
            {
                txtotherAmt.Text = "0";
            }
            decimal final11 = Convert.ToDecimal(txttotalAmt.Text) - Convert.ToDecimal(txttransport.Text) - Convert.ToDecimal(txtpacking.Text) - Convert.ToDecimal(txtotherAmt.Text) - Convert.ToDecimal(txtFriegtAmt.Text);
            txttotalAmtfinal.Text = System.Math.Round(final11, 2).ToString();




        }
        catch { }


    }

    protected void txtFriegtAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtFriegtAmt .Text == "")
        {
            txtFriegtAmt.Text = "0";
        }
        gridTotal();
    }
}
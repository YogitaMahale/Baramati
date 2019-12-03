using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

public partial class ManualOrder1 : System.Web.UI.Page
{
    common ocommon = new common();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

       



        txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtduedate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        //txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        //txtduedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        //-------------------------------------------------
        if (!IsPostBack)
        {
            SetFocus(ddlname);
          
            BindRepeater();
            BindDealer();
            BindProducts();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["oid"] != null)
            {



                BindOrders(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Order Update";
                Page.Title = "Order Update";
            }
            else
            {
                hPageTitle.InnerText = "Order Add";
                Page.Title = "Order Add";
            }
        }
    }
    public void BindDealer()
    {
        try
        {
            //if (ddlUserType.SelectedItem.Value == "Dealer")
            //{
            string Getalldealer = "select distinct did,name from dealermaster  where isactive=1 and isdeleted=0 ";
            SqlDataAdapter dadealer = new SqlDataAdapter(Getalldealer, con);
            DataTable dtdealer = new DataTable();
            dadealer.Fill(dtdealer);
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

            //}
            //else if (ddlUserType.SelectedItem.Value == "Customer")
            //{
            //    string Getallcusotmer = "select distinct uid,fname+''+mname+''+lname as name from userregistration where isactive=1 and isdelete=0 ";
            //    SqlDataAdapter dacust = new SqlDataAdapter(Getallcusotmer, con);
            //    DataTable dtcustoemr = new DataTable();
            //    dacust.Fill(dtcustoemr);
            //    if (dtcustoemr.Rows.Count > 0)
            //    {
            //        ddlname.DataSource = dtcustoemr;
            //        ddlname.DataTextField = "name";
            //        ddlname.DataValueField = "uid";
            //        ddlname.DataBind();
            //        ddlname.Items.Insert(0, "---select---");
            //    }
            //    else
            //    {

            //    }
            //}
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }
    public void BindProducts()
    {
        string GetProduct = "select distinct pid,productname from product";
        SqlDataAdapter daprodcut = new SqlDataAdapter(GetProduct, con);
        DataTable dtrpoduct = new DataTable();
        daprodcut.Fill(dtrpoduct);
        if (dtrpoduct.Rows.Count > 0)
        {
            ddlProduct.Items.Clear();
            ddlProduct.DataSource = dtrpoduct;
            ddlProduct.DataTextField = "productname";
            ddlProduct.DataValueField = "pid";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, "---Select----");
        }
        else
        { ddlProduct.Items.Clear(); }
    }
    protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CustmerState = "";
            //if (ddlUserType.SelectedItem.Value == "Dealer")
            //{
            string GetdealerData = "select name,userloginmobileno,email,gstno,address1+''+address2 as dadd from dealermaster  where did=" + ddlname.SelectedValue + "";
            SqlDataAdapter dadealer = new SqlDataAdapter(GetdealerData, con);
            DataTable dtdealer = new DataTable();
            dadealer.Fill(dtdealer);
            if (dtdealer.Rows.Count > 0)
            {
                txtAddress.Text = dtdealer.Rows[0]["dadd"].ToString();
                // txtemail.Text = dtdealer.Rows[0]["email"].ToString();
                txtPhone.Text = dtdealer.Rows[0]["userloginmobileno"].ToString();

                //hfid.Value = ddlname.SelectedValue;

            }
            else
            {

            }

            //}
            //else
            //{
            //    string Getcusotmerdata = "select fname+''+mname+''+lname as name,email,phone,address1+''+address2 as uadd from userregistration where uid=" + ddlname.SelectedValue + "";
            //    SqlDataAdapter dacust = new SqlDataAdapter(Getcusotmerdata, con);
            //    DataTable dtcustoemr = new DataTable();
            //    dacust.Fill(dtcustoemr);
            //    if (dtcustoemr.Rows.Count > 0)
            //    {
            //        txtAddress.Text = dtcustoemr.Rows[0]["uadd"].ToString();
            //        txtemail.Text = dtcustoemr.Rows[0]["email"].ToString();
            //        txtPhone.Text = dtcustoemr.Rows[0]["phone"].ToString();
            //        //txtcity.Text = dtcustoemr.Rows[0]["city"].ToString();
            //        //txtcountry.Text = dtcustoemr.Rows[0]["country"].ToString();
            //        // txtstate.Text = dtcustoemr.Rows[0]["State"].ToString();
            //        // txtGstNo.Text = "";
            //        hfid.Value = ddlname.SelectedValue;
            //        //CustmerState = dtcustoemr.Rows[0]["StateID"].ToString();
            //    }
            //    else
            //    {

            //    }
            //}


        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
        ddlPaymentType.Focus();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlname.SelectedIndex == 0 || ddlProduct.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Please Select All Product Name and Customer Name')", true);
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "salegrid_byproductId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pid", Convert.ToInt64(ddlProduct.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@uid", Convert.ToInt64(ddlname.SelectedValue.ToString()));

                con.Open();
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt != null)
                {
                    // ddlProduct
                    txtBrand.Text = dt.Rows[0]["brandName"].ToString();
                    txtSize.Text = dt.Rows[0]["sizeid"].ToString();
                    txtColor.Text = dt.Rows[0]["colorid"].ToString();
                    txtCart.Text = "0";
                    txtPack.Text = dt.Rows[0]["packing"].ToString();
                    txtQty.Text = "0";
                    txtMrp.Text = dt.Rows[0]["MRP"].ToString();
                    txtUnitRate.Text = dt.Rows[0]["unitRate"].ToString();
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtScheme.Text = "0";
                    txttaxable.Text = "0";
                    decimal cgst1 = Convert.ToDecimal(dt.Rows[0]["CGST"].ToString());
                    txtCGST.Text = System.Math.Round(cgst1, 2).ToString();
                    decimal sgst1 = Convert.ToDecimal(dt.Rows[0]["SGST"].ToString());
                    txtSgst.Text = System.Math.Round(sgst1, 2).ToString();
                    decimal Igst1 = Convert.ToDecimal(dt.Rows[0]["IGST"].ToString());
                    txtIgst.Text = System.Math.Round(Igst1, 2).ToString();
                    txtGSTtotal.Text = "0";
                    txtTotal.Text = "0";
                    //pid,productname,brandid,brandName,sizeid,colorid,packing,MRP,unitRate,CGST,SGST,IGST
                }
                con.Close();
                //salegrid_byproductId
            }
        }
        catch { }
        txtCart.Focus();
    }
    protected void txtCart_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txttradDis.Text == "")
            {
                txttradDis.Text = "0";
            }             
            if (txtCart.Text == "")
            {
                txtCart.Text = "0";
            }
            double cartqty = Convert.ToDouble(txtCart.Text);
            double packing = Convert.ToDouble(txtPack.Text);
            double unitRate = Convert.ToDouble(txtUnitRate.Text);
            double gst = Convert.ToDouble(txtCGST.Text) + Convert.ToDouble(txtSgst.Text) + Convert.ToDouble(txtIgst.Text);
            double totalQty = cartqty * packing;
            txtQty.Text = totalQty.ToString();
            double subtotal = totalQty * unitRate;
            txtSubTotal.Text = System.Math.Round(subtotal, 2).ToString();



            double disamt = (subtotal * Convert.ToDouble(txtDiscount.Text)) / 100;

            double tot1 = subtotal - disamt;

            double d1 = tot1 / (gst + 100);
            double taxableamt = d1 * 100;
            txttaxable.Text = System.Math.Round(taxableamt, 2).ToString();


            //grand
            double p = subtotal - disamt;
            txtTotal.Text = System.Math.Round(p, 2).ToString();

            double gstamt = (taxableamt * gst) / 100;
            txtGSTtotal.Text = System.Math.Round(gstamt, 2).ToString();


            //txtDiscount.Text = txttradDis.Text;
        }
        catch { }

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
    //            //double subTotal1 = Convert.ToDouble(dtprodn.Rows[i]["subTotal"].ToString());
    //            //double disamt1 = (subTotal1 * discount) / 100;

    //            //double grandtot = subTotal1 - disamt1;
    //            //dtprodn.Rows[i]["discount"] = txttradDis.Text;
    //            //dtprodn.Rows[i]["TotalAmount"] = grandtot.ToString();

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
    //         if (t_SGST == 0)
    //        {

    //            txtsgstfinal.Text = "0";

    //        }
    //         if (t_IGST == 0)
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
    //        totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text));
    //        txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();





    //        //if (Convert.ToDecimal(txttradDis.Text) > 0)
    //        //{
    //        //    decimal tradDisamt = ((Convert.ToDecimal(txt_Subtotal.Text)) * Convert.ToDecimal(txttradDis.Text)) / 100;
    //        //    txttradAmt.Text = tradDisamt.ToString();
    //        //    totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - tradDisamt;
    //        //    txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
    //        //}
    //        //else if (Convert.ToDecimal(txttaxableDis.Text) > 0)
    //        //{
    //        //    decimal disamt = (Convert.ToDecimal(txttaxableAmt.Text) * Convert.ToDecimal(txttaxableDis.Text)) / 100;
    //        //    txttaxableDisamt.Text = disamt.ToString();

    //        //    totalamt1 = (Convert.ToDecimal(txt_Subtotal.Text)) - Convert.ToDecimal(txttaxableDisamt.Text);
    //        //    txttotalAmt.Text = System.Math.Round(totalamt1, 2).ToString();
    //        //}


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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {


            if (ddlProduct.SelectedIndex == 0 || txtCart.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Enter Proper Value ');", true);
                return;
            }

            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            DataRow[] drr = dtprodn.Select("pid='" + ddlProduct.SelectedValue.ToString() + " ' ");
            if (drr.Length > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('This Product already add')", true);
            }
            else
            {
                DataRow dr = dtprodn.NewRow();
                int rowcnt = dtprodn.Rows.Count + 1;


                dr["sr"] = rowcnt.ToString();
                dr["productName"] = ddlProduct.SelectedItem.ToString();
                dr["pid"] = Convert.ToInt64(ddlProduct.SelectedValue.ToString());
                dr["brandid"] = Convert.ToString(txtBrand.Text);
                dr["sizeid"] = Convert.ToString(txtSize.Text);
                dr["colorid"] = Convert.ToString(txtColor.Text);
                dr["cart"] = Convert.ToDecimal(txtCart.Text);
                dr["pack"] = Convert.ToDecimal(txtPack.Text);
                dr["qty"] = Convert.ToDecimal(txtQty.Text);
                dr["mrp"] = Convert.ToDecimal(txtMrp.Text);
                dr["unitRate"] = Convert.ToDecimal(txtUnitRate.Text);
                dr["subTotal"] = Convert.ToDecimal(txtSubTotal.Text);
                dr["discount"] = Convert.ToDecimal(txtDiscount.Text);
                dr["scheme"] = Convert.ToDecimal(txtScheme.Text);
                dr["taxableamt"] = Convert.ToDecimal(txttaxable.Text);
                dr["CGSTper"] = Convert.ToDecimal(txtCGST.Text);
                dr["SGSTper"] = Convert.ToDecimal(txtSgst.Text);
                dr["IGSTper"] = Convert.ToDecimal(txtIgst.Text);
                dr["GSTamt"] = Convert.ToDecimal(txtGSTtotal.Text);
                dr["TotalAmount"] = Convert.ToDecimal(txtTotal.Text);


                dtprodn.Rows.Add(dr);
                Repeater1.DataSource = dtprodn;
                Repeater1.DataBind();
                ViewState["dtprod"] = dtprodn;


                //clear
                txtBrand.Text = "";
                txtSize.Text = "";
                txtColor.Text = "";
                txtCart.Text = "0";
                txtPack.Text = "0";
                txtQty.Text = "0";
                txtMrp.Text = "0";
                txtUnitRate.Text = "0";
                txtSubTotal.Text = "0";
                txtDiscount.Text = "0";
                txtScheme.Text = "0";
                txttaxable.Text = "0";
                txtCGST.Text = "0";
                txtSgst.Text = "0";
                txtIgst.Text = "0";
                txtGSTtotal.Text = "0";
                txtTotal.Text = "0";
                ddlProduct.SelectedIndex = 0;
                //GridTotals();
                //total Calculationd
                gridTotal();

            }
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }

    }
    private void BindRepeater()
    {

        string productdata = "select '' as sr,'' as productName,pid,brandid,sizeid, colorid, cart, pack, qty, mrp, unitRate, subTotal, discount, scheme, taxableamt, CGSTper, SGSTper, IGSTper, GSTamt, TotalAmount from saleorder_product where oid=-1";
        SqlDataAdapter daproduct = new SqlDataAdapter(productdata, con);
        DataTable dtprod = new DataTable();
        daproduct.Fill(dtprod);
        Repeater1.DataSource = dtprod;
        Repeater1.DataBind();
        ViewState["dtprod"] = dtprod;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlname.SelectedIndex == 0 || Repeater1.Items.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Fill all Details ');", true);
        }
        else
        {
            //Int64 OrderId_old = appOrderID;
            int tt = 0;
            Int64 OrderId = 0;
            try
            {

                con.Open();


                string finalResult = string.Empty;
                orders objorders = new orders();

                objorders.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());
                objorders.paymentType = Convert.ToString(ddlPaymentType.SelectedValue.ToString());
                objorders.invoicetype = Convert.ToString(ddlinvoiceType.SelectedValue.ToString());


                string paymentmode1 = "";
                if (rdoCash.Checked)
                {
                    paymentmode1 = "Cash";
                }
                else //else if(rdoCredit.Checked)
                {
                    paymentmode1 = "Credit";
                }
                objorders.paymentMode = Convert.ToString(paymentmode1);
                objorders.orderdate = DateTime.Now;
                //objorders.orderdate = Convert.ToDateTime(txt_Date.Text);
                objorders.subamount = Convert.ToDecimal(txt_Subtotal.Text);
                objorders.totalGSTAmount = Convert.ToDecimal(txtTotalGstAmt.Text);
                objorders.per_tradeDisandScheme = Convert.ToDecimal(txttradDis.Text);
                objorders.amt_tradeDisandScheme = Convert.ToDecimal(txttradAmt.Text);

                objorders.per_taxableDiscount = Convert.ToDecimal(txttaxableDis.Text);
                objorders.amt_taxableDiscount = Convert.ToDecimal(txttaxableDisamt.Text);
                objorders.TaxableAmount = Convert.ToDecimal(txttaxableAmt.Text);

                objorders.TotalAmount = Convert.ToDecimal(txttotalAmt.Text);
                objorders.CGSTamt = Convert.ToDecimal(txtcsgtfinal.Text);
                objorders.SGSTamt = Convert.ToDecimal(txtsgstfinal.Text);
                objorders.IGSTamt = Convert.ToDecimal(txtIgstfinal.Text);
                objorders.totalGSTAmount = Convert.ToDecimal(txtTotalGstAmt.Text);
                objorders.freightDiscount = Convert.ToDecimal(txtfreightdis.Text);
                objorders.duedate = Convert.ToDateTime(txtduedate.Text);
                objorders.otheramt = Convert.ToDecimal(txtotherAmt.Text);
                objorders.grandTotal = Convert.ToDecimal(txttotalAmtfinal.Text);
                objorders.Referenceby = Convert.ToString(txtreferenceby.Text);
                objorders.DeliveredThrough = Convert.ToString(txtdeliveredthrough.Text);
                objorders.DeliveredDetails = Convert.ToString(txtdelivereddetails.Text);
                objorders.ordertype = "";
                objorders.pendingAmt = Convert.ToDecimal(txttotalAmtfinal.Text);
                objorders.isconfirmed = true;

                Int64 OrderProductAdd = 0;

                DataTable ds = new DataTable();
                ds = (DataTable)ViewState["dtprod"];
                if (Request.QueryString["oid"] == null)
                {
                    //------------------------------

                    int year = int.Parse(DateTime.Now.Year.ToString());
                    string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                    int day = int.Parse(DateTime.Now.Day.ToString());
                    int min = int.Parse(DateTime.Now.Minute.ToString());
                    int hour = int.Parse(DateTime.Now.Hour.ToString());

                    string orderno1 = year + "_" + month.Substring(0, 3).ToUpper() + "_" + day + "_" + hour + min;

                    objorders.orderno = orderno1;

                    //save3
                    #region
                    if (ds != null)
                    {
                        if (ds.Rows.Count > 0)
                        {
                            OrderId = (new Cls_orders_b().Insert(objorders));
                            if (OrderId > 0)
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    OrderProductAdd = 0;

                                    orderproducts objorderproducts = new orderproducts();
                                    objorderproducts.oid = OrderId;
                                    objorderproducts.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());
                                    objorderproducts.pid = Convert.ToInt64(ds.Rows[i]["pid"]);
                                    objorderproducts.brandid = Convert.ToString(ds.Rows[i]["brandid"]);
                                    objorderproducts.sizeid = Convert.ToString(ds.Rows[i]["sizeid"]);
                                    objorderproducts.colorid = Convert.ToString(ds.Rows[i]["colorid"]);
                                    objorderproducts.cart = Convert.ToDecimal(ds.Rows[i]["cart"]);
                                    objorderproducts.pack = Convert.ToString(ds.Rows[i]["pack"]);
                                    objorderproducts.qty = Convert.ToDecimal(ds.Rows[i]["qty"]);
                                    objorderproducts.mrp = Convert.ToDecimal(ds.Rows[i]["mrp"]);
                                    objorderproducts.unitRate = Convert.ToDecimal(ds.Rows[i]["unitRate"]);
                                    objorderproducts.subTotal = Convert.ToDecimal(ds.Rows[i]["subTotal"]);

                                    objorderproducts.discount = Convert.ToDecimal(ds.Rows[i]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Rows[i]["scheme"]);
                                    objorderproducts.taxableamt = Convert.ToDecimal(ds.Rows[i]["taxableamt"]);

                                    objorderproducts.CGSTper = Convert.ToDecimal(ds.Rows[i]["CGSTper"]);
                                    objorderproducts.SGSTper = Convert.ToDecimal(ds.Rows[i]["SGSTper"]);
                                    objorderproducts.IGSTper = Convert.ToDecimal(ds.Rows[i]["IGSTper"]);
                                    objorderproducts.GSTamt = Convert.ToDecimal(ds.Rows[i]["GSTamt"]);
                                    objorderproducts.TotalAmount = Convert.ToDecimal(ds.Rows[i]["TotalAmount"]);
                                    objorderproducts.isdelete = false;



                                    OrderProductAdd = (new Cls_orderproducts_b().Insert(objorderproducts));
                                    #region " Stock Update "
                                    //  Product_StockUpdate(objorderproducts.pid, objorderproducts.quantites);
                                    #endregion " Stock Update "

                                }
                            }
                        }
                    }
                    if (OrderId > 0)
                    {
                        //SendOrderMail(OrderId);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record Saved Successfully')", true);
                        clear();
                    }
                    else
                    {

                    }
                    #endregion
                }
                else
                {
                    //objorders.orderno = orderno1;
                    //--------------------------------
                    objorders.orderno = Convert.ToString(txt_InvoieNo.Text);
                    //update
                    #region

                    if (ds != null)
                    {
                        if (ds.Rows.Count > 0)
                        {
                            objorders.oid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
                            OrderId = (new Cls_orders_b().Update(objorders));
                            if (OrderId > 0)
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    OrderProductAdd = 0;

                                    orderproducts objorderproducts = new orderproducts();
                                    objorderproducts.oid = OrderId;
                                    objorderproducts.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());
                                    objorderproducts.pid = Convert.ToInt64(ds.Rows[i]["pid"]);
                                    objorderproducts.brandid = Convert.ToString(ds.Rows[i]["brandid"]);
                                    objorderproducts.sizeid = Convert.ToString(ds.Rows[i]["sizeid"]);
                                    objorderproducts.colorid = Convert.ToString(ds.Rows[i]["colorid"]);
                                    objorderproducts.cart = Convert.ToDecimal(ds.Rows[i]["cart"]);
                                    objorderproducts.pack = Convert.ToString(ds.Rows[i]["pack"]);
                                    objorderproducts.qty = Convert.ToDecimal(ds.Rows[i]["qty"]);
                                    objorderproducts.mrp = Convert.ToDecimal(ds.Rows[i]["mrp"]);
                                    objorderproducts.unitRate = Convert.ToDecimal(ds.Rows[i]["unitRate"]);
                                    objorderproducts.subTotal = Convert.ToDecimal(ds.Rows[i]["subTotal"]);

                                    objorderproducts.discount = Convert.ToDecimal(ds.Rows[i]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Rows[i]["scheme"]);
                                    objorderproducts.taxableamt = Convert.ToDecimal(ds.Rows[i]["taxableamt"]);

                                    objorderproducts.CGSTper = Convert.ToDecimal(ds.Rows[i]["CGSTper"]);
                                    objorderproducts.SGSTper = Convert.ToDecimal(ds.Rows[i]["SGSTper"]);
                                    objorderproducts.IGSTper = Convert.ToDecimal(ds.Rows[i]["IGSTper"]);
                                    objorderproducts.GSTamt = Convert.ToDecimal(ds.Rows[i]["GSTamt"]);
                                    objorderproducts.TotalAmount = Convert.ToDecimal(ds.Rows[i]["TotalAmount"]);
                                    objorderproducts.isdelete = false;


                                    try
                                    {
                                        con.Close();
                                        con.Open();
                                        string sel = "select * from orderproducts  where oid=" + OrderId + " and pid=" + Convert.ToInt64(ds.Rows[i]["pid"]) + "";
                                        SqlCommand sel_cmd = new SqlCommand(sel, con);
                                        SqlDataReader sel_dr = sel_cmd.ExecuteReader();
                                        if (sel_dr.HasRows)
                                        {
                                            if (sel_dr.Read())
                                            {
                                                objorderproducts.opid = Convert.ToInt64(sel_dr["opid"].ToString());
                                            }
                                            con.Close();
                                            sel_dr.Close();

                                            OrderProductAdd = (new Cls_orderproducts_b().Update(objorderproducts));
                                        }
                                        else
                                        {
                                            con.Close();
                                            sel_dr.Close();
                                            OrderProductAdd = (new Cls_orderproducts_b().Insert(objorderproducts));

                                        }
                                    }
                                    catch { }
                                    finally { con.Close(); }


                                    //  Product_StockUpdate(objorderproducts.pid, objorderproducts.quantites);


                                }
                            }
                        }
                    }
                    if (OrderId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('Record updated Successfully')", true);
                        clear();
                    }
                    else
                    {

                    }
                    #endregion



                }






                ////******************
                //string s_update = "update Customer_orders set isCreateInvoice=1,ConfirmedInvoiceId='" + OrderId + "' where oid=" + OrderId_old;
                //SqlCommand cmd_update = new SqlCommand(s_update, con);
                //tt = cmd_update.ExecuteNonQuery();

                ////--------------------------




            }
            catch { }
            finally { con.Close(); }

            if (OrderId > 0)
            {
                clear();
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Order insert Successfully ');", true);
              
                
                Response.Redirect(Page.ResolveUrl("~/manageCustomerOrder.aspx?mode=i"));
               
            }
            else
            {
                clear();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Order not insert ');", true);

            }
        }
    }
    public void clear()
    {


        txtPhone.Text = String.Empty;
        txtAddress.Text = String.Empty;
        ddlname.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        ddlinvoiceType.SelectedIndex = 0;
        txt_InvoieNo.Text = String.Empty;
        rdoCash.Checked = true;
        rdoCredit.Checked = false;
        txt_Date.Text = String.Empty;


        txt_Subtotal.Text = String.Empty;
        txtTotalGstAmt.Text = String.Empty;
        txttradDis.Text = String.Empty;
        txttradAmt.Text = String.Empty;
        txttaxableDis.Text = String.Empty;
        txttaxableDisamt.Text = String.Empty;
        txttaxableAmt.Text = String.Empty;
        txttotalAmt.Text = String.Empty;
        txtcsgtfinal.Text = String.Empty;
        txtsgstfinal.Text = String.Empty;
        txtIgstfinal.Text = String.Empty;
        txtotherAmt.Text = String.Empty;
        txtfreightdis.Text = String.Empty;
        txttotalAmtfinal.Text = String.Empty;
        txtduedate.Text = String.Empty;
        txtreferenceby.Text = String.Empty;
        txtdeliveredthrough.Text = String.Empty;
        txtdelivereddetails.Text = String.Empty;

        DataTable dtprodn = new DataTable();
        dtprodn = (DataTable)ViewState["dtprod"];
        dtprodn.Rows.Clear();
        Repeater1.DataSource = dtprodn;
        Repeater1.DataBind();
        ViewState["dtprod"] = dtprodn;

        txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtduedate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);


    }
    public void BindOrders(Int64 Oid)
    {
        try
        {
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Display_CustomerOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@oid", Oid);
            //con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[0] != null)
            {
                ddlname.SelectedValue = ds.Tables[0].Rows[0]["uid"].ToString();
                ddlname_SelectedIndexChanged(null, null);
                ddlPaymentType.SelectedValue = ds.Tables[0].Rows[0]["paymentType"].ToString();
                ddlinvoiceType.SelectedValue = ds.Tables[0].Rows[0]["invoicetype"].ToString();
                txt_InvoieNo.Text = ds.Tables[0].Rows[0]["orderno"].ToString();
                if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Cash")
                {
                    rdoCash.Checked = true;
                    rdoCredit.Checked = false;

                }
                else if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Credit")
                {
                    rdoCash.Checked = false;
                    rdoCredit.Checked = true;
                }

                txt_Date.Text = ds.Tables[0].Rows[0]["orderdate"].ToString();
                txt_Subtotal.Text = ds.Tables[0].Rows[0]["subamount"].ToString();
                txtTotalGstAmt.Text = ds.Tables[0].Rows[0]["totalGSTAmount"].ToString();
                txttradDis.Text = ds.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString();
                txttradAmt.Text = ds.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString();
                txttaxableDis.Text = ds.Tables[0].Rows[0]["per_taxableDiscount"].ToString();
                txttaxableDisamt.Text = ds.Tables[0].Rows[0]["amt_taxableDiscount"].ToString();
                txttaxableAmt.Text = ds.Tables[0].Rows[0]["TaxableAmount"].ToString();
                txttotalAmt.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                txtcsgtfinal.Text = ds.Tables[0].Rows[0]["CGSTamt"].ToString();
                txtsgstfinal.Text = ds.Tables[0].Rows[0]["SGSTamt"].ToString();
                txtIgstfinal.Text = ds.Tables[0].Rows[0]["IGSTamt"].ToString();
                txtotherAmt.Text = ds.Tables[0].Rows[0]["otheramt"].ToString();
                txtfreightdis.Text = ds.Tables[0].Rows[0]["freightDiscount"].ToString();
                txttotalAmtfinal.Text = ds.Tables[0].Rows[0]["grandTotal"].ToString();
                txtduedate.Text = ds.Tables[0].Rows[0]["duedate"].ToString();
                txtreferenceby.Text = ds.Tables[0].Rows[0]["Referenceby"].ToString();
                txtdeliveredthrough.Text = ds.Tables[0].Rows[0]["DeliveredThrough"].ToString();
                txtdelivereddetails.Text = ds.Tables[0].Rows[0]["DeliveredDetails"].ToString();

                DataTable dtprodn = new DataTable();
                dtprodn = (DataTable)ViewState["dtprod"];
                dtprodn.Rows.Clear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtprodn.NewRow();
                    dr["sr"] = (i + 1).ToString();
                    dr["productName"] = ds.Tables[1].Rows[i]["productName"].ToString();
                    dr["pid"] = Convert.ToInt64(ds.Tables[1].Rows[i]["pid"].ToString());
                    dr["brandid"] = Convert.ToString(ds.Tables[1].Rows[i]["brandid"].ToString());
                    dr["sizeid"] = Convert.ToString(ds.Tables[1].Rows[i]["sizeid"].ToString());
                    dr["colorid"] = Convert.ToString(ds.Tables[1].Rows[i]["colorid"].ToString());
                    dr["cart"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["cart"].ToString());
                    dr["pack"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["pack"].ToString());
                    dr["qty"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["qty"].ToString());
                    dr["mrp"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["mrp"].ToString());
                    dr["unitRate"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["unitRate"].ToString());
                    dr["subTotal"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["subTotal"].ToString());
                    dr["discount"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["discount"].ToString());
                    dr["scheme"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["scheme"].ToString());
                    dr["taxableamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["taxableamt"].ToString());
                    dr["CGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTper"].ToString());
                    dr["SGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTper"].ToString());
                    dr["IGSTper"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTper"].ToString());
                    dr["GSTamt"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["GSTamt"].ToString());
                    dr["TotalAmount"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["TotalAmount"].ToString());

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
    protected void rep_txtCart_TextChanged(object sender, EventArgs e)
    {
        try
        {
            RepeaterItem row = ((RepeaterItem)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in    
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            Label txtsr = (Label)row.FindControl("txtsr");
            Label rep_txtproductName = (Label)row.FindControl("rep_txtproductName");
            Label rep_txtproductid = (Label)row.FindControl("rep_txtproductid");
            Label rep_txtBrand = (Label)row.FindControl("rep_txtBrand");
            Label rep_txtSize = (Label)row.FindControl("rep_txtSize");
            Label rep_txtColor = (Label)row.FindControl("rep_txtColor");
            TextBox rep_txtCart = (TextBox)row.FindControl("rep_txtCart");
            Label rep_txtpacking = (Label)row.FindControl("rep_txtpacking");
            Label rep_txtqty = (Label)row.FindControl("rep_txtqty");
            Label rep_txtMrp = (Label)row.FindControl("rep_txtMrp");
            Label rep_txtUnitRate = (Label)row.FindControl("rep_txtUnitRate");
            Label rep_txtSubtotal = (Label)row.FindControl("rep_txtSubtotal");
            TextBox rep_txtDiscount = (TextBox)row.FindControl("rep_txtDiscount");
            Label rep_txtScheme = (Label)row.FindControl("rep_txtScheme");
            Label rep_txtTaxableAmt = (Label)row.FindControl("rep_txtTaxableAmt");
            Label rep_txtCGST = (Label)row.FindControl("rep_txtCGST");
            Label rep_txtSGST = (Label)row.FindControl("rep_txtSGST");
            Label rep_txtIGST = (Label)row.FindControl("rep_txtIGST");
            Label rep_txtGSTamt = (Label)row.FindControl("rep_txtGSTamt");
            Label rep_txtfinalTotal = (Label)row.FindControl("rep_txtfinalTotal");




            if (rep_txtCart.Text == "")
            {
                rep_txtCart.Text = "0";
            }
            double cartqty = Convert.ToDouble(rep_txtCart.Text);
            double packing = Convert.ToDouble(rep_txtpacking.Text);
            double unitRate = Convert.ToDouble(rep_txtUnitRate.Text);
            double gst = Convert.ToDouble(rep_txtCGST.Text) + Convert.ToDouble(rep_txtSGST.Text) + Convert.ToDouble(rep_txtIGST.Text);
            double totalQty = cartqty * packing;

            //rep_txtqty.Text = totalQty.ToString();
            double subtotal = totalQty * unitRate;
            if (rep_txtDiscount.Text == "")
            {
                rep_txtDiscount.Text="0";
            }
            //double  disAmt = Convert.ToDouble(rep_txtDiscount.Text);
            //double grandtotal = subtotal - disAmt;
            //double d1 = subtotal / (gst + 100);
            //double taxableamt = d1 * 100;
            //double gstamt = subtotal - taxableamt;
            double disamt = (subtotal * Convert.ToDouble(rep_txtDiscount.Text)) / 100;
            double tot1 = subtotal - disamt;
            double d1 = tot1 / (gst + 100);
            double taxableamt = d1 * 100;
            //txttaxable.Text = System.Math.Round(taxableamt, 2).ToString();
           
            double grandtotal = subtotal - disamt;
           // txtTotal.Text = System.Math.Round(p, 2).ToString();
            double gstamt = (taxableamt * gst) / 100;
            txtGSTtotal.Text = System.Math.Round(gstamt, 2).ToString();


            DataRow[] drr = dtprodn.Select("sr='" + txtsr.Text + " ' ");
            foreach (var dr in drr)
            {
              
              
                dr["cart"] = cartqty.ToString() ;
                dr["qty"] = totalQty.ToString();
                dr["discount"] = rep_txtDiscount.Text;
                dr["subTotal"] = System.Math.Round(subtotal, 2).ToString();
                dr["taxableamt"] = System.Math.Round(taxableamt, 2).ToString();
                dr["GSTamt"] = System.Math.Round(gstamt, 2).ToString();
                dr["TotalAmount"] = System.Math.Round(grandtotal, 2).ToString();


            }
            // dtProduct.Rows.RemoveAt(prodid);

            dtprodn.AcceptChanges();
            ViewState["dtprod"] = dtprodn;
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();
        }

        catch(Exception p)
        { }
        gridTotal();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            Int64 rep_txtproductid = int.Parse((item.FindControl("rep_txtproductid") as Label  ).Text);
            Int64 txtsr = int.Parse((item.FindControl("txtsr") as Label).Text);
            DataRow[] drr = dtprodn.Select("sr='" + txtsr + " ' ");

            foreach (var row in drr)
                row.Delete();

            dtprodn.AcceptChanges();
            ViewState["dtprod"] = dtprodn;
            if (Request.QueryString["oid"] != null)
            {
                Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
                //   string s = "update Customer_orderproducts set isdelete=1 where oid=" + OrderID + " and pid=" + pid + "";
                string s = "delete from orderproducts where oid=" + OrderID + " and pid=" + rep_txtproductid + "";
                SqlCommand cmd = new SqlCommand(s, con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    #region " Stock Update "
                   // Product_StockAdd(pid, Qty);
                    #endregion " Stock Update "
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Product Remove Successfully');", true);
            con.Close();
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();
        }
        catch (Exception p)
        { }
        finally { con.Close(); }
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
    protected void txttradDis_TextChanged(object sender, EventArgs e)
    {
        if (txttradDis.Text == "")
        {
            txttradDis.Text = "";
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageCustomerOrder.aspx"));
    }

    public void gridDiscount()
    {
        try
        {
           
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            double discount = Convert.ToDouble(txttradDis.Text);
            for (int i = 0; i < dtprodn.Rows.Count; i++)
            {
                double subTotal1 = Convert.ToDouble(dtprodn.Rows[i]["subTotal"].ToString());
                double disamt = (subTotal1 * discount) / 100;
                double grandtot = subTotal1 - disamt;
                dtprodn.Rows[i]["TotalAmount"] = grandtot.ToString();
                //dr["sr"] = rowcnt.ToString();
                //dr["productName"] = ddlProduct.SelectedItem.ToString();
                //dr["pid"] = Convert.ToInt64(ddlProduct.SelectedValue.ToString());
                //dr["brandid"] = Convert.ToString(txtBrand.Text);
                //dr["sizeid"] = Convert.ToString(txtSize.Text);
                //dr["colorid"] = Convert.ToString(txtColor.Text);
                //dr["cart"] = Convert.ToDecimal(txtCart.Text);
                //dr["pack"] = Convert.ToDecimal(txtPack.Text);
                //dr["qty"] = Convert.ToDecimal(txtQty.Text);
                //dr["mrp"] = Convert.ToDecimal(txtMrp.Text);
                //dr["unitRate"] = Convert.ToDecimal(txtUnitRate.Text);
                //dr["subTotal"] = Convert.ToDecimal(txtSubTotal.Text);
                //dr["discount"] = Convert.ToDecimal(txtDiscount.Text);
                //dr["scheme"] = Convert.ToDecimal(txtScheme.Text);
                //dr["taxableamt"] = Convert.ToDecimal(txttaxable.Text);
                //dr["CGSTper"] = Convert.ToDecimal(txtCGST.Text);
                //dr["SGSTper"] = Convert.ToDecimal(txtSgst.Text);
                //dr["IGSTper"] = Convert.ToDecimal(txtIgst.Text);
                //dr["GSTamt"] = Convert.ToDecimal(txtGSTtotal.Text);
                //dr["TotalAmount"] = Convert.ToDecimal(txtTotal.Text);
            }

            dtprodn.AcceptChanges();
            ViewState["dtprod"] = dtprodn;
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();



            //if (rep_txtCart.Text == "")
            //{
            //    rep_txtCart.Text = "0";
            //}
            //double cartqty = Convert.ToDouble(rep_txtCart.Text);
            //double packing = Convert.ToDouble(rep_txtpacking.Text);
            //double unitRate = Convert.ToDouble(rep_txtUnitRate.Text);
            //double gst = Convert.ToDouble(rep_txtCGST.Text) + Convert.ToDouble(rep_txtSGST.Text) + Convert.ToDouble(rep_txtIGST.Text);
            //double totalQty = cartqty * packing;

            
            //double subtotal = totalQty * unitRate;
            //if (rep_txtDiscount.Text == "")
            //{
            //    rep_txtDiscount.Text = "0";
            //}
            //double disAmt = Convert.ToDouble(rep_txtDiscount.Text);
            //double grandtotal = subtotal - disAmt;

            

            //double d1 = subtotal / (gst + 100);
            //double taxableamt = d1 * 100;
           
            //double gstamt = subtotal - taxableamt;
            

            //DataRow[] drr = dtprodn.Select("sr='" + txtsr.Text + " ' ");
            //foreach (var dr in drr)
            //{

               
            //    dr["cart"] = cartqty.ToString();
            //    dr["qty"] = totalQty.ToString();
            //    dr["discount"] = rep_txtDiscount.Text;
            //    dr["subTotal"] = System.Math.Round(subtotal, 2).ToString();
            //    dr["taxableamt"] = System.Math.Round(taxableamt, 2).ToString();
            //    dr["GSTamt"] = System.Math.Round(gstamt, 2).ToString();
            //    dr["TotalAmount"] = System.Math.Round(grandtotal, 2).ToString();


            //}
            

            
        }

        catch { }
    }


    //private bool SendOrderMail(Int64 OrderId)
    //{
    //    common ocommon = new common();
    //    string oSB = string.Empty;
    //    bool send = false;
    //    MailMessage mail = new MailMessage();
    //    mail.To.Add("kshatriya.enterprises@gmail.com");

    //    mail.CC.Add("Acnts.moryatools@gmail.com");
    //    mail.From = new MailAddress("demo@moryatools.com", "Morya Tools App");
    //    mail.Subject = "Customer Generate New Order - InvoiceNo - " + OrderId;
    //    mail.Body = OrderMailCreate(OrderId);
    //    mail.IsBodyHtml = true;
    //    SmtpClient smtp = new SmtpClient();
    //    smtp.Host = "103.250.184.62";
    //    smtp.Port = 25;
    //    smtp.UseDefaultCredentials = false;
    //    smtp.Credentials = new System.Net.NetworkCredential("demo@moryatools.com", "vsys@2017");
    //    try
    //    {
    //        smtp.Send(mail);
    //        send = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        send = false;
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    return send;
    //}

    //private string OrderMailCreate(Int64 OrderId)
    //{
    //    common ocommon = new common();
    //    string OrderLink = "http://moryaapp.moryatools.com/orderinvoice.aspx?oid=" + ocommon.Encrypt(OrderId.ToString(), true);
    //    string oSB = string.Empty;
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        //cmd.CommandText = "order_invoice";
    //        cmd.CommandText = "order_invoice1";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@oid", OrderId);
    //        cmd.Connection = con;
    //        con.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);
    //        oSB += "<div>Hello Admin,</div";
    //        if (ds.Tables != null)
    //        {
    //            if (ds.Tables[2].Rows.Count > 0)
    //            {
    //                oSB += "<br/>";
    //                oSB += "<table><tr><td><b><u>Customer Details - </u></b></td></tr><tr><td> Name - " + ds.Tables[2].Rows[0]["name"].ToString() + "</td></tr><tr><td>Phone: <span>" + ds.Tables[2].Rows[0]["phone"].ToString() + "</span></td></tr><tr><td>GST NO: <span>" + ds.Tables[2].Rows[0]["gstno"].ToString() + "</span></td></tr><tr><td>Address: <span>" + ds.Tables[2].Rows[0]["address"].ToString() + "</span></td></tr><tr><td>Email: <span>" + ds.Tables[2].Rows[0]["email"].ToString() + "</span></td></tr></table>";
    //                oSB += "<hr/>";
    //            }

    //            if (ds.Tables[0] != null)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    oSB += "<br/>";
    //                    oSB += "<table><tr><td><b><u>Order Details -</u></b></td></tr><tr><td> Invoice No - #" + ds.Tables[0].Rows[0]["oid"].ToString() + "</td></tr><tr><td>Order Date: <span>" + ds.Tables[0].Rows[0]["orderdate"].ToString() + "</span></td></tr><tr><td>Total Amount: <span>" + ds.Tables[0].Rows[0]["totalamount"].ToString() + "</span></td></tr></table>";
    //                    oSB += "<hr/>";
    //                }
    //            }

    //            if (ds.Tables[1].Rows.Count > 0)
    //            {
    //                oSB += "<br/>";
    //                oSB += "<table><tr><td><b><u>Order Products Details - </u></b></td></tr></table>";
    //                oSB += "<br/>";
    //                oSB += "<table style='border: 1px solid black'><thead><tr style='border: 1px solid black'><th style='border: 1px solid black'>Product Name</th><th style='border: 1px solid black'>SKU</th><th style='text-align: center;border: 1px solid black'>Product Price</th><th style='text-align: center;border: 1px solid black'>Quantites</th><th style='text-align: center;border: 1px solid black'>GST</th><th style='text-align: center;border: 1px solid black'>Product Basic Price</th><th style='text-align: center;border: 1px solid black'>Product Total Price</th></tr></thead><tbody>";
    //                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
    //                {
    //                    oSB += "<tr style='border: 1px solid black'>";
    //                    oSB += "<td style='border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["productname"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["sku"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["productprice"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["quantites"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["gst"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["ProductBasicPrice"].ToString() + "</span></td>";
    //                    oSB += "<td style='text-align: center;border: 1px solid black'><span>" + ds.Tables[1].Rows[i]["producttotalprice"].ToString() + "</span></td>";
    //                    oSB += "</tr>";
    //                }
    //                oSB += "</tbody></table>";
    //                oSB += "<br/>";
    //                oSB += "<b><u>Website Page Link -</u>  <a href=" + OrderLink + ">" + OrderLink + "</a></b>";
    //                oSB += "<br/>";
    //                oSB += "<br/>";
    //                oSB += "<hr/>";
    //                oSB += "<div>Thank you,</div>";
    //                oSB += "<div>Morya App - Support Team.</div>";
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //        return null;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //    return oSB;
    //}


    protected void ddlname_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlPaymentType_TextChanged(object sender, EventArgs e)
    {
        ddlinvoiceType.Focus();
    }

    protected void ddlinvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdoCash.Focus();
    }
}

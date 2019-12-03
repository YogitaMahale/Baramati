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
public partial class addeditponew : System.Web.UI.Page
{
    common ocommon = new common();
    Int64 addbtnClick = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {


        txt_Date.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);
        txtStockDate.Text = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:d/M/yyyy}", DateTime.Now);

        //-------------------------------------------------
        if (!IsPostBack)
        {
            ddlname.Focus();

            ddlinvoiceType.SelectedValue = "Including GST";
            BindRepeater();
            Bindvendor();
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
    public void Bindvendor()
    {
        try
        {
            //if (ddlUserType.SelectedItem.Value == "Dealer")
            //{
            Cls_VendorMaster_b obj = new Cls_VendorMaster_b();
            DataTable dtVendor = new DataTable();
            dtVendor = obj.SelectAll();
            if (dtVendor.Rows.Count > 0)
            {
                ddlname.DataSource = dtVendor;
                ddlname.DataTextField = "vendorName";
                ddlname.DataValueField = "vid";
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
        string GetProduct = "select distinct id,productname from tbl_RawMaterialRegi";
        SqlDataAdapter daprodcut = new SqlDataAdapter(GetProduct, con);
        DataTable dtrpoduct = new DataTable();
        daprodcut.Fill(dtrpoduct);
        if (dtrpoduct.Rows.Count > 0)
        {
            ddlProduct.DataSource = dtrpoduct;
            ddlProduct.DataTextField = "productname";
            ddlProduct.DataValueField = "id";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, "---Select----");
        }
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

         

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
        ddlinvoiceType_SelectedIndexChanged(null, null);
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
                cmd.CommandText = "purchasegrid_byproductId";
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
                    txtQty.Text = "0";
                    txtRate.Text = dt.Rows[0]["MRP"].ToString();
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtScheme.Text = "0";
                    txtFrieghtAmt.Text = "0";
                    txttaxable.Text = "0";
                    decimal cgst1 = Convert.ToDecimal(dt.Rows[0]["CGST"].ToString());
                    txtCGST.Text = System.Math.Round(cgst1, 2).ToString();
                    decimal sgst1 = Convert.ToDecimal(dt.Rows[0]["SGST"].ToString());
                    txtSgst.Text = System.Math.Round(sgst1, 2).ToString();
                    decimal Igst1 = Convert.ToDecimal(dt.Rows[0]["IGST"].ToString());
                    txtIgst.Text = System.Math.Round(Igst1, 2).ToString();


                    txtGSTtotal.Text = "0";
                    txtTotal.Text = "0";
                    txtNetRate.Text = dt.Rows[0]["MRP"].ToString();


                }
                con.Close();
                //salegrid_byproductId
            }
        }
        catch { }

    }
    protected void txtCart_TextChanged(object sender, EventArgs e)
    {
        

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

            txt_Subtotal.Text = t_subtotal.ToString();

            txttaxableAmt.Text = t_taxableamt.ToString();

            if (t_CGST > 0 && t_SGST > 0)
            {
                txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
                txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
                txtIgstfinal.Text = "0";
            }
            else if (t_IGST > 0 )
            {
                txtcsgtfinal.Text = (t_totalGSTamt / 2).ToString();
                txtsgstfinal.Text = (t_totalGSTamt / 2).ToString();
                txtIgstfinal.Text = "0";
            }




            txttotalAmt.Text = t_grandTotal.ToString();

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
            decimal final11 = Convert.ToDecimal(t_grandTotal) - Convert.ToDecimal(txttransport.Text) - Convert.ToDecimal(txtpacking.Text) - Convert.ToDecimal(txtotherAmt.Text) - Convert.ToDecimal(txtFriegtAmt.Text); 
            txttotalAmtfinal.Text = System.Math.Round(final11, 2).ToString();


             

        }
        catch { }

        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {



    }
    private void BindRepeater()
    {

        string productdata = "select '' as sr,'' as productName,pid, qty, rate, subtotal, discount, scheme, frieghtamt, taxableamt, csgtper, sgstper, igstper, gstamt, total, netrate  from PurchaseOrderDetails where oid=-1";
        SqlDataAdapter daproduct = new SqlDataAdapter(productdata, con);
        DataTable dtprod = new DataTable();
        daproduct.Fill(dtprod);
        Repeater1.DataSource = dtprod;
        Repeater1.DataBind();
        ViewState["dtprod"] = dtprod;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Repeater1.Items.Count == 0 || ddlname.SelectedIndex == 0)
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
                PurchaseOrderHeader objorders = new PurchaseOrderHeader();

                objorders.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());

                objorders.invoicetype = Convert.ToString(ddlinvoiceType.SelectedValue.ToString());
                objorders.invoiceno = Convert.ToString(txt_InvoieNo.Text);
                objorders.accountYear = Convert.ToString(ddlaccountYear.SelectedItem.ToString());

                string paymentmode1 = "";
                if (rdoCash.Checked)
                {
                    paymentmode1 = "Cash";
                }
                else //else if(rdoCredit.Checked)
                {
                    paymentmode1 = "Credit";
                }



                objorders.orderdate = Convert.ToDateTime(txt_Date.Text);
                objorders.stockdate = Convert.ToDateTime(txtStockDate.Text);
                objorders.subtotal = Convert.ToDecimal(txt_Subtotal.Text);
                objorders.discandScheme = Convert.ToDecimal(txttradDis.Text);
                objorders.frieghtamount = Convert.ToDecimal(txtFriegtAmt.Text);
                objorders.taxableamount = Convert.ToDecimal(txttaxableAmt.Text);
                objorders.CGSTamt = Convert.ToDecimal(txtcsgtfinal.Text);
                objorders.SGSTamt = Convert.ToDecimal(txtsgstfinal.Text);

                objorders.IGSTamt = Convert.ToDecimal(txtIgstfinal.Text);
                objorders.totalAmt = Convert.ToDecimal(txttotalAmt.Text);
                objorders.transportamt = Convert.ToDecimal(txttransport.Text);
                objorders.packingamt = Convert.ToDecimal(txtpacking.Text);

                objorders.otheramt = Convert.ToDecimal(txtotherAmt.Text);

                objorders.grandtotal = Convert.ToDecimal(txttotalAmtfinal.Text);
                objorders.pendingAmt = Convert.ToDecimal(txttotalAmtfinal.Text);
                objorders.stockdate = Convert.ToDateTime(txtStockDate.Text);
                objorders.dicountamt = Convert.ToDecimal("0");
                Int64 OrderProductAdd = 0;

                DataTable ds = new DataTable();
                ds = (DataTable)ViewState["dtprod"];
                if (Request.QueryString["oid"] == null)
                {

                    #region
                    if (ds != null)
                    {
                        if (ds.Rows.Count > 0)
                        {
                            OrderId = (new clsPurchaseOrderHeader_b().Insert(objorders));
                            if (OrderId > 0)
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    OrderProductAdd = 0;

                                    PurchaseOrderDetails objorderproducts = new PurchaseOrderDetails();
                                    objorderproducts.oid = OrderId;
                                    objorderproducts.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());
                                    objorderproducts.pid = Convert.ToInt64(ds.Rows[i]["pid"]);
                                    objorderproducts.qty = Convert.ToInt64(ds.Rows[i]["qty"]);
                                    objorderproducts.rate = Convert.ToDecimal(ds.Rows[i]["rate"]);
                                    objorderproducts.subtotal = Convert.ToDecimal(ds.Rows[i]["subtotal"]);
                                    objorderproducts.discount = Convert.ToDecimal(ds.Rows[i]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Rows[i]["scheme"]);
                                    objorderproducts.frieghtamt = Convert.ToDecimal(ds.Rows[i]["frieghtamt"]);
                                    objorderproducts.taxableamt = Convert.ToDecimal(ds.Rows[i]["taxableamt"]);
                                    objorderproducts.csgtper = Convert.ToDecimal(ds.Rows[i]["csgtper"]);
                                    objorderproducts.sgstper = Convert.ToDecimal(ds.Rows[i]["sgstper"]);

                                    objorderproducts.igstper = Convert.ToDecimal(ds.Rows[i]["igstper"]);
                                    objorderproducts.gstamt = Convert.ToDecimal(ds.Rows[i]["gstamt"]);
                                    objorderproducts.total = Convert.ToDecimal(ds.Rows[i]["total"]);

                                    objorderproducts.netrate = Convert.ToDecimal(ds.Rows[i]["netrate"]);



                                    OrderProductAdd = (new Cls_PurchaseOrderDetails_b().Insert(objorderproducts));
                                    #region " Stock Update "
                                    //  Product_StockUpdate(objorderproducts.pid, objorderproducts.quantites);
                                    #endregion " Stock Update "

                                }
                            }
                        }
                    }
                    if (OrderId > 0)
                    {
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

                    //update
                    #region

                    if (ds != null)
                    {
                        if (ds.Rows.Count > 0)
                        {
                            objorders.oid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
                            OrderId = (new clsPurchaseOrderHeader_b().Update(objorders));
                            if (OrderId > 0)
                            {
                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    OrderProductAdd = 0;

                                    PurchaseOrderDetails objorderproducts = new PurchaseOrderDetails();
                                    objorderproducts.oid = OrderId;
                                    objorderproducts.uid = Convert.ToInt64(ddlname.SelectedValue.ToString());
                                    objorderproducts.pid = Convert.ToInt64(ds.Rows[i]["pid"]);
                                    objorderproducts.qty = Convert.ToInt64(ds.Rows[i]["qty"]);
                                    objorderproducts.rate = Convert.ToDecimal(ds.Rows[i]["rate"]);
                                    objorderproducts.subtotal = Convert.ToDecimal(ds.Rows[i]["subtotal"]);
                                    objorderproducts.discount = Convert.ToDecimal(ds.Rows[i]["discount"]);
                                    objorderproducts.scheme = Convert.ToDecimal(ds.Rows[i]["scheme"]);
                                    objorderproducts.frieghtamt = Convert.ToDecimal(ds.Rows[i]["frieghtamt"]);
                                    objorderproducts.taxableamt = Convert.ToDecimal(ds.Rows[i]["taxableamt"]);
                                    objorderproducts.csgtper = Convert.ToDecimal(ds.Rows[i]["csgtper"]);
                                    objorderproducts.sgstper = Convert.ToDecimal(ds.Rows[i]["sgstper"]);

                                    objorderproducts.igstper = Convert.ToDecimal(ds.Rows[i]["igstper"]);
                                    objorderproducts.gstamt = Convert.ToDecimal(ds.Rows[i]["gstamt"]);
                                    objorderproducts.total = Convert.ToDecimal(ds.Rows[i]["total"]);

                                    objorderproducts.netrate = Convert.ToDecimal(ds.Rows[i]["netrate"]);


                                    try
                                    {
                                        con.Close();
                                        con.Open();
                                        string sel = "select * from PurchaseOrderDetails  where oid=" + OrderId + " and pid=" + Convert.ToInt64(ds.Rows[i]["pid"]) + "";
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

                                            OrderProductAdd = (new Cls_PurchaseOrderDetails_b().Update(objorderproducts));
                                        }
                                        else
                                        {
                                            con.Close();
                                            sel_dr.Close();
                                            OrderProductAdd = (new Cls_PurchaseOrderDetails_b().Insert(objorderproducts));

                                        }
                                    }
                                    catch (Exception p)
                                    { }
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






                ///*****************
                //string s_update = "update Customer_orders set isCreateInvoice=1,ConfirmedInvoiceId='" + OrderId + "' where oid=" + OrderId_old;
                //SqlCommand cmd_update = new SqlCommand(s_update, con);
                //tt = cmd_update.ExecuteNonQuery();

                ////--------------------------




            }
            catch (Exception p)
            { }
            finally { con.Close(); }

            if (OrderId > 0)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Order insert Successfully ');", true);

                clear();
                Response.Redirect(Page.ResolveUrl("~/managepurchaseordernew.aspx?mode=i"));
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

        ddlname.SelectedIndex = 0;
        ddlPaymentType.SelectedIndex = 0;
        //ddlinvoiceType
        txt_InvoieNo.Text = string.Empty;
        //rdoCredit.che
        //rdoCash
        //txt_Date
        //ddlaccountYear.sel
        //txtStockDate
        txt_Subtotal.Text = "0";
        txttradDis.Text = "0";
        txtFriegtAmt.Text = "0";
        txttaxableAmt.Text = "0";
        txtcsgtfinal.Text = "0";
        txtsgstfinal.Text = "0";
        txtIgstfinal.Text = "0";
        txttotalAmt.Text = "0";
        txttransport.Text = "0";
        txtpacking.Text = "0";
        txtotherAmt.Text = "0";
        txtdiscountamt.Text = "0";
        txttotalAmtfinal.Text = "0";
        DataTable dtprodn = new DataTable();
        dtprodn = (DataTable)ViewState["dtprod"];
        dtprodn.Rows.Clear();
        Repeater1.DataSource = dtprodn;
        Repeater1.DataBind();
        ViewState["dtprod"] = dtprodn;

    }
    public void BindOrders(Int64 Oid)
    {

        try
        {
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Display_PurchaseOrder";
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

                ddlinvoiceType.SelectedValue = ds.Tables[0].Rows[0]["invoicetype"].ToString();
                txt_InvoieNo.Text = ds.Tables[0].Rows[0]["invoiceno"].ToString();
                //if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Cash")
                //{
                //    rdoCash.Checked = true;
                //    rdoCredit.Checked = false;

                //}
                //else if (ds.Tables[0].Rows[0]["orderno"].ToString() == "Credit")
                //{
                //    rdoCash.Checked = false;
                //    rdoCredit.Checked = true;
                //}
                ddlaccountYear.SelectedValue = ds.Tables[0].Rows[0]["accountYear"].ToString();

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
                txtStockDate.Text = ds.Tables[0].Rows[0]["stockdate"].ToString();


                DataTable dtprodn = new DataTable();
                dtprodn = (DataTable)ViewState["dtprod"];
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
            TextBox rep_txtQty = (TextBox)row.FindControl("rep_txtQty");
            Label  rep_txtRate = (Label)row.FindControl("rep_txtRate");
            Label rep_txtSubTotal = (Label)row.FindControl("rep_txtSubTotal");
            TextBox rep_txtDiscount = (TextBox)row.FindControl("rep_txtDiscount");

            Label rep_txtScheme = (Label)row.FindControl("rep_txtScheme");
            Label rep_txtFrieghtAmt = (Label)row.FindControl("rep_txtFrieghtAmt");
            Label rep_txttaxable = (Label)row.FindControl("rep_txttaxable");
            Label rep_txtCGST = (Label)row.FindControl("rep_txtCGST");
            Label rep_txtSgst = (Label)row.FindControl("rep_txtSgst");
            Label rep_txtIgst = (Label)row.FindControl("rep_txtIgst");

            Label rep_txtGSTtotal = (Label)row.FindControl("rep_txtGSTtotal");
            Label rep_txtTotal = (Label)row.FindControl("rep_txtTotal");
            Label rep_txtNetRate = (Label)row.FindControl("rep_txtNetRate");




            if (rep_txtDiscount.Text == "")
            {
                rep_txtDiscount.Text = "0";
            }

            if (rep_txtQty.Text == "")
            {
                rep_txtQty.Text = "0";
            }

            decimal qty = Convert.ToDecimal(rep_txtQty.Text);
            decimal rate = Convert.ToDecimal(rep_txtRate.Text);
            decimal discount = Convert.ToDecimal(rep_txtDiscount.Text);
            decimal subtotal = qty * rate;

            decimal disamt = (subtotal * discount) / 100;
            decimal tot1 = subtotal - disamt;
            decimal gst = Convert.ToDecimal(rep_txtCGST.Text) + Convert.ToDecimal(rep_txtSgst.Text) + Convert.ToDecimal(rep_txtIgst.Text);

            decimal taxable = (tot1 / (gst + 100)) * 100;
           // txttaxable.Text = System.Math.Round(taxable, 2).ToString();
            string finalTaxableamt = System.Math.Round(taxable, 2).ToString();

            decimal gstamt = (taxable * gst) / 100;
            string finalGSTamt = gstamt.ToString();
            string FinalTotal = "";
           // txtGSTtotal.Text = System.Math.Round(gstamt, 2).ToString();
            if (string.Equals("Excluding GST", ddlinvoiceType.SelectedValue.ToString()))
            {
                FinalTotal = (tot1 + gstamt).ToString();

            }
            else if (string.Equals("Including GST", ddlinvoiceType.SelectedValue.ToString()))
            {
                FinalTotal = (tot1).ToString();
            }
           

            DataRow[] drr = dtprodn.Select("sr='" + txtsr.Text + " ' ");
            foreach (var dr in drr)
            {

                dr["qty"] = rep_txtQty.Text.ToString();

                dr["discount"] = rep_txtDiscount.Text;
                dr["subtotal"] = System.Math.Round(subtotal, 2).ToString();
                dr["taxableamt"] = System.Math.Round(Convert.ToDecimal(finalTaxableamt), 2).ToString();
                dr["gstamt"] = System.Math.Round(gstamt, 2).ToString();
                dr["total"] = System.Math.Round(Convert.ToDecimal(FinalTotal), 2).ToString();


            }
            // dtProduct.Rows.RemoveAt(prodid);

            dtprodn.AcceptChanges();
            ViewState["dtprod"] = dtprodn;
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();
        }

        catch { }
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

            Int64 rep_txtproductid = int.Parse((item.FindControl("rep_txtproductid") as Label).Text);
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
                string s = "delete from PurchaseOrderDetails where oid=" + OrderID + " and pid=" + rep_txtproductid + "";
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
        //if (txttaxableDis.Text == "")
        //{
        //    txttaxableDis.Text = "0";
        //}
        //gridTotal();
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
        //if (txtotherAmt.Text == "")
        //{
        //    txtotherAmt.Text = "0";
        //}
        //gridTotal();
    }
    protected void txtfreightdis_TextChanged(object sender, EventArgs e)
    {
        //if (txtfreightdis.Text == "")
        //{
        //    txtfreightdis.Text = "0";
        //}
        //gridTotal();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managepurchaseordernew.aspx"));
    }



    protected void txttransport_TextChanged(object sender, EventArgs e)
    {
        gridTotal();
    }

    protected void txtpacking_TextChanged(object sender, EventArgs e)
    {
        gridTotal();

    }

    protected void txtotherAmt_TextChanged1(object sender, EventArgs e)
    {
        gridTotal();
    }

    protected void txtdiscountamt_TextChanged(object sender, EventArgs e)
    {
        gridTotal();

    }

    //protected void txtQty_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtDiscount.Text == "")
    //        {
    //            txtDiscount.Text = "0";
    //        }

    //        if (txtQty.Text == "")
    //        {
    //            txtQty.Text = "0";
    //        }

    //        decimal qty = Convert.ToDecimal(txtQty.Text);
    //        decimal rate = Convert.ToDecimal(txtRate.Text);
    //        decimal discount = Convert.ToDecimal(txtDiscount.Text);
    //        decimal subtotal = qty * rate;
    //        txtSubTotal.Text = subtotal.ToString();
    //        decimal disamt = (subtotal * discount) / 100;
    //        decimal tot1 = subtotal - disamt;



    //        decimal gst = Convert.ToDecimal(txtCGST.Text) + Convert.ToDecimal(txtSgst.Text) + Convert.ToDecimal(txtIgst.Text);
    //        decimal d1 = subtotal / (gst + 100);
    //        decimal taxableamt = d1 * 100;
    //        txttaxable.Text = System.Math.Round(taxableamt, 2).ToString();

    //        decimal gstamt = (Convert.ToDecimal(txtSubTotal.Text) * gst) / 100;
    //        txtGSTtotal.Text = gstamt.ToString();
    //        if (string.Equals("Excluding GST", ddlinvoiceType.SelectedValue.ToString()))
    //        {
    //            txtTotal.Text = (tot1 + gstamt).ToString();

    //        }
    //        else if (string.Equals("Including GST", ddlinvoiceType.SelectedValue.ToString()))
    //        {
    //            txtTotal.Text = tot1.ToString();
    //        }



    //    }
    //    catch { }
    //}





    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDiscount.Text == "")
            {
                txtDiscount.Text = "0";
            }

            if (txtQty.Text == "")
            {
                txtQty.Text = "0";
            }

            decimal qty = Convert.ToDecimal(txtQty.Text);
            decimal rate = Convert.ToDecimal(txtRate.Text);
            decimal discount = Convert.ToDecimal(txtDiscount.Text);
            decimal subtotal = qty * rate;
            txtSubTotal.Text = subtotal.ToString();

            decimal disamt = (subtotal * discount) / 100;
            decimal tot1 = subtotal - disamt;
            decimal gst = Convert.ToDecimal(txtCGST.Text) + Convert.ToDecimal(txtSgst.Text) + Convert.ToDecimal(txtIgst.Text);

            decimal taxable = (tot1 / (gst + 100)) * 100;
            txttaxable.Text = System.Math.Round(taxable, 2).ToString();


            decimal gstamt = (taxable * gst) / 100;
            
           
            txtGSTtotal.Text = System.Math.Round(gstamt, 2).ToString();
            if (string.Equals("Excluding GST", ddlinvoiceType.SelectedValue.ToString()))
            {
                txtTotal.Text = System.Math.Round((tot1 + gstamt), 2).ToString();  

            }
            else if (string.Equals("Including GST", ddlinvoiceType.SelectedValue.ToString()))
            {
                txtTotal.Text = System.Math.Round(tot1, 2).ToString();  
            }



        }
        catch { }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {


            if (ddlProduct.SelectedIndex == 0 || txtQty.Text == string.Empty)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Enter Proper Value ');", true);
                //return;
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
                dr["qty"] = Convert.ToString(txtQty.Text);
                dr["rate"] = Convert.ToString(txtRate.Text);
                dr["subtotal"] = Convert.ToString(txtSubTotal.Text);
                dr["discount"] = Convert.ToDecimal(txtDiscount.Text);
                dr["scheme"] = Convert.ToDecimal(txtScheme.Text);
                dr["frieghtamt"] = Convert.ToDecimal(txtFrieghtAmt.Text);
                dr["taxableamt"] = Convert.ToDecimal(txttaxable.Text);
                dr["csgtper"] = Convert.ToDecimal(txtCGST.Text);
                dr["sgstper"] = Convert.ToDecimal(txtSgst.Text);
                dr["igstper"] = Convert.ToDecimal(txtIgst.Text);
                dr["gstamt"] = Convert.ToDecimal(txtGSTtotal.Text);
                dr["total"] = Convert.ToDecimal(txtTotal.Text);
                dr["netrate"] = Convert.ToDecimal(txtNetRate.Text);




                dtprodn.Rows.Add(dr);
                Repeater1.DataSource = dtprodn;
                Repeater1.DataBind();
                ViewState["dtprod"] = dtprodn;


                txtQty.Text = "0";
                txtRate.Text = "0";
                txtSubTotal.Text = "0";
                txtDiscount.Text = "0";
                txtScheme.Text = "0";
                txtFrieghtAmt.Text = "0";
                txttaxable.Text = "0";
                txtCGST.Text = "0";
                txtSgst.Text = "0";
                txtIgst.Text = "0";
                txtGSTtotal.Text = "0";
                txtTotal.Text = "0";
                txtNetRate.Text = "0";
                ddlProduct.SelectedIndex = 0;

                gridTotal();

            }
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }

    protected void txtFriegtAmt_TextChanged(object sender, EventArgs e)
    {
        if(txtFriegtAmt.Text=="")
        {
            txtFriegtAmt.Text = "0";
        }
        gridTotal();
    }

    protected void ddlinvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtQty.Text = "0";
            txtRate.Text = "0";
            txtSubTotal.Text = "0";
            txtDiscount.Text = "0";
            txtScheme.Text = "0";
            txtFrieghtAmt.Text = "0";
            txttaxable.Text = "0";
            txtCGST.Text = "0";
            txtSgst.Text = "0";
            txtIgst.Text = "0";
            txtGSTtotal.Text = "0";
            txtTotal.Text = "0";
            txtNetRate.Text = "0";
            ddlProduct.SelectedIndex = 0;

            txt_Subtotal.Text = "0";
            txttradDis.Text = "0";
            txtFriegtAmt.Text = "0";
            txttaxableAmt.Text = "0";
            txtcsgtfinal.Text = "0";
            txtsgstfinal.Text = "0";
            txtIgstfinal.Text = "0";
            txttotalAmt.Text = "0";
            txttransport.Text = "0";
            txtpacking.Text = "0";
            txtotherAmt.Text = "0";
            txtdiscountamt.Text = "0";
            txttotalAmtfinal.Text = "0";
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            dtprodn.Rows.Clear();
            Repeater1.DataSource = dtprodn;
            Repeater1.DataBind();
            ViewState["dtprod"] = dtprodn;
        }
        catch { }
    }
}

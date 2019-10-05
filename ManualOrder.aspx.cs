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

public partial class ManualOrder : System.Web.UI.Page
{
    int productImageFrontWidth = 500;
    int productImageFrontHeight = 226;
    string productMainPath = "~/uploads/product/";
    string productFrontPath = "~/uploads/product/front/";
    common ocommon = new common();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    DataTable dtprod = new DataTable();
    double TotQty = 0.0, AmtBfrTax = 0.0, totIGSTamt, totCGSTamt = 0.0, totSGSTamt = 0.0, totdisc = 0.0;
    string InsertDetailTable = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindProducts();
            BindCategory();
            txtOrderDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            Int64 Oid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
            // Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)
            if (Request.QueryString["oid"] != null)
            {

                ddlUserType.Enabled = false;
                ddlname.Enabled = false;
                BindOrders(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
                btnSave.Text = "Update";
                hPageTitle.InnerText = "Product Update";
                Page.Title = "Product Update";
            }
            else
            {
                //hPageTitle.InnerText = "Product Add";
                //Page.Title = "Product Add";
            }
        }
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
                if (ds.Tables[0].Rows[0]["UserType"].ToString().ToUpper().Trim() == "D".ToUpper().Trim())
                {
                    ddlUserType.SelectedIndex = 1;
                }
                else
                {

                    ddlUserType.SelectedIndex = 2;
                }
                ddlUserType_SelectedIndexChanged(null, null);
                ddlname.SelectedValue = ds.Tables[0].Rows[0]["uid"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["orderdate"].ToString();
                ddlname_SelectedIndexChanged(null, null);
                //txtAddress.Text = ds_order.Rows[0][""].ToString();
                //txtemail.Text = ds_order.Rows[0][""].ToString();
                //txtPhone.Text = ds_order.Rows[0][""].ToString();
                //txtcountry.Text = ds_order.Rows[0][""].ToString();
                //txtstate.Text = ds_order.Rows[0][""].ToString();
                //txtcity.Text = ds_order.Rows[0][""].ToString();
                //txtGstNo.Text = ds_order.Rows[0][""].ToString();

                //lbldiscount.Text = ds.Tables[0].Rows[0]["discount"].ToString();
                //lbltotqtyval.Text = ds.Tables[0].Rows[0]["productquantites"].ToString();
                //lbltotamtbfrtax.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                //lbltotalamtaftertax.Text = ds.Tables[0].Rows[0]["totalamount"].ToString();
                //lblIGSTtot.Text = ds.Tables[0].Rows[0]["tax"].ToString();
                //lblCGSTtot.Text = ds.Tables[0].Rows[0]["tax"].ToString();
                //lblSGSTtot.Text = ds.Tables[0].Rows[0]["tax"].ToString();

                ////string UserStateGST = "";
                ////if (ds.Tables[2] != null)
                ////{
                ////    string adminState = Session["AdminState"].ToString();
                ////    string UserState = ds.Tables[2].Rows[0]["StateID"].ToString();
                ////    if (adminState.ToString().Trim() == UserState.ToString().Trim())
                ////    {
                ////        UserStateGST = "CGST";
                ////    }
                ////    else
                ////    {
                ////        UserStateGST = "IGST";
                ////    }
                ////}



                DataTable dtprodn = new DataTable();
                dtprodn = (DataTable)ViewState["dtprod"];
                dtprodn.Rows.Clear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtprodn.NewRow();
                    dr["pid"] = ds.Tables[1].Rows[i]["pid"].ToString();
                    dr["productprice"] = ds.Tables[1].Rows[i]["productprice"].ToString();
                    //dr["discount"] = ds.Tables[1].Rows[i]["discount"].ToString();
                    //dr["productafterdiscountprice"] = ds.Tables[1].Rows[i]["productafterdiscountprice"].ToString();
                    dr["quantites"] = ds.Tables[1].Rows[i]["quantites"].ToString();
                    dr["producttotalprice"] = ds.Tables[1].Rows[i]["producttotalprice"].ToString();
                    //dr["producttotalprice"] = ds.Tables[1].Rows[i]["producttotalprice"].ToString();
                    dr["gst"] = ds.Tables[1].Rows[i]["gst"].ToString();
                    dr["Productname"] = ds.Tables[1].Rows[i]["productname"].ToString();


                    //if (UserStateGST.ToString().ToUpper().Trim() == "IGST".ToString().Trim())
                    //{

                    //string ss = dr["producttotalprice"].ToString();
                    //double amtt1 = Convert.ToDouble(ss) * Convert.ToDouble(ds.Tables[1].Rows[i]["gst"].ToString());
                    //string amt = (amtt1 / 100).ToString();

                    //// string amt =Math.Round( ((Convert.ToDouble(ds.Tables[1].Rows[i]["producttotalprice"].ToString()) * Convert.ToDouble(ds.Tables[1].Rows[i]["gst"].ToString()))/100),2).ToString();
                    //dr["IGSTAmt"] = amt;
                    //dr["CGSTAmt"] = "0";
                    //dr["SGSTAmt"] = "0";
                    //dr["IGSTper"] = ds.Tables[1].Rows[i]["gst"].ToString();
                    //}
                    //else if (UserStateGST.ToString().ToUpper().Trim() == "CGST".ToString().Trim())
                    //{
                    //    string ss = dr["producttotalprice"].ToString();
                    //    double amtt1 = Convert.ToDouble(ss) * Convert.ToDouble(ds.Tables[1].Rows[i]["gst"].ToString());
                    //    string amt = (amtt1 / 100).ToString();

                    //    //  string amt = Math.Round(((Convert.ToDouble(ds.Tables[1].Rows[i]["producttotalprice"].ToString()) * Convert.ToDouble(ds.Tables[1].Rows[i]["gst"].ToString())) / 100), 2).ToString();
                    //    dr["IGSTAmt"] = "0";
                    //    dr["CGSTAmt"] = (double.Parse(amt) / 2).ToString();
                    //    dr["SGSTAmt"] = (double.Parse(amt) / 2).ToString();
                    //    dr["IGSTper"] = "0";
                    //}




                    dtprodn.Rows.Add(dr);
                }

                gvproduct.DataSource = dtprodn;
                gvproduct.DataBind();
                ViewState["dtprod"] = dtprodn;
                GridTotals();


            }


            con.Close();
        }
        catch { }
        finally { con.Close(); }

    }

    public void clear()
    {
        ddlUserType.Enabled = true;
        ddlname.Enabled = true;

        ddlname.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
        txtemail.Text = String.Empty;
        txtPhone.Text = String.Empty;
        txtAddress.Text = String.Empty;
        //txtGstNo.Text = String.Empty;
        gvproduct.DataSource = "";
        gvproduct.DataBind();
        detailpartclear();
        //lbltotqtyval.Text = "0.0";
        //lbltotamtbfrtax.Text = "0.0";
        lbltotalamtaftertax.Text = "0.0";
        //lblCGSTtot.Text = "0.0";
        //lblIGSTtot.Text = "0.0";
        //lblSGSTtot.Text = "0.0";
        //lbldiscount.Text = "0.0";
        //lbltotalQty0.Text = "0.0";
        lblQty.Text = "0";
        ddlname.SelectedIndex = 0;

        DataTable dtprodn = new DataTable();
        dtprodn = (DataTable)ViewState["dtprod"];
        dtprodn.Rows.Clear();
        gvproduct.DataSource = dtprodn;
        gvproduct.DataBind();
        ViewState["dtprod"] = dtprodn;



    }

    public void detailpartclear()
    {
        txtPrice.Text = string.Empty;
        //  txt_discountAmt.Text = string.Empty;
        //txtdiscounted.Text = string.Empty;
        txtQty.Text = string.Empty;
        txttaxabletotal.Text = string.Empty;
        // txtIGSTper.Text = string.Empty;
        // txtCGSTper.Text = string.Empty;
        // txtSGSTper.Text = string.Empty;
        // txtIGSTAmt.Text = string.Empty;
        //txtCGSTAmt.Text = string.Empty;
        //txtSGSTAmt.Text = string.Empty;
        ddlProduct.SelectedIndex = 0;
    }
    public void BindProducts()
    {
        string GetProduct = "select distinct pid,productname from product";
        SqlDataAdapter daprodcut = new SqlDataAdapter(GetProduct, con);
        DataTable dtrpoduct = new DataTable();
        daprodcut.Fill(dtrpoduct);
        if (dtrpoduct.Rows.Count > 0)
        {
            ddlProduct.DataSource = dtrpoduct;
            ddlProduct.DataTextField = "productname";
            ddlProduct.DataValueField = "pid";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, "---Select----");
        }
    }

    private void BindCategory()
    {
        //string productdata = "select pid,productprice,discount,productafterdiscountprice,quantites,producttotalprice,gst,'' as Productname,'' as IGSTAmt,'' as CGSTAmt,'' as SGSTAmt ,'' as IGSTper,'' as NoOfBox"
        //                                    + "  from orderproducts where oid = -1";
        string productdata = "select pid,productprice,quantites,producttotalprice,gst,'' as Productname "
                                           + "  from orderproducts where oid = -1";


        SqlDataAdapter daproduct = new SqlDataAdapter(productdata, con);
        DataTable dtprod = new DataTable();
        daproduct.Fill(dtprod);
        gvproduct.DataSource = dtprod;
        gvproduct.DataBind();
        ViewState["dtprod"] = dtprod;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {

    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["page"].ToString() == "price")
        {
            Response.Redirect(Page.ResolveUrl("~/manageproductprice.aspx"));
        }
        else if (Request.QueryString["page"].ToString() == "stock")
        {
            Response.Redirect(Page.ResolveUrl("~/manageproductstock.aspx"));
        }
        else
        {
            Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx"));
        }
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
    }

    protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CustmerState = "";
            if (ddlUserType.SelectedItem.Value == "Dealer")
            {
                string GetdealerData = "select name,userloginmobileno,email,gstno,address1+''+address2 as dadd from dealermaster  where did=" + ddlname.SelectedValue + "";
                SqlDataAdapter dadealer = new SqlDataAdapter(GetdealerData, con);
                DataTable dtdealer = new DataTable();
                dadealer.Fill(dtdealer);
                if (dtdealer.Rows.Count > 0)
                {
                    txtAddress.Text = dtdealer.Rows[0]["dadd"].ToString();
                    txtemail.Text = dtdealer.Rows[0]["email"].ToString();
                    txtPhone.Text = dtdealer.Rows[0]["userloginmobileno"].ToString();
                    //txtcountry.Text = dtdealer.Rows[0]["country"].ToString();
                    //txtcity.Text = dtdealer.Rows[0]["city"].ToString();
                    //txtstate.Text = dtdealer.Rows[0]["state"].ToString();
                    //txtGstNo.Text = dtdealer.Rows[0]["gstno"].ToString();
                    hfid.Value = ddlname.SelectedValue;
                    //CustmerState = dtdealer.Rows[0]["StateID"].ToString();
                }
                else
                {

                }

            }
            else
            {
                string Getcusotmerdata = "select fname+''+mname+''+lname as name,email,phone,address1+''+address2 as uadd from userregistration where uid=" + ddlname.SelectedValue + "";
                SqlDataAdapter dacust = new SqlDataAdapter(Getcusotmerdata, con);
                DataTable dtcustoemr = new DataTable();
                dacust.Fill(dtcustoemr);
                if (dtcustoemr.Rows.Count > 0)
                {
                    txtAddress.Text = dtcustoemr.Rows[0]["uadd"].ToString();
                    txtemail.Text = dtcustoemr.Rows[0]["email"].ToString();
                    txtPhone.Text = dtcustoemr.Rows[0]["phone"].ToString();
                    //txtcity.Text = dtcustoemr.Rows[0]["city"].ToString();
                    //txtcountry.Text = dtcustoemr.Rows[0]["country"].ToString();
                    // txtstate.Text = dtcustoemr.Rows[0]["State"].ToString();
                    // txtGstNo.Text = "";
                    hfid.Value = ddlname.SelectedValue;
                    //CustmerState = dtcustoemr.Rows[0]["StateID"].ToString();
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
    }

    public void GridTotals()
    {
        try
        {
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            foreach (DataRow dtrout in dtprodn.Rows)
            {
                TotQty = (TotQty + Convert.ToDouble(dtrout["quantites"]));
                AmtBfrTax = AmtBfrTax + Convert.ToDouble(dtrout["producttotalprice"]);
                //totIGSTamt = totIGSTamt + Convert.ToDouble(dtrout["IGSTAmt"]);
                //totCGSTamt = totCGSTamt + Convert.ToDouble(dtrout["CGSTAmt"]);
                //totSGSTamt = totSGSTamt + Convert.ToDouble(dtrout["SGSTAmt"]);
                //totdisc = totdisc + Convert.ToDouble(dtrout["discount"]);
            }


            lbltotalamtaftertax.Text = AmtBfrTax.ToString();
            lblQty.Text = TotQty.ToString();
        }
        catch { }


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            #region
            //if (txtIGSTper.Text == "")
            //{
            //    txtIGSTper.Text = "0";
            //}
            //if (txtIGSTAmt.Text == "")
            //{
            //    txtIGSTAmt.Text = "0";
            //}
            //if (txtCGSTper.Text == "")
            //{
            //    txtCGSTper.Text = "0";
            //}
            //if (txtCGSTAmt.Text == "")
            //{
            //    txtCGSTAmt.Text = "0";
            //}
            //if (txtSGSTper.Text == "")
            //{
            //    txtSGSTper.Text = "0";
            //}
            //if (txtSGSTAmt.Text == "")
            //{
            //    txtSGSTAmt.Text = "0";
            //}



            #endregion


            if (ddlProduct.SelectedIndex == 0 || txtPrice.Text == string.Empty || txtQty.Text == string.Empty || txttaxabletotal.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Enter Proper Value ');", true);
                return;
            }

            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            DataRow dr = dtprodn.NewRow();

            dr["pid"] = ddlProduct.SelectedValue;
            dr["productprice"] = txtPrice.Text;
            //dr["discount"] = txtdiscounted.Text;
            //dr["productafterdiscountprice"] = txt_discountAmt.Text;
            dr["quantites"] = txtQty.Text;
            dr["producttotalprice"] = txttaxabletotal.Text;
            dr["gst"] = Convert.ToDouble(txt_GST.Text);
            dr["Productname"] = ddlProduct.SelectedItem.Text;


            //            pid
            //Productname
            //productprice
            //discount
            //productafterdiscountprice

            //quantites
            //producttotalprice
            //gst



            dtprodn.Rows.Add(dr);
            gvproduct.DataSource = dtprodn;
            gvproduct.DataBind();
            ViewState["dtprod"] = dtprodn;
            gvproduct.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvproduct.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvproduct.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvproduct.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvproduct.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            gvproduct.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[6].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[7].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[8].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[9].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[10].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //gvproduct.Columns[11].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            // gvproduct.Columns[12].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            GridTotals();
            /*
            foreach ( DataRow dtrout in dtprodn.Rows )
            {
                  TotQty = (TotQty + Convert.ToDouble(dtrout["quantites"]));
                AmtBfrTax = AmtBfrTax + Convert.ToDouble(dtrout["producttotalprice"]);
                totIGSTamt=totIGSTamt+ Convert.ToDouble(dtrout["IGSTAmt"]);
                totCGSTamt=totCGSTamt + Convert.ToDouble(dtrout["CGSTAmt"]);
                totSGSTamt=totSGSTamt+ Convert.ToDouble(dtrout["SGSTAmt"]);
                totdisc = totdisc + Convert.ToDouble(dtrout["discount"]);
            }

            lbltotqtyval.Text = TotQty.ToString();
            lbltotamtbfrtax.Text = AmtBfrTax.ToString();
            lblIGSTtot.Text = totIGSTamt.ToString();
            lblCGSTtot.Text = totCGSTamt.ToString();
            lblSGSTtot.Text = totSGSTamt.ToString();
            lbltotalamtaftertax.Text = (Convert.ToDouble(AmtBfrTax.ToString()) + Convert.ToDouble(totCGSTamt.ToString()) + Convert.ToDouble(totSGSTamt.ToString()) + Convert.ToDouble(totIGSTamt.ToString())).ToString();
            lbldiscount.Text = totdisc.ToString();
             */
            detailpartclear();
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }


    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtPrice.Text = "0";
            txtQty.Text = "0";
            //txtdiscounted.Text = "0";
            //  txt_discountAmt.Text = "0";
            txttaxabletotal.Text = "0";
            ////txtIGSTper.Text = "0";
            ////txtIGSTAmt.Text = "0";
            ////txtCGSTper.Text = "0";
            ////txtCGSTAmt.Text = "0";
            ////txtSGSTper.Text = "0";
            ////txtSGSTAmt.Text = "0";
            if (ddlUserType.SelectedItem.Value == "Dealer")
            {
                string getprice = "select dealerprice,discountprice,gst  from product where pid=" + ddlProduct.SelectedValue + "";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(getprice, con);
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[0]["dealerprice"].ToString() == "")
                    {
                        txtPrice.Text = "0.0";
                    }
                    else
                    {
                        txtPrice.Text = ds.Tables[0].Rows[0]["dealerprice"].ToString();
                    }
                    //if (ds.Tables[0].Rows[0]["discountprice"].ToString() == "")
                    //{
                    //    txtdiscounted.Text = "0.0";
                    //}
                    //else
                    //{
                    //    txtdiscounted.Text = ds.Tables[0].Rows[0]["discountprice"].ToString();
                    //}
                    if (ds.Tables[0].Rows[0]["gst"].ToString() == "")
                    {
                        txt_GST.Text = "0.0";
                    }
                    else
                    {
                        txt_GST.Text = ds.Tables[0].Rows[0]["gst"].ToString();
                    }
                }

                //SqlCommand cmdprice = new SqlCommand(getprice, con);
                //con.Open();
                //object objprise = cmdprice.ExecuteScalar();

                //if (string.IsNullOrWhiteSpace(objprise.ToString()))
                //{
                //    txtPrice.Text = "0.0";
                //    txtdiscounted.Text = "0.0";
                //}
                //else
                //{
                //    txtPrice.Text = objprise.ToString();
                //    txtdiscounted.Text = "0.0";
                //}
                con.Close();

            }
            else
            {
                string getprice = "select customerprice,discountprice,gst   from product where pid=" + ddlProduct.SelectedValue + "";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(getprice, con);
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[0]["customerprice"].ToString() == "")
                    {
                        txtPrice.Text = "0.0";
                    }
                    else
                    {
                        txtPrice.Text = ds.Tables[0].Rows[0]["customerprice"].ToString();
                    }
                    //if (ds.Tables[0].Rows[0]["discountprice"].ToString() == "")
                    //{
                    //    txtdiscounted.Text = "0.0";
                    //}
                    //else
                    //{
                    //    txtdiscounted.Text = ds.Tables[0].Rows[0]["discountprice"].ToString();
                    //}
                    if (ds.Tables[0].Rows[0]["gst"].ToString() == "")
                    {
                        txt_GST.Text = "0.0";
                    }
                    else
                    {
                        txt_GST.Text = ds.Tables[0].Rows[0]["gst"].ToString();
                    }

                }
                //SqlCommand cmdprice = new SqlCommand(getprice, con);
                //con.Open();
                //object objprise = cmdprice.ExecuteScalar();

                //if (string.IsNullOrWhiteSpace(objprise.ToString()))
                //{
                //    txtPrice.Text = "0.0";
                //}
                //else
                //{
                //    txtPrice.Text = objprise.ToString();
                //}
                con.Close();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message + ex.StackTrace);
        }
    }

    protected void txtdiscounted_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string discountedPrice = "";
            if (txtQty.Text == "")
            {
                txtQty.Text = "0";
            }
            //if (txtdiscounted.Text == "")
            //{
            //    txtdiscounted.Text = "0";
            //}

            //string price = txtPrice.Text + "" + txtdiscounted.Text + "" + txtQty.Text;
            //double amt1 = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtdiscounted.Text)) / 100;
            //double amt2 = (amt1 * 10.70) / 100;
            //double amt3 = amt1 - amt2;
            //txttaxabletotal.Text = (amt3 * Convert.ToDouble(txtQty.Text)).ToString();
            //txt_discountAmt.Text = (amt3).ToString();

            txttaxabletotal.Text = Math.Round(((Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQty.Text))), 2).ToString();


        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }


    private void Product_StockLess(Int64 ProductId, int Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_stockupdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@productid", ProductId);
            cmd.Parameters.AddWithValue("@quantites", Quantites);
            con.Open();
            cmd.ExecuteNonQuery();
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
    private void Product_StockAdd(Int64 ProductId, int Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_stockupdate_add";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@productid", ProductId);
            cmd.Parameters.AddWithValue("@quantites", Quantites);
            con.Open();
            cmd.ExecuteNonQuery();
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
    protected void btnSave_Click1(object sender, EventArgs e)
    {

        if (Request.QueryString["oid"] != null)
        {
            Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
            #region Save
            try
            {
                //string Usertype = "";
                //// if(ddlUserType.SelectedValue           

                //if (ddlUserType.SelectedItem.Value == "Stockiest")
                //{
                //    Usertype = "Dealer";
                //}
                //else if (ddlUserType.SelectedItem.Value == "Distributor") //else if (ddlUserType.SelectedItem.Value == "Customer")
                //{
                //    Usertype = "Customer";
                //}
                DataTable dtproduct = new DataTable();
                dtproduct = (DataTable)ViewState["dtprod"];


                bool flg = false;

                string Insertorderheader = "update Customer_orders  set [productquantites]=" + lblQty.Text + "  ,[amount]=" + lbltotalamtaftertax.Text + "  ,[totalamount]=" + Convert.ToDouble(lbltotalamtaftertax.Text) + " where oid=" + OrderID + "";

                SqlCommand cmdheade = new SqlCommand(Insertorderheader, con);
                con.Open();
                int objorder = cmdheade.ExecuteNonQuery();


                foreach (DataRow dr in dtproduct.Rows)
                {



                    string sel = "select * from Customer_orderproducts  where oid=" + OrderID + " and pid=" + dr["pid"].ToString() + "";
                    SqlCommand sel_cmd = new SqlCommand(sel, con);
                    SqlDataReader sel_dr = sel_cmd.ExecuteReader();
                    if (sel_dr.HasRows)
                    {
                        int Old_qty = 0;
                        int New_Qty = 0;
                        if (sel_dr.Read())
                        {
                            Old_qty = int.Parse(sel_dr["quantites"].ToString());
                        }
                        New_Qty = int.Parse(dr["quantites"].ToString());

                        sel_dr.Close();
                        InsertDetailTable = " update Customer_orderproducts set [productprice]=" + dr["productprice"].ToString() + ",[quantites]=" + dr["quantites"].ToString() + ",[producttotalprice]=" + dr["producttotalprice"].ToString() + ",[gst]=" + (Convert.ToDouble(dr["gst"].ToString())) + " where oid=" + OrderID + " and [pid]=" + dr["pid"].ToString() + "";

                        SqlCommand cmddeta = new SqlCommand(InsertDetailTable, con);
                        int i = cmddeta.ExecuteNonQuery();


                        if (Old_qty == New_Qty)
                        {
                        }
                        else if (Old_qty < New_Qty)
                        {
                            //less Stock
                            #region " Stock Update "
                            int qty = int.Parse(dr["quantites"].ToString());
                            int newqty = New_Qty - Old_qty;
                            Product_StockLess(Convert.ToInt64(dr["pid"].ToString()), newqty);
                            #endregion " Stock Update "
                        }
                        else if (Old_qty > New_Qty)
                        {
                            #region " Stock Update "
                            int qty = int.Parse(dr["quantites"].ToString());
                            int newqty = Old_qty - New_Qty;
                            Product_StockAdd(Convert.ToInt64(dr["pid"].ToString()), qty);
                            #endregion " Stock Update "
                        }

                    }
                    else
                    {
                        sel_dr.Close();
                        InsertDetailTable = " INSERT INTO Customer_orderproducts ([oid] ,[uid] ,[pid] ,[productprice] ,[discount] ,[productafterdiscountprice] ,[quantites] ,[producttotalprice]  ,[isdelete] ,[gst])  VALUES "
                                                + "  (" + OrderID + "," + hfid.Value + " ," + dr["pid"].ToString() + "," + dr["productprice"].ToString() + " ," + "0" + "," + "0" + "," + dr["quantites"].ToString() + "," + dr["producttotalprice"].ToString() + " "
                                                 + " ,0 ," + (Convert.ToDouble(dr["gst"].ToString())) + ") ";
                        SqlCommand cmddeta = new SqlCommand(InsertDetailTable, con);
                        int i = cmddeta.ExecuteNonQuery();

                        #region " Stock Update "
                        int qty1 = int.Parse(dr["quantites"].ToString());
                        Product_StockLess(Convert.ToInt64(dr["pid"].ToString()), qty1);
                        #endregion " Stock Update "
                    }

                }



                con.Close();



                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Salesorder" + objorder.ToString() + " Updated Successfully ');", true);

                clear();


            }
            catch (Exception ex)
            {

                Response.Write(ex.Message + ex.StackTrace);
            }
            #endregion
        }
        else
        {
            //#region Save
            //try
            //{
            //    string Usertype = "";
            //    // if(ddlUserType.SelectedValue           

            //    if (ddlUserType.SelectedItem.Value == "Stockiest")
            //    {
            //        Usertype = "Dealer";
            //    }
            //    else if (ddlUserType.SelectedItem.Value == "Distributor") //else if (ddlUserType.SelectedItem.Value == "Customer")
            //    {
            //        Usertype = "Customer";
            //    }
            //    DataTable dtproduct = new DataTable();
            //    dtproduct = (DataTable)ViewState["dtprod"];
            //    /*
            //     string Insertorderheader = "INSERT INTO [dbo].[orders]  ([uid]  ,[productquantites]  ,[billpaidornot] ,[amount] ,[discount] ,[tax] ,[totalamount] ,[orderdate]  ,[isdelete] ,[UserType]) "
            //                                                + " VALUES  ("+hfid.Value+" ,"+lbltotqtyval.Text+" ,0 ,"+lbltotamtbfrtax.Text+" ,"+lbldiscount.Text+" ,"+(Convert.ToDouble(lblIGSTtot.Text)+Convert.ToDouble(lblCGSTtot.Text)+Convert.ToDouble(lblSGSTtot.Text))+" ,"+Convert.ToDouble(lbltotalamtaftertax.Text)+",'"+txtOrderDate.Text+"' ,0 ,'"+ddlUserType.SelectedValue+ "');select isnull(SCOPE_IDENTITY(),0) as orderNo";
            //    */
            //    bool flg = false;
            //    string Insertorderheader = "INSERT INTO [dbo].[Ordersystem_orders]  ([uid]  ,[productquantites]  ,[billpaidornot] ,[amount] ,[discount] ,[tax] ,[totalamount] ,[orderdate]  ,[isdelete] ,[UserType],[isOrderConfirmed]) "
            //                                              + " VALUES  (" + hfid.Value + " ," + lbltotqtyval.Text + " ,0 ," + lbltotamtbfrtax.Text + " ," + lbldiscount.Text + " ," + (Convert.ToDouble(lblIGSTtot.Text) + Convert.ToDouble(lblCGSTtot.Text) + Convert.ToDouble(lblSGSTtot.Text)) + " ," + Convert.ToDouble(lbltotalamtaftertax.Text) + ",'" + txtOrderDate.Text + "' ,0 ,'" + Usertype + "','" + flg + "');select isnull(SCOPE_IDENTITY(),0) as orderNo";



            //    SqlCommand cmdheade = new SqlCommand(Insertorderheader, con);
            //    con.Open();
            //    object objorder = cmdheade.ExecuteScalar();


            //    foreach (DataRow dr in dtproduct.Rows)
            //    {
            //        InsertDetailTable = " INSERT INTO[dbo].[Ordersystem_orderproducts] ([oid] ,[uid] ,[pid] ,[productprice] ,[discount] ,[productafterdiscountprice] ,[quantites] ,[producttotalprice]  ,[isdelete] ,[gst],IGST,IGSTAmt,NoofBox)  VALUES "
            //                                + "  (" + objorder.ToString() + "," + hfid.Value + " ," + dr["pid"].ToString() + "," + dr["productprice"].ToString() + " ," + dr["discount"].ToString() + "," + dr["productafterdiscountprice"].ToString() + "," + dr["quantites"].ToString() + "," + dr["producttotalprice"].ToString() + " "
            //                                 + " ,0 ," + (Convert.ToDouble(dr["gst"].ToString())) + ", " + dr["IGSTper"].ToString() + "," + dr["IGSTAmt"].ToString() + " ,'" + dr["NoofBox"].ToString() + "') ";
            //        SqlCommand cmddeta = new SqlCommand(InsertDetailTable, con);
            //        int i = cmddeta.ExecuteNonQuery();

            //        //#region " Stock Update "
            //        //SqlCommand cmd = new SqlCommand();
            //        //cmd.CommandText = "product_stockupdate";
            //        //cmd.CommandType = CommandType.StoredProcedure;
            //        //cmd.Connection = con;
            //        //cmd.Parameters.AddWithValue("@productid", dr["pid"].ToString());
            //        //cmd.Parameters.AddWithValue("@quantites", dr["quantites"].ToString());
            //        //// con.Open();
            //        //cmd.ExecuteNonQuery();

            //        //#endregion " Stock Update "


            //        if (i > 0)
            //        {

            //        }
            //        else
            //        {

            //        }

            //    }



            //    con.Close();



            //    ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Salesorder" + objorder.ToString() + " Save Successfully ');", true);

            //    clear();


            //}
            //catch (Exception ex)
            //{

            //    Response.Write(ex.Message + ex.StackTrace);
            //}
            //#endregion

        }

        Response.Redirect("manageCustomerOrder.aspx");


    }

    protected void Remove_member1(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
            int ind = gr.RowIndex;
            Int64 pid = Convert.ToInt64(gvproduct.Rows[ind].Cells[0].Text);
            int Qty = int.Parse(gvproduct.Rows[ind].Cells[3].Text);

            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            dtprodn.Rows.RemoveAt(ind);

            dtprodn.AcceptChanges();
            gvproduct.DataSource = dtprodn;
            gvproduct.DataBind();

            if (Request.QueryString["oid"] != null)
            {
                Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true));
                //   string s = "update Customer_orderproducts set isdelete=1 where oid=" + OrderID + " and pid=" + pid + "";
                string s = "delete from Customer_orderproducts where oid=" + OrderID + " and pid=" + pid + "";
                SqlCommand cmd = new SqlCommand(s, con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    #region " Stock Update "
                    Product_StockAdd(pid, Qty);
                    #endregion " Stock Update "
                }

            }






            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Product Remove Successfully');", true);
            con.Close();
        }
        catch (Exception p)
        { }
        finally { con.Close(); }
        GridTotals();
    }
    protected void Edit_member1(object sender, EventArgs e)
    {
        //try
        //{
        //    // con.Open();

        //    GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
        //    int ind = gr.RowIndex;
        //    string regno = (gvproduct.Rows[ind].Cells[2].Text);


        //    dr["pid"] = ddlProduct.SelectedValue;
        //    dr["productprice"] = txtPrice.Text;
        //    dr["discount"] = txtdiscounted.Text;
        //    dr["productafterdiscountprice"] = txt_discountAmt.Text;
        //    dr["quantites"] = txtQty.Text;
        //    dr["producttotalprice"] = txttaxabletotal.Text;
        //    dr["gst"] = Convert.ToDouble(txtIGSTper.Text) + Convert.ToDouble(txtSGSTper.Text) + Convert.ToDouble(txtCGSTper.Text);
        //    dr["Productname"] = ddlProduct.SelectedItem.Text;
        //    dr["IGSTAmt"] = txtIGSTAmt.Text;
        //    dr["CGSTAmt"] = txtCGSTAmt.Text;
        //    dr["SGSTAmt"] = txtSGSTAmt.Text;
        //    dr["IGSTper"] = txtIGSTper.Text;
        //    dr["NoOfBox"] = txtNOB.Text;


        //}
        //catch (Exception p)
        //{ }
        //finally { }
    }


}
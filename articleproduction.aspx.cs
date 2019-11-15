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

public partial class articleproduction : System.Web.UI.Page
{
    common ocommon = new common();
    Int64 worksheetid;
    DataTable dtProduct = new DataTable();
    String createdby = string.Empty;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Session["userid"].ToString()))
            createdby = Session["userid"].ToString();

        if (!IsPostBack)
        {
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Article Production";
        }
        
    }

    private void Bind()
    {
        if (Request.QueryString["id"] != null)
        {
            worksheetid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
        }
            DataTable dtWorksheetdetails = new DataTable();
            SqlDataAdapter da;
            try
            {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "getWorksheetDetailsForArticleProduction",
                CommandType = CommandType.StoredProcedure,
                Connection = con
            };
            cmd.Parameters.AddWithValue("@id", worksheetid);
            con.Open();

            da = new SqlDataAdapter(cmd);
            da.Fill(dtWorksheetdetails);
            }
            catch (Exception ex)
            {
                ErrHandler.writeError(ex.Message, ex.StackTrace);
                
            }
            finally
            {
                con.Close();
            }

        if (dtWorksheetdetails != null)
        {
            if (dtWorksheetdetails.Rows.Count > 0)
            {
                txtworksheetno.Text = Convert.ToString(dtWorksheetdetails.Rows[0]["id"]);
                txtworksheetdate.Text = Convert.ToString(dtWorksheetdetails.Rows[0]["createddate"]);
                txtarticleno.Text = Convert.ToString(dtWorksheetdetails.Rows[0]["productname"]);
                txtcolor.Text = Convert.ToString(dtWorksheetdetails.Rows[0]["colornames"]);
                txtsize.Text = Convert.ToString(dtWorksheetdetails.Rows[0]["sizenames"]);
            }
        }


    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Int64 id = Int64.Parse(ddlVendor.SelectedValue.ToString());
        //try
        //{
        //    VendorMaster vm = new VendorMaster();
        //    DataTable dt = new DataTable();
        //    Cls_VendorMaster_b clsbrand = new Cls_VendorMaster_b();
        //    vm = clsbrand.SelectById(id);
        //    txtMobile.Text = vm.MobileNo1.ToString();
        //    txtEmail.Text = vm.email.ToString();
        //    //txtCity.Text = vm.fk_cityId.ToString();
        //    //txtState.Text = vm.fk_stateId.ToString();
        //    //txtCountry.Text = vm.fk_countryId.ToString();
        //    txtCity.Text = vm.city.ToString();
        //    txtState.Text = vm.state.ToString();
        //    txtCountry.Text = vm.country.ToString();
        //    txtAddress.Text = vm.Address1.ToString();


        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.writeError(ex.Message, ex.StackTrace);
        //    //return null;
        //}
        //finally { con.Close(); }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 id = Int64.Parse(ddlCategory.SelectedValue.ToString());
        try
        {
            product objProduct = new product();
            Cls_product_b clsProduct = new Cls_product_b();
            dtProduct = clsProduct.Product_WSSelectAllProductUsingCategoryId(id);
            if (dtProduct != null)
            {
                if (dtProduct.Rows.Count > 0)
                {
                    //ddlProduct.DataSource = dtProduct;
                    //ddlProduct.DataTextField = "productname";
                    //ddlProduct.DataValueField = "pid";
                    //ddlProduct.DataBind();
                    //System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Product(s)--", "0");
                    //ddlProduct.Items.Insert(0, objListItem);
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            //return null;
        }
        finally { con.Close(); }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtProduct = new DataTable();
        dtProduct = GetProducts();

        if (ViewState["Products"] != null)
        {
            dtProduct = (DataTable)ViewState["Products"];


            Repeater1.DataSource = dtProduct;
            Repeater1.DataBind();
            Repeater1.Visible = true;
        }
        else
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            Repeater1.Visible = false;
        }

        // ddlProductName.SelectedItem = string.Empty;
        //ddlCategory.SelectedIndex = 0;
        //ddlProduct.SelectedIndex = 0;

        txtquantity.Text = "0";
    }



    private DataTable GetProducts()
    {

        DataTable dtProduct = null;
        if (ViewState["SrNo"] != null)
        {
            int SrNo = Convert.ToInt32((ViewState["SrNo"]));
            SrNo++;
            ViewState["SrNo"] = SrNo;
        }
        else
        {
            ViewState["SrNo"] = 1;
        }

        if (ViewState["Products"] == null)
        {

            // [ProdId],[ProdName],[ProdBrand] ,[ProdSize]

            dtProduct = new DataTable("Products");
            dtProduct.Columns.Add(new DataColumn("SrNo", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("CatId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("ProdId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Quantity", typeof(string)));

            ViewState["Products"] = dtProduct;
        }
        else
        {
            dtProduct = (DataTable)ViewState["Products"];
        }
        DataRow dtRow = dtProduct.NewRow();

        dtRow["SrNo"] = ViewState["SrNo"];
        //dtRow["ProductName"] = ddlProduct.SelectedItem.ToString();
        //dtRow["ProdId"] = ddlProduct.SelectedValue;
        dtRow["CategoryName"] = ddlCategory.SelectedItem.ToString();
        dtRow["CatId"] = ddlCategory.SelectedValue;
        dtRow["Quantity"] = txtquantity.Text.Trim();
        dtProduct.Rows.Add(dtRow);
        ViewState["Products"] = dtProduct;
        return dtProduct;
    }



    protected void Remove_Product(object sender, EventArgs e)
    {
        try
        {
            //  con.Open();

            //GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
            //int ind = gr.RowIndex;
            //string pid = (gvproduct.Rows[ind].Cells[0].Text);
            int prodid;
            Button button = (sender as Button);
            //Get the command argument
            string commandArgument = button.CommandArgument;


            prodid = int.Parse(commandArgument);

            DataTable dtProduct = new DataTable();

            if (ViewState["Products"] == null)
            {

                // [ProdId],[ProdName],[ProdBrand] ,[ProdSize]

                dtProduct = new DataTable("Products");
                dtProduct.Columns.Add(new DataColumn("SrNo", typeof(int)));
                dtProduct.Columns.Add(new DataColumn("ProductName", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("Quantity", typeof(string)));

                ViewState["Products"] = dtProduct;
            }
            else
            {
                dtProduct = (DataTable)ViewState["Products"];
            }


            // dtProduct = (DataTable)ViewState["Products"];

            DataRow[] drr = dtProduct.Select("SrNo=' " + prodid + " ' ");
            foreach (var row in drr)
                row.Delete();

            // dtProduct.Rows.RemoveAt(prodid);

            dtProduct.AcceptChanges();
            Repeater1.DataSource = dtProduct;
            Repeater1.DataBind();
            //ddlProduct.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            txtquantity.Text = "0";
            //if (Request.QueryString["id"] != null)
            //{
            //    Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            //    string s = "delete from Ordersystem_orderproducts where oid=" + OrderID + " and pid=" + pid + "";
            //    SqlCommand cmd = new SqlCommand(s, con);
            //    int t = cmd.ExecuteNonQuery();


            //}
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Product Remove Successfully');", true);
            //con.Close();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Product Removed Successfully');", true);

        }
        catch (Exception p)
        { }
        finally
        {
            //con.Close(); 
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Int64 Result = 0, Result1 = 0;
        //DataTable dtProduct = new DataTable();


        //String user = null, month = null, pono = null;
        //bool flag = false, flag1 = false, flag2 = false, flag3 = false;

        //int year = int.Parse(DateTime.Now.Year.ToString());
        //month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        //int day = int.Parse(DateTime.Now.Day.ToString());
        //int min = int.Parse(DateTime.Now.Minute.ToString());
        //int hour = int.Parse(DateTime.Now.Hour.ToString());

        //pono = year + "_" + month.Substring(0, 3).ToUpper() + "_" + day + "_" + hour + min;

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //PurchaseOrderHeader objcategory = new PurchaseOrderHeader();
        //objcategory.PONo = pono;
        //objcategory.VendorId = Int64.Parse(ddlVendor.SelectedValue);

        //objcategory.isdeleted = false;

        //if (Request.QueryString["id"] != null)
        //{
        //    objcategory.PurchaseOrderId = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
        //    Result = (new Cls_PurchaseOrderHeader_b().Update(objcategory));
        //    if (Result > 0)
        //    {

        //        Response.Redirect(Page.ResolveUrl("~/managepurchaseorder.aspx?mode=u"));
        //    }
        //    else
        //    {

        //    }
        //}
        //else
        //{
        //    Result = (new Cls_PurchaseOrderHeader_b().Insert(objcategory));
        //    if (Result > 0)
        //    {
        //        con.Open();
        //        if (ViewState["Products"] != null)
        //            dtProduct = (DataTable)ViewState["Products"];
        //        PurchaseOrderDetails objPod = new PurchaseOrderDetails();
        //        foreach (DataRow row in dtProduct.Rows)
        //        {
        //            objPod.PurchaseOrderId = Result;
        //            objPod.ProdId = Convert.ToInt64(row["ProdId"]);
        //            objPod.CategoryId = Convert.ToInt64(row["CatId"]);
        //            objPod.Quantity = Convert.ToInt64(row["Quantity"]);
        //            objPod.Quantity1 = Convert.ToInt64(row["Quantity"]);

        //            Result1 = (new Cls_PurchaseOrderDetails_b().Insert(objPod));

        //            //if (Result1 > 0)
        //            // flag = true;


        //        }

        //        con.Close();

        //        //Clear();

        //        String vendorname = ddlVendor.SelectedItem.ToString();
        //        String mailTo = txtEmail.Text.Trim();
        //        //String Name = txt_contactperson.Text.Trim();
        //        String MobileNo = txtMobile.Text.Trim();
        //        flag = PDFUpload(Result);
        //        flag1 = SendOrderMail(mailTo, pono);
        //        //flag2 = SendSMS(vendorname, MobileNo);

        //        if (flag)
        //            Response.Redirect(Page.ResolveUrl("~/managepurchaseorder.aspx?mode=i"));
        //    }
        //    else
        //    {


        //    }
        //}
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manageworksheets.aspx");
    }
}
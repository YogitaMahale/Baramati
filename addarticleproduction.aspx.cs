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

public partial class addarticleproduction : System.Web.UI.Page
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
        DataSet dtWorksheetdetails = new DataSet();
        DataTable dtEmployee = new DataTable();
        SqlDataAdapter da;

        if (Request.QueryString["id"] != null)
        {
            worksheetid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
        }
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


            SqlCommand cmdemp = new SqlCommand
            {
                CommandText = "SELECT employeename,id FROM tblEmployeeMaster WHERE id IN (SELECT empid FROM employeewages WHERE isdeleted=0) AND isdelete=0",
                CommandType = CommandType.Text,
                Connection = con
            };
            cmdemp.Parameters.AddWithValue("@id", worksheetid);
            //con.Open();

            da = new SqlDataAdapter(cmdemp);
            da.Fill(dtEmployee);


            if (dtEmployee != null)
            {
                if (dtEmployee.Rows.Count > 0)
                {


                    lstemployee.DataSource = dtEmployee;
                    lstemployee.DataTextField = "employeename";
                    lstemployee.DataValueField = "id";
                    lstemployee.DataBind();
                    //System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Color--", "0");
                    //lstemployee.Items.Insert(0, objListItem);


                }
                else
                {
                    lstemployee.DataSource = dtEmployee;
                    lstemployee.DataTextField = "employeename";
                    lstemployee.DataValueField = "id";
                    lstemployee.DataBind();


                }
            }
            else
            {
                lstemployee.DataSource = dtEmployee;
                lstemployee.DataTextField = "employeename";
                lstemployee.DataValueField = "id";
                lstemployee.DataBind();
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

        if (dtWorksheetdetails != null)
        {
            if (dtWorksheetdetails.Tables[0].Rows.Count > 0)
            {
                if (dtWorksheetdetails.Tables[0].Rows.Count > 0)
                {
                    txtworksheetno.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["id"]);
                    txtworksheetdate.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["createddate"]);
                    txtarticleno.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["productname"]);
                    txtcolor.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["colornames"]);
                    txtsize.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["groupname"]);
                    txtpid.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["productid"]);
                    txtcolorid.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["colorid"]);
                    txtsizegroupid.Text = Convert.ToString(dtWorksheetdetails.Tables[0].Rows[0]["sizeid"]);
                }
            }

            if (dtWorksheetdetails.Tables[1].Rows.Count > 0)
            {
                if (dtWorksheetdetails.Tables[1].Rows.Count > 0)
                {
                    Repeater1.DataSource = dtWorksheetdetails.Tables[1];
                    Repeater1.DataBind();
                }
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

    /*
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

    */

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0, Result1 = 0, Result2=0;
        DataTable dtProduct = new DataTable();

        //Response.Write(hf1.Value);

        //String valuesColor = hfcolorid.Value, valuesSize = hfsizeid.Value;


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //id, worksheetno, employeeid, totalpairs, vshape, silai, factorysecond, isdeleted, createddate

        articleproduction objarticleproduction = new articleproduction();

        objarticleproduction.worksheetno = Convert.ToInt64(txtworksheetno.Text);
        objarticleproduction.employeeid = Convert.ToInt64(hfemployeeid.Value);
        if (cbvshape.Checked)
        {
            objarticleproduction.vshape = Convert.ToInt64(txttotalpairs.Text);

        }
        else if (cbsilai.Checked)
        {
            objarticleproduction.silai = Convert.ToInt64(txttotalpairs.Text);
        }
        else
            objarticleproduction.totalpairs = Convert.ToInt64(txttotalpairs.Text);

        objarticleproduction.factorysecond = Convert.ToInt64(txtfactorysecond.Text);


        Result = (new Cls_articleproduction_b().Insert(objarticleproduction));
        if (Result > 0)
        {
            //if (ViewState["Products"] != null)
            //    dtProduct = (DataTable)ViewState["Products"];

            //id, pid, sizeid, colorid, quantity

            Label id = new Label(); TextBox quantity=new TextBox();
            articlestock objPod = new articlestock();
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    id = (Label)item.FindControl("lblid");
                    quantity = (TextBox)item.FindControl("txtquantity");
                    
                }
                objPod.pid = Convert.ToInt64(txtpid.Text);
                objPod.sizeid = Convert.ToInt64(id.Text);
                objPod.colorid = Convert.ToInt64(txtcolorid.Text);
                objPod.quantity = Convert.ToDecimal(quantity.Text);
               
                Result1 = (new Cls_articlestock_b().InsertUpdate(objPod));

                //if (Result1 > 0)
                // flag = true;

            }

            articleloosepairs objarticle = new articleloosepairs();
            objarticle.pid = Convert.ToInt64(txtpid.Text);
            objarticle.sizegroupid = Convert.ToInt64(txtsizegroupid.Text);
            objarticle.colorid = Convert.ToInt64(txtcolorid.Text);
            objarticle.quantity = Convert.ToDecimal(txtloosepair.Text);

            Result2 = (new Cls_articleloosepairs_b().InsertUpdate(objarticle));

            Response.Redirect(Page.ResolveUrl("~/manageworksheets.aspx?mode=a"));
        }
        else
        {


        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manageworksheets.aspx");
    }
}
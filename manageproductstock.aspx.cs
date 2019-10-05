using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class manageproductstock : System.Web.UI.Page
{
    string productFrontPath = "~/uploads/product/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Products Stock";
            Page.Title = "Manage Product Stock";
        }
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["mode"] == "u")
            {
                if (Request.QueryString["catid"] != null)
                {
                    BindProduct(Convert.ToInt64(Request.QueryString["catid"].ToString()));
                    ddlCategory.SelectedValue = Request.QueryString["catid"].ToString();
                }
                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Product Updated Successfully";

            }
            else if (Request.QueryString["mode"] == "i")
            {
                if (Request.QueryString["catid"] != null)
                {
                    BindProduct(Convert.ToInt64(Request.QueryString["catid"].ToString()));
                    ddlCategory.SelectedValue = Request.QueryString["catid"].ToString();
                }
                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Product Inserted Successfully";
            }
        }
    }

    private void BindCategory()
    {
        DataTable dtCategory = (new Cls_category_b().SelectAll());
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "cid";
                ddlCategory.DataBind();
                ListItem objListItem = new ListItem("--Select Category--", "0");
                ddlCategory.Items.Insert(0, objListItem);
            }
        }
    }

    private void BindProduct(Int64 CategoryId)
    {
        if (CategoryId > 0)
        {
            divProduct.Visible = true;
            DataTable dtProduct = (new Cls_product_b().SelectAllProductUsingCategoryIdAdmin(CategoryId));
            if (dtProduct != null)
            {
                divSaveAll.Visible = true;
                repProduct.DataSource = dtProduct;
                repProduct.DataBind();
            }
            else
            {
                divSaveAll.Visible = false;
                repProduct.DataSource = null;
                repProduct.DataBind();
            }
        }
        else
        {
            divSaveAll.Visible = false;
            repProduct.DataSource = null;
            repProduct.DataBind();
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 ProductId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);

        bool yes = (new Cls_product_b().Delete(ProductId, CategoryId));
        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Deleted Successfully";
            BindProduct(Convert.ToInt64(ViewState["CategoryId"]));

        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Not Deleted";
        }
    }

    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditproduct.aspx?page=stock"));
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProduct(Convert.ToInt64(ddlCategory.SelectedValue));
        ViewState["CategoryId"] = ddlCategory.SelectedValue;
    }

    protected void repProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Image imgProduct = (Image)e.Item.FindControl("imgProduct");
            DropDownList ddlSeqNo = (DropDownList)e.Item.FindControl("ddlSeqNo");
            HyperLink hlAddImageVideo = (HyperLink)e.Item.FindControl("hlAddImageVideo");
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditproduct.aspx?page=stock&id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "pid").ToString(), true));
            hlAddImageVideo.NavigateUrl = Page.ResolveUrl("~/addeditproductmultipleimages.aspx?page=stock&id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "pid").ToString(), true));
            imgProduct.ImageUrl = productFrontPath + DataBinder.Eval(e.Item.DataItem, "mainimage").ToString();
            Fill_SeqNo(Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "SeqNo")), Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "MaxSeqNo")), ref ddlSeqNo);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in repProduct.Items)
        {
            CheckBox chkContainer = (CheckBox)item.FindControl("chkContainer");
            if (chkContainer.Checked)
            {
                string ProductId = chkContainer.Attributes["attr-ID"];
                TextBox txtStockQuantites = (TextBox)item.FindControl("txtStockQuantites");
                CheckBox chbIsStock = (CheckBox)item.FindControl("chbIsStock");
                CheckBox chbIsActive = (CheckBox)item.FindControl("IsActive");
                Product_UpdateProductStockOnly(Convert.ToInt64(ProductId), Convert.ToInt32(txtStockQuantites.Text), chbIsStock.Checked, chbIsActive.Checked);
            }
        }
        spnMessage.Visible = true;
        spnMessage.Style.Add("color", "green");
        spnMessage.InnerText = "Product Updated Successfully";
        if (ViewState["CategoryId"] != null)
        {
            BindProduct(Convert.ToInt64(ViewState["CategoryId"]));
        }
        else
        {
            BindProduct(Convert.ToInt64(ddlCategory.SelectedValue));
        }
    }

    public void Product_UpdateProductStockOnly(Int64 ProductId, Int32 StockQuantites, bool IsStock, bool IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_UpdateProductStockOnly";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@pid", ProductId);
            cmd.Parameters.AddWithValue("@stockquantites", StockQuantites);
            cmd.Parameters.AddWithValue("@isstock", IsStock);
            cmd.Parameters.AddWithValue("@isactive", IsActive);
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
    }

    protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList DDLSeqNo = (DropDownList)sender;
        RepeaterItem item = (sender as DropDownList).Parent as RepeaterItem;
        Int64 ProductId = int.Parse((item.FindControl("lblProductId") as Label).Text);
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
        Int64 PreSeqNo = int.Parse((item.FindControl("lblSeqNo") as Label).Text);
        Update_SeqNo_Product_DB(ProductId, CategoryId, PreSeqNo, Convert.ToInt64(DDLSeqNo.SelectedValue));
    }

    public void Update_SeqNo_Product_DB(Int64 ProductId, Int64 CategoryId, Int64 PrevSeqNo, Int64 SeqNo)
    {
        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Update_SeqNo_Product";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Con;

            cmd.Parameters.AddWithValue("@ProductID", ProductId);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@PrevSeqNo", PrevSeqNo);
            cmd.Parameters.AddWithValue("@SeqNo", SeqNo);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product Updated Successfully";
            BindProduct(CategoryId);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            Con.Close();
        }
    }

    public void Fill_SeqNo(Int64 SelectedSeqNo, Int64 MaxSeqNo, ref DropDownList DDLSeqNo)
    {
        ListItem lst0;
        for (int i = 0; i < MaxSeqNo; i++)
        {
            lst0 = new ListItem((i + 1).ToString(), (i + 1).ToString());
            DDLSeqNo.Items.Add(lst0);
        }
        DDLSeqNo.DataBind();
        if (DDLSeqNo.Items.Count > 0)
            DDLSeqNo.SelectedValue = SelectedSeqNo.ToString();
    }

}
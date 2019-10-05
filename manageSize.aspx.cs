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
public partial class manageSize : System.Web.UI.Page
{
    //string categoryFrontPath = "~/uploads/category/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Size";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Size Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Size Inserted Successfully";
        }
    }

    private void BindCategory()
    {
        DataTable dtCategory = (new Cls_size_b().SelectAll());
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                repCategory.DataSource = dtCategory;
                repCategory.DataBind();
            }
            else
            {
                repCategory.DataSource = null;
                repCategory.DataBind();
            }
        }
        else
        {
            repCategory.DataSource = null;
            repCategory.DataBind();
        }
    }

    protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditSize.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "cid").ToString(), true));

        }
    }



    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //Int64 ProductCount = int.Parse((item.FindControl("lblProductCount") as Label).Text);
        spnMessage.Visible = true;
        //if (ProductCount.ToString() == "0")
        //{
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
        bool yes = (new Cls_size_b ().Delete(CategoryId));

        if (yes)
        {
            BindCategory();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Size Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Size Not Deleted";
        }
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "In this category product added..so you can not delete.";
        //}

    }


    protected void btnNewCategoty_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditSize.aspx"));
    }


}
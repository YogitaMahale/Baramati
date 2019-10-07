using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class manage_article : System.Web.UI.Page
{
    //string agentFrontPath = "~/uploads/vendor/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindArticle();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Article";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Article Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Article Inserted Successfully";
        }
    }

    private void BindArticle()
    {
        DataTable dtArticle = (new Cls_article_b ().SelectAll());
        if (dtArticle != null)
        {
            if (dtArticle.Rows.Count > 0)
            {
                repArticle .DataSource = dtArticle;
                repArticle.DataBind();
            }
            else
            {
                repArticle.DataSource = null;
                repArticle.DataBind();
            }
        }
        else
        {
            repArticle.DataSource = null;
            repArticle.DataBind();
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditArticle.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //  Int32 BankCount = int.Parse((item.FindControl("lblBankCount") as Label).Text);
        spnMessage.Visible = true;
        //if (BankCount.ToString() == "0")
        //{
        Int32 pid = int.Parse((item.FindControl("lblpid") as Label).Text);
        bool yes = (new Cls_article_b ().Delete(pid));

        if (yes)
        {
            BindArticle();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Article Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Article Not Deleted";
        }
        //}
        //else
        //{
        //    spnMessage.Style.Add("color", "red");
        //    spnMessage.InnerText = "In this bank category added..so you can not delete.";
        //}
    }


     



    protected void repArticle_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditArticle.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "productid").ToString(), true));

            //Image imgCategory = (Image)e.Item.FindControl("imgProfile");
            //imgCategory.ImageUrl = agentFrontPath + DataBinder.Eval(e.Item.DataItem, "img").ToString();

        }
    }
}
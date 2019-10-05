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

public partial class managenews : System.Web.UI.Page
{
    string newsFrontPath = "~/uploads/news/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindNews();
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product News Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product News Inserted Successfully";
        }

        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage New Product Arrival";
    }

    private void BindNews()
    {
        DataTable dtStudent = (new Cls_newsupdate_b().SelectAll());
        if (dtStudent != null)
        {
            if (dtStudent.Rows.Count > 0)
            {
                repNews.DataSource = dtStudent;
                repNews.DataBind();
            }
            else
            {
                repNews.DataSource = null;
                repNews.DataBind();
            }
        }
        else
        {
            repNews.DataSource = null;
            repNews.DataBind();
        }
    }

    protected void btnAddStudent_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditnews.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 NewsId = int.Parse((item.FindControl("hfId") as HiddenField).Value);
        bool yes = (new Cls_newsupdate_b().Delete(NewsId));
        spnMessage.Visible = (new Cls_newsupdate_b().Delete(NewsId));
        if (yes)
        {
            BindNews();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "News Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "News Not Deleted";
        }
    }

    public bool News_IsActive(Int64 NewsId, Boolean IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "newsupdate_IsActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@newsid", NewsId);
            cmd.Parameters.AddWithValue("@isactive", IsActive);
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return true;
    }

    protected void cbIsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 NewsId = int.Parse((item.FindControl("lblNewsId") as Label).Text);
        bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsActive") as CheckBox).Checked);
        bool yes = News_IsActive(NewsId, cbIsActive);
        spnMessage.Visible = true;
        BindNews();
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "News Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "News Not Updated";
        }
    }

    protected void repNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            Image imgNews = (Image)e.Item.FindControl("imgNews");
            imgNews.ImageUrl = newsFrontPath + DataBinder.Eval(e.Item.DataItem, "imagename").ToString();
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditnews.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "newsupdateid").ToString(), true));
        }
    }
}
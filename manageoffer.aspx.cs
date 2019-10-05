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

public partial class manageoffer : System.Web.UI.Page
{
    string OfferFrontPath = "~/uploads/offer/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindScheme();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Offer's on Product";
        }
        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Offer Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Offer Inserted Successfully";
        }

    }

    public void BindScheme()
    {
        DataTable dtScheme = (new Cls_offers_b().SelectAll());
        if (dtScheme != null)
        {
            if (dtScheme.Rows.Count > 0)
            {
                repScheme.DataSource = dtScheme;
                repScheme.DataBind();
            }
            else
            {
                repScheme.DataSource = null;
                repScheme.DataBind();
            }
        }
        else
        {
            repScheme.DataSource = null;
            repScheme.DataBind();
        }
    }

    protected void cbIsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 SchemeId = int.Parse((item.FindControl("lblSchemeId") as Label).Text);
        bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsActive") as CheckBox).Checked);
        bool yes = Offer_IsActive(SchemeId, cbIsActive);
        spnMessage.Visible = true;
        BindScheme();
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Offer Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Offer Not Updated";
        }
    }

    protected void btnAddOffer_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditoffer.aspx"));
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 OfferId = int.Parse((item.FindControl("lblSchemeId") as Label).Text);
        bool yes = (new Cls_offers_b().Delete(OfferId));
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Offer Deleted Successfully";
            BindScheme();
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Offer Not Deleted";
            BindScheme();
        }
    }

    public bool Offer_IsActive(Int64 OfferId, Boolean IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "offer_IsActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@offerid", OfferId);
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

    protected void repScheme_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Image imgOffer = (Image)e.Item.FindControl("imgOffer");
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditoffer.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "offerid").ToString(), true));
            if (DataBinder.Eval(e.Item.DataItem, "imagename").ToString() != string.Empty)
                imgOffer.ImageUrl = OfferFrontPath + DataBinder.Eval(e.Item.DataItem, "imagename").ToString();
        }
    }
}
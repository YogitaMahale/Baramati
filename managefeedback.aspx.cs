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

public partial class managefeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindFeedBack();
        }
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "User Feedback";
    }

    private void BindFeedBack()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp;
        DataTable dtFeedback = new DataTable();
        cmd.Connection = con;
        cmd.CommandText = "SelectAll_productfeedback";
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            con.Open();
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtFeedback);
            if (dtFeedback != null)
            {
                if (dtFeedback.Rows.Count > 0)
                {
                    repFeedBack.DataSource = dtFeedback;
                    repFeedBack.DataBind();
                }
                else
                {
                    repFeedBack.DataSource = null;
                    repFeedBack.DataBind();
                }
            }
            else
            {
                repFeedBack.DataSource = null;
                repFeedBack.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }

    protected void repFeedBack_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlImage1 = (HyperLink)e.Item.FindControl("hlImage1");
            hlImage1.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/productfeedbackimage/" + DataBinder.Eval(e.Item.DataItem, "image1").ToString());
            HyperLink hlImage2 = (HyperLink)e.Item.FindControl("hlImage2");
            hlImage2.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/productfeedbackimage/" + DataBinder.Eval(e.Item.DataItem, "image2").ToString());
        }
    }
}
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditgroup : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindBank(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Group";
                Page.Title = "Update Group";
            }
            else
            {
                hPageTitle.InnerText = "New Group";
                Page.Title = "New Group";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtgroupname.Text = string.Empty;
     
    }

    public void BindBank(Int64 id)
    {
        groupmaster objgroupmaster = (new Cls_groupmaster_b().SelectById(id));
        if (objgroupmaster != null)
        {
            txtgroupname.Text = objgroupmaster.groupname;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managegroup.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        groupmaster objgroupmaster = new groupmaster();
        objgroupmaster.groupname = txtgroupname.Text.Trim();

        if (Request.QueryString["id"] != null)
        {
            objgroupmaster.id = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_groupmaster_b().Update(objgroupmaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managegroup.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Failed To Upadate Group";
                BindBank(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_groupmaster_b().Insert(objgroupmaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managegroup.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Failed To Create Group";

            }
        }
    }
}
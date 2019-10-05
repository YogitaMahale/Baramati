using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditAgent : System.Web.UI.Page
{
    int categoryImageFrontWidth = 500;
    int categoryImageFrontHeight = 605;
    string categoryMainPath = "~/uploads/agent/";
    string categoryFrontPath = "~/uploads/agent/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindAgent(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Agent";
                Page.Title = "Update Agent";
            }
            else
            {
                hPageTitle.InnerText = "Add Agent";
                Page.Title = "Add Agent";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtAgentName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtEmail.Text = string.Empty;
        imgCategory.Visible = false;
        ViewState["fileName"] = null;
    }

    public void BindAgent(Int64 BankId)
    {
        AgentMaster objAgentMaster = (new Cls_agentmaster_b().SelectById(BankId));
        if (objAgentMaster != null)
        {
            txtAgentName.Text = objAgentMaster.Agentname;
            txtAddress.Text = objAgentMaster.Address;
            txtMobileNo.Text = objAgentMaster.MobileNo;
            txtEmail.Text = objAgentMaster.email;


            if (!string.IsNullOrEmpty(objAgentMaster.img))
            {
                imgCategory.Visible = true;
                ViewState["fileName"] = objAgentMaster.img;
                imgCategory.ImageUrl = categoryFrontPath + objAgentMaster.img;
                btnImageUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                btnImageUpload.Visible = true;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/ManageAgent.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        AgentMaster objAgentMaster = new AgentMaster();
        objAgentMaster.Agentname = txtAgentName.Text.Trim();
        objAgentMaster.Address = txtAddress.Text.Trim();
        objAgentMaster.MobileNo = txtMobileNo.Text.Trim();
        objAgentMaster.email = txtEmail.Text.Trim();

        if (ViewState["fileName"] != null)
        {
            objAgentMaster.img = ViewState["fileName"].ToString();
        }


        if (Request.QueryString["id"] != null)
        {
            objAgentMaster.aid = Convert.ToInt32(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_agentmaster_b().Update(objAgentMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/ManageAgent.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Agent Not Updated";
                BindAgent(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_agentmaster_b().Insert(objAgentMaster));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/ManageAgent.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Agent Not Inserted";

            }
        }
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        if (fpCategory.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpCategory.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpCategory.FileName);
            fpCategory.SaveAs(MapPath(categoryMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\agent\\", categoryImageFrontWidth, categoryImageFrontHeight, "~/Uploads/agent/front/", fileName);
            imgCategory.Visible = true;
            imgCategory.ImageUrl = categoryFrontPath + fileName;
            ViewState["fileName"] = fileName;
            btnRemove.Visible = true;
            btnImageUpload.Visible = false;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        var filePath = Server.MapPath("~/uploads/agent/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var filePath1 = Server.MapPath("~/uploads/agent/front/" + ViewState["fileName"].ToString());
        if (File.Exists(filePath1))
        {
            File.Delete(filePath1);
        }

        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = string.Empty;
        imgCategory.Visible = false;
    }
}
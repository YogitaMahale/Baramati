using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class excelexport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = Request.QueryString["filename"].ToString();
        this.GridView1.DataSource = Session["dtProduct"];
        this.GridView1.DataBind();
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        this.GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
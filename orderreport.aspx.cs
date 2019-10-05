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

public partial class orderreport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Order Report";
    }

    public void BindOrderReport()
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ToString());
        DataTable dtOrderReport = new DataTable();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_OrderReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue.ToString());
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtOrderReport);
            if (dtOrderReport != null)
            {
                if (dtOrderReport.Rows.Count > 0)
                {
                    spnMessage.Visible = false;
                    gvOrderReport.DataSource = dtOrderReport;
                    gvOrderReport.DataBind();
                    Session["dtProduct"] = dtOrderReport;
                }
                else
                {
                    spnMessage.Visible = true;
                    gvOrderReport.DataSource = null;
                    gvOrderReport.DataBind();
                }
            }
            else
            {
                spnMessage.Visible = true;
                gvOrderReport.DataSource = null;
                gvOrderReport.DataBind();
            }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindOrderReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtProduct"] != null)
            {
                Response.Redirect("ExcelExport.aspx?filename=Order_Report_" + ddlMonth.SelectedValue.ToString() + "_" + ddlYear.SelectedValue.ToString() + ".xls");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvOrderReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindOrderReport();
        gvOrderReport.PageIndex = e.NewPageIndex;
        gvOrderReport.DataBind();
    }
}
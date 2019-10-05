using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_AgentList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    String from = DateTime.Now.ToString("dd/MM/yyyy"), to = DateTime.Now.ToString("dd/MM/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWebUserList();
            //getAgentsByCash();
        }
    }



    private void BindWebUserList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "WebsiteUser_SelectAllAdmin";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        DataTable dtUserType = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtUserType);
        //SqlCommand cmd = new SqlCommand("select * from AdminLogin Where isdelete=0");
        //SqlDataAdapter sda = new SqlDataAdapter();
        //DataTable dtUserType = new DataTable();
        //con.Open();
        //cmd.Connection = con;
        //sda.SelectCommand = cmd;
        //sda.Fill(dtUserType);
        if (dtUserType != null)
        {
            if (dtUserType.Rows.Count > 0)
            {
                ddlAgents.DataSource = dtUserType;
                ddlAgents.DataTextField = "name";
                ddlAgents.DataValueField = "adminid";
                ddlAgents.DataBind();
                ListItem objListItem = new ListItem("--Select Agent--", "0");
                ddlAgents.Items.Insert(0, objListItem);
            }
            else
            {
                
            }
        }
        else
        {
            
        }
    }

    protected void txt_fromDate_TextChanged(object sender, EventArgs e)
    {
        getAgentsByCash();
    }

    private void getAgentsByCash()
    {
        DataTable dtAgents = new DataTable();
        SqlDataAdapter daAgents = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        String agentid = ddlAgents.SelectedValue;
        if (!String.IsNullOrEmpty(txt_fromDate.Text.ToString()) && !String.IsNullOrEmpty(txt_toDate.Text.ToString()))
        {
            DateTime ff = DateTime.ParseExact(txt_fromDate.Text, "dd/MM/yyyy", null);
            System.Data.SqlTypes.SqlDateTime dtSql = System.Data.SqlTypes.SqlDateTime.Parse(ff.ToString("yyyy-MM-dd"));
            from = dtSql.ToString();
            from = txt_fromDate.Text.ToString();
            to = txt_toDate.Text.ToString();

            

        }
        
        try
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getAgentsByCash";
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            cmd.Parameters.AddWithValue("@agentid",agentid);

            daAgents = new SqlDataAdapter(cmd);
            daAgents.Fill(dtAgents);
            if (dtAgents != null)
            {
                if (dtAgents.Rows.Count > 0)
                {
                    Session["dtProduct"] = dtAgents;
                    repAgents.DataSource = dtAgents;
                    repAgents.DataBind();
                    btnExcelExport.Visible = true;
                }
                else
                {
                    repAgents.DataSource = null;
                    repAgents.DataBind();
                    btnExcelExport.Visible = false;
                }
            }
            else
            {
                repAgents.DataSource = null;
                repAgents.DataBind();
                btnExcelExport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            con.Close();
        }


    }


    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["dtProduct"] != null)
            {
                 //Response.Redirect("ExcelExport.aspx?filename=Agents By Cash.xls");
                repAgents.DataSource = Session["dtProduct"];
                repAgents.DataBind();
            }




            //foreach (RepeaterItem item in repDealerActive.Items)
            //{
            //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            //    {
            //        var checkBox = (CheckBox)item.FindControl("chkContainerActive");
            //        var checkBox1 = (CheckBox)item.FindControl("cbIsActiveUser");
            //        var xx = item.FindControl("hfActiveUserId");
            //        var img = (System.Web.UI.WebControls.Image)item.FindControl("imgStatus");
            //        var lButton = (LinkButton)item.FindControl("lnkActiveUserDelete");
            //        var status = (Label)item.FindControl("lblStatus");


            //        xx.Visible = false;
            //        lButton.Visible = false;
            //        checkBox.Visible = false;
            //        checkBox1.Visible = false;
            //        img.Visible = false;
            //        status.Visible = false;

            //    }
            //    //Do something with your checkbox...


            //}



            String agentname = ddlAgents.SelectedItem.Text;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Report For "+agentname+".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);




            repAgents.RenderControl(htmlWrite);
            Response.Write("<table>"
                +"<thead>"
                                    + "<tr>"
                //                      + "  <th ></th>"
                                        + "<th >Dealer</th>"

                                        + "<th >Payment (Rs.)</th>"
                //                        + "<th >Password</th>"

                //                        + "<th>WhatsApp MNo.</th>"

                //                        + "<th>Login Count</th>"


                //                        + "<th>State City</th>"

                //                        + "<th>Regn. Date</th>"

                //                        + "<th>Access</th>"

                //                        + "<th>User Type</th>"

                //                        + "<th>Status</th>"

                //                        + "<th>Agent</th>"

                //                        //+"<th>User Status</th>"

                //                        //+"<th>Action</th>"

                                    + "</tr>"
                                + "</thead>"
                                );
            Response.Write(stringWrite.ToString());
            Response.Write("</table>");
            Response.End();

            //HttpContext.Current.ApplicationInstance.CompleteRequest();



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
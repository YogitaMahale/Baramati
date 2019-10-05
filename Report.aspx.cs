using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report : System.Web.UI.Page
{
    string sqlconnect = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
    private void BindWebUserList()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from AdminLogin Where isdelete=0");
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtUserType = new DataTable();
        con.Open();
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dtUserType);
        if (dtUserType != null)
        {
            if (dtUserType.Rows.Count > 0)
            {
                ddlUser.DataSource = dtUserType;
                ddlUser.DataTextField = "name";
                ddlUser.DataValueField = "AdminID";
                ddlUser.DataBind();
                ListItem objListItem = new ListItem("--Select User--", "0");
                ddlUser.Items.Insert(0, objListItem);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindWebUserList();
        }

        //if (Request.QueryString["mode"] == "u")
        //{
        //    spnMessage.Visible = true;
        //    spnMessage.Style.Add("color", "green");
        //    spnMessage.InnerText = "Product News Updated Successfully";
        //}
        //else if (Request.QueryString["mode"] == "i")
        //{
        //    spnMessage.Visible = true;
        //    spnMessage.Style.Add("color", "green");
        //    spnMessage.InnerText = "Product News Inserted Successfully";
        //}

        //HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        //hPageTitle.InnerText = "Manage Morya Followup ";
    }

    private void BindNews()
    {
        //string selectquery = "SELECT [PKId],[Name],[Mobile],[City],[IsRead],[Remark] FROM [admin_moryatools].[admin_moryatools].[EZACUSTblFollowup] where [IsRead]=0";
        //SqlDataAdapter danewdata = new SqlDataAdapter(selectquery, sqlconnect);
        //DataTable dtStudent = new DataTable();

        //danewdata.Fill(dtStudent);

        //if (dtStudent != null)
        //{
        //    if (dtStudent.Rows.Count > 0)
        //    {
        //        repNews.DataSource = dtStudent;
        //        repNews.DataBind();
        //    }
        //    else
        //    {
        //        repNews.DataSource = null;
        //        repNews.DataBind();
        //    }
        //}
        //else
        //{
        //    repNews.DataSource = null;
        //    repNews.DataBind();
        //}
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txt_fromDate.Text.Trim() == "".Trim().ToString() || txt_toDate.Text.Trim() == "".ToString().Trim())
        {
        }
        else
        {
            try
            {

                string UserID = ddlUser.SelectedValue.ToString();
                string selectquery = "";
                if (ddlUser.SelectedIndex == 0)
                {
                    if (DropDownList1.SelectedItem.ToString() == "MYSSKFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark] ,date1 FROM   [MySSKTblFollowup] where [IsRead]=1 and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "')  or date1 is null)";
                    }
                    else if (DropDownList1.SelectedItem.ToString() == "MoryaFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark],date1  FROM   [MoryaTblFollowup] where [IsRead]=1 and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "') or date1 is null)";

                    }
                    else if (DropDownList1.SelectedItem.ToString() == "EZACUSFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark] ,date1 FROM   [EZACUSTblFollowup] where [IsRead]=1 and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "')  or date1 is null)";

                    }
                }
                else
                {
                    if (DropDownList1.SelectedItem.ToString() == "MYSSKFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark],date1  FROM   [MySSKTblFollowup] where [IsRead]=1 and Remark='" + UserID + "' and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "') or date1 is null )";
                    }
                    else if (DropDownList1.SelectedItem.ToString() == "MoryaFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark] ,date1 FROM   [MoryaTblFollowup] where [IsRead]=1 and Remark='" + UserID + "' and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "') or date1 is null) ";

                    }
                    else if (DropDownList1.SelectedItem.ToString() == "EZACUSFollowup")
                    {
                        selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [AdminLogin] where [AdminID]=[Remark]) as [Remark],date1  FROM   [EZACUSTblFollowup] where [IsRead]=1 and Remark='" + UserID + "' and ((CONVERT(date,date1 , 105) >='" + txt_fromDate.Text + "') AND(CONVERT(date,date1 , 105) <='" + txt_toDate.Text + "')  or date1 is null)";

                    }
                }
                //  string selectquery = "SELECT [PKId],[Name],[Mobile],[City],[IsRead],[Remark] FROM [admin_moryatools].[admin_moryatools].[EZACUSTblFollowup] where [IsRead]=0";
                SqlDataAdapter danewdata = new SqlDataAdapter(selectquery, sqlconnect);
                DataTable dtStudent = new DataTable();

                danewdata.Fill(dtStudent);

                if (dtStudent != null)
                {
                    if (dtStudent.Rows.Count > 0)
                    {
                        Session["dtProduct"] = dtStudent;
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
            catch { }
            finally { }
        }
    }



    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtStudent = new DataTable();

            //try
            //{

            //    string UserID = ddlUser.SelectedValue.ToString();
            //    string selectquery = "";
            //    if (ddlUser.SelectedIndex == 0)
            //    {
            //        if (DropDownList1.SelectedItem.ToString() == "MYSSKFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[MySSKTblFollowup] where [IsRead]=1";
            //        }
            //        else if (DropDownList1.SelectedItem.ToString() == "MoryaFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[MoryaTblFollowup] where [IsRead]=1";

            //        }
            //        else if (DropDownList1.SelectedItem.ToString() == "EZACUSFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[EZACUSTblFollowup] where [IsRead]=1";

            //        }
            //    }
            //    else
            //    {
            //        if (DropDownList1.SelectedItem.ToString() == "MYSSKFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[MySSKTblFollowup] where [IsRead]=1 and Remark='" + UserID + "'";
            //        }
            //        else if (DropDownList1.SelectedItem.ToString() == "MoryaFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[MoryaTblFollowup] where [IsRead]=1 and Remark='" + UserID + "'";

            //        }
            //        else if (DropDownList1.SelectedItem.ToString() == "EZACUSFollowup")
            //        {
            //            selectquery = " SELECT [PKId],[Name],[Mobile],[City],[IsRead],(select [name] from [dbo].[AdminLogin] where [AdminID]=[Remark]) as [Remark]  FROM   [admin_moryatools].[admin_moryatools].[EZACUSTblFollowup] where [IsRead]=1 and Remark='" + UserID + "'";

            //        }
            //    }
            //    //  string selectquery = "SELECT [PKId],[Name],[Mobile],[City],[IsRead],[Remark] FROM [admin_moryatools].[admin_moryatools].[EZACUSTblFollowup] where [IsRead]=0";
            //    SqlDataAdapter danewdata = new SqlDataAdapter(selectquery, sqlconnect);

            //    danewdata.Fill(dtStudent);

            //    if (dtStudent != null)
            //    {
            //        if (dtStudent.Rows.Count > 0)
            //        {

            //            repNews.DataSource = dtStudent;
            //            repNews.DataBind();
            //        }
            //        else
            //        {
            //            repNews.DataSource = null;
            //            repNews.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        repNews.DataSource = null;
            //        repNews.DataBind();
            //    }
            //}
            //catch { }
            //finally { }

            if (dtStudent != null)
            {
                Response.Redirect("ExcelExport.aspx?filename=User_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");

                //string fileName = "User_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls";
                //this.GridView1.DataSource = dtStudent;
                //this.GridView1.DataBind();
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                //Response.ContentType = "application/ms-excel";
                //System.IO.StringWriter sw = new System.IO.StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);
                //this.GridView1.RenderControl(htw);
                //Response.Write(sw.ToString());
                //Response.End();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        DropDownList1_SelectedIndexChanged(null, null);
    }
}
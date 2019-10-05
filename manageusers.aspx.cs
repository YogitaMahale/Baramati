
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class manageusers : System.Web.UI.Page
{
    //paging
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;
    //--


    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage Users";
        //if (!Page.IsPostBack)
        //{
        //    SelectAllActiveUserList();
        //    SelectAllNotActiveUserList();
        //}
        //-------------
        if (Page.IsPostBack) return;
        search();
    }

    //public void SelectAllActiveUserList()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtuser = new DataTable();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "userregistration_SelectAllAdmin";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        ConnectionString.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dtuser);
    //        if (dtuser != null)
    //        {
    //            if (dtuser.Rows.Count > 0)
    //            {
    //                repUser.Visible = true;
    //                repUser.DataSource = dtuser;
    //                repUser.DataBind();
    //            }
    //            else
    //            {
    //                repUser.DataSource = null;
    //                repUser.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            repUser.DataSource = null;
    //            repUser.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }

    //}

    //public void SelectAllNotActiveUserList()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtuser = new DataTable();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "userregistration_SelectAllAdminNotActive";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        ConnectionString.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dtuser);
    //        if (dtuser != null)
    //        {
    //            if (dtuser.Rows.Count > 0)
    //            {
    //                repUserNotActive.Visible = true;
    //                repUserNotActive.DataSource = dtuser;
    //                repUserNotActive.DataBind();
    //            }
    //            else
    //            {
    //                repUserNotActive.DataSource = null;
    //                repUserNotActive.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            repUserNotActive.DataSource = null;
    //            repUserNotActive.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }
    //}

    public bool User_Delete(Int64 UserID)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userregistration_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", UserID);
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

    protected void lnkNotActiveUserDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkNotActiveUserDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkNotActiveUserDelete.NamingContainer;
        Int64 UserId = Convert.ToInt64(lnkNotActiveUserDelete.CommandArgument);
        bool yes = User_Delete(UserId);
        spnMessage.Visible = true;
        if (yes)
        {
            search();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Not Deleted";
        }
    }

    //protected void lnkActiveUserDelete_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnkActiveUserDelete = (LinkButton)sender;
    //    RepeaterItem item = (RepeaterItem)lnkActiveUserDelete.NamingContainer;
    //    Int64 UserId = Convert.ToInt64(lnkActiveUserDelete.CommandArgument);
    //    bool yes = User_Delete(UserId);
    //    spnMessage.Visible = true;
    //    if (yes)
    //    {
    //        SelectAllActiveUserList();
    //        SelectAllNotActiveUserList();
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "User Deleted Successfully";
    //    }
    //    else
    //    {
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "User Not Deleted";
    //    }
    //}

    public bool User_IsActive(Int64 UserID, Boolean IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userregistration_IsActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", UserID);
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

    protected void cbIsNotActiveUser_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsNotActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsNotActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfNonActiveUser") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfNonActiveUser") as HiddenField).Value);
                bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsNotActiveUser") as CheckBox).Checked);
                bool yes = User_IsActive(UserID, cbIsActive);
                spnMessage.Visible = true;
                if (yes)
                {
                    search();
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "User Updated Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "User Not Updated";
                }
            }
        }
    }

    //protected void cbIsActiveUser_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox cbIsActiveUser = (CheckBox)sender;
    //    RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
    //    if (item != null)
    //    {
    //        if (!string.IsNullOrEmpty((item.FindControl("hfActiveUser") as HiddenField).Value))
    //        {
    //            Int64 UserID = int.Parse((item.FindControl("hfActiveUser") as HiddenField).Value);
    //            bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsActiveUser") as CheckBox).Checked);
    //            bool yes = User_IsActive(UserID, cbIsActive);
    //            spnMessage.Visible = true;
    //            if (yes)
    //            {
    //                SelectAllActiveUserList();
    //                SelectAllNotActiveUserList();
    //                spnMessage.Style.Add("color", "green");
    //                spnMessage.InnerText = "User Updated Successfully";
    //            }
    //            else
    //            {
    //                spnMessage.Style.Add("color", "red");
    //                spnMessage.InnerText = "User Not Updated";
    //            }
    //        }
    //    }
    //}


    //paging


    #region Paging
    private int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] == null)
            {
                return 0;
            }
            return ((int)ViewState["CurrentPage"]);
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }
    // Bind PagedDataSource into Repeater
    private void BindDataIntoRepeater(DataTable dtData)
    {
        //var dt = SelectAllActiveUser();
        var dt = dtData;
        _pgsource.DataSource = dt.DefaultView;
        _pgsource.AllowPaging = true;
        // Number of items to be displayed in the Repeater
        _pgsource.PageSize = _pageSize;
        _pgsource.CurrentPageIndex = CurrentPage;
        // Keep the Total pages in View State
        ViewState["TotalPages"] = _pgsource.PageCount;
        // Example: "Page 1 of 10"
        lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
        // Enable First, Last, Previous, Next buttons
        lbPrevious.Enabled = !_pgsource.IsFirstPage;
        lbNext.Enabled = !_pgsource.IsLastPage;
        lbFirst.Enabled = !_pgsource.IsFirstPage;
        lbLast.Enabled = !_pgsource.IsLastPage;

        repUser.DataSource = null;
        repUser.DataBind();
        // Bind data into repeater
        repUser.DataSource = _pgsource;
        repUser.DataBind();

        // Call the function to do paging
        HandlePaging();
    }

    private void HandlePaging()
    {
        var dt = new DataTable();
        dt.Columns.Add("PageIndex"); //Start from 0
        dt.Columns.Add("PageText"); //Start from 1

        _firstIndex = CurrentPage - 5;
        if (CurrentPage > 5)
            _lastIndex = CurrentPage + 5;
        else
            _lastIndex = 10;

        // Check last page is greater than total page then reduced it to total no. of page is last index
        if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            _firstIndex = _lastIndex - 10;
        }

        if (_firstIndex < 0)
            _firstIndex = 0;

        // Now creating page number based on above first and last page index
        for (var i = _firstIndex; i < _lastIndex; i++)
        {
            var dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        rptPaging.DataSource = null;
        rptPaging.DataBind();
        rptPaging.DataSource = dt;
        rptPaging.DataBind();
    }

    protected void lbFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        //BindDataIntoRepeater(SelectAllActiveUser());
        //txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        //BindDataIntoRepeater(SelectAllActiveUser());
        //txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        //BindDataIntoRepeater(SelectAllActiveUser());
        //  txtSearch_TextChanged(null, null);
        search();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        // txtSearch_TextChanged(null, null);
        //BindDataIntoRepeater(SelectAllActiveUser());
        search();
    }
    #endregion

    protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (!e.CommandName.Equals("newPage")) return;
        CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
        //  txtSearch_TextChanged(null, null);
        //BindDataIntoRepeater(SelectAllActiveUser());
        search();
    }

    protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
        if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
        lnkPage.Enabled = false;
        lnkPage.BackColor = Color.FromName("#00FF00");
    }
    public void search()
    {
        #region
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable ds = new DataTable();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "userregistration_SelectAllAdmin";

            cmd.CommandType = CommandType.StoredProcedure;
            if (txtSearch.Text == "")
            {
                cmd.Parameters.Add("@seachtext", null);
            }
            else
            {
                cmd.Parameters.Add("@seachtext", txtSearch.Text);

            }
            cmd.Parameters.Add("@isactive", Convert.ToInt64(ddlUserstatus.SelectedValue.ToString()));
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Rows.Count > 0)
                {
                    Session["dtProduct"] = ds;
                }
            }

            lbPrevious.Enabled = true;
            lbNext.Enabled = false;
            lbFirst.Enabled = true;
            lbLast.Enabled = false;
            //CurrentPage = 0;
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
        finally
        {
            ConnectionString.Close();
        }
        BindDataIntoRepeater(ds);
        #endregion
    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        search();
    }
    protected void ddlSelectEntry_SelectedIndexChanged(object sender, EventArgs e)
    {

        //_pageSize =int.Parse(ddlSelectEntry.SelectedValue.ToString());
        ////txtSearch_TextChanged(null, null);
        //search();
    }
    protected void ddlUserstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        search();
    }


    protected void btnExcelExport_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["dtProduct"] != null)
            {
                // Response.Redirect("ExcelExport.aspx?filename=Dealer's List.xls");
                repUser.DataSource = Session["dtProduct"];
                repUser.DataBind();
            }




            foreach (RepeaterItem item in repUser.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var checkBox = (CheckBox)item.FindControl("cbIsGuest");
                    var checkBox1 = (CheckBox)item.FindControl("cbIsNotActiveUser");
                    var xx = item.FindControl("hfNonActiveUser");
                    var status = (Label)item.FindControl("lblStatus");
                    //var img = (System.Web.UI.WebControls.Image)item.FindControl("imgStatus");
                    var lButton = (LinkButton)item.FindControl("lnkNotActiveUserDelete");
                    //Button grn = (Button)item.FindControl("btnGRN");
                    //Button view = (Button)item.FindControl("btnView");
                    //view.Visible = false;
                    xx.Visible = false;
                    lButton.Visible = false;
                    checkBox.Visible = false;
                    checkBox1.Visible = false;
                    status.Visible = false;
                    //img.Visible = false;
                }
                //Do something with your checkbox...


            }

            //repDealerActive.Controls.RemoveAt(0);
            //repDealerActive.Controls.RemoveAt(8);
            //repDealerActive.Controls.RemoveAt(12);



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Users List.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);




            repUser.RenderControl(htmlWrite);
            Response.Write("<table><thead>"
                                    + "<tr>"
                                      + "  <th >User Name</th>"
                                        + "<th >Email</th>"

                                        + "<th >Phone No.</th>"
                                        + "<th >Password</th>"

                                        + "<th>Reg. Date</th>"

                                        + "<th>Guest</th>"


                                        + "<th>Active</th>"
                                        + "<th>Status</th>"

                                        + "<th>Created Date</th>"

                                        //+ "<th>User Status</th>"

                                        //+"<th>Action</th>"

                                    + "</tr>"
                                + "</thead>");
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
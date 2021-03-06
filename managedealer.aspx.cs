﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class managedealer1 : System.Web.UI.Page
{
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;


    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
       

        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage Dealers";

        //if (!Page.IsPostBack)
        //{

        //   // SelectAllActiveUser();
        //    SelectAllNotActiveUser();
        //}

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Dealer Updated Successfully";
        }
        //if (!Page.IsPostBack)
        //{
        //      dt = SelectAllActiveUser();
        //}




        //-------------
        if (Page.IsPostBack) return;
        
            //BindDataIntoRepeater(SelectAllActiveUser());
        search();
        //txtSearch_TextChanged(null, null);
    }

    //public void SelectAllActiveUser()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "dealer_SelectAllAdmin";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        ConnectionString.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);
    //        if (ds.Tables[0] != null)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                divActiveSaveAll.Visible = true;
    //                repDealerActive.Visible = true;
    //                repDealerActive.DataSource = ds.Tables[0];
    //                repDealerActive.DataBind();
    //            }
    //            else
    //            {
    //                divActiveSaveAll.Visible = false;
    //                repDealerActive.DataSource = null;
    //                repDealerActive.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            repDealerActive.DataSource = null;
    //            repDealerActive.DataBind();
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

    public void SelectAllNotActiveUser()
    {
        /*
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealer_SelectAllAdminNotActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divNonActiveSaveAll.Visible = true;
                    repDealerNotActive.Visible = true;
                    repDealerNotActive.DataSource = ds.Tables[0];
                    repDealerNotActive.DataBind();
                }
                else
                {
                    divNonActiveSaveAll.Visible = false;
                    repDealerNotActive.DataSource = null;
                    repDealerNotActive.DataBind();
                }
            }
            else
            {
                repDealerNotActive.DataSource = null;
                repDealerNotActive.DataBind();
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
         */ 
    }

    protected void cbIsActiveUser_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfActiveUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfActiveUserId") as HiddenField).Value);
                bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsActiveUser") as CheckBox).Checked);
                bool yes = User_IsActive(UserID, cbIsActive);
                spnMessage.Visible = true;
                if (yes)
                {
                    //SelectAllActiveUser();
                    //SelectAllNotActiveUser();
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

    //protected void cbNonIsActive_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox cbNonIsActive = (CheckBox)sender;
    //    RepeaterItem item = (RepeaterItem)cbNonIsActive.NamingContainer;
    //    if (item != null)
    //    {
    //        if (!string.IsNullOrEmpty((item.FindControl("hfNonActiveUserId") as HiddenField).Value))
    //        {
    //            Int64 UserID = int.Parse((item.FindControl("hfNonActiveUserId") as HiddenField).Value);
    //            bool cbIsActive = Convert.ToBoolean((item.FindControl("cbNonIsActive") as CheckBox).Checked);
    //            bool yes = User_IsActive(UserID, cbIsActive);
    //            spnMessage.Visible = true;
    //            if (yes)
    //            {
    //                SelectAllActiveUser();
    //                SelectAllNotActiveUser();
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

    public bool User_IsActive(Int64 UserID, Boolean IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_IsActive";
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

    //protected void lnkNotActiveDelete_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnkNotActiveDelete = (LinkButton)sender;
    //    RepeaterItem item = (RepeaterItem)lnkNotActiveDelete.NamingContainer;
    //    Int64 UserId = Convert.ToInt64(lnkNotActiveDelete.CommandArgument);
    //    bool yes = User_Delete(UserId);
    //    spnMessage.Visible = true;
    //    if (yes)
    //    {
    //        SelectAllActiveUser();
    //        SelectAllNotActiveUser();
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "User Deleted Successfully";
    //    }
    //    else
    //    {
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "User Not Deleted";
    //    }
    //}

    protected void lnkActiveUserDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkActiveUserDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkActiveUserDelete.NamingContainer;
        Int64 UserId = Convert.ToInt64(lnkActiveUserDelete.CommandArgument);
        bool yes = User_Delete(UserId);
        spnMessage.Visible = true;
        if (yes)
        {
            //SelectAllActiveUser();
            //SelectAllNotActiveUser();
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

    public bool User_Delete(Int64 UserID)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_Delete";
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

    protected void repDealerNotActive_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/editdealerprofile.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "did").ToString(), true));
        }
    }

    protected void repDealerActive_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/editdealerprofile.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "did").ToString(), true));
        }
    }

    protected void btnNonActiveSave_Click(object sender, EventArgs e)
    {
        /*
        bool yes = false;
        foreach (RepeaterItem item in repDealerNotActive.Items)
        {
            CheckBox chkContainer = (CheckBox)item.FindControl("chkContainer");
            if (chkContainer.Checked)
            {
                string DealerId = chkContainer.Attributes["attr-ID"];
                CheckBox cbNonSuperStockiest = (CheckBox)item.FindControl("cbNonSuperStockiest");
                CheckBox cbNonDealer = (CheckBox)item.FindControl("cbNonDealer");
                CheckBox cbNonWholesaler = (CheckBox)item.FindControl("cbNonWholesaler");
                CheckBox cbNonCustomer = (CheckBox)item.FindControl("cbNonCustomer");
                yes = Dealer_Update_UserType(Convert.ToInt64(DealerId), cbNonSuperStockiest.Checked, cbNonDealer.Checked, cbNonWholesaler.Checked, cbNonCustomer.Checked);
            }
        }
        spnMessage.Visible = true;
        spnMessage.Style.Add("color", "green");
        spnMessage.InnerText = "Dealer Information Updated Successfully";
        SelectAllNotActiveUser();
         */ 
    }

    protected void btnActiveSave_Click(object sender, EventArgs e)
    {
        bool yes = false;
        foreach (RepeaterItem item in repDealerActive.Items)
        {
            CheckBox chkContainer = (CheckBox)item.FindControl("chkContainerActive");
            if (chkContainer.Checked)
            {
                string DealerId = chkContainer.Attributes["attr-ID"];
                CheckBox cbNonSuperStockiest = (CheckBox)item.FindControl("cbActiveSuperStockiest");
                CheckBox cbNonDealer = (CheckBox)item.FindControl("cbActiveDealer");
                CheckBox cbNonWholesaler = (CheckBox)item.FindControl("cbActiveWholesaler");
                CheckBox cbNonCustomer = (CheckBox)item.FindControl("cbActiveCustomer");
                yes = Dealer_Update_UserType(Convert.ToInt64(DealerId), cbNonSuperStockiest.Checked, cbNonDealer.Checked, cbNonWholesaler.Checked, cbNonCustomer.Checked);
            }
        }
        spnMessage.Visible = true;
        spnMessage.Style.Add("color", "green");
        spnMessage.InnerText = "Dealer Information Updated Successfully";
        SelectAllNotActiveUser();
    }

    private bool Dealer_Update_UserType(Int64 DealerId, bool SuperStockiest, bool Dealer, bool Wholesaler, bool Customer)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_UpdateuserType";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", DealerId);
            cmd.Parameters.AddWithValue("@superstockiest", SuperStockiest);
            cmd.Parameters.AddWithValue("@dealer", Dealer);
            cmd.Parameters.AddWithValue("@wholesaler", Wholesaler);
            cmd.Parameters.AddWithValue("@customer", Customer);
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


    //public DataTable SelectAllActiveUser()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable ds = new DataTable();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "dealer_SelectAllAdmin";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = ConnectionString;
    //        ConnectionString.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    finally
    //    {
    //        ConnectionString.Close();
    //    }
    //    return ds;
    //}
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

        repDealerActive.DataSource = null;
        repDealerActive.DataBind();
        // Bind data into repeater
        repDealerActive.DataSource = _pgsource;
        repDealerActive.DataBind();

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

        rptPaging.DataSource =null;
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
            cmd.CommandText = "dealer_SelectAllAdminPagingNew";

            cmd.CommandType = CommandType.StoredProcedure;
            if (txtSearch.Text == "")
            {
                cmd.Parameters.AddWithValue("@seachtext", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@seachtext", txtSearch.Text);
               
            }
            cmd.Parameters.AddWithValue("@isactive", Convert.ToInt64(ddlUserstatus.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@isonline", Convert.ToInt64(ddlOnlineStatus.SelectedValue.ToString()));
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
                repDealerActive.DataSource = Session["dtProduct"];
                repDealerActive.DataBind();
            }




            foreach (RepeaterItem item in repDealerActive.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var checkBox = (CheckBox)item.FindControl("chkContainerActive");
                    var checkBox1 = (CheckBox)item.FindControl("cbIsActiveUser");
                    var xx = item.FindControl("hfActiveUserId");
                    var img = (System.Web.UI.WebControls.Image)item.FindControl("imgStatus");
                    var lButton = (LinkButton)item.FindControl("lnkActiveUserDelete");
                    var status = (Label)item.FindControl("lblStatus");

                    //Button grn = (Button)item.FindControl("btnGRN");
                    //Button view = (Button)item.FindControl("btnView");
                    //view.Visible = false;
                    xx.Visible = false;
                    lButton.Visible = false;
                    checkBox.Visible = false;
                    checkBox1.Visible = false;
                    img.Visible = false;
                    status.Visible = false;

                }
                //Do something with your checkbox...


            }

            //repDealerActive.Controls.RemoveAt(0);
            //repDealerActive.Controls.RemoveAt(8);
            //repDealerActive.Controls.RemoveAt(12);



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Dealers List.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);




            repDealerActive.RenderControl(htmlWrite);
            Response.Write("<table><thead>"
                                    +"<tr>"
                                      +"  <th ></th>"
                                        +"<th >Dealer</th>"
                                        
                                        +"<th >Login MNo.</th>"
                                        +"<th >Password</th>"
                                        
                                        +"<th>WhatsApp MNo.</th>"
                                        
                                        +"<th>Login Count</th>"
                                        
                                        
                                        +"<th>State City</th>"
                                        
                                        +"<th>Regn. Date</th>"
                                        
                                        +"<th>Access</th>"
                                        
                                        +"<th>User Type</th>"
                                        
                                        +"<th>Status</th>"
                                        
                                        +"<th>Agent</th>"
                                        
                                        //+"<th>User Status</th>"
                                        
                                        //+"<th>Action</th>"
                                        
                                    +"</tr>"                                   
                                +"</thead>");
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



    protected void btnNewDealer_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/editdealerprofile.aspx"));

    }
}
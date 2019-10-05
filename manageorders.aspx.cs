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

public partial class manageorders : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    common ocommon = new common();

    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        //ddlPaymentType.SelectedIndex = 0;
         

        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Confirmed Order's";
        if (!Page.IsPostBack)
        {
            //SelectAll(0);
            search();
        }

        if (Request.QueryString["mode"] == "u")
        {
            //spnMessage.Visible = true;
            //spnMessage.Style.Add("color", "green");
            //spnMessage.InnerText = "Product News Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Payment add Successfully";
        }
    }

    public void SelectAll(Int64 id)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Order_SelectAll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paymentStatus", id);
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            repTodayOrder.DataSource = null;
            repTodayOrder.DataBind();
            //repYesterDayOrder.DataSource = null;
            //repYesterDayOrder.DataBind();
            //repRemaining.DataSource = null;
            //repRemaining.DataBind();


            if (ds.Tables != null)
            {
                /* Todays Order */
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        repTodayOrder.DataSource = ds.Tables[0];
                        repTodayOrder.DataBind();
                    }
                }

                ///* Yesterday Order */
                //if (ds.Tables[1] != null)
                //{
                //    if (ds.Tables[1].Rows.Count > 0)
                //    {
                //        repYesterDayOrder.DataSource = ds.Tables[1];
                //        repYesterDayOrder.DataBind();
                //    }
                //}

                ///* Remaining  Order */
                //if (ds.Tables[2] != null)
                //{
                //    if (ds.Tables[2].Rows.Count > 0)
                //    {
                //        repRemaining.DataSource = ds.Tables[2];
                //        repRemaining.DataBind();
                //    }
                //}
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

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkDelete.NamingContainer;
        Int64 OrderId = Convert.ToInt64(lnkDelete.CommandArgument);
        bool yes = (new Cls_orders_b().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
            GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Not Deleted";
        }
    }

    protected void lnkYesterdayDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkYesterdayDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkYesterdayDelete.NamingContainer;
        Int64 OrderId = Convert.ToInt64(lnkYesterdayDelete.CommandArgument);
        bool yes = (new Cls_orders_b().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
            GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Not Deleted";
        }
    }

    protected void lnkRemainingDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkRemainingDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkRemainingDelete.NamingContainer;
        Int64 OrderId = Convert.ToInt64(lnkRemainingDelete.CommandArgument);
        bool yes = (new Cls_orders_b().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
            GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Not Deleted";
        }
    }

    private void GetOrderProducts(Int64 OrderId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable dtProduct = new DataTable();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from orderproducts Where oid=" + OrderId;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtProduct);
            con.Close();
            if (dtProduct != null)
            {
                if (dtProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {
                        UpdateStocksUsingProductId(OrderId, Convert.ToInt64(dtProduct.Rows[i]["pid"]), Convert.ToInt64(dtProduct.Rows[i]["quantites"]));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
        }
    }

    private void UpdateStocksUsingProductId(Int64 OrderId, Int64 ProductId, Int64 Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "order_RealProductstockmaintainMaintain";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@oid", OrderId);
            cmd.Parameters.AddWithValue("@pid", ProductId);
            cmd.Parameters.AddWithValue("@qty", Quantites);
            con.Open();
            cmd.ExecuteNonQuery();
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

    protected void cbRemainingPaymentPaid_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        bool cbRemainingPaymentPaid = Convert.ToBoolean((item.FindControl("cbRemainingPaymentPaid") as CheckBox).Checked);
        bool yes = (Order_IsBillPaidorNot(OrderId, cbRemainingPaymentPaid));
        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Updated Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }

    protected void cbYesterdayPaymentPaid_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        bool cbYesterdayPaymentPaid = Convert.ToBoolean((item.FindControl("cbYesterdayPaymentPaid") as CheckBox).Checked);
        bool yes = (Order_IsBillPaidorNot(OrderId, cbYesterdayPaymentPaid));
        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Updated Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }

    protected void cbTodayPaymentPaid_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        bool cbTodayPaymentPaid = Convert.ToBoolean((item.FindControl("cbTodayPaymentPaid") as CheckBox).Checked);
        bool yes = (Order_IsBillPaidorNot(OrderId, cbTodayPaymentPaid));
        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Updated Successfully";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }

    public bool Order_IsBillPaidorNot(Int64 OrderId, Boolean IsPayment)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "order_Isbillpaidornot";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@oid", OrderId);
            cmd.Parameters.AddWithValue("@billpaidornot", IsPayment);
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

    protected void repTodayOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));


            HyperLink hlPayment = (HyperLink)e.Item.FindControl("hlPayment");
            hlPayment.NavigateUrl = Page.ResolveUrl("~/frm_Payment.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

            Label lblRemaining = (Label)e.Item.FindControl("lblRemaining");

            if (Convert.ToDouble(lblRemaining.Text) == 0)
            {
                hlPayment.Visible = false;
            }
            else
            {
                hlPayment.Visible = true;
            }


            //---------dropbox-----------
            Label l_orderId = (Label)e.Item.FindControl("lbl_orderstatus");
            DropDownList d = (DropDownList)e.Item.FindControl("ddlOrderStatus");

            d.DataSource = load_orderStatus();
            d.DataBind();
            d.SelectedValue = l_orderId.Text;
            //d.SelectedValue = DirectCast(e.Item.DataItem, DataRowView)("ContactTypeID").ToString()
        }
    }

    protected void repYesterDayOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

            //---------dropbox-----------
            Label l_orderId = (Label)e.Item.FindControl("lbl_orderstatus");
            DropDownList d = (DropDownList)e.Item.FindControl("ddlOrderStatus");

            d.DataSource = load_orderStatus();
            d.DataBind();
            d.SelectedValue = l_orderId.Text;
            //d.SelectedValue = DirectCast(e.Item.DataItem, DataRowView)("ContactTypeID").ToString()
        }
    }

    protected void repRemaining_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
            //---------dropbox-----------
            Label l_orderId = (Label)e.Item.FindControl("lbl_orderstatus");
            DropDownList d = (DropDownList)e.Item.FindControl("ddlOrderStatus");

            d.DataSource = load_orderStatus();
            d.DataBind();
            d.SelectedValue = l_orderId.Text;
            //d.SelectedValue = DirectCast(e.Item.DataItem, DataRowView)("ContactTypeID").ToString()
        }
    }

    protected void btnTodayOrder_Click(object sender, EventArgs e)
    {
        bool yes = false;
        foreach (RepeaterItem item in repTodayOrder.Items)
        {
            CheckBox chkTodayOrder = (CheckBox)item.FindControl("chkTodayOrder");
            if (chkTodayOrder.Checked)
            {
                string OrderId = chkTodayOrder.Attributes["attr-ID"];
                TextBox txtComments = (TextBox)item.FindControl("txtComments");
                yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());
            }
        }
        if (yes == true)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
    }

   

    public bool Order_CommentUpdate(Int64 OId, string Comment)
    {
        bool yes = false;
        SqlConnection ConnectionString = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "order_CommentUpdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@oid", OId);
            cmd.Parameters.AddWithValue("@comment", Comment);

            ConnectionString.Open();
            count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                yes = true;
            }
            else
            {
                yes = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            yes = false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return yes;
    }

    protected void txtRemainingComments_TextChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as TextBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        TextBox txtComments = (TextBox)item.FindControl("txtRemainingComments");
        bool yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());

        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }

    protected void txtTodayComments_TextChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as TextBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        TextBox txtComments = (TextBox)item.FindControl("txtTodayComments");
        bool yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());

        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }

    protected void txtYesterDayComments_TextChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as TextBox).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblOId") as Label).Text);
        TextBox txtComments = (TextBox)item.FindControl("txtYesterDayComments");
        bool yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());

        spnMessage.Visible = true;
        if (yes)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }
    protected DataTable load_orderStatus()
    {
        DataTable data = new DataTable();
        try
        {

            Cls_OrderStatusmaster_b obj = new Cls_OrderStatusmaster_b();
            data = obj.SelectAll();
            DataRow dr;
            dr = data.NewRow();
            dr["id"] = "0";
            dr["type"] = "--Select--";
            data.Rows.Add(dr);

        }
        catch { }
        finally { con.Close(); }

        return data;
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as DropDownList).Parent as RepeaterItem;
        Int64 OrderId = int.Parse((item.FindControl("lblorderid") as Label).Text);
        Int64 OrderStatusVal = Convert.ToInt64((item.FindControl("ddlOrderStatus") as DropDownList).SelectedValue);
        try
        {
            con.Close();
            con.Open();
            string cmdText = "update orders set OrderStatus='" + OrderStatusVal + "' where oid=" + OrderId + "";
            SqlCommand cmd = new SqlCommand(cmdText, con);
            int t = cmd.ExecuteNonQuery();
            spnMessage.Visible = true;
            if (t > 0)
            {
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Order Status Updated Successfully";
                SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
            }
            else
            {
                spnMessage.Style.Add("color", "red");
                spnMessage.InnerText = "Order Status Not Updated";
            }
            con.Close();
        }
        catch { }
        finally { con.Close(); }

    }
    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        search();
        //try
        //{
        //    SelectAll(Convert.ToInt64(ddlPaymentType.SelectedValue.ToString()));
        //}
        //catch { }
    }


    //paging------------------
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

        repTodayOrder.DataSource = null;
        repTodayOrder.DataBind();
        // Bind data into repeater
        repTodayOrder.DataSource = _pgsource;
        repTodayOrder.DataBind();

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
        lnkPage.BackColor = System.Drawing.Color.FromName("#00FF00");
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
            cmd.CommandText = "Order_SelectAll";

            cmd.CommandType = CommandType.StoredProcedure;
            
            Int64 idd = Convert.ToInt64(ddlPaymentType.SelectedValue.ToString());
             
            cmd.Parameters.Add("@paymentStatus", idd);
            //if (txtSearch.Text == "")
            //{
            //    cmd.Parameters.Add("@seachtext", null);
            //}
            //else
            //{
            //    cmd.Parameters.Add("@seachtext", txtSearch.Text);

            //}
        
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);


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


}
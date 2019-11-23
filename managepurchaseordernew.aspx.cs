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

public partial class managepurchaseordernew : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {


        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage Purchase Order's";
        if (!Page.IsPostBack)
        {
            SelectAll();
        }
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["mode"] == "u")
            {

                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Order Updated Successfully";

            }
            else if (Request.QueryString["mode"] == "i")
            {

                spnMessage.Visible = true;
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Order Inserted Successfully";
            }
        }
    }

    public void SelectAll()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet   ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            clsPurchaseOrderHeader_b obj = new clsPurchaseOrderHeader_b();
            ds = obj.SelectAll();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "Customer_Order_SelectAll";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = con;
            //con.Open();
            //da = new SqlDataAdapter(cmd);
            //da.Fill(ds);
            //-------------
            repTodayOrder.DataSource = null;
            repTodayOrder.DataBind();
            repYesterDayOrder.DataSource = null;
            repYesterDayOrder.DataBind();
            repRemaining.DataSource = null;
            repRemaining.DataBind();
            //------------
            if (ds != null)
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

                /* Yesterday Order */
                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        repYesterDayOrder.DataSource = ds.Tables[1];
                        repYesterDayOrder.DataBind();
                    }
                }

                /* Remaining  Order */
                if (ds.Tables[2] != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        repRemaining.DataSource = ds.Tables[2];
                        repRemaining.DataBind();
                    }
                }
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
        bool yes = (new clsPurchaseOrderHeader_b ().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
          //  GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll();
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
        bool yes = (new clsPurchaseOrderHeader_b ().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
           // GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll();
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
        bool yes = (new clsPurchaseOrderHeader_b ().Delete(OrderId));
        spnMessage.Visible = true;
        if (yes)
        {
          //  GetOrderProducts(OrderId);
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Deleted Successfully";
            SelectAll();
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Order Not Deleted";
        }
    }

    //private void GetOrderProducts(Int64 OrderId)
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtProduct = new DataTable();
    //    SqlDataAdapter da;
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "Select * from Customer_orderproducts Where oid=" + OrderId;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.Connection = con;
    //        con.Open();
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dtProduct);
    //        con.Close();
    //        if (dtProduct != null)
    //        {
    //            if (dtProduct.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dtProduct.Rows.Count; i++)
    //                {
    //                    UpdateStocksUsingProductId(OrderId, Convert.ToInt64(dtProduct.Rows[i]["pid"]), Convert.ToInt64(dtProduct.Rows[i]["quantites"]));
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //}

    private void UpdateStocksUsingProductId(Int64 OrderId, Int64 ProductId, Int64 Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "order_productstockmaintain";
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
            SelectAll();
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
            SelectAll();
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
            SelectAll();
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
            //HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            //hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            //HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            //hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

            HyperLink hlEditOrder = (HyperLink)e.Item.FindControl("hlEditOrder");
            hlEditOrder.NavigateUrl = Page.ResolveUrl("~/addeditponew.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/Customer_orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

            //HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            //hlInvoice.NavigateUrl = Page.ResolveUrl("~/Customer_orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
        }
    }

    protected void repYesterDayOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            //hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            //HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            //hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));


            HyperLink hlEditOrder = (HyperLink)e.Item.FindControl("hlEditOrder");
            hlEditOrder.NavigateUrl = Page.ResolveUrl("~/addeditponew.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));



            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/Customer_orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

        }
    }

    protected void repRemaining_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //HyperLink hlDealerImage = (HyperLink)e.Item.FindControl("hlDealerImage");
            //hlDealerImage.Attributes.Add("src", "http://moryaapp.moryatools.com/uploads/orderimage/" + DataBinder.Eval(e.Item.DataItem, "dealerimage").ToString());
            //HyperLink hlMoryaImage = (HyperLink)e.Item.FindControl("hlMoryaImage");
            //hlMoryaImage.NavigateUrl = Page.ResolveUrl("~/ordermoryainvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));
            HyperLink hlEditOrder = (HyperLink)e.Item.FindControl("hlEditOrder");
            hlEditOrder.NavigateUrl = Page.ResolveUrl("~/addeditponew.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));


            HyperLink hlInvoice = (HyperLink)e.Item.FindControl("hlInvoice");
            hlInvoice.NavigateUrl = Page.ResolveUrl("~/Customer_orderinvoice.aspx?oid=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "oid").ToString(), true));

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
            SelectAll();
        }
    }

    protected void btnYesterdayOrder_Click(object sender, EventArgs e)
    {
        bool yes = false;
        foreach (RepeaterItem item in repYesterDayOrder.Items)
        {
            CheckBox chkYesterDayOrder = (CheckBox)item.FindControl("chkYesterDayOrder");
            if (chkYesterDayOrder.Checked)
            {
                string OrderId = chkYesterDayOrder.Attributes["attr-ID"];
                TextBox txtComments = (TextBox)item.FindControl("txtComments");
                yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());
            }
        }
        if (yes == true)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll();
        }
    }

    protected void btnRemainingOrder_Click(object sender, EventArgs e)
    {
        bool yes = false;
        foreach (RepeaterItem item in repRemaining.Items)
        {
            CheckBox chkRemainingOrder = (CheckBox)item.FindControl("chkRemainingOrder");
            if (chkRemainingOrder.Checked)
            {
                string OrderId = chkRemainingOrder.Attributes["attr-ID"];
                TextBox txtComments = (TextBox)item.FindControl("txtComments");
                yes = Order_CommentUpdate(Convert.ToInt64(OrderId), txtComments.Text.Trim());
            }
        }
        if (yes == true)
        {
            spnMessage.Visible = true;
            spnMessage.InnerText = "Comment Updated Successfully.";
            SelectAll();
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
            SelectAll();
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
            SelectAll();
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
            SelectAll();
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Order Not Updated";
        }
    }
    

    private void Product_StockUpdate(Int64 ProductId, int Quantites)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_Realstockupdate";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@productid", ProductId);
            cmd.Parameters.AddWithValue("@quantites", Quantites);
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

    protected void btnNewOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditponew.aspx"));

    }
}
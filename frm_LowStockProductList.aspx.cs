using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class frm_LowStockProductList : System.Web.UI.Page
{
    readonly PagedDataSource _pgsource = new PagedDataSource();
    int _firstIndex, _lastIndex;
    private int _pageSize = 20;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Low Stock Product List";
            //GetLowStockProduct();
        }
        if (Page.IsPostBack) return;
        search();
    }
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

        repLowStockProductList.DataSource = null;
        repLowStockProductList.DataBind();
        // Bind data into repeater
        repLowStockProductList.DataSource = _pgsource;
        repLowStockProductList.DataBind();

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
            cmd.CommandText = "ReportPaging_getLowStockProduct";
            cmd.CommandType = CommandType.StoredProcedure;
            if (txtSearch.Text == "")
            {
                cmd.Parameters.Add("@seachtext", null);
            }
            else
            {
                cmd.Parameters.Add("@seachtext", txtSearch.Text);

            }
            
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

    //public void GetLowStockProduct()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtTable = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter();

    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "getLowStockProduct";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Connection = ConnectionString;
    //    ConnectionString.Open();
    //    try
    //    {
    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dtTable);
    //        if (dtTable != null)
    //        {
    //            if (dtTable.Rows.Count > 0)
    //            {
    //                gvLowProductList.DataSource = dtTable;
    //                gvLowProductList.DataBind();
    //            }
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
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        search();
    }
}
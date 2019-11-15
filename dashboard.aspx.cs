using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Globalization;

public partial class dashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    //DateTime from = DateTime.ParseExact("1900-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture), to =DateTime.Now;
    String from = DateTime.Now.ToString("dd/MM/yyyy"), to = DateTime.Now.ToString("dd/MM/yyyy");
    String from1 = "1900-01-01", to1 = DateTime.Now.ToString("dd/MM/yyyy");
    //DateTime.ParseExact("YouDateString", "dd-MM-yyyy", CultureInfo.InvariantCulture)
    //IFormatProvider dateFormat = new IFormatProvider("yyyy-MM-dd");
    protected void Page_Load(object sender, EventArgs e)
    {
        
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Dashboard";
        if (!Page.IsPostBack)
        {
            GetOrderJSON();
            GetIncomeExpenseJSON();
            GetMoryaFollowupJSON();
            GetOrderDetails();
         //   GetLowStockProduct();
             
           // GetHighestProductDetails();
           // GetHighestDealerOrderDetails();
        }
        if (!IsPostBack)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "dashboard_SP";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter  da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if(ds.Tables[0].Rows.Count>0)
                {
                    lbl_AppOrder.Text = ds.Tables[0].Rows[0]["appOrder"].ToString();
                    lbl_confirmedOrder.Text = ds.Tables[0].Rows[0]["confirmedOrder"].ToString();
                    lbl_DealerRegi.Text = ds.Tables[0].Rows[0]["dealerOrder"].ToString();
                    lbl_UserRegi.Text = ds.Tables[0].Rows[0]["customerOrder"].ToString();

                  
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                con.Close();
            }
        }
    }

    public string markersLst
    {
        get
        {
            if (ViewState["markersLst"] != null)
                return Convert.ToString(ViewState["markersLst"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["markersLst"] = value;
        }
    }

    public string markers
    {
        get
        {
            if (ViewState["markers"] != null)
                return Convert.ToString(ViewState["markers"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["markers"] = value;
        }
    }
    public string markersFollowup
    {
        get
        {
            if (ViewState["markersFollowup"] != null)
                return Convert.ToString(ViewState["markersFollowup"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["markersFollowup"] = value;
        }
    }


    private void GetOrderJSON()
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable dtOrderDetails  = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "getOrderDetailsJSON";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = ConnectionString;
        ConnectionString.Open();
        try
        {
            //string markers = string.Empty;
            da = new SqlDataAdapter(cmd);
            da.Fill(dtOrderDetails);
            if (dtOrderDetails != null)
            {
                if (dtOrderDetails.Rows.Count > 0)
                {
                    markersLst = DataTableToJSONWithJavaScriptSerializer(dtOrderDetails);
                }
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

    private void GetIncomeExpenseJSON()
    {
        DataTable dtDetails = new DataTable();
        SqlDataAdapter daIncomeExpenseJSON = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        if (!String.IsNullOrEmpty(txt_fromDate.Text.ToString()) && !String.IsNullOrEmpty(txt_toDate.Text.ToString()))
        {
            DateTime ff = DateTime.ParseExact(txt_fromDate.Text, "dd/MM/yyyy", null);
            System.Data.SqlTypes.SqlDateTime dtSql = System.Data.SqlTypes.SqlDateTime.Parse(ff.ToString("yyyy-MM-dd"));
            from = dtSql.ToString();
            from = txt_fromDate.Text.ToString();
            to = txt_toDate.Text.ToString();

            //from = String.Format("yyyy-MM-dd", txt_fromDate.Text.ToString());
            //to = String.Format("yyyy-MM-dd", txt_toDate.Text.ToString());
            

        }
        //else {
        //    //from = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
        //    //to = DateTime.Now;
        //    from = "1900-01-01";
        //    to = DateTime.Now.ToString("yyyy-MM-dd");
        //}
        try
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getIncomeExpenseDetails";
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            daIncomeExpenseJSON = new SqlDataAdapter(cmd);
            daIncomeExpenseJSON.Fill(dtDetails);
            if (dtDetails != null)
            {
                if (dtDetails.Rows.Count > 0)
                {
                    markers = DataTableToJSONWithJavaScriptSerializerDetails(dtDetails);
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

    private void GetMoryaFollowupJSON()
    {
        DataTable dtFollowup = new DataTable();
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter daMoryaFollowupJSON = new SqlDataAdapter();
        if (!String.IsNullOrEmpty(txt_fromDate1.Text.ToString()) && !String.IsNullOrEmpty(txt_toDate1.Text.ToString()))
        {

            from1 = txt_fromDate1.Text.ToString();
            to1 = txt_toDate1.Text.ToString();

            //from = String.Format("yyyy-MM-dd", txt_fromDate1.Text.ToString());
            //to = String.Format("yyyy-MM-dd", txt_toDate1.Text.ToString());
        }
        
        try
        {
            
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.CommandText = "getFollowupDetails";
            cmd.Parameters.AddWithValue("@from", from1);
            cmd.Parameters.AddWithValue("@to", to1);
            daMoryaFollowupJSON = new SqlDataAdapter(cmd);
            daMoryaFollowupJSON.Fill(dtFollowup);
            if (dtFollowup != null)
            {
                if (dtFollowup.Rows.Count > 0)
                {
                    markersFollowup = DataTableToJSONWithJavaScriptSerializerFollowup(dtFollowup);
                }
                else {
                    markersFollowup = "{}";
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



    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {

            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "" + table.Rows[i][j].ToString().Trim() + " , ");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "'" + table.Rows[i][j].ToString().Trim() + "'");


                        //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + @""":" + "\"" + table.Rows[i][j].ToString().Trim() + @"""");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("} , ");
                }
            }
        }
        
        return JSONString.ToString().Replace(@"""", @"\""");
    }

    public string DataTableToJSONWithJavaScriptSerializerFollowup(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
           // { y: '2014', a: 50, b: 90},

            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{ ");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ": " + "'" + table.Rows[i][j].ToString().Trim() + "', ");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ": " + "" + table.Rows[i][j].ToString().Trim() + "");


                        //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + @""":" + "\"" + table.Rows[i][j].ToString().Trim() + @"""");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("}, ");
                }
            }
        }

        return JSONString.ToString().Replace(@"""", @"\""");
    }

    public string DataTableToJSONWithJavaScriptSerializerDetails(DataTable table)
    {
        //{ label: "Friends", value: 30 },
        //{ label: "Allies", value: 15 },
        
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {

            for (int i = 0; i < table.Rows.Count; i++)
            {
                //JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    JSONString.Append("{");

                    //if (j < table.Columns.Count - 1)
                    //{
                    //    //JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "" + table.Rows[i][j].ToString().Trim() + " , ");
                    //    JSONString.Append("" + "label" + ":" + "\"" + table.Columns[j].ColumnName.ToString()+"\","+"value"+":" + table.Rows[i][j].ToString().Trim());

                    //}
                    //else if (j == table.Columns.Count - 1)
                    //{
                    //    JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "'" + table.Rows[i][j].ToString().Trim() + "'");


                    //    //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + @""":" + "\"" + table.Rows[i][j].ToString().Trim() + @"""");
                    //}
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("" + "label" + ":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "value" + ":" + table.Rows[i][j].ToString().Trim()+"},");
                    }
                    else if (j == table.Columns.Count - 1) {
                        JSONString.Append("" + "label" + ":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "value" + ":" + table.Rows[i][j].ToString().Trim());
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("} , ");
                }
            }
        }
        //return JSONString.ToString();
        return JSONString.ToString().Replace(@"\""", @"""");
    }

    

    public void GetOrderDetails()
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataTable dtOrderDetails = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "getDashboardOrderDetails";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = ConnectionString;
        ConnectionString.Open();
        try
        {
            da = new SqlDataAdapter(cmd);
            da.Fill(dtOrderDetails);
            if (dtOrderDetails != null)
            {
                if (dtOrderDetails.Rows.Count > 0)
                {
                    gvOrderDetails.DataSource = dtOrderDetails;
                    gvOrderDetails.DataBind();
                }
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

    //public void GetHighestUserOrder()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtTable = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter();

    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "getHighestUserOrder";
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
    //                gvHighestUserOrder.DataSource = dtTable;
    //                gvHighestUserOrder.DataBind();
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

    //public void GetHighestProductDetails()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtTable = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter();

    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "getHighestProductDetails";
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
    //                gvHighestProduct.DataSource = dtTable;
    //                gvHighestProduct.DataBind();
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

    //public void GetHighestDealerOrderDetails()
    //{
    //    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    DataTable dtTable = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter();

    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandText = "getHighestDealerOrderDetails";
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
    //                gvHighestDealerOrder.DataSource = dtTable;
    //                gvHighestDealerOrder.DataBind();
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

    //public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    //{
    //    var JSONString = new StringBuilder();
    //    if (table.Rows.Count > 0)
    //    {

    //        for (int i = 0; i < table.Rows.Count; i++)
    //        {
    //            JSONString.Append("{");
    //            for (int j = 0; j < table.Columns.Count; j++)
    //            {
    //                if (j < table.Columns.Count - 1)
    //                {
    //                    JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "" + table.Rows[i][j].ToString().Trim() + " , ");
    //                }
    //                else if (j == table.Columns.Count - 1)
    //                {
    //                    JSONString.Append("" + table.Columns[j].ColumnName.ToString() + ":" + "'" + table.Rows[i][j].ToString().Trim() + "'");


    //                    //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + @""":" + "\"" + table.Rows[i][j].ToString().Trim() + @"""");
    //                }
    //            }
    //            if (i == table.Rows.Count - 1)
    //            {
    //                JSONString.Append("}");
    //            }
    //            else
    //            {
    //                JSONString.Append("} , ");
    //            }
    //        }
    //    }
    //    return JSONString.ToString().Replace(@"""", @"\""");
    //}

    protected void gvOrderDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrderDetails.PageIndex = e.NewPageIndex;
        GetOrderDetails();
    }

    //protected void gvHighestProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvHighestProduct.PageIndex = e.NewPageIndex;
    //    GetHighestProductDetails();
    //}

    //protected void gvHighestDealerOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvHighestDealerOrder.PageIndex = e.NewPageIndex;
    //    GetHighestDealerOrderDetails();
    //}

    //protected void gvHighestUserOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvHighestUserOrder.PageIndex = e.NewPageIndex;
    //    GetHighestUserOrder();
    //}
    //protected void gvLowProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvLowProductList.PageIndex = e.NewPageIndex;
    //    GetLowStockProduct();
    //}
    protected void txt_fromDate_TextChanged(object sender, EventArgs e)
    {
        GetIncomeExpenseJSON();
    }
    
    protected void txt_fromDate1_TextChanged(object sender, EventArgs e)
    {
        GetMoryaFollowupJSON();
    }
}
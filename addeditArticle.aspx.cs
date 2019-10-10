using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditArticle : System.Web.UI.Page
{
    int categoryImageFrontWidth = 500;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    int categoryImageFrontHeight = 605;
    string categoryMainPath = "~/uploads/vendor/";
    string categoryFrontPath = "~/uploads/vendor/front/";
    common ocommon = new common();
    private void BindProduct()
    {

        DataTable  dt = new DataTable();
        SqlDataAdapter da;
        try
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "product_SelectAllAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
           

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlProduct .DataSource = dt;
                    ddlProduct.DataTextField = "productname";
                    ddlProduct.DataValueField = "pid";
                    ddlProduct.DataBind();
                    ListItem objListItem = new ListItem("--Select Product--", "0");
                    ddlProduct.Items.Insert(0, objListItem);
                }
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

    //private void BindRepeter()
    //{
    //    string productdata = "SELECT id,'' as sr, particulars, wages  FROM   productparticulars where id = 0";
    //    SqlDataAdapter daproduct = new SqlDataAdapter(productdata, con);
    //    DataTable dtprod = new DataTable();
    //    daproduct.Fill(dtprod);
    //    repProcess.DataSource = dtprod;
    //    repProcess.DataBind();
    //    ViewState["dtprod"] = dtprod;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");

            BindProduct();

            //BindRepeter();
            if (Request.QueryString["id"] != null)
            {
                Int64 pid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
                ddlProduct.SelectedValue = pid.ToString();
                BindArticle(pid );
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Article";
                Page.Title = "Update Article";
            }
            else
            {
                BindArticle(0);
                hPageTitle.InnerText = "New Article";
                Page.Title = "New Article";
                btnSave.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txt_price.Text = string.Empty;
        txt_processName .Text = string.Empty;
        ddlProduct.SelectedIndex = 0;
         
    }
   

    public void BindArticle(Int64 pid)
    {
        try
        {
            con.Open();
            DataTable  ds = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getArticleDetailsby_productId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@productid", pid );
            //con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            repProcess.DataSource = ds;
            repProcess.DataBind();
            ViewState["dtprod"] = ds;
            if (ds.Rows.Count > 0)
            {
                ddlProduct.SelectedValue = ds.Rows[0]["productid"].ToString();
            }
            else
            {

            }
          


            con.Close();
        }
        catch { }
        finally { con.Close(); }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            Int64 Result = 0;
            //Article objArticle = new Article();        
            DataTable dtproduct = new DataTable();
            dtproduct = (DataTable)ViewState["dtprod"];

            if (Request.QueryString["id"] != null)
            {

                foreach (DataRow dr in dtproduct.Rows)
                {
                    Int64 idd = 0;
                    if (dr["id"].ToString().ToLower().Trim()!= "")
                    {
                        idd = Convert.ToInt64(dr["id"].ToString().ToLower().Trim());
                    }
                    string s = "select * from productparticulars where productid = "+ddlProduct.SelectedValue.ToString()+ " and id = " + idd + "";
                    SqlCommand cmd = new SqlCommand(s, con);
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    if(dr1.HasRows)
                    {
                        dr1.Close();
                        string s1 = " update productparticulars set   particulars=@particulars ,wages=@wages where productid=@productid and id=@id ";

                        SqlCommand cmddeta = new SqlCommand(s1, con);
                        cmddeta.Parameters.AddWithValue("@id", idd);
                        cmddeta.Parameters.AddWithValue("@productid", Convert.ToInt64(ddlProduct.SelectedValue.ToString()));
                        cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                        cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());
                        
                       Result = cmddeta.ExecuteNonQuery();
                    }
                    else
                    {
                        dr1.Close();
                        string s1 = " INSERT INTO productparticulars(productid, particulars, wages, quantity, isdeleted)  VALUES (@productid, @particulars, @wages, @quantity, @isdeleted) ";

                        SqlCommand cmddeta = new SqlCommand(s1, con);
                        cmddeta.Parameters.AddWithValue("@productid", Convert.ToInt64(ddlProduct.SelectedValue.ToString()));
                        cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                        cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());
                        cmddeta.Parameters.AddWithValue("@quantity", "0");
                        cmddeta.Parameters.AddWithValue("@isdeleted", "0");
                        Result = cmddeta.ExecuteNonQuery();

                    }

                   
                }

                if (Result > 0)
                {
                    Clear();
                    Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=u"));
                }
                else
                {
                    Clear();
                    spnMessgae.Style.Add("color", "red");
                    spnMessgae.InnerText = "Article Not Updated";
                   // BindVendor(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                }
            }
            else
            {
                int i = 0;
                foreach (DataRow dr in dtproduct.Rows)
                {
                    string s1 = " INSERT INTO productparticulars(productid, particulars, wages, quantity, isdeleted)  VALUES (@productid, @particulars, @wages, @quantity, @isdeleted) ";

                    SqlCommand cmddeta = new SqlCommand(s1, con);
                    cmddeta.Parameters.AddWithValue("@productid", ddlProduct.SelectedValue.ToString());
                    cmddeta.Parameters.AddWithValue("@particulars", dr["particulars"].ToString());
                    cmddeta.Parameters.AddWithValue("@wages", dr["wages"].ToString());
                    cmddeta.Parameters.AddWithValue("@quantity", "0");
                    cmddeta.Parameters.AddWithValue("@isdeleted", "0");
                     i = cmddeta.ExecuteNonQuery();
                }
                if (i > 0)
                {
                    Clear();
                    Response.Redirect(Page.ResolveUrl("~/manageproduct.aspx?mode=i"));
                }
                else
                {
                    Clear();
                    spnMessgae.Style.Add("color", "red");
                    spnMessgae.InnerText = "Vendor Not Inserted";

                }

            }
            con.Close();
        }
        catch(Exception p)
        { }
        finally {
            con.Close();
        }

    }


    protected void Remove_member1(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
            int ind = gr.RowIndex;
            //string pid = (gvproduct.Rows[ind].Cells[0].Text);        
            string pid = "";
            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];
            pid = dtprodn.Rows[ind]["id"].ToString();
            dtprodn.Rows.RemoveAt(ind);

            //-----------------
            for (int i = 0; i < dtprodn.Rows.Count; i++)
            {
                dtprodn.Rows[i]["sr"] = (i + 1).ToString();
            }
            //-----------------
            dtprodn.AcceptChanges();
            repProcess.DataSource = dtprodn;
            repProcess.DataBind();

            if (Request.QueryString["id"] != null)
            {
                Int64 OrderID = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
                string s = "update productparticulars set isdeleted =1  where id=" + pid + "";
                SqlCommand cmd = new SqlCommand(s, con);
                int t = cmd.ExecuteNonQuery();


            }
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Process Remove Successfully');", true);
            con.Close();
        }
        catch (Exception p)
        { }
        finally { con.Close(); }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
         
            if (  txt_processName .Text == string.Empty || txt_price .Text == string.Empty )
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Please Enter All Fields ');", true);
                return;
            }

            DataTable dtprodn = new DataTable();
            dtprodn = (DataTable)ViewState["dtprod"];

            DataRow dr = dtprodn.NewRow();
            int srr = 0;
            srr = dtprodn.Rows.Count + 1;
            dr["sr"] = srr.ToString();
            dr["particulars"] = txt_processName.Text.Trim();
            dr["wages"] = txt_price .Text;
            
            dtprodn.Rows.Add(dr);
            repProcess .DataSource = dtprodn;
            repProcess.DataBind();
            ViewState["dtprod"] = dtprodn;
            txt_processName.Text = string.Empty;
            txt_price.Text = string.Empty;
            
           
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }
}
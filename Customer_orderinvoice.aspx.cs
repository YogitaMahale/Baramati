using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_orderinvoice : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindOrderDetails(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
        }
    }

    private void BindOrderDetails(Int64 OrderId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "purchaseorder_invoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@oid", OrderId);
            cmd.Connection = con;
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables != null)
            {
                /* Order Details */
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sminvoiceNo.InnerText = ds.Tables[0].Rows[0]["oid"].ToString();


                        smOrderDate.InnerText = "Order Date : " + ds.Tables[0].Rows[0]["orderdate"].ToString();

                        spnTotal.InnerText = ds.Tables[0].Rows[0]["grandtotal"].ToString();


                        sporder2.InnerText = ds.Tables[0].Rows[0]["invoiceno"].ToString();
                        //spdeliverth.InnerText = ds.Tables[0].Rows[0]["DeliveredThrough"].ToString();
                        //spdeliveryDetails.InnerText = ds.Tables[0].Rows[0]["DeliveredDetails"].ToString();
                        //spReferenceby.InnerText = ds.Tables[0].Rows[0]["Referenceby"].ToString();
                    }
                }

                /* Order Products Details */
                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        repOrderProducts.DataSource = ds.Tables[1];
                        repOrderProducts.DataBind();
                    }
                }

                /* User Details */
                if (ds.Tables[2] != null)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        spnName.InnerHtml = ds.Tables[2].Rows[0]["name"].ToString();
                        strGSTNo.Visible = true;
                        spnGST.InnerHtml = ds.Tables[2].Rows[0]["gstno"].ToString();
                        spnToAddress.InnerHtml = ds.Tables[2].Rows[0]["address"].ToString();
                        spnToEmail.InnerHtml = ds.Tables[2].Rows[0]["email"].ToString();
                        spnToPhone.InnerHtml = ds.Tables[2].Rows[0]["phone"].ToString();
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
}
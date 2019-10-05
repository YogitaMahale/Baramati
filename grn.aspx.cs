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

public partial class grn : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProductID = new DataTable();
    int rowcount = 0, chkcount = 0;
    Int64 qtyrequested = 0, qtyreceived = 0, PurchaseOrderDetailsId = 0, PurchaseOrderId = 0;


    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Goods Received Note (STOCK IN)";
            try
            {
                if (Session["PurchaseOrderId"] == null || Session["PurchaseOrderId"].ToString() == "")
                {
                }
                else
                {
                    Int64 PurchaseOrderId = Int64.Parse(Session["PurchaseOrderId"].ToString());
                    // PurchaseOrderHeader objcategory = (new Cls_PurchaseOrderHeader_b().SelectById(CategoryId));

                    //POH.isdeleted=0 AND 
                    //string s = "";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "PurchaseOrderDetailsByOrderId";
                    cmd.Parameters.AddWithValue("@PurchaseOrderId", PurchaseOrderId);
                    //cmd.Parameters.AddWithValue("@password", password);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                   
                    DataSet ds = new DataSet();
                   //SqlDataAdapter da = new SqlDataAdapter(s, con);
                    sda.Fill(ds);

                    rowcount = ds.Tables[0].Rows.Count;
                    ViewState["rowcount"] = rowcount;

                    if (ds.Tables[0].Rows.Count > 0)
                    //VendorId, VendorName, VendorContactPerson, VendorPhone, VendorAddress, VendorGST, VendorEmailId, isactive, isdeleted
                    {
                        lbl_ID.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                        lbl_vendorName.Text = ds.Tables[0].Rows[0]["vendorName"].ToString();
                       // lbl_contactPerson.Text = ds.Tables[0].Rows[0]["VendorContactPerson"].ToString();
                        lbl_mobile.Text = ds.Tables[0].Rows[0]["MobileNo1"].ToString();
                        lbl_email.Text = ds.Tables[0].Rows[0]["email"].ToString();

                    }
                    Repeater1.DataSource = ds.Tables[0];
                    Repeater1.DataBind();

                    if (ViewState["dtProductID"] != null)
                        // dtProductID = (DataTable)ViewState["Products"];
                        ViewState["dtProductID"] = ds.Tables[0];

                }
            }
            catch { }
            finally { }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;





        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        // PurchaseOrderHeader objcategory = new PurchaseOrderHeader();
        con.Open();

        if (ViewState["dtProductID"] != null)
            dtProductID = (DataTable)ViewState["Products"];

        //foreach (DataRow row in dtProductID.Rows)
        //{
        //    //string s = "INSERT INTO [DoctorDiagnosisNew].[PurchaseOrderDetails]([PurchaseOrderId],[ProdId],[BrandId],[SizeId],[Quantity],[isdeleted]) VALUES("
        //      //  + Result + "," + row["ProdId"] + "," + row["BrandId"] + "," + row["SizeId"] + "," + row["Quantity"] + "," + 0 + ")";
        //     string queryString = "UPDATE [dbo].[ProductMaster] SET [stock] = <stock, int,> WHERE [ProdId] ="+ row["ProdId"]+ "AND [ProdBrand] = "+ row ["BrandId"]+" AND [ProdSize] = "+row["SizeId"] ;

        //SqlCommand cmd = new SqlCommand(queryString, con);
        //int t = cmd.ExecuteNonQuery();

        foreach (RepeaterItem item in Repeater1.Items)
        {
            //string val = null;

            // This is Requested Quantity

            qtyrequested = int.Parse((item.FindControl("LabelQuantity") as Label).Text);


            // This is Received Quantity 

            qtyreceived = int.Parse((item.FindControl("txt_receivedqty") as TextBox).Text);


            //TextBox txt_receivedqty = (TextBox)item.FindControl("txt_receivedqty");
            //if (txt_receivedqty != null)
            //{   
            //    val = txt_receivedqty.Text;
            //    //do something with val
            //}


            Label lblProdId = (Label)item.FindControl("LabelProdId");
            //Label lblBrandId = (Label)item.FindControl("LabelBrandId");
            //Label lblSizeId = (Label)item.FindControl("LabelSizeId");

            //Response.Write("<script>alert(Record Inserted Successfully!!!)</script>")


            string queryString1 = "UPDATE product SET [RealStock] = [RealStock] +" + qtyreceived + " WHERE [pid] =" + lblProdId.Text;
            SqlCommand cmd1 = new SqlCommand(queryString1, con);
            int t1 = cmd1.ExecuteNonQuery();

            PurchaseOrderDetailsId = int.Parse((item.FindControl("LabelPODId") as Label).Text);

            if (qtyrequested == qtyreceived)
            {
                chkcount++;
                // Int64 CategoryId = Int64.Parse(Session["PurchaseOrderId"].ToString());

                string queryString2 = "UPDATE PurchaseOrderDetails SET [isdeleted] = " + 1 + " WHERE [PurchaseOrderDetailsId] = " + PurchaseOrderDetailsId;
                SqlCommand cmd2 = new SqlCommand(queryString2, con);
                int t2 = cmd2.ExecuteNonQuery();
            }
            else if (qtyreceived < qtyrequested)
            {
                //chkcount++;
                // Int64 CategoryId = Int64.Parse(Session["PurchaseOrderId"].ToString());

                string queryString3 = "UPDATE PurchaseOrderDetails SET [Quantity] = [Quantity] - " + qtyreceived + " WHERE [PurchaseOrderDetailsId] = " + PurchaseOrderDetailsId;
                SqlCommand cmd3 = new SqlCommand(queryString3, con);
                int t3 = cmd3.ExecuteNonQuery();
            }
            if (Session["PurchaseOrderId"] != null || Session["PurchaseOrderId"].ToString() != "")
            {

                PurchaseOrderId = Int64.Parse(Session["PurchaseOrderId"].ToString());

            }
            string queryString4 = "INSERT INTO GRNMaster ([POHId],[PODId],[quantity]) VALUES(" + PurchaseOrderId + "," + PurchaseOrderDetailsId + "," + qtyreceived + ")";
            SqlCommand cmd4 = new SqlCommand(queryString4, con);
            int t4 = cmd4.ExecuteNonQuery();

        }

        try
        {
            rowcount = int.Parse(ViewState["rowcount"].ToString());
            if (rowcount == chkcount)
            {

                if (Session["PurchaseOrderId"] == null || Session["PurchaseOrderId"].ToString() == "")
                {
                }
                else
                {


                    Int64 CategoryId = Int64.Parse(Session["PurchaseOrderId"].ToString());
                    string queryString = "UPDATE PurchaseOrderHeader SET [orderstatus] = " + 1 + " WHERE [PurchaseOrderId] = " + CategoryId;
                    SqlCommand cmd = new SqlCommand(queryString, con);
                    int t5 = cmd.ExecuteNonQuery();
                    // if(t>0)
                    //   Response.Redirect(Page.ResolveUrl("~/frm_ManagePurchaseOrder.aspx"));

                }



            }

            Response.Redirect(Page.ResolveUrl("~/managepurchaseorder.aspx?mode=s"));

        }
        catch { }
        finally { con.Close(); }


    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/managepurchaseorder.aspx");

    }
    
}
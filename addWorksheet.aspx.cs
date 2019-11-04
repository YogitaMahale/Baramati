﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;


public partial class addWorksheet : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProduct = new DataTable();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    String usermail = String.Empty;
    String createdby = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["userid"]
        if (!String.IsNullOrEmpty(Session["userid"].ToString()))
            createdby = Session["userid"].ToString();
        //Bind();
        
        if (!IsPostBack)
        {
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Work Sheet";
            txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
        }
        //if (!String.IsNullOrEmpty(Session["usermail"].ToString()))
        //  usermail = Session["usermail"].ToString();

        if (Page.IsPostBack)
        {
            //Bind();

            // get a reference to ScriptManager and check if we have a partial postback
            if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                // partial (asynchronous) postback occured
                // insert Ajax custom logic here
                Console.Write("partial");
            }
            else
            {
                Console.Write("full");
                // regular full page postback occured
                // custom logic accordingly    

            }
        }
        



    }


    private void Bind()
    {
        //List<SelectListItem> list = new List<SelectListItem>();
        DataTable dtArticle = new DataTable();
        DataTable dtColor = new DataTable();
        DataTable dtSize = new DataTable();
        DataTable dtEmployee = new DataTable();
        //if (dtProduct == null)
        //{
        //    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Product(s)--", "0");
        //    ddlProduct.Items.Insert(0, objListItem);
        //}
        try
        {

            Cls_product_b clsProduct = new Cls_product_b();
            dtArticle = clsProduct.SelectAll();

            Cls_color_b clsColor = new Cls_color_b();
            dtColor = clsColor.SelectAll();

            Cls_size_b clsSize = new Cls_size_b();
            dtSize = clsSize.SelectAll();

            Cls_Employee_b clsEmployee = new Cls_Employee_b();
            dtEmployee = clsEmployee.SelectAll();
            


        }
        catch { }
        finally { con.Close(); }

        if (dtArticle != null)
        {
            if (dtArticle.Rows.Count > 0)
            {
                ddlArticle.DataSource = dtArticle;
                ddlArticle.DataTextField = "productname";
                ddlArticle.DataValueField = "pid";
                ddlArticle.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Article--", "0");
                ddlArticle.Items.Insert(0, objListItem);
            }
        }

        if (dtColor != null)
        {
            if (dtColor.Rows.Count > 0)
            {
                //ddlColor.DataSource = dtColor;
                //ddlColor.DataTextField = "colorname";
                //ddlColor.DataValueField = "cid";
                //ddlColor.DataBind();
                //System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Color--", "0");
                //ddlColor.Items.Insert(0, objListItem);


                lstColor.DataSource = dtColor;
                lstColor.DataTextField = "colorname";
                lstColor.DataValueField = "cid";
                lstColor.DataBind();
                //System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Color--", "0");
                //lstColor.Items.Insert(0, objListItem);


            }
            else {
                lstColor.DataSource = dtColor;
                lstColor.DataTextField = "colorname";
                lstColor.DataValueField = "cid";
                lstColor.DataBind();


            }
        }
        else
        {
            lstColor.DataSource = dtColor;
            lstColor.DataTextField = "colorname";
            lstColor.DataValueField = "cid";
            lstColor.DataBind();


        }

        if (dtSize != null)
        {
            if (dtSize.Rows.Count > 0)
            {
                //ddlSize.DataSource = dtSize;
                //ddlSize.DataTextField = "sizeName";
                //ddlSize.DataValueField = "cid";
                //ddlSize.DataBind();
                //System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Size--", "0");
                //ddlSize.Items.Insert(0, objListItem);

                lstSize.DataSource = dtSize;
                lstSize.DataTextField = "sizeName";
                lstSize.DataValueField = "cid";
                lstSize.DataBind();

            }
            else {
                lstSize.DataSource = dtSize;
                lstSize.DataTextField = "sizeName";
                lstSize.DataValueField = "cid";
                lstSize.DataBind();
            }
        }
        else
        {
            lstSize.DataSource = dtSize;
            lstSize.DataTextField = "sizeName";
            lstSize.DataValueField = "cid";
            lstSize.DataBind();
        }
        if (dtEmployee != null)
        {
            if (dtEmployee.Rows.Count > 0)
            {
                Session["employee"] = dtEmployee;
                ddlEmployee.DataSource = dtEmployee;
                ddlEmployee.DataTextField = "employeeName";
                ddlEmployee.DataValueField = "id";
                ddlEmployee.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Employee--", "0");
                ddlEmployee.Items.Insert(0, objListItem);
            }
        }
    }
   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
   
        DataTable dtProduct = new DataTable();
        dtProduct = GetProducts();

        
        if (ViewState["Products"] != null)
        {
            dtProduct = (DataTable)ViewState["Products"];


            Repeater1.DataSource = dtProduct;
            Repeater1.DataBind();
            Repeater1.Visible = true;
        }
        else
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            Repeater1.Visible = false;
        }

        
        txtquantity.Text = "0";
    }



    private DataTable GetProducts()
    {

        DataTable dtProduct = null;
        if (ViewState["SrNo"] != null)
        {
            int SrNo = Convert.ToInt32((ViewState["SrNo"]));
            SrNo++;
            ViewState["SrNo"] = SrNo;
        }
        else
        {
            ViewState["SrNo"] = 1;
        }

        if (ViewState["Products"] == null)
        {

            // [ProdId],[ProdName],[ProdBrand] ,[ProdSize]

            dtProduct = new DataTable("Products");
            dtProduct.Columns.Add(new DataColumn("SrNo", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("operation", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("operationid", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("date", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("employee", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("employeeid", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("quantity", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("wages", typeof(string)));

            dtProduct.Columns.Add(new DataColumn("remark", typeof(string)));

            ViewState["Products"] = dtProduct;
        }
        else
        {
            dtProduct = (DataTable)ViewState["Products"];
        }
        DataRow dtRow = dtProduct.NewRow();

        dtRow["SrNo"] = ViewState["SrNo"]; 
        dtRow["operation"] = ddlOperation.SelectedItem;
        dtRow["operationid"] = ddlOperation.SelectedValue;

        dtRow["employee"] = ddlEmployee.SelectedItem;
        dtRow["employeeid"] = ddlEmployee.SelectedValue;
        dtRow["date"] = txtWorkDate.Text.ToString();
        dtRow["remark"] = txtRemark.Text.Trim();
        dtRow["quantity"] = txtquantity.Text.Trim();
        dtRow["wages"] = txtwages.Text.Trim();
        dtProduct.Rows.Add(dtRow);
        ViewState["Products"] = dtProduct;
        return dtProduct;
    }



    protected void Remove_Product(object sender, EventArgs e)
    {
        try
        {
            //  con.Open();

            //GridViewRow gr = (GridViewRow)(sender as Control).Parent.Parent;
            //int ind = gr.RowIndex;
            //string pid = (gvproduct.Rows[ind].Cells[0].Text);
            int prodid;
            Button button = (sender as Button);
            //Get the command argument
            string commandArgument = button.CommandArgument;


            prodid = int.Parse(commandArgument);

            DataTable dtProduct = new DataTable();

            if (ViewState["Products"] == null)
            {

                // [ProdId],[ProdName],[ProdBrand] ,[ProdSize]

                dtProduct = new DataTable("Products");
                dtProduct.Columns.Add(new DataColumn("SrNo", typeof(int)));
                dtProduct.Columns.Add(new DataColumn("operation", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("operationid", typeof(int)));

                dtProduct.Columns.Add(new DataColumn("date", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("employee", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("employeeid", typeof(int)));
                dtProduct.Columns.Add(new DataColumn("quantity", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("wages", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("remark", typeof(string)));
                ViewState["Products"] = dtProduct;
            }
            else
            {
                dtProduct = (DataTable)ViewState["Products"];
            }


            // dtProduct = (DataTable)ViewState["Products"];

            DataRow[] drr = dtProduct.Select("SrNo=' " + prodid + " ' ");
            foreach (var row in drr)
                row.Delete();

            // dtProduct.Rows.RemoveAt(prodid);

            dtProduct.AcceptChanges();
            Repeater1.DataSource = dtProduct;
            Repeater1.DataBind();
            ddlEmployee.SelectedIndex = 0;
            ddlOperation.SelectedIndex = 0;
            txtquantity.Text = "0";
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmsg", "alert('Product Removed Successfully');", true);

        }
        catch (Exception p)
        { }
        finally
        {
            //con.Close(); 
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0, Result1 = 0;
        DataTable dtProduct = new DataTable();

        //Response.Write(hf1.Value);

        String valuesColor = hfcolorid.Value, valuesSize = hfsizeid.Value;

        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //id,productid,sizeid,colorid,createdby,createddate,isdeleted
        worksheetmaster objworksheetmaster = new worksheetmaster();
        objworksheetmaster.Productid =Convert.ToInt64(ddlArticle.SelectedValue.ToString());
        //objworksheetmaster.Sizeid = Convert.ToInt64(ddlSize.SelectedValue.ToString());
        //objworksheetmaster.Colorid = Convert.ToInt64(ddlColor.SelectedValue.ToString());
        objworksheetmaster.Colorid = valuesColor;
        objworksheetmaster.Sizeid = valuesSize;
        objworksheetmaster.Createdby = Convert.ToInt64(createdby);
        objworksheetmaster.Createddate =Convert.ToDateTime(txtWorkDate.Text.ToString());

        //PurchaseOrderHeader objcategory = new PurchaseOrderHeader();
        //objcategory.PONo = pono;
        //objcategory.VendorId = Int64.Parse(ddlVendor.SelectedValue);

        //objcategory.isdeleted = false;

        if (Request.QueryString["id"] != null)
        {
            objworksheetmaster.Id = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_worksheetmaster_b().Update(objworksheetmaster));
            if (Result > 0)
            {

                Response.Redirect(Page.ResolveUrl("~/manageworksheets.aspx?mode=u"));
            }
            else
            {

            }
        }
        else
        {
            Result = (new Cls_worksheetmaster_b().Insert(objworksheetmaster));
            if (Result > 0)
            {
                con.Open();
                if (ViewState["Products"] != null)
                    dtProduct = (DataTable)ViewState["Products"];
                worksheetdetails objPod = new worksheetdetails();
                foreach (DataRow row in dtProduct.Rows)
                {
                    //id,worksheetid,particularsid,employeeid,workdate,quantity,remark

                    objPod.Worksheetid = Result;
                    objPod.Particularsid= Convert.ToInt64(row["operationid"]);
                    objPod.Employeeid = Convert.ToInt64(row["employeeid"]);
                    objPod.Quantity = Convert.ToInt64(row["quantity"]);
                    objPod.Wages = Convert.ToDouble(row["wages"]);
                    objPod.Workdate = Convert.ToDateTime(row["date"]);
                    objPod.Remark = Convert.ToString(row["remark"]);

                    Result1 = (new Cls_worksheetdetails_b().Insert(objPod));

                    //if (Result1 > 0)
                    // flag = true;


                }

                con.Close();

                
                if (Result1>0)
                    Response.Redirect(Page.ResolveUrl("~/manageworksheets.aspx?mode=i"));
            }
            else
            {


            }
        }
    }





    #region-- Send Mail
    /*
    private bool SendOrderMail(String mailTo, String filename)
    {
        common ocommon = new common();
        string oSB = string.Empty;
        bool send = false;
        MailMessage mail = new MailMessage();
        mail.To.Add(new MailAddress(mailTo));
        mail.CC.Add(usermail);
        mail.Subject = "Purchase Order";
        mail.Body = "Hello,<p>A purchase order has been generated for you.\nPlease find the details in the attachment.</p>Thank you.";
        mail.From = new MailAddress("demo@moryatools.com", "Morya Tools");
        string Filepath = Server.MapPath("~/uploads/PurchaseOrders/") + filename + ".pdf"; //("~/uploads/PurchaseOrders/")
        System.Net.Mail.Attachment attachment;
        attachment = new System.Net.Mail.Attachment(Filepath);
        mail.Attachments.Add(attachment);

        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "103.250.184.62";
        smtp.Port = 25;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential("demo@moryatools.com", "vsys@2017");
        try
        {

            smtp.Send(mail);
            send = true;
            mail.Attachments.Dispose();
            //mail.Attachments = null;
            //String filename = ViewState["fileName"].ToString();
            //var filePath = Server.MapPath("~/uploads/PatientImage/" + filename);
            //if (File.Exists(Filepath))
            //{
            //File.Delete(Filepath);
            //}
        }
        catch (Exception ex)
        {
            send = false;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "", "alert('" + ex.Message + "," + ex.StackTrace + "')", true);

        }
        if (File.Exists(Filepath))
        {

            File.Delete(Filepath);
        }
        return send;
    }
    */
    #endregion



    #region SMS
    /*
public bool SendSMS(string Name, string MobileNo)
{
    bool flg = false;

    #region
    try
    {
        string m = null;
        string msg = "Dear " + Name + ",";
        string msg1 = "\nA purchase order has been generated for you. Please check your inbox for details.";
        string msg2 = "\n-Trimurti Diagnostics Alerts";

        string OPTINS = "TDCNSK";
        m = msg + msg1 + msg2;

        string type = "3";
        string strUrl = "https://www.bulksmsgateway.in/sendmessage.php?user=Trimurti&password=" + "Trimurti@123" + "&message=" + m + "&sender=" + OPTINS + "&mobile=" + MobileNo + "&type=" + 3;

        System.Net.WebRequest request = System.Net.WebRequest.Create(strUrl);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream s = (Stream)response.GetResponseStream();
        StreamReader readStream = new StreamReader(s);
        string dataString = readStream.ReadToEnd();
        response.Close();
        s.Close();
        readStream.Close();
        //  Response.Write("Sent");
        flg = true;


    }
    catch (Exception p)
    {

    }
    #endregion
    return flg;
}
#endregion
    */
    #endregion

    #region Purchase Order
        
    //public Boolean PDFUpload(Int64 PurchaseOrderId)
    //{
    //    string finalResult = string.Empty;
    //    bool flag = true;
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

    //    Int64 inid = 0;
    //    try
    //    {


    //        //string query = "Select POH.*,CONVERT(nvarchar, POH.OrderDate,103) + ' '+REPLACE(REPLACE(CONVERT(varchar(15), CAST(POH.OrderDate AS TIME), 100), 'P', ' P'), 'A', ' A') as OrderDate1, POD.*, PM.ProdName as ProdName , VM.*, BM.BrandName as Brand, SM.Size as Size from [DoctorDiagnosisNew].[PurchaseOrderHeader] POH ,[DoctorDiagnosisNew].[PurchaseOrderDetails] POD ,[dbo].[VendorMaster] VM ,[dbo].[BrandMaster] BM ,[dbo].[SizeMaster] SM ,[dbo].[ProductMaster] PM where POH.isdeleted=0 AND POH.[PurchaseOrderId] = " + CategoryId + " AND POH.[VendorId] = VM.[VendorId] AND POH.[PurchaseOrderId] = POD.[PurchaseOrderId] AND POD.[BrandId] = BM.[BrandId] AND POD.[SizeId] = SM.[SizeId] AND POD.[ProdId] = PM.[ProdId] ORDER BY ProdName DESC";
    //        DataSet ds = new DataSet();

    //        SqlCommand cmd = new SqlCommand();

    //        cmd.CommandText = "PurchaseOrderHeader_SelectById";
    //        cmd.Parameters.AddWithValue("@id", PurchaseOrderId);
    //        //cmd.Parameters.AddWithValue("@password", password);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        SqlDataAdapter sda = new SqlDataAdapter();
    //        cmd.Connection = con;
    //        sda.SelectCommand = cmd;



    //        //SqlDataAdapter da = new SqlDataAdapter(query, con);
    //        sda.Fill(ds);
    //        String po = ds.Tables[0].Rows[0]["PONo"].ToString();
    //        Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
    //        MemoryStream PDFData = new MemoryStream();
    //        //PdfWriter pw = PdfWriter.GetInstance(
    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Context.Server.MapPath("~/uploads/PurchaseOrders/") + po + ".pdf", FileMode.Create));

    //        var titleFont = FontFactory.GetFont("Arial", 6, Font.NORMAL);
    //        var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
    //        var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
    //        var boldTableFont1 = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
    //        var bodyFont = FontFactory.GetFont("Arial", 7, Font.NORMAL);//8
    //        var EmailFont = FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK);//8
    //        var HeaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK);

    //        var footerfont = FontFactory.GetFont("Arial", 6, Font.NORMAL, Color.BLACK);//8
    //        Color TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

    //        Rectangle pageSize = writer.PageSize;
    //        // Open the Document for writing
    //        pdfDoc.Open();
    //        //Add elements to the document here

    //        #region Top table
    //        // Create the header table 
    //        PdfPTable headertable = new PdfPTable(3);
    //        headertable.HorizontalAlignment = 0;
    //        headertable.WidthPercentage = 100;
    //        headertable.SetWidths(new float[] { 100f, 320f, 220f });  // then set the column's __relative__ widths
    //        headertable.DefaultCell.Border = Rectangle.NO_BORDER;
    //        headertable.DefaultCell.Border = Rectangle.BOX; //for testing           



    //        // invoice rperte

    //        PdfPTable Invoicetable2 = new PdfPTable(1);
    //        Invoicetable2.HorizontalAlignment = 0;
    //        Invoicetable2.WidthPercentage = 100;
    //        Invoicetable2.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
    //        Invoicetable2.DefaultCell.Border = Rectangle.NO_BORDER;

    //        {

    //            PdfPTable mainN = new PdfPTable(1);
    //            // tablenew.HorizontalAlignment = 1;
    //            //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
    //            //    PdfPCell cellN = new PdfPCell(new Phrase("Trimurti Diagnostics \n\n Home/Clinic Collection No : 7777 8866 04/05, 9766 6600 83 | 0253 2376062 "
    //            //+ "\n\n28, Purab Paschim Plaza, Divya Adlabs Building, Trimurti Chowk, CIDCO, Nashik - 422008", HeaderFont));
    //            PdfPCell cellN = new PdfPCell(new Phrase("Morya Tools \n\n 22, Pradhan Park, M.G Road, Nashik, Maharashtra, India 422001 "
    //        + "\n\nPhone: (0253) 3014578 Email: kshatriya.enterprises@gmail.com", HeaderFont));
    //            cellN.PaddingTop = 15f;
    //            cellN.PaddingBottom = 15f;
    //            cellN.VerticalAlignment = 1;
    //            //cellnew.Colspan = 2;
    //            cellN.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
    //            mainN.AddCell(cellN);
    //            PdfPCell nesthousingnn = new PdfPCell(mainN);

    //            PdfPTable tablenew = new PdfPTable(1);
    //            // tablenew.HorizontalAlignment = 1;
    //            //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
    //            PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER", EmailFont));
    //            cellnew.PaddingTop = 15f;
    //            cellnew.PaddingBottom = 15f;
    //            cellnew.VerticalAlignment = 1;
    //            //cellnew.Colspan = 2;
    //            cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
    //            tablenew.AddCell(cellnew);
    //            PdfPCell nesthousingn = new PdfPCell(tablenew);





    //            PdfPTable tablenew1 = new PdfPTable(2);

    //            // tablenew1.DefaultCell.FixedHeight = 100f;
    //            PdfPCell cellnew4 = new PdfPCell(new Phrase("PO No :    " + ds.Tables[0].Rows[0]["PONo"].ToString(), EmailFont));
    //            cellnew4.FixedHeight = 30f;
    //            cellnew4.HorizontalAlignment = 1;
    //            cellnew4.PaddingTop = 15f;
    //            cellnew4.PaddingBottom = 15f;
    //            PdfPCell cellnew5 = new PdfPCell(new Phrase("PO Date :  " + ds.Tables[0].Rows[0]["OrderDate1"].ToString(), EmailFont));
    //            cellnew5.HorizontalAlignment = 1;
    //            cellnew5.PaddingTop = 15f;
    //            cellnew5.PaddingBottom = 15f;

    //            tablenew1.AddCell(cellnew4);

    //            tablenew1.AddCell(cellnew5);



    //            PdfPTable tablevendor = new PdfPTable(2);

    //            PdfPCell suppliername = new PdfPCell(new Phrase("VENDOR NAME :    " + ds.Tables[0].Rows[0]["vendorName"].ToString(), EmailFont));
    //            suppliername.MinimumHeight = 80f;
    //            suppliername.PaddingTop = 10f;
    //            PdfPCell supplierdetails = new PdfPCell(new Phrase("VENDOR DETAILS:  \n\n\t Contact Person : " + ds.Tables[0].Rows[0]["vendorName"].ToString() + "\n\n\t Mobile No : " + ds.Tables[0].Rows[0]["MobileNo1"].ToString() + "\n\n\t Email : " + ds.Tables[0].Rows[0]["email"].ToString(), EmailFont));
    //            supplierdetails.PaddingTop = 10f;
    //            // PdfPCell termsnconditions = new PdfPCell(new Phrase("Terms & Conditions:    ", EmailFont));

    //            tablevendor.AddCell(suppliername);
    //            tablevendor.AddCell(supplierdetails);
    //            //tablevendor.AddCell(termsnconditions);



    //            PdfPCell nesthousingn1 = new PdfPCell(tablenew1);
    //            PdfPCell nesthousingn2 = new PdfPCell(tablevendor);
    //            // nesthousingn2.Height = 10f;
    //            //PdfPCell nesthousingn3 = new PdfPCell(tablenew3);


    //            nesthousingn.Border = Rectangle.NO_BORDER;

    //            //nesthousingn.PaddingBottom = 10f;
    //            Invoicetable2.AddCell(nesthousingnn);
    //            Invoicetable2.AddCell(nesthousingn);
    //            Invoicetable2.AddCell(nesthousingn1);
    //            Invoicetable2.AddCell(nesthousingn2);
    //            //Invoicetable2.AddCell(nesthousingn3);



    //        }
    //        //invoice repeat        

    //        // Invoicetable2.AddCell(headertable);
    //        Invoicetable2.SpacingBefore = 3f;
    //        //  Invoicetable. = 10f;

    //        // pdfDoc.Add(Invoicetable);




    //        #endregion

    //        #region Items Table
    //        //Create body table
    //        PdfPTable itemTable = new PdfPTable(3);

    //        itemTable.HorizontalAlignment = 0;
    //        itemTable.WidthPercentage = 100;
    //        // itemTable.SetWidths(new float[] { });  // then set the column's __relative__ widths
    //        //itemTable.SetWidths(new float[] { 4, 30, 6, 6, 6 });
    //        itemTable.SpacingAfter = 10;

    //        //itemTable.DefaultCell.Border = Rectangle.BOX;




    //        PdfPCell cell1 = new PdfPCell(new Phrase("PRODUCT", boldTableFont));
    //        //cell1.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell1);


    //        PdfPCell cell2 = new PdfPCell(new Phrase("IMAGE", boldTableFont));
    //        //cell2.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell2.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell2);


    //        /*
    //        PdfPCell cell3 = new PdfPCell(new Phrase("SIZE", boldTableFont));
    //        //cell4.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell3.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell3);
    //        */

    //        PdfPCell cell4 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
    //        //cell5.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell4.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell4);


    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {

    //            PdfPCell cell1i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["ProdName"].ToString(), bodyFont));
    //            //cell1.BackgroundColor = TabelHeaderBackGroundColor;
    //            cell1i.HorizontalAlignment = Element.ALIGN_CENTER;
    //            itemTable.AddCell(cell1i);

    //            //iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance("Image location");
    //            //PdfPCell cell = new PdfPCell(myImage);
    //            //content.AddCell(cell);
    //            //string xy = null;
    //            String Filepath = Server.MapPath("~/uploads/product/water/" + ds.Tables[0].Rows[i]["imagename"].ToString());
    //            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["imagename"].ToString()))
    //            {

    //                if (File.Exists(Filepath))
    //                {



    //                    //iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath(ds.Tables[0].Rows[i]["imagename"].ToString()));
    //                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Filepath);
    //                    //iTextSharp.text.Image.GetInstance(xy);
    //                    myImage.ScaleAbsolute(20f, 20f);
    //                    PdfPCell cell2i = new PdfPCell(myImage);
    //                    //cell2.BackgroundColor = TabelHeaderBackGroundColor;
    //                    cell2i.HorizontalAlignment = 1;
    //                    itemTable.AddCell(cell2i);
    //                }
    //                else
    //                {
    //                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath("uploads/ImageNotFound.png"));
    //                    myImage.ScaleAbsolute(20f, 20f);
    //                    PdfPCell cell2i = new PdfPCell(myImage);
    //                    //cell2.BackgroundColor = TabelHeaderBackGroundColor;
    //                    cell2i.HorizontalAlignment = 1;
    //                    itemTable.AddCell(cell2i);
    //                }
    //            }
    //            else
    //            {
    //                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath("uploads/ImageNotFound.png"));
    //                myImage.ScaleAbsolute(20f, 20f);
    //                PdfPCell cell2i = new PdfPCell(myImage);
    //                //cell2.BackgroundColor = TabelHeaderBackGroundColor;
    //                cell2i.HorizontalAlignment = 1;
    //                itemTable.AddCell(cell2i);
    //            }


    //            /*
    //            PdfPCell cell4i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Size"].ToString(), bodyFont));
    //            //cell4.BackgroundColor = TabelHeaderBackGroundColor;
    //            cell4i.HorizontalAlignment = 1;
    //            itemTable.AddCell(cell4i);
    //            */

    //            PdfPCell cell5i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Quantity1"].ToString(), bodyFont));
    //            //cell5.BackgroundColor = TabelHeaderBackGroundColor;
    //            cell5i.HorizontalAlignment = 1;
    //            itemTable.AddCell(cell5i);

    //            //    PdfPCell cell3i = new PdfPCell(new Phrase("" + dtOrderProducts.Rows[i]["Amount"], bodyFont));
    //            //    //cell3.BackgroundColor = TabelHeaderBackGroundColor;
    //            //    cell3i.HorizontalAlignment = 1;
    //            //    itemTable.AddCell(cell3i);

    //        }




    //        PdfPCell nesthousingn3 = new PdfPCell(itemTable);
    //        //  PdfPCell nesthousingn4 = new PdfPCell(totalTable);


    //        //nesthousingn.Border = Rectangle.NO_BORDER;

    //        //nesthousingn.PaddingBottom = 10f;
    //        Invoicetable2.AddCell(nesthousingn3);
    //        // Invoicetable2.AddCell(nesthousingn4);
    //        // pdfDoc.Add(Invoicetable2);
    //        #endregion





    //        PdfPTable noTable1 = new PdfPTable(2);

    //        noTable1.HorizontalAlignment = 0;
    //        noTable1.WidthPercentage = 100;
    //        // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
    //        //amtTable.SetWidths(new float[] { 4, 30});
    //        // amtTable.SpacingAfter = 10;

    //        noTable1.DefaultCell.Border = 0;


    //        PdfPCell cellnote1 = new PdfPCell(new Phrase("PREPARED BY\nNAME : " + "\nSIGNATURE : " + "\nDATE & TIME : ", EmailFont));
    //        cellnote1.Border = 0;
    //        cellnote1.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right


    //        noTable1.AddCell(cellnote1);




    //        PdfPCell cellnote3 = new PdfPCell(new Phrase("FOR MORYA TOOLS\n\n\n\nAUTHORIZED SIGNATORY", EmailFont));
    //        cellnote3.Border = 0;
    //        cellnote3.HorizontalAlignment = 2;//0=Left, 1=Centre, 2=Right


    //        noTable1.AddCell(cellnote3);





    //        PdfPCell nesthousingn5 = new PdfPCell(noTable1);
    //        Invoicetable2.AddCell(nesthousingn5);



    //        pdfDoc.Add(Invoicetable2);
    //        pdfDoc.Close();
    //        writer.Close();




    //        //flag = DownloadPDF(PDFData, po);




    //        //Context.Response.Clear();
    //        //Context.Response.ContentType = "application/json";
    //        //Context.Response.Flush();
    //        //Context.Response.Write(finalResult);
    //        //Context.Response.End();






    //        //}
    //    }
    //    catch { }
    //    finally { con.Close(); }

    //    return flag;


    //}
    
    #endregion

    #region--Download PDF
    protected Boolean DownloadPDF(System.IO.MemoryStream PDFData, String po)
    {
        try
        {
            // Clear response content & headers
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.Charset = string.Empty;
            HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Purchase Order-{0}.pdf", po));
            HttpContext.Current.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);

            //  HttpContext.Current.Response.OutputStream.
            HttpContext.Current.Response.OutputStream.Flush();
            HttpContext.Current.Response.OutputStream.Close();
            HttpContext.Current.Response.End();
        }
        catch { }
        finally { con.Close(); }
        return true;
    }
    #endregion








    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manageworksheets.aspx");
    }

    protected void ddlArticle_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["colorname"] = hfcolorname.Value;
        DataTable dtOperations = new DataTable();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "SELECT * FROM productparticulars WHERE productid = @productid",
                CommandType = CommandType.Text,
                Connection = con

            };
            cmd.Parameters.AddWithValue("@productid", ddlArticle.SelectedValue);
            con.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(dtOperations);

            if (dtOperations != null)
            {
                if (dtOperations.Rows.Count > 0)
                {

                    Session["operations"] = dtOperations;
                    ddlOperation.DataSource = dtOperations;
                    ddlOperation.DataTextField = "particulars";
                    ddlOperation.DataValueField = "id";
                    ddlOperation.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Operation--", "0");
                    ddlOperation.Items.Insert(0, objListItem);

                }
            }
            ViewState["Products"] = null;
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            Repeater1.Visible = false;
            ddlEmployee.SelectedIndex = 0;
            ddlOperation.SelectedIndex = 0;

            txtquantity.Text = "0";
        }
        catch (Exception ex)
        {
            ////ErrHandler.writeError(ex.Message, ex.StackTrace);

        }
        finally
        {
            con.Close();
        }
        
    }

    
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            DataTable dtOperations = new DataTable();
            DataTable dtEmployee = new DataTable();
            if (!String.IsNullOrEmpty(Session["employee"].ToString()))
            {
                dtEmployee = (DataTable)Session["employee"];
            }
            //if (!String.IsNullOrEmpty(Session["operations"].ToString()))
            {
                dtOperations = (DataTable)Session["operations"];
            }
            if (dtOperations != null)
            {
                if (dtOperations.Rows.Count > 0)
                {


                    //foreach (RepeaterItem item in Repeater1.Items)
                    //{
                        //if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        //{
                            var ddlOR = (DropDownList)e.Item.FindControl("ddlOperationRep");


                            ddlOR.DataSource = dtOperations;
                            ddlOR.DataTextField = "particulars";
                            ddlOR.DataValueField = "id";
                            ddlOR.DataBind();
                            System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Operation--", "0");
                            ddlOR.Items.Insert(0, objListItem);

                    Label index = (Label)e.Item.FindControl("lbloperationid");

                    ddlOR.SelectedValue = index.Text;
                    



                }
            }
            if (dtEmployee != null)
            {
                if (dtEmployee.Rows.Count > 0)
                {


                    var ddlER = (DropDownList)e.Item.FindControl("ddlEmployeeRep");


                    ddlER.DataSource = dtEmployee;
                    ddlER.DataTextField = "employeeName";
                    ddlER.DataValueField = "id";
                    ddlER.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Employee--", "0");
                    ddlER.Items.Insert(0, objListItem);
                    Label index = (Label)e.Item.FindControl("lblemployeeid");

                    ddlER.SelectedValue = index.Text;
                    


                }
            }

            
        }
    }

    protected void ddlOperationRep_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlOR = (DropDownList)sender;
        RepeaterItem row = (RepeaterItem)ddlOR.NamingContainer;
        int srno = int.Parse(((Label)row.FindControl("lblSrNo")).Text);

        dtProduct = (DataTable)ViewState["Products"];
        DataRow[] drr = dtProduct.Select("SrNo=' " + srno + " ' ");
        foreach (var newrow in drr)
        {
            newrow["operationid"] = ddlOR.SelectedValue;
            newrow["operation"] = ddlOR.SelectedItem;
        }

        dtProduct.AcceptChanges();

    }


    protected void ddlemployeeRep_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlER = (DropDownList)sender;
        RepeaterItem row = (RepeaterItem)ddlER.NamingContainer;
        int srno = int.Parse(((Label)row.FindControl("lblSrNo")).Text);

        dtProduct = (DataTable)ViewState["Products"];



        DataRow[] drr = dtProduct.Select("SrNo=' " + srno + " ' ");
        foreach (var newrow in drr)
        {
            newrow["employeeid"] = ddlER.SelectedValue;
            newrow["employee"] = ddlER.SelectedItem;
        }
        // dtProduct.Rows.RemoveAt(prodid);

        dtProduct.AcceptChanges();

    }

    protected void Quantity_TextChanged(object sender, EventArgs e)
    {
        TextBox qty = (TextBox)sender;
        RepeaterItem row = (RepeaterItem)qty.NamingContainer;
        //TextBox txtName = (TextBox)row.FindControl("TxtName");
        int srno = int.Parse(((Label)row.FindControl("lblSrNo")).Text);

        dtProduct = (DataTable)ViewState["Products"];


        // dtProduct = (DataTable)ViewState["Products"];

        DataRow[] drr = dtProduct.Select("SrNo=' " + srno + " ' ");
        foreach (var newrow in drr)
        {
            newrow["quantity"] = qty.Text;
        }
        // dtProduct.Rows.RemoveAt(prodid);

        dtProduct.AcceptChanges();
        //Repeater1.DataSource = dtProduct;
        //Repeater1.DataBind();

    }

    protected void Remark_TextChanged(object sender, EventArgs e)
    {
        TextBox remark = (TextBox)sender;
        RepeaterItem row = (RepeaterItem)remark.NamingContainer;
        //TextBox txtName = (TextBox)row.FindControl("TxtName");
        int srno = int.Parse(((Label)row.FindControl("lblSrNo")).Text);

        dtProduct = (DataTable)ViewState["Products"];


        // dtProduct = (DataTable)ViewState["Products"];

        DataRow[] drr = dtProduct.Select("SrNo=' " + srno + " ' ");
        foreach (var newrow in drr)
        {
            newrow["remark"] = remark.Text;
        }
        // dtProduct.Rows.RemoveAt(prodid);

        dtProduct.AcceptChanges();
        //Repeater1.DataSource = dtProduct;
        //Repeater1.DataBind();

    }

    protected void txtWorkDateRep_TextChanged(object sender, EventArgs e)
    {
        TextBox workdate = (TextBox)sender;
        RepeaterItem row = (RepeaterItem)workdate.NamingContainer;
        //TextBox txtName = (TextBox)row.FindControl("TxtName");
        int srno = int.Parse(((Label)row.FindControl("lblSrNo")).Text);

        dtProduct = (DataTable)ViewState["Products"];


        // dtProduct = (DataTable)ViewState["Products"];

        DataRow[] drr = dtProduct.Select("SrNo=' " + srno + " ' ");
        foreach (var newrow in drr)
        {
            newrow["date"] = workdate.Text;
        }
        // dtProduct.Rows.RemoveAt(prodid);

        dtProduct.AcceptChanges();
        //Repeater1.DataSource = dtProduct;
        //Repeater1.DataBind();

    }
}
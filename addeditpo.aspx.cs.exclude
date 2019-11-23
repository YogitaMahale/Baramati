using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditpo : System.Web.UI.Page
{
    common ocommon = new common();
    DataTable dtProduct = new DataTable();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    String usermail = String.Empty;
    Decimal totalamount = 0;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Bind();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Purchase Order";
        }
        if(! String.IsNullOrEmpty( Session["usermail"].ToString()))
            usermail = Session["usermail"].ToString();
    }

    private void Bind() {
        DataTable dtVendorName = new DataTable();
        DataTable dtCategory = new DataTable();
        if (dtProduct == null)
        {
            System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Product(s)--", "0");
            ddlProduct.Items.Insert(0, objListItem);
        } 
        try
        {


            
            //ProductMaster objProductMaster = new ProductMaster();
            Cls_VendorMaster_b clsbrand = new Cls_VendorMaster_b();
            dtVendorName = clsbrand.SelectAll();

            Cls_category_b clsCategory = new Cls_category_b();
            dtCategory = clsCategory.SelectAll();


           
        }
        catch { }
        finally { con.Close(); }

        if (dtVendorName != null)
        {
            if (dtVendorName.Rows.Count > 0)
            {
                ddlVendor.DataSource = dtVendorName;
                ddlVendor.DataTextField = "vendorName";
                ddlVendor.DataValueField = "vid";
                ddlVendor.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Vendor--", "0");
                ddlVendor.Items.Insert(0, objListItem);
            }
        }

        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                ddlCategory.DataSource = dtCategory;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "cid";
                ddlCategory.DataBind();
                System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Category--", "0");
                ddlCategory.Items.Insert(0, objListItem);
            }
        }
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 id = Int64.Parse(ddlVendor.SelectedValue.ToString());
        try
        {
            VendorMaster vm = new VendorMaster();
            DataTable dt = new DataTable();
            Cls_VendorMaster_b clsbrand = new Cls_VendorMaster_b();
            vm = clsbrand.SelectById(id);
            txtMobile.Text = vm.MobileNo1.ToString();
            txtEmail.Text = vm.email.ToString();
            //txtCity.Text = vm.fk_cityId.ToString();
            //txtState.Text = vm.fk_stateId.ToString();
            //txtCountry.Text = vm.fk_countryId.ToString();
            txtCity.Text = vm.city.ToString();
            txtState.Text = vm.state.ToString();
            txtCountry.Text = vm.country.ToString();
            txtAddress.Text = vm.Address1.ToString();

            
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            //return null;
        }
        finally { con.Close(); }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 id = Int64.Parse(ddlCategory.SelectedValue.ToString());
        try
        {
            product objProduct = new product();
            Cls_product_b clsProduct = new Cls_product_b();
            dtProduct = clsProduct.Product_WSSelectAllProductUsingCategoryId(id);
            if (dtProduct != null)
            {
                if (dtProduct.Rows.Count > 0)
                {
                    ddlProduct.DataSource = dtProduct;
                    ddlProduct.DataTextField = "productname";
                    ddlProduct.DataValueField = "pid";
                    ddlProduct.DataBind();
                    System.Web.UI.WebControls.ListItem objListItem = new System.Web.UI.WebControls.ListItem("--Select Product(s)--", "0");
                    ddlProduct.Items.Insert(0, objListItem);
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            //return null;
        }
        finally { con.Close(); }

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

            foreach(DataRow row in dtProduct.Select()){
                totalamount += Convert.ToDecimal( row["Total"].ToString()); 
            }


        }
        else
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            Repeater1.Visible = false;
        }

        // ddlProductName.SelectedItem = string.Empty;
        //ddlCategory.SelectedIndex = 0;
        //ddlProduct.SelectedIndex = 0;
       
        txtquantity.Text = "0";
        txttotal.Text = "0";
        txttotalamount.Text = totalamount.ToString();
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
            dtProduct.Columns.Add(new DataColumn("CatId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("ProdId", typeof(int)));
            dtProduct.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dtProduct.Columns.Add(new DataColumn("Total", typeof(string)));

            ViewState["Products"] = dtProduct;
        }
        else
        {
            dtProduct = (DataTable)ViewState["Products"];
        }
        DataRow dtRow = dtProduct.NewRow();

        dtRow["SrNo"] = ViewState["SrNo"];
        dtRow["ProductName"] = ddlProduct.SelectedItem.ToString();
        dtRow["ProdId"] = ddlProduct.SelectedValue;
        dtRow["CategoryName"] = ddlCategory.SelectedItem.ToString();
        dtRow["CatId"] = ddlCategory.SelectedValue;
        dtRow["Rate"] = txtrate.Text.Trim();
        dtRow["Quantity"] = txtquantity.Text.Trim();
        dtRow["Total"] = txttotal.Text.Trim();
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
                dtProduct.Columns.Add(new DataColumn("ProductName", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("Rate", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("Quantity", typeof(string)));
                dtProduct.Columns.Add(new DataColumn("Total", typeof(string)));

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
            ddlProduct.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            txtquantity.Text = "0";
            txtrate.Text = "0";
            txttotal.Text = "0";
            

            foreach (DataRow row in dtProduct.Select())
            {
                totalamount += Convert.ToDecimal(row["Total"].ToString());
            }

            txttotalamount.Text = totalamount.ToString();
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
        Int64 Result = 0, Result1=0;
        DataTable dtProduct = new DataTable();


        String user = null, month = null, pono = null;
        bool flag = false, flag1 = false, flag2 = false, flag3 = false;

        int year = int.Parse(DateTime.Now.Year.ToString());
        month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        int day = int.Parse(DateTime.Now.Day.ToString());
        int min = int.Parse(DateTime.Now.Minute.ToString());
        int hour = int.Parse(DateTime.Now.Hour.ToString());
        String ticks = DateTime.Now.Ticks.ToString();
        ticks = ticks.Substring(ticks.Length - 2, 2);


        pono = year + "_" + month.Substring(0, 3).ToUpper() + "_" + day + "_" + hour + min+ ticks;

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        PurchaseOrderHeader objcategory = new PurchaseOrderHeader();
        objcategory.PONo = pono;
        objcategory.VendorId = Int64.Parse(ddlVendor.SelectedValue);

        objcategory.isdeleted = false;
        objcategory.totalamount = Convert.ToDecimal(txttotalamount.Text);

        if (Request.QueryString["id"] != null)
        {
            objcategory.PurchaseOrderId = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_PurchaseOrderHeader_b().Update(objcategory));
            if (Result > 0)
            {
                
                Response.Redirect(Page.ResolveUrl("~/managepurchaseorder.aspx?mode=u"));
            }
            else
            {
                
            }
        }
        else
        {
            Result = (new Cls_PurchaseOrderHeader_b().Insert(objcategory));
            if (Result > 0)
            {
                con.Open();
                if (ViewState["Products"] != null)
                    dtProduct = (DataTable)ViewState["Products"];
                PurchaseOrderDetails objPod = new PurchaseOrderDetails();
                foreach (DataRow row in dtProduct.Rows)
                {
                    /*
                    string s = "INSERT INTO [DoctorDiagnosisNew].[PurchaseOrderDetails]([PurchaseOrderId],[ProdId],[BrandId],[SizeId],[Quantity],[isdeleted],[Quantity1]) VALUES("
                        + Result + "," + row["ProdId"] + "," + row["BrandId"] + "," + row["SizeId"] + "," + row["Quantity"] + "," + 0 + "," + row["Quantity"] + ")";
                    SqlCommand cmd = new SqlCommand(s, con);
                     * */
                    objPod.PurchaseOrderId = Result;
                    objPod.ProdId = Convert.ToInt64(row["ProdId"]);
                    objPod.CategoryId = Convert.ToInt64(row["CatId"]);
                    objPod.Quantity = Convert.ToInt64(row["Quantity"]);
                    objPod.Quantity1 = Convert.ToInt64(row["Quantity"]);
                    objPod.Rate = Convert.ToDecimal(row["Rate"]);

                    Result1 = (new Cls_PurchaseOrderDetails_b().Insert(objPod));
                    
                    //if (Result1 > 0)
                       // flag = true;
                    

                }

                con.Close();

                //Clear();

                String vendorname = ddlVendor.SelectedItem.ToString();
                String mailTo = txtEmail.Text.Trim();
                //String Name = txt_contactperson.Text.Trim();
                String MobileNo = txtMobile.Text.Trim();
                flag = PDFUpload(Result);
                flag1 = SendOrderMail(mailTo, pono);
                //flag2 = SendSMS(vendorname, MobileNo);

                if (flag)
                    Response.Redirect(Page.ResolveUrl("~/managepurchaseorder.aspx?mode=i"));
            }
            else
            {
                

            }
        }
    }



    #region-- Send Mail
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
    #endregion

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



    #region Purchase Order
    public Boolean PDFUpload(Int64 PurchaseOrderId)
    {
        string finalResult = string.Empty;
        bool flag = true;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        
        Int64 inid = 0;
        try
        {
        

            //string query = "Select POH.*,CONVERT(nvarchar, POH.OrderDate,103) + ' '+REPLACE(REPLACE(CONVERT(varchar(15), CAST(POH.OrderDate AS TIME), 100), 'P', ' P'), 'A', ' A') as OrderDate1, POD.*, PM.ProdName as ProdName , VM.*, BM.BrandName as Brand, SM.Size as Size from [DoctorDiagnosisNew].[PurchaseOrderHeader] POH ,[DoctorDiagnosisNew].[PurchaseOrderDetails] POD ,[dbo].[VendorMaster] VM ,[dbo].[BrandMaster] BM ,[dbo].[SizeMaster] SM ,[dbo].[ProductMaster] PM where POH.isdeleted=0 AND POH.[PurchaseOrderId] = " + CategoryId + " AND POH.[VendorId] = VM.[VendorId] AND POH.[PurchaseOrderId] = POD.[PurchaseOrderId] AND POD.[BrandId] = BM.[BrandId] AND POD.[SizeId] = SM.[SizeId] AND POD.[ProdId] = PM.[ProdId] ORDER BY ProdName DESC";
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "PurchaseOrderHeader_SelectById";
            cmd.Parameters.AddWithValue("@id", PurchaseOrderId);
            //cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;



            //SqlDataAdapter da = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            String po = ds.Tables[0].Rows[0]["PONo"].ToString();
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            //PdfWriter pw = PdfWriter.GetInstance(
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Context.Server.MapPath("~/uploads/PurchaseOrders/") + po + ".pdf", FileMode.Create));
        
            var titleFont = FontFactory.GetFont("Arial", 6, Font.NORMAL);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
            var boldTableFont1 = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
            var bodyFont = FontFactory.GetFont("Arial", 7, Font.NORMAL);//8
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK);//8
            var HeaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK);

            var footerfont = FontFactory.GetFont("Arial", 6, Font.NORMAL, Color.BLACK);//8
            Color TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 220f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            headertable.DefaultCell.Border = Rectangle.BOX; //for testing           



            // invoice rperte

            PdfPTable Invoicetable2 = new PdfPTable(1);
            Invoicetable2.HorizontalAlignment = 0;
            Invoicetable2.WidthPercentage = 100;
            Invoicetable2.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
            Invoicetable2.DefaultCell.Border = Rectangle.NO_BORDER;

            {

                PdfPTable mainN = new PdfPTable(1);
                // tablenew.HorizontalAlignment = 1;
                //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
            //    PdfPCell cellN = new PdfPCell(new Phrase("Trimurti Diagnostics \n\n Home/Clinic Collection No : 7777 8866 04/05, 9766 6600 83 | 0253 2376062 "
            //+ "\n\n28, Purab Paschim Plaza, Divya Adlabs Building, Trimurti Chowk, CIDCO, Nashik - 422008", HeaderFont));
                PdfPCell cellN = new PdfPCell(new Phrase("Morya Tools \n\n 22, Pradhan Park, M.G Road, Nashik, Maharashtra, India 422001 "
            + "\n\nPhone: (0253) 3014578 Email: kshatriya.enterprises@gmail.com", HeaderFont));
                cellN.PaddingTop = 15f;
                cellN.PaddingBottom = 15f;
                cellN.VerticalAlignment = 1;
                //cellnew.Colspan = 2;
                cellN.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                mainN.AddCell(cellN);
                PdfPCell nesthousingnn = new PdfPCell(mainN);

                PdfPTable tablenew = new PdfPTable(1);
                // tablenew.HorizontalAlignment = 1;
                //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
                PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER", EmailFont));
                cellnew.PaddingTop = 15f;
                cellnew.PaddingBottom = 15f;
                cellnew.VerticalAlignment = 1;
                //cellnew.Colspan = 2;
                cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                tablenew.AddCell(cellnew);
                PdfPCell nesthousingn = new PdfPCell(tablenew);





                PdfPTable tablenew1 = new PdfPTable(2);

                // tablenew1.DefaultCell.FixedHeight = 100f;
                PdfPCell cellnew4 = new PdfPCell(new Phrase("PO No :    " + ds.Tables[0].Rows[0]["PONo"].ToString(), EmailFont));
                cellnew4.FixedHeight = 30f;
                cellnew4.HorizontalAlignment = 1;
                cellnew4.PaddingTop = 15f;
                cellnew4.PaddingBottom = 15f;
                PdfPCell cellnew5 = new PdfPCell(new Phrase("PO Date :  " + ds.Tables[0].Rows[0]["OrderDate1"].ToString(), EmailFont));
                cellnew5.HorizontalAlignment = 1;
                cellnew5.PaddingTop = 15f;
                cellnew5.PaddingBottom = 15f;

                tablenew1.AddCell(cellnew4);

                tablenew1.AddCell(cellnew5);



                PdfPTable tablevendor = new PdfPTable(2);

                PdfPCell suppliername = new PdfPCell(new Phrase("VENDOR NAME :    " + ds.Tables[0].Rows[0]["vendorName"].ToString(), EmailFont));
                suppliername.MinimumHeight = 80f;
                suppliername.PaddingTop = 10f;
                PdfPCell supplierdetails = new PdfPCell(new Phrase("VENDOR DETAILS:  \n\n\t Contact Person : " + ds.Tables[0].Rows[0]["vendorName"].ToString() + "\n\n\t Mobile No : " + ds.Tables[0].Rows[0]["MobileNo1"].ToString() + "\n\n\t Email : " + ds.Tables[0].Rows[0]["email"].ToString(), EmailFont));
                supplierdetails.PaddingTop = 10f;
                // PdfPCell termsnconditions = new PdfPCell(new Phrase("Terms & Conditions:    ", EmailFont));

                tablevendor.AddCell(suppliername);
                tablevendor.AddCell(supplierdetails);
                //tablevendor.AddCell(termsnconditions);



                PdfPCell nesthousingn1 = new PdfPCell(tablenew1);
                PdfPCell nesthousingn2 = new PdfPCell(tablevendor);
                // nesthousingn2.Height = 10f;
                //PdfPCell nesthousingn3 = new PdfPCell(tablenew3);


                nesthousingn.Border = Rectangle.NO_BORDER;

                //nesthousingn.PaddingBottom = 10f;
                Invoicetable2.AddCell(nesthousingnn);
                Invoicetable2.AddCell(nesthousingn);
                Invoicetable2.AddCell(nesthousingn1);
                Invoicetable2.AddCell(nesthousingn2);
                //Invoicetable2.AddCell(nesthousingn3);



            }
            //invoice repeat        

            // Invoicetable2.AddCell(headertable);
            Invoicetable2.SpacingBefore = 3f;
            //  Invoicetable. = 10f;

            // pdfDoc.Add(Invoicetable);




            #endregion

            #region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(3);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            // itemTable.SetWidths(new float[] { });  // then set the column's __relative__ widths
            //itemTable.SetWidths(new float[] { 4, 30, 6, 6, 6 });
            itemTable.SpacingAfter = 10;

            //itemTable.DefaultCell.Border = Rectangle.BOX;




            PdfPCell cell1 = new PdfPCell(new Phrase("PRODUCT", boldTableFont));
            //cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);

            
            PdfPCell cell2 = new PdfPCell(new Phrase("IMAGE", boldTableFont));
            //cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);


            /*
            PdfPCell cell3 = new PdfPCell(new Phrase("SIZE", boldTableFont));
            //cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = 1;
            itemTable.AddCell(cell3);
            */

            PdfPCell cell4 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
            //cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = 1;
            itemTable.AddCell(cell4);


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                PdfPCell cell1i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["ProdName"].ToString(), bodyFont));
                //cell1.BackgroundColor = TabelHeaderBackGroundColor;
                cell1i.HorizontalAlignment = Element.ALIGN_CENTER;
                itemTable.AddCell(cell1i);

                //iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance("Image location");
                //PdfPCell cell = new PdfPCell(myImage);
                //content.AddCell(cell);
                //string xy = null;
                String Filepath = Server.MapPath("~/uploads/product/water/" + ds.Tables[0].Rows[i]["imagename"].ToString());
                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["imagename"].ToString()))
                {

                    if (File.Exists(Filepath))
                    {

                        
                    
                    //iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath(ds.Tables[0].Rows[i]["imagename"].ToString()));
                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Filepath);
                    //iTextSharp.text.Image.GetInstance(xy);
                    myImage.ScaleAbsolute(20f, 20f);
                    PdfPCell cell2i = new PdfPCell(myImage);
                    //cell2.BackgroundColor = TabelHeaderBackGroundColor;
                    cell2i.HorizontalAlignment = 1;
                    itemTable.AddCell(cell2i);
                    }
                    else
                    {
                        iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath("uploads/ImageNotFound.png"));
                        myImage.ScaleAbsolute(20f, 20f);
                        PdfPCell cell2i = new PdfPCell(myImage);
                        //cell2.BackgroundColor = TabelHeaderBackGroundColor;
                        cell2i.HorizontalAlignment = 1;
                        itemTable.AddCell(cell2i);
                    }
                }
                else
                {
                    iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath("uploads/ImageNotFound.png"));
                    myImage.ScaleAbsolute(20f, 20f);
                    PdfPCell cell2i = new PdfPCell(myImage);
                    //cell2.BackgroundColor = TabelHeaderBackGroundColor;
                    cell2i.HorizontalAlignment = 1;
                    itemTable.AddCell(cell2i);
                }


                /*
                PdfPCell cell4i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Size"].ToString(), bodyFont));
                //cell4.BackgroundColor = TabelHeaderBackGroundColor;
                cell4i.HorizontalAlignment = 1;
                itemTable.AddCell(cell4i);
                */

                PdfPCell cell5i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Quantity1"].ToString(), bodyFont));
                //cell5.BackgroundColor = TabelHeaderBackGroundColor;
                cell5i.HorizontalAlignment = 1;
                itemTable.AddCell(cell5i);

                //    PdfPCell cell3i = new PdfPCell(new Phrase("" + dtOrderProducts.Rows[i]["Amount"], bodyFont));
                //    //cell3.BackgroundColor = TabelHeaderBackGroundColor;
                //    cell3i.HorizontalAlignment = 1;
                //    itemTable.AddCell(cell3i);

            }




            PdfPCell nesthousingn3 = new PdfPCell(itemTable);
            //  PdfPCell nesthousingn4 = new PdfPCell(totalTable);


            //nesthousingn.Border = Rectangle.NO_BORDER;

            //nesthousingn.PaddingBottom = 10f;
            Invoicetable2.AddCell(nesthousingn3);
            // Invoicetable2.AddCell(nesthousingn4);
            // pdfDoc.Add(Invoicetable2);
            #endregion





            PdfPTable noTable1 = new PdfPTable(2);

            noTable1.HorizontalAlignment = 0;
            noTable1.WidthPercentage = 100;
            // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
            //amtTable.SetWidths(new float[] { 4, 30});
            // amtTable.SpacingAfter = 10;

            noTable1.DefaultCell.Border = 0;


            PdfPCell cellnote1 = new PdfPCell(new Phrase("PREPARED BY\nNAME : " + "\nSIGNATURE : " + "\nDATE & TIME : ", EmailFont));
            cellnote1.Border = 0;
            cellnote1.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right


            noTable1.AddCell(cellnote1);




            PdfPCell cellnote3 = new PdfPCell(new Phrase("FOR MORYA TOOLS\n\n\n\nAUTHORIZED SIGNATORY", EmailFont));
            cellnote3.Border = 0;
            cellnote3.HorizontalAlignment = 2;//0=Left, 1=Centre, 2=Right


            noTable1.AddCell(cellnote3);





            PdfPCell nesthousingn5 = new PdfPCell(noTable1);
            Invoicetable2.AddCell(nesthousingn5);



            pdfDoc.Add(Invoicetable2);
            pdfDoc.Close();
            writer.Close();



                
             //flag = DownloadPDF(PDFData, po);




             //Context.Response.Clear();
             //Context.Response.ContentType = "application/json";
             //Context.Response.Flush();
             //Context.Response.Write(finalResult);
             //Context.Response.End();






            //}
        }
        catch { }
        finally { con.Close(); }

        return flag;


    }

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
        Response.Redirect("~/managepurchaseorder.aspx");
    }

    protected void txtratequantity_TextChanged(object sender, EventArgs e)
    {
        Decimal total = Convert.ToDecimal(txtrate.Text.ToString()) * Convert.ToDecimal(txtquantity.Text.ToString());
        txttotal.Text = total.ToString();
    }
}
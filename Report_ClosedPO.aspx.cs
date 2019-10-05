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

public partial class Report_ClosedPO : System.Web.UI.Page
{
    common ocommon = new common();
        DataTable dtCategory = new DataTable();
        DataTable dtPurchaseOrder = new DataTable("PurchaseOrder");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategory();
                HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
                hPageTitle.InnerText = "Closed POs";
            }

            //if (Request.QueryString["mode"] == "u")
            //{
            //    spnMessage.Visible = true;
            //    spnMessage.Style.Add("color", "green");
            //    spnMessage.InnerText = "Purchase Order Updated Successfully";
            //}
            //else if (Request.QueryString["mode"] == "i")
            //{
            //    spnMessage.Visible = true;
            //    spnMessage.Style.Add("color", "green");
            //    spnMessage.InnerText = "Purchase Order Raised!!!";
            //}
        }
        private void BindCategory()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);




            try
            {

                con.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PurchaseOrderHeader_SelectAllClosed";
                // cmd.Parameters.AddWithValue("@ProdBrand", ProdBrand);
                //cmd.Parameters.AddWithValue("@password", password);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                sda.Fill(dtCategory);
            }
            catch { }
            finally { con.Close(); }


            if (dtCategory != null)
            {
                if (dtCategory.Rows.Count > 0)
                {

                    ViewState["PurchaseOrder"] = dtCategory;
                    repCategory.DataSource = dtCategory;
                    repCategory.DataBind();
                }
                else
                {
                    repCategory.DataSource = null;
                    repCategory.DataBind();
                }
            }
            else
            {
                repCategory.DataSource = null;
                repCategory.DataBind();
            }
            //foreach (RepeaterItem item in repCategory.Items)
            //{
            //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            //    {
                  
            //        Button grn = (Button)item.FindControl("btnGRN");
            //        Button view = (Button)item.FindControl("btnView");
            //        Label status = (Label)item.FindControl("lblStatus");
            //        if (status.Text == "True")
            //        {
            //            view.Text = "CLOSED";
            //            view.CssClass = "btn btn-sm btn-danger";
            //            //view.Enabled = false;

            //            grn.Visible = false;
            //        }
                    
            //    }
            //}
        }
        protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                
                HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
                


            }
        }

        //protected void lnkDelete_Click(object sender, EventArgs e)
        //{
        //    RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        //    //Int64 ProductCount = int.Parse((item.FindControl("lblProductCount") as Label).Text);
        //    //spnMessage.Visible = true;
        //    //if (ProductCount.ToString() == "0")
        //    //{
        //    Int64 CategoryId = int.Parse((item.FindControl("lblid") as Label).Text);
        //    bool yes = (new Cls_ProductMaster_b().Delete(CategoryId));

        //    if (yes)
        //    {
        //        BindCategory();
        //        spnMessage.Style.Add("color", "green");
        //        spnMessage.InnerText = "Product Deleted Successfully";
        //    }
        //    else
        //    {
        //        spnMessage.Style.Add("color", "red");
        //        spnMessage.InnerText = "Product Not Deleted";
        //    }
        //    //}
        //    //else
        //    //{
        //    //    spnMessage.Style.Add("color", "red");
        //    //    spnMessage.InnerText = "In this category product added..so you can not delete.";
        //    //}

        //}

        //protected void btnAddNEW_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(Page.ResolveUrl("~/frm_PurchaseOrder.aspx"));
        //}

        protected void btnView_Click(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            //Get the command argument
            string ca = button.CommandArgument;
            string commandArgument = button.CommandArgument;
            Session["PurchaseOrderId"] = ca;
            Session["Parent"] = "CP";
            //Response.Redirect(Page.ResolveUrl("~/ShowPurchaseOrder.aspx"));

        }

        //protected void btnGRN_Click(object sender, EventArgs e)
        //{
        //    Button button = (sender as Button);
        //    //Get the command argument
        //    string ca = button.CommandArgument;
        //    string commandArgument = button.CommandArgument;
        //    Session["PurchaseOrderId"] = ca;
        //    Response.Redirect(Page.ResolveUrl("~/GRN.aspx"));

    //}

    #region ddlMonth Change
    //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    int index = ddlMonth.SelectedIndex;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);

        //    try
        //    {
        //        if (index == 0)
        //        {
        //            BindCategory();
        //        }
        //        else
        //        {

                    
        //            con.Open();
                    
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.CommandText = "PurchaseOrderByMonth";
        //            cmd.Parameters.AddWithValue("@index", index);
                    
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlDataAdapter sda = new SqlDataAdapter();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            sda.Fill(dtCategory);
        //            con.Close();
        //        }
        //    }
        //    catch { }
        //    finally { con.Close(); }


        //    if (dtCategory != null)
        //    {
        //        if (dtCategory.Rows.Count > 0)
        //        {

                    
        //            repCategory.DataSource = dtCategory;
        //            repCategory.DataBind();
        //        }
        //        else
        //        {
        //            repCategory.DataSource = null;
        //            repCategory.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        repCategory.DataSource = null;
        //        repCategory.DataBind();
        //    }


    //}
    #endregion

    #region Purchase Order
    //public void btnDownload_Click(object sender, EventArgs e)
        //{
        //    string finalResult = string.Empty;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        //    Button button = (sender as Button);
        //    string commandArgument = button.CommandArgument;
        //    Session["PurchaseOrderId"] = commandArgument;

        //    Int64 inid = 0;
        //    try
        //    {
        //        if (Session["PurchaseOrderId"] == null || Session["PurchaseOrderId"].ToString() == "")
        //        {
        //        }
        //        else
        //        {
        //            Int64 CategoryId = Int64.Parse(Session["PurchaseOrderId"].ToString());
        //            // PurchaseOrderHeader objcategory = (new Cls_PurchaseOrderHeader_b().SelectById(CategoryId));


        //            string query = "Select POH.*,CONVERT(nvarchar, POH.OrderDate,103) + ' '+REPLACE(REPLACE(CONVERT(varchar(15), CAST(POH.OrderDate AS TIME), 100), 'P', ' P'), 'A', ' A') as OrderDate1, POD.*, PM.ProdName as ProdName , VM.*, BM.BrandName as Brand, SM.Size as Size from [DoctorDiagnosisNew].[PurchaseOrderHeader] POH ,[DoctorDiagnosisNew].[PurchaseOrderDetails] POD ,[dbo].[VendorMaster] VM ,[dbo].[BrandMaster] BM ,[dbo].[SizeMaster] SM ,[dbo].[ProductMaster] PM where POH.isdeleted=0 AND POH.[PurchaseOrderId] = " + CategoryId + " AND POH.[VendorId] = VM.[VendorId] AND POH.[PurchaseOrderId] = POD.[PurchaseOrderId] AND POD.[BrandId] = BM.[BrandId] AND POD.[SizeId] = SM.[SizeId] AND POD.[ProdId] = PM.[ProdId] ORDER BY ProdName DESC";
        //            //string s = "select  [ID],[PatientName],(select [TestName] from [dbo].[tbl_TestMaster] where [ID]=[testID])as testID,[TotalAmt],[PaidAmt],[Remaining],[date1],(select [DoctorName] from [dbo].[tbl_DoctorRegistration] where[did] =[DoctorID])as DoctorID,[Address],[MobileNo],(select [Name]  from [dbo].[tbl_userRegistration] where [uid]=[USerID])as USerID from [DoctorDiagnosisNew].[tbl_PatientDetails] where [DoctorID]=" + drop_doctor.SelectedValue + " and [USerID]=" + drop_user.SelectedValue + " and [date1]='" + Calendar1.SelectedDate.ToString("MM/dd/yyyy") + "'";
        //            //   string s = "select  DISTICT([ID]),[PatientName],[TotalAmt],[PaidAmt],[Remaining],[date1],(select [DoctorName] from [dbo].[tbl_DoctorRegistration] where[did] =[DoctorID])as DoctorID,[Address],[MobileNo],(select [Name]  from [dbo].[tbl_userRegistration] where [uid]=[USerID])as USerID from [DoctorDiagnosisNew].[tbl_PatientDetails] where [DoctorID]=" + drop_doctor.SelectedValue + " and [USerID]=" + drop_user.SelectedValue + " and [date1]='" + Calendar1.SelectedDate.ToString("MM/dd/yyyy") + "'";
        //            //  string s = "select  distinct([ID]),[PatientName],[TotalAmt],[PaidAmt],[Remaining],[date1],(select [DoctorName] from [dbo].[tbl_DoctorRegistration] where[did] =[DoctorID])as DoctorID,[Address],[MobileNo],(select [Name]  from [dbo].[tbl_userRegistration] where [uid]=[USerID])as USerID from [DoctorDiagnosisNew].[tbl_PatientDetails] where [DoctorID]=" + drop_doctor.SelectedValue + " and [USerID]=" + drop_user.SelectedValue + " and [date1]='" + Calendar1.SelectedDate.ToString("MM/dd/yyyy") + "'";
        //            DataSet ds = new DataSet();
        //            SqlDataAdapter da = new SqlDataAdapter(query, con);
        //            da.Fill(ds);
        //            String po = ds.Tables[0].Rows[0]["PONo"].ToString();
        //            /* if (ds.Tables[0].Rows.Count > 0)
        //             //VendorId, VendorName, VendorContactPerson, VendorPhone, VendorAddress, VendorGST, VendorEmailId, isactive, isdeleted
        //             {
        //                 lbl_ID.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
        //                 lbl_vendorName.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
        //                 lbl_contactPerson.Text = ds.Tables[0].Rows[0]["VendorContactPerson"].ToString();
        //                 lbl_mobile.Text = ds.Tables[0].Rows[0]["VendorPhone"].ToString();
        //                 lbl_email.Text = ds.Tables[0].Rows[0]["VendorEmailId"].ToString();

        //             }
        //             Repeater1.DataSource = ds.Tables[0];
        //             Repeater1.DataBind();
        //             */

        //            // public void PDFDesign() {
        //            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
        //            MemoryStream PDFData = new MemoryStream();
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

        //            var titleFont = FontFactory.GetFont("Arial", 6, Font.NORMAL);
        //            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLUE);
        //            //var boldTableFont = FontFactory.GetFont("Arial", 6, Font.BOLD);//8
        //            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
        //            var boldTableFont1 = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
        //            var bodyFont = FontFactory.GetFont("Arial", 7, Font.NORMAL);//8
        //            var EmailFont = FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK);//8
        //            var HeaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK);

        //            //var EmailFontBold = FontFactory.GetFont("Arial", 14, Font.BOLD, Color.BLACK);//8
        //            var footerfont = FontFactory.GetFont("Arial", 6, Font.NORMAL, Color.BLACK);//8
        //            Color TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

        //            Rectangle pageSize = writer.PageSize;
        //            // Open the Document for writing
        //            pdfDoc.Open();
        //            //Add elements to the document here

        //            #region Top table
        //            // Create the header table 
        //            PdfPTable headertable = new PdfPTable(3);
        //            headertable.HorizontalAlignment = 0;
        //            headertable.WidthPercentage = 100;
        //            headertable.SetWidths(new float[] { 100f, 320f, 220f });  // then set the column's __relative__ widths
        //            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
        //            headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

        //            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/500.png"));

        //            //logo.ScaleToFit(70, 120);
        //            //{
        //            //    PdfPCell pdfCelllogo = new PdfPCell(logo);
        //            //    pdfCelllogo.Border = Rectangle.NO_BORDER;
        //            //    pdfCelllogo.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //            //    pdfCelllogo.BorderWidthBottom = 1f;
        //            //    pdfCelllogo.Top = 0;
        //            //    headertable.AddCell(pdfCelllogo);
        //            //}

        //            //{
        //            //    PdfPCell middlecell = new PdfPCell(new Phrase("GIRI MEDIA \n SERVICES PVT LTD", boldTableFont1));
        //            //    middlecell.Border = Rectangle.NO_BORDER;
        //            //    // middlecell.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //            //    middlecell.BorderWidthBottom = 1f;
        //            //    headertable.AddCell(middlecell);
        //            //}

        //            //{
        //            //    PdfPTable nested = new PdfPTable(1);
        //            //    nested.DefaultCell.Border = Rectangle.NO_BORDER;



        //            //    PdfPCell nextPostCell1 = new PdfPCell(new Phrase("THE INDIAN NEWSPAPER SOCIETY", boldTableFont1));
        //            //    nextPostCell1.Border = Rectangle.NO_BORDER;
        //            //    nextPostCell1.HorizontalAlignment = 1;
        //            //    nested.AddCell(nextPostCell1);
        //            //    PdfPCell nextPostCell2 = new PdfPCell(new Phrase("\"INS\" Accredited Agency", boldTableFont1));
        //            //    nextPostCell2.HorizontalAlignment = 1;
        //            //    nextPostCell2.Border = Rectangle.NO_BORDER;
        //            //    nested.AddCell(nextPostCell2);
        //            //    PdfPCell nextPostCell3 = new PdfPCell(new Phrase("CODE : 11091", boldTableFont1));
        //            //    nextPostCell3.HorizontalAlignment = 1;
        //            //    nextPostCell3.Border = Rectangle.NO_BORDER;
        //            //    nested.AddCell(nextPostCell3);
        //            //    //PdfPCell nextPostCell4 = new PdfPCell(new Phrase("The Leading Manufacturing Company For exercise Notebook & All kind of stationary products", titleFont));
        //            //    //nextPostCell4.Border = Rectangle.NO_BORDER;
        //            //    //nested.AddCell(nextPostCell4);


        //            //    //nested.AddCell("");


        //            //    PdfPCell nesthousing = new PdfPCell(nested);
        //            //    nesthousing.Border = Rectangle.NO_BORDER;
        //            //    //nesthousing.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //            //    //nesthousing.BorderWidthBottom = 1f;
        //            //    // nesthousing.Rowspan = 5;
        //            //    //nesthousing.PaddingBottom = 10f;
        //            //    headertable.AddCell(nesthousing);

        //            //    //pdfDoc.Add(headertable);

        //            //    PdfPTable headerbottom1 = new PdfPTable(1);
        //            //    headerbottom1.DefaultCell.Border = Rectangle.NO_BORDER;
        //            //    PdfPCell bottomcell1 = new PdfPCell(new Phrase("4. Shree Sankul, Above Saroj Travels, Opp. Mahamarg Bus Stand, Mumbai Naka, Nashik-422009. T: +91 253 250 15 85", boldTableFont1));
        //            //    bottomcell1.Colspan = 2;
        //            //    bottomcell1.HorizontalAlignment = 0;
        //            //    bottomcell1.Border = Rectangle.NO_BORDER;
        //            //    headerbottom1.AddCell(bottomcell1);

        //            //    PdfPCell headernest1 = new PdfPCell(headerbottom1);
        //            //    headernest1.Border = Rectangle.NO_BORDER;

        //            //    //headertable.AddCell(headernest1);



        //            //    PdfPTable headerbottom2 = new PdfPTable(3);
        //            //    headerbottom2.DefaultCell.Border = Rectangle.NO_BORDER;
        //            //    PdfPCell bottomcell21 = new PdfPCell(new Phrase("CIN : U74300MH2008PTC179935", boldTableFont1));
        //            //    bottomcell21.HorizontalAlignment = 0;
        //            //    bottomcell21.Border = Rectangle.NO_BORDER;
        //            //    headerbottom2.AddCell(bottomcell21);

        //            //    PdfPCell bottomcell22 = new PdfPCell(new Phrase("GSTN : 27AADCG1023F1Z6", boldTableFont1));
        //            //    bottomcell22.HorizontalAlignment = 0;
        //            //    bottomcell22.Border = Rectangle.NO_BORDER;
        //            //    headerbottom2.AddCell(bottomcell22);

        //            //    PdfPCell bottomcell23 = new PdfPCell(new Phrase("PAN : AADCG1023F", boldTableFont1));
        //            //    bottomcell23.HorizontalAlignment = 0;
        //            //    bottomcell23.Border = Rectangle.NO_BORDER;
        //            //    headerbottom2.AddCell(bottomcell23);

        //            //    PdfPCell headernest2 = new PdfPCell(headerbottom2);
        //            //    headernest2.Border = Rectangle.NO_BORDER;


        //            //    //headertable.AddCell(headernest2);

        //            //    PdfPTable headerbottom3 = new PdfPTable(1);
        //            //    headerbottom3.DefaultCell.Border = Rectangle.NO_BORDER;
        //            //    PdfPCell bottomcell31 = new PdfPCell(new Phrase("GST NO : 27AADCG1023F1Z6", boldTableFont1));
        //            //    bottomcell31.HorizontalAlignment = 2;
        //            //    bottomcell31.Border = Rectangle.NO_BORDER;
        //            //    headerbottom3.AddCell(bottomcell31);

        //            //    PdfPCell headernest3 = new PdfPCell(headerbottom3);
        //            //    headernest3.Border = Rectangle.NO_BORDER;
        //            //    //headernest3.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //            //    //headernest3.BorderWidthBottom = 1f;
        //            //    // nesthousing.Rowspan = 5;
        //            //    //headernest3.PaddingBottom = 10f;


        //            //    headertable.AddCell(headernest1);
        //            //    headertable.AddCell(headernest2);
        //            //    headertable.AddCell(headernest3);
        //            //    pdfDoc.Add(headertable);






        //            //    PdfPTable tablenew = new PdfPTable(3);
        //            //    PdfPCell cellnew = new PdfPCell(new Phrase("Header spanning 3 columns"));
        //            //    cellnew.Colspan = 3;
        //            //    cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //            //    tablenew.AddCell(cellnew);
        //            //    tablenew.AddCell("Col 1 Row 1");
        //            //    tablenew.AddCell("Col 2 Row 1");
        //            //    tablenew.AddCell("Col 3 Row 1");
        //            //    tablenew.AddCell("Col 1 Row 2");
        //            //    tablenew.AddCell("Col 2 Row 2");
        //            //    tablenew.AddCell("Col 3 Row 2");
        //            //    // headertable.AddCell(tablenew);


        //            //    PdfPCell nesthousingn = new PdfPCell(tablenew);
        //            //    nesthousingn.Border = Rectangle.NO_BORDER;
        //            //    nesthousingn.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //            //    nesthousingn.BorderWidthBottom = 1f;
        //            //    // nesthousing.Rowspan = 5;
        //            //    nesthousingn.PaddingBottom = 10f;
        //            //    headertable.AddCell(nesthousingn);
        //            //    // pdfDoc.Add(tablenew);
        //            //}


        //            // invoice rperte

        //            PdfPTable Invoicetable2 = new PdfPTable(1);
        //            Invoicetable2.HorizontalAlignment = 0;
        //            Invoicetable2.WidthPercentage = 100;
        //            Invoicetable2.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
        //            Invoicetable2.DefaultCell.Border = Rectangle.NO_BORDER;

        //            {

        //                PdfPTable mainN = new PdfPTable(1);
        //                // tablenew.HorizontalAlignment = 1;
        //                //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
        //                PdfPCell cellN = new PdfPCell(new Phrase("Trimurti Diagnostics \n\n Home/Clinic Collection No : 7777 8866 04/05, 9766 6600 83 | 0253 2376062 "
        //            + "\n\n28, Purab Paschim Plaza, Divya Adlabs Building, Trimurti Chowk, CIDCO, Nashik - 422008", HeaderFont)); ;
        //                cellN.PaddingTop = 15f;
        //                cellN.PaddingBottom = 15f;
        //                cellN.VerticalAlignment = 1;
        //                //cellnew.Colspan = 2;
        //                cellN.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //                mainN.AddCell(cellN);
        //                PdfPCell nesthousingnn = new PdfPCell(mainN);

        //                PdfPTable tablenew = new PdfPTable(1);
        //                // tablenew.HorizontalAlignment = 1;
        //                //PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER"),EmailFont);
        //                PdfPCell cellnew = new PdfPCell(new Phrase("PURCHASE ORDER", EmailFont));
        //                cellnew.PaddingTop = 15f;
        //                cellnew.PaddingBottom = 15f;
        //                cellnew.VerticalAlignment = 1;
        //                //cellnew.Colspan = 2;
        //                cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //                tablenew.AddCell(cellnew);
        //                PdfPCell nesthousingn = new PdfPCell(tablenew);





        //                PdfPTable tablenew1 = new PdfPTable(2);

        //                // tablenew1.DefaultCell.FixedHeight = 100f;
        //                PdfPCell cellnew4 = new PdfPCell(new Phrase("PO No :    " + ds.Tables[0].Rows[0]["PONo"].ToString(), EmailFont));
        //                cellnew4.FixedHeight = 30f;
        //                cellnew4.HorizontalAlignment = 1;
        //                cellnew4.PaddingTop = 15f;
        //                cellnew4.PaddingBottom = 15f;
        //                PdfPCell cellnew5 = new PdfPCell(new Phrase("PO Date :  " + ds.Tables[0].Rows[0]["OrderDate1"].ToString(), EmailFont));
        //                cellnew5.HorizontalAlignment = 1;
        //                cellnew5.PaddingTop = 15f;
        //                cellnew5.PaddingBottom = 15f;

        //                tablenew1.AddCell(cellnew4);

        //                tablenew1.AddCell(cellnew5);



        //                PdfPTable tablevendor = new PdfPTable(2);

        //                PdfPCell suppliername = new PdfPCell(new Phrase("VENDOR NAME :    " + ds.Tables[0].Rows[0]["VendorName"].ToString(), EmailFont));
        //                suppliername.MinimumHeight = 80f;
        //                suppliername.PaddingTop = 10f;
        //                PdfPCell supplierdetails = new PdfPCell(new Phrase("VENDOR DETAILS:  \n\n\t Contact Person : " + ds.Tables[0].Rows[0]["VendorContactPerson"].ToString() + "\n\n\t Mobile No : " + ds.Tables[0].Rows[0]["VendorPhone"].ToString() + "\n\n\t Email : " + ds.Tables[0].Rows[0]["VendorEmailId"].ToString(), EmailFont));
        //                supplierdetails.PaddingTop = 10f;
        //                // PdfPCell termsnconditions = new PdfPCell(new Phrase("Terms & Conditions:    ", EmailFont));

        //                tablevendor.AddCell(suppliername);
        //                tablevendor.AddCell(supplierdetails);
        //                //tablevendor.AddCell(termsnconditions);



        //                PdfPCell nesthousingn1 = new PdfPCell(tablenew1);
        //                PdfPCell nesthousingn2 = new PdfPCell(tablevendor);
        //                // nesthousingn2.Height = 10f;
        //                //PdfPCell nesthousingn3 = new PdfPCell(tablenew3);


        //                nesthousingn.Border = Rectangle.NO_BORDER;

        //                //nesthousingn.PaddingBottom = 10f;
        //                Invoicetable2.AddCell(nesthousingnn);
        //                Invoicetable2.AddCell(nesthousingn);
        //                Invoicetable2.AddCell(nesthousingn1);
        //                Invoicetable2.AddCell(nesthousingn2);
        //                //Invoicetable2.AddCell(nesthousingn3);



        //            }
        //            //invoice repeat        

        //            // Invoicetable2.AddCell(headertable);
        //            Invoicetable2.SpacingBefore = 3f;
        //            //  Invoicetable. = 10f;

        //            // pdfDoc.Add(Invoicetable);




        //            #endregion

        //            #region Items Table
        //            //Create body table
        //            PdfPTable itemTable = new PdfPTable(4);

        //            itemTable.HorizontalAlignment = 0;
        //            itemTable.WidthPercentage = 100;
        //            // itemTable.SetWidths(new float[] { });  // then set the column's __relative__ widths
        //            //itemTable.SetWidths(new float[] { 4, 30, 6, 6, 6 });
        //            itemTable.SpacingAfter = 10;

        //            //itemTable.DefaultCell.Border = Rectangle.BOX;




        //            PdfPCell cell1 = new PdfPCell(new Phrase("PRODUCT", boldTableFont));
        //            //cell1.BackgroundColor = TabelHeaderBackGroundColor;
        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
        //            itemTable.AddCell(cell1);

        //            PdfPCell cell2 = new PdfPCell(new Phrase("BRAND", boldTableFont));
        //            //cell2.BackgroundColor = TabelHeaderBackGroundColor;
        //            cell2.HorizontalAlignment = 1;
        //            itemTable.AddCell(cell2);



        //            PdfPCell cell3 = new PdfPCell(new Phrase("SIZE", boldTableFont));
        //            //cell4.BackgroundColor = TabelHeaderBackGroundColor;
        //            cell3.HorizontalAlignment = 1;
        //            itemTable.AddCell(cell3);


        //            PdfPCell cell4 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
        //            //cell5.BackgroundColor = TabelHeaderBackGroundColor;
        //            cell4.HorizontalAlignment = 1;
        //            itemTable.AddCell(cell4);

        //            /*  PdfPCell cell5 = new PdfPCell(new Phrase("DEPT", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell5.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell5);

        //              PdfPCell cell6 = new PdfPCell(new Phrase("UOM", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell6.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell6);

        //              PdfPCell cell7 = new PdfPCell(new Phrase("QTY (TON)", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell7.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell7);

        //              PdfPCell cell8 = new PdfPCell(new Phrase("QTY (KGS)", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell8.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell8);

        //              PdfPCell cell9 = new PdfPCell(new Phrase("PACKING", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell9.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell9);
        //              PdfPCell cell10 = new PdfPCell(new Phrase("RATE (TON)", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell10.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell10);
        //              PdfPCell cell11 = new PdfPCell(new Phrase("RATE (KGS)", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell11.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell11);
        //              PdfPCell cell12 = new PdfPCell(new Phrase("GROSS VALUE", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell12.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell12);

        //              PdfPCell cell13 = new PdfPCell(new Phrase("SGST @ 2.50", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell13.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell13);
        //              PdfPCell cell14 = new PdfPCell(new Phrase("CGST @ 2.50", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell14.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell14);
        //              PdfPCell cell15 = new PdfPCell(new Phrase("ADVANCE", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell15.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell15);
        //              PdfPCell cell16 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
        //              //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //              cell16.HorizontalAlignment = 1;
        //              itemTable.AddCell(cell16);

        //              */
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {

        //                PdfPCell cell1i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["ProdName"].ToString(), bodyFont));
        //                //cell1.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell1i.HorizontalAlignment = Element.ALIGN_CENTER;
        //                itemTable.AddCell(cell1i);

        //                PdfPCell cell2i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Brand"].ToString(), bodyFont));
        //                //cell2.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell2i.HorizontalAlignment = 1;
        //                itemTable.AddCell(cell2i);

        //                /*
        //                PdfPCell cell3 = new PdfPCell(new Phrase("Discount", boldTableFont));
        //                cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell3.HorizontalAlignment = 1;
        //                itemTable.AddCell(cell3);
        //                 */

        //                PdfPCell cell4i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Size"].ToString(), bodyFont));
        //                //cell4.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell4i.HorizontalAlignment = 1;
        //                itemTable.AddCell(cell4i);


        //                PdfPCell cell5i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Quantity1"].ToString(), bodyFont));
        //                //cell5.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell5i.HorizontalAlignment = 1;
        //                itemTable.AddCell(cell5i);

        //                //    PdfPCell cell3i = new PdfPCell(new Phrase("" + dtOrderProducts.Rows[i]["Amount"], bodyFont));
        //                //    //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                //    cell3i.HorizontalAlignment = 1;
        //                //    itemTable.AddCell(cell3i);

        //            }

        //            /*
        //             * 
        //             * 
        //             * PdfPTable totalTable = new PdfPTable(11);

        //                totalTable.HorizontalAlignment = 0;
        //                totalTable.WidthPercentage = 100;
        //                totalTable.SetWidths(new float[] { 46, 6, 6, 10, 6, 6, 8, 7, 7, 10, 7 });  // then set the column's __relative__ widths
        //                //totalTable.SetWidths(new float[] { 4, 30, 6, 6, 6 });
        //                totalTable.SpacingAfter = 10;

        //                //totalTable.DefaultCell.Border = Rectangle.BOX;




        //                PdfPCell cell17 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
        //                //cell1.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell17.HorizontalAlignment = Element.ALIGN_CENTER;
        //                totalTable.AddCell(cell17);

        //                PdfPCell cell18 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell2.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell18.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell18);

                
        //              //  PdfPCell cell3 = new PdfPCell(new Phrase("Discount", boldTableFont));
        //               // cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                //cell3.HorizontalAlignment = 1;
        //                //totalTable.AddCell(cell3);
                 

        //                PdfPCell cell19 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell4.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell19.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell19);


        //                PdfPCell cell20 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell5.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell20.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell20);

        //                PdfPCell cell21 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell21.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell21);

        //                PdfPCell cell22 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell22.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell22);

        //                PdfPCell cel23 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cel23.HorizontalAlignment = 1;
        //                totalTable.AddCell(cel23);

        //                PdfPCell cell24 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell24.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell24);

        //                PdfPCell cell25 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell25.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell25);
        //                PdfPCell cell26 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell26.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell26);
        //                PdfPCell cell27 = new PdfPCell(new Phrase("", boldTableFont));
        //                //cell3.BackgroundColor = TabelHeaderBackGroundColor;
        //                cell27.HorizontalAlignment = 1;
        //                totalTable.AddCell(cell27);

        //                */


        //            PdfPCell nesthousingn3 = new PdfPCell(itemTable);
        //            //  PdfPCell nesthousingn4 = new PdfPCell(totalTable);


        //            //nesthousingn.Border = Rectangle.NO_BORDER;

        //            //nesthousingn.PaddingBottom = 10f;
        //            Invoicetable2.AddCell(nesthousingn3);
        //            // Invoicetable2.AddCell(nesthousingn4);
        //            // pdfDoc.Add(Invoicetable2);
        //            #endregion


        //            /*     PdfPTable amtTable = new PdfPTable(1);

        //        amtTable.HorizontalAlignment = 0;
        //        amtTable.WidthPercentage = 100;
        //        // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
        //        //amtTable.SetWidths(new float[] { 4, 30});
        //        // amtTable.SpacingAfter = 10;

        //        //amtTable.DefaultCell.Border = Rectangle.BOX;
        //        amtTable.DefaultCell.Border = 0;


        //        PdfPCell cell37 = new PdfPCell(new Phrase("AMOUNT CHARGEABLE (IN WORDS)  \n\n", EmailFont));

        //        cell37.Border = 0;
        //        cell37.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right
        //        amtTable.AddCell(cell37);


        //        PdfPCell nesthousingn45 = new PdfPCell(amtTable);
        //        Invoicetable2.AddCell(nesthousingn45);


        //        PdfPTable noTable = new PdfPTable(1);

        //        noTable.HorizontalAlignment = 0;
        //        noTable.WidthPercentage = 100;
        //        // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
        //        //amtTable.SetWidths(new float[] { 4, 30});
        //        // amtTable.SpacingAfter = 10;

        //        noTable.DefaultCell.Border = 0;



        //        PdfPCell cell38 = new PdfPCell(new Phrase("COMPANY GST TIN NO : " + "" + "\nCOMPANY PAN NO : " + "", EmailFont));

        //        cell38.Border = 0;
        //        cell38.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right
        //        noTable.AddCell(cell38);


        //        PdfPCell nesthousingn46 = new PdfPCell(noTable);
        //        Invoicetable2.AddCell(nesthousingn46);



        //        PdfPTable noteTable = new PdfPTable(1);
        //        PdfPCell cellnote = new PdfPCell(new Phrase("OTHER TERMS & CONDITIONS :\n1. The contract is subject to our (Buyer's) General Terms & conditions for"
        //            + "\n a) Purchase of Poultry Feed Raw Materials, b) Purchase of goods and services under works contract circulated separately."
        //            + "\n2. The duplicate copy of this contract duly signed should be returned as a token of acceptance of our terms and conditions already mentioned."
        //            + "\n3. Original Invoices should accompany along with all the supplies. Bill accounting will be done only on receipt of Original Invoices.", bodyFont));

        //        //cellnote.BackgroundColor = new Color(System.Drawing.Color.Gray);
        //        cellnote.Border = 0;
        //        cellnote.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right
        //        noteTable.AddCell(cellnote);

        //        PdfPCell nesthousingn47 = new PdfPCell(noteTable);
        //        Invoicetable2.AddCell(nesthousingn47);
        //        */


        //            PdfPTable noTable1 = new PdfPTable(2);

        //            noTable1.HorizontalAlignment = 0;
        //            noTable1.WidthPercentage = 100;
        //            // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
        //            //amtTable.SetWidths(new float[] { 4, 30});
        //            // amtTable.SpacingAfter = 10;

        //            noTable1.DefaultCell.Border = 0;


        //            PdfPCell cellnote1 = new PdfPCell(new Phrase("PREPARED BY\nNAME : " + "\nSIGNATURE : " + "\nDATE & TIME : ", EmailFont));
        //            cellnote1.Border = 0;
        //            cellnote1.HorizontalAlignment = 0;//0=Left, 1=Centre, 2=Right


        //            noTable1.AddCell(cellnote1);




        //            PdfPCell cellnote3 = new PdfPCell(new Phrase("FOR TRIMURTI DIAGNOSTICS\n\n\n\nAUTHORIZED SIGNATORY", EmailFont));
        //            cellnote3.Border = 0;
        //            cellnote3.HorizontalAlignment = 2;//0=Left, 1=Centre, 2=Right


        //            noTable1.AddCell(cellnote3);





        //            PdfPCell nesthousingn5 = new PdfPCell(noTable1);
        //            Invoicetable2.AddCell(nesthousingn5);



        //            pdfDoc.Add(Invoicetable2);
        //            pdfDoc.Close();

        //            DownloadPDF(PDFData, po);
        //            // bool res = SendMail("Nikhil", "nikhilkd77@gmail.com", PDFData);
        //            // bool res = SendMail("Nikhil", "umya08@gmail.com", PDFData);
        //            //if (res)
        //            //{
        //            //    //string output = DataTableToJSONWithJavaScriptSerializer(dtCountry);
        //            //    finalResult = "{\"success\" : 1, \"message\" : \" Mail Sent \", \"data\" :  }";
        //            //}
        //            //else
        //            //{
        //            //    finalResult = "{\"success\" : 0, \"message\" : \"Failed!!! \", \"data\" : \"\"}";
        //            //}


        //            Context.Response.Clear();
        //            Context.Response.ContentType = "application/json";
        //            Context.Response.Flush();
        //            Context.Response.Write(finalResult);
        //            Context.Response.End();





        //        }
        //    }
        //    catch { }
        //    finally { con.Close(); }
        //    {



        //    }


        //}

        #endregion

        #region--Download PDF
        //protected void DownloadPDF(System.IO.MemoryStream PDFData, String po)
        //{
        //    // Clear response content & headers
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ClearContent();
        //    HttpContext.Current.Response.ClearHeaders();
        //    HttpContext.Current.Response.ContentType = "application/pdf";
        //    HttpContext.Current.Response.Charset = string.Empty;
        //    HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Purchase Order-{0}.pdf", po));
        //    HttpContext.Current.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);

        //    //  HttpContext.Current.Response.OutputStream.
        //    HttpContext.Current.Response.OutputStream.Flush();
        //    HttpContext.Current.Response.OutputStream.Close();
        //    HttpContext.Current.Response.End();

        //}
        #endregion
    }


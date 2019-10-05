using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Data;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class managepurchaseorder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    DataTable dtOrders = new DataTable();
    DataTable dtPurchaseOrder = new DataTable("PurchaseOrder");


    protected void Page_Load(object sender, EventArgs e)
    {
        Button btnDownload = new Button();
        if (!Page.IsPostBack)
        {
            //foreach (RepeaterItem item in repCategory.Items)
            //{
            //    btnDownload = (Button)item.FindControl("btnDownload");
            //}
           
            //System.Web.UI.ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDownload);
            BindOrders();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Purchase Orders";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Purchase Order Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Purchase Order Raised!!!";
        }
        else if (Request.QueryString["mode"] == "s")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Stock Updated!!!";
        }

    }

    private void BindOrders()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);




        try
        {

            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "PurchaseOrderHeader_SelectAll";
            // cmd.Parameters.AddWithValue("@ProdBrand", ProdBrand);
            //cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtOrders);
        }
        catch { }
        finally { con.Close(); }


        if (dtOrders != null)
        {
            if (dtOrders.Rows.Count > 0)
            {

                ViewState["PurchaseOrder"] = dtOrders;
                repOrders.DataSource = dtOrders;
                repOrders.DataBind();
            }
            else
            {
                repOrders.DataSource = null;
                repOrders.DataBind();
            }
        }
        else
        {
            repOrders.DataSource = null;
            repOrders.DataBind();
        }
        foreach (RepeaterItem item in repOrders.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                //var checkBox = (CheckBox)item.FindControl("ckbActive");
                Button grn = (Button)item.FindControl("btnGRN");
                Button view = (Button)item.FindControl("btnView");
                Label status = (Label)item.FindControl("lblStatus");
                if (status.Text == "True")
                {
                    view.Text = "CLOSED";
                    view.CssClass = "btn btn-sm btn-danger";
                    //view.Enabled = false;

                    grn.Visible = false;
                }
                //Do something with your checkbox...

            }
        }
    }
    protected void btnpo_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditpo.aspx"));

    }


    #region PDF Download 
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        
        string finalResult = string.Empty;

        

        Button button = (sender as Button);
        //Get the command argument
        Int64 PurchaseOrderId = Convert.ToInt64(button.CommandArgument.ToString());
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
        //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Context.Server.MapPath("~/uploads/PurchaseOrders/") + po + ".pdf", FileMode.Create));
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

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
        //+ "\n\n28, Purab Paschim Plaza, Divya Adlabs Building, Trimurti Chowk, CIDCO, Nashik - 422008", HeaderFont)); ;
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
        PdfPCell cell2 = new PdfPCell(new Phrase("BRAND", boldTableFont));
        //cell2.BackgroundColor = TabelHeaderBackGroundColor;
        cell2.HorizontalAlignment = 1;
        itemTable.AddCell(cell2);



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


            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["imagename"].ToString()))
            {
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(Server.MapPath("uploads/product/" + ds.Tables[0].Rows[i]["imagename"].ToString()));
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

            /*
            PdfPCell cell2i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Brand"].ToString(), bodyFont));
            //cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2i.HorizontalAlignment = 1;
            itemTable.AddCell(cell2i);

                

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





        DownloadPDF(PDFData, po);




        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(finalResult);
        Context.Response.End();



    }
    
#endregion




    protected void btnGRN_Click(object sender, EventArgs e)
    {
        Button button = (sender as Button);
        //Get the command argument
        string ca = button.CommandArgument;
        string commandArgument = button.CommandArgument;
        Session["PurchaseOrderId"] = ca;
        Response.Redirect(Page.ResolveUrl("~/grn.aspx"));

    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }


    #region Purchase Order
    public Boolean PDFUpload(Int64 PurchaseOrderId)
    {
        string finalResult = string.Empty;
        bool flag = true;

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
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Context.Server.MapPath("~/uploads/PurchaseOrders/") + po + ".pdf", FileMode.Create));
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

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
            PdfPTable itemTable = new PdfPTable(2);

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

            /*
            PdfPCell cell2 = new PdfPCell(new Phrase("BRAND", boldTableFont));
            //cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);



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

                /*
                PdfPCell cell2i = new PdfPCell(new Phrase(ds.Tables[0].Rows[i]["Brand"].ToString(), bodyFont));
                //cell2.BackgroundColor = TabelHeaderBackGroundColor;
                cell2i.HorizontalAlignment = 1;
                itemTable.AddCell(cell2i);

                

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





            DownloadPDF(PDFData, po);




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(finalResult);
            Context.Response.End();






            //}
        }
        catch { }
        finally { con.Close(); }

        return flag;


    }

    #endregion


    #region--Download PDF
    protected void DownloadPDF(System.IO.MemoryStream PDFData, String po)
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
    #endregion



}
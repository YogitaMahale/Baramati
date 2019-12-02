using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Net;

public partial class GSTInvoice : System.Web.UI.Page
{
    common ocommon = new common();
    string orderid = "";
    double totalqty = 0, totaltaxable = 0, total = 0, totalcgst = 0, totalsgst = 0, totalIGST = 0;
    DataSet dt;
    SqlDataAdapter da;
    SqlCommand cmd;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //  BindOrderDetails(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)));
            orderid = Request.QueryString["OrderId"].ToString();

            DownloadPDF(GeneratePDF());
        }
    }


    #region pdfgeenration

    //    public MemoryStream GeneratePDF()
    //    {

    //        insert();

    //      //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //     dt = new DataSet();

    //        cmd = new SqlCommand();
    //        cmd.CommandText = "order_invoice";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@oid",orderid);
    //        cmd.Connection = con;
    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }

    //        da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        //binddetails();
    //       /* if (dt.Tables[4].Rows.Count==0)
    //        {
    //            insert();

    //          dt = new DataSet();

    //          cmd= new SqlCommand();
    //            cmd.CommandText = "order_invoice";
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@oid", orderid);
    //            cmd.Connection = con;
    //            con.Open();
    //            da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        */
    //        Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
    //        MemoryStream PDFData = new MemoryStream();
    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

    //        var titleFont = FontFactory.GetFont("Arial",6, Font.NORMAL);
    //        var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL,Color.BLUE);
    //        //var boldTableFont = FontFactory.GetFont("Arial", 6, Font.BOLD);//8
    //        var boldTableFont = FontFactory.GetFont("Arial",6 ,Font.BOLD);//8
    //        var boldTableFont1 = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
    //        var bodyFont = FontFactory.GetFont("Arial", 7, Font.NORMAL);//8
    //        var EmailFont = FontFactory.GetFont("Arial", 7, Font.BOLD, Color.BLACK);//8
    //        var footerfont = FontFactory.GetFont("Arial",6, Font.NORMAL, Color.BLACK);//8
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
    //        headertable.SetWidths(new float[] { 100f, 320f, 120f });  // then set the column's __relative__ widths
    //        headertable.DefaultCell.Border = Rectangle.NO_BORDER;
    //        headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

    //       iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/500.png"));
    //       // //logo.ScaleToFit(100,80);

    //        logo.ScaleToFit(70,120);
    //        {
    //            PdfPCell pdfCelllogo = new PdfPCell(logo);
    //            pdfCelllogo.Border = Rectangle.NO_BORDER;
    //            pdfCelllogo.BorderColorBottom = new Color(System.Drawing.Color.Black);
    //            pdfCelllogo.BorderWidthBottom = 1f;
    //            pdfCelllogo.Top = 0;
    //            headertable.AddCell(pdfCelllogo);
    //        }

    //        {
    //            PdfPCell middlecell = new PdfPCell();
    //            middlecell.Border = Rectangle.NO_BORDER;
    //            middlecell.BorderColorBottom = new Color(System.Drawing.Color.Black);
    //            middlecell.BorderWidthBottom = 1f;
    //            headertable.AddCell(middlecell);
    //        }

    //        {
    //            PdfPTable nested = new PdfPTable(1);
    //            nested.DefaultCell.Border = Rectangle.NO_BORDER;



    //            PdfPCell nextPostCell1 = new PdfPCell(new Phrase("+91-8412887171", boldTableFont1));
    //            nextPostCell1.Border = Rectangle.NO_BORDER;
    //            nested.AddCell(nextPostCell1);
    //            PdfPCell nextPostCell2 = new PdfPCell(new Phrase("anant@adhikarindustries.com," + "\n" + "info@adhikarindustries.com", boldTableFont1));
    //            nextPostCell2.Border = Rectangle.NO_BORDER;
    //            nested.AddCell(nextPostCell2);
    //            PdfPCell nextPostCell3 = new PdfPCell(new Phrase("www.adhikarindustries.com", boldTableFont1));
    //            nextPostCell3.Border = Rectangle.NO_BORDER;
    //            nested.AddCell(nextPostCell3);
    //            PdfPCell nextPostCell4 = new PdfPCell(new Phrase("The Leading Manufacturing Company For exercise Notebook & All kind of stationary products", titleFont));
    //            nextPostCell4.Border = Rectangle.NO_BORDER;
    //            nested.AddCell(nextPostCell4);


    //            //PdfPCell nextPostCell1 = new PdfPCell(new Phrase("+91-8412887171", boldTableFont));
    //            //nextPostCell1.Border = Rectangle.NO_BORDER;
    //            //nested.AddCell(nextPostCell1);
    //            //PdfPCell nextPostCell2 = new PdfPCell(new Phrase("anant@adhikarindustries.com,"+"\n"+"info@adhikarindustries.com", boldTableFont));
    //            //nextPostCell2.Border = Rectangle.NO_BORDER;
    //            //nested.AddCell(nextPostCell2);
    //            //PdfPCell nextPostCell3 = new PdfPCell(new Phrase("www.adhikarindustries.com", boldTableFont));
    //            //nextPostCell3.Border = Rectangle.NO_BORDER;
    //            //nested.AddCell(nextPostCell3);
    //            //PdfPCell nextPostCell4 = new PdfPCell(new Phrase("The Leading Manufacturing Company For "+"\n"+"exercise Notebook & All kind of stationary products", titleFont));
    //            //nextPostCell4.Border = Rectangle.NO_BORDER;
    //            //nested.AddCell(nextPostCell4);
    //            nested.AddCell("");


    //            PdfPCell nesthousing = new PdfPCell(nested);
    //            nesthousing.Border = Rectangle.NO_BORDER;
    //            nesthousing.BorderColorBottom = new Color(System.Drawing.Color.Black);
    //            nesthousing.BorderWidthBottom = 1f;
    //           // nesthousing.Rowspan = 5;
    //            nesthousing.PaddingBottom = 10f;
    //            headertable.AddCell(nesthousing);
    //            PdfPTable tablenew = new PdfPTable(3);
    //            PdfPCell cellnew = new PdfPCell(new Phrase("Header spanning 3 columns"));
    //            cellnew.Colspan = 3;
    //            cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
    //            tablenew.AddCell(cellnew);
    //            tablenew.AddCell("Col 1 Row 1");
    //            tablenew.AddCell("Col 2 Row 1");
    //            tablenew.AddCell("Col 3 Row 1");
    //            tablenew.AddCell("Col 1 Row 2");
    //            tablenew.AddCell("Col 2 Row 2");
    //            tablenew.AddCell("Col 3 Row 2");
    //           // headertable.AddCell(tablenew);


    //            PdfPCell nesthousingn = new PdfPCell(tablenew);
    //            nesthousingn.Border = Rectangle.NO_BORDER;
    //            nesthousingn.BorderColorBottom = new Color(System.Drawing.Color.Black);
    //            nesthousingn.BorderWidthBottom = 1f;
    //            // nesthousing.Rowspan = 5;
    //            nesthousingn.PaddingBottom = 10f;
    //            headertable.AddCell(nesthousingn);
    //            // pdfDoc.Add(tablenew);
    //        }


    //        // invoice rperte

    //        PdfPTable Invoicetable2 = new PdfPTable(1);
    //        Invoicetable2.HorizontalAlignment = 0;
    //        Invoicetable2.WidthPercentage = 100;
    //        Invoicetable2.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
    //        Invoicetable2.DefaultCell.Border = Rectangle.NO_BORDER;

    //        {


    //            PdfPTable tablenew = new PdfPTable(3);
    //            PdfPCell cellnew = new PdfPCell(new Phrase("TAX INVOICE"));
    //            cellnew.Colspan = 2;
    //            cellnew.BackgroundColor = new Color(System.Drawing.Color.Khaki);
    //            PdfPCell cellnew1 = new PdfPCell(new Phrase("Orignal For receipeint"+":"+"  "+"\n"+"Duplicate For Transportor :"+ "     "  +"\n"+"Triplicate for Comapny: "+"  ",EmailFont));
    //            cellnew1.BackgroundColor = new Color(System.Drawing.Color.Khaki);

    //            //************************
    //            string InvoiceName = string.Empty;
    //            string fff = dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            if (fff.Length == 1)
    //            {
    //                InvoiceName = "API/" + DateTime.Now.Year.ToString().Trim() + "-" + (DateTime.Now.Year + 1) + "/000" + dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            }
    //            else if(fff.Length == 2)
    //            {
    //                InvoiceName = "API/" + DateTime.Now.Year.ToString().Trim() + "-" + (DateTime.Now.Year + 1) + "/00" + dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            }
    //            else if (fff.Length == 3)
    //            {
    //                InvoiceName = "API/" + DateTime.Now.Year.ToString().Trim() + "-" + (DateTime.Now.Year + 1) + "/0" + dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            }
    //            else
    //            {
    //                InvoiceName = "API/" + DateTime.Now.Year.ToString().Trim() + "-" + (DateTime.Now.Year + 1) + "/000" + dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            }
    //            //************************
    //              //InvoiceName = "API/" + DateTime.Now.Year.ToString().Trim() + "-" + (DateTime.Now.Year + 1) + "/" + dt.Tables[0].Rows[0]["oid"].ToString().Trim();
    //            //PdfPCell cellnew2 = new PdfPCell(new Phrase("GSTIN: 27BOOPS0449LIZ9 "+"\n"+"Invoice No : "+dt.Tables[0].Rows[0]["oid"]+""+"\n"+"Invoice Date: "+dt.Tables[0].Rows[0]["orderdate"]+""+"\n"+"State:Maharashtra"+"                                                     "+"State Code : 27"+"\n"+"HSNCode:48202000",EmailFont));
    //            PdfPCell cellnew2 = new PdfPCell(new Phrase("GSTIN: 27BOOPS0449L1Z9 " + "\n" + "Invoice No : " + InvoiceName + "" + "\n" + "Invoice Date: " + dt.Tables[0].Rows[0]["orderdate"] + "" + "\n" + "State:Maharashtra" + "                                                     " + "State Code : 27" + "\n" + "HSNCode:48202000", EmailFont));
    //          //  PdfCell cellnew3 = new PdfCell(new Phrase());
    //            //if (dt.Tables[4].Rows.Count > 0)
    //           // {
    //                PdfPCell cellnew3 = new PdfPCell(new Phrase("Transporatation Mode:" + dt.Tables[4].Rows[0]["TransportatipnMode"] + "\n" + "Vehicle Number :" + dt.Tables[4].Rows[0]["VechileNo"] + "\n" + "Date Of Supply:" + dt.Tables[4].Rows[0]["DateOfSupply"] + "\n" + "Purchase order no & Date :" + dt.Tables[4].Rows[0]["PoNo&Date"] + "\n" + "Place of suppliy:" + dt.Tables[4].Rows[0]["PlaceOfSupply"] + "\n" + "Sale officer :" + "Nashik", EmailFont));
    //            //}
    //           // else
    //           // {
    //              //  PdfPCell cellnew3 = new PdfPCell(new Phrase("Transporatation Mode:" + " comapny vehicle " + "\n" + "Vehicle Number :" + "Mahindra maxiam MH 15 DK 3561 " + "\n" + "Date Of Supply:" + dt.Tables[0].Rows[0]["orderdate"] + "\n" + "Purchase order no :" + "     " + "   " + "Date :" + "     " + "\n" + "Place of suppliy:" + "Nashik" + "\n" + "Sale officer :" + "Nashik", EmailFont));
    //           // }


    //            PdfPCell cellnew4 = new PdfPCell(new Phrase("                              "+"Detailed Of Receiver/Billed To:"+"                              ",EmailFont));

    //            PdfPCell cellnew5 = new PdfPCell(new Phrase("                            " + "Detailed of Consignee/Shiped To: "+ "        ",EmailFont));
    //            cellnew.Colspan = 2;
    //            PdfPCell cellnew6 = new PdfPCell(new Phrase("Name:" + dt.Tables[2].Rows[0]["NAME"] + "" + "\n" + "Address:" + dt.Tables[2].Rows[0]["address"] + "\n" + "Properiter:" + "  " + "\n" + "GSTIN:" + dt.Tables[2].Rows[0]["gstno"] + "\n" + "State:" + dt.Tables[2].Rows[0]["statename"] + "                                                        " + "State Code:" + dt.Tables[2].Rows[0]["StateCode"], EmailFont));
    //           //PdfPCell cellnew6 = new PdfPCell(new Phrase("Name:"+dt.Tables[2].Rows[0]["NAME"]+""+"\n"+"Address:"+dt.Tables[2].Rows[0]["address"]+"\n"+"Properiter:"+"  "+"\n"+"GSTIN:"+dt.Tables[2].Rows[0]["gstno"]+"\n"+"State:"+"Maharashtra,India"+"                                                        "+"State Code:27",EmailFont));
    //            PdfPCell cellnew7 = new PdfPCell(new Phrase("Name:" + dt.Tables[4].Rows[0]["Consigneename"] + "" + "\n" + "Address:" + dt.Tables[4].Rows[0]["ConsigneAdd"] + "\n" + "Properiter:" + "  " + "\n" + "GSTIN:" + dt.Tables[4].Rows[0]["ConsigneeGSTIN"] + "\n" + "State:" + dt.Tables[4].Rows[0]["statename"] + "                               " + "State Code:" + dt.Tables[4].Rows[0]["StateCode"], EmailFont));

    //            cellnew.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
    //            tablenew.AddCell(cellnew);
    //            tablenew.AddCell(cellnew1);
    // cellnew2.Colspan = 2;
    //            tablenew.AddCell(cellnew2);

    //            //cellnew3.Colspan = 2;
    //            tablenew.AddCell(cellnew3);


    //            cellnew4.Colspan = 2;
    //            tablenew.AddCell(cellnew4);

    //            cellnew5.Colspan = 2;
    //            tablenew.AddCell(cellnew5);


    //            cellnew6.Colspan = 2;
    //            tablenew.AddCell(cellnew6);

    //            cellnew7.Colspan = 2;
    //            tablenew.AddCell(cellnew7);


    //            PdfPCell nesthousingn = new PdfPCell(tablenew);
    //            nesthousingn.Border = Rectangle.NO_BORDER;

    //            nesthousingn.PaddingBottom = 10f;
    //            Invoicetable2.AddCell(nesthousingn);



    //        }
    //        //invoice repeat        
    //        pdfDoc.Add(headertable);
    //        Invoicetable2.SpacingBefore =3f;
    //      //  Invoicetable. = 10f;

    //       // pdfDoc.Add(Invoicetable);

    //        pdfDoc.Add(Invoicetable2);


    //        #endregion

    //        #region Items Table
    //        //Create body table
    //        PdfPTable itemTable = new PdfPTable(14);

    //        itemTable.HorizontalAlignment = 0;
    //        itemTable.WidthPercentage = 100;
    //       // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
    //        itemTable.SetWidths(new float[] { 4, 30, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 4, 7 });
    //        itemTable.SpacingAfter = 10;

    //        itemTable.DefaultCell.Border = Rectangle.BOX;

    //        PdfPCell cell1 = new PdfPCell(new Phrase("SNo", boldTableFont));
    //        cell1.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell1);

    //        PdfPCell cell2 = new PdfPCell(new Phrase("Name of Product", boldTableFont));
    //        cell2.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell2.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell2);

    //        /*
    //        PdfPCell cell3 = new PdfPCell(new Phrase("Discount", boldTableFont));
    //        cell3.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell3.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell3);
    //         */

    //        PdfPCell cell4 = new PdfPCell(new Phrase("Total Qty", boldTableFont));
    //        cell4.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell4.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell4);


    //        PdfPCell cell5 = new PdfPCell(new Phrase("MRP/ \n Piece", boldTableFont));
    //        cell5.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell5.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell5);

    //        PdfPCell cell3 = new PdfPCell(new Phrase("Discount", boldTableFont));
    //        cell3.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell3.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell3);


    //        PdfPCell cell6 = new PdfPCell(new Phrase("Discount Amt/ \n Piece", boldTableFont));
    //        //PdfPCell cell6 = new PdfPCell(new Phrase("Discounted @ "+dt.Tables[8].Rows[0]["discounted"]+"", boldTableFont));
    //        cell6.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell6.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell6);


    //        PdfPCell cell7 = new PdfPCell(new Phrase("Taxable \n Amt", boldTableFont));
    //        cell7.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell7.HorizontalAlignment = 1;
    //        itemTable.AddCell(cell7);


    //        PdfPCell cell8 = new PdfPCell(new Phrase("CGST%", boldTableFont));
    //        cell8.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell8.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell8);

    //        PdfPCell cell9 = new PdfPCell(new Phrase("CGSTAmt", boldTableFont));
    //        cell9.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell9.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell9);

    //        PdfPCell cell10 = new PdfPCell(new Phrase("SGST %", boldTableFont));
    //        cell10.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell10.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell10);

    //        PdfPCell cell11 = new PdfPCell(new Phrase("SGST AMt", boldTableFont));
    //        cell11.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell11.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell11);

    //        PdfPCell cell12 = new PdfPCell(new Phrase("IGST%", boldTableFont));
    //        cell12.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell12.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell12);

    //        PdfPCell cell13 = new PdfPCell(new Phrase("IGSTAmt", boldTableFont));
    //        cell13.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell13.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell13);


    //        PdfPCell cell14 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
    //        cell14.BackgroundColor = TabelHeaderBackGroundColor;
    //        cell14.HorizontalAlignment = Element.ALIGN_CENTER;
    //        itemTable.AddCell(cell14);



    //double  Total_TotalQty=0.0;
    //double Total_Pricee=0.0;
    //double Total_Discount=0.0;
    //double Total_Discountamt=0.0;
    //double Total_TaxableAmt=0.0;
    //double Total_per_CGST=0.0;
    //double Total_Amt_CGST=0.0;
    //double Total_per_sGST=0.0;
    //double Total_Amt_sGST=0.0;

    //double Total_per_iGST=0.0;
    //double Total_Amt_iGST=0.0;
    //double Total_col_Total = 0.0;
    //        foreach (DataRow row in dt.Tables[1].Rows)
    //        {

    //            //name of product
    //            var _phrase1 = new Phrase();
    //            _phrase1.Add(new Chunk(row["SRNO"].ToString(), EmailFont));
    //            PdfPCell descCells = new PdfPCell(_phrase1);
    //            descCells.HorizontalAlignment = 0;
    //            descCells.PaddingLeft = 10f;
    //            descCells.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(descCells);
    //            /*
    //            //Sr no

    //            PdfPCell numberCell = new PdfPCell(new Phrase(row["SRNO"].ToString(), bodyFont));
    //            numberCell.HorizontalAlignment = 1;
    //            numberCell.PaddingLeft = 10f;
    //            numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(numberCell);
    //            */
    //            //name of product
    //            var _phrase = new Phrase();
    //            _phrase.Add(new Chunk(row["productname"].ToString(), EmailFont));
    //            PdfPCell descCell = new PdfPCell(_phrase);
    //            descCell.HorizontalAlignment = 0;
    //            descCell.PaddingLeft = 10f;
    //            descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(descCell);
    //           /*
    //            //no of boxes
    //           // PdfPCell qtyCell = new PdfPCell(new Phrase("", bodyFont));
    //            PdfPCell qtyCell = new PdfPCell(new Phrase((Convert.ToDouble(row["discount"].ToString())).ToString(), bodyFont));
    //            qtyCell.HorizontalAlignment = 1;
    //            qtyCell.PaddingLeft = 10f;
    //            qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(qtyCell);
    //           */
    //            //totalqty
    //            PdfPCell amountCell = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble (row["quantites"].ToString()),2).ToString(), bodyFont));
    //            amountCell.HorizontalAlignment = 1;
    //            amountCell.PaddingLeft = 10f;
    //            amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(amountCell);
    //            totalqty += Convert.ToDouble(row["quantites"].ToString());
    //            Total_TotalQty += Math.Round(Convert.ToDouble(row["quantites"].ToString()), 2);

    //            //MRP/perice
    //            PdfPCell totalamtCell = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["productprice"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell.HorizontalAlignment = 1;
    //            totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell);
    //            Total_Pricee += Math.Round(Convert.ToDouble(row["productprice"].ToString()), 2);

    //            //no of boxes
    //            // PdfPCell qtyCell = new PdfPCell(new Phrase("", bodyFont));
    //            PdfPCell qtyCell = new PdfPCell(new Phrase((Convert.ToDouble(row["discount"].ToString())).ToString(), bodyFont));
    //            qtyCell.HorizontalAlignment = 1;
    //            qtyCell.PaddingLeft = 10f;
    //            qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(qtyCell);
    //            Total_Discount += Convert.ToDouble(row["discount"].ToString());
    //            //discounted @ 50%
    //            PdfPCell totalamtCell1 = new PdfPCell(new Phrase((Convert.ToDouble(row["productafterdiscountprice"].ToString())).ToString(), bodyFont));
    //           // PdfPCell totalamtCell1 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["discount"].ToString())).ToString(), bodyFont));
    //            //PdfPCell totalamtCell1 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["discounted"].ToString())).ToString(), bodyFont));
    //            totalamtCell1.HorizontalAlignment = 1;
    //            totalamtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell1);
    //            Total_Discountamt += Convert.ToDouble(row["productafterdiscountprice"].ToString());

    //            //Taxable Amt
    //            PdfPCell totalamtCell3 = new PdfPCell(new Phrase( Math.Round( Convert.ToDouble( row["taxable"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell3.HorizontalAlignment = 1;
    //            totalamtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell3);
    //            totaltaxable += Convert.ToDouble(row["taxable"].ToString());
    //            Total_TaxableAmt += Math.Round(Convert.ToDouble(row["taxable"].ToString()), 2);

    //            //CGST per
    //            PdfPCell totalamtCell4 = new PdfPCell(new Phrase( Math.Round(Convert.ToDouble(row["gstper"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell4.HorizontalAlignment = 1;
    //            totalamtCell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell4);
    //            Total_per_CGST += Math.Round(Convert.ToDouble(row["gstper"].ToString()), 2);

    //            //CGST amt
    //            PdfPCell totalamtCell5 = new PdfPCell(new Phrase( Math.Round(Convert.ToDouble(row["CGST"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell5.HorizontalAlignment = 1;
    //            totalamtCell5.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell5);
    //            totalcgst += Convert.ToDouble(row["CGST"].ToString());
    //            Total_Amt_CGST += Math.Round(Convert.ToDouble(row["CGST"].ToString()), 2);

    //            //SGST per
    //            PdfPCell totalamtCell6 = new PdfPCell( new Phrase( Math.Round (Convert.ToDouble(row["gstper"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell6.HorizontalAlignment = 1;
    //            totalamtCell6.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell6);
    //            Total_per_sGST += Math.Round(Convert.ToDouble(row["gstper"].ToString()), 2);


    //            //SGST amt
    //            PdfPCell totalamtCell7 = new PdfPCell(new Phrase(Math.Round (Convert.ToDouble(row["SGST"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell7.HorizontalAlignment = 1;
    //            totalamtCell7.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell7);
    //            totalsgst += Convert.ToDouble(row["SGST"].ToString());
    //            Total_Amt_sGST += Math.Round(Convert.ToDouble(row["SGST"].ToString()), 2);

    //            //IGSTPer
    //            PdfPCell totalamtCell8 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["IGST"].ToString()), 2).ToString(), bodyFont));
    //            totalamtCell8.HorizontalAlignment = 1;
    //            totalamtCell8.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell8);
    //            Total_per_iGST += Math.Round(Convert.ToDouble(row["IGST"].ToString()), 2);

    //           // total += Convert.ToDouble(row["IGST"].ToString());



    //            //IGST Amt
    //            PdfPCell totalamtCell9 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["IGSTAmt"].ToString()), 2).ToString(), bodyFont));
    //            totalamtCell9.HorizontalAlignment = 1;
    //            totalamtCell9.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell9);
    //            totalIGST += Convert.ToDouble(row["IGSTAmt"].ToString());
    //            Total_Amt_iGST += Math.Round(Convert.ToDouble(row["IGSTAmt"].ToString()), 2);



    //            //Total Amt
    //            //double tot=Convert.ToDouble( row["taxable"].ToString())+Convert.ToDouble(row["CGST"].ToString())+Convert.ToDouble(row["SGST"].ToString())+Convert.ToDouble(row["IGSTAmt"].ToString());
    //            double tot = Convert.ToDouble(row["TotalGst"].ToString());
    //            PdfPCell totalamtCell10 = new PdfPCell(new Phrase((Convert.ToDouble(tot)).ToString(), bodyFont));
    //            //PdfPCell totalamtCell10 = new PdfPCell(new Phrase((Convert.ToDouble(tot)).ToString(), bodyFont));
    //            //PdfPCell totalamtCell10 = new PdfPCell(new Phrase(Math.Round( Convert.ToDouble(row["TotalGst"].ToString()),2).ToString(), bodyFont));
    //            totalamtCell10.HorizontalAlignment = 1;
    //            totalamtCell10.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
    //            itemTable.AddCell(totalamtCell10);
    //            total += Convert.ToDouble(row["TotalGst"].ToString());
    //            Total_col_Total += tot;

    //        }

    //        // Table footer
    //        //sr number
    //        PdfPCell totalAmtCell1 = new PdfPCell(new Phrase("  ",bodyFont));
    //        totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER  | Rectangle.BOTTOM_BORDER;        
    //        totalAmtCell1.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell1);

    //        PdfPCell totalAmtCell2 = new PdfPCell(new Phrase("Total",FontFactory.GetFont("arial",7,Font.BOLD)));
    //        totalAmtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell2.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell2);

    //        PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(Total_TotalQty.ToString(), bodyFont));
    //        totalAmtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell3.HorizontalAlignment = 1;
    //            itemTable.AddCell(totalAmtCell3);

    //            PdfPCell totalAmtCell4 = new PdfPCell(new Phrase(Total_Pricee.ToString(), bodyFont));
    //        //PdfPCell totalAmtCell4 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totalqty.ToString()),2).ToString() , bodyFont));
    //        totalAmtCell4.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell4.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell4);

    //        PdfPCell totalAmtCell5 = new PdfPCell(new Phrase(Total_Discount.ToString(), bodyFont));
    //        totalAmtCell5.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell5.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell5);

    //        PdfPCell totalAmtCell6 = new PdfPCell(new Phrase(Total_Discountamt.ToString(), bodyFont));
    //        totalAmtCell6.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell6.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell6);

    //        PdfPCell totalAmtCell7 = new PdfPCell(new Phrase(Total_TaxableAmt.ToString(), bodyFont));
    //        //PdfPCell totalAmtCell7 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totaltaxable.ToString()),2).ToString(), bodyFont));
    //        totalAmtCell7.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell7.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell7);



    //        PdfPCell totalAmtCell8 = new PdfPCell(new Phrase(Total_per_CGST.ToString(), bodyFont));
    //        totalAmtCell8.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell8.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell8);


    //        PdfPCell totalAmtCell9 = new PdfPCell(new Phrase(Total_Amt_CGST.ToString(), bodyFont));
    //        totalAmtCell9.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell9.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell9);


    //        PdfPCell totalAmtCell10 = new PdfPCell(new Phrase(Total_per_sGST.ToString(), bodyFont));
    //        totalAmtCell10.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell10.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell10);


    //        PdfPCell totalAmtCell11 = new PdfPCell(new Phrase(Total_Amt_sGST.ToString(), bodyFont));
    //        totalAmtCell11.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
    //        totalAmtCell11.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell11);

    //        PdfPCell totalAmtCell12 = new PdfPCell(new Phrase(Total_per_iGST.ToString(), bodyFont));
    //       // PdfPCell totalAmtCell12 = new PdfPCell(new Phrase("0.00", bodyFont));
    //        totalAmtCell12.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
    //        totalAmtCell12.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell12);

    //        PdfPCell totalAmtCell13 = new PdfPCell(new Phrase(Total_Amt_iGST.ToString(), bodyFont));
    //        //PdfPCell totalAmtCell13 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totalIGST.ToString()), 2).ToString(), bodyFont));
    //        totalAmtCell13.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
    //        totalAmtCell13.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell13);

    //        PdfPCell totalAmtCell14 = new PdfPCell(new Phrase(Total_col_Total.ToString(), bodyFont));
    //        //PdfPCell totalAmtCell14 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(total.ToString()), 2).ToString(), bodyFont));
    //        totalAmtCell14.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
    //        totalAmtCell14.HorizontalAlignment = 1;
    //        itemTable.AddCell(totalAmtCell14);


    //        PdfPTable Invoicetable3 = new PdfPTable(1);
    //        Invoicetable3.HorizontalAlignment = 0;
    //        Invoicetable3.WidthPercentage = 100;
    //        Invoicetable3.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
    //        Invoicetable3.DefaultCell.Border = Rectangle.NO_BORDER;
    //        Invoicetable3.SpacingBefore = 10f;
    //       // Invoicetable3.SpacingBefore = 270f;
    //        PdfPTable tablenewt = new PdfPTable(5);

    //        PdfPCell cellnewt = new PdfPCell(new Phrase("No Of Boxes:"+dt.Tables[4].Rows[0]["NoofBoxes"].ToString() +"\n"+"Total Invoice Amount In words :-\n  Rupees INR:"+ConvertToWords(Math.Round((Convert.ToDouble(totaltaxable.ToString()) + Convert.ToDouble(totalIGST.ToString())+Convert.ToDouble(totalsgst.ToString())+Convert.ToDouble(totalcgst.ToString())), 0).ToString()),FontFactory.GetFont("Mangal",8,Font.BOLD)));
    //        cellnewt.Colspan = 3;
    //        cellnewt.HorizontalAlignment = 1;
    //        PdfPCell cellnew1t = new PdfPCell(new Phrase("Total Amt Before Tax :" + "\n\n" + "Add CGST: " + " \n\n "+"Add SGST :"+" \n\n " + "Add IGST :" + "\n\n"+"Tax Amount GST :"+"\n\n"+"Total Amt after Tax :", EmailFont));
    //        cellnew1t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
    //        PdfPCell cellnew2t = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totaltaxable.ToString()), 2).ToString() + "\n\n" + Math.Round(Convert.ToDouble(totalcgst.ToString()), 2).ToString() + "\n\n"+ Math.Round(Convert.ToDouble(totalsgst.ToString()), 2).ToString() + "\n\n"+ Math.Round(Convert.ToDouble(totalIGST.ToString())) + "\n\n" +Math.Round(Convert.ToDouble(totalIGST.ToString())+Convert.ToDouble(totalcgst.ToString())+Convert.ToDouble(totalsgst.ToString())).ToString() +"\n\n"+Math.Round ( (Convert.ToDouble(totaltaxable.ToString())+ Convert.ToDouble(totalsgst.ToString())+Convert.ToDouble(totalcgst.ToString())+Convert.ToDouble(totalIGST.ToString())),0), EmailFont));
    //        cellnew2t.BackgroundColor = new Color(System.Drawing.Color.Khaki);

    //       // PdfPCell cellnew3t = new PdfPCell(new Phrase("Bank Details:- A/c. name:- Adhikar Paper Industries" + "\n" + "Bank Name:- 'UNION BANK OF INDIA'" + "\n" + "1.CC ACCOUNT NO:-'596805010000102'" + "\n" + "2: Currnet Account Numer: 5968050100000102 " + "\n" + "Jail Road Branch,Nashik" + "\n" + "IFSC Code :UBIN0559687", EmailFont));
    //        PdfPCell cellnew3t = new PdfPCell(new Phrase("Bank Details:- A/c. name:- Adhikar Paper Industries" + "\n" + "Bank Name:- 'UNION BANK OF INDIA'" + "\n" + "CC ACCOUNT NO:-'596805010000102'" +"\n"+"Current Account Number: 596801010050408"+ "\n" + "Jail Road Branch,Nashik" + "\n" + "IFSC Code :UBIN0559687", EmailFont));


    //        PdfPCell cellnew5t = new PdfPCell(new Phrase("   "));
    //        PdfPCell cellnew6t = new PdfPCell(new Phrase("  "));
    //        PdfPCell cellnew7t = new PdfPCell(new Phrase("Name:Terms And Condition" + "\n" + "I/We hereby that my /our registeration certificate under the maharashtra value added tax act 2002 is in force on the date on which safe of goods specified in the tax invoice is made by me/us that the tranaction of the sale covered by this Bill/cash memorandum has been effect by me and it shall be accounted for the turnover of sales while filling my return:" +"\n"+ "1)Subject to nashik jurisdiction" + "\n" + "2)Any Irregularities and issues in the bill to be inform by mail or written within 7 days only" +"\n"+ "3)Despatches from duty paid transportation godown idpack of model towards company in order booking form " + "\n" + "4)Transportation will be only from company to transporters depot at client city.Means no door delivery committed by company" + "  " + "\n" + "State:" + "Maharashtra,India" + " " + "Code:20", footerfont));

    //        Phrase ph1 = new Phrase();
    //        ph1.Add(new Chunk("(company seal)\n\n\n\n\n\n\n\n\n\n\n\n", FontFactory.GetFont("Arial", 8)));
    //        ph1.Add(new Chunk("(Receivers Seal and Sign)", FontFactory.GetFont("Mangala",8, Font.BOLD)));


    //        PdfPCell cellnew8t = new PdfPCell(ph1);

    //       PdfPCell cellnew9t = new PdfPCell(new Phrase("Certified that the particular given above are true and correct" + "\n" + "For," + "\n" + "Adhikar Paper Industries:" +"\n\n\n\n\n"+ "Authorised Signatory" + "\n\n\n" + "Adhikar Paper Industries" + "  " + "\n" + "Plot No 16/2 MIDc area,Malegaon Village ,Sinner,Tal-Sinner,Dist:Nashik" +"\n"+ "Registered office Address" + " " + "Adjikari House,Chetna Hospital Building,Tapovan  link road"+"\n"+"shankar garden lawn,Kathe lane,Dwarka-422011,Cell:-+ +91 8530744771", footerfont));

    //        cellnewt.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right

    //        tablenewt.AddCell(cellnewt);
    //        cellnew1t.HorizontalAlignment = 2;
    //        tablenewt.AddCell(cellnew1t);
    //        cellnew2t.Colspan = 2;
    //        tablenewt.AddCell(cellnew2t);

    //        cellnew3t.Colspan = 3;
    //        tablenewt.AddCell(cellnew3t);


    //     //   cellnew4t.Colspan = 1;
    //      //  tablenewt.AddCell(cellnew4t);



    //        tablenewt.AddCell(cellnew5t);


    //        tablenewt.AddCell(cellnew6t);

    //        cellnew7t.Colspan = 2;
    //        tablenewt.AddCell(cellnew7t);

    //        cellnew8t.Colspan = 2;
    //        cellnew8t.HorizontalAlignment = Element.ALIGN_CENTER;

    //        tablenewt.AddCell(cellnew8t);

    //        cellnew9t.Colspan = 2;
    //        cellnew9t.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cellnew9t.VerticalAlignment = Element.ALIGN_MIDDLE;
    //        tablenewt.AddCell(cellnew9t);


    //        PdfPCell nesthousingnt = new PdfPCell(tablenewt);
    //        nesthousingnt.Border = Rectangle.NO_BORDER;

    //        nesthousingnt.PaddingBottom = 10f;
    //        Invoicetable3.AddCell(nesthousingnt);


    //        pdfDoc.Add(itemTable);
    //            pdfDoc.Add(Invoicetable3);

    //        #endregion


    //        pdfDoc.Close();


    //        return PDFData;


    //    }


    public MemoryStream GeneratePDF()
    {



        //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        dt = new DataSet();

        cmd = new SqlCommand();
        cmd.CommandText = "order_invoice1";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@oid", orderid);
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
        MemoryStream PDFData = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

        var titleFont = FontFactory.GetFont("Arial", 6, Font.NORMAL);
        var titleFontBlue = FontFactory.GetFont("Arial", 18, Font.NORMAL, Color.BLACK );
        //var boldTableFont = FontFactory.GetFont("Arial", 6, Font.BOLD);//8
        var boldTableFont = FontFactory.GetFont("Arial", 6, Font.BOLD);//8
        var boldTableFont1 = FontFactory.GetFont("Arial", 8, Font.BOLD);//8
        var bodyFont = FontFactory.GetFont("Arial", 7, Font.NORMAL);//8
        var EmailFont = FontFactory.GetFont("Arial", 7, Font.BOLD, Color.BLACK);//8
        var footerfont = FontFactory.GetFont("Arial", 6, Font.NORMAL, Color.BLACK);//8
        var newfontt = FontFactory.GetFont("Arial", 6, Font.NORMAL, Color.BLACK);//8
        Color TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

        Rectangle pageSize = writer.PageSize;
        // Open the Document for writing
        pdfDoc.Open();
        //Add elements to the document here


        double f_totalAmtBeforeTax = 0;
        double f_addCgst = 0;
        double f_addSgst = 0;
        double f_addIgst = 0;
        double f_taxAmountGst = 0;
        double F_totalAmtAfterTax = 0;


        #region Top table
        // Create the header table 
        PdfPTable headertable = new PdfPTable(2);
        headertable.HorizontalAlignment = 0;
        headertable.WidthPercentage = 100;
        headertable.SetWidths(new float[] { 100f, 440f });  // then set the column's __relative__ widths
        headertable.DefaultCell.Border = Rectangle.NO_BORDER;
        headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/500.png"));
        // //logo.ScaleToFit(100,80);
        //logo.ScaleToFit(70, 120);
        logo.ScaleToFit(70, 120);
        {
             
            PdfPCell pdfCelllogo = new PdfPCell(logo);
            pdfCelllogo.Border = Rectangle.NO_BORDER;
            pdfCelllogo.BorderColorBottom = new Color(System.Drawing.Color.Black);
            pdfCelllogo.BorderWidthBottom = 1f;
            pdfCelllogo.Top = 0;
             

            headertable.AddCell(pdfCelllogo);
        }

        {
            PdfPTable nested = new PdfPTable(1);

            nested.DefaultCell.Border = Rectangle.NO_BORDER;



            PdfPCell nextPostCell1 = new PdfPCell(new Phrase(dt.Tables[3].Rows[0]["shopName"].ToString(), titleFontBlue));
            nextPostCell1.Border = Rectangle.NO_BORDER;
            nextPostCell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            nested.AddCell(nextPostCell1);
            PdfPCell nextPostCell2 = new PdfPCell(new Phrase(dt.Tables[3].Rows[0]["address"].ToString() + "\n" + dt.Tables[3].Rows[0]["phone"].ToString(), boldTableFont1));
            nextPostCell2.Border = Rectangle.NO_BORDER;
            nextPostCell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            nested.AddCell(nextPostCell2);
            PdfPCell nextPostCell3 = new PdfPCell(new Phrase(dt.Tables[3].Rows[0]["GSTIN"].ToString(), boldTableFont1));
            nextPostCell3.Border = Rectangle.NO_BORDER;
            nextPostCell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            nested.AddCell(nextPostCell3);
            //PdfPCell nextPostCell4 = new PdfPCell(new Phrase("The Leading Manufacturing Company For exercise Notebook & All kind of stationary products", titleFont));
            //nextPostCell4.Border = Rectangle.NO_BORDER;
            //nested.AddCell(nextPostCell4);


            nested.AddCell("");


            PdfPCell nesthousing = new PdfPCell(nested);


            nesthousing.Border = Rectangle.NO_BORDER;
            nesthousing.BorderColorBottom = new Color(System.Drawing.Color.Black);
            nesthousing.BorderWidthBottom = 1f;
            headertable.AddCell(nesthousing);
        }

      


        // invoice rperte

        PdfPTable Invoicetable2 = new PdfPTable(1);
        Invoicetable2.HorizontalAlignment = 0;
        Invoicetable2.WidthPercentage = 100;
        Invoicetable2.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
        Invoicetable2.DefaultCell.Border = Rectangle.NO_BORDER;
        //**********
        {


            PdfPTable tablenew = new PdfPTable(3);


            PdfPCell cellnew2 = new PdfPCell(new Phrase(" " + "\n" + dt.Tables[2].Rows[0]["NAME"].ToString() + "\n" + dt.Tables[2].Rows[0]["address"].ToString() + "\n" + "" + dt.Tables[2].Rows[0]["phone"].ToString() + "" + "\n" + "GST No." + dt.Tables[2].Rows[0]["gstno"].ToString() + "                                                     ", EmailFont));

            PdfPCell cellnew3 = new PdfPCell(new Phrase("Invoice No.:" + dt.Tables[0].Rows[0]["orderno"].ToString() + "\n" + "Date :" + dt.Tables[0].Rows[0]["orderdate"].ToString() + "\n" + "Pan No.:" + dt.Tables[2].Rows[0]["panno"].ToString() + "\n" + "Transport :" + dt.Tables[2].Rows[0]["Transaport"].ToString() + "\n", EmailFont));


            /* cellnew.HorizontalAlignment = 1; //*//*0=Left, 1=Centre, 2=Right*/





            cellnew2.Colspan = 2;
            tablenew.AddCell(cellnew2);

            //cellnew3.Colspan = 2;
            tablenew.AddCell(cellnew3);


            PdfPCell nesthousingn = new PdfPCell(tablenew);
            nesthousingn.Border = Rectangle.NO_BORDER;

            nesthousingn.PaddingBottom = 10f;
            Invoicetable2.AddCell(nesthousingn);



        }
        //******
        //invoice repeat        
        pdfDoc.Add(headertable);
        Invoicetable2.SpacingBefore = 3f;
        //  Invoicetable. = 10f;

        // pdfDoc.Add(Invoicetable);

        pdfDoc.Add(Invoicetable2);


        #endregion

        #region Items Table
        //Create body table
        PdfPTable itemTable = new PdfPTable(9);

        itemTable.HorizontalAlignment = 0;
        itemTable.WidthPercentage = 100;
        // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 });  // then set the column's __relative__ widths
        itemTable.SetWidths(new float[] { 4, 30, 6, 6, 6, 6, 6, 6, 6 });
        itemTable.SpacingAfter = 10;

        itemTable.DefaultCell.Border = Rectangle.BOX;

        PdfPCell cell1 = new PdfPCell(new Phrase("Sr No", boldTableFont));
        cell1.BackgroundColor = TabelHeaderBackGroundColor;
        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
        itemTable.AddCell(cell1);

        PdfPCell cell2 = new PdfPCell(new Phrase("Description", boldTableFont));
        cell2.BackgroundColor = TabelHeaderBackGroundColor;
        cell2.HorizontalAlignment = 1;
        itemTable.AddCell(cell2);



        PdfPCell cell4 = new PdfPCell(new Phrase("Brand", boldTableFont));
        cell4.BackgroundColor = TabelHeaderBackGroundColor;
        cell4.HorizontalAlignment = 1;
        itemTable.AddCell(cell4);


        PdfPCell cell5 = new PdfPCell(new Phrase("Size", boldTableFont));
        cell5.BackgroundColor = TabelHeaderBackGroundColor;
        cell5.HorizontalAlignment = 1;
        itemTable.AddCell(cell5);

        PdfPCell cell3 = new PdfPCell(new Phrase("HSN Code", boldTableFont));
        cell3.BackgroundColor = TabelHeaderBackGroundColor;
        cell3.HorizontalAlignment = 1;
        itemTable.AddCell(cell3);


        PdfPCell cell6 = new PdfPCell(new Phrase("Mrp", boldTableFont));
        //PdfPCell cell6 = new PdfPCell(new Phrase("Discounted @ "+dt.Tables[8].Rows[0]["discounted"]+"", boldTableFont));
        cell6.BackgroundColor = TabelHeaderBackGroundColor;
        cell6.HorizontalAlignment = 1;
        itemTable.AddCell(cell6);


        PdfPCell cell7 = new PdfPCell(new Phrase("Qty", boldTableFont));
        cell7.BackgroundColor = TabelHeaderBackGroundColor;
        cell7.HorizontalAlignment = 1;
        itemTable.AddCell(cell7);


        PdfPCell cell8 = new PdfPCell(new Phrase("Disc.", boldTableFont));
        cell8.BackgroundColor = TabelHeaderBackGroundColor;
        cell8.HorizontalAlignment = Element.ALIGN_CENTER;
        itemTable.AddCell(cell8);

        PdfPCell cell9 = new PdfPCell(new Phrase("Amount", boldTableFont));
        cell9.BackgroundColor = TabelHeaderBackGroundColor;
        cell9.HorizontalAlignment = Element.ALIGN_CENTER;
        itemTable.AddCell(cell9);



        decimal Total_TotalQty = 0;
        decimal Total_per_CGST = 0;
        decimal Total_per_sGST = 0;
        decimal Total_per_iGST = 0;


        foreach (DataRow row in dt.Tables[1].Rows)
        {

            //name of product
            var _phrase1 = new Phrase();
            _phrase1.Add(new Chunk(row["sr"].ToString(), EmailFont));
            PdfPCell descCells = new PdfPCell(_phrase1);
            descCells.HorizontalAlignment = 0;
            descCells.PaddingLeft = 10f;
            descCells.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(descCells);

            //name of product
            var _phrase = new Phrase();
            _phrase.Add(new Chunk(row["productname"].ToString(), EmailFont));
            PdfPCell descCell = new PdfPCell(_phrase);
            descCell.HorizontalAlignment = 0;
            descCell.PaddingLeft = 10f;
            descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(descCell);

            //totalqty
            PdfPCell amountCell = new PdfPCell(new Phrase(new Chunk(row["brandid"].ToString(), EmailFont)));
            amountCell.HorizontalAlignment = 1;
            amountCell.PaddingLeft = 10f;
            amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(amountCell);
            //totalqty += Convert.ToDouble(row["brandid"].ToString());
            //Total_TotalQty += Math.Round(Convert.ToDouble(row["quantites"].ToString()), 2);

            //MRP/perice
            PdfPCell totalamtCell = new PdfPCell(new Phrase(new Chunk(row["sizeid"].ToString(), EmailFont)));
            totalamtCell.HorizontalAlignment = 1;
            totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(totalamtCell);


            //no of boxes
            // PdfPCell qtyCell = new PdfPCell(new Phrase("", bodyFont));
            PdfPCell qtyCell = new PdfPCell(new Phrase(new Chunk(row["HSNCode"].ToString(), EmailFont)));
            qtyCell.HorizontalAlignment = 1;
            qtyCell.PaddingLeft = 10f;
            qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(qtyCell);

            //discounted @ 50%
            PdfPCell totalamtCell1 = new PdfPCell(new Phrase((Convert.ToDouble(row["mrp"].ToString())).ToString(), bodyFont));
            // PdfPCell totalamtCell1 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["discount"].ToString())).ToString(), bodyFont));
            //PdfPCell totalamtCell1 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(row["discounted"].ToString())).ToString(), bodyFont));
            totalamtCell1.HorizontalAlignment = 1;
            totalamtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(totalamtCell1);


            //Taxable Amt
            PdfPCell totalamtCell3 = new PdfPCell(new Phrase((Convert.ToDouble(row["qty"].ToString())).ToString(), bodyFont));
            totalamtCell3.HorizontalAlignment = 1;
            totalamtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(totalamtCell3);



            //CGST per
            PdfPCell totalamtCell4 = new PdfPCell(new Phrase((Convert.ToDouble(row["discount"].ToString())).ToString(), bodyFont));
            totalamtCell4.HorizontalAlignment = 1;
            totalamtCell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(totalamtCell4);


            //CGST amt
            PdfPCell totalamtCell5 = new PdfPCell(new Phrase((row["TotalAmount"].ToString()).ToString(), bodyFont));
            totalamtCell5.HorizontalAlignment = 1;
            totalamtCell5.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(totalamtCell5);




            Total_TotalQty += Convert.ToDecimal(row["qty"].ToString());
            Total_per_CGST += Convert.ToDecimal(row["CGSTper"].ToString());
            Total_per_sGST += Convert.ToDecimal(row["SGSTper"].ToString());
            Total_per_iGST += Convert.ToDecimal(row["IGSTper"].ToString());

        }

        // Table footer
        //sr number
        PdfPCell totalAmtCell1 = new PdfPCell(new Phrase("  ", bodyFont));
        totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell1.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell1);

        PdfPCell totalAmtCell2 = new PdfPCell(new Phrase("G.Total", FontFactory.GetFont("arial", 7, Font.BOLD)));
        totalAmtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell2.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell2);

        //PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(Total_TotalQty.ToString(), bodyFont));
        PdfPCell totalAmtCell3 = new PdfPCell(new Phrase("", bodyFont));
        totalAmtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell3.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell3);

        //PdfPCell totalAmtCell4 = new PdfPCell(new Phrase(Total_Pricee.ToString(), bodyFont));
        PdfPCell totalAmtCell4 = new PdfPCell(new Phrase("", bodyFont));
        //PdfPCell totalAmtCell4 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totalqty.ToString()),2).ToString() , bodyFont));
        totalAmtCell4.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell4.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell4);

        //PdfPCell totalAmtCell5 = new PdfPCell(new Phrase(Total_Discount.ToString(), bodyFont));
        PdfPCell totalAmtCell5 = new PdfPCell(new Phrase("", bodyFont));
        totalAmtCell5.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell5.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell5);

        PdfPCell totalAmtCell6 = new PdfPCell(new Phrase("", bodyFont));
        totalAmtCell6.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell6.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell6);

        PdfPCell totalAmtCell7 = new PdfPCell(new Phrase(Total_TotalQty.ToString(), bodyFont));
        //PdfPCell totalAmtCell7 = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble(totaltaxable.ToString()),2).ToString(), bodyFont));
        totalAmtCell7.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell7.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell7);



        PdfPCell totalAmtCell8 = new PdfPCell(new Phrase("", bodyFont));
        totalAmtCell8.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell8.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell8);


        PdfPCell totalAmtCellt = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["subamount"].ToString(), bodyFont));
        totalAmtCellt.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER ;
        totalAmtCellt.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCellt);

        pdfDoc.Add(itemTable);


        PdfPTable Invoicetable3 = new PdfPTable(1);
        Invoicetable3.HorizontalAlignment = 0;
        Invoicetable3.WidthPercentage = 100;
        Invoicetable3.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
        Invoicetable3.DefaultCell.Border = Rectangle.NO_BORDER;
        Invoicetable3.SpacingBefore = 10f;
        // Invoicetable3.SpacingBefore = 270f;
        PdfPTable tablenewt = new PdfPTable(5);

        PdfPCell cellnewt = new PdfPCell(new Phrase("Bank Name:" + dt.Tables[3].Rows[0]["bankName"].ToString() + "\n\n" + "Ac No. : "+dt.Tables[3].Rows[0]["accountNo"].ToString() + "\n\n" + "Branch : " + dt.Tables[3].Rows[0]["branch"].ToString()+ "\n\n" + "IFSC Code : " + dt.Tables[3].Rows[0]["ifscCode"].ToString(), FontFactory.GetFont("Mangal", 8, Font.BOLD)));
        cellnewt.HorizontalAlignment = PdfPCell.ALIGN_LEFT ;
        cellnewt.Colspan = 3;
        cellnewt.HorizontalAlignment = 1;

        cellnewt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right

        tablenewt.AddCell(cellnewt);

        PdfPCell cellnew1t;
        PdfPCell cellnew2t;
        if (Total_per_CGST != 0 && Total_per_sGST != 0)
        {
            cellnew1t = new PdfPCell(new Phrase("Sub Total :" + "\n\n" + "Extra Disc : " + dt.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString() + "\n\n " + "Total CGST :" + Total_per_CGST.ToString() + " %" + "\n\n " + "Total SGST :" + Total_per_sGST.ToString() + " %" +  "\n\n" + "Net Amount :", EmailFont));
            cellnew1t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew1t.HorizontalAlignment = 2;


            cellnew2t = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["subamount"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["CGSTamt"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["SGSTamt"].ToString() + "\n\n" +  dt.Tables[0].Rows[0]["grandTotal"].ToString(), EmailFont));
            cellnew2t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew2t.Colspan = 2;
            tablenewt.AddCell(cellnew1t);

            tablenewt.AddCell(cellnew2t);
        }
        else if (Total_per_iGST != 0 )
        {

              cellnew1t = new PdfPCell(new Phrase("Sub Total :" + "\n\n" + "Extra Disc : " + dt.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString() + "\n\n " + "Total IGST :" + Total_per_iGST.ToString() + " %" + "\n\n" + "Net Amount :", EmailFont));
            cellnew1t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew1t.HorizontalAlignment = 2;


            cellnew2t = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["subamount"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["IGSTamt"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["grandTotal"].ToString(), EmailFont));
            cellnew2t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew2t.Colspan = 2;
            tablenewt.AddCell(cellnew1t);

            tablenewt.AddCell(cellnew2t);

        }
        //PdfPCell cellnew1t = new PdfPCell(new Phrase("Sub Total :" + "\n\n" + "Extra Disc : "+dt.Tables[0].Rows[0]["per_tradeDisandScheme"].ToString() + "\n\n " + "Total CGST :" + Total_per_CGST.ToString()+" %" + "\n\n " + "Total SGST :"+ Total_per_sGST.ToString() + " %" + "\n\n" + "Total IGST :"+ Total_per_iGST.ToString() + " %" + "\n\n" + "Net Amount :", EmailFont));
        //cellnew1t.BackgroundColor = new Color(System.Drawing.Color.Khaki);    



        //PdfPCell cellnew2t = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["subamount"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["amt_tradeDisandScheme"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["CGSTamt"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["SGSTamt"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["IGSTamt"].ToString() + "\n\n" + dt.Tables[0].Rows[0]["grandTotal"].ToString(), EmailFont));
        //cellnew2t.BackgroundColor = new Color(System.Drawing.Color.Khaki);

        // PdfPCell cellnew3t = new PdfPCell(new Phrase("Bank Details:- A/c. name:- Adhikar Paper Industries" + "\n" + "Bank Name:- 'UNION BANK OF INDIA'" + "\n" + "1.CC ACCOUNT NO:-'596805010000102'" + "\n" + "2: Currnet Account Numer: 5968050100000102 " + "\n" + "Jail Road Branch,Nashik" + "\n" + "IFSC Code :UBIN0559687", EmailFont));
        PdfPCell cellnew3t = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["inwords"].ToString(), EmailFont));


        PdfPCell cellnew5t = new PdfPCell(new Phrase("I/We hereby certify that my registration certificate under the GST act 2017 is in force on the date on which the transaction of sale covered by this tax invoice has been effected by me and turnover of sale and the tax if any,payable on the sales has been or shall be paid.", footerfont));
        PdfPCell cellnew6t = new PdfPCell(new Phrase(dt.Tables[3].Rows[0]["shopName"].ToString()+"\n\n\n"+"Proprietor Manager"));
         
       
        

        cellnew3t.Colspan = 7;
        tablenewt.AddCell(cellnew3t);




        cellnew5t.Colspan =3;
        tablenewt.AddCell(cellnew5t);

        cellnew6t.Colspan = 3;
        cellnew6t.HorizontalAlignment =1;
        tablenewt.AddCell(cellnew6t);

      

      
 

        PdfPCell nesthousingnt = new PdfPCell(tablenewt);
        nesthousingnt.Border = Rectangle.NO_BORDER;

        nesthousingnt.PaddingBottom = 10f;
        Invoicetable3.AddCell(nesthousingnt);


        //pdfDoc.Add(itemTable);
        pdfDoc.Add(Invoicetable3);

        #endregion



        //////////////////////////////////////

        pdfDoc.Close();


        return PDFData;


    }



    private static String ConvertToWords(String numb)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = "Only";
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = "and";// just to separate whole numbers from points/cents  
                    endStr = "Paisa " + endStr;//Cents  
                    pointStr = ConvertDecimals(points);
                }
            }
            val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch { }
        return val;
    }


    private static String ConvertDecimals(String number)
    {
        String cd = "", digit = "", engOne = "";
        for (int i = 0; i < number.Length; i++)
        {
            digit = number[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cd += " " + engOne;
        }
        return cd;
    }



    private static String ones(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }


    private static String ConvertWholeNumber(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX  
            bool isDone = false;//test if already translated  
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))  
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric  
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping  
                String place = "";//digit grouping name:hundres,thousand,etc...  
                switch (numDigits)
                {
                    case 1://ones' range  

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range  
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range  
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range  
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range  
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range  
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...  
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)  
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }

                    //check for trailing zeros  
                    //if (beginsZero) word = " and " + word.Trim();  
                }
                //ignore digit grouping names  
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }
        return word.Trim();
    }

    private static String tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }
    #endregion

    #region--Download PDF
    protected void DownloadPDF(System.IO.MemoryStream PDFData)
    {
        // Clear response content & headers
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.Charset = string.Empty;
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Invoice-{0}.pdf", "OrderNo"));
        HttpContext.Current.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);

        //  HttpContext.Current.Response.OutputStream.
        HttpContext.Current.Response.OutputStream.Flush();
        HttpContext.Current.Response.OutputStream.Close();
        HttpContext.Current.Response.End();





    }

    #endregion


}
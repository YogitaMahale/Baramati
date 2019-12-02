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


public partial class addCreditNoteInvoice : System.Web.UI.Page
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
            orderid = Request.QueryString["id"].ToString();

            DownloadPDF(GeneratePDF());
        }
    }


    #region pdfgeenration

  

    public MemoryStream GeneratePDF()
    {



        //  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        dt = new DataSet();

        cmd = new SqlCommand();
        cmd.CommandText = "creditnotes_invoice1";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", orderid);
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
        var titleFontBlue = FontFactory.GetFont("Arial", 18, Font.NORMAL, Color.BLACK);
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


     
        #region Top table
        // Create the header table 
        PdfPTable headertable = new PdfPTable(1);
        headertable.HorizontalAlignment = 0;
        headertable.WidthPercentage = 100;
        headertable.SetWidths(new float[] {  440f });  // then set the column's __relative__ widths
        headertable.DefaultCell.Border = Rectangle.NO_BORDER;
        headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

        //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/Images/500.png"));
        //// //logo.ScaleToFit(100,80);
        ////logo.ScaleToFit(70, 120);
        //logo.ScaleToFit(70, 120);
        //{

        //    PdfPCell pdfCelllogo = new PdfPCell(logo);
        //    pdfCelllogo.Border = Rectangle.NO_BORDER;
        //    pdfCelllogo.BorderColorBottom = new Color(System.Drawing.Color.Black);
        //    pdfCelllogo.BorderWidthBottom = 1f;
        //    pdfCelllogo.Top = 0;


        //    headertable.AddCell(pdfCelllogo);
        //}

        {
            PdfPTable nested = new PdfPTable(1);

            nested.DefaultCell.Border = Rectangle.NO_BORDER;



            PdfPCell nextPostCell1 = new PdfPCell(new Phrase(dt.Tables[2].Rows[0]["shopName"].ToString(), titleFontBlue));
            nextPostCell1.Border = Rectangle.NO_BORDER;
            nextPostCell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            nested.AddCell(nextPostCell1);
            string ddd = dt.Tables[2].Rows[0]["address"].ToString() + "\n" + dt.Tables[2].Rows[0]["phone"].ToString();
            PdfPCell nextPostCell2 = new PdfPCell(new Phrase(ddd , boldTableFont1));
            nextPostCell2.Border = Rectangle.NO_BORDER;
            nextPostCell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            nested.AddCell(nextPostCell2);
            PdfPCell nextPostCell3 = new PdfPCell(new Phrase(dt.Tables[2].Rows[0]["GSTIN"].ToString(), boldTableFont1));
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


            PdfPCell cellnew2 = new PdfPCell(new Phrase(" Name : "  + dt.Tables[1].Rows[0]["NAME"].ToString() + "\n Address :" + dt.Tables[1].Rows[0]["address"].ToString() + "\n Mobile No. : " + "" + dt.Tables[1].Rows[0]["phone"].ToString() + "" + "\n" + "GST No." + dt.Tables[1].Rows[0]["gstno"].ToString() + "                                                     ", EmailFont));

            PdfPCell cellnew3 = new PdfPCell(new Phrase("Credit Note No.:" + dt.Tables[0].Rows[0]["id"].ToString() + "\n" + "Date :" + dt.Tables[0].Rows[0]["createddate"].ToString() + "\n" + "Reason :" + dt.Tables[0].Rows[0]["reason"].ToString() , EmailFont));


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
        PdfPTable itemTable = new PdfPTable(3);

        itemTable.HorizontalAlignment = 0;
        itemTable.WidthPercentage = 100;
        // itemTable.SetWidths(new float[] {2,30,6,6,6,6,6,6,6,6,6,4,4,7 }); 4, 30, 6, 6, 6, 6, 6, 6, 6 // then set the column's __relative__ widths
        itemTable.SetWidths(new float[] { 4, 30, 6});
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



        PdfPCell cell3 = new PdfPCell(new Phrase("Amount", boldTableFont));
        cell3.BackgroundColor = TabelHeaderBackGroundColor;
        cell3.HorizontalAlignment = 1;
        itemTable.AddCell(cell3);


        foreach (DataRow row in dt.Tables[0].Rows)
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
            _phrase.Add(new Chunk(row["description"].ToString(), EmailFont));
            PdfPCell descCell = new PdfPCell(_phrase);
            descCell.HorizontalAlignment = 0;
            descCell.PaddingLeft = 10f;
            descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(descCell);

            //totalqty
            PdfPCell amountCell = new PdfPCell(new Phrase(new Chunk(row["famt"].ToString(), EmailFont)));
            amountCell.HorizontalAlignment = 1;
            amountCell.PaddingLeft = 10f;
            amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            itemTable.AddCell(amountCell);
           

        }
        // Table footer
        //sr number
        PdfPCell totalAmtCell1 = new PdfPCell(new Phrase("", bodyFont));
        totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell1.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell1);

        PdfPCell totalAmtCell2 = new PdfPCell(new Phrase("Total Amount : ", FontFactory.GetFont("arial", 7, Font.BOLD)));
        totalAmtCell2.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
        totalAmtCell2.HorizontalAlignment =2;
        itemTable.AddCell(totalAmtCell2);

        //PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(Total_TotalQty.ToString(), bodyFont));
        PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["ftotal"].ToString(), bodyFont));
        totalAmtCell3.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER|Rectangle.RIGHT_BORDER ;
        totalAmtCell3.HorizontalAlignment = 1;
        itemTable.AddCell(totalAmtCell3);

      

        pdfDoc.Add(itemTable);


        PdfPTable Invoicetable3 = new PdfPTable(1);
        Invoicetable3.HorizontalAlignment = 0;
        Invoicetable3.WidthPercentage = 100;
        Invoicetable3.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
        Invoicetable3.DefaultCell.Border = Rectangle.NO_BORDER;
        Invoicetable3.SpacingBefore = 10f;
        // Invoicetable3.SpacingBefore = 270f;
        PdfPTable tablenewt = new PdfPTable(5);

        PdfPCell cellnewt = new PdfPCell(new Phrase("In Words :" + dt.Tables[0].Rows[0]["inwords"].ToString() , FontFactory.GetFont("Mangal", 8, Font.BOLD)));
        cellnewt.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cellnewt.Colspan = 100;
        cellnewt.HorizontalAlignment = 1;

        cellnewt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right

        tablenewt.AddCell(cellnewt);

        PdfPCell cellnew1t;
        PdfPCell cellnew2t;
        
            cellnew1t = new PdfPCell(new Phrase("Total Amount:" , EmailFont));
            cellnew1t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew1t.HorizontalAlignment = 2;


            cellnew2t = new PdfPCell(new Phrase(dt.Tables[0].Rows[0]["ftotal"].ToString(), EmailFont));
            cellnew2t.BackgroundColor = new Color(System.Drawing.Color.Khaki);
            cellnew2t.Colspan = 2;
            //tablenewt.AddCell(cellnew1t);

            //tablenewt.AddCell(cellnew2t);
       

        PdfPCell cellnew3t = new PdfPCell(new Phrase("Declaration : We declare that this invoice shows the actual price of the goods described and that all particulars are true and coorect.", EmailFont));

               


        cellnew3t.Colspan = 7;
        tablenewt.AddCell(cellnew3t);



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
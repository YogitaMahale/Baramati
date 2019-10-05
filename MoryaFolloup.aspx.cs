using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
public partial class MoryaFolloup : System.Web.UI.Page
{
    string newsFrontPath = "~/uploads/news/front/";
    common ocommon = new common();
    string sqlconnect = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindNews();
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product News Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Product News Inserted Successfully";
        }

        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage Morya Followup ";
    }

    private void BindNews()
    {
        string selectquery = "SELECT [PKId],[Name],[Mobile],[City],[IsRead],[Remark], CustomerType, Product, EnquiryFrom FROM MoryaTblFollowup where [IsRead]=0";
        SqlDataAdapter danewdata = new SqlDataAdapter(selectquery, sqlconnect);
        DataTable dtStudent = new DataTable();

        danewdata.Fill(dtStudent);

        if (dtStudent != null)
        {
            if (dtStudent.Rows.Count > 0)
            {
                repNews.DataSource = dtStudent;
                repNews.DataBind();
            }
            else
            {
                repNews.DataSource = null;
                repNews.DataBind();
            }
        }
        else
        {
            repNews.DataSource = null;
            repNews.DataBind();
        }
    }






    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload and save the file
            string excelPath = Server.MapPath("~/uploads/UploadedFile/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    //   conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "data source=" + excelPath + ";Extended Properties='Excel 8.0;IMEX=1'"
;
                    break;
                case ".xlsx": //Excel 07 or higher
                    // conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "data source=" + excelPath + ";Extended Properties='Excel 8.0;IMEX=1'"
;



                    break;


            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[7] {
                new DataColumn("NAME", typeof(string)),
                new DataColumn("MOBILE", typeof(string)),
                new DataColumn("CITY", typeof(string)),
                new DataColumn("REMARK", typeof(string)),
                new DataColumn("Customer", typeof(string)),
                      new DataColumn("Product", typeof(string)),
                         new DataColumn("Enquiry From", typeof(string)),

                  });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "MoryaTblFollowup";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("NAME", "Name");
                        sqlBulkCopy.ColumnMappings.Add("MOBILE", "Mobile");
                        sqlBulkCopy.ColumnMappings.Add("CITY", "City");
                        sqlBulkCopy.ColumnMappings.Add("REMARK", "Remark");


                        sqlBulkCopy.ColumnMappings.Add("Customer", "CustomerType");
                        sqlBulkCopy.ColumnMappings.Add("Product", "Product");
                        sqlBulkCopy.ColumnMappings.Add("Enquiry From", "EnquiryFrom");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }

                    Label1.Text = "File Uploaded Successfully";
                }

                BindNews();
            }
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message + ex.StackTrace);
        }
    }
}
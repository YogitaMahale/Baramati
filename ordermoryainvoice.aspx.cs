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

public partial class ordermoryainvoice : System.Web.UI.Page
{
    common ocommon = new common();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    string OrderInvoiceMainPath = "~/uploads/orderimage/";
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Manage Morya Order's Invoice";
        if (!Page.IsPostBack)
        {
            BindInvoiceImage();
        }
    }

    private void BindInvoiceImage()
    {
        DataTable dtImage = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select top 2 adminimage from orderimagemaster where adminimage is not null and oid=" + Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)) + " order by createddatetime desc ";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtImage);
        try
        {
            divImage.Visible = true;
            if (dtImage.Rows.Count > 0)
            {
                for (int i = 0; i < dtImage.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        imgOrderImage1.ImageUrl = OrderInvoiceMainPath + dtImage.Rows[i]["adminimage"].ToString();
                    }
                    if (i == 1)
                    {
                        imgOrderImage2.ImageUrl = OrderInvoiceMainPath + dtImage.Rows[i]["adminimage"].ToString();
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

    protected void btnImages_Click(object sender, EventArgs e)
    {
        if (fpImage.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage.FileName);
            fpImage.SaveAs(MapPath(OrderInvoiceMainPath + fileName));
            Int64 Result = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO orderimagemaster([oid],[adminimage],createddatetime)VALUES(" + Convert.ToInt64(ocommon.Decrypt(Request.QueryString["oid"].ToString(), true)) + ",'" + fileName + "',GETDATE())";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                Result = cmd.ExecuteNonQuery();
                bMessage.Visible = true;
                if (Result > 0)
                {
                    divImage.Visible = true;
                    bMessage.InnerHtml = "Morya Order Image Uploaded Successfully...";
                    con.Close();
                    BindInvoiceImage();
                }
                else
                {
                    con.Close();
                    bMessage.InnerHtml = "Image Not Inserted...";
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ErrHandler.writeError(ex.Message, ex.StackTrace);
            }
        }
    }

}
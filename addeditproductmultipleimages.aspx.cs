using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using System.IO;
using System.Drawing;
public partial class addeditproductmultipleimages : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    int productImageFrontWidth = 1000;
    int productImageFrontHeight = 900;
    string productMainPath = "~/uploads/product/";
    string productFrontPath = "~/uploads/product/water/";
    string productWaterFrontPath = "~/uploads/product/front/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Add Edit Product Images";
        if (!Page.IsPostBack)
        {
            BindImagesUsingProductId(Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true)));
        }
    }

    public void BindImagesUsingProductId(Int64 ProductId)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            DataTable dtGallery = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM productimagesvideos Where isdelete=0 and pid=" + ProductId;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dtGallery);
            con.Close();
            if (dtGallery != null)
            {
                if (dtGallery.Rows.Count > 0)
                {
                    repImage.Visible = true;
                    repImage.DataSource = dtGallery;
                    repImage.DataBind();
                }
                else
                {
                    repImage.Visible = false;
                    repImage.DataSource = null;
                    repImage.DataBind();
                }
            }
            else
            {
                repImage.Visible = false;
                repImage.DataSource = null;
                repImage.DataBind();
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

    protected void repImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            System.Web.UI.WebControls.Image imgProduct = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgProduct");
            imgProduct.ImageUrl = "~/uploads/product/front/" + DataBinder.Eval(e.Item.DataItem, "imagevideopath").ToString();
        }
    }

    protected void lnDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 imageId = int.Parse((item.FindControl("hfImageId") as HiddenField).Value);
        bool yes = (new Cls_productimagesvideos_b().Delete(imageId));
        bMessage.Visible = true;
        if (yes)
        {
            BindImagesUsingProductId(Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true)));
            bMessage.Style.Add("color", "green");
            bMessage.InnerText = "Product Image Deleted Successfully";
        }
        else
        {
            bMessage.Style.Add("color", "red");
            bMessage.InnerText = "Product Image Not Deleted";
        }
    }

    protected void btnImages_Click(object sender, EventArgs e)
    {
        if (fpImage.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage.FileName.Replace(' ', '_')) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage.FileName);
            fpImage.SaveAs(MapPath(productMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\product\\", productImageFrontWidth, productImageFrontHeight, "~/Uploads/product/water/", fileName);
            WatermarkImageCreate(fileName);
            productimagesvideos objproductimagesvideos = new productimagesvideos();
            objproductimagesvideos.pid = Convert.ToInt64(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
            objproductimagesvideos.imagevideoname = txtImageName.Text.Trim();
            objproductimagesvideos.imagevideopath = fileName;
            Int64 Result = (new Cls_productimagesvideos_b().Insert(objproductimagesvideos));
            bMessage.Visible = true;
            if (Result > 0)
            {
                txtImageName.Text = string.Empty;
                bMessage.InnerHtml = "Image Inserted Successfully...";
            }
            else
            {
                bMessage.InnerHtml = "Image Not Inserted...";
            }
            BindImagesUsingProductId(Convert.ToInt64(objproductimagesvideos.pid));
        }
    }

    private void WatermarkImageCreate(string fileName)
    {
        string watermarkText = "© MORYATOOLS";
        using (Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(productFrontPath) + fileName, false))
        {
            using (Graphics grp = Graphics.FromImage(bmp))
            {
                Brush brush = new SolidBrush(Color.Gray);
                Font font = new System.Drawing.Font("Book Antiqua", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF textSize = new SizeF();
                textSize = grp.MeasureString(watermarkText, font);

                #region " Top "

                Point positionLeftTop = new Point(0, 0);
                grp.DrawString(watermarkText, font, brush, positionLeftTop);

                Point positionCenterTop = new Point(((bmp.Width - ((int)textSize.Width)) / 2), 0);
                grp.DrawString(watermarkText, font, brush, positionCenterTop);


                Point positionRightTop = new Point((bmp.Width - ((int)textSize.Width)), 0);
                grp.DrawString(watermarkText, font, brush, positionRightTop);

                #endregion " Top "

                #region " Bottom "

                Point positionLeftBottom = new Point(0, ((bmp.Height - ((int)textSize.Height))));
                grp.DrawString(watermarkText, font, brush, positionLeftBottom);


                Point positionCenterBottom = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height))));
                grp.DrawString(watermarkText, font, brush, positionCenterBottom);

                Point positionRightBottom = new Point((bmp.Width - ((int)textSize.Width)), (bmp.Height - ((int)textSize.Height)));
                grp.DrawString(watermarkText, font, brush, positionRightBottom);

                #endregion " Bottom "

                #region " Center "

                Point positionLeftCenter = new Point(0, ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionLeftCenter);

                Point positionCenter = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionCenter);

                Point positionRightCenter = new Point((bmp.Width - ((int)textSize.Width)), ((bmp.Height - ((int)textSize.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionRightCenter);

                #endregion " Center "

                #region " Top Middle "

                Point positionTopLeftMiddle = new Point(0, ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopLeftMiddle);


                Point positionTopMiddleCenter = new Point(((bmp.Width - ((int)textSize.Width)) / 2), ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopMiddleCenter);

                Point positionTopRightMiddle = new Point((bmp.Width - ((int)textSize.Width)), ((bmp.Height - ((int)textSize.Height)) / 4));
                grp.DrawString(watermarkText, font, brush, positionTopRightMiddle);

                Point positionBottomLeftMiddle = new Point(0, (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomLeftMiddle);

                Point positionBottomRightMiddle = new Point((bmp.Width - ((int)textSize.Width)), (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomRightMiddle);

                Point positionBottomCenterMiddle = new Point((((bmp.Width - ((int)textSize.Width)) / 2)), (((bmp.Height / 2) + (bmp.Height)) / 2));
                grp.DrawString(watermarkText, font, brush, positionBottomCenterMiddle);


                #endregion " Top Middle "

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bmp.Save(HttpContext.Current.Server.MapPath(productWaterFrontPath) + fileName);
                    memoryStream.Position = 0;
                }
            }
        }
    }


}
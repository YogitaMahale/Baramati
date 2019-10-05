using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class addeditnews : System.Web.UI.Page
{
    int newsImageFrontWidth = 800;
    int newsImageFrontHeight = 750;
    string newsMainPath = "~/uploads/news/";
    string newsFrontPath = "~/uploads/news/front/";
    string newsWaterFrontPath = "~/uploads/news/water/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                hPageTitle.InnerText = "Update New Product Arrival";
                Page.Title = "Update New Product Arrival";
                btnAddEditNews.Text = "UPADTE";
                BindNews(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
            }
            else
            {
                hPageTitle.InnerText = "Add New Product Arrival";
                Page.Title = "Add New Product Arrival";
                btnAddEditNews.Text = "ADD";
            }
        }
    }

    private void Clear()
    {
        txtNewsTitle.Text = string.Empty;
        txtShortDescp.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtNewsDate.Text = string.Empty;
        btnUpload.Visible = true;
        btnUpload2.Visible = true;
        btnUpload3.Visible = true;
        btnUpload4.Visible = true;
        btnUpload5.Visible = true;
        btnRemove.Visible = false;
        btnRemove2.Visible = false;
        btnRemove3.Visible = false;
        btnRemove4.Visible = false;
        btnRemove5.Visible = false;
        imgNews.Visible = false;
        imgNews2.Visible = false;
        imgNews3.Visible = false;
        imgNews4.Visible = false;
        imgNews5.Visible = false;
        ViewState["fileName"] = null;
        ViewState["fileName2"] = null;
        ViewState["fileName3"] = null;
        ViewState["fileName4"] = null;
        ViewState["fileName5"] = null;
    }

    private void ClearAll()
    {
        btnUpload.Visible = true;
        btnUpload2.Visible = true;
        btnUpload3.Visible = true;
        btnUpload4.Visible = true;
        btnUpload5.Visible = true;

        btnRemove.Visible = false;
        btnRemove2.Visible = false;
        btnRemove3.Visible = false;
        btnRemove4.Visible = false;
        btnRemove5.Visible = false;

        imgNews.Visible = false;
        imgNews2.Visible = false;
        imgNews3.Visible = false;
        imgNews4.Visible = false;
        imgNews5.Visible = false;

        ViewState["fileName"] = null;
        ViewState["fileName"] = null;
        ViewState["fileName2"] = null;
        ViewState["fileName3"] = null;
        ViewState["fileName4"] = null;
        ViewState["fileName5"] = null;

        txtNewsTitle.Text = string.Empty;
        txtNewsDate.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }

    private void BindNews(string Id)
    {
        newsupdate objnewsupdate = (new Cls_newsupdate_b().SelectById(Convert.ToInt64(Id)));
        if (objnewsupdate != null)
        {
            txtNewsTitle.Text = objnewsupdate.title;
            txtShortDescp.Text = objnewsupdate.shortdescp;
            txtDescription.Text = objnewsupdate.longdescp;
            txtNewsDate.Text = objnewsupdate.newsdate;
            if (!string.IsNullOrEmpty(objnewsupdate.imagename))
            {
                imgNews.Visible = true;
                ViewState["fileName"] = objnewsupdate.imagename;
                imgNews.ImageUrl = newsFrontPath + objnewsupdate.imagename;
                btnUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                btnUpload.Visible = true;
            }
            if (!string.IsNullOrEmpty(objnewsupdate.imagename2))
            {
                imgNews2.Visible = true;
                ViewState["fileName2"] = objnewsupdate.imagename2;
                imgNews2.ImageUrl = newsFrontPath + objnewsupdate.imagename2;
                btnUpload2.Visible = false;
                btnRemove2.Visible = true;
            }
            else
            {
                btnUpload2.Visible = true;
            }
            if (!string.IsNullOrEmpty(objnewsupdate.imagename3))
            {
                imgNews3.Visible = true;
                ViewState["fileName3"] = objnewsupdate.imagename3;
                imgNews3.ImageUrl = newsFrontPath + objnewsupdate.imagename3;
                btnUpload3.Visible = false;
                btnRemove3.Visible = true;
            }
            else
            {
                btnUpload3.Visible = true;
            }
            if (!string.IsNullOrEmpty(objnewsupdate.imagename4))
            {
                imgNews4.Visible = true;
                ViewState["fileName4"] = objnewsupdate.imagename4;
                imgNews4.ImageUrl = newsFrontPath + objnewsupdate.imagename4;
                btnUpload4.Visible = false;
                btnRemove4.Visible = true;
            }
            else
            {
                btnUpload4.Visible = true;
            }
            if (!string.IsNullOrEmpty(objnewsupdate.imagename5))
            {
                imgNews5.Visible = true;
                ViewState["fileName5"] = objnewsupdate.imagename5;
                imgNews5.ImageUrl = newsFrontPath + objnewsupdate.imagename5;
                btnUpload5.Visible = false;
                btnRemove5.Visible = true;
            }
            else
            {
                btnUpload5.Visible = true;
            }
        }
    }

    private DataTable SelectAllDealers()
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select did,name,deviceid,userloginmobileno from dealermaster Where isdeleted=0";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = ConnectionString;
            ConnectionString.Open();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ErrHandler.writeError(ex.Message, ex.StackTrace);
            return null;
        }
        finally
        {
            ConnectionString.Close();
        }
        return ds.Tables[0];
    }

    protected override void Render(HtmlTextWriter writer)
    {
        string validatorOverrideScripts = "<script src=\"" + Page.ResolveUrl("~") + "js/validators.js\" type=\"text/javascript\"></script>";
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ValidatorOverrideScripts", validatorOverrideScripts, false);
        base.Render(writer);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/managenews.aspx"));
    }

    protected void btnAddEditNews_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        newsupdate objnewsupdate = new newsupdate();
        objnewsupdate.title = txtNewsTitle.Text;
        objnewsupdate.shortdescp = txtShortDescp.Text;
        objnewsupdate.longdescp = txtDescription.Text;
        objnewsupdate.newsdate = txtNewsDate.Text;
        if (ViewState["fileName"] != null)
        {
            objnewsupdate.imagename = ViewState["fileName"].ToString();
        }
        if (ViewState["fileName2"] != null)
        {
            objnewsupdate.imagename2 = ViewState["fileName2"].ToString();
        }
        if (ViewState["fileName3"] != null)
        {
            objnewsupdate.imagename3 = ViewState["fileName3"].ToString();
        }
        if (ViewState["fileName4"] != null)
        {
            objnewsupdate.imagename4 = ViewState["fileName4"].ToString();
        }
        if (ViewState["fileName5"] != null)
        {
            objnewsupdate.imagename5 = ViewState["fileName5"].ToString();
        }
        if (Request.QueryString["id"] != null)
        {
            objnewsupdate.newsupdateid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_newsupdate_b().Update(objnewsupdate));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/managenews.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "News Not Updated";
                BindNews(ocommon.Decrypt(Convert.ToString(Request.QueryString["id"]), true));
            }
        }
        else
        {
            Result = (new Cls_newsupdate_b().Insert(objnewsupdate));
            if (Result > 0)
            {
                SendNotification(objnewsupdate.title, "http://moryaapp.moryatools.com/uploads/news/front/" + objnewsupdate.imagename + " , " + "http://moryaapp.moryatools.com/uploads/news/front/" + objnewsupdate.imagename2 + " , " + "http://moryaapp.moryatools.com/uploads/news/front/" + objnewsupdate.imagename3 + " , " + "http://moryaapp.moryatools.com/uploads/news/front/" + objnewsupdate.imagename4 + " , " + "http://moryaapp.moryatools.com/uploads/news/front/" + objnewsupdate.imagename5, objnewsupdate.shortdescp, objnewsupdate.newsdate);
                Response.Redirect(Page.ResolveUrl("~/managenews.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "News Not Inserted";
            }
        }
    }

    private void SendNotification(string NewsTitle, string NewsImage, string NewsDescp, string NewsDate)
    {
        String sResponseFromServer = string.Empty;
        DataTable dtDealerList = SelectAllDealers();
        if (dtDealerList != null)
        {
            for (int i = 0; i < dtDealerList.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dtDealerList.Rows[i]["deviceid"].ToString()))
                {
                    try
                    {
                        sResponseFromServer = string.Empty;
                        var applicationID = "AIzaSyBHxPITxegYgcgMmhdJ0ceRJCqz8wxJyUM";
                        var senderId = "315798993928";
                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";
                        tRequest.ContentType = "application/json";
                        var data1 = new
                        {
                            to = dtDealerList.Rows[i]["deviceid"].ToString(),
                            data = new
                            {
                                StatusCode = "302",
                                //Image = "http://moryaapp.moryatools.com/uploads/news/front/" + NewsImage,
                                Image = NewsImage,
                                Title = NewsTitle,
                                Description = NewsDescp + ' ' + NewsDate,
                            },
                            priority = "high"
                        };
                        var serializer = new JavaScriptSerializer();
                        var json = serializer.Serialize(data1);
                        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                        tRequest.ContentLength = byteArray.Length;

                        using (Stream dataStream = tRequest.GetRequestStream())
                        {
                            dataStream.Write(byteArray, 0, byteArray.Length);

                            using (WebResponse tResponse = tRequest.GetResponse())
                            {
                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {
                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {
                                        sResponseFromServer = tReader.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(sResponseFromServer);
                }
            }
        }
        Context.Response.End();
    }

    private void WatermarkImageCreate(string fileName)
    {
        string watermarkText = "© MORYATOOLS";
        using (Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(newsWaterFrontPath) + fileName, false))
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
                    bmp.Save(HttpContext.Current.Server.MapPath(newsFrontPath) + fileName);
                    memoryStream.Position = 0;
                }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fpImage.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage.FileName);
            fpImage.SaveAs(MapPath(newsMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\news\\", newsImageFrontWidth, newsImageFrontHeight, "~/uploads/news/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgNews.Visible = true;
            imgNews.ImageUrl = newsMainPath + fileName;
            ViewState["fileName"] = fileName;
            btnRemove.Visible = true;
            btnUpload.Visible = false;
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        btnUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = null;
        imgNews.Visible = false;
    }

    protected void btnUpload2_Click(object sender, EventArgs e)
    {
        if (fpImage2.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage2.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage2.FileName);
            fpImage2.SaveAs(MapPath(newsMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\news\\", newsImageFrontWidth, newsImageFrontHeight, "~/uploads/news/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgNews2.Visible = true;
            imgNews2.ImageUrl = newsMainPath + fileName;
            ViewState["fileName2"] = fileName;
            btnRemove2.Visible = true;
            btnUpload2.Visible = false;
        }
    }

    protected void btnRemove2_Click(object sender, EventArgs e)
    {
        btnUpload2.Visible = true;
        btnRemove2.Visible = false;
        ViewState["fileName2"] = null;
        imgNews2.Visible = false;
    }

    protected void btnUpload3_Click(object sender, EventArgs e)
    {
        if (fpImage3.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage3.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage3.FileName);
            fpImage3.SaveAs(MapPath(newsMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\news\\", newsImageFrontWidth, newsImageFrontHeight, "~/uploads/news/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgNews3.Visible = true;
            imgNews3.ImageUrl = newsMainPath + fileName;
            ViewState["fileName3"] = fileName;
            btnRemove3.Visible = true;
            btnUpload3.Visible = false;
        }
    }

    protected void btnRemove3_Click(object sender, EventArgs e)
    {
        btnUpload3.Visible = true;
        btnRemove3.Visible = false;
        ViewState["fileName3"] = null;
        imgNews3.Visible = false;
    }

    protected void btnUpload4_Click(object sender, EventArgs e)
    {
        if (fpImage4.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage4.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage4.FileName);
            fpImage4.SaveAs(MapPath(newsMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\news\\", newsImageFrontWidth, newsImageFrontHeight, "~/uploads/news/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgNews4.Visible = true;
            imgNews4.ImageUrl = newsMainPath + fileName;
            ViewState["fileName4"] = fileName;
            btnRemove4.Visible = true;
            btnUpload4.Visible = false;
        }
    }

    protected void btnRemove4_Click(object sender, EventArgs e)
    {
        btnUpload4.Visible = true;
        btnRemove4.Visible = false;
        ViewState["fileName4"] = null;
        imgNews4.Visible = false;
    }

    protected void btnUpload5_Click(object sender, EventArgs e)
    {
        if (fpImage5.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpImage5.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpImage5.FileName);
            fpImage5.SaveAs(MapPath(newsMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\news\\", newsImageFrontWidth, newsImageFrontHeight, "~/uploads/news/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgNews5.Visible = true;
            imgNews5.ImageUrl = newsMainPath + fileName;
            ViewState["fileName5"] = fileName;
            btnRemove5.Visible = true;
            btnUpload5.Visible = false;
        }
    }

    protected void btnRemove5_Click(object sender, EventArgs e)
    {
        btnUpload5.Visible = true;
        btnRemove5.Visible = false;
        ViewState["fileName5"] = null;
        imgNews5.Visible = false;
    }
}
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

public partial class addeditoffer : System.Web.UI.Page
{
    int offerImageFrontWidth = 1000;
    int offerImageFrontHeight = 900;
    string offerMainPath = "~/uploads/offer/";
    string offerFrontPath = "~/uploads/offer/front/";
    string offerWaterFrontPath = "~/uploads/offer/water/";
    common ocommon = new common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            if (Request.QueryString["id"] != null)
            {
                BindOffer(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
                btnSave.Text = "UPDATE";
                hPageTitle.InnerText = "Update Offer ";
                Page.Title = "Update Offer ";
            }
            else
            {
                hPageTitle.InnerText = "Add Offer";
                Page.Title = "Add Offer";
            }
        }
    }

    private void Clear()
    {
        txtSchemeName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtValidFrom.Text = string.Empty;
        txtValidTo.Text = string.Empty;
        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        imgCategory.Visible = false;
        ViewState["fileName"] = null;
    }

    private void BindOffer(Int64 OfferId)
    {
        offers objoffers = (new Cls_offers_b().SelectById(OfferId));
        if (objoffers != null)
        {
            txtSchemeName.Text = objoffers.title;
            txtDescription.Text = objoffers.descp;
            txtValidFrom.Text = objoffers.validfrom;
            txtValidTo.Text = objoffers.validto;
            if (!string.IsNullOrEmpty(objoffers.imagename))
            {
                imgCategory.Visible = true;
                ViewState["fileName"] = objoffers.imagename;
                imgCategory.ImageUrl = offerFrontPath + objoffers.imagename;
                btnImageUpload.Visible = false;
                btnRemove.Visible = true;
            }
            else
            {
                btnImageUpload.Visible = true;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 Result = 0;
        offers objoffers = new offers();
        objoffers.title = txtSchemeName.Text;
        objoffers.descp = txtDescription.Text;
        objoffers.validfrom = txtValidFrom.Text;
        objoffers.validto = txtValidTo.Text;
        if (ViewState["fileName"] != null)
        {
            objoffers.imagename = ViewState["fileName"].ToString();
        }
        if (Request.QueryString["id"] != null)
        {
            objoffers.offerid = Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true));
            Result = (new Cls_offers_b().Update(objoffers));
            if (Result > 0)
            {
                Clear();
                Response.Redirect(Page.ResolveUrl("~/manageoffer.aspx?mode=u"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Offer Not Updated";
                BindOffer(Convert.ToInt64(ocommon.Decrypt(Request.QueryString["id"].ToString(), true)));
            }
        }
        else
        {
            Result = (new Cls_offers_b().Insert(objoffers));
            if (Result > 0)
            {
                SendNotification(objoffers.title, objoffers.imagename, objoffers.descp, objoffers.validfrom, objoffers.validto);
                Response.Redirect(Page.ResolveUrl("~/manageoffer.aspx?mode=i"));
            }
            else
            {
                Clear();
                spnMessgae.Style.Add("color", "red");
                spnMessgae.InnerText = "Offer Not Inserted";

            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/manageoffer.aspx"));
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        btnImageUpload.Visible = true;
        btnRemove.Visible = false;
        ViewState["fileName"] = string.Empty;
        imgCategory.Visible = false;
    }

    protected void btnImageUpload_Click(object sender, EventArgs e)
    {
        
        if (fpCategory.HasFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(fpCategory.FileName) + DateTime.Now.Ticks.ToString() + Path.GetExtension(fpCategory.FileName);
            fpCategory.SaveAs(MapPath(offerMainPath + fileName));
            ocommon.CreateThumbnail1("uploads\\offer\\", offerImageFrontWidth, offerImageFrontHeight, "~/Uploads/offer/water/", fileName);
            //WatermarkImageCreate(fileName);
            imgCategory.Visible = true;
            imgCategory.ImageUrl = offerMainPath + fileName;
            ViewState["fileName"] = fileName;
            btnRemove.Visible = true;
            btnImageUpload.Visible = false;
        }
    }

    private void WatermarkImageCreate(string fileName)
    {
        string watermarkText = "© MORYATOOLS";
        using (Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(offerWaterFrontPath) + fileName, false))
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
                    bmp.Save(HttpContext.Current.Server.MapPath(offerFrontPath) + fileName);
                    memoryStream.Position = 0;
                }
            }
        }
    }

    private void SendNotification(string OfferTitle, string OfferImage, string OfferDescp, string OfferValid, string OfferTo)
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
                                StatusCode = "301",
                                Image = "http://moryaapp.moryatools.com/uploads/offer/front/" + OfferImage,
                                Title = OfferTitle,
                                Description = OfferDescp + ' ' + OfferValid + ' ' + OfferTo,
                                //Sender_Name = "Admin",
                                //Sender_Id = "1",
                                //Sender_type = "A",
                                //Receiver_Name = dtDealerList.Rows[i]["name"].ToString(),
                                //Receiver_id = dtDealerList.Rows[i]["did"].ToString(),
                                //Receiver_type = "D",                                
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
}
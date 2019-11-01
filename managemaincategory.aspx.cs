using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class managemaincategory : System.Web.UI.Page
{
    string categoryFrontPath = "~/uploads/maincategory/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCategory();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Category";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Category Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Category Saved Successfully";
        }
    }

    private void BindCategory()
    {
        DataTable dtCategory = (new Cls_maincategory_b().SelectAllAdmin());
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
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
    }

    protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //DropDownList ddlSeqNo = (DropDownList)e.Item.FindControl("ddlSeqNo");
            Image imgCategory = (Image)e.Item.FindControl("imgCategory");
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditmaincategory.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));
            imgCategory.ImageUrl = categoryFrontPath + DataBinder.Eval(e.Item.DataItem, "imagename").ToString();
            //Fill_SeqNo(Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "SeqNo")), Convert.ToInt64(DataBinder.Eval(e.Item.DataItem, "MaxSeqNo")), ref ddlSeqNo);
        }
    }

    //public void Fill_SeqNo(Int64 SelectedSeqNo, Int64 MaxSeqNo, ref DropDownList DDLSeqNo)
    //{
    //    ListItem lst0;
    //    for (int i = 0; i < MaxSeqNo; i++)
    //    {
    //        lst0 = new ListItem((i + 1).ToString(), (i + 1).ToString());
    //        DDLSeqNo.Items.Add(lst0);
    //    }
    //    DDLSeqNo.DataBind();
    //    if (DDLSeqNo.Items.Count > 0)
    //        DDLSeqNo.SelectedValue = SelectedSeqNo.ToString();
    //}

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
        Int64 CategoryCount = int.Parse((item.FindControl("lblProductCount") as Label).Text);
        spnMessage.Visible = true;
        if (CategoryCount.ToString() == "0")
        {
            Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
            bool yes = (new Cls_maincategory_b().Delete(CategoryId));

            if (yes)
            {
                BindCategory();
                spnMessage.Style.Add("color", "green");
                spnMessage.InnerText = "Category Deleted Successfully";
            }
            else
            {
                spnMessage.Style.Add("color", "red");
                spnMessage.InnerText = "Failed to Delete this Category";
            }
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "There are sub-categories in this category, so you can not delete.";
        }

    }

    protected void IsActive_CheckedChanged(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as CheckBox).Parent as RepeaterItem;
        Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
        bool chkSelected = Convert.ToBoolean((item.FindControl("IsActive") as CheckBox).Checked);
        bool yes = (new Cls_maincategory_b().Category_IsActive(CategoryId, chkSelected));
        if (yes)
        {
            BindCategory();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Category Updated Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Category Not Updated";
        }
    }

    protected void btnNewCategoty_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditmaincategory.aspx"));
    }

    #region seqno indexchanged
    //protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList DDLSeqNo = (DropDownList)sender;
    //    RepeaterItem item = (sender as DropDownList).Parent as RepeaterItem;
    //    Int64 CategoryId = int.Parse((item.FindControl("lblCategoryId") as Label).Text);
    //    Int64 PreSeqNo = int.Parse((item.FindControl("lblSeqNo") as Label).Text);
    //    Update_SeqNo_Category_DB(CategoryId, PreSeqNo, Convert.ToInt64(DDLSeqNo.SelectedValue));
    //}
    #endregion


    #region update seqno in database
    //public void Update_SeqNo_Category_DB(Int64 CategoryId, Int64 PrevSeqNo, Int64 SeqNo)
    //{
    //    SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "Update_SeqNo_Category";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = Con;
    //        cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
    //        cmd.Parameters.AddWithValue("@PrevSeqNo", PrevSeqNo);
    //        cmd.Parameters.AddWithValue("@SeqNo", SeqNo);
    //        Con.Open();
    //        cmd.ExecuteNonQuery();
    //        Con.Close();
    //        spnMessage.Visible = true;
    //        spnMessage.Style.Add("color", "green");
    //        spnMessage.InnerText = "Category Updated Successfully";
    //        BindCategory();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.writeError(ex.Message, ex.StackTrace);
    //    }
    //    finally
    //    {
    //        Con.Close();
    //    }
    //}
    #endregion
}
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Manage_expenseDetails : System.Web.UI.Page
{
    string categoryFrontPath = "~/uploads/category/front/";
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindExpense();
            HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
            hPageTitle.InnerText = "Manage Expense";
        }

        if (Request.QueryString["mode"] == "u")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Expense Updated Successfully";
        }
        else if (Request.QueryString["mode"] == "i")
        {
            spnMessage.Visible = true;
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Expense Inserted Successfully";
        }
    }

    private void BindExpense()
    {
        DataTable dtCategory = (new Cls_ExpenseDetails_b().SelectAll());
        if (dtCategory != null)
        {
            if (dtCategory.Rows.Count > 0)
            {
                repExpense.DataSource = dtCategory;
                repExpense.DataBind();
            }
            else
            {
                repExpense.DataSource = null;
                repExpense.DataBind();
            }
        }
        else
        {
            repExpense.DataSource = null;
            repExpense.DataBind();
        }
    }

    protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //DropDownList ddlSeqNo = (DropDownList)e.Item.FindControl("ddlSeqNo");
            //Image imgCategory = (Image)e.Item.FindControl("imgCategory");
            HyperLink hlEdit = (HyperLink)e.Item.FindControl("hlEdit");
            hlEdit.NavigateUrl = Page.ResolveUrl("~/addeditExpenseDetails.aspx?id=" + ocommon.Encrypt(DataBinder.Eval(e.Item.DataItem, "id").ToString(), true));
            //imgCategory.ImageUrl = categoryFrontPath + DataBinder.Eval(e.Item.DataItem, "imagename").ToString();
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

        spnMessage.Visible = true;

        Int64 ExpenseId = int.Parse((item.FindControl("lblExpenseId") as Label).Text);
        bool yes = (new Cls_ExpenseDetails_b().Delete(ExpenseId));

        if (yes)
        {
            BindExpense();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "Expense Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "red");
            spnMessage.InnerText = "Expense Not Deleted";
        }

    }



    protected void btnNewCategoty_Click(object sender, EventArgs e)
    {
        Response.Redirect(Page.ResolveUrl("~/addeditExpenseDetails.aspx"));
    }




}
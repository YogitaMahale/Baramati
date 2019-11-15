using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
public partial class morya : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usertype"] != null)
        {
            if (Session["usertype"].ToString() == "superadmin" || Session["usertype"].ToString() == "user")
            {
                //divUserAuthority.Visible = true;
            }
            else
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }
        }
        else
        {
            Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
        }
        String userName = String.Empty;

        #region loadMenu         
        try
        {
            if (Session["nameuser"] != null)
                userName = Session["nameuser"].ToString();

            Label lblLogin = (Label)Page.Master.FindControl("lblLogin");
            lblLogin.Text = userName.ToUpper();
            Label lblLogin1 = (Label)Page.Master.FindControl("lblLogin1");
            lblLogin1.Text = userName.ToUpper();
              
            StringBuilder objstr = new StringBuilder();
            objstr.Length = 0;
            if (Session["userid"].ToString() == "" || Session["userid"] == null)
            {

            }
            else
            {
                Int64 adminid = Convert.ToInt64(Session["userid"].ToString());
                DesktopMenu obj = new DesktopMenu();
                objstr = obj.GetMenuData(adminid);
            }

            cssmenu.InnerHtml = objstr.ToString();

        }
        catch { }
        finally {  }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userlocationmap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "User Location Map";
        if (!Page.IsPostBack)
        {
            GetBindMapData();
        }
    }

    public string markersLst
    {
        get
        {
            if (ViewState["markersLst"] != null)
                return Convert.ToString(ViewState["markersLst"]);
            else
                return string.Empty;
        }
        set
        {
            ViewState["markersLst"] = value;
        }
    }

    private void GetBindMapData()
    {
        string query = "select REPLACE(name + ' '+ userloginmobileno+ ' ' + address1 + ' '+ address2 + ' '+ city + ' '+state,'''', '') as name,latitude,longitude,'d' as usertype from dealermaster	Where isdeleted=0 and latitude is not null and longitude is not null UNION ALL Select  REPLACE(fname + ' '+ mname+' '+ lname + ' ' + phone + ' ' + address1 + ' '+ address2,'''', '') as name ,latitude,longitude,'u' as usertype  from userregistration Where isdelete=0 and latitude is not null and longitude is not null ";
        string conString = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                string markers = string.Empty;
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    if (dt.Rows != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                markers = string.Empty;
                                markers = markers + "{\"title\":'" + dt.Rows[i]["name"].ToString() + "'," + Environment.NewLine;
                                markers = markers + "\"lat\": '" + dt.Rows[i]["latitude"].ToString() + "'," + Environment.NewLine;
                                markers = markers + "\"lng\": '" + dt.Rows[i]["longitude"].ToString() + "'," + Environment.NewLine;
                                markers = markers + "\"type\": '" + dt.Rows[i]["usertype"].ToString() + "'" + Environment.NewLine;
                                markers = markers + "}," + Environment.NewLine;
                                markersLst += markers;
                            }
                        }
                    }
                }
            }
        }
    }
}
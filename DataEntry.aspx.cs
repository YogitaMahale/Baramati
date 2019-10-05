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

public partial class DataEntry : System.Web.UI.Page
{
    SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        txtOrderDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Data Entry";
        Page.Title = "Data Entry";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int64 result = 0;
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dataEntry_MoryaFollowup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@PKId";
            param.Value = 0;
            param.SqlDbType = SqlDbType.BigInt;
            param.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(param);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Mobile", txt_mobileno.Text);
            cmd.Parameters.AddWithValue("@City", "");
            cmd.Parameters.AddWithValue("@Remark", "");

            cmd.Parameters.AddWithValue("@CustomerType", "");
            cmd.Parameters.AddWithValue("@Product", txt_product.Text);
            cmd.Parameters.AddWithValue("@EnquiryFrom", txt_enquiryFrom.Text);
            cmd.Parameters.AddWithValue("@date1", DateTime.Parse(txtOrderDate.Text));
            cmd.Parameters.AddWithValue("@agentAssigned", txt_agentAssigned.Text);
            cmd.Parameters.AddWithValue("@comment", txt_comment.Text);



            ConnectionString.Open();
            cmd.ExecuteNonQuery();
            result = Convert.ToInt64(param.Value);
            if (result > 0)
            {
                spnMessgae.InnerHtml = "record Saved";
            }
            else
            {
                spnMessgae.InnerHtml = "record Not Saved";
            }
            txtName.Text = string.Empty;
            txt_mobileno.Text = string.Empty;
            txt_product.Text = string.Empty;
            txt_enquiryFrom.Text = string.Empty;
            txt_agentAssigned.Text = string.Empty;
            txt_comment.Text = string.Empty;

        }
        catch (Exception ex)
        {


        }
        finally
        {
            ConnectionString.Close();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
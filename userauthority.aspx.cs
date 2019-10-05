using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using BusinessLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class userauthority : System.Web.UI.Page
{
    common ocommon = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
         

        HtmlGenericControl hPageTitle = (HtmlGenericControl)this.Page.Master.FindControl("hPageTitle");
        hPageTitle.InnerText = "Set User Authority";
    }

    private void Search()
    {
        spnMessage.Visible = true;
        spnMessage.InnerHtml = string.Empty;
        if (!string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
        {
            string query = string.Empty;
            string mobileNo = string.Empty;
            query = " select did,name, userloginmobileno, whatappno,email,gstno,address1,address2,city,state,isactive,isdeleted,CONVERT(VARCHAR(30), createddate, 120) as createddate,CASE WHEN status = 0 THEN '~/images/offline.ico' WHEN status = 1 THEN '~/images/online.ico' WHEN status IS NULL THEN '~/images/offline.ico' END AS status, CASE WHEN status = 0 THEN 'Offline' WHEN status = 1 THEN 'Live' WHEN status IS NULL THEN 'Offline' END AS livestatus,CASE WHEN usertype = 'superstockiest' THEN 'superstockiest' WHEN usertype = 'dealer' THEN 'dealer'WHEN usertype = 'wholesaler' THEN 'wholesaler' WHEN usertype = 'fakecustomer' THEN 'fakecustomer' ELSE 'dealer' END AS 'usertype' from dealermaster Where isdeleted=0 and userloginmobileno IN ( ";

            string[] splitmobileNo = txtMobileNo.Text.Trim().Split(',');
            for (int i = 0; i < splitmobileNo.Count(); i++)
            {
                mobileNo += "'" + splitmobileNo[i] + "',";
            }

            query = query + mobileNo.TrimEnd(',') + " ) ";
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        divDealer.Visible = true;
                        repDealer.DataSource = dt;
                        repDealer.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                query = string.Empty;
                splitmobileNo = null;
                mobileNo = string.Empty;
                txtMobileNo.Text = string.Empty;
                spnMessage.InnerHtml = ex.StackTrace + " - " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            spnMessage.InnerHtml = "No record found";
        }
    }

    public bool User_Delete(Int64 UserID)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", UserID);
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    public bool User_IsActive(Int64 UserID, Boolean IsActive)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_IsActive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@isactive", IsActive);
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return true;
    }

    public bool User_Authority(Int64 UserID, string UserAuthority)
    {
        SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dealermaster_User_Authority";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = ConnectionString;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@authority", true);
            cmd.Parameters.AddWithValue("@userauthority", UserAuthority);
            ConnectionString.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            ConnectionString.Close();
        }
        return true;
    }

    protected void lnkActiveDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkActiveDelete = (LinkButton)sender;
        RepeaterItem item = (RepeaterItem)lnkActiveDelete.NamingContainer;
        Int64 UserId = Convert.ToInt64(lnkActiveDelete.CommandArgument);
        bool yes = User_Delete(UserId);
        spnMessage.Visible = true;
        if (yes)
        {
            Search();
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Deleted Successfully";
        }
        else
        {
            spnMessage.Style.Add("color", "green");
            spnMessage.InnerText = "User Not Deleted";
        }
    }

    protected void cbIsActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfUserId") as HiddenField).Value);
                bool cbIsActive = Convert.ToBoolean((item.FindControl("cbIsActive") as CheckBox).Checked);
                bool yes = User_IsActive(UserID, cbIsActive);
                spnMessage.Visible = true;
                if (yes)
                {
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "User Updated Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "User Not Updated";
                }
            }
        }
    }

    protected void cbSuperStockiest_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfUserId") as HiddenField).Value);
                bool yes = User_Authority(UserID, "super");
                spnMessage.Visible = true;
                if (yes)
                {
                    Search();
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "Super Stockiest Authority Set Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "Authority Not Updated";
                }
            }
        }
    }

    protected void cbDealer_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfUserId") as HiddenField).Value);
                bool yes = User_Authority(UserID, "dealer");
                spnMessage.Visible = true;
                if (yes)
                {
                    Search();
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "Dealer Authority Set Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "Authority Not Updated";
                }
            }
        }
    }

    protected void cbWholesaler_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfUserId") as HiddenField).Value);
                bool yes = User_Authority(UserID, "wholesaler");
                spnMessage.Visible = true;
                if (yes)
                {
                    Search();
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "Wholesaler Authority Set Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "Authority Not Updated";
                }
            }
        }
    }

    protected void cbCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbIsActiveUser = (CheckBox)sender;
        RepeaterItem item = (RepeaterItem)cbIsActiveUser.NamingContainer;
        if (item != null)
        {
            if (!string.IsNullOrEmpty((item.FindControl("hfUserId") as HiddenField).Value))
            {
                Int64 UserID = int.Parse((item.FindControl("hfUserId") as HiddenField).Value);
                bool yes = User_Authority(UserID, "fakecustomer");
                spnMessage.Visible = true;
                if (yes)
                {
                    Search();
                    spnMessage.Style.Add("color", "green");
                    spnMessage.InnerText = "Customer Authority Set Successfully";
                }
                else
                {
                    spnMessage.Style.Add("color", "red");
                    spnMessage.InnerText = "Authority Not Updated";
                }
            }
        }
    }
}

// Add new column - usertype varchar(30) dealer_SelectAllAdmin ,dealer_SelectAllAdminNotActive

//superstockiest ---- S

//dealer ------ D

//wholesaler ------ W

//fakecustomer ------ C

// User ------------- U

//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dbo].[userdealer_Login]    
//@mobileno NVARCHAR(25) = NULL    
//,@password NVARCHAR(25) = NULL    
//AS    
//BEGIN    

//DECLARE @Type INT = 0    
//DECLARE @TypeUser BIT = 0    
//DECLARE @TypeDealer BIT = 0   

//SELECT @TypeUser = 1    
//FROM userregistration    
//WHERE isdelete = 0    
//--AND isactive = 1    
//AND phone = @mobileno    
//AND lower(password) = lower(@password)   

//SELECT @TypeDealer = 1    
//FROM dealermaster    
//WHERE isdeleted = 0    
//--AND isactive = 1    
//AND userloginmobileno = @mobileno    
//AND lower(password) = lower(@password)   

//IF (@TypeUser = 1)    
//SET @Type = 1    

//ELSE IF (@TypeDealer = 1)    
//SET @Type = 2    

//IF (@Type = 1)    
//SELECT 'user' AS type    
//,uid    
//,fname    
//,mname    
//,lname    
//,email    
//,phone    
//,password    
//,dob    
//,address1    
//,address2    
//,isguest    
//,registrationdate    
//,uguid    
//,isactive
//,latitude
//,longitude  
//FROM userregistration    
//WHERE isdelete = 0    
//--AND isactive = 1    
//AND phone = @mobileno    
//AND lower(password) = lower(@password)   

//ELSE IF (@Type = 2)    
//SELECT 
//CASE 
//WHEN usertype = 'superstockiest' THEN 'superstockiest' 
//WHEN usertype = 'dealer' THEN 'dealer' 
//WHEN usertype = 'wholesaler' THEN 'wholesaler' 
//WHEN usertype = 'fakecustomer' THEN 'fakecustomer' 
//ELSE 'dealer' 
//END AS 'type'  
//,did as uid    
//,name    
//,userloginmobileno    
//,password    
//,whatappno    
//,email    
//,gstno    
//,address1    
//,address2    
//,city    
//,state    
//,guid    
//,isactive  
//,latitude
//,longitude
//FROM dealermaster    
//WHERE isdeleted = 0    
//--AND isactive = 1    
//AND userloginmobileno = @mobileno    
//AND lower(password) = lower(@password)   

//END

//--------------------------------------------------------------------------------------------------------------------------------------------------------------//

//ALTER PROCEDURE [userdealerlogin_update]

//@uid bigint = null,
//@utype nvarchar(5)=null,
//@deviceid nvarchar(500)

//AS
//BEGIN

//IF(@utype='U')
//BEGIN

//Update userregistration SET deviceid=@deviceid WHERE uid=@uid

//END

//ELSE IF(@utype='D' OR @utype='S' OR @utype='W' OR @utype='C')
//BEGIN

//Update dealermaster SET deviceid=@deviceid WHERE did=@uid

//END


//END


//-----------------------------------------------------------------------------------------//


//ALTER PROCEDURE [userdealerlogin_status]

//@uid bigint = null,
//@utype nvarchar(5)=null,
//@status bit

//AS
//BEGIN

//IF(@utype='U')
//BEGIN

//Update userregistration SET status=@status WHERE uid=@uid

//END

//ELSE IF(@utype='D' OR @utype='S' OR @utype='W' OR @utype='C')
//BEGIN

//Update dealermaster SET status=@status WHERE did=@uid

//END

//END


//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dbo].[product_WSSelectAllProductUsingCategoryId]    
//@cid bigint = NULL    
//AS    
//BEGIN    
//Select    
//pid    
//,cid    
//,(select categoryname from category where cid=@cid) as Categoryname    
//,productname    
//,'http://moryaapp.moryatools.com/uploads/product/front/' + mainimage as imagename    
//,sku    
//,customerprice    
//,dealerprice  
//,wholesaleprice	
//,superwholesaleprice  
//,discountprice    
//,quantites    
//,isstock    
//,shortdescp    
//,longdescp    
//,video1    
//,video2    
//,video3    
//,video4    
//,gst
//,wholesaleprice
//,video_name_1
//,video_name_2
//,video_name_3 
//,video_name_4  
//from product pr where isactive=1 and isdelete=0 and cid=@cid  ORDER BY seqno asc  
//END


//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dbo].[product_WSSelectAllProductUsingCategoryId]    
//@cid bigint = NULL    
//AS    
//BEGIN    
//Select    
//pid    
//,cid    
//,(select categoryname from category where cid=@cid) as Categoryname    
//,productname    
//,'http://moryaapp.moryatools.com/uploads/product/front/' + mainimage as imagename    
//,sku    
//,customerprice    
//,dealerprice  
//,wholesaleprice	
//,superwholesaleprice  
//,discountprice    
//,quantites    
//,isstock    
//,shortdescp    
//,longdescp    
//,video1    
//,video2    
//,video3    
//,video4    
//,gst
//,wholesaleprice
//,video_name_1
//,video_name_2
//,video_name_3 
//,video_name_4  
//from product pr where isactive=1 and isdelete=0 and cid=@cid  ORDER BY seqno asc  
//END

//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dbo].[dealermaster_User_Authority]

//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dealer_SelectAllAdminNotActive]       

//-----------------------------------------------------------------------------------------//

//ALTER PROCEDURE [dealer_SelectAllAdmin]        

//-----------------------------------------------------------------------------------------//
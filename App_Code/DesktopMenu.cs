using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BusinessLayer
{
    public class DesktopMenu
    {
        public DesktopMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        Int64 adminID1 = 0;
        string connectionstring = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
        public StringBuilder GetMenuData(Int64 adminID)
        {
            String classname = String.Empty, textcolor = String.Empty ;
            adminID1 = adminID;
            SqlConnection con = new SqlConnection(connectionstring);
            try
            {

                con.Close();
            }
            catch { }
            finally { con.Close(); }
            StringBuilder objstr = new StringBuilder();
            List<Menu> objpmenu = new List<Menu>();
            List<SubMenu> objsmenu = new List<SubMenu>();
            //    List<ChildSubMenu> objcmenu = new List<ChildSubMenu>();
            objpmenu = GetMenu();
            objsmenu = GetSubMenu();
            // objcmenu = GetChildSubMenu();

            objstr.Append("<ul class='sidebar-menu' data-widget='tree'>");
            
            foreach (Menu _pitem in objpmenu)
            {
                switch (_pitem.Id)
                {
                    case 1:
                        classname = "fa fa-dashboard text-aqua";
                        textcolor = "text-red"; 
                        break;
                    case 2:
                        classname = "fa fa-th text-green";
                        textcolor = "text-blue";
                        break;
                    case 3:
                        classname = "fa fa-laptop text-yellow";
                        textcolor = "text-aqua";
                        break;
                    case 4:
                        classname = "fa fa-shopping-cart text-red";
                        textcolor = "text-green";
                        break;
                    case 5:
                        classname = "fa fa-shopping-cart text-orange";
                        textcolor = "text-green";
                        break;
                    case 6:
                        classname = "fa fa-shopping-cart text-maroon";
                        textcolor = "text-green";
                        break;
                    case 7:
                        classname = "fa fa-users text-aqua";
                        textcolor = "text-maroon";
                        break;
                    case 8:
                        classname = "fa fa-share text-green";
                        textcolor = "text-aqua";
                        break;
                    case 9:
                        classname = "fa fa-table text-blue";
                        textcolor = "text-maroon";
                        break;
                    case 10:
                        classname = "fa fa-book text-yellow";
                        textcolor = "text-green";
                        break;
                    case 11:
                        classname = "fa fa-edit text-lime";
                        textcolor = "text-blue";
                        break;
                    case 12:
                        classname = "fa fa-dashboard text-maroon";
                        textcolor = "text-blue";
                        break;
                    default:
                        classname = "fa fa-dashboard";
                        textcolor = "text-red";
                        break;
                }
                objstr.Append("<li class='treeview'><a href='" + _pitem.MenuUrl + "'><i class='"+classname+"'></i> <span>" + _pitem.MenuName + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>");

                var subitem = objsmenu.Where(m => m.ParentId == _pitem.Id).ToList();
                if (subitem.Count > 0)
                {
                    objstr.Append("<ul class='treeview-menu'>");
                    foreach (var _sitem in subitem)
                    {
                        //objstr.Append("<li class='has-sub'><a href='" + _sitem.SubMenuUrl + "'><span>" + _sitem.SubMenuName + "</span></a>");
                        objstr.Append("<li><a href=" + _sitem.SubMenuUrl + "><i class='fa fa-circle-o "+textcolor+"'></i>" + _sitem.SubMenuName + "</a>");

                        //objstr.Append("<li class='has-sub'><a href=saree.aspx?SubMenu=" + _sitem.Id + "&ChildMenu=" + "0" + "><span>" + _sitem.SubMenuName + "</span></a>");

                        //var childsubitem = objcmenu.Where(s => s.SubParentId == _sitem.Id).ToList();
                        //if (childsubitem.Count > 0)
                        //{
                        //    objstr.Append("<ul>");
                        //    foreach (var _citem in childsubitem)
                        //    {
                        //        objstr.Append("<li class='has-sub'><a href=saree.aspx?SubMenu=" + _citem.SubParentId + "&ChildMenu=" + _citem.Id + "><span>" + _citem.ChildSubMenuName + "</span></a></li>");
                        //    }
                        //    objstr.Append("</ul>");
                        //}

                        objstr.Append("</li>");
                    }
                    objstr.Append("</ul>");
                }

                objstr.Append("</li>");
            }

            objstr.Append("</ul>");
            return objstr;
            //cssmenu.InnerHtml = objstr.ToString();
        }
        public List<Menu> GetMenu()
        {
            List<Menu> objmenu = new List<Menu>();
            DataTable _objdt = new DataTable();
            //string OrderLink = Session["Show_orderLink"].ToString();
            //Order Link
            /*
            string querystring = "";

            if (Session["Show_orderLink"] == null || Session["Show_orderLink"].ToString().Trim() == "0")
            {
                querystring = "select * from Menu where Id not in (select Id from Menu where MenuName in('LOGOUT','ORDERS')) order by seqno asc";
            }
            else
            {
                querystring = "select * from Menu where Id not in (select Id from Menu where MenuName in('LOGIN')) order by seqno asc";

            }
             */
            // string querystring = "select * from Menu;";
            //select distinct(ParentId) from SubMenu where Id IN (select pageid from pageauthority where adminid=1)

            

            string querystring = "select * from Menu where Id  in (select distinct(ParentId) from SubMenu where Id IN (select pageid from pageauthority where adminid=" + adminID1 + ")) and isactive = 1  order by seqno asc";
            //string querystring = "select * from Menu where Id  in (select Id from Menu where MenuName Not in('LOGIN','ORDERS','CART','LOGOUT')) order by seqno asc";
            SqlConnection _objcon = new SqlConnection(connectionstring);
            SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
            _objcon.Open();
            _objda.Fill(_objdt);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    objmenu.Add(new Menu { Id = (int)_objdt.Rows[i]["Id"], MenuName = _objdt.Rows[i]["MenuName"].ToString(), MenuUrl = _objdt.Rows[i]["MenuUrl"].ToString() });
                }
            }
            return objmenu;
        }
        public List<SubMenu> GetSubMenu()
        {
            List<SubMenu> objmenu = new List<SubMenu>();
            DataTable _objdt = new DataTable();
            //string querystring = "select * from SubMenu;";
            string querystring = "select * from SubMenu where  Id IN (select pageid from pageauthority where [adminid]=" + adminID1 + ")";
            //
            SqlConnection _objcon = new SqlConnection(connectionstring);
            SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
            _objcon.Open();
            _objda.Fill(_objdt);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    objmenu.Add(new SubMenu { Id = (int)_objdt.Rows[i]["Id"], ParentId = (int)_objdt.Rows[i]["ParentId"], SubMenuName = _objdt.Rows[i]["SubMenuName"].ToString(), SubMenuUrl = _objdt.Rows[i]["SubMenuUrl"].ToString() });
                }
            }
            return objmenu;
        }
        //public List<ChildSubMenu> GetChildSubMenu()
        //{
        //    List<ChildSubMenu> objmenu = new List<ChildSubMenu>();
        //    DataTable _objdt = new DataTable();
        //    string querystring = "select * from ChildSubMenu;";
        //    SqlConnection _objcon = new SqlConnection(connectionstring);
        //    SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
        //    _objcon.Open();
        //    _objda.Fill(_objdt);
        //    if (_objdt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < _objdt.Rows.Count; i++)
        //        {
        //            objmenu.Add(new ChildSubMenu { Id = (int)_objdt.Rows[i]["Id"], SubParentId = (int)_objdt.Rows[i]["SubParentId"], ChildSubMenuName = _objdt.Rows[i]["ChildSubMenuName"].ToString(), ChildSubMenuUrl = _objdt.Rows[i]["ChildSubMenuUrl"].ToString() });
        //        }
        //    }
        //    return objmenu;
        //}
    }
    public class Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
    }
    public class SubMenu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuUrl { get; set; }
    }

}
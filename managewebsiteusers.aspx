<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managewebsiteusers.aspx.cs" Inherits="managewebsiteusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
              
                    <!-- /.box -->

                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                           <div class="pull-right" >
        <asp:Button ID="btnAddEditUser" runat="server" Text="New User" CssClass="btn btn-success" OnClick="btnAddEditUser_Click" />

                    </div>

                        <%--<div style="overflow-x:auto;">--%>
    <table id="tbUser" class="display table table-hover table-striped table-bordered tbDealer">
        <thead>
            <tr class="tableheader">
                <th style="text-align: center">Name
                </th>
                <th style="text-align: center">Login Name
                </th>
               <%--<th style="text-align: center">User Type
                </th>--%>
                <th style="text-align: center">Email Id
                </th>
                <th style="text-align: center">Mobile No.
                </th>
                  <th style="width: 80px; text-align: center">Live Status
                    </th>
                    <th style="width: 80px; text-align: center">User Status
                    </th>
                <th style="text-align: center">Action
                </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repWebSiteUser" runat="server" OnItemDataBound="repWebSiteUser_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblAdminId" runat="server" Visible="false" Text='<%# Eval("adminid") %>'></asp:Label>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                        </td>
                        <%--<td style="text-align: center">
                            <asp:Label ID="lblUsertype" runat="server" Text='<%# Eval("usertype") %>'></asp:Label>
                        </td>--%>
                        <td style="text-align: center">
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("phone") %>'></asp:Label>
                        </td>
                         <td style="text-align: center">
                                <asp:Image ID="imgStatus" runat="server" ImageUrl='<%#Eval("status") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("livestatus") %>'></asp:Label>
                            </td>
                        <td style="text-align: center">
                            <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-sm btn-danger" Text="Delete" OnClientClick="return confirm('Do you want to delete this user?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
<%--</div>--%>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <!-- ./wrapper -->
        </ContentTemplate>
       
    </asp:UpdatePanel>

    <!-- jQuery 3 -->
    <script src="Template/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="Template/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- DataTables -->
    <script src="Template/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="Template/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="Template/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="Template/bower_components/fastclick/lib/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="Template/dist/js/adminlte.min.js"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="Template/dist/js/demo.js"></script>
    <!-- page script -->
    <script>
        $(function () {
            $('#tbUser').DataTable({ "order": [[2, "desc"]] })
            $('#example2').DataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false




            })
        })
</script>
</asp:Content>


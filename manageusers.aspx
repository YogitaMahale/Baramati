<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageusers.aspx.cs" Inherits="manageusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">

                    <!-- /.box -->


                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box">
                    </div>
                    <div class="box">
                        <br />
                        <div style="text-align:right;">
                                 <asp:Button ID="btnExcelExport" runat="server" class="btn btn-flickr" Width="100" Text="Excel Export" OnClick="btnExcelExport_Click" /></td>
                
                                
                            </div>
                        <table>
                            <tr>
                                <%--  <td><label for="exampleInputPassword1">Select</label></td>
                            <td>  <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>--%>
                                <td>
                                    <asp:DropDownList ID="ddlSelectEntry" Visible="false" Class="form-control" Width="80px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectEntry_SelectedIndexChanged">
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <label for="exampleInputPassword1">User Status</label></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlUserstatus" Class="form-control" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserstatus_SelectedIndexChanged">

                                        <asp:ListItem Value="-1">ALL</asp:ListItem>
                                        <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                        <asp:ListItem Value="0">NON ACTIVE</asp:ListItem>

                                    </asp:DropDownList></td>
                                <%--<td> &nbsp;&nbsp; <label for="exampleInputPassword1">    Entries</label></td>--%>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Width="600"></asp:Label></td>
                                <td>
                                    <label for="exampleInputPassword1">Search</label></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" Width="200"></asp:TextBox></td>

                            </tr>

                        </table>






                        <!-- /.box-header -->
                        <div class="box-body">
                            <table class="table table-bordered table-striped">
                                <thead>
                                    <tr>


                                        <th style="width: 200px; text-align: center">User Name
                                        </th>
                                        <th style="width: 80px; text-align: center">Email
                                        </th>
                                        <th style="width: 80px; text-align: center">Phone No.
                                        </th>
                                        <%--<th style="width: 150px; text-align: center">Address
                                        </th>--%>
                                        <th style="width: 150px; text-align: center">Password
                                        </th>
                                        <th style="width: 150px; text-align: center">Reg. Date
                                        </th>
                                        <th style="width: 20px; text-align: center">Guest
                                        </th>
                                        <th style="width: 20px; text-align: center">Active
                                        </th>
                                        <th style="width: 80px; text-align: center">User Status
                                        </th>
                                        <th style="width: 10px; text-align: center">CreatedDate
                                        </th>

                                        <th style="width: 120px; text-align: center">Action
                                        </th>
                                    </tr>

                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repUser" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 200px; text-align: center">
                                                    <asp:HiddenField ID="hfNonActiveUser" runat="server" Value='<%# Eval("uid") %>' />
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("fname") + " " + Eval("mname") + " " + Eval("lname") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("phone") %>'></asp:Label>
                                                </td>
                                                <%--<td style="text-align: center">
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("address1")%>'>></asp:Label>
                                                </td>--%>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("password")%>'>></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("registrationdate") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:CheckBox ID="cbIsGuest" runat="server" Checked='<%#Eval("isguest") %>'></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbIsNotActiveUser" runat="server" AutoPostBack="true" Checked='<%#Eval("isactive") %>' OnCheckedChanged="cbIsNotActiveUser_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("livestatus") %>'></asp:Label>
                                                </td>

                                                 <td>
                                                    <asp:Label ID="lblcreateddate" runat="server" Text='<%# Eval("createddate") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lnkNotActiveUserDelete" runat="server" CssClass="btn btn-sm btn-danger" Text="X" ToolTip="Delete" CommandArgument='<%# Eval("uid") %>' OnClientClick="return confirm('Do you want to delete this user?');" OnClick="lnkNotActiveUserDelete_Click"></asp:LinkButton>
                                                </td>
                                               
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="width: 200px; text-align: center">User Name
                                        </th>
                                        <th style="width: 80px; text-align: center">Email
                                        </th>
                                        <th style="width: 80px; text-align: center">Phone No.
                                        </th>
                                        <th style="width: 150px; text-align: center">Address
                                        </th>
                                        <th style="width: 150px; text-align: center">Reg. Date
                                        </th>
                                        <th style="width: 150px; text-align: center">Is Guest
                                        </th>
                                        <th style="width: 20px; text-align: center">IsActive
                                        </th>
                                        <th style="width: 80px; text-align: center">User Status
                                        </th>
                                        <th style="width: 10px; text-align: center">CreatedDate
                                        </th>

                                        <th style="width: 120px; text-align: center">Action
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <!-- /.box-body -->
                        <div style="margin-top: 20px;">
                            <center>
                    <table style="width: 600px;">
                        <tr>
                            <td>                               
                                <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click">First</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click">Previous</asp:LinkButton>
                            </td>
                            <td>
                                <asp:DataList ID="rptPaging" runat="server"
                                    OnItemCommand="rptPaging_ItemCommand"
                                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPaging" runat="server"
                                            CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage"
                                            Text='<%# Eval("PageText") %> ' Width="20px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click">Next</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbLast" runat="server" OnClick="lbLast_Click">Last</asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                            </center>
                        </div>
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <!-- ./wrapper -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
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
            $('#example1').DataTable()
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

<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="userauthority.aspx.cs" Inherits="userauthority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">


                    <div class="text-center">
                        <span id="spnMessage" runat="server" class="text-bold" style="color: green" visible="false"></span>
                    </div>

                    <div class="box">
                        <br />



                        <div class="form-group row">

                            <div class="col-xs-6">
                                <b>Mobile No </b>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile No"></asp:TextBox>
                            </div>
                            <div class="col-xs-6">
                                <b>&nbsp;</b>
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                            </div>
                        </div>

                        <%--  <div style="text-align: right;">
                            <asp:HyperLink ID="hlUserAuthority" runat="server" class="btn btn-Normal btn-primary" Target="_blank" Text="User Authority" NavigateUrl="~/userauthority.aspx"></asp:HyperLink>

                        </div>--%>

                        <br />

                        <!-- /.box-header -->
                        <div class="box-body" style="overflow-x: auto;" id="divDealer" runat="server" visible="false">
                            <table id="tbDealer" class="table table-bordered table-striped">
                                <thead>
                                    <tr>



                                        <th style="width: 150px; text-align: center">Dealer
                                        </th>
                                        <th style="width: 80px; text-align: center">Login MNo.
                                        </th>
                                        <th style="width: 80px; text-align: center">Whatapp MNo.
                                        </th>
                                        <th style="width: 150px; text-align: center">Address
                                        </th>
                                        <th style="width: 150px; text-align: center">Registration Date
                                        </th>
                                        <th style="width: 80px; text-align: center">User Access
                                        </th>
                                        <th style="width: 80px; text-align: center">Current User Type
                                        </th>
                                        <th style="width: 80px; text-align: center">Super Stockiest
                                        </th>
                                        <th style="width: 80px; text-align: center">Dealer 
                                        </th>
                                        <th style="width: 80px; text-align: center">Wholesaler 
                                        </th>
                                        <th style="width: 80px; text-align: center">Customer(Fake) 
                                        </th>
                                        <th style="width: 80px; text-align: center">Live Status
                                        </th>
                                        <th style="width: 80px; text-align: center">User Status
                                        </th>
                                        <th style="width: 80px; text-align: center">Action
                                        </th>
                                    </tr>

                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repDealer" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:HiddenField ID="hfUserId" runat="server" Value='<%# Eval("did") %>' />
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblLoginNo" runat="server" Text='<%# Eval("userloginmobileno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblWhatappno" runat="server" Text='<%# Eval("whatappno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("address1") + " " + Eval("address2") + ". " + Eval("city") + " ," + Eval("state") %>'>></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblRegistration" runat="server" Text='<%# Eval("createddate") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbIsActive" runat="server" AutoPostBack="true" Checked='<%#Eval("isactive") %>' OnCheckedChanged="cbIsActive_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblCurrentUserAccess" runat="server" Style="font-weight: bold" Text='<%# Eval("usertype").ToString().ToUpper() %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbSuperStockiest" runat="server" AutoPostBack="true" OnCheckedChanged="cbSuperStockiest_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbDealer" runat="server" AutoPostBack="true" OnCheckedChanged="cbDealer_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbWholesaler" runat="server" AutoPostBack="true" OnCheckedChanged="cbWholesaler_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbCustomer" runat="server" AutoPostBack="true" OnCheckedChanged="cbCustomer_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Image ID="imgStatus" runat="server" ImageUrl='<%#Eval("status") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("livestatus") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lnkActiveDelete" runat="server" CssClass="btn btn-sm btn-danger" ToolTip="Delete User" Text="X" CommandArgument='<%# Eval("did") %>' OnClientClick="return confirm('Do you want to delete this dealer?');" OnClick="lnkActiveDelete_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="width: 150px; text-align: center">Dealer
                                        </th>
                                        <th style="width: 80px; text-align: center">Login MNo.
                                        </th>
                                        <th style="width: 80px; text-align: center">Whatapp MNo.
                                        </th>
                                        <th style="width: 150px; text-align: center">Address
                                        </th>
                                        <th style="width: 150px; text-align: center">Registration Date
                                        </th>
                                        <th style="width: 80px; text-align: center">User Access
                                        </th>
                                        <th style="width: 80px; text-align: center">Current User Type
                                        </th>
                                        <th style="width: 80px; text-align: center">Super Stockiest
                                        </th>
                                        <th style="width: 80px; text-align: center">Dealer 
                                        </th>
                                        <th style="width: 80px; text-align: center">Wholesaler 
                                        </th>
                                        <th style="width: 80px; text-align: center">Customer(Fake) 
                                        </th>
                                        <th style="width: 80px; text-align: center">Live Status
                                        </th>
                                        <th style="width: 80px; text-align: center">User Status
                                        </th>
                                        <th style="width: 80px; text-align: center">Action
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <!-- /.box-body -->
                        <%--<div style="margin-top: 20px;">
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
                        </div>--%>
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
            $('#example1').DataTable({ "order": [[4, "desc"]] })
            $('#example2').DataTable({
                //"order": [[6, "desc"]],
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


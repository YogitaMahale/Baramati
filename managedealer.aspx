<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managedealer.aspx.cs" Inherits="managedealer1" %>

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
                     <%--<div class="box">
                        
                         </div>--%>
                    <div class="box box-success">
                         <br />
                            
                            <div style="text-align:right;">
                                 <asp:HyperLink ID="hlUserAuthority" runat="server" class="btn btn-Normal btn-dropbox" Target="_blank" Text="User Authority" NavigateUrl="~/userauthority.aspx"></asp:HyperLink>
                                 <asp:Button ID="btnExcelExport" runat="server" class="btn btn-flickr" Width="100" Text="Excel Export" OnClick="btnExcelExport_Click" /></td>
                                 <asp:Button ID="btnNewDealer" runat="server" class="btn btn-info" Width="100" Text="New Dealer" OnClick="btnNewDealer_Click" /></td>
                <div class="btn-"></div>
                                
                            </div>
                             
                         <br />
                        <table>
                            <tr>
                          <%--  <td><label for="exampleInputPassword1">Select</label></td>
                            <td>  <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>--%>
                            <td> <asp:DropDownList ID="ddlSelectEntry" Visible="false" Class="form-control" Width="80px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectEntry_SelectedIndexChanged">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList></td>
                                     <td><label for="exampleInputPassword1">Dealer Status</label></td>
                            <td>  <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>
                            <td> <asp:DropDownList ID="ddlUserstatus"  Class="form-control" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserstatus_SelectedIndexChanged">
                                
                                <asp:ListItem Value="-1">ALL</asp:ListItem>
                                <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                <asp:ListItem Value="0">NON ACTIVE</asp:ListItem>
                                
                                </asp:DropDownList></td>
                                <td>  <asp:Label ID="Label7" runat="server" Width="10"></asp:Label></td>
                                <td><label for="exampleInputPassword1">Online / Offline</label></td>
                            <td>  <asp:Label ID="Label6" runat="server" Width="10"></asp:Label></td>
                                <td> <asp:DropDownList ID="ddlOnlineStatus"  Class="form-control" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserstatus_SelectedIndexChanged">
                                
                                <asp:ListItem Value="-1">ALL</asp:ListItem>
                                <asp:ListItem Value="1">ONLINE</asp:ListItem>
                                <asp:ListItem Value="0">OFFLINE</asp:ListItem>
                                
                                </asp:DropDownList></td>
                             <%--<td> &nbsp;&nbsp; <label for="exampleInputPassword1">    Entries</label></td>--%>
                            <td>  <asp:Label ID="Label1" runat="server" Width="600"></asp:Label></td>
                            <td><label for="exampleInputPassword1" >Search</label></td>
                                <td>  <asp:Label ID="Label3" runat="server" Width="10"></asp:Label></td>
                            <td><asp:TextBox ID="txtSearch" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" Width="200"></asp:TextBox></td>

                            </tr>
                            
                        </table>
                      


                         
                        
                             
                        <!-- /.box-header -->
                        <div class="box-body" style="overflow:scroll;">
                            <table  class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="width: 10px; text-align: center" class="allCheckboxActive">
                                            <asp:CheckBox ID="cbDealerActive" runat="server" /></th>
                                        <th style="width: 200px; text-align: center">Dealer
                                        </th>
                                        <th style="width: 80px; text-align: center">Login No
                                        </th>
                                        <th style="width: 80px; text-align: center">Password
                                        </th>
                                        <th style="width: 80px; text-align: center">WhatsApp No
                                        </th>
                                        <th style="width: 80px; text-align: center">GST
                                        </th>
                                        <th style="width: 80px; text-align: center">Aadhar
                                        </th>
                                        <th style="width: 80px; text-align: center">PAN
                                        </th>
                                        <%--<th style="width: 80px; text-align: center">Login Count
                                        </th>--%>
                                        <%--<th style="width: 150px; text-align: center">Address
                                        </th>--%>
                                        <th style="width: 150px; text-align: center">State City
                                        </th>
                                        <th style="width: 150px; text-align: center">Regn. Date
                                        </th>
                                        <th style="width: 20px; text-align: center">Access
                                        </th>
                                        <th style="width: 80px; text-align: center">User Type
                                        </th>
                                        <th style="width: 10px; text-align: center">Status
                                        </th>
                                        <th style="width: 150px; text-align: center">Agent
                                        </th>
                                        <th style="width: 80px; text-align: center;display:none;">User Status
                                        </th>
                                        <th style="width:120px; text-align: center">Action
                                        </th>
                                    </tr>
                                   
                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repDealerActive" runat="server" OnItemDataBound="repDealerActive_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="singleCheckboxActive" style="text-align: center">
                                                    <asp:CheckBox ID="chkContainerActive" runat="server" CssClass="Container" attr-ID='<%# Eval("did") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:HiddenField ID="hfActiveUserId" runat="server" Value='<%# Eval("did") %>' />
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblLoginNo" runat="server" Text='<%# Eval("userloginmobileno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("password") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblWhatappno" runat="server" Text='<%# Eval("whatappno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("gstno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("aadharno") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("panno") %>'></asp:Label>
                                                </td>
                                                <%--<td style="text-align: center">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("logincount") %>'></asp:Label>
                                                </td>--%>
                                                <%--<td style="text-align: center">
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("address1") %>'>></asp:Label>
                                                </td>--%>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblCityState" runat="server" Text='<%# Eval("city") + " , " + Eval("state") %>'>></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblRegistration" runat="server" Text='<%# Eval("createddate") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbIsActiveUser" runat="server" AutoPostBack="true" Checked='<%#Eval("isactive") %>' OnCheckedChanged="cbIsActiveUser_CheckedChanged"></asp:CheckBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <div>
                                                        <asp:Label ID="lblCurrentUserAccess" runat="server" Style="font-weight: bold" Text='<%# Eval("usertype").ToString().ToUpper() %>'></asp:Label>
                                                    </div>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Image ID="imgStatus" runat="server" ImageUrl='<%#Eval("status") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblAgentName" runat="server" Text='<%# Eval("agentname") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center;display:none;">
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("livestatus") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:HyperLink ID="hlEdit" CssClass="btn btn-sm btn-success" runat="server"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></asp:HyperLink>
                                                    <asp:LinkButton ID="lnkActiveUserDelete" runat="server" CssClass="btn btn-sm btn-danger" ToolTip="Delete User" Text="X" CommandArgument='<%# Eval("did") %>' OnClientClick="return confirm('Do you want to delete this dealer?');" OnClick="lnkActiveUserDelete_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                           
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="width: 10px; text-align: center" class="allCheckboxActive">
                                            <%--<asp:CheckBox ID="CheckBox1" runat="server" />--%></th>
                                        <th style="width: 200px; text-align: center">Dealer
                                        </th>
                                        <th style="width: 80px; text-align: center">Login No.
                                        </th>
                                        <th style="width: 80px; text-align: center">Password
                                        </th>
                                        <th style="width: 80px; text-align: center">WhatsApp No.
                                        </th>
                                        <th style="width: 80px; text-align: center">GST
                                        </th>
                                        <th style="width: 80px; text-align: center">Aadhar
                                        </th>
                                        <th style="width: 80px; text-align: center">PAN
                                        </th>
                                        <%--<th style="width: 80px; text-align: center">Login Count
                                        </th>--%>
                                        <%--<th style="width: 150px; text-align: center">Address
                                        </th>--%>
                                        <th style="width: 150px; text-align: center">State City
                                        </th>
                                        <th style="width: 150px; text-align: center">Regn. Date
                                        </th>
                                        <th style="width: 20px; text-align: center">Access
                                        </th>
                                        <th style="width: 80px; text-align: center">User Type
                                        </th>
                                        <th style="width: 10px; text-align: center">Status
                                        </th>
                                        <th style="width: 150px; text-align: center">Agent
                                        </th>
                                        <th style="width: 80px; text-align: center;display:none;">User Status
                                        </th>
                                        <th style="width:120px; text-align: center">Action
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


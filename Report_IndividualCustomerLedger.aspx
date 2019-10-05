<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report_IndividualCustomerLedger.aspx.cs" Inherits="Report_IndividualCustomerLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">

                    <div class="box">
                        <br />


                        <br />
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                
                                <div class="col-md-12" style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td><asp:Button ID="btnExcelExport" runat="server" class="btn btn-Normal btn-primary" Width="100" Text="Excel Export" OnClick="btnExcelExport_Click" /></td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text=" " Width="1"></asp:Label></td>
                                            <td><asp:Button ID="btnpdf" runat="server" class="btn btn-Normal btn-primary" Width="100" Text="PDF Report" OnClick="btnpdf_Click" /></td>
                                            <td><asp:Button ID="btnMail" runat="server" class="btn btn-Normal btn-primary" Width="100" Text="Send Mail" OnClick="SendMail" /></td>
                                            <td>
                                                <label for="exampleInputPassword1">Select Type</label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="" Width="1"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlUserType" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" Width="100px" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="D">Dealer</asp:ListItem>
                                                    <asp:ListItem Value="U">User</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                              <td>
                                                <label for="exampleInputPassword1">Select</label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="" Width="1"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlUserName" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" Width="300px" runat="server">
                                                    
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                
                                                <asp:Label ID="Label4" runat="server" Text=":" Width="5"></asp:Label></td>
                                            <td>
                                                <label for="exampleInputPassword1"></label></td>
                                            <td>
                                                 <asp:Label ID="lblMailId" runat="server" Text=""  ></asp:Label></td>
                                                <asp:Label ID="Label3" runat="server" Width="10"></asp:Label></td>
                                            <td>


                                                <asp:TextBox ID="txttsearch" runat="server" class="form-control" Visible="false" AutoPostBack="true" OnTextChanged="txttsearch_TextChanged" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>

                            <br />
                            <table id="example12" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="text-align: center">Sr No</th>
                                        <th style="text-align: center">Invoice No</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">Total Amount</th>
                                        <th style="text-align: center">Paid Amount</th>
                                        <th style="text-align: center">Balance Amount</th>
                                                                

                                    </tr>
                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repIndividualCustomerLedger" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("rowId") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("oid") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("orderdate") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("totalamount") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("paid") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("remain") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="text-align: center"></th>
                                        <th style="text-align: center"></th>
                                        <th style="text-align: center">Total </th>
                                        <th style="text-align: center">
                                            <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label></th>
                                        <th style="text-align: center">
                                            <asp:Label ID="lblPaid" runat="server" Text=""></asp:Label></th>
                                        <th style="text-align: center">
                                            <asp:Label ID="lblRemaining" runat="server" Text=""></asp:Label></th>
                                        <%-- <th style="text-align: center">Total Amount</th>
                                        <th style="text-align: center">Paid Amount</th>
                                        <th style="text-align: center">Balance Amount</th>--%>
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
                </div>
                <!-- /.box -->
            </div>
            <!-- /.col -->

            <!-- /.row -->
            <!-- ./wrapper -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnpdf" />
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


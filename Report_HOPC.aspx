<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report_HOPC.aspx.cs" Inherits="Report_HOPC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                            <%--<div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">Vendor Name </label>
                        <asp:DropDownList ID="ddlMonth" class="mdl-textfield__input" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                    <asp:ListItem Text="ALL" Value="0" />
                                                    <asp:ListItem Text ="January" Value="1"/>
                                                    <asp:ListItem Text="February" Value="2" />
                                                    <asp:ListItem Text="March" Value="3"/>
                                                    <asp:ListItem Text="April" Value ="4" />
                                                    <asp:ListItem Text ="May" Value="5" />
                                                    <asp:ListItem Text ="June" Value ="6" />
                                                    <asp:ListItem Text="July" Value="7" />
                                                    <asp:ListItem Text="August" Value="8" />
                                                    <asp:ListItem Text="September" Value="9" />
                                                    <asp:ListItem Text ="October" Value="10" />
                                                    <asp:ListItem Text="November" Value="11" />
                                                    <asp:ListItem Text="December" Value="12" />

                        </asp:DropDownList>
                        
                    </div>
                    </div>--%>
                            <table id="tblCustomers" class="display table table-hover table-striped table-bordered">
                                <thead>
                                    <tr class="tableheader">
                                        <%--<th style="text-align: center">PO No</th>--%>
                                        <th style="text-align: center">Client Name</th>
                                        <th style="text-align: center">Count</th>
                                        <%--<th style="text-align: center">Date</th>--%>
                                       <%-- <th style="text-align: center">Brand</th>
                                        <th style="text-align: center">Size</th>
                                        <th style="text-align: center">Quantity</th>--%>
                                        <%--<th style="text-align: center">Action</th>--%>
                                        

                                       
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repCategory" runat="server" OnItemDataBound="repCategory_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td class="singleCheckbox" style="text-align: center">
                                <asp:DropDownList ID="ddlSeqNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged" />
                            </td>--%>
                                                <%--<td style="text-align: center">
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Visible ="false" Text='<%# Eval("PurchaseOrderId") %>'></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" Visible ="false" Text='<%# Eval("orderstatus") %>'></asp:Label>
                                                </td>--%>
                                                <td style="text-align: center">

                                                    <%--<asp:Label ID="lblProductCount" runat="server" Visible="false" Text='<%# Eval("productcount") %>'></asp:Label>--%>
                                                    <%--<asp:Label ID="lblSeqNo" runat="server" Visible="false" Text='<%# Eval("seqno") %>'></asp:Label>--%>
                                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Eval("ordercount") %>'></asp:Label>
                                                </td>
                                                <%--<td style="text-align: center">

                                                    <%--<asp:Label ID="lblProductCount" runat="server" Visible="false" Text='<%# Eval("productcount") %>'></asp:Label>--%>
                                                    <%--<asp:Label ID="lblSeqNo" runat="server" Visible="false" Text='<%# Eval("seqno") %>'></asp:Label>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProdName") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblbrand" runat="server" Text='<%# Eval("Brand") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                </td>--%>
                                                <%--<td style="text-align: center">
                                                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" CommandArgument ='<%#Eval("PurchaseOrderId") %>' CssClass="btn btn-sm btn-danger"  Text="Closed"  ></asp:Button>
                                         
                                                </td>--%>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="tableheader">
                                        <%--<th style="text-align: center">PO No</th>--%>
                                        <th style="text-align: center">Client Name</th>
                                        <th style="text-align: center">Count</th>
                                       <%-- <th style="text-align: center">Brand</th>
                                        <th style="text-align: center">Size</th>
                                        <th style="text-align: center">Quantity</th>--%>
                                        <%--<th style="text-align: center">Action</th>--%>
                                        

                                       
                                    </tr>

                                </tfoot>
                            </table>
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
            $('#tblCustomers').DataTable({ "order": [[1, "desc"]] })
            //$('#example2').DataTable({ 
            //    'paging': true,
            //    'lengthChange': false,
            //    'searching': false,
            //    'ordering': true,
            //    'info': true,
            //    'autoWidth': false




            //})
        })
</script>

</asp:Content>


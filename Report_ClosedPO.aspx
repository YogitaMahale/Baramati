<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report_ClosedPO.aspx.cs" Inherits="Report_ClosedPO" %>

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
                            <table id="tblCategory" class="display table table-hover table-striped table-bordered">
                                <thead>
                                    <tr class="tableheader">
                                        <th style="text-align: center">PO No</th>
                                        <th style="text-align: center">Vendor Name</th>
                                        <th style="text-align: center">PO Date</th>
                                        <th style="text-align: center">GRN Date</th>
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
                                                <td style="text-align: center">
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                                                    <asp:Label ID="lblid" runat="server" Visible ="false" Text='<%# Eval("PurchaseOrderId") %>'></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" Visible ="false" Text='<%# Eval("orderstatus") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">

                                                    <%--<asp:Label ID="lblProductCount" runat="server" Visible="false" Text='<%# Eval("productcount") %>'></asp:Label>--%>
                                                    <%--<asp:Label ID="lblSeqNo" runat="server" Visible="false" Text='<%# Eval("seqno") %>'></asp:Label>--%>
                                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("OrderDate1") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("grndate") %>'></asp:Label>
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
                                                <%--<td style="text-align: center">--%>
                                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Style="text-decoration: underline" CssClass="btn btn-sm btn-success" Text="View"></asp:HyperLink>   CommandArgument='<%# Eval("PurchaseOrderId") %>' --%>
                                                    <%--<td class="center"><asp:Button ID="Button2" runat="server" OnClick ="Remove_Product" CommandArgument='<%# Eval("SrNo") %>' Text="REMOVE" /></td>--%>
                                                     <%--<asp:Button ID="btnView" runat="server" OnClick="btnView_Click" CommandArgument ='<%#Eval("PurchaseOrderId") %>' CssClass="btn btn-sm btn-danger"  Text="Closed"  ></asp:Button>--%>
                                         <%--&nbsp;&nbsp;<asp:Button ID="btnGRN" runat="server" OnClick ="btnGRN_Click" Text="GRN (STOCK IN)" CommandArgument ='<%#Eval("PurchaseOrderId") %>' CssClass="btn btn-sm btn-success" ></asp:Button> >
                                         &nbsp;&nbsp;<asp:Button ID="btnDownload" runat="server" OnClick ="btnDownload_Click" Text="Download PDF" CommandArgument ='<%#Eval("PurchaseOrderId") %>' CssClass="btn btn-sm btn-success" ></asp:Button>--%>
                                                <%--</td>--%>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="tableheader">
                                        <th style="text-align: center">PO No</th>
                                        <th style="text-align: center">Vendor Name</th>
                                        <th style="text-align: center">PO Date</th>
                                        <th style="text-align: center">GRN Date</th>
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
            $('#tblOrders').DataTable({ "order": [[2, "desc"]] })
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


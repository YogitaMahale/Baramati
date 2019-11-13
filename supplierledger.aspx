<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="supplierledger.aspx.cs" Inherits="supplierledger" %>

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

                            <div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">Supplier Name </label>
                        <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"  />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlVendor"  ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>

                            </div>

                            <div class="col-xs-6">
                            <table id="tblpopayments" class="display table table-hover table-striped table-bordered">
                                <thead>
                                    <tr class="tableheader">
                                        <th style="text-align: center">Payment Id</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">Type</th>
                                        <th style="text-align: center">Amount(Rs.)</th>
                                        <%--<th style="text-align: center">Date</th>--%>
                                       <%-- <th style="text-align: center">Brand</th>
                                        <th style="text-align: center">Size</th>
                                        <th style="text-align: center">Quantity</th>--%>
                                        <%--<th style="text-align: center">Action</th>--%>
                                        

                                       
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repCategory" runat="server" >
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
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("createddate") %>'></asp:Label>
                                                    
                                                </td>
                                                
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("paymentType") %>'></asp:Label>
                                                    
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="tableheader">
                                        <th style="text-align: center">Payment Id</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">Type</th>
                                        <th style="text-align: center">Amount(Rs.)</th>
                                        

                                       
                                    </tr>

                                </tfoot>
                            </table>
                                </div>
                            <div class="col-xs-6">
                            <table id="tblpodetails" class="display table table-hover table-striped table-bordered">
                                <thead>
                                    <tr class="tableheader">
                                        <th style="text-align: center">PO No</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">Total Amount(Rs.)</th>
                                        <th style="text-align: center">Payment Due(Rs.)</th>
                                        
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="reppodetails" runat="server" OnItemDataBound="reppodetails_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("orderdate") %>'></asp:Label>
                                                    
                                                </td>
                                                
                                                <td style="text-align: center">
                                                    <asp:Label ID="lbltotalamount" runat="server" Text='<%# Eval("totalamount") %>'></asp:Label>
                                                    
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblpendingamount" runat="server" Text='<%# Eval("pendingamount") %>'></asp:Label>
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr class="tableheader">
                                        <%--<th style="text-align: center" >PO No</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">Total Amount(Rs.)</th>
                                        <th style="text-align: center">Payment Due(Rs.)</th>
                                        --%>
                                        <td style="text-align: center">
                                                    <asp:Label ID="lblid" Font-Bold="true" runat="server" Text='TOTALS'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    
                                                </td>
                                                
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblgrandtotal" Font-Bold="true" runat="server" ></asp:Label>
                                                    
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblgranddue" Font-Bold="true" runat="server" ></asp:Label>
                                                    
                                                </td>
                                       
                                    </tr>

                                </tfoot>
                            </table>
                                <%--<div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">Supplier Name </label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"  />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlVendor"  ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>

                            </div>--%>
                                <table>
                                
                                <tbody>
                                            <tr>
                                                
                                            </tr>
                                    
                                </tbody>
                                
                            </table>


                                </div>

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

        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDataTables();


        }

        $(document).ready(function () {

            //initDataTables();

        });


        function initDataTables() {
            //$('#tblpopayments').DataTable({ "order": [[1, "desc"]] })
            //$('#tblpodetails').DataTable({ "order": [[2, "desc"]] })

            
        }


        //$(function () {
            //$('#tblpopayments').DataTable({ "order": [[1, "desc"]] })
            //$('#example2').DataTable({  
            //    'paging': true,
            //    'lengthChange': false,
            //    'searching': false,
            //    'ordering': true,
            //    'info': true,
            //    'autoWidth': false




            //})

            //$('#tblpodetails').DataTable({ "order": [[1, "desc"]] })

        //})
</script>

</asp:Content>


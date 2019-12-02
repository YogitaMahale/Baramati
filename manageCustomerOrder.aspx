<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageCustomerOrder.aspx.cs" Inherits="manageCustomerOrder" %>

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
                    <div class="text-center">
                        <h4><b id="B1" visible="false" style="color: green" runat="server"></b></h4>
                    </div>
                    <div class="box box-success">

                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="text-right">
                                <asp:Button ID="btnNewOrder" runat="server" Text="Add Order" CssClass="btn btn-success" OnClick="btnNewOrder_Click" />
                            </div>


                            <div class="text-center">
                                <h3 style="color: red"><b>Todays Order</b></h3>
                            </div>
                            <br />
                            <div style="text-align: center">
                                <asp:LinkButton ID="btnTodayOrder" runat="server" Text="Save" Visible="false" CssClass="btn btn-danger" OnClick="btnTodayOrder_Click" />
                            </div>
                            <br />
                            <div style="overflow-x: auto;">
                                <div style="overflow-x: auto;">
                                    <table id="tblTodayOrder" class="display table table-hover table-striped table-bordered order">
                                        <thead>
                                            <tr class="tableheader">

                                                <th style="text-align: center">OrderId</th>
                                                <th style="text-align: center">Order NO</th>
                                                <th style="text-align: center">Name</th>
                                                <th style="text-align: center">Amount</th>
                                                <th style="text-align: center">Date</th>
                                                <th style="text-align: center">Is Confirmed Order </th>
                                                <th style="text-align: center">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repTodayOrder" runat="server" OnItemDataBound="repTodayOrder_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblorderid" runat="server" CssClass="Container" Text='<%# Eval("oid") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label1" runat="server" CssClass="Container" Text='<%# Eval("orderno") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label2" runat="server" CssClass="Container" Text='<%# Eval("dealerName") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Container" Text='<%# Eval("grandTotal") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("orderdate") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="isconfirmed" runat="server"   Checked='<%# Eval("isconfirmed") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:HyperLink ID="hlInvoice" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Details"></asp:HyperLink>&nbsp;
                                    
                                                            <asp:HyperLink ID="hlEditOrder" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Edit"></asp:HyperLink>&nbsp;
                                   
                                                            <asp:LinkButton ID="lnkTaxInvoices" runat="server" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" CommandArgument='<%# Eval(" oid")+";"+Eval("orderdate") %>' OnClick="lnkTaxInvoices_Click">Tax Invoice</asp:LinkButton>&nbsp;
                                                           
                                                            &nbsp;<asp:LinkButton ID="lnkTodayDelete" runat="server" Text="Cancel" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this order?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="text-center">
                                <h3 style="color: green"><b>Yesterday Order</b></h3>
                            </div>
                            <br />
                            <div style="text-align: center">
                                <asp:LinkButton ID="btnYesterdayOrder" runat="server" Text="Save" Visible="false" CssClass="btn btn-warning" OnClick="btnYesterdayOrder_Click" />
                            </div>
                            <br />
                            <div style="overflow-x: auto;">
                                <div style="overflow-x: auto;">
                                    <table id="tblYesterDayOrder" class="display table table-hover table-striped table-bordered order">
                                        <thead>
                                            <tr class="tableheader">
                                                <th style="text-align: center">OrderId</th>
                                                <th style="text-align: center">Order NO</th>
                                                <th style="text-align: center">Name</th>
                                                <th style="text-align: center">Amount</th>
                                                 <th style="text-align: center">Date</th>
                                                <th style="text-align: center">Is Confirmed Order </th>
                                                <th style="text-align: center">Action</th>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repYesterDayOrder" runat="server" OnItemDataBound="repYesterDayOrder_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblorderid" runat="server" CssClass="Container" Text='<%# Eval("oid") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label1" runat="server" CssClass="Container" Text='<%# Eval("orderno") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label2" runat="server" CssClass="Container" Text='<%# Eval("dealerName") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Container" Text='<%# Eval("grandTotal") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("orderdate") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="isconfirmed" runat="server"   Checked='<%# Eval("isconfirmed") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:HyperLink ID="hlInvoice" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Details"></asp:HyperLink>&nbsp;
                                    
                                                            <asp:HyperLink ID="hlEditOrder" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Edit"></asp:HyperLink>&nbsp;
                                                                      <asp:LinkButton ID="lnkTaxInvoices" runat="server" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" CommandArgument='<%# Eval(" oid")+";"+Eval("orderdate") %>' OnClick="lnkTaxInvoices_Click">Tax Invoice</asp:LinkButton>&nbsp;
                                                           
                                                            &nbsp;<asp:LinkButton ID="lnkTodayDelete" runat="server" Text="Cancel" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this order?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="text-center">
                                <h3 style="color: blueviolet"><b>Remaining Order</b></h3>
                            </div>
                            <br />
                            <div style="text-align: center">
                                <asp:LinkButton ID="btnRemainingOrder" runat="server" Text="Save" Visible="false" CssClass="btn btn-success" OnClick="btnRemainingOrder_Click" />
                            </div>
                            <br />
                            <div style="overflow-x: auto;">
                                <div style="overflow-x: auto;">
                                    <table id="tblRemainingOrder" class="display table table-hover table-striped table-bordered order">
                                        <thead>
                                            <tr class="tableheader">
                                                <th style="text-align: center">OrderId</th>
                                                <th style="text-align: center">Order NO</th>
                                                <th style="text-align: center">Name</th>
                                                <th style="text-align: center">Amount</th>
                                                <th style="text-align: center">Date</th>
                                                <th style="text-align: center">Is Confirmed Order </th>
                                                <th style="text-align: center">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="repRemaining" runat="server" OnItemDataBound="repRemaining_ItemDataBound">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblorderid" runat="server" CssClass="Container" Text='<%# Eval("oid") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label1" runat="server" CssClass="Container" Text='<%# Eval("orderno") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label2" runat="server" CssClass="Container" Text='<%# Eval("dealerName") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Container" Text='<%# Eval("grandTotal") %>' />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("orderdate") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox ID="isconfirmed" runat="server"   Checked='<%# Eval("isconfirmed") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:HyperLink ID="hlInvoice" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Details"></asp:HyperLink>&nbsp;
                                    
                                                            <asp:HyperLink ID="hlEditOrder" runat="server" Target="_blank" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Edit"></asp:HyperLink>&nbsp;
                                   <asp:LinkButton ID="lnkTaxInvoices" runat="server" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" CommandArgument='<%# Eval(" oid")+";"+Eval("orderdate") %>' OnClick="lnkTaxInvoices_Click">Tax Invoice</asp:LinkButton>&nbsp;
                                                           
                                                            &nbsp;<asp:LinkButton ID="lnkTodayDelete" runat="server" Text="Cancel" CommandArgument='<%# Eval("oid") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this order?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div id="myModal" class="modal fade" role="dialog">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Dealer Uploaded Image</h4>
                                        </div>
                                        <div class="modal-body">
                                            <img id="mimg" width="500" height="500" />
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
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
        $(function () {
            $('#tblTodayOrder').DataTable({
                "destroy": true,
                "order": [[2, "desc"]],
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false
            })
            $('#tblYesterDayOrder').DataTable({
                "destroy": true,
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false




            })
            $('#tblRemainingOrder').DataTable({
                "destroy": true,
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false




            })
        })
    </script>


    <script>
        //$(document).ready(function () {
        //    $('.order').DataTable({
        //        "bLengthChange": true,
        //        "iDisplayLength": 100,
        //        "bFilter": true,
        //        "bInfo": true,
        //        "targets": 'no-sort',
        //        "orderable": false,
        //        "order": [],
        //        dom: 'Bfrtip',
        //        buttons: [
        //            'excelHtml5',
        //        ]
        //    });
        //});

        $('.img').on('click', function () {
            var sr = $(this).attr('src');
            if (sr.length > 50) {
                $('#mimg').attr('src', sr);
                $('#myModal').modal('show');
            }
        });


        $(function () {

            var $allCheckboxTodayOrder = $('.allCheckboxTodayOrder :checkbox');
            var $TodayOrder = $('.TodayOrder :checkbox');

            $allCheckboxTodayOrder.change(function () {
                if ($allCheckboxTodayOrder.is(':checked')) {
                    $TodayOrder.prop('checked', 'checked');
                }
                else {
                    $TodayOrder.removeAttr('checked');
                }
            });

            $TodayOrder.change(function () {
                if ($TodayOrder.not(':checked').length) {
                    $allCheckboxTodayOrder.removeProp('checked');
                }
                else {
                    $allCheckboxTodayOrder.prop('checked', 'checked');
                }
            });


            var $allCheckboxYesterDayOrder = $('.allCheckboxYesterDayOrder :checkbox');
            var $YesterDayOrder = $('.YesterDayOrder :checkbox');

            $allCheckboxYesterDayOrder.change(function () {
                if ($allCheckboxYesterDayOrder.is(':checked')) {
                    $YesterDayOrder.prop('checked', 'checked');
                }
                else {
                    $YesterDayOrder.removeAttr('checked');
                }
            });

            $YesterDayOrder.change(function () {
                if ($YesterDayOrder.not(':checked').length) {
                    $allCheckboxYesterDayOrder.removeProp('checked');
                }
                else {
                    $allCheckboxYesterDayOrder.prop('checked', 'checked');
                }
            });


            var $allCheckboxRemainingOrder = $('.allCheckboxRemainingOrder :checkbox');
            var $RemainingOrder = $('.RemainingOrder :checkbox');

            $allCheckboxRemainingOrder.change(function () {
                if ($allCheckboxRemainingOrder.is(':checked')) {
                    $RemainingOrder.prop('checked', 'checked');
                }
                else {
                    $RemainingOrder.removeAttr('checked');
                }
            });

            $RemainingOrder.change(function () {
                if ($RemainingOrder.not(':checked').length) {
                    $allCheckboxRemainingOrder.removeProp('checked');
                }
                else {
                    $allCheckboxRemainingOrder.prop('checked', 'checked');
                }
            });

        });


    </script>

</asp:Content>


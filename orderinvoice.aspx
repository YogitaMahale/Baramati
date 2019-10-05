<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="orderinvoice.aspx.cs" Inherits="orderinvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">
                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                           
                           <%-- <div class="hold-transition skin-blue sidebar-mini">
        <div class="">
            <div class="content-wrapper">--%>
                <section >
                    <h1>Invoice
                    Order No - #<span id="sminvoiceNo" runat="server" class="text-center"></span>
                    </h1>
                </section>

                <section class="invoice">

                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="page-header">
                                <i class="fa fa-globe"></i>Morya Tools.
                                <small id="smOrderDate" runat="server" class="pull-right"></small>
                            </h2>
                        </div>
                    </div>

                    <div class="row invoice-info">
                        <div class="col-sm-4 invoice-col">
                            From
                              <address>
                                  <strong>Morya Tools.</strong><br />
                                  <span id="spnFormAddress" runat="server">22, Pradhan Park,M.G Road,
                                   Nashik, Maharashtra, India 422001</span><br />
                                  Phone: <span id="spnFromPhone" runat="server">(0253) 3014578</span><br />
                                  Email: <span id="spnFromEmail" runat="server">kshatriya.enterprises@gmail.com</span>
                              </address>
                        </div>
                        <div class="col-sm-4 invoice-col">
                            To
                            <address>
                                <strong id="strCustomerName"><span id="spnName" runat="server"></span></strong>
                                <br />
                                <strong id="strGSTNo" runat="server" visible="false">GST No -<span id="spnGST" runat="server"></span></strong>
                                <br />
                                <span id="spnToAddress" runat="server"></span>
                                <br />
                                Phone: <span id="spnToPhone" runat="server"></span>
                                <br />
                                Email: <span id="spnToEmail" runat="server"></span>
                            </address>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="text-align: center">Product Name</th>
                                        <th style="text-align: center">SKU</th>
                                        <th style="text-align: center">Product Price</th>
                                        <th style="text-align: center">Quantites</th>
                                        <th style="text-align: center">GST</th>
                                        <th style="text-align: center">Product Basic Price</th>
                                        <th style="text-align: center">Product Basic Amount</th>
                                        <th style="text-align: center">Product Total Price</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repOrderProducts" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("productname") %>'></asp:Label></td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblSKU" runat="server" Text='<%# Eval("sku") %>'></asp:Label></td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("productprice") %>'></asp:Label></td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("quantites") %>'></asp:Label></td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblGST" runat="server" Text='<%# Eval("gst") %>'></asp:Label></td>
                                                 <td style="text-align:center">
                                                    <asp:Label ID="lblProductBasicPrice" runat="server" Text='<%# Eval("ProductBasicPrice") %>'></asp:Label></td>
                                                 <td style="text-align:center">
                                                    <asp:Label ID="lblProductAfterDiscountPrice" runat="server" Text='<%# Eval("ProductBasicAmt") %>'></asp:Label></td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("producttotalprice") %>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-xs-6">
                            <button class="btn btn-default" onclick="window.print();"><i class="fa fa-print"></i>&nbsp;&nbsp;Print</button>
                            &nbsp;
                        </div>
                        <div class="col-xs-6">
                            <div class="table-responsive">
                                <table class="table table-bordered">
                                    <tr style="display: none">
                                        <th style="width: 50%">Subtotal:</th>
                                        <td>&#8377 &nbsp;<span id="spnSubTotal" runat="server"></span></td>
                                    </tr>
                                    <tr style="display: none">
                                        <th>GST (18.0%)</th>
                                        <td>&#8377 &nbsp;<span id="spnTax" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <th>Total:</th>
                                        <td>&#8377 &nbsp;<span id="spnTotal" runat="server"></span></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </section>

                <%--<div class="clearfix"></div>
            </div>
            <div class="control-sidebar-bg"></div>
        </div>
    </div>--%>

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
            $('#tblCategory').DataTable()
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


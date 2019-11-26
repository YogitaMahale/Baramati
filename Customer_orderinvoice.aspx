<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Customer_orderinvoice.aspx.cs" Inherits="Customer_orderinvoice" %>

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
                                  
                                <br />
                                   
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
                            Vendor Details
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
                        <div class="col-sm-4 invoice-col">
                            To
                            <address>
                                 Invoice No: <strong id="sporder2" runat="server"></strong>
                               
                             <%--   <br />
                                Delivery Details :<span id="spdeliveryDetails" runat="server"></span>
                                <br />
                                Reference by : <span id="spReferenceby" runat="server"></span>
                                <br />
                              Delivered Through :  <span id="spdeliverth" runat="server" > </span>   --%>
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
                                        <th>sr</th>
                                                <td>Product</td>
                                                <td>Quantity</td>
                                                <td>Rate</td>
                                                <td>SubTotal</td>
                                                <td>Discount</td>
                                                <td>Scheme</td>
                                                <td>Frieght Amount</td>
                                                <td>Taxable Amt</td>
                                                <td>CGST</td>
                                                <td>SGST</td>
                                                <td>IGST</td>
                                                <td>GST amt</td>
                                                <td>Total</td>
                                                <td>Net Rate</td>
                                               
                                            
                                    </tr>
                                  
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repOrderProducts" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <tr class="odd gradeX">
                                                      <td class="center">
                                                            <asp:Label ID="txtsr" runat="server" Text='<%# Eval("sr") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="txtsr" ReadOnly="true" Width="20" runat="server" Text=' <%#Eval("sr")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtproductName" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                                                            <asp:Label ID="rep_txtproductid" Visible="false"  runat="server" Text='<%# Eval("pid") %>'></asp:Label>

                                                        </td>

                                                        <td>
                                                            <asp:Label  ID="rep_txtQty" Text='<%# Eval("qty") %>' AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged" Width="70" runat="server" Enabled="true" placeholder="Qty"></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtRate" Text='<%# Eval("rate") %>' Width="70" runat="server" Enabled="false" placeholder="MRP"></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtSubTotal" Text='<%# Eval("subtotal") %>' Width="70" Enabled="false" runat="server" placeholder="SubTotal" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtDiscount" Text='<%# Eval("discount") %>' Width="70" Enabled="false" runat="server" placeholder="Dis(%)" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtScheme"  Text='<%# Eval("scheme") %>' Width="70" Enabled="false" runat="server" placeholder="Scheme" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtFrieghtAmt" Text='<%# Eval("frieghtamt") %>' Enabled="false" Width="70" runat="server" placeholder="Cart" AutoPostBack="true" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txttaxable" Text='<%# Eval("taxableamt") %>' Width="70" Enabled="false" runat="server" placeholder="Taxable" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtCGST" Text='<%# Eval("csgtper") %>' Width="70" Enabled="false" runat="server" placeholder="CGST" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtSgst"  Text='<%# Eval("sgstper") %>'  Width="70" Enabled="false" runat="server" placeholder="SGST" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtIgst" Text='<%# Eval("igstper") %>' Width="70" Enabled="false" runat="server" placeholder="IGST" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtGSTtotal" Text='<%# Eval("gstamt") %>' Width="70" Enabled="false" runat="server" placeholder="GSTtotal"></asp:Label></td>
                                                        <td>
                                                            <asp:Label   ID="rep_txtTotal"  Text='<%# Eval("total") %>' Width="70" Enabled="false" runat="server" placeholder="Total" ></asp:Label></td>
                                                        <td>
                                                            <asp:Label  ID="rep_txtNetRate" Text='<%# Eval("netrate") %>' Width="70" runat="server" Enabled="false" placeholder="UnitRate" ></asp:Label></td>
                                                        
                                                     
                                                </tr>
                                            </tr>
                                           <%-- <tr>
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
                                            </tr>--%>
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
                                    <%--<tr style="display: none">
                                        <th style="width: 50%">Subtotal:</th>
                                        <td>&#8377 &nbsp;<span id="spnSubTotal" runat="server"></span></td>
                                    </tr>
                                    <tr style="display: none">
                                        <th>GST (18.0%)</th>
                                        <td>&#8377 &nbsp;<span id="spnTax" runat="server"></span></td>
                                    </tr>--%>
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


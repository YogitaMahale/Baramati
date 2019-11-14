<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageproduct.aspx.cs" Inherits="manageproduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        label {
            display: inline-block;
        }

        .blink_me {
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">

                    <!-- /.box -->




                    <div class="box">
                        <br />
                        <div class="text-center">
                            <b id="spnMessage" visible="false" runat="server"></b>
                        </div>
                        <br />
                        <div style="text-align: right;">
                            <asp:Button ID="btn_updateRealStock" Visible="false"  runat="server" Text="Update Stock" class="btn btn-Normal btn-primary" OnClick="btn_updateRealStock_Click" />
                            <asp:Button ID="btnAddNewProduct" runat="server" Text=" New Product" class="btn btn-Normal btn-primary" OnClick="btnAddNewProduct_Click" />

                        </div>
                        <br />
                        <table>
                            <tr>
                                <%--  <td><label for="exampleInputPassword1">Select</label></td>
                            <td>  <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>--%>
                                <%--   <td>
                                    <asp:DropDownList ID="ddlSelectEntry" Visible="false" Class="form-control" Width="80px" runat="server">
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList></td>--%>
                                <td>
                                    <label for="exampleInputPassword1">Select Category</label></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" Class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                       
                                    </asp:DropDownList></td>

                                 <td>
                                    <label for="exampleInputPassword1">Select</label></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlActiveStatus" Class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlActiveStatus_SelectedIndexChanged">
                                         <asp:ListItem Value="0">NotActive</asp:ListItem>
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                         <asp:ListItem Value="2">All</asp:ListItem>
                                       <%-- <asp:ListItem Value="2">All</asp:ListItem>--%>
                                    </asp:DropDownList></td>

                                <td >
                                    <asp:Label ID="Label1" runat="server" Width="20"></asp:Label></td>
                               
                                   
                                <td style="display:none ">
                                    <label for="exampleInputPassword1">Select</label></td>
                                <td style="display:none ">
                                    <asp:Label ID="Labelr2" runat="server" Width="10"></asp:Label></td>
                                <td style="display:none ">
                                    <asp:ListBox ID="ListBox1" runat="server" Width="350px">

                                        <asp:ListItem Value="Select">Not any Price</asp:ListItem>
                                        <asp:ListItem Value="superwholesaleprice">Super Whole Sale Price</asp:ListItem>
                                        <asp:ListItem Value="wholesaleprice">Whole Sale Price</asp:ListItem>
                                        <asp:ListItem Value="dealerprice">Dealer Price</asp:ListItem>
                                        <asp:ListItem Value="customerprice">Customer Price</asp:ListItem>

                                    </asp:ListBox>

                                </td>
                                <td style="display:none ">
                                    <asp:TextBox ID="txtPath" Visible="false" runat="server" Width="20px"></asp:TextBox>
                                </td>
                                <td style="display:none ">
                                    <label for="exampleInputPassword1"></label>
                                </td>
                                <td style="display:none ">
                                    <asp:Label ID="Label4" runat="server" Width="20"></asp:Label></td>
                                <td style="display:none ">
                                    <asp:Button ID="btn_pdfDownload" runat="server" Text="PDF Download" CssClass="btn btn-flickr" OnClick="btn_pdfDownload_Click" />

                                </td>   

                            </tr>
                         <%--   <tr>
                                <td>
                                    <label for="exampleInputPassword1">Select</label></td>
                                <td>
                                    <asp:Label ID="Labelr2" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:ListBox ID="ListBox1" runat="server" Width="350px">

                                        <asp:ListItem Value="Select">Not any Price</asp:ListItem>
                                        <asp:ListItem Value="superwholesaleprice">Super Whole Sale Price</asp:ListItem>
                                        <asp:ListItem Value="wholesaleprice">Whole Sale Price</asp:ListItem>
                                        <asp:ListItem Value="dealerprice">Dealer Price</asp:ListItem>
                                        <asp:ListItem Value="customerprice">Customer Price</asp:ListItem>

                                    </asp:ListBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtPath" Visible="false" runat="server" Width="10px"></asp:TextBox>
                                </td>

                            </tr>--%>
                            <%--<tr>
                                <td>
                                    <label for="exampleInputPassword1"></label>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Width="10"></asp:Label></td>
                                <td>
                                    <asp:Button ID="btn_pdfDownload" runat="server" Text="PDF Download" CssClass="btn btn-danger" OnClick="btn_pdfDownload_Click" />

                                </td>

                            </tr>--%>

                        </table>
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="text-align: left;">
                                <asp:Button ID="btnExcelExport" runat="server" class="btn btn-Normal btn-primary" Width="150" Text="Excel Export" OnClick="btnExcelExport_Click" />

                                <asp:Button ID="btnSave" runat="server" Visible="false"  class="btn btn-Normal btn-primary" Width="150" Text="Save All" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-md-6" style="text-align: right;">
                                <table>
                                    <tr>
                                        <td>
                                            <label for="exampleInputPassword1">Search</label></td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Width="10"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" Width="300"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                
                            </div>
                        </div>



                        <!-- /.box-header -->
                        <div class="box-body" style="overflow: scroll;">
                            <table class="table table-bordered table-striped">
                                <thead>
                                    <tr>

                                        <th style="text-align: center" class="allCheckbox">
                                            <asp:CheckBox ID="allCheckbox1" runat="server" /></th>
                                        <th style="text-align: center">Sr No</th>
                                        <th style="text-align: center">Name</th>
                                        <th style="text-align: center">SKU No.</th>
                                        <th style="text-align: center">SS</th>
                                        <th style="text-align: center">DP</th>
                                        <%-- <th style="text-align: center">Wholesale Price</th>
                                <th style="text-align: center">Customer(Fake) Price</th>--%>
                                        <th style="text-align: center">Fake</th>
                                        <th style="text-align: center">Retail</th>
                                        <th style="text-align: center">Landing Price</th>
                                        <th style="text-align: center; display: none">Discount</th>
                                        <th style="text-align: center">GST %</th>
                                        <th style="text-align: center">App Stock</th>
                                        <th style="text-align: center">Real Stock</th>
                                        <th style="text-align: center">Stock Alert</th>
                                        <th style="text-align: center; display: none;">Is Stock</th>
                                        <th style="text-align: center">Is Active</th>
                                         <th style="text-align: center">Is HotProduct</th>
                                        <th style="text-align: center; display: none;">HSN Code</th>
                                        <th style="text-align: center">Stock Alert</th>
                                        <th style="text-align: center">Action</th>



                                    </tr>

                                </thead>


                                <tbody>

                                    <asp:Repeater ID="repProduct" runat="server" OnItemDataBound="repProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="singleCheckbox" style="text-align: center">
                                                    <asp:CheckBox ID="chkContainer" runat="server" attr-ID='<%# Eval("pid") %>' />
                                                </td>
                                                <td class="singleCheckbox" style="text-align: center">
                                                    <asp:DropDownList ID="ddlSeqNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged" />
                                                </td>
                                                <td style="text-align: center; width: 150px;">
                                                    <asp:Label ID="lblProductId" runat="server" Visible="false" Text='<%# Eval("pid") %>'></asp:Label>
                                                    <asp:Label ID="lblCategoryId" runat="server" Visible="false" Text='<%# Eval("cid") %>'></asp:Label>
                                                    <asp:Label ID="lblSeqNo" runat="server" Visible="false" Text='<%# Eval("seqno") %>'></asp:Label>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("productname") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center; width: 100px;">
                                                    <asp:Label ID="lblSKU" runat="server" Text='<%# Eval("sku") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtSuperWholesaleprice" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("superwholesaleprice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtDealerPrice" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("dealerprice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtWholesaleprice" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("wholesaleprice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtCustomerPrice" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("customerprice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <%-- <asp:Label ID="Label3" runat="server" Text='<%# Eval("LandingPrice") %>'></asp:Label>--%>
                                                    <asp:TextBox ID="txtLandingPrice" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("LandingPrice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center; display: none">
                                                    <asp:TextBox ID="txtDiscountPrice" Style="width: 75px" runat="server" CssClass="form-control" Text='<%# Eval("discountprice") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtGST" Style="width: 40px" runat="server" CssClass="form-control" Text='<%# Eval("gst") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtStockQuantites" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("quantites") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtRealStock" Style="width: 60px" runat="server" CssClass="form-control" Text='<%# Eval("RealStock") %>'></asp:TextBox>


                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtStockAlertQuantites" Style="width: 50px" CssClass="form-control" runat="server" Text='<%# Eval("alertquantites") %>'></asp:TextBox>
                                                </td>
                                                <td style="text-align: center; display: none;">
                                                    <asp:CheckBox ID="chbIsStock" runat="server" Checked='<%# Eval("isstock") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="IsActive" runat="server" AutoPostBack="true" Checked='<%# Eval("isactive") %>' OnCheckedChanged="IsActive_CheckedChanged" />
                                                </td>
                                                 <td style="text-align: center"> 
                                                    <asp:CheckBox ID="isHotproduct" runat="server" AutoPostBack="true" Checked='<%# Eval("isHotproduct") %>' OnCheckedChanged="isHotproduct_CheckedChanged" />
                                                </td>


                                                <td style="text-align: center; display: none;">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("HSNCode") %>'></asp:Label>

                                                </td>




                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblStockAlert" runat="server" CssClass='<%# Eval("CssClass") %>' Style="font-size: medium; font-weight: bold; width: 50px" Text='<%# Eval("quantites") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>&nbsp;
                                    &nbsp;<asp:LinkButton ID="lnkUpdate" runat="server" Visible="false" Text="Update" CssClass="btn btn-sm btn-info"></asp:LinkButton>
                                                    &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this product?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                    &nbsp;<asp:HyperLink ID="hlAddImageVideo" runat="server" Target="_blank" CssClass="btn btn-sm btn-warning" Text="More Images"></asp:HyperLink>
                                                     <asp:HyperLink ID="hlAddProcess" runat="server" CssClass="btn btn-sm btn-success" Visible="false"  Text="Add Process"></asp:HyperLink>&nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="text-align: center" class="allCheckbox">
                                            <asp:CheckBox ID="CheckBox1" runat="server" /></th>
                                        <th style="text-align: center">Sr No</th>
                                        <th style="text-align: center">Name</th>
                                        <th style="text-align: center">SKU No.</th>
                                        <th style="text-align: center">Super Stockist Price</th>
                                        <th style="text-align: center">Dealer Price</th>
                                        <%-- <th style="text-align: center">Wholesale Price</th>
                                <th style="text-align: center">Customer(Fake) Price</th>--%>
                                        <th style="text-align: center">Wholesale Fake Price</th>
                                        <th style="text-align: center">Customer Price</th>
                                        <th style="text-align: center">Landing Price</th>
                                        <th style="text-align: center; display: none">Discount</th>
                                        <th style="text-align: center">GST %</th>
                                        <th style="text-align: center">App Stock</th>
                                        <th style="text-align: center">Real Stock</th>
                                        <th style="text-align: center">Stock Alert</th>
                                        <th style="text-align: center; display: none;">Is Stock</th>
                                        <th style="text-align: center">Is Active</th>
                                        <th style="text-align: center">Is HotProduct</th>
                                        <th style="text-align: center; display: none;">HSN Code</th>
                                        <th style="text-align: center">Stock Alert</th>
                                        <th style="text-align: center">Action</th>


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
            <asp:PostBackTrigger ControlID="btn_pdfDownload" />
        </Triggers>
    </asp:UpdatePanel>

  <%--  <!-- jQuery 3 -->
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
    </script>--%>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageproductstock.aspx.cs" Inherits="manageproductstock" %>

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
                            <div> <table>
        <tr>
            <td><b>Select Category:&nbsp;&nbsp;</b></td>
            <td>
                <asp:DropDownList ID="ddlCategory" CssClass="form-control" AutoPostBack="true" Width="350px" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
    </table>
   </div>
                            <div class="pull-right" >
        <asp:Button ID="btnAddNewProduct" runat="server" Text="Add New Product" CssClass="btn btn-danger" OnClick="btnAddNewProduct_Click" />
                    </div>
                            <div style="text-align: center" id="divSaveAll" runat="server" visible="false">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-dropbox" Text="Save All" OnClick="btnSave_Click" />
    </div>
    <br />
    <div id="divProduct" runat="server" visible="false">
        <table id="tblProduct" class="display table table-hover table-striped table-bordered">
            <thead>
                <tr class="tableheader">
                    <th style="text-align: center" class="allCheckbox">
                    <asp:CheckBox ID="allCheckbox1" runat="server" /></th>
                    <th style="text-align: center">Seq No</th>
                    <th style="text-align: center">Product Name</th>
                    <th style="text-align: center">SKU No.</th>
                    <th style="text-align: center">Image</th>
                    <th style="text-align: center; display: none">Customer Price</th>
                    <th style="text-align: center; display: none">Decler Price</th>
                    <th style="text-align: center; display: none">Discount</th>
                    <th style="text-align: center; display: none">GST %</th>
                    <th style="text-align: center">Stock Quantites</th>
                    <th style="text-align: center">Is Stock</th>
                    <th style="text-align: center">Is Active</th>
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
                            <td style="text-align: center">
                                <asp:Label ID="lblProductId" runat="server" Visible="false" Text='<%# Eval("pid") %>'></asp:Label>
                                <asp:Label ID="lblCategoryId" runat="server" Visible="false" Text='<%# Eval("cid") %>'></asp:Label>
                                <asp:Label ID="lblSeqNo" runat="server" Visible="false" Text='<%# Eval("seqno") %>'></asp:Label>
                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("productname") %>'></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="lblSKU" runat="server" Text='<%# Eval("sku") %>'></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Image ID="imgProduct" Width="75px" Height="50px" runat="server"></asp:Image>
                            </td>
                            <td style="text-align: center; display: none">
                                <asp:TextBox ID="txtCustomerPrice" Style="width: 100px" runat="server" Text='<%# Eval("customerprice") %>'></asp:TextBox>
                            </td>
                            <td style="text-align: center; display: none">
                                <asp:TextBox ID="txtDealerPrice" Style="width: 100px" runat="server" Text='<%# Eval("dealerprice") %>'></asp:TextBox>
                            </td>
                            <td style="text-align: center; display: none">
                                <asp:TextBox ID="txtDiscountPrice" Style="width: 100px" runat="server" Text='<%# Eval("discountprice") %>'></asp:TextBox>
                            </td>
                            <td style="text-align: center; display: none">
                                <asp:TextBox ID="txtGST" Style="width: 100px" runat="server" Text='<%# Eval("gst") %>'></asp:TextBox>
                            </td>
                            <td style="text-align: center">
                                <asp:TextBox ID="txtStockQuantites" Style="width: 100px" runat="server" Text='<%# Eval("quantites") %>'></asp:TextBox>
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="chbIsStock" runat="server" Checked='<%# Eval("isstock") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="IsActive" runat="server" Checked='<%# Eval("isactive") %>' />
                            </td>
                            <td style="text-align: center">
                                <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>&nbsp;
                                &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this product?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                &nbsp;<asp:HyperLink ID="hlAddImageVideo" runat="server" Target="_blank" CssClass="btn btn-sm btn-warning" Text="More Images"></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
        $(function () {
            $('#tblProduct').DataTable({ "order": [[2, "desc"]] })
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


    <script>
        //$(document).ready(function () {
        //    $('#tblProduct').DataTable({
        //        "bLengthChange": true,
        //        "iDisplayLength": 100,
        //        "bFilter": true,
        //        "bInfo": true,
        //        dom: 'Bfrtip',
        //        buttons: [
        //            'excelHtml5',
        //        ]
        //    });
        //});

        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.prop('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeProp('checked');
                }
                else {
                    $allCheckbox.prop('checked', 'checked');
                }
            });
        });

    </script>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report_HigestProductselling.aspx.cs" Inherits="Report_HigestProductselling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <div class="col-md-6" style="text-align: left;">
                                <asp:Button ID="btnExcelExport" runat="server" class="btn btn-Normal btn-primary" Width="100" Text="Excel Export" OnClick="btnExcelExport_Click" />

                                <asp:Button ID="btnpdf" runat="server" class="btn btn-Normal btn-primary" Width="100" Text="PDF Report" OnClick="btnpdf_Click" />
                            </div>
                            <div class="col-md-6" style="text-align: right;">
                                 
                                
                            </div>
                        </div>
                            
                            <br />
                            <table id="example12" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                       <%-- <th style="text-align: center">Sr No</th>--%>
                                        <th style="text-align: center">Product Name</th>
                                        <th style="text-align: center">Product Count</th>

                                    </tr>
                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repHigestProductselling" runat="server">
                                        <ItemTemplate>
                                            <tr>

                                                <%--<td style="text-align: center">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RowId") %>'></asp:Label>
                                                </td>--%>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductCount") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                         <%-- <th style="text-align: center">Sr No</th>--%>
                                        <th style="text-align: center">Product Name</th>
                                        <th style="text-align: center">Product Count</th>
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


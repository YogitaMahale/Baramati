<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageworksheets.aspx.cs" Inherits="manageworksheets" %>

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
                           
                            <div class="pull-right" >
                <asp:Button ID="Button1" runat="server" Text="New Worksheet" CssClass="btn btn-flickr" OnClick="BtnAddNew_Click" />


                    </div>
         <table id="tblBank" class="display table table-hover table-striped table-bordered">
                    <thead>
                        <tr class="tableheader">
                            <th style="text-align: center">Worksheet</th>
                            <th style="text-align: center">Date</th>
                            <th style="text-align: center">Article</th>
                            <th style="text-align: center">Color</th>
                            
                            <th style="text-align: center">Action</th>

                           
                            
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repWorksheet" runat="server" OnItemDataBound="repWorksheet_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">

                                                <%--<td class="center">
                                                    <asp:HyperLink ID="lnkDetails" runat="server" NavigateUrl='<%# Eval("ID","~/ShowDetails.aspx?ID={0}") + "&tag=rd" %>' ><%#Eval("PatientName")%></asp:HyperLink>

                                                </td>--%>


                                        <asp:HyperLink ID="lblAgentId" runat="server"  ><%#Eval("id")%></asp:HyperLink>

                                        <%--<asp:Label ID="lblBankCount" runat="server" Visible="false" Text='<%# Eval("bankcount") %>'></asp:Label>--%>
                                        <%--<asp:Label ID="lblName" runat="server" Text='<%# Eval("vendorName") %>'></asp:Label>--%>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("createddate") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        
                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("id","~/articleproduction.aspx?id={0}") %>' ><%#Eval("productid")%></asp:HyperLink>--%>
                                        <asp:HyperLink ID="lblMobileno" runat="server" ><%#Eval("productid")%></asp:HyperLink>

                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblemail" runat="server" Text='<%# Eval("colornames") %>'></asp:Label>
                                    </td>
                                    <%--<td style="text-align: center">
                                        <asp:Image ID="imgProfile" Width="75px" Height="50px" runat="server"></asp:Image>
                                    </td>--%>
                                    
                                    <td style="text-align: center">
                                        <%--<asp:HyperLink ID="hlEdit" runat="server" Style="text-decoration: underline" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>--%>
                                        &nbsp;
                                        &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this bank?');" OnClick="LnkDelete_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
             <tfoot>
                 <tr>
                 <th style="text-align: center">Worksheet</th>
                            <th style="text-align: center">Date</th>
                            <th style="text-align: center">Article</th>
                            <th style="text-align: center">Color</th>
                            
                            <th style="text-align: center">Action</th>
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
            $('#tblBank').DataTable()
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


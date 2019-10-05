<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageEmployee.aspx.cs" Inherits="manageEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <asp:Button ID="Button1" runat="server" Text="Add New Employee" CssClass="btn btn-danger" OnClick="btnAddNew_Click" />


                    </div>
         <table id="tblBank" class="display table table-hover table-striped table-bordered">
                    <thead>
                        <tr class="tableheader">
                            <th style="text-align: center">Employee Name</th>
                             <th style="text-align: center">Image</th>
                            <th style="text-align: center">Address</th>
                            <th style="text-align: center">Mobile No</th>
                            <th style="text-align: center">E-mail</th>                      
                           <th style="text-align: center">Action</th>

                          
                            
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repemployee" runat="server" OnItemDataBound="repBank_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAgentId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        <%--<asp:Label ID="lblBankCount" runat="server" Visible="false" Text='<%# Eval("bankcount") %>'></asp:Label>--%>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("employeeName") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Image ID="imgProfile" Width="75px" Height="50px" runat="server"></asp:Image>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address1") %>'></asp:Label>
                                    </td>

                                    <td style="text-align: center">
                                        <asp:Label ID="lblMobileno" runat="server" Text='<%# Eval("MobileNo1") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                    </td>
                                    
                                    
                                    <td style="text-align: center">
                                        <asp:HyperLink ID="hlEdit" runat="server" Style="text-decoration: underline" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>&nbsp;
                                        &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this bank?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
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

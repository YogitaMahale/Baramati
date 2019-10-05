<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managebank.aspx.cs" Inherits="managebank" %>

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
                <asp:Button ID="btnAddNew" runat="server" Text="Add New Bank" CssClass="btn btn-danger" OnClick="btnAddNew_Click" />

                    </div>
           <%--<div style="overflow-x: auto;">--%>
                <table id="tblBank" class="display table table-hover table-striped table-bordered">
                    <thead>
                        <tr class="tableheader">
                            <th style="text-align: center">Name</th>
                            <th style="text-align: center">IFSC Code</th>
                            <th style="text-align: center">Branch</th>
                            <th style="text-align: center">Account No</th>
                            <th style="text-align: center">Account Holder Name</th>
                            <th style="text-align: center">Is Show</th>
                            <th style="text-align: center">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repBank" runat="server" OnItemDataBound="repBank_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblBankId" runat="server" Visible="false" Text='<%# Eval("bankid") %>'></asp:Label>
                                        <asp:Label ID="lblBankCount" runat="server" Visible="false" Text='<%# Eval("bankcount") %>'></asp:Label>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("bankname") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblIFSC" runat="server" Text='<%# Eval("bankifsccode") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("bankbranch") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAccountNo" runat="server" Text='<%# Eval("accountno") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAccoutHolderame" runat="server" Text='<%# Eval("accountholdername") %>'></asp:Label>
                                    </td>
                                     <td style="text-align: center">
                                        <asp:CheckBox ID="IsActive" runat="server" AutoPostBack="true" Checked='<%# Eval("isactive") %>' OnCheckedChanged="IsActive_CheckedChanged" />
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
            <%--</div>--%>
   
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
            $('#tblBank').DataTable({ "order": [[2, "desc"]] })
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


   <%--<script>

       $(function () {
           bindDataTable();
           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
       });

       function bindDataTable() {
           var topProductsTable = $('#tblBank').DataTable({
               "bLengthChange": true,
               "iDisplayLength": 50,
               "bFilter": true,
               "bInfo": true,
           });
       }

    </script>--%>
</asp:Content>

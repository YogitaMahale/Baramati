<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="manageappfeedback.aspx.cs" Inherits="manageappfeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
              
                    <!-- /.box -->

                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div style="overflow-x:auto;">
                    <table class="display table table-hover table-striped table-bordered" id="tbFeedBack">
                        <thead>
                            <tr class="tableheader">
                                <th class="text-center">Customer Name
                                </th>
                                <th class="text-center">Contact No
                                </th>
                                <th class="text-center">Feed Back
                                </th>
                                <th class="text-center">Feedback Date
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repFeedBack" runat="server">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td>
                                            <asp:HiddenField ID="hfUserId" runat="server" Value='<%# Eval("feedbackid") %>' />
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("phone") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFeedBack" runat="server" Text='<%# Eval("feedback") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFeedBackDate" runat="server" Text='<%# Eval("feedbackdt") %>'>
                                            </asp:Label>
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
            $('#tbFeedBack').DataTable({ "order": [[2, "desc"]] })
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


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managefeedback.aspx.cs" Inherits="managefeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">
                  
                    <!-- /.box -->

                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                             <%--<div class="pull-right" >
        <asp:Button ID="btnAddStudent" runat="server" Text="Add New Product Arrival" CssClass="btn btn-success" OnClick="btnAddStudent_Click" />
                        
                    </div>--%>
                         
        <%--<div class="box">
            <div class="box-header">
                <div class="col-lg-12">--%>
					<div style="overflow-x:auto;">
                    <table class="display table table-hover table-striped table-bordered" id="tbFeedBack">
                        <thead>
                            <tr class="tableheader">
                                <th class="text-center">Name
                                </th>
                                <th class="text-center">User Type
                                </th>
                                 <th class="text-center">Feedback Date
                                </th>
                                <th class="text-center">Title
                                </th>
                                <th class="text-center">Description
                                </th>
                                <th class="text-center">Image 1
                                </th>
                                <th class="text-center">Image 2
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repFeedBack" runat="server" OnItemDataBound="repFeedBack_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td>
                                            <asp:HiddenField ID="hfUserId" runat="server" Value='<%# Eval("productfeedid") %>' />
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("username") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("usertype") %>'>
                                            </asp:Label>
                                        </td>
                                         <td>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%# Eval("createddatetime") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescp" runat="server" Text='<%# Eval("descption") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlImage1" runat="server" CssClass="img btn btn-sm btn-success" Text="Image 1"></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlImage2" runat="server" CssClass="img btn btn-sm btn-danger" Text="Image 2"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
				</div>
               <%-- </div>
            </div>
        </div>--%>
    
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Feedback Uploaded Image</h4>
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
            $('#tbFeedBack').DataTable({ "order": [[3, "desc"]] })
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
        //    $('#tbFeedBack').DataTable({
        //        "bLengthChange": true,
        //        "iDisplayLength": 50,
        //        "bFilter": true,
        //        "bInfo": true,
        //        dom: 'Bfrtip',
        //        buttons: [
        //            'excelHtml5',
        //        ]
        //    });
        //});
        $('.img').on('click', function () {
            var sr = $(this).attr('src');
            $('#mimg').attr('src', sr);
            $('#myModal').modal('show');
        });
    </script>


</asp:Content>


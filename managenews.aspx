<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managenews.aspx.cs" Inherits="managenews" %>

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
                             <div class="pull-right" >
        <asp:Button ID="btnAddStudent" runat="server" Text="Add New Product Arrival" CssClass="btn btn-success" OnClick="btnAddStudent_Click" />
                        
                    </div>
                            <%--<div style="overflow-x: auto;">--%>
        <table class="display table table-hover table-striped table-bordered" id="tblNews">
            <thead>
                <tr class="tableheader">
                    <th style="width: 150px" class="text-center">Title
                    </th>
                    <th style="width: 80px" class="text-center">Image
                    </th>
                    <th style="width: 100px" class="text-center">Date
                    </th>
                    <th style="width: 350px" class="text-center">Description
                    </th>
                    <th style="text-align: center; width: 50px">IsActive</th>
                    <th style="width: 150px" class="text-center">Action
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repNews" runat="server" OnItemDataBound="repNews_ItemDataBound">
                    <ItemTemplate>
                        <tr class="text-center">
                            <td>
                                <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("newsupdateid") %>' />
                                <asp:Label ID="lblNewsId" runat="server" Visible="false" Text='<%# Eval("newsupdateid") %>'></asp:Label>
                                <asp:Label ID="lblNewsTitle" runat="server" Text='<%# Eval("title") %>'>
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Image ID="imgNews" Width="75px" Height="50px" runat="server"></asp:Image>
                            </td>
                            <td>
                                <asp:Label ID="lblNewsDate" runat="server" Text='<%# Eval("newsdate") %>'>
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblShortDescription" Style="width: 350px" runat="server" Text='<%# Eval("shortdescp") %>'>
                                </asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:CheckBox ID="cbIsActive" runat="server" AutoPostBack="true" Checked='<%#Eval("isactive") %>' OnCheckedChanged="cbIsActive_CheckedChanged"></asp:CheckBox>
                            </td>
                            <td>
                                <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>&nbsp;&nbsp;
                            
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-sm btn-danger" Text="Delete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Do you want to delete this news?');"></asp:LinkButton>
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
            $('#tblNews').DataTable({ "order": [[3, "desc"]] })
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
        $(document).ready(function () {
            $('#tblNews').DataTable({
                "bLengthChange": true,
                "iDisplayLength": 90,
                "bFilter": true,
                "bInfo": true,
            });
        });
    </script>--%>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="managetransporters.aspx.cs" Inherits="managetransporters" %>

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
                <asp:Button ID="btnAddNew" runat="server" Text="New Transporter" CssClass="btn btn-danger" OnClick="btnAddNew_Click" />


                    </div>
        <%--<div style="overflow-x: auto;">--%>
                <table id="tblBank" class="display table table-hover table-striped table-bordered">
                    
<thead>
                                    <tr>
                                        <th style="width: 200px; text-align: center">Name
                                        </th>
                                        <th style="width: 80px; text-align: center">Contact No
                                        </th>
                                        <th style="width: 80px; text-align: center">Email
                                        </th>
                                        <th style="width: 80px; text-align: center">GSTIN
                                        </th>
                                        <th style="width: 80px; text-align: center">GST TYPE
                                        </th>
                                        <th style="width: 80px; text-align: center">Aadhar
                                        </th>
                                        <th style="width: 80px; text-align: center">PAN
                                        </th>
                                        <th style="width: 150px; text-align: center">Address
                                        </th>
                                        <th style="width: 150px; text-align: center">Remark
                                        </th>
                                        <th style="width:120px; text-align: center">Action
                                        </th>
                                    </tr>
                                   
                                </thead>

                    <tbody>
                        <asp:Repeater ID="repAgent" runat="server" OnItemDataBound="repBank_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("mobileno") +" / "+ Eval("phoneno") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblgstno" runat="server" Text='<%# Eval("gstno") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblgsttype" runat="server" Text='<%# Eval("gsttype") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblaadharno" runat="server" Text='<%# Eval("aadharno") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblpanno" runat="server" Text='<%# Eval("panno") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("address") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                    </td>
                                    
                                    <td style="text-align: center">
                                        <asp:HyperLink ID="hlEdit" runat="server" Style="text-decoration: underline" CssClass="btn btn-sm btn-success" Text="Edit"></asp:HyperLink>&nbsp;
                                        &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this transporter?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr>
                                        <th style="width: 200px; text-align: center">Name
                                        </th>
                                        <th style="width: 80px; text-align: center">Contact No
                                        </th>
                                        <th style="width: 80px; text-align: center">Email
                                        </th>
                                        <th style="width: 80px; text-align: center">GSTIN
                                        </th>
                                        <th style="width: 80px; text-align: center">GST TYPE
                                        </th>
                                        <th style="width: 80px; text-align: center">Aadhar
                                        </th>
                                        <th style="width: 80px; text-align: center">PAN
                                        </th>
                                        <th style="width: 150px; text-align: center">Address
                                        </th>
                                        <th style="width: 150px; text-align: center">Remark
                                        </th>
                                        <th style="width:120px; text-align: center">Action
                                        </th>
                                    </tr>

                    </tfoot>
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




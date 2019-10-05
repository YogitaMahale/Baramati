<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="EZACUSfollowup.aspx.cs" Inherits="EZACUSfollowup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <!-- /.box -->
            <div class="box">
                <br />
                <div class="text-center">
                    <b id="spnMessage" visible="false" runat="server"></b>
                </div>
                <br />
                <center>
                <table>
                    <tr>
                        
                        <td>
                            <label for="exampleInputPassword1">Import Excel File :</label></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>
                        <td>
                            
                            <asp:FileUpload ID="FileUpload1" runat="server"   />
                        </td>

                        
                        <td>
                           <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload"  class="btn btn-Normal btn-primary"/> </td>
                        <td>
                            <asp:Label ID="Label1" runat="server"  ></asp:Label></td>                       
                    </tr>                    
                </table>
                         </center>
                <br />

                <!-- /.box-header -->
                <div class="box-body" style="overflow: scroll;">
                    <table id="example1" class="table table-bordered table-striped">   
                        <thead>
                            <tr>
                                <th style="text-align: center">SrNo</th>
                                <th style="text-align: center">Name</th>
                                <th style="text-align: center">Mobile</th>
                                <th style="text-align: center">City</th>
                                <th style="text-align: center">Customer Type</th>
                                <th style="text-align: center">Product</th>
                                <th style="text-align: center">Enquiry From</th>
                                <th style="text-align: center">IsRead</th>
                                <th style="text-align: center">USER</th>


                            </tr>

                        </thead>


                        <tbody>
                            <asp:Repeater ID="repNews" runat="server">
                                <ItemTemplate>
                                    <tr class="text-center">
                                        <td>
                                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("PKId") %>' />
                                            <asp:Label ID="lblNewsId" runat="server" Visible="true" Text='<%# Eval("PKid") %>'></asp:Label>


                                        </td>
                                        <td>

                                            <asp:Label ID="lblNewsTitle" runat="server" Text='<%# Eval("Name") %>'></asp:Label>


                                        </td>
                                        <td>
                                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile") %>'>
                                            </asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblNewsDate" runat="server" Text='<%# Eval("city") %>'>
                                            </asp:Label>


                                        </td>


                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("CustomerType") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Product") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("EnquiryFrom") %>'>
                                            </asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Eval("Isread") %>'></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblremark" Style="width: 350px" runat="server" Text='<%# Eval("remark") %>'>
                                            </asp:Label>





                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                        <tfoot>
                            <tr>
                                <th style="text-align: center">SrNo</th>
                                <th style="text-align: center">Name</th>
                                <th style="text-align: center">Mobile</th>
                                <th style="text-align: center">City</th>
                                <th style="text-align: center">Customer Type</th>
                                <th style="text-align: center">Product</th>
                                <th style="text-align: center">Enquiry From</th>
                                <th style="text-align: center">IsRead</th>
                                <th style="text-align: center">USER</th>

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
    <%-- </ContentTemplate>
          <Triggers><asp:PostBackTrigger ControlID="btn_pdfDownload"/>  </Triggers>
    </asp:UpdatePanel>--%>

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

<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditproductmultipleimages.aspx.cs" Inherits="addeditproductmultipleimages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function checkFileExtension() {
            var result = false;
            var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            if (($("#ctl00_ContentPlaceHolder1_fpImage").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage").val() == null)) {
                alert("Please Select Image.")
                result = false;
            }
            else {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".jpeg"];
                var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpImage");
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                if (!regex.test(fileUpload.value.toLowerCase())) {
                    alert("Please upload files having extensions:" + allowedFiles.join(', ') + " only.");
                    result = false;
                }
                else {
                    result = true;
                }
            }

            return result;
        }
    </script>
    0
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


    <div class="row">
        <div class="col-xs-12">

            <!-- /.box -->




            <div class="box">
                <br />
                <div class="text-center">
                    <b id="bMessage" visible="false" runat="server"></b>
                </div>
                <br />
                <div style="text-align: center;">
                    <center>
                    <table>
                        <tr>
                            <td>
                                <label for="exampleInputPassword1">Select Images</label></td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Width="10"></asp:Label></td>
                            <td>
                                <asp:FileUpload ID="fpImage" runat="server" />

                            </td>
                            <td>
                                <asp:TextBox ID="txtImageName" placeholder="Image Name" CssClass="form-control" runat="server" />
 
                            </td>
                            <td>
                                <asp:Button ID="btnImages" runat="server" Text="Upload" class="btn btn-primary" OnClientClick="return checkFileExtension();" OnClick="btnImages_Click" />
                                <%--<asp:Button ID="btnImages" runat="server" Text="Upload" class="btn btn-primary"  OnClick="btnImages_Click" />--%>


                            </td>

                        </tr>


                    </table>
                        </center>
                </div>
                <br />




                <!-- /.box-header -->
                <div class="box-body" style="overflow: scroll;">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>

                                <th style="text-align: center">Sr No.</th>
                                <th style="text-align: center">Image Name</th>
                                <th style="text-align: center">Image</th>
                                <th style="text-align: center">Action </th>

                            </tr>

                        </thead>


                        <tbody>

                            <asp:Repeater ID="repImage" runat="server" OnItemDataBound="repImage_ItemDataBound">
                                <ItemTemplate>
                                    <tr style="text-align: center">
                                        <td>
                                            <asp:HiddenField ID="hfImageId" runat="server" Value='<%# Eval("piid") %>' />
                                            <asp:Label ID="lblNo" runat="server" Text=' <%#Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblImageName" runat="server" Text='<%# Eval("imagevideoname") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgProduct" runat="server" Width="75" Height="50" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClientClick="return confirm('Do you want to delete this image?');" OnClick="lnDelete_Click"></asp:LinkButton>
                                        </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th style="text-align: center">Sr No.</th>
                                <th style="text-align: center">Image Name</th>
                                <th style="text-align: center">Image</th>
                                <th style="text-align: center">Action </th>

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
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

    <!-- jQuery 3 -->
    <%--<script src="Template/bower_components/jquery/dist/jquery.min.js"></script>--%>
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


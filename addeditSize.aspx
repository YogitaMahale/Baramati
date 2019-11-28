<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditSize.aspx.cs" Inherits="addeditSize" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
        .error {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">  <b id="spnMessgae" runat="server"></b></h3>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    <div class="form-group row">
                        <div class="col-xs-6">
                        <label for="exampleInputEmail1">Size Name </label>
                         <asp:TextBox ID="txtSizeName" class="form-control" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RFVtxtCategoryName" runat="server" Display="Dynamic" ControlToValidate="txtSizeName" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>
                    </div>
                    </div>

                    <div class="form-group row">
                        <div class="col-xs-6">
                        <label for="exampleInputEmail1">Size Group </label>
                         <asp:ListBox ID="lstGroup" runat="server" class="form-control select2"></asp:ListBox>
                         <asp:HiddenField ID="hfgroupid" runat="server" />
            
                    </div>
                    </div>




                  <%--  <div class="form-group">
                        <label for="exampleInputPassword1">Category Short Description</label>
                         <asp:TextBox ID="txtCategoryShortDescription" Class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>                  
                        <asp:RequiredFieldValidator ID="RFVtxtCategoryShortDescription" runat="server" Display="Dynamic" ControlToValidate="txtCategoryShortDescription" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Category Long Description</label>
                         <asp:TextBox ID="txtCategoryLongDescription" Class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>--%>


                    <%--<div class="form-group">
                        <label for="exampleInputFile">Category Image</label>
                         <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpCategory" runat="server"/>
                                </td>
                                <td>
                                    <asp:Image ID="imgCategory" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Visible="false" CssClass="btn btn-danger" Text="X" CausesValidation="false" OnClick="btnRemove_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnImageUpload" runat="server" CssClass="btn btn-info" Text="Upload" OnClick="btnImageUpload_Click" OnClientClick="return checkFileExtension()" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                     --%>
                    
                </div>
                <!-- /.box-body -->

                <div class="box-footer">
                     
                     <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" CssClass="btn btn-info" OnClick="btnCancel_Click" Text="Cancel" />
                </div>

            </div>
            <!-- /.box -->

            <!-- Form Element sizes -->

            <!-- /.box -->


            <!-- /.box -->

            <!-- Input addon -->

            <!-- /.box -->

        </div>
        <!--/.col (left) -->
        <!-- right column -->

        <!--/.col (right) -->
    </div>
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
    <script type="text/javascript">
        function checkFileExtension() {
            var result = false;
            var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            //if (($("#ctl00_ContentPlaceHolder1_fpCategory").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpCategory").val() == null)) {
            if ((document.getElementById("fpCategory").val() == "") || (document.getElementById("fpCategory").val() == null)) {
                alert("Please Upload Image.")
                result = false;
            }
            else {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                //var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpCategory");
                var fileUpload = document.getElementById("fpCategory");
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



    <script type="text/javascript">

        $(document).ready(function () {

            initDropDowns();

        });


        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }


        function initDropDowns() {

            $("#<%=lstGroup.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstGroup.ClientID%>").select2("val"));
                    $('[id*=hfgroupid]').val($(this).val());
                });

            
        }


    </script>

    <%--<script type="text/javascript">
    </script>
    <script type="text/javascript">
    </script>--%>


</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditRawmaterial.aspx.cs" Inherits="addeditRawmaterial" %>
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
        <div style="text-align:center;">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                </div>
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-primary">
                <%--<div class="box-header with-border">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                </div>--%>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    
                    <div class="form-group">
                        <label for="exampleInputEmail1">Material Name </label>
                        <asp:TextBox ID="txtProductName" Class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtProductName" runat="server" Display="Dynamic" ControlToValidate="txtProductName" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>

                   

                    <div class="form-group">
                        <label for="exampleInputPassword1">  Price </label>
                        <asp:TextBox ID="txtPrice" Class="form-control" runat="server"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FTBtxtCustomerProductPrice" runat="server" FilterMode="ValidChars" TargetControlID="txtPrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="RFVtxtCustomerProductPrice" runat="server" Display="Dynamic" ControlToValidate="txtPrice" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="exampleInputEmail1">Unit</label>
                        <asp:DropDownList ID="ddlunit" Class="form-control" Width="500px" runat="server"></asp:DropDownList>

                        <%--<asp:TextBox ID="TextBox3" Class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlunit" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputEmail1">HSN Code</label>
                        <asp:TextBox ID="txthsncode" Class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txthsncode" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputEmail1">GST</label>
                        <asp:TextBox ID="txtgstno" Class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtgstno" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>
                    

                   

                    <div class="form-group">
                        <label for="exampleInputPassword1">Stock Quantites </label>
                        <asp:TextBox ID="txtQuantites" Class="form-control" runat="server"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FTBtxtQuantites" runat="server" FilterMode="ValidChars" TargetControlID="txtQuantites" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                        <asp:RequiredFieldValidator ID="RFVtxtQuantites" runat="server" Display="Dynamic" ControlToValidate="txtQuantites" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Stock Alert Quantites</label>
                        <asp:TextBox ID="txtAlertQuantites" Class="form-control" runat="server"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FTBtxtAlertQuantites" runat="server" FilterMode="ValidChars" TargetControlID="txtAlertQuantites" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                        <asp:RequiredFieldValidator ID="RFVtxtAlertQuantites" runat="server" Display="Dynamic" ControlToValidate="txtAlertQuantites" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Is Stock</label>
                        <asp:CheckBox ID="cbIsStock" Class="form-control" runat="server"></asp:CheckBox>
                    </div>
                     
                </div>
                
            </div>
             

        </div>
        <div class="col-md-6">
            <div class="box box-primary">
                <div class="box-body">
                    
                    <div class="form-group">
                        <label for="exampleInputPassword1">Short Description </label>
                        <asp:TextBox ID="txtProductShortDescription" Class="form-control"  TextMode="MultiLine" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RFVtxtProductShortDescription" runat="server" Display="Dynamic" ControlToValidate="txtProductShortDescription" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Long Description</label>
                        <asp:TextBox ID="txtProductDescription" Class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox></td>
               
                    </div>



                    <div class="form-group" style="display:none;" >
                        <label for="exampleInputFile">Image</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpProduct" runat="server" Visible="false" />
                                </td>
                                <td>
                                    <asp:Image ID="imgProduct" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Visible="false" Text="X" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnRemove_Click" />
                                    &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnImageUpload" runat="server" Visible="false" Text="Upload" CssClass="btn btn-info" OnClick="btnImageUpload_Click" OnClientClick="return checkFileExtension()" />
                                </td>
                            </tr>
                        </table>
                    </div>


                   
                    <div class="box-footer">
                        <asp:Button ID="btnSave" runat="server" CausesValidation="true" ValidationGroup="p1" Text="Save" class="btn btn-primary" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" class="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />


                    </div>
                </div>

            </div>
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
            //if (($("#ctl00_ContentPlaceHolder1_fpProduct").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpProduct").val() == null)) {
            if ((document.getElementById("fpProduct").val() == "") || (document.getElementById("fpProduct").val() == null)) {
                alert("Please Upload Image.")
                result = false;
            }
            else {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
                //var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpProduct");
                var fileUpload = document.getElementById("fpProduct");
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
</asp:Content>


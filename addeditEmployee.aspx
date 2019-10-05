<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditEmployee.aspx.cs" Inherits="addeditEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3><span style="color: red">* Indicates Required Fields</span></h3>
                            <h3 class="box-title" style="text-align: center"><b id="spnMessgae" runat="server"></b></h3>

                        </div>

                        <div class="box-body">


                            <div class="form-group row">

                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Employee Name<span style="color: red">*</span> </label>
                                    <asp:TextBox ID="txtEmployeeName" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtEmployeeName" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Address 1 <span style="color: red">*</span></label>
                                    <asp:TextBox ID="txtAddress1" CssClass="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAddress1" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Mobile No 1<span style="color: red">*</span></label>
                                    <asp:TextBox ID="txtMobileNo1" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo1" ValidationGroup="c1" ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />
                                    <asp:RequiredFieldValidator ID="RFVtxtBankBranch" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo1" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Landline</label>
                                    <asp:TextBox ID="txtLandline" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLandline" ValidationGroup="c1" ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">E-mail<span style="color: red">*</span></label>
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <asp:RequiredFieldValidator ID="RFVtxtAccountNo" runat="server" Display="Dynamic" ControlToValidate="txtEmail" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

                                </div>
                            </div>


                            <div class="form-group row">

                                <div class="col-xs-12">
                                    <table class="table table-user-information">
                                        <tbody>
                                            <tr>
                                                <td style="width: 250px; font-weight: bold">Image</td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:FileUpload ID="fpCategory" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="imgCategory" Visible="false" Width="75px" Height="50px" runat="server" />
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Visible="false" CssClass="btn btn-danger" Text="X" CausesValidation="false" OnClick="btnRemove_Click" />
                                                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnImageUpload" runat="server" CssClass="btn btn-info" Text="UPLOAD" OnClick="btnImageUpload_Click" OnClientClick="return checkFileExtension()" />
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;<%-- <asp:RequiredFieldValidator ID="RFVfpCategory" runat="server" Display="Dynamic" ControlToValidate="fpCategory" CssClass="error" ErrorMessage="This is required field" ValidationGroup="c1"></asp:RequiredFieldValidator>--%></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>


                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE"  OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
                                </div>
                            </div>

                        </div>







                        </>
                

                    </div>
                    
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImageUpload" />
        </Triggers>

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



     
</asp:Content>

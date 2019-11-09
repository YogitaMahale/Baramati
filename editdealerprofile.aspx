<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="editdealerprofile.aspx.cs" Inherits="editdealerprofile" %>
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
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">  <b id="spnMessgae" runat="server"></b></h3>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    <div class="form-group row">
                        <div class="col-xs-4">
                        <label for="exampleInputEmail1">Dealer Name </label>
                         <asp:TextBox ID="txtDealerName" class="form-control"   runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RFVtxtDealerName" runat="server" Display="Dynamic" ControlToValidate="txtDealerName" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    </div>
                        <div class="col-xs-4">
                            <label for="exampleInputEmail1">User Login No. </label>
                         <asp:TextBox ID="txtUserLoginNo"   class="form-control"  runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVtxtUserLoginNo" runat="server" Display="Dynamic" ControlToValidate="txtUserLoginNo" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    </div>
                          <div class="col-xs-4">
                             <label for="exampleInputEmail1">Password </label>
                            <asp:TextBox ID="txtPassword"    class="form-control" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                 </div>
                    </div>

                    

                    <div class="form-group row">
                        <div class="col-xs-4"><label for="exampleInputPassword1">What App No.</label>
                         <asp:TextBox ID="txtWhatappNo" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtWhatappNo" runat="server" Display="Dynamic" ControlToValidate="txtWhatappNo" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    </div>
                        <div class="col-xs-4"><label for="exampleInputPassword1">Email Id</label>
                          <asp:TextBox ID="txtEmailId" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtEmailId" runat="server" Display="Dynamic" ControlToValidate="txtEmailId" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REtxtEmailId" runat="server" Display="Dynamic" CssClass="erroe" ControlToValidate="txtEmailId" ErrorMessage="Invalid Email" ValidationGroup="d1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                        <div class="col-xs-4"><label for="exampleInputPassword1">GST No</label>
                         <asp:TextBox ID="txtGSTNO" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>
                      <div class="form-group row">
                       <div class="col-xs-4"> <label for="exampleInputPassword1">Aadhar No</label>
                         <asp:TextBox ID="txtaadharno" class="form-control" runat="server"></asp:TextBox>
                    </div>
                          <div class="col-xs-4"><label for="exampleInputPassword1">PAN No</label>
                         <asp:TextBox ID="txtpanno" class="form-control" runat="server"></asp:TextBox>
                    </div>
                          <div class="col-xs-4"><label for="exampleInputPassword1"> Agent Name</label>
                        <asp:DropDownList ID="ddlUser" runat="server" class="form-control"></asp:DropDownList>
                        <%--<asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>--%>
                    </div>
                      </div>
                      
                    <div class="form-group row">
                        
                        <div class="col-xs-4"><label for="exampleInputPassword1"> State</label>
                        <asp:DropDownList ID="txtSate" runat="server" OnSelectedIndexChanged="txtSate_SelectedIndexChanged" AutoPostBack="true"  class="form-control"></asp:DropDownList>
                       
                    </div>
                        <div class="col-xs-4"><label for="exampleInputPassword1"> District</label>
                        <asp:DropDownList ID="ddldistrict" runat="server"  class="form-control"></asp:DropDownList>
                       
                    </div>
                        <div class="col-xs-4"><label for="exampleInputPassword1">City</label>
                            <asp:TextBox ID="txtCity" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    </div>
                    
                    <div class="form-group row">
                        <div class="col-xs-4"><label for="exampleInputPassword1"> Address 1</label>
                         <asp:TextBox ID="txtAddress1" class="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                        <div class="col-xs-4">
                            <label for="exampleInputPassword1"> Address 2</label>
                         <asp:TextBox ID="txtAddress2" class="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                        <div class="col-xs-4"><label for="exampleInputPassword1">Opening Balance</label>
                            <asp:TextBox ID="txtbalance" class="form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbalance" ValidationGroup="gg"  ValidationExpression="^-?([0-9]{0,9}(\.[0-9]{0,2})?|100(\.00?)?)$" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />

                    </div>
                                            
                </div>
                <!-- /.box-body -->

                <div class="box-footer">
                      <asp:Button ID="btnUpdate" runat="server" CausesValidation="true" ValidationGroup="d1" Text="Save" class="btn btn-primary" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" class="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />

                     
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
    
    <script src="Template/dist/js/canvasjs.min.js"></script>
    <!-- page script -->

    <script type="text/javascript">
        function checkFileExtension() {
            var result = false;
            var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            if (($("#ctl00_ContentPlaceHolder1_fpCategory").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpCategory").val() == null)) {
                alert("Please Upload Image.")
                result = false;
            }
            else {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpCategory");
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

<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditnews.aspx.cs" Inherits="addeditnews" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .error {
            color: red;
        }
    </style>
   
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
                    <h3><span style="color:red">* Indicates Required Fields</span></h3>
                    <h3 class="box-title" style="text-align:center">  <b id="spnMessgae" runat="server"></b></h3>
                    <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%> 
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                   
                    <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1">News Title<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtNewsTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtNewsTitle" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1">New Date<span style="color:red">*</span></label>
                        <asp:TextBox ID="txtNewsDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CEtxtNewsDate" runat="server" TargetControlID="txtNewsDate" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                        
<%--                        <asp:TextBox ID="txtMobile" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMobile" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   --%>
                         
                    </div>
                        <div class="col-xs-12"> 
                        <label for="exampleInputEmail1">Short Description </label>
                        <asp:TextBox ID="txtShortDescp" runat="server" Rows="2" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                        <%--<asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtShortDescp" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                    <div class="col-xs-12"> 
                        <label for="exampleInputEmail1"> Description </label>
                        <asp:TextBox ID="txtDescription" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        <%--<asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDescription" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                    <div class="col-xs-12"> 
                       <table class="table table-user-information">
            <tbody><tr>
                    <td style="width: 250px; font-weight: bold">Image 1</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpImage" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgNews" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnUpload_Click" OnClientClick="return checkFileExtension()" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Text="X" CssClass="btn btn-info" Visible="false" CausesValidation="false" OnClick="btnRemove_Click" />

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; font-weight: bold">Image 2</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpImage2" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgNews2" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload2" runat="server" Text="UPLOAD" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnUpload2_Click" OnClientClick="return checkFileExtension2()" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove2" runat="server" Text="X" CssClass="btn btn-info" Visible="false" CausesValidation="false" OnClick="btnRemove2_Click" />

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; font-weight: bold">Image 3</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpImage3" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgNews3" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload3" runat="server" Text="UPLOAD" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnUpload3_Click" OnClientClick="return checkFileExtension3()" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove3" runat="server" Text="X" CssClass="btn btn-info" Visible="false" CausesValidation="false" OnClick="btnRemove3_Click" />

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>


                <tr>
                    <td style="width: 250px; font-weight: bold">Image 4</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpImage4" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgNews4" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload4" runat="server" Text="UPLOAD" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnUpload4_Click" OnClientClick="return checkFileExtension4()" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove4" runat="server" Text="X" CssClass="btn btn-info" Visible="false" CausesValidation="false" OnClick="btnRemove4_Click" />

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; font-weight: bold">Image 5</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpImage5" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgNews5" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload5" runat="server" Text="UPLOAD" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnUpload5_Click" OnClientClick="return checkFileExtension5()" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove5" runat="server" Text="X" CssClass="btn btn-info" Visible="false" CausesValidation="false" OnClick="btnRemove5_Click" />

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </tbody>
                           </table>
                    </div>
                    </div>
                   
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnAddEditNews" runat="server" CssClass="btn btn-primary" Text="ADD" ValidationGroup="n1" CausesValidation="true" OnClick="btnAddEditNews_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="CANCEL" CssClass="btn btn-default" CausesValidation="false" OnClick="btnClear_Click" />
                </div>
                </div>


                    


                 </>
                 <%--</div>--%>
                   
                </>
                <!-- /.box-body -->

                
            
            </>
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
        </div>
 </ContentTemplate>
      <Triggers><asp:PostBackTrigger ControlID="btnUpload"/>  </Triggers>   
      <Triggers><asp:PostBackTrigger ControlID="btnUpload2"/>  </Triggers>   
      <Triggers><asp:PostBackTrigger ControlID="btnUpload3"/>  </Triggers>   
      <Triggers><asp:PostBackTrigger ControlID="btnUpload4"/>  </Triggers>   
      <Triggers><asp:PostBackTrigger ControlID="btnUpload5"/>  </Triggers>   
        
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
             //if (($("#ctl00_ContentPlaceHolder1_fpImage").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage").val() == null)) {
             if ((document.getElementById("fpImage").val() == "") || (document.getElementById("fpImage").val() == null)) {
                     alert("Please Upload Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                 var fileUpload = document.getElementById("fpImage");
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
         function checkFileExtension2() {
             var result = false;
             var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
             //if (($("#ctl00_ContentPlaceHolder1_fpImage2").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage2").val() == null)) {
             if ((document.getElementById("fpImage2").val() == "") || (document.getElementById("fpImage2").val() == null)) {
                     alert("Please Upload Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                 var fileUpload = document.getElementById("fpImage2");
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
         function checkFileExtension3() {
             var result = false;
             var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            // if (($("#ctl00_ContentPlaceHolder1_fpImage3").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage3").val() == null)) {
             if ((document.getElementById("fpImage3").val() == "") || (document.getElementById("fpImage3").val() == null)) {
                     alert("Please Upload Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                 var fileUpload = document.getElementById("fpImage3");
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
         function checkFileExtension4() {
             var result = false;
             var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
            // if (($("#ctl00_ContentPlaceHolder1_fpImage4").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage4").val() == null)) {
             if ((document.getElementById("fpImage4").val() == "") || (document.getElementById("fpImage4").val() == null)) {
                     alert("Please Upload Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                 var fileUpload = document.getElementById("fpImage4");
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
         function checkFileExtension5() {
             var result = false;
             var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
           //  if (($("#ctl00_ContentPlaceHolder1_fpImage5").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpImage5").val() == null)) {
             if ((document.getElementById("fpImage5").val() == "") || (document.getElementById("fpImage5").val() == null)) {
                     alert("Please Upload Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
                 var fileUpload = document.getElementById("fpImage5");
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



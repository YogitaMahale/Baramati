<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditoffer.aspx.cs" Inherits="addeditoffer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .error {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <label for="exampleInputEmail1">Offer Name<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtSchemeName" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <%--<asp:TextBox ID="txtNewsTitle" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtSchemeName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        </div>
                        <div class="form-group row">
                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Description </label>
                        <asp:TextBox ID="txtDescription" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        <%--<asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDescription" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                        <div class="form-group row">

                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Valid From</label>
                        <asp:TextBox ID="txtValidFrom" Style="width: 350px" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CEtxtValidFrom" runat="server" TargetControlID="txtValidFrom" Format="dd-MM-yyyy"></cc1:CalendarExtender> 
                    </div>
                            <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Valid To</label>
                        <asp:TextBox ID="txtValidTo" Style="width: 350px" CssClass="form-control" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CEtxtValidTo" runat="server" TargetControlID="txtValidTo" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                    </div>
                        </div>
                        <%--<div class="form-group row">

                        
                        </div>--%>
                        <br />
                        <div class="form-group">
                        <label for="exampleInputFile">Offer Image</label>
                         <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpCategory" runat="server" />
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
                   
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
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
      <Triggers><asp:PostBackTrigger ControlID="btnImageUpload"/>  </Triggers>   
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


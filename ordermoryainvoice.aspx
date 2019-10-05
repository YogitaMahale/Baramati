<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="ordermoryainvoice.aspx.cs" Inherits="ordermoryainvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                   
                    <%--<div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1"> Name<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtBankName" CssClass="form-control" runat="server"></asp:TextBox>
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtBankName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                  </div>--%>
                   <div class="row">
        <div class="col-md-12 col-sm-12 col-lg-6 col-md-offset-3 well" id="divMessage" runat="server" visible="false">
            <div class="col-lg-4">
                &nbsp;
            </div>
            <div class="col-lg-5">
                <b id="bMessage" visible="false" runat="server"></b>
            </div>

        </div>
        <div class="col-md-12 col-sm-12 col-lg-8 col-md-offset-3 well">
            <div class="col-lg-2">
                <label class="control-label">Select Images  <span class="text-red">*</span></label>
            </div>
            <div class="col-lg-3">
                <asp:FileUpload ID="fpImage" runat="server" />
            </div>
            <div class="col-lg-3">
                <asp:Button ID="btnImages" runat="server" Text="Upload" CssClass="btn btn-success" OnClientClick="return checkFileExtension();" OnClick="btnImages_Click" />
            </div>
        </div>
    </div>
    <br />
    <div class="row text-center" id="divImage" runat="server">
        <div class="col-md-6">
            <div class="box box-primary">
                <div class="box-body chart-responsive text-center">
                    <asp:Image ID="imgOrderImage1" runat="server" Width="500px" Height="500px" />
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="box box-primary">
                <div class="box-body chart-responsive text-center">
                    <asp:Image ID="imgOrderImage2" runat="server" Width="500px" Height="500px" />
                </div>
            </div>
        </div>
    </div> 

                    


                 </div>
                 <%--</div>--%>
                   
                
                <!-- /.box-body -->

                
            
            
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
        
 </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnImages" />

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
             if ((document.getElementById("fpImage").val() == "") || (document.getElementById("fpImage").val() == null)) {
                 alert("Please Select Image.")
                 result = false;
             }
             else {
                 var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".jpeg"];
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
    </script>



    
</asp:Content>


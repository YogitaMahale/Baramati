<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="DataEntry.aspx.cs" Inherits="DataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .error {
            color: red;
        }
    </style>
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
    <script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border" style="text-align:center;">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                    <h4>* Indicates Required Fields</h4>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Name <span style="color: red">*</span> </label>
                        <%-- <asp:TextBox ID="txtCategoryName" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtCategoryName" runat="server" Display="Dynamic" ControlToValidate="txtName" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group" style="display: none;">
                        <label for="exampleInputEmail1">Mobile No <span style="color: red">*</span></label>
                        <asp:TextBox ID="txt_mobileno" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="exampleInputEmail1">Enquiry From</label>
                        <asp:TextBox ID="txt_enquiryFrom" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>




                    <div class="form-group">
                        <label for="exampleInputPassword1">Product<span style="color: red">*</span></label>
                        <asp:TextBox ID="txt_product" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Date and Time</label>
                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control"></asp:TextBox>

                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" BehaviorID="Calendar1"
                            runat="server" TargetControlID="txtOrderDate" OnClientDateSelectionChanged="dateselect" Format="yyyy/MM/dd  HH:mm:ss">
                        </cc1:CalendarExtender>
                    </div>


                    
                    
                     <div class="form-group">
                        <label for="exampleInputPassword1">Agent Assigned<span style="color: red">*</span></label>
                         <asp:TextBox ID="txt_agentAssigned" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                     </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Comment<span style="color: red">*</span></label>
                        <asp:TextBox ID="txt_comment" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                     </div>
                    


                </div>
                <!-- /.box-body -->

                <div class="box-footer">
                     <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" class="btn btn-primary" OnClick="btnCancel_Click" Text="Cancel" />
                      
                    
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
    
</asp:Content>

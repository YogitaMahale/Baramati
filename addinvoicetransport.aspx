<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addinvoicetransport.aspx.cs" Inherits="addinvoicetransport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="Template/dist/css/AdminLTE.min.css" />

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
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">

                            <div class="form-group row">

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Transporter Name<span style="color: red">*</span> </label>
                                    <%--<asp:ListBox ID="lstColor" runat="server" class="form-control select2"></asp:ListBox>
                                        <asp:HiddenField ID="hfcolorid" runat="server" />
                                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                    --%>
                                    <asp:ListBox ID="lstTransporter" runat="server" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hftransporter" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Customer Name<span style="color: red">*</span> </label>
                                    <asp:ListBox ID="lstCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCustomer_SelectedIndexChanged" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfcustomer" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">LR No </label>
                                    <asp:TextBox ID="txtlrno" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtlrno" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmobileno" ValidationGroup="c1" ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />--%>
                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">LR Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice No<span style="color: red">*</span> </label>

                                    <asp:ListBox ID="lstinvoiceno" runat="server" OnSelectedIndexChanged="lstinvoiceno_SelectedIndexChanged" AutoPostBack="true" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfinvoiceno" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice Date</label>
                                    <asp:TextBox ID="txtinvoicedate" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Freight Amount</label>
                                    <asp:TextBox ID="txtfreightamount" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Grand Total</label>
                                    <asp:TextBox ID="txttotal" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />--%>
                                </div>
                            </div>
                        </div>





                        <%--</div>--%>
                    </div>
                    <!-- /.box-body -->


                </div>
                <!-- /.box -->

                <!-- Form Element sizes -->

                <!-- /.box -->


                <!-- /.box -->

                <!-- Input addon -->

                <!-- /.box -->


                <!--/.col (left) -->
                <!-- right column -->

                <!--/.col (right) -->

            </div>

        </ContentTemplate>

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

    
    <%--<script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>--%>


    <script type="text/javascript">
        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }
    </script>

    <script type="text/javascript">
        function initDropDowns() {

            $("#<%=lstCustomer.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstCustomer.ClientID%>").select2("val"));
                    $('[id*=hfcustomer]').val($(this).val());
                });


            $("#<%=lstTransporter.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstTransporter.ClientID%>").select2("val"));
                    $('[id*=hftransporter]').val($(this).val());
                });

            
            $("#<%=lstinvoiceno.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstinvoiceno.ClientID%>").select2("val"));
                    $('[id*=hfinvoiceno]').val($(this).val());
                });

        }

    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            initDropDowns();

        });

    </script>


</asp:Content>

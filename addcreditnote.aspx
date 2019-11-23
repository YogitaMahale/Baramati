<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addcreditnote.aspx.cs" Inherits="addcreditnote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">

                            <div class="form-group row">

                                
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Customer Name<span style="color: red">*</span> </label>
                                    <asp:ListBox ID="lstCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCustomer_SelectedIndexChanged" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfcustomer" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Credit Note Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" ReadOnly="true" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Reason<span style="color: red">*</span> </label>

                                    <asp:ListBox ID="lstreason" runat="server" class="form-control select2">
                                        <asp:ListItem Text="Sales Return" Value="Sales Return" />
                                        <asp:ListItem Text="Post Sale Discount" Value="Post Sale Discount" />
                                        <asp:ListItem Text="Claim Adjustment" Value="Claim Adjustment" />
                                    </asp:ListBox>
                                    <asp:HiddenField ID="hfreason" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="lstreason" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>

                            </div>
                            
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Invoice No<span style="color: red">*</span> </label>

                                    <asp:ListBox ID="lstinvoiceno" runat="server" OnSelectedIndexChanged="lstinvoiceno_SelectedIndexChanged" AutoPostBack="true" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfinvoiceno" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Invoice Date</label>
                                    <asp:TextBox ID="txtinvoicedate" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                                </div>

                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Delivered Through</label>
                                    <asp:TextBox ID="txttransporter" CssClass="form-control" runat="server" ReadOnly="true" ></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Delivered Details</label>
                                    <asp:TextBox ID="txtdeliverydetails" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                                </div>
                                
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Sub Total</label>
                                    <asp:TextBox ID="txtsubtotal" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Trade Discount(%) & Amount</label>
                                    <%--<asp:RadioButton ID="rdbtntrade" Checked="true" runat="server" Text="Trade Discount(%) & Amount" GroupName="discounttype"/>--%>
                                    <div class="form-group row">
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txttradedisc" OnTextChanged="txttradedisc_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>
                                    </div>
                                        
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txttradeamount" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>
                                    </div>
                                </div>
                                    </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Taxable Amount</label>
                                    <asp:TextBox ID="txttaxable" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">CGST Amount</label>
                                    <asp:TextBox ID="txtcgst" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">SGST Amount</label>
                                    <asp:TextBox ID="txtsgst" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">IGST Amount</label>
                                    <asp:TextBox ID="txtigst" CssClass="form-control" ReadOnly="true" runat="server" Text ="0"></asp:TextBox>

                                </div>
            </div>
            <div class="form-group row">
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Total GST</label>
                                    <asp:TextBox ID="txttotalgst" CssClass="form-control" runat="server" ReadOnly="true" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Taxable Discount(%) & Amount</label>
                                    <%--<asp:RadioButton ID="rdbtntaxable" runat="server" Text="Taxable Discount(%) & Amount" GroupName="discounttype"/>--%>
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txttaxabledisc" CssClass="form-control" OnTextChanged="txttaxabledisc_TextChanged" AutoPostBack="true" runat="server" Text ="0"></asp:TextBox>
                                    </div>
                                <div class="col-xs-6">
                                    <asp:TextBox ID="txttaxableamount" CssClass="form-control" ReadOnly="true" runat="server" Text ="0"></asp:TextBox>
                                    </div>
                                    </div>
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Total Amount</label>
                                    <asp:TextBox ID="txttotal" CssClass="form-control" ReadOnly="true" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Other Amount</label>
                                    <asp:TextBox ID="txtother" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Freight Discount</label>
                                    <asp:TextBox ID="txtfreightdiscount" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Freight Amount</label>
                                    <asp:TextBox ID="txtfreightamount" ReadOnly="true" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">
                                
                                </div>
                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-info" OnClick="btnCancel_Click" Text="CANCEL" />
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


            $("#<%=lstreason.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstreason.ClientID%>").select2("val"));
                    $('[id*=hfreason]').val($(this).val());
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


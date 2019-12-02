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
                          
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                            <div>
                              <h3 class="box-title" style="text-align: center"><b id="spnMessgae" runat="server"></b></h3>
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
                                    <asp:TextBox ID="txttransporter" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Delivered Details</label>
                                    <asp:TextBox ID="txtdeliverydetails" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>

                                </div>

                            </div>
                            <div class="form-group row" style="overflow: scroll;">
                                <div class="col-xs-12">

                                    <table class="table table-hover table-checkable order-column full-width" id="example4">
                                        <thead>
                                            <tr>
                                                <th>sr</th>
                                                <th>Product</th>
                                                <th>Brand</th>
                                                <th>Size</th>
                                                <th>Color</th>
                                                <th>Cart</th>
                                                <th>Pack</th>
                                                <th>Quanity</th>
                                                <th>Mrp</th>
                                                <th>Unit Rate</th>
                                                <th>SubTotal</th>
                                                <th>Discount</th>
                                                <th>Scheme</th>
                                                <th>Taxable Amt</th>
                                                <th>CGST</th>
                                                <th>SGST</th>
                                                <th>IGST</th>
                                                <th>GST amt</th>
                                                <th>Total</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td class="center">
                                                            <asp:Label ID="txtsr" runat="server" Text='<%# Eval("sr") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtproductName" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                                                            <asp:Label ID="rep_txtproductid" runat="server" Text='<%# Eval("pid") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtBrand" runat="server" Text='<%# Eval("brandid") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">

                                                            <asp:Label ID="rep_txtSize" runat="server" Text='<%# Eval("sizeid") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtColor" runat="server" Text='<%# Eval("colorid") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">

                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("cart") %>'></asp:Label>
                                                            <%--<asp:TextBox ID="rep_txtCart" Width="50" AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged" runat="server" Text=' <%#Eval("cart")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtpacking" runat="server" Text='<%# Eval("pack") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtMrp" runat="server" Text='<%# Eval("mrp") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtUnitRate" runat="server" Text='<%# Eval("unitRate") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtSubtotal" runat="server" Text='<%# Eval("subTotal") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtDiscount" runat="server" Text='<%# Eval("discount") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtScheme" runat="server" Text='<%# Eval("scheme") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtTaxableAmt" runat="server" Text='<%# Eval("taxableamt") %>'></asp:Label>


                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtCGST" runat="server" Text='<%# Eval("CGSTper") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtSGST" runat="server" Text='<%# Eval("SGSTper") %>'></asp:Label>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtIGST" runat="server" Text='<%# Eval("IGSTper") %>'></asp:Label>


                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtGSTamt" runat="server" Text='<%# Eval("GSTamt") %>'></asp:Label>


                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtfinalTotal" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>

                                                        </td>

                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>
                            </div>


                            <div class="form-group row">

                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Sub Amount </label>
                                    <asp:TextBox ID="txt_Subtotal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>

                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Total GST Amt</label>
                                    <asp:TextBox ID="txtTotalGstAmt" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Trad Dis(%) And Scheme </label>
                                    <table>
                                        <tr>

                                            <td>
                                                <asp:TextBox ID="txttradDis" class="form-control" Width="70" runat="server" AutoPostBack="true" OnTextChanged="txttradDis_TextChanged" ></asp:TextBox>

                                            </td>


                                            <td>
                                                <asp:TextBox ID="txttradAmt" Enabled="false" Width="70" class="form-control" Text="0" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>


                                    
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Taxable dis(%) Amount </label>

                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txttaxableDis" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txttaxableDis_TextChanged"></asp:TextBox></td>


                                            <td>
                                                <asp:TextBox ID="txttaxableDisamt" Enabled="false" Text="0" class="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>





                                </div>
                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Taxable Amount </label>
                                    <asp:TextBox ID="txttaxableAmt" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Total Amount</label>
                                    <asp:TextBox ID="txttotalAmt" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">CGST Amount </label>
                                    <asp:TextBox ID="txtcsgtfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>

                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">SGST Amount</label>
                                    <asp:TextBox ID="txtsgstfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">IGST Amount </label>
                                    <asp:TextBox ID="txtIgstfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Other Amount </label>
                                    <asp:TextBox ID="txtotherAmt" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtotherAmt_TextChanged"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Freight Amount </label>
                                    <asp:TextBox ID="txtfreightdis" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtfreightdis_TextChanged"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Grand Amount</label>
                                    <asp:TextBox ID="txttotalAmtfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Due Date </label>

                                    <asp:TextBox ID="txtduedate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtduedate" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtduedate" Format="yyyy/MM/dd"></cc1:CalendarExtender>

                                </div>

                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Reference by</label>
                                    <asp:TextBox ID="txtreferenceby" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Delivered Through </label>
                                    <asp:TextBox ID="txtdeliveredthrough" class="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Delivered Details </label>
                                    <asp:TextBox ID="txtdelivereddetails" class="form-control" runat="server"></asp:TextBox>

                                </div>

                            </div>
                            <%--  --%>
                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server"   CssClass="btn btn-info" OnClick="btnCancel_Click" Text="CANCEL" />
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


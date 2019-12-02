<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="adddebitnote.aspx.cs" Inherits="adddebitnote" %>

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
                                    <label for="exampleInputEmail1">Supplier Name<span style="color: red">*</span> </label>
                                    <asp:ListBox ID="lstCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCustomer_SelectedIndexChanged" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfcustomer" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Debit Note Date</label>
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


                            </div>
                            <div class="form-group row" style="overflow: scroll;">
                                <div class="col-md-12">

                                    <table class="table table-hover table-checkable order-column full-width" id="example4">
                                        <thead>
                                            <tr>
                                                <th>sr</th>
                                                <td>Product</td>
                                                <td>Quantity</td>
                                                <td>Rate</td>
                                                <td>SubTotal</td>
                                                <td>Discount</td>
                                                <td>Scheme</td>
                                                <td>Frieght Amount</td>
                                                <td>Taxable Amt</td>
                                                <td>CGST</td>
                                                <td>SGST</td>
                                                <td>IGST</td>
                                                <td>GST amt</td>
                                                <td>Total</td>
                                                <td>Net Rate</td>
                                                
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                       <td class="center">
                                                            <asp:Label ID="txtsr" runat="server" Text='<%# Eval("sr") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="txtsr" ReadOnly="true" Width="20" runat="server" Text=' <%#Eval("sr")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtproductName" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                                                            <asp:Label ID="rep_txtproductid" Visible="false" runat="server" Text='<%# Eval("pid") %>'></asp:Label>

                                                        </td>

                                                        <td>
                                                            <asp:Label ID="rep_txtQty" Text='<%# Eval("qty") %>' AutoPostBack="true"  Width="70" runat="server" Enabled="true" placeholder="Qty"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtRate" Text='<%# Eval("rate") %>' Width="70" runat="server" Enabled="false" placeholder="MRP"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtSubTotal" Text='<%# Eval("subtotal") %>' Width="70" Enabled="false" runat="server" placeholder="SubTotal"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtDiscount" Text='<%# Eval("discount") %>' Width="70" Enabled="false" runat="server" placeholder="Dis(%)"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtScheme" Text='<%# Eval("scheme") %>' Width="70" Enabled="false" runat="server" placeholder="Scheme"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtFrieghtAmt" Text='<%# Eval("frieghtamt") %>' Enabled="false" Width="70" runat="server" placeholder="Cart" AutoPostBack="true"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txttaxable" Text='<%# Eval("taxableamt") %>' Width="70" Enabled="false" runat="server" placeholder="Taxable"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtCGST" Text='<%# Eval("csgtper") %>' Width="70" Enabled="false" runat="server" placeholder="CGST"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtSgst" Text='<%# Eval("sgstper") %>' Width="70" Enabled="false" runat="server" placeholder="SGST"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtIgst" Text='<%# Eval("igstper") %>' Width="70" Enabled="false" runat="server" placeholder="IGST"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtGSTtotal" Text='<%# Eval("gstamt") %>' Width="70" Enabled="false" runat="server" placeholder="GSTtotal"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtTotal" Text='<%# Eval("total") %>' Width="70" Enabled="false" runat="server" placeholder="Total"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtNetRate" Text='<%# Eval("netrate") %>' Width="70" runat="server" Enabled="false" placeholder="UnitRate"></asp:Label></td>
                                                         
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>
                        </div>
                        <div class="form-group row">

                            <div class="col-xs-3">
                                <label for="exampleInputEmail1">Sub Amount </label>
                                <asp:TextBox ID="txt_Subtotal" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                            </div>

                            <div class="col-xs-3">
                                <label for="exampleInputEmail1">Dis  And Scheme </label>

                                <asp:TextBox ID="txttradDis" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txttradDis_TextChanged"></asp:TextBox></td>
                                            <%--<td>
                                                <asp:TextBox ID="txttradAmt" Enabled="false" Width="70" class="form-control" Text="0" runat="server"></asp:TextBox></td>--%>
                            </div>
                            <div class="col-xs-3">

                                <label for="exampleInputEmail1">Frieght Amount </label>
                                <asp:TextBox ID="txtFriegtAmt" class="form-control" AutoPostBack="true" OnTextChanged="txtFriegtAmt_TextChanged"  runat="server"></asp:TextBox>

                            </div>

                            <div class="col-xs-3">

                                <label for="exampleInputEmail1">Taxable Amount </label>
                                <asp:TextBox ID="txttaxableAmt" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>



                        </div>
                        <div class="form-group row">

                            <div class="col-xs-3">

                                <label for="exampleInputEmail1">CGST Amount </label>
                                <asp:TextBox ID="txtcsgtfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>

                            <div class="col-xs-3">
                                <label for="exampleInputEmail1">SGST Amount</label>
                                <asp:TextBox ID="txtsgstfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-xs-3">
                                <label for="exampleInputEmail1">IGST Amount </label>
                                <asp:TextBox ID="txtIgstfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-xs-3">

                                <label for="exampleInputEmail1">Total Amount</label>
                                <asp:TextBox ID="txttotalAmt" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>

                        </div>

                        <div class="form-group row">
                            <div class="col-xs-2">

                                <label for="exampleInputEmail1">Transport Amount</label>
                                <asp:TextBox ID="txttransport" class="form-control" runat="server" OnTextChanged="txttransport_TextChanged"  AutoPostBack="true" ></asp:TextBox>

                            </div>
                            <div class="col-xs-2">

                                <label for="exampleInputEmail1">Packing Amount</label>
                                <asp:TextBox ID="txtpacking" class="form-control" runat="server" OnTextChanged="txtpacking_TextChanged"  AutoPostBack="true" ></asp:TextBox>

                            </div>
                            <div class="col-xs-2">
                                <label for="exampleInputEmail1">Other Amount </label>
                                <asp:TextBox ID="txtotherAmt" class="form-control" runat="server" OnTextChanged="txtotherAmt_TextChanged"  AutoPostBack="true" ></asp:TextBox>
                            </div>

                            <div class="col-xs-3" style="display: none;">
                                <label for="exampleInputEmail1">Discount Amount </label>
                                <asp:TextBox ID="txtdiscountamt" class="form-control" runat="server" Text="0"></asp:TextBox>
                            </div>

                            <div class="col-xs-3">
                                <label for="exampleInputEmail1">Grand Amount</label>
                                <asp:TextBox ID="txttotalAmtfinal" Enabled="false" class="form-control" runat="server"></asp:TextBox>

                            </div>
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


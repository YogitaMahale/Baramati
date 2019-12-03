<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditponew.aspx.cs" Inherits="addeditponew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">

                            <div class="form-group row">

                                <div class="col-xs-4">

                                    <label for="exampleInputEmail1">Supplier Name </label>

                                    <asp:ListBox ID="ddlname" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlname_SelectedIndexChanged" class="form-control select2"></asp:ListBox>
                                     <%--<asp:ListBox ID="ddlname" runat="server" AutoPostBack="True" onkeydown="return focusOnNext(event, 'ddlPaymentType')" OnSelectedIndexChanged="ddlname_SelectedIndexChanged" class="form-control select2"></asp:ListBox>--%>
                                    <asp:HiddenField ID="hfddlname" runat="server" />

                                  <%--  <asp:DropDownList ID="ddlname" CssClass="form-control" Width="350px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlname_SelectedIndexChanged">
                                    </asp:DropDownList>--%>

                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlname" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Mobile</label>
                                    <asp:TextBox ID="txtPhone" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPhone" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Address </label>
                                    <asp:TextBox ID="txtAddress" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAddress" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <div class="form-group row">

                                <div class="col-xs-4" style="display: none;">
                                    <label for="exampleInputEmail1">Payment Type </label>
                                    <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="0">--select--</asp:ListItem>
                                        <asp:ListItem>Counter Cash</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlPaymentType" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>


                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Billing Type</label>

                                    <asp:DropDownList ID="ddlinvoiceType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoiceType_SelectedIndexChanged" >
                                        <asp:ListItem Value="0">-- select -- </asp:ListItem>
                                        <asp:ListItem Value="Including GST">Including GST</asp:ListItem>
                                        <asp:ListItem Value="Excluding GST">Excluding GST</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlinvoiceType" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice No </label>

                                    <asp:TextBox ID="txt_InvoieNo" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_InvoieNo" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Payment Mode </label>
                                    <br />
                                    &nbsp;&nbsp;
                      <asp:RadioButton ID="rdoCash" runat="server" Text="Cash" GroupName="aa" Checked="true" />
                                    &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdoCredit" runat="server" Text="Credit" GroupName="aa" Checked="false" />


                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Invoice Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Account Year</label>

                                    <asp:DropDownList ID="ddlaccountYear" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="0">-- select -- </asp:ListItem>
                                        <asp:ListItem>2019-2020</asp:ListItem>
                                        <asp:ListItem>2020-2021</asp:ListItem>
                                        <asp:ListItem>2021-2022</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Stock Date</label>
                                    <asp:TextBox ID="txtStockDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtStockDate" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStockDate" Format="yyyy/MM/dd"></cc1:CalendarExtender>

                                </div>

                            </div>
                            <div class="form-group row" style="overflow: scroll;">
                                <div class="col-md-12">
                                    <%-- <table>
                                        <tr>
                                            <td>Product</td>
                                            <td>Quantity</td>
                                            <td>Rate</td>
                                            <td>SubTotal</td>
                                            <td>Discount</td>
                                            <td>Scheme</td>
                                            <td>Frieght </td>
                                            <td>Taxable </td>
                                            <td>CGST</td>
                                            <td>SGST</td>
                                            <td>IGST</td>
                                            <td>GST amt</td>
                                            <td>Total</td>
                                            <td>Net Rate</td>
                                             

                                        </tr>
                                        <tr>
                                            <td>
                                                 <asp:DropDownList ID="ddlProduct" CssClass="form-control"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                    </asp:DropDownList>
                                               
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" Width="70" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"  placeholder="Qty" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtRate" Width="70" runat="server" Enabled="false" placeholder="MRP" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtSubTotal" Width="70" Enabled="false" runat="server" placeholder="SubTotal" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDiscount" Width="70" Enabled="false"  runat="server" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" placeholder="Dis(%)" ></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtScheme" Width="70" Enabled="false" runat="server" placeholder="Scheme" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtFrieghtAmt" Width="70"  Enabled="false"  runat ="server" placeholder="Cart" AutoPostBack="true"  Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txttaxable" Width="70" Enabled="false" runat="server" placeholder="Taxable" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtCGST" Width="70" Enabled="false" runat="server" placeholder="CGST" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtSgst" Width="70" Enabled="false" runat="server" placeholder="SGST" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtIgst" Width="70" Enabled="false" runat="server" placeholder="IGST" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtGSTtotal" Width="70" Enabled="false" runat="server" placeholder="GSTtotal" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtTotal" Width="70" Enabled="false" runat="server" placeholder="Total" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNetRate" Width="70" runat="server" Enabled="false" placeholder="UnitRate" Text="0"></asp:TextBox></td>
                                           
                                        </tr>
                                        

                                        
                                    </table>--%>
                                </div>

                            </div>
                            <div>
                            </div>
                            <div class="form-group row" style="overflow: scroll;">
                                <div class="col-md-12">

                                    <table class="table table-hover table-checkable order-column full-width" id="example4"  >
                                        <thead>
                                            <tr>
                                                <th><b>sr</b></th>
                                                <td><b>Product</b></th>
                                                <td><b>Quantity</b></th>
                                                <td><b>Rate</b></th>
                                                <td><b>SubTotal</b></th>
                                                <td><b>Discount</b></th>
                                                <td><b>Scheme</b></th>
                                                <td><b>Frieght Amount</b></th>
                                                <td><b>Taxable Amt</b></th>
                                                <td><b>CGST</b></th>
                                                <td><b>SGST</b></th>
                                                <td><b>IGST</b></th>
                                                <td><b>GST amt</b></th>
                                                <td><b>Total</b></th>
                                                <td><b>Net Rate</b></th>
                                                <th><b>Action</b></th>
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
                                                            <asp:TextBox ID="rep_txtQty" Text='<%# Eval("qty") %>' AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged" Width="70" runat="server" Enabled="true" placeholder="Qty"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtRate" Text='<%# Eval("rate") %>' Width="70" runat="server" Enabled="false" placeholder="MRP"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="rep_txtSubTotal" Text='<%# Eval("subtotal") %>' Width="70" Enabled="false" runat="server" placeholder="SubTotal"></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="rep_txtDiscount" Text='<%# Eval("discount") %>' Width="70"  AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged"  runat="server" placeholder="Dis(%)" ></asp:TextBox></td>
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
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" class="btn btn-danger" OnClientClick="return confirm('Do you want to delete this  ?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                        <tfoot style="background-color: #ecf0f5;border-style:solid;border:thin;">
                                            <th>
                                                <asp:Button ID="Button1" runat="server" Text="Add" BackColor="#3c8dbc" ForeColor="White"  OnClick="Button1_Click" /></th>
                                            <td>
                                              <%--  <asp:DropDownList ID="ddlProduct" Width="150" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                </asp:DropDownList>--%>

                                                  <asp:ListBox ID="ddlProduct" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" Width="70" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" placeholder="Qty" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="txtRate"  runat="server" Enabled="false" placeholder="MRP" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtSubTotal"  Enabled="false" runat="server" placeholder="SubTotal" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDiscount" Width="70" runat="server" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" placeholder="0"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="txtScheme"  Enabled="false" runat="server" placeholder="Scheme" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtFrieghtAmt"  Enabled="false" runat="server" placeholder="Cart" AutoPostBack="true" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txttaxable" Enabled="false" runat="server" placeholder="Taxable" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtCGST" Enabled="false" runat="server" placeholder="CGST" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtSgst"  Enabled="false" runat="server" placeholder="SGST" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtIgst"  Enabled="false" runat="server" placeholder="IGST" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtGSTtotal"  Enabled="false" runat="server" placeholder="GSTtotal" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtTotal"  Enabled="false" runat="server" placeholder="Total" Text="0"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtNetRate"  runat="server" Enabled="false" placeholder="UnitRate" Text="0"></asp:Label></td>
                                            <th></th>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <%--lggg--%>
                            <div class="form-group row">

                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Sub Amount </label>
                                    <asp:TextBox ID="txt_Subtotal" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Dis  And Scheme </label>

                                    <asp:TextBox ID="txttradDis" Enabled="false"  class="form-control" runat="server" Text="0"></asp:TextBox></td>
                                            <%--<td>
                                                <asp:TextBox ID="txttradAmt" Enabled="false" Width="70" class="form-control" Text="0" runat="server"></asp:TextBox></td>--%>
                                </div>
                                <div class="col-xs-3">

                                    <label for="exampleInputEmail1">Frieght Amount </label>
                                    <asp:TextBox ID="txtFriegtAmt" class="form-control" AutoPostBack="true" OnTextChanged="txtFriegtAmt_TextChanged" runat="server"></asp:TextBox>

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
                                    <asp:TextBox ID="txttransport" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txttransport_TextChanged"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Packing Amount</label>
                                    <asp:TextBox ID="txtpacking" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtpacking_TextChanged"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Other Amount </label>
                                    <asp:TextBox ID="txtotherAmt" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtotherAmt_TextChanged1"></asp:TextBox>
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

                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="box-footer" style="text-align: center">

                                        <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="gg" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>













                    </div>

                </div>
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

    <script src="Template/dist/js/canvasjs.min.js"></script>


    <%--********************************--%>
    <script type="text/javascript">
        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }
    </script>

    <script type="text/javascript">
        function initDropDowns() {

            $("#<%=ddlname.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=ddlname.ClientID%>").select2("val"));
                    $('[id*=hfddlname]').val($(this).val());
                });

 
      $("#<%=ddlProduct.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=ddlProduct.ClientID%>").select2("val"));
                    $('[id*=HiddenField1]').val($(this).val());
                });

        }

    </script>
     

    <script type="text/javascript">
        $(document).ready(function () {

            initDropDowns();

        });

    </script>

   <%-- <script type="text/javascript">
        function focusOnNext(e, nextControl) {
            alert(e.keyCode);
            if (e.keyCode == 13) {
                $("." + nextControl).focus();
                   
                return false;
            }
            
        }
    </script>--%>
</asp:Content>



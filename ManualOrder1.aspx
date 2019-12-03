<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="ManualOrder1.aspx.cs" Inherits="ManualOrder1" %>

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

                                    <label for="exampleInputEmail1">Customer Name </label>
                                    <%-- <asp:DropDownList ID="ddlname" CssClass="form-control" Width="350px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlname_SelectedIndexChanged">
                                    </asp:DropDownList>--%>

                                    <asp:ListBox ID="ddlname" runat="server" class="form-control select2" AutoPostBack="True" TabIndex="1" onkeyup="MyKeyUpEvent();"   OnSelectedIndexChanged="ddlname_SelectedIndexChanged"></asp:ListBox>
                                    <asp:HiddenField ID="hfddlnameid" runat="server" />
                                    <asp:HiddenField ID="hfddlname1" runat="server" />
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

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Payment Type </label>
                                    <asp:DropDownList ID="ddlPaymentType" TabIndex="2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlPaymentType_TextChanged">
                                        <asp:ListItem Value="0">--select--</asp:ListItem>
                                        <asp:ListItem>Counter Cash</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlPaymentType" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>


                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice Type</label>

                                    <asp:DropDownList ID="ddlinvoiceType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoiceType_SelectedIndexChanged" TabIndex="3">
                                        <asp:ListItem Value="0">-- select -- </asp:ListItem>
                                        <asp:ListItem>GST Invoice</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlinvoiceType" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice No </label>

                                    <asp:TextBox ID="txt_InvoieNo" Enabled="false" class="form-control" runat="server" TabIndex="4"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Payment Mode </label>
                                    <br />
                                    &nbsp;&nbsp;
                      <asp:RadioButton ID="rdoCash" runat="server" Text="Cash" GroupName="aa" Checked="true" TabIndex="5" />
                                    &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdoCredit" runat="server" Text="Credit" GroupName="aa" Checked="false" TabIndex="6" />


                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Invoice Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" Enabled="false"  class="form-control" autocomplete="off" TabIndex="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-xs-4">
                                </div>
                            </div>
                            <div class="form-group row" style="overflow: scroll;">
                                <div class="col-xs-12">
                                    <%--  <table>
                                        <tr>
                                            <td>Product</td>
                                            <td>Brand</td>
                                            <td>Size</td>
                                            <td>Color</td>
                                            <td>Cart</td>
                                            <td>Pack</td>
                                            <td>Quanity</td>
                                            <td>Mrp</td>
                                            <td>Unit Rate</td>
                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                

                                                <asp:ListBox ID="ddlProduct" runat="server" class="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:ListBox>
                                                <asp:HiddenField ID="ddlProductid" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlProduct" ValidationGroup="bb" runat="server" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"></asp:RequiredFieldValidator>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBrand" Width="70" runat="server" Enabled="false" placeholder="Brand" Text=""></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtSize" Width="70" runat="server" Enabled="false" placeholder="Size" Text=""></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtColor" Width="70" runat="server" Enabled="false" placeholder="Color" Text=""></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtCart" Width="70" runat="server" placeholder="Cart" AutoPostBack="true" OnTextChanged="txtCart_TextChanged" Text="0"></asp:TextBox></td>
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator6"
                                                runat="server"
                                                ValidationGroup="bb"
                                                ControlToValidate="txtCart"
                                                ValidationExpression="^\d+"
                                                ErrorMessage="Enter only Digit in Cart" Font-Bold="True" Font-Size="Medium"></asp:RegularExpressionValidator>
                                            <td>
                                                <asp:TextBox ID="txtPack" Width="70" runat="server" Enabled="false" placeholder="Pack" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtQty" Width="70" runat="server" Enabled="false" placeholder="Qty" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtMrp" Width="70" runat="server" Enabled="false" placeholder="MRP" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtUnitRate" Width="70" runat="server" Enabled="false" placeholder="UnitRate" Text="0"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>SubTotal</td>
                                            <td>Discount</td>
                                            <td>Scheme</td>
                                            <td>Taxable Amt</td>
                                            <td>CGST</td>
                                            <td>SGST</td>
                                            <td>IGST</td>
                                            <td>GST amt</td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSubTotal" Width="200" Enabled="false" runat="server" placeholder="SubTotal" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDiscount" Width="70" Enabled="false" runat="server" placeholder="Dis(%)" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtScheme" Width="70" Enabled="false" runat="server" placeholder="Scheme" Text="0"></asp:TextBox></td>
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
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" ValidationGroup="bb" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>--%>
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
                                                <th>Action</th>
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
                                                            <asp:Label ID="rep_txtproductid" runat="server" Text='<%# Eval("pid") %>'></asp:Label>


                                                            <%--<asp:TextBox ID="rep_txtproductName" ReadOnly="true" Width="100" runat="server" Text=' <%#Eval("productName")%>'></asp:TextBox>--%>
                                                            <%--<asp:TextBox ID="rep_txtproductid" runat="server" Visible="false" Text=' <%#Eval("pid")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtBrand" runat="server" Text='<%# Eval("brandid") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtBrand" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("brandid")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">

                                                            <asp:Label ID="rep_txtSize" runat="server" Text='<%# Eval("sizeid") %>'></asp:Label>
                                                            <%--<asp:TextBox ID="rep_txtSize" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("sizeid")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtColor" runat="server" Text='<%# Eval("colorid") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtColor" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("colorid")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">


                                                            <asp:TextBox ID="rep_txtCart" Width="50" AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged" runat="server" Text=' <%#Eval("cart")%>'></asp:TextBox>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtpacking" runat="server" Text='<%# Eval("pack") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtpacking" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("pack")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtqty" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("qty")%>'></asp:TextBox>--%>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtMrp" runat="server" Text='<%# Eval("mrp") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtMrp" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("mrp")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtUnitRate" runat="server" Text='<%# Eval("unitRate") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtUnitRate" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("unitRate")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtSubtotal" runat="server" Text='<%# Eval("subTotal") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtSubtotal" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("subTotal")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:TextBox ID="rep_txtDiscount" runat="server" Width="70"  AutoPostBack="true" OnTextChanged="rep_txtCart_TextChanged" Text='<%# Eval("discount") %>'></asp:TextBox>

                                                            <%--<asp:TextBox ID="rep_txtDiscount" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("discount")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtScheme" runat="server" Text='<%# Eval("scheme") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtScheme" ReadOnly="true" Width="40" runat="server" Text=' <%#Eval("scheme")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtTaxableAmt" runat="server" Text='<%# Eval("taxableamt") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtTaxableAmt" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("taxableamt")%>'></asp:TextBox>--%>

                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtCGST" runat="server" Text='<%# Eval("CGSTper") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtCGST" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("CGSTper")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtSGST" runat="server" Text='<%# Eval("SGSTper") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtSGST" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("SGSTper")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtIGST" runat="server" Text='<%# Eval("IGSTper") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtIGST" ReadOnly="true" Width="50" runat="server" Text=' <%#Eval("IGSTper")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtGSTamt" runat="server" Text='<%# Eval("GSTamt") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtGSTamt" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("GSTamt")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td class="center">
                                                            <asp:Label ID="rep_txtfinalTotal" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>

                                                            <%--<asp:TextBox ID="rep_txtfinalTotal" ReadOnly="true" Width="80" runat="server" Text=' <%#Eval("TotalAmount")%>'></asp:TextBox>--%>
                                                        </td>
                                                        <td style="text-align: center">


                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" class="btn btn-danger" OnClientClick="return confirm('Do you want to delete this  ?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                        <tfoot  style="background-color: #ecf0f5;border-style:solid;border:thin;">
                                            <tr>
                                                <th><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" ValidationGroup="bb" BackColor="#3c8dbc" ForeColor="White" OnClick="btnAdd_Click" TabIndex="11" /></th>
                                                <th>
                                                    <asp:ListBox ID="ddlProduct" runat="server" class="form-control select2" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" TabIndex="8"></asp:ListBox>
                                                    <asp:HiddenField ID="ddlProductid" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlProduct" ValidationGroup="bb" runat="server" ErrorMessage="*" Font-Bold="True" Font-Size="Medium"></asp:RequiredFieldValidator>

                                                </th>
                                                <th>
                                                    <asp:Label  ID="txtBrand"  runat="server" Enabled="false" placeholder="Brand" Text=""></asp:Label>

                                                </th>
                                                <th>
                                                    <asp:Label ID="txtSize" runat="server" Enabled="false" placeholder="Size" Text=""></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtColor"  runat="server" Enabled="false" placeholder="Color" Text=""></asp:Label></th>
                                                <th>
                                                    <asp:TextBox ID="txtCart" Width="70"  runat="server" placeholder="Cart" AutoPostBack="true"  OnTextChanged="txtCart_TextChanged" Text="0" TabIndex="9"></asp:TextBox></td>
                                            <asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator6"
                                                runat="server"
                                                ValidationGroup="bb"
                                                ControlToValidate="txtCart"
                                                ValidationExpression="^\d+"
                                                ErrorMessage="Enter only Digit in Cart" Font-Bold="True" Font-Size="Medium"></asp:RegularExpressionValidator></th>
                                                <th>
                                                    <asp:Label ID="txtPack" runat="server" Enabled="false" placeholder="Pack" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtQty"  runat="server" Enabled="false" placeholder="Qty" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtMrp"  runat="server" Enabled="false" placeholder="MRP" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtUnitRate"  runat="server" Enabled="false" placeholder="UnitRate" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtSubTotal" Enabled="false" runat="server" placeholder="SubTotal" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:TextBox ID="txtDiscount" Width="70" Enabled="true" runat="server" AutoPostBack="true" OnTextChanged="txtCart_TextChanged" placeholder="Dis(%)" Text="0" TabIndex="10"></asp:TextBox></th>
                                                <th>
                                                    <asp:Label  ID="txtScheme"    Enabled="false" runat="server" placeholder="Scheme" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txttaxable"  Enabled="false" runat="server" placeholder="Taxable" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtCGST"  Enabled="false" runat="server" placeholder="CGST" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtSgst" Enabled="false" runat="server" placeholder="SGST" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtIgst"  Enabled="false" runat="server" placeholder="IGST" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtGSTtotal"  Enabled="false" runat="server" placeholder="GSTtotal" Text="0"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="txtTotal"  Enabled="false" runat="server" placeholder="Total" Text="0"></asp:Label></th>
                                                <th>
                                                    </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <%---------------%>
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
                                                <asp:TextBox ID="txttradDis" class="form-control" Width="70" Enabled="false"  runat="server" AutoPostBack="true" OnTextChanged="txttradDis_TextChanged"></asp:TextBox>

                                            </td>


                                            <td>
                                                <asp:TextBox ID="txttradAmt" Enabled="false" Width="70" class="form-control" Text="0" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtAddress" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="txtotherAmt" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtotherAmt_TextChanged" TabIndex="12"></asp:TextBox>

                                </div>
                                <div class="col-xs-2">

                                    <label for="exampleInputEmail1">Freight Disc. </label>
                                    <asp:TextBox ID="txtfreightdis" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtfreightdis_TextChanged" TabIndex="13"></asp:TextBox>

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
                            <div class="form-group row">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="gg" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
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



            $("#<%=ddlProduct.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
                //alert("Selected value is: "+$("#<%=ddlProduct.ClientID%>").select2("val"));
                    $('[id*=ddlProductid]').val($(this).val());
                });

            $("#<%=ddlname.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
                //alert("Selected value is: "+$("#<%=ddlname.ClientID%>").select2("val"));
                    $('[id*=hfddlnameid]').val($(this).val());
                });
        }

    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            initDropDowns();

        });

    </script>
</asp:Content>


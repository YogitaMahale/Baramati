<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="ManualOrder.aspx.cs" Inherits="ManualOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
        .error {
            color: red;
        }

        .auto-style5 {
            text-align: left;
            width: 138px;
        }

        .auto-style6 {
            width: 138px;
        }

        .auto-style9 {
            text-align: left;
            width: 94px;
        }

        .auto-style10 {
            width: 94px;
        }

        .auto-style11 {
            text-align: left;
            width: 89px;
        }

        .auto-style12 {
            width: 89px;
        }

        .auto-style13 {
            text-align: left;
            width: 52px;
        }

        .auto-style14 {
            width: 52px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="" style="background-color: white">
                <table class="table table-user-information">
                    <tr id="trMessage" runat="server" visible="false">
                        <td class="text-right" colspan="2">&nbsp;</td>
                        <td colspan="3">
                            <b id="spnMessgae" runat="server"></b>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right" colspan="2">&nbsp;</td>
                        <td class="text-right" colspan="3"><span style="color: red">* Indicates required fields</span> </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">Select User Type</td>
                        <td class="text-right" colspan="3">

                            <asp:DropDownList ID="ddlUserType" CssClass="form-control" Width="500px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                <asp:ListItem>--Select----- </asp:ListItem>
                                <asp:ListItem>Dealer</asp:ListItem>
                                <asp:ListItem>Customer</asp:ListItem>
                     <%--      <asp:ListItem>Stockiest</asp:ListItem>
                                <asp:ListItem>Distributor</asp:ListItem>--%>
                            </asp:DropDownList>

                        </td>
                        <td class="text-right" colspan="4">
                            <asp:RequiredFieldValidator ID="RFVddlCategory" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlUserType" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">Name <span style="color: red">*</span></td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlname" CssClass="form-control" Width="500px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlname_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="text-right" colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="text-right" colspan="2" style="width: 250px; font-weight: bold">Order Date:</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control"></asp:TextBox>
                               <%--<cc1:calendarextender ID="Calendarextender2" runat="server" TargetControlID="txtOrderDate" Format="yyyy/MM/dd">
      </cc1:calendarextender>--%>
                              <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" BehaviorID="Calendar1"
        runat="server" TargetControlID="txtOrderDate" OnClientDateSelectionChanged="dateselect" Format="yyyy/MM/dd  HH:mm:ss">
    </cc1:CalendarExtender>                      </td>
                        <td class="text-right" colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">Address</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <td class="text-right" colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">Email</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">Phone</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td class="text-right" colspan="4"></td>
                    </tr>
                    <tr style="display:none;">
                        <td class="text-right" colspan="2" style="width: 250px; font-weight: bold">Country</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td class="text-right" colspan="4"></td>
                    </tr>
                    
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-left">Product<asp:DropDownList ID="ddlProduct" CssClass="form-control" Width="218px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                        </asp:DropDownList>

                        </td>
                        <td style="width: 250px; font-weight: bold" class="text-left">Price :
                    <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server" Height="27px" Width="124px"></asp:TextBox>
                            <%--NoOfBoxces:--%>
                                <asp:TextBox ID="txtNOB" CssClass="form-control" Visible="false" runat="server" Height="27px" Width="124px"></asp:TextBox>  </td>
                       
                            <cc1:FilteredTextBoxExtender ID="txtPrice_FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" TargetControlID="txtPrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>
                        </td>
                        <td class="auto-style9">
                            <strong>Qty :</strong>
                            <asp:TextBox ID="txtQty" CssClass="form-control" runat="server" AutoPostBack="true"  Height="27px" OnTextChanged="txtdiscounted_TextChanged" Width="124px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVtxtQuantites" runat="server" Display="Dynamic" ControlToValidate="txtQty" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="txtQty_FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" TargetControlID="txtQty" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>
                        </td>
                        <td class="auto-style5">
                            
                            <strong>GST</strong><asp:TextBox ID="txt_GST" CssClass="form-control" runat="server" Height="27px" Width="124px" AutoPostBack="True" ></asp:TextBox>
                              
                        </td>
                        
                        <td class="auto-style11">
                            <strong>Total:</strong><asp:TextBox ID="txttaxabletotal" CssClass="form-control" runat="server" Height="27px" Width="124px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txttaxabletotal_FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" TargetControlID="txttaxabletotal" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>
                        </td>
                     
                        <td class="text-right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 250px; font-weight: bold" class="text-right" colspan="2">&nbsp;</td>
                        <td class="text-right" colspan="3">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnAdd_Click" />&nbsp;&nbsp;

                        </td>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="text-right" colspan="9" style="font-weight: bold">
                            <asp:GridView ID="gvproduct" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="pid" ForeColor="#333333" GridLines="None" Width="1151px" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="pid" HeaderStyle-Width="200px" HeaderText="Pid" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="50" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle Width="50px"  HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Productname" HeaderStyle-Width="50px"  HeaderText="ProductName" ItemStyle-Width="300">
                                        <ControlStyle Font-Strikeout="False" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="productprice"  HeaderText="Price"  ItemStyle-Width="150">
                                        <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Top" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>                                    
                                   
                                    <asp:BoundField DataField="quantites" HeaderText="Qty" ItemStyle-Width="150">
                                        <HeaderStyle Width="150px" />
                                         <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>                                       
                                      <asp:BoundField DataField="gst"  HeaderText="GST" ItemStyle-Width="150">
                                        <HeaderStyle Width="10px"  HorizontalAlign="Right" VerticalAlign="Top"/>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="producttotalprice"  HeaderText="Total" >
                                        <HeaderStyle Width="10px" HorizontalAlign="Right" VerticalAlign="Top" />
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundField>
                                  
                                    



                                     <asp:TemplateField>
                            <ItemTemplate>
                               <%-- <asp:Button ID="Button2" runat="server" Text="Edit"  OnClick="Edit_member1" class="btn btn-primary"
                                    CommandName="Edit" Width="60"></asp:Button>--%>
                                 <asp:Button ID="Button1" runat="server" Text="Remove"  OnClick="Remove_member1" class="btn btn-primary"
                                    CommandName="Remove" Width="60"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>

                                </Columns>
                                <EditRowStyle BackColor="#999999" HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle  BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"   VerticalAlign="Top" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                    </tr>

                    

                    <tr>
                        <td>&nbsp;</td>
                        <td><strong>Total Amount After Tax:</strong></td>
                        <td class="auto-style10" style="align-items:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbltotalamtaftertax" runat="server" Font-Bold="True" Font-Size="Medium"> 0.00 </asp:Label>
                        </td>
                        <td>Total Quantity</td>
                        <td>

                            <asp:Label ID="lblQty" runat="server" Font-Bold="True" Font-Size="Medium"> 0.00 </asp:Label>
                        </td>
                        <td class="auto-style14">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:HiddenField ID="hfid" runat="server" />
                        </td>
                        <td class="auto-style10" style="align-items:center">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" OnClick="btnSave_Click1" Text="Save" />

                        </td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style12">&nbsp;</td>
                        <td class="auto-style14">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                </table>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>
</asp:Content>

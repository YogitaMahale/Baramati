<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditproduct.aspx.cs" Inherits="addeditproduct" %>

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
                <div style="text-align: center;">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                </div>
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-danger">
                        <%--<div class="box-header with-border">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                </div>--%>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Main Category </label>
                                    <asp:DropDownList ID="ddlMain" Class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged" runat="server"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlMain" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Select Category </label>
                                    <asp:DropDownList ID="ddlCategory" Class="form-control" runat="server"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RFVddlCategory" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlCategory" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>
                                <%--</div>
                     <div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Article Name </label>
                                    <asp:TextBox ID="txtProductName" Class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVtxtProductName" runat="server" Display="Dynamic" ControlToValidate="txtProductName" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Brand Name </label>
                                    <asp:DropDownList ID="ddlBrand" Class="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlBrand" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>

                                <%--<div class="col-xs-3">
                         <label for="exampleInputEmail1">Select Color </label>
                        <asp:DropDownList ID="ddlColor" Class="form-control"  runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlColor" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>

                        <div class="col-xs-3">
                          <label for="exampleInputEmail1">Select Size </label>
                        <asp:DropDownList ID="ddlSize" Class="form-control"  runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlSize" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                    </div>--%>
                            </div>

                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">MRP</label><%--Customer Price --%>
                                    <asp:TextBox ID="txtCustomerProductPrice" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtCustomerProductPrice" runat="server" FilterMode="ValidChars" TargetControlID="txtCustomerProductPrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RFVtxtCustomerProductPrice" runat="server" Display="Dynamic" ControlToValidate="txtCustomerProductPrice" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-xs-3">
                                    <asp:TextBox ID="txtSKU" Visible="false" Class="form-control" runat="server"></asp:TextBox>
                                    <label for="exampleInputPassword1">Retailer Price ( % )</label><%--Dealer Price--%>
                                    <asp:TextBox ID="txtDealerPrice" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBDealer" runat="server" FilterMode="ValidChars" TargetControlID="txtDealerPrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RFVtxtDealerPrice" runat="server" Display="Dynamic" ControlToValidate="txtDealerPrice" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>



                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Wholesale Price ( % )</label>
                                    <asp:TextBox ID="txtWholesalePrice" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtWholesalePrice" runat="server" FilterMode="ValidChars" TargetControlID="txtWholesalePrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                </div>
                                 <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Puchase( Landing ) Price</label>
                                    <asp:TextBox ID="txt_landingprice" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterMode="ValidChars" TargetControlID="txt_landingprice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txt_landingprice" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-3" style ="display:none;">
                                    <label for="exampleInputPassword1">Super Wholesale Price</label>
                                    <asp:TextBox ID="txtSuperWholesalePrice" Class="form-control" Text="0" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtSuperWholesalePrice" runat="server" FilterMode="ValidChars" TargetControlID="txtSuperWholesalePrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                </div>
                                <div class="col-xs-3"  style ="display:none;">
                                    <label for="exampleInputPassword1">Product Discount Price</label>
                                    <asp:TextBox ID="txtDiscountProductPrice" Class="form-control" Text="0" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtDiscountProductPrice" runat="server" FilterMode="ValidChars" TargetControlID="txtDiscountProductPrice" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                </div>
                               
                            </div>
                            <div class="form-group row">

                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">GST (%)</label>
                                    <asp:TextBox ID="txtGST" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtGST" runat="server" FilterMode="ValidChars" TargetControlID="txtGST" ValidChars="01234567890."></cc1:FilteredTextBoxExtender>

                                </div>

                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Stock Quantites </label>
                                    <asp:TextBox ID="txtQuantites" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtQuantites" runat="server" FilterMode="ValidChars" TargetControlID="txtQuantites" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RFVtxtQuantites" runat="server" Display="Dynamic" ControlToValidate="txtQuantites" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Stock Alert Quantites</label>
                                    <asp:TextBox ID="txtAlertQuantites" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FTBtxtAlertQuantites" runat="server" FilterMode="ValidChars" TargetControlID="txtAlertQuantites" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RFVtxtAlertQuantites" runat="server" Display="Dynamic" ControlToValidate="txtAlertQuantites" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Real Stock</label>
                                    <asp:TextBox ID="txt_RealStock" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" TargetControlID="txt_RealStock" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txt_RealStock" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">HSN Code</label>
                                    <asp:TextBox ID="txt_Hsncode" Class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txt_Hsncode" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="form-group">--%>

                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Packing</label>
                                    <asp:TextBox ID="txtpack" Class="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterMode="ValidChars" TargetControlID="txtpack" ValidChars="01234567890.%"></cc1:FilteredTextBoxExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtpack" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>

                                <div class="col-xs-2">
                                    <label for="exampleInputPassword1">Is Stock</label>
                                    <asp:CheckBox ID="cbIsStock" Class="form-control" runat="server"></asp:CheckBox>
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputPassword1">Is HotProduct</label>
                                    <asp:CheckBox ID="cbIsHotproduct" Class="form-control" runat="server"></asp:CheckBox>
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputPassword1">GST Included </label>
                                    <asp:CheckBox ID="cbIsgstType" Class="form-control" runat="server"></asp:CheckBox>
                                </div>
                            </div>


                            <%--<div class="form-group row"> </div>--%>
                            <div class="form-group row">

                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Short Description </label>
                                    <asp:TextBox ID="txtProductShortDescription" Class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RFVtxtProductShortDescription" runat="server" Display="Dynamic" ControlToValidate="txtProductShortDescription" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>

                                </div>
                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Long Description</label>
                                    <asp:TextBox ID="txtProductDescription" Class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>

                                </div>



                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputFile">Image</label>
                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <%--<asp:FileUpload ID="fpProduct" runat="server" />--%>
                                                <asp:FileUpload ID="fpProduct1" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Image ID="imgProduct" Visible="false" Width="75px" Height="50px" runat="server" />
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Visible="false" Text="X" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnRemove_Click" />
                                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnImageUpload" runat="server" Text="Upload" CssClass="btn btn-info" OnClick="btnImageUpload_Click" OnClientClick="return checkFileExtension()" />
                                            </td>
                                        </tr>
                                    </table>
            
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video 1</label>
                                    <asp:TextBox ID="txtYouTubeVideo1" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video Name 1 </label>
                                    <asp:TextBox ID="txtYoutubeName1" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video 2</label>
                                    <asp:TextBox ID="txtYouTubeVideo2" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video Name 2</label>
                                    <asp:TextBox ID="txtYoutubeName2" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">

                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video 3</label>
                                    <asp:TextBox ID="txtYouTubeVideo3" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video Name 3</label>
                                    <asp:TextBox ID="txtYoutubeName3" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video 4</label>
                                    <asp:TextBox ID="txtYouTubeVideo4" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">--%>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">YouTube Video Name 4</label>
                                    <asp:TextBox ID="txtYoutubeName4" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                    
                  <div class="col-xs-3">
                        <label for="exampleInputEmail1">Process Name </label>
                      <asp:DropDownList ID="ddlProcess" Class="form-control"  runat="server"></asp:DropDownList>

                                    
                   <%--  <asp:TextBox ID="txt_processName" CssClass="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlProcess" ValidationGroup="c1"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                   </div>
                         <div class="col-xs-3">
                        <label for="exampleInputEmail1">Price </label>
                     <asp:TextBox ID="txt_price" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txt_price" ValidationGroup="c1"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                         <div class="col-xs-3">
                             <br />
                             
                       <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Add" OnClick="btnAdd_Click" />
                    </div>
                    </div>
                     

                    <div class="form-group row" style="overflow:scroll;">
                        <asp:GridView ID="repProcess" runat="server" AutoGenerateColumns="False" CellPadding="3" DataKeyNames="id" Width="1151px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                <Columns>
                                     
                                    <asp:BoundField DataField="id"     HeaderStyle-Width="10px" Visible="false" HeaderText="TableId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                        <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="sr" HeaderStyle-Width="10px" HeaderText="sr" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                                        <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="processName" HeaderText="process Name" HeaderStyle-Width="50px">
                                        <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="500px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="particulars" HeaderStyle-Width="50px" HeaderText="ProductName" Visible="false" >
                                        <ControlStyle Font-Strikeout="False" />
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="wages" HeaderText="Price">
                                        <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    
                                 

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                             
                                            <asp:Button ID="Button1" runat="server" Text="Remove" OnClick="Remove_member1" class="btn btn-primary"
                                                CommandName="Remove" Width="70"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" VerticalAlign="Top" />
                                <RowStyle ForeColor="#000066" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" HorizontalAlign="Center" VerticalAlign="Top" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                          
                        </div> 
                            <div class="form-group row">
                            </div>

                            <div class="col-md-12">

                                <div class="box-footer" style="text-align: center">
                                    <asp:Button ID="btnSave" runat="server" CausesValidation="true" ValidationGroup="p1" Text="Save" class="btn btn-success" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />


                                </div>
                            </div>







                        </div>
                        <!-- /.box-body -->



                    </div>
                   

                </div>
 
            </div>

        </ContentTemplate>
         <Triggers>
               <asp:PostBackTrigger ControlID="btnImageUpload"  />
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
            //if (($("#ctl00_ContentPlaceHolder1_fpProduct").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpProduct").val() == null)) {
            if ((document.getElementById("fpProduct").val() == "") || (document.getElementById("fpProduct").val() == null)) {
                alert("Please Upload Image.")
                result = false;
            }
            else {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
                //var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpProduct");
                var fileUpload = document.getElementById("fpProduct");
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


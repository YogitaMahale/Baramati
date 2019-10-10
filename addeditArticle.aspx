<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditArticle.aspx.cs" Inherits="addeditArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                
                <div class="box-body">
                   

                  <%--   <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1">Vendor Name<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtvendorName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtvendorName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        </div>--%>
                    <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1">Product </label>
                    <asp:DropDownList ID="ddlProduct" Enabled ="false"  CssClass="form-control" Width="500px" runat="server"></asp:DropDownList></td>
    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlProduct" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                    </div>
                    <div class="form-group row">
                    
                  <div class="col-xs-3">
                        <label for="exampleInputEmail1">Process Name </label>
                     <asp:TextBox ID="txt_processName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txt_processName" ValidationGroup="c1"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                   </div>
                         <div class="col-xs-3">
                        <label for="exampleInputEmail1">Price </label>
                     <asp:TextBox ID="txt_price" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_price" ValidationGroup="c1"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                         <div class="col-xs-3">
                             <br />
                             
                       <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Add" OnClick="btnAdd_Click" />
                    </div>
                    </div>
                     

                    <div class="form-group row">
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
                                    <asp:BoundField DataField="particulars" HeaderStyle-Width="50px" HeaderText="ProductName">
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
                         <%--<table id="tblBank" class="display table table-hover table-striped table-bordered">
                    <thead>
                        <tr class="tableheader">
                            <th style="text-align: center">sr</th>
                            <th style="text-align: center">Product Name</th>
                            <th style="text-align: center">Price</th>
                             <th style="text-align: center">ID</th>
                             <th style="text-align: center">Action</th>
                           
                            
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repProcess" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblSr" runat="server" Text='<%# Eval("sr") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("particulars") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("wages") %>'></asp:Label>
                                    </td>
                                     <td style="text-align: center">
                                        <asp:Label ID="lbl_processId" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    </td>
                                   <td style="text-align: center">
                                         
                                        &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this Process?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>--%>
                        </div> 
                        <br />
                         
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="gg" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
                </div>
                </div>

 
            

        </div>
        
    </div>
            </div>
        </div>
 </ContentTemplate>
     <%-- <Triggers><asp:PostBackTrigger ControlID="btnImageUpload"/>  </Triggers> --%>  
        
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
        //if (($("#ctl00_ContentPlaceHolder1_fpCategory").val() == "") || ($("#ctl00_ContentPlaceHolder1_fpCategory").val() == null)) {
        if ((document.getElementById("fpCategory").val() == "") || (document.getElementById("fpCategory").val() == null)) {
            alert("Please Upload Image.")
            result = false;
        }
        else {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".JPEG"];
            //var fileUpload = document.getElementById("ctl00_ContentPlaceHolder1_fpCategory");
            var fileUpload = document.getElementById("fpCategory");
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

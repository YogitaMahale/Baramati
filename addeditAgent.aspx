<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditAgent.aspx.cs" Inherits="addeditAgent" %>

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
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                   
                    <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1">Agent Name<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtAgentName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtAgentName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        </div>
                        <div class="form-group row">
                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Address <span style="color:red">*</span></label>
                        <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAddress" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                        <div class="form-group row">

                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Mobile No<span style="color:red">*</span></label>
                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="c1"  ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />
                        <asp:RequiredFieldValidator ID="RFVtxtBankBranch" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>
                            <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">E-mail<span style="color:red">*</span></label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <asp:RequiredFieldValidator ID="RFVtxtAccountNo" runat="server" Display="Dynamic" ControlToValidate="txtEmail" CssClass="error" ErrorMessage="Required Field" ValidationGroup="c1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>


                    </div>
                        </div>
                        <%--<div class="form-group row">

                        
                        </div>--%>
                        <br />
                        <div class="form-group row">
                    
                    <div class="col-xs-12"> 
                       <table class="table table-user-information">
            <tbody>
                <tr>
                    <td style="width: 250px; font-weight: bold"> Image</td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fpCategory" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="imgCategory" Visible="false" Width="75px" Height="50px" runat="server" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Visible="false" CssClass="btn btn-danger" Text="X" CausesValidation="false" OnClick="btnRemove_Click" />
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnImageUpload" runat="server" CssClass="btn btn-info" Text="UPLOAD" OnClick="btnImageUpload_Click" OnClientClick="return checkFileExtension()" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;<%-- <asp:RequiredFieldValidator ID="RFVfpCategory" runat="server" Display="Dynamic" ControlToValidate="fpCategory" CssClass="error" ErrorMessage="This is required field" ValidationGroup="c1"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </tbody>
                           </table>
                    </div>
                    </div>
                   
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
                </div>
                </div>


                    


                 </>
                 <%--</div>--%>
                   
                </>
                <!-- /.box-body -->

                
            
            </>
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
            </div>
        </div>
 </ContentTemplate>
      <Triggers><asp:PostBackTrigger ControlID="btnImageUpload"/>  </Triggers>   
        
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

</asp:Content>


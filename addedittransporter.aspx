<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addedittransporter.aspx.cs" Inherits="addedittransporter" %>

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

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Transporter Name<span style="color: red">*</span> </label>
                                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtname" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Mobile No<span style="color: red">*</span> </label>
                                    <asp:TextBox ID="txtmobileno" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtmobileno" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmobileno" ValidationGroup="c1" ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />

                                </div>
                                </div><div class="form-group row">
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Phone No<span style="color: red">*</span> </label>
                                    <asp:TextBox ID="txtphoneno" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtphoneno" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtphoneno" ValidationGroup="c1" ValidationExpression="^\d+" ErrorMessage="*" Font-Bold="True" Font-Size="Large" />

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Email<%--<span style="color:red">*</span>--%> </label>
                                    <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtemail" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

                                </div>
                                    </div><div class="form-group row">
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Aadhar No </label>
                                    <asp:TextBox ID="txtaadharno" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">PAN No </label>
                                    <asp:TextBox ID="txtpanno" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                        </div><div class="form-group row">
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">GSTIN</label>
                                    <asp:TextBox ID="txtgstno" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">GST Type</label>
                                    <asp:DropDownList ID="ddlgsttype" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Consumer" Value="Consumer"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                            </div><div class="form-group row">
                                
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Address <span style="color: red">*</span></label>
                                    <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAddress" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Remark</label>
                                    <asp:TextBox ID="txtremark" CssClass="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>

                                </div>
                                </div>
                            
                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
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

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditwebuser.aspx.cs" Inherits="addeditwebuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border" style="text-align: center;">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                    <h4><span style="color: red">* Indicates required fields</span> </h4>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    <div class="form-group">
                        <label for="exampleInputEmail1">User Name <b style="color: red">* </label>
                        <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="error" ErrorMessage="Required" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group" >
                        <label for="exampleInputEmail1">User Login Id <b style="color: red">*</b></label>
                        <asp:TextBox ID="txtUserId" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtUserId" runat="server" Display="Dynamic" ControlToValidate="txtUserId" CssClass="error" ErrorMessage="Required" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>

                    <div class="form-group">
                        <label for="exampleInputEmail1">Password <b style="color: red">*</b> </label>
                        <asp:TextBox ID="txtPassword" class="form-control" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="error" ErrorMessage="Required" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>




                    <div class="form-group" style="display: none;">
                        <label for="exampleInputPassword1">User Type <b style="color: red">*</b></label>
                        <asp:DropDownList ID="ddlUserType" runat="server" class="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Email Address <b style="color: red">*</b></label>
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>

                        <asp:RegularExpressionValidator ID="REtxtEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="c1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><span class="boldClass" style="color:red">Invalid Email Address</span></asp:RegularExpressionValidator>
                    </div>



                    <div class="form-group">
                        <label for="exampleInputPassword1">Phone <b style="color: red">*</b></label>
                        <asp:TextBox ID="txtPhone" class="form-control" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RFVtxtPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" CssClass="error" ErrorMessage="Required" ValidationGroup="c1"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Page Access <b style="color: red">*</b></label>
                         <div style="overflow:scroll;height:200px;width:1000px;" >
                          <asp:CheckBoxList ID="chkList" runat="server" class="form-control"  RepeatColumns="4" RepeatDirection="Vertical" Width="800" >
                        </asp:CheckBoxList>
                            </div>
                    </div>


                
                <!-- /.box-body -->

                <div class="box-footer">
                      <asp:Button ID="btnSave" runat="server" ValidationGroup="c1" class="btn btn-primary" CausesValidation="true"  Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" class="btn btn-primary" OnClick="btnCancel_Click" Text="Cancel" />
                  <%----%>
                     
                </div>

            </div>
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
    <!-- page script --> 
</asp:Content>



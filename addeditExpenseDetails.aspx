<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditExpenseDetails.aspx.cs" Inherits="addeditExpenseDetails" %>

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
                    
                  <div class="col-xs-3">
                        <label for="exampleInputEmail1">Expense </label>
                        <asp:DropDownList ID="ddlExpenseType" CssClass="form-control" Width="500px" runat="server"></asp:DropDownList>
    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlExpenseType" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                    </div>
                    <div class="form-group row">

                        <div class="col-xs-2">
                        <label for="exampleInputEmail1">Bank Name </label>
                        <asp:DropDownList ID="ddlBank" CssClass="form-control" Width="500px" runat="server"></asp:DropDownList>
    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlBank" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                    
                        
                        
                    </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                    <label for="exampleInputEmail1">Expense Amount<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtExpenseAmt" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtExpenseAmt" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                </div>
                    <div class="col-xs-6">
                    <label for="exampleInputEmail1">Description<span style="color:red">*</span> </label>
                    <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" runat="server" Rows="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDescription" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                </div>
                        </div>
                       
                        
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
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


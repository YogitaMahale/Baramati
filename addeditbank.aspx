﻿<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addeditbank.aspx.cs" Inherits="addeditbank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                    <h3><span style="color:red">* Indicates Required Fields</span></h3>
                    <h3 class="box-title" style="text-align:center">  <b id="spnMessgae" runat="server"></b></h3>
                    <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%> 
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                   
                    <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1"> Name<span style="color:red">*</span> </label>
                        <asp:TextBox ID="txtBankName" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--<asp:TextBox ID="txtNewsTitle" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtBankName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        </div>
                    <div class="form-group row">

                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> IFSC Code<span style="color:red">*</span></label>
                        <asp:TextBox ID="txtIFSC" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--<asp:TextBox ID="txtNewsDate" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CEtxtNewsDate" runat="server" TargetControlID="txtNewsDate" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                        --%>
<%--                        <asp:TextBox ID="txtMobile" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtIFSC" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                     </div>    
                    </div>
                    <div class="form-group row">

                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Branch <span style="color:red">*</span></label>
                        <asp:TextBox ID="txtBankBranch" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtBankBranch" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                    <div class="form-group row">

                    <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Account No <span style="color:red">*</span></label>
                        <asp:TextBox ID="txtAccountNo" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--<asp:TextBox ID="txtDescription" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>--%>
                        <%--<asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountNo" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                    <div class="form-group row">

                    <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Account Holder Name <span style="color:red">*</span></label>
                        <asp:TextBox ID="txtAccountHolderName" CssClass="form-control" runat="server"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAccountHolderName" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                    
                    <div class="form-group row">

                        <div class="col-xs-6"> 
                        <label for="exampleInputEmail1"> Opening Balance <span style="color:red">*</span></label>
                        <asp:TextBox ID="txtbalance" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtbalance" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        </div>
                    
                    <div class="form-group row">
                    
                   
                   
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />
                </div>
                </div>
                        </div>


                    


                 </div>
                 <%--</div>--%>
                   
                
                <!-- /.box-body -->

                
            
            
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

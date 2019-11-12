<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="vendorpayment.aspx.cs" Inherits="vendorpayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">  <b id="spnMessgae" runat="server"></b></h3>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                    <div class="form-group row">
                        <div class="col-xs-4">
                        <label for="exampleInputEmail1">Supplier Name </label>
                                                      <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" AutoPostBack="true" />

                           <asp:RequiredFieldValidator ID="RFVddlVendor" runat="server" Display="Dynamic" ControlToValidate="ddlVendor" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    </div>
                    </div><div class="form-group row">
                        <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                </div>
                    </div><div class="form-group row">
                         
                        <div class="col-xs-4">
                             <label for="exampleInputEmail1">Payment Type </label>
                             <asp:DropDownList ID="ddlpaytype" runat="server" CssClass="form-control" AutoPostBack="true">
                                 <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                                 <asp:ListItem Text="CASH" Value="CASH"></asp:ListItem>
                             </asp:DropDownList>

                         <%--<asp:RequiredFieldValidator ID="RFVtxtPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>--%>
                 </div>
                    </div><div class="form-group row">

                        <div class="col-xs-4"><label for="exampleInputPassword1">Amount</label>
                         <asp:TextBox ID="txtamount" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVtxtamount" runat="server" Display="Dynamic" ControlToValidate="txtamount" CssClass="error" ErrorMessage="Required Field" ValidationGroup="d1"></asp:RequiredFieldValidator>
                    </div>
                    </div><div class="form-group row">
                        
                        <div class="col-xs-4"><label for="exampleInputPassword1">Note</label>
                         <asp:TextBox ID="txtnote" class="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                                            
                </div>
                <!-- /.box-body -->

                <div class="box-footer">
                      <asp:Button ID="btnUpdate" runat="server" CausesValidation="true" ValidationGroup="d1" Text="Save" class="btn btn-primary" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" class="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />

                     
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


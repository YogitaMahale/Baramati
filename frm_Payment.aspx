<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="frm_Payment.aspx.cs" Inherits="frm_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .error {
            color: red;
        }

        .auto-style6 {
            width: 138px;
        }

        .auto-style12 {
            width: 89px;
        }

        .auto-style14 {
            width: 52px;
        }

        .auto-style15 {
            width: 167px;
        }

        .auto-style16 {
            height: 20px;
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
                            <h3><span style="color: red">* Indicates Required Fields</span></h3>
                            <h3 class="box-title" style="text-align: center"><b id="spnMessgae" runat="server"></b></h3>
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Payment Date </label>

                                    <asp:TextBox ID="txtpaymentDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" BehaviorID="Calendar1" runat="server" TargetControlID="txtpaymentDate" OnClientDateSelectionChanged="dateselect" Format="yyyy/MM/dd  HH:mm:ss">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>
                             <div class="form-group row">


                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Name<span style="color: red"> *</span></label>
                                    <asp:DropDownList ID="ddlname" CssClass="form-control" runat="server" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Payment Type</label>

                                    <asp:DropDownList ID="ddlPaymentType" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">
                                        <asp:ListItem>--Select----- </asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Bank</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlPaymentType" CssClass="error" ErrorMessage="Required Field" ValidationGroup="gg"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                     <label  for="exampleInputEmail1">   </label>

                                    <asp:DropDownList ID="ddlPaymentType1" Visible="false"  CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlPaymentType1_SelectedIndexChanged" AutoPostBack="True" >
                                        <asp:ListItem>--Select----- </asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlPaymentType" CssClass="error" ErrorMessage="Required Field" ValidationGroup="gg"></asp:RequiredFieldValidator>

                                   
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-xs-3">
                                     <label for="exampleInputEmail1"></label>
                                    <asp:TextBox ID="txtbankName" placeholder="Bank Name" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                <div class="col-xs-3">
                                     <label for="exampleInputEmail1"></label>
                                    <asp:TextBox ID="txtChequeNo" placeholder="Cheque No" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div> 

                            <div class="form-group row">
                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Paid Amount</label>
                                    <asp:TextBox ID="txtPaidamt" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group row">

                                <div class="col-xs-6">
                                    <label for="exampleInputEmail1">Note</label>
                                    <asp:TextBox ID="txtComment" CssClass="form-control" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>

                                </div>
                                
                            </div>






                            <div class="form-group row">



                                <div class="col-md-6">
                                    <div class="box-footer" style="text-align: center">

                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                   
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />



                                    </div>
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
    <!-- page script -->
    <script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>

</asp:Content>


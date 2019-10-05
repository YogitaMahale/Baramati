<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report_AgentList.aspx.cs" Inherits="Report_AgentList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">
                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                           
                            <%--<div class="pull-left" >--%>
                            <div class="form-group row">
                    
                    <%--<div class="col-xs-3"></div>--%>    
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">From :</label>--%>
                        <asp:TextBox ID="txt_fromDate" runat="server" class="form-control" autocomplete="off" ReadOnly="false" AutoPostBack="true" placeholder="FROM" OnTextChanged="txt_fromDate_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_fromDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_fromDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">To :</label>--%>
                        <asp:TextBox ID="txt_toDate" runat="server" class="form-control" autocomplete="off" ReadOnly="false" AutoPostBack="true" placeholder="TO" OnTextChanged="txt_fromDate_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_toDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"   TargetControlID="txt_toDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    <div class="col-xs-3"> 

                            <%--<label for="exampleInputEmail1">Agent </label>--%>
                        <asp:DropDownList ID="ddlAgents" Class="form-control" runat="server" OnSelectedIndexChanged="txt_fromDate_TextChanged" AutoPostBack="true"></asp:DropDownList>

                        <%--<asp:RequiredFieldValidator ID="RFVddlCategory" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlAgents" CssClass="error" ErrorMessage="Required Field" ValidationGroup="p1"></asp:RequiredFieldValidator>--%>
                        </div>
                    <div class="col-xs-3" style="text-align:right;">
                                 <asp:Button ID="btnExcelExport" runat="server" Visible="false" class="btn btn-flickr" Width="100" Text="Excel Export" OnClick="btnExcelExport_Click" /></td>
                
                                
                            </div>

                    </div>

                    


                    <%--</div>--%>
         <%--<div class="table-responsive" style="overflow-x:auto;">--%> 
    <table id="tblAgents" class="display table table-hover table-striped table-bordered">
        <thead>
            <tr class="tableheader">
                <th>Dealer Name</th>
                
                <%--<th>Agent</th>--%>
                <th style="text-align: center">Payment (Rs.)</th>
                <%--<th style="text-align: center">Valid From To</th>
                <th style="text-align: center">IsActive</th>
                <th style="text-align: center">Action</th>--%>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repAgents" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblDealer" runat="server" Text='<%# Eval("dealername") %>'></asp:Label>
                        </td>
                        
                        <%--<td>
                            <asp:Label ID="lblAgent" runat="server" Text='<%# Eval("agentname") %>'></asp:Label>
                        </td>--%>
                        <td style="text-align: right">
                            <asp:Label ID="lblBusiness" runat="server" Text='<%# Eval("payment") %>'></asp:Label>

                        </td>
                        <%--<td style="text-align: center">
                            <asp:Label ID="lblFromTo" runat="server" Text='<%# Eval("validfrom") + " - " + Eval("validto") %>'></asp:Label>
                        </td>
                        <td style="text-align: center">
                            <asp:CheckBox ID="cbIsActive" runat="server" AutoPostBack="true" Checked='<%#Eval("isactive") %>' OnCheckedChanged="cbIsActive_CheckedChanged"></asp:CheckBox>
                        </td>
                        <td style="text-align: center">
                            <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-sm btn-success" Style="text-decoration: underline" Text="Edit"></asp:HyperLink>&nbsp;
                            &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Do you want to delete this offer?');" OnClick="lnkDelete_Click"></asp:LinkButton>

                        </td>--%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr class="tableheader">
                <th>Dealer Name</th>
                
                <%--<th>Agent</th>--%>
                <th style="text-align: center">Payment (Rs.)</th>
                <%--<th style="text-align: center">Valid From To</th>
                <th style="text-align: center">IsActive</th>
                <th style="text-align: center">Action</th>--%>
            </tr>
        </tfoot>
    </table>
	<%--</div>--%>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
            <!-- ./wrapper -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
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
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>
    <%--<script>
        $(function () { //"order": [[2, "desc"]]
            $('#tblAgents').DataTable({  })
            $('#example2').DataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false




            })
        })
</script>--%>

    


   
</asp:Content>


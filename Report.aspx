<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-xs-12">
                   
                    <!-- /.box -->

                    
                    <div class="text-center">
                        <b id="spnMessage" visible="false" runat="server"></b>
                    </div>
                    <div class="box box-success">
                         
                        <!-- /.box-header -->
                        <div class="box-body">
                            <%--<div class="pull-right" >
                        <asp:Button ID="btnpo" runat="server" Text="RAISE PO" class="btn btn-block btn-flickr" Width="150" OnClick="btnpo_Click" /><div class="btn-flickr"></div>
                    </div>--%>
                            <div class="form-group row">
                    
                  <div class="col-xs-3">
                        <label for="exampleInputEmail1">Select </label>
                    <asp:DropDownList ID="DropDownList1" CssClass="form-control" AutoPostBack="true" Width="350px" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                              <asp:ListItem>MYSSKFollowup</asp:ListItem>
            <asp:ListItem>MoryaFollowup</asp:ListItem>
            <asp:ListItem>EZACUSFollowup</asp:ListItem>
                        </asp:DropDownList>
    
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlAgent" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>   
                       
                    </div>
                    <div class="col-xs-3">
                        <label for="exampleInputEmail1">Select User </label>
                   <asp:DropDownList ID="ddlUser" CssClass="form-control" AutoPostBack="true" Width="350px" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                              
                        </asp:DropDownList>
    
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlAgent" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>   
                       
                    </div>
                    <div class="col-xs-3"> 
                        <label for="exampleInputEmail1">From :</label>
                        <asp:TextBox ID="txt_fromDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_fromDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_fromDate" Format="yyyy/MM/dd"> </cc1:CalendarExtender>
                    </div>
                    <div class="col-xs-3"> 
                        <label for="exampleInputEmail1">To :</label>
                        <asp:TextBox ID="txt_toDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_toDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"   TargetControlID="txt_toDate" Format="yyyy/MM/dd"> </cc1:CalendarExtender>
                    </div>
                    </div>
                            <%--<div class="form-group row">--%>
                                      <div style="text-align: center" id="divExcel" runat="server" >
               <asp:Button ID="btnShow" runat="server" class="btn btn-flickr" Width="100" Text="SHOW" OnClick="btnShow_Click" />
                <asp:Button ID="btnExcelExport" runat="server" CssClass="btn btn-info" Text="EXCEL EXPORT" OnClick="btnExcelExport_Click" />
            </div><br />

                            <%--</div>--%>


        <table class="display table table-hover table-striped table-bordered" id="tblNews">
                                <thead>
                <tr class="tableheader">
                    <th style="width: 150px" class="text-center">SrNo
                    </th>
                    <th style="width: 80px" class="text-center">Name
                    </th>
                    <th style="width: 100px" class="text-center">Mobile
                    </th>
                    <th style="width: 350px" class="text-center">City
                    </th>
                     <th style="width: 350px" class="text-center">Date
                    </th>
                    <th style="text-align: center; width: 50px">IsRead</th>
                    <th style="width: 150px" class="text-center">USER
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repNews" runat="server" >
                    <ItemTemplate>
                        <tr class="text-center">
                            <td>
                                <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("PKId") %>' />
                                <asp:Label ID="lblNewsId" runat="server" Visible="true" Text='<%# Eval("PKid") %>'></asp:Label>
                              
                             
                            </td>
                            <td>
                                
                                  <asp:Label ID="lblNewsTitle" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                           
                       
                            </td>
                            <td>
                                     <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile") %>'>
                                </asp:Label>
                            
                            </td>
                            <td>
                                    <asp:Label ID="lblNewsDate" runat="server" Text='<%# Eval("city") %>'>
                                </asp:Label>
                           
                            
                            </td>
                             <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("date1") %>'>
                                </asp:Label>
                           
                            
                            </td>
                            <td style="text-align: center">
                                  <asp:CheckBox ID="cbIsActive" runat="server"  Checked='<%#Eval("Isread") %>' ></asp:CheckBox>
                            </td>
                            <td>
                                     <asp:Label ID="lblremark" Style="width: 350px" runat="server" Text='<%# Eval("remark") %>'>
                                </asp:Label>
                             
                       
                          
                            

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
                                <tfoot>
                                    <tr class="tableheader">
                    <th style="width: 150px" class="text-center">SrNo
                    </th>
                    <th style="width: 80px" class="text-center">Name
                    </th>
                    <th style="width: 100px" class="text-center">Mobile
                    </th>
                    <th style="width: 350px" class="text-center">City
                    </th>
                     <th style="width: 350px" class="text-center">Date
                    </th>
                    <th style="text-align: center; width: 50px">IsRead</th>
                    <th style="width: 150px" class="text-center">USER
                    </th>
                </tr>
                                </tfoot>
                            </table>
        <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>

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
    <script>
        $(function () {
            $('#tblNews').DataTable({ "order": [[4, "desc"]] })
            $('#example2').DataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false




            })
        })
</script>

</asp:Content>

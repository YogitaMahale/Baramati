<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="orderreport.aspx.cs" Inherits="orderreport" %>

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
                        <label for="exampleInputEmail1">YEAR </label>
                    <asp:DropDownList ID="ddlYear" runat="server" Width="150px" CssClass="form-control control-label">
                    <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                    <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                    <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                    <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                    <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                    <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFVYear" runat="server" Display="Dynamic" ControlToValidate="ddlYear" InitialValue="-1" ErrorMessage="please select month" ValidationGroup="o1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-xs-3">
                        <label for="exampleInputEmail1">MONTH </label>
                   <asp:DropDownList ID="ddlMonth" runat="server" Width="150px" CssClass="form-control control-label">
                    <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFVMonth" runat="server" Display="Dynamic" ControlToValidate="ddlMonth" InitialValue="-1" ErrorMessage="please select month" ValidationGroup="o1"></asp:RequiredFieldValidator>
    
                       
                    </div>
                    
                    </div>
                            <%--<div class="form-group row">--%>
                                      <div style="text-align: center" id="divExcel" runat="server" >
               <%--<asp:Button ID="btnShow" runat="server" class="btn btn-flickr" Width="150" Text="Show" OnClick="btnShow_Click" />
                <asp:Button ID="btnExcelExport" runat="server" CssClass="btn btn-info" Text="Excel Export" OnClick="btnExcelExport_Click" />--%>
                    <asp:Button ID="btnSearch" runat="server" Text="SEARCH" class="btn btn-flickr" Width="100" ValidationGroup="o1" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="EXPORT" CssClass="btn btn-info" OnClick="btnExport_Click" /></td>
            </div><br />

                            <%--</div>--%>

                            <div style="overflow-x: auto;">
        <asp:GridView ID="gvOrderReport" runat="server" AllowPaging="true" PageSize="25" CssClass="table table-hover table-striped table-bordered" CellPadding="4" GridLines="None" ForeColor="#333333" OnPageIndexChanging="gvOrderReport_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
            <Columns>
                <asp:TemplateField HeaderText="Invoice">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlViewInVoice" runat="server" CssClass="btn btn-success" Text="Invoice" Target="_blank" NavigateUrl='<%# Page.ResolveUrl("~/orderinvoice.aspx?oid=" + common.Encrypt_New(Eval("Order Id").ToString(), true)) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

       <%-- <table class="display table table-hover table-striped table-bordered" id="tblNews">
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
        <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>--%>

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
   <%-- <script>
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
</script>--%>

</asp:Content>  


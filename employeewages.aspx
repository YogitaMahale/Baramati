<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="employeewages.aspx.cs" Inherits="employeewages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                           
                            <h3 class="box-title" style="text-align: center"><b id="spnMessage" runat="server"></b></h3>
                            <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">

                            <div class="form-group row">

                                
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Employee Name<span style="color: red">*</span> </label>
                                    <asp:ListBox ID="lstCustomer" runat="server"  class="form-control select2"></asp:ListBox>
                                    <asp:HiddenField ID="hfcustomer" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="lstCustomer" ValidationGroup="gg" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" ReadOnly="true" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                                

                            </div>
                            
                            <div class="form-group row">
                                
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">First Count</label>
                                    <asp:TextBox ID="txtfirstcount" CssClass="form-control" runat="server" ></asp:TextBox>

                                </div>
                                
                            </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">First Rate</label>
                                    <asp:TextBox ID="txtfirstrate" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Current First Rate</label>
                                    
                                    <asp:TextBox ID="txtcurrentfirstrate"  CssClass="form-control" runat="server" Text ="0"></asp:TextBox>
                                 </div>       
                                
                                    
                                
                                    </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Second Rate</label>
                                    <asp:TextBox ID="txtsecondrate" CssClass="form-control" runat="server"  Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Current Second Rate</label>
                                    
                                    <asp:TextBox ID="txtcurrentsecondrate"  CssClass="form-control" runat="server" Text ="0"></asp:TextBox>
                                 </div>       
                                    </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">V-Shape Rate</label>
                                    <asp:TextBox ID="txtvshaperate" CssClass="form-control" runat="server"  Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Current V-Shape Rate</label>
                                    
                                    <asp:TextBox ID="txtcurrentvshaperate"  CssClass="form-control" runat="server" Text ="0"></asp:TextBox>
                                 </div>       
                                    </div>
                            <div class="form-group row">
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Silai Rate</label>
                                    <asp:TextBox ID="txtsilairate" CssClass="form-control" runat="server" Text ="0"></asp:TextBox>

                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputEmail1">Current Silai Rate</label>
                                    
                                    <asp:TextBox ID="txtcurrentsilairate"  CssClass="form-control" runat="server" Text ="0"></asp:TextBox>
                                 </div>       
                                    </div>
            
                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" Text="CANCEL" />--%>
                                </div>
                            </div>



                            <table id="example1" class="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th style="text-align: center">Employee Name</th>
                                        <th style="text-align: center">First Count</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">First Pair Rate</th>
                                        <th style="text-align: center">Second Rate</th>
                                        <th style="text-align: center">V-Shape Rate</th>
                                        <th style="text-align: center">Silai Rate</th>
                                        
                                      
                                        <th style="text-align: center">Action</th>
                                    </tr>
                                </thead>


                                <tbody>
                                    <asp:Repeater ID="repCategory" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td style="text-align: center">
                                                    
                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                                  <asp:Label ID="lblemployeename" runat="server"  Text='<%# Eval("employeename") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center">  
                                                  <asp:Label ID="Label1" runat="server"  Text='<%# Eval("firstcount") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center"> 
                                                  <asp:Label ID="Label2" runat="server"  Text='<%# Eval("createddate") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center">  
                                                  <asp:Label ID="Label3" runat="server"  Text='<%# Eval("firstrate") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center">  
                                                  <asp:Label ID="Label4" runat="server"  Text='<%# Eval("secondrate") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center">  
                                                  <asp:Label ID="Label5" runat="server"  Text='<%# Eval("vshaperate") %>'></asp:Label>

                                                </td>
                                                <td style="text-align: center">  
                                                  <asp:Label ID="Label6" runat="server"  Text='<%# Eval("silairate") %>'></asp:Label>

                                                </td>
                                             
                                                <td style="text-align: center">
                                                    <asp:Button ID="hlEdit" runat="server" Style="text-decoration: underline" OnClick="hlEdit_Click" class="btn btn-success" Text="Edit" />
                                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Style="text-decoration: underline" class="btn btn-success" Text="Edit"></asp:HyperLink>&nbsp;--%>
                                        &nbsp;<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" class="btn btn-danger" OnClientClick="return confirm('Do you want to delete this Employee?');" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th style="text-align: center">Employee Name</th>
                                        <th style="text-align: center">First Count</th>
                                        <th style="text-align: center">Date</th>
                                        <th style="text-align: center">First Pair Rate</th>
                                        <th style="text-align: center">Second Rate</th>
                                        <th style="text-align: center">V-Shape Rate</th>
                                        <th style="text-align: center">Silai Rate</th>
                                        
                                      
                                        <th style="text-align: center">Action</th>
                                    </tr>
                                </tfoot>
                            </table>




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

    <script type="text/javascript">
        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }
    </script>

    <script type="text/javascript">
        function initDropDowns() {

            $("#<%=lstCustomer.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstCustomer.ClientID%>").select2("val"));
                    $('[id*=hfcustomer]').val($(this).val());
                });

            
        }

    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            initDropDowns();

        });

    </script>


</asp:Content>


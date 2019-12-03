<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addWorksheet.aspx.cs" Inherits="addWorksheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Select2 -->
    <%--<link rel="stylesheet" href="Template/bower_components/select2/dist/css/select2.min.css" />--%>
    <!-- Theme style -->
    <link rel="stylesheet" href="Template/dist/css/AdminLTE.min.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>


            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">

                            <div class="form-group row">

                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Article</label>
                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged" />--%>
                                    <asp:DropDownList ID="ddlArticle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged" />

                                </div>
                                <div class="col-xs-4">
                                    <label for="exampleInputEmail1">Date</label>
                                    <asp:TextBox ID="txt_Date" runat="server" class="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="yyyy/MM/dd"></cc1:CalendarExtender>
                                </div>
                                <%--                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Email </label>
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>--%>
                            </div>

                            <div class="form-group row">

                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <%--<asp:HiddenField ID="hfcolorid" runat="server" />
                                <asp:HiddenField ID="hfcolorname" runat="server" />--%>
                                        <label for="exampleInputEmail1">Color</label>

                                        <%--<asp:DropDownList ID="ddlColor" class="form-control select2" multiple="multiple" runat="server" />--%>
                                        <%--<asp:TextBox ID="dummycolor" class="form-control" Enabled="false" Visible="false" runat="server"></asp:TextBox>--%>
                                        <asp:ListBox ID="lstColor" runat="server" class="form-control select2"></asp:ListBox>
                                        <asp:HiddenField ID="hfcolorid" runat="server" />
                                        <asp:HiddenField ID="hfcolorname" runat="server" />

                                    </div>

                                </div>
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <%--<asp:HiddenField ID="hfsizeid" runat="server" />
                                <asp:HiddenField ID="hfsizename" runat="server" />--%>
                                        <label for="exampleInputEmail1">Size</label>
                                        <%--<asp:DropDownList ID="ddlSize" runat="server" class="form-control select2" multiple="multiple" />--%>
                                        <asp:ListBox ID="lstSize" runat="server" class="form-control select2"></asp:ListBox>
                                        <asp:HiddenField ID="hfsizeid" runat="server" />
                                        <asp:HiddenField ID="hfsizename" runat="server" />


                                    </div>


                                </div>
                            </div>
                            <%--<div class="form-group row">

                        <div class="col-xs-4">
                            <asp:TextBox ID="txtcolorname" class="form-control" Visible="false" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtsize" class="form-control" Visible="false" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>--%>

                            <%--                            <div class="form-group row">

                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Operation</label>
                                    <asp:DropDownList ID="ddlOperation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged" CssClass="form-control" />
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Date</label>
                                    <asp:TextBox ID="txtWorkDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtWorkDate" ValidationGroup="c1" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtWorkDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                </div>
                                <div class="col-xs-2">
                                    <label for="exampleInputEmail1">Employee</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-xs-1">
                                    <label for="exampleInputEmail1">Quantity</label>
                                    <asp:TextBox ID="txtquantity" class="form-control" runat="server" Text="0"></asp:TextBox>


                                </div>
                                <div class="col-xs-1">
                                    <label for="exampleInputEmail1">Wages</label>
                                    <asp:TextBox ID="txtwages" ReadOnly="true" class="form-control" runat="server" Text="0"></asp:TextBox>


                                </div>
                                <div class="col-xs-1">
                                    <label for="exampleInputEmail1">Remark</label>
                                    <asp:TextBox ID="txtRemark" class="form-control" runat="server"></asp:TextBox>


                                </div>
                                <div class="col-xs-2 pad">
                                    
                            <asp:Button ID="btnAdd" runat="server" class="form-control" CssClass="btn btn-app bg-orange"  CausesValidation="true" ValidationGroup="c1" Text="ADD" OnClick="btnAdd_Click" />

                                </div>
                                </div>
                            --%>



                            <div class="col-xs-12">

                                <table class="table table-hover table-checkable order-column full-width" id="example4">
                                    <thead>
                                        <tr>

                                            <th class="center">ACTION </th>
                                            <%--<th class="center">SR NO </th>--%>
                                            <th class="center">Operation </th>
                                            <th class="center">Date</th>
                                            <th class="center">Employee</th>
                                            <th class="center">Quantity</th>
                                            <th class="center">Wages</th>
                                            <th class="center">Remark</th>
                                            <%--  <th class="center"> test Name </th>                               
                                            	<th class="center"> BRAND  </th>
                                                <th class="center"> SIZE </th>
                                                  <th class="center">QUANTITY</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">

                                                    <td class="center">
                                                        <asp:Button ID="Button2" class="form-control" CssClass="btn btn-info" runat="server" Text="REMOVE" OnClick="Remove_Product" CommandArgument='<%# Eval("SrNo") %>' />
                                                        <asp:Label ID="lblSrNo" Visible="false" runat="server" Text=' <%#Eval("SrNo")%>'></asp:Label>
                                                    </td>
                                                    <%--<td class="center"><asp:Label ID="Label1" runat="server" Text=' <%#Eval("operation")%>'></asp:Label></td>--%>
                                                    <td class="center">
                                                        <asp:Label ID="lbloperationid" runat="server" Visible="false" Text=' <%#Eval("operationid")%>'></asp:Label>
                                                        <asp:DropDownList ID="ddlOperationRep" Width="180" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOperationRep_SelectedIndexChanged" AutoPostBack="true" />

                                                    </td>
                                                    <td class="center">
                                                        <%--<asp:Label ID="Date" runat="server" Text=' <%#Eval("date")%>' > </asp:Label>--%>
                                                        <asp:TextBox ID="txtWorkDateRep" runat="server" class="form-control" autocomplete="off" Text=' <%#Eval("date")%>' AutoPostBack="true" OnTextChanged="txtWorkDateRep_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtWorkDateRep" ValidationGroup="gg" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtWorkDateRep" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </td>
                                                    <td class="center">
                                                        <asp:Label ID="lblemployeeid" runat="server" Visible="false" Text=' <%#Eval("employeeid")%>'></asp:Label>
                                                        <asp:DropDownList ID="ddlemployeeRep" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlemployeeRep_SelectedIndexChanged" AutoPostBack="true" />

                                                    </td>


                                                    <td class="center">
                                                        <asp:TextBox ID="Quantity" runat="server" class="form-control" Text=' <%#Eval("quantity")%>' AutoPostBack="true" OnTextChanged="Quantity_TextChanged"></asp:TextBox></td>
                                                    <td class="center">
                                                        <asp:TextBox ID="Wages" runat="server" class="form-control" ReadOnly="true" Text=' <%#Eval("wages")%>'></asp:TextBox></td>
                                                    <td class="center">
                                                        <asp:TextBox ID="Remark" runat="server" class="form-control" Text=' <%#Eval("remark")%>' AutoPostBack="true" OnTextChanged="Remark_TextChanged"></asp:TextBox></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                    <tfoot>
                                        <tr class="odd gradeX">

                                            <th class="center">
                                                <asp:Button ID="btnAdd" runat="server" class="form-control" CssClass="btn bg-orange" CausesValidation="true" ValidationGroup="c1" Text="ADD" OnClick="btnAdd_Click" />
                                            </th>
                                            <th class="center">
                                                <asp:DropDownList ID="ddlOperation" Width="180" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged" CssClass="form-control" />
                                            </th>
                                            <th class="center">
                                                <asp:TextBox ID="txtWorkDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtWorkDate" ValidationGroup="c1" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtWorkDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                            </th>
                                            <th class="center">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />

                                            </th>
                                            <th class="center">
                                                <asp:TextBox ID="txtquantity" class="form-control" runat="server" Text="0"></asp:TextBox>

                                            </th>
                                            <th class="center">
                                                <asp:TextBox ID="txtwages" ReadOnly="true" class="form-control" runat="server" Text="0"></asp:TextBox>

                                            </th>
                                            <th class="center">
                                                <asp:TextBox ID="txtRemark" class="form-control" runat="server"></asp:TextBox>

                                            </th>

                                        </tr>

                                    </tfoot>
                                </table>
                            </div>
                            <div class="col-md-12">
                                <div class="box-footer" style="text-align: center">

                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" CssClass="btn btn-info" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </div>




                            </>
                 <%--</div>--%>
                   
                </>
                <!-- /.box-body -->



                            </>
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

    <script src="Template/dist/js/canvasjs.min.js"></script>

    <!-- Select2 -->
    <script src="Template/bower_components/select2/dist/js/select2.full.min.js"></script>

    <!-- page script -->

    <script type="text/javascript">
        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }
    </script>

    <script type="text/javascript">
        function initDropDowns() {

            $("#<%=lstSize.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstSize.ClientID%>").select2("val"));
                    $('[id*=hfsizeid]').val($(this).val());
                });


            $("#<%=lstColor.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstColor.ClientID%>").select2("val"));
                    $('[id*=hfcolorid]').val($(this).val());
                });
        }

    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            initDropDowns();

        });

    </script>
</asp:Content>

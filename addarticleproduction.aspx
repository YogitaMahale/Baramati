<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addarticleproduction.aspx.cs" Inherits="addarticleproduction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b id="spnMessgae" runat="server"></b></h3>
                    <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">

                    <div class="form-group row">

                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Worksheet No </label>
                            <asp:TextBox ID="txtworksheetno" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Worksheet Date</label>
                            <asp:TextBox ID="txtworksheetdate" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Article</label>
                            <asp:TextBox ID="txtpid" ReadOnly="true" Visible="false" class="form-control" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtarticleno" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Color</label>
                            <asp:TextBox ID="txtcolorid" ReadOnly="true" Visible="false" class="form-control" runat="server"></asp:TextBox>

                            <asp:TextBox ID="txtcolor" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Size</label>
                            <asp:TextBox ID="txtsizegroupid" ReadOnly="true" Visible="false" class="form-control" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtsize" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Employee Name</label>
                            <asp:ListBox ID="lstemployee" runat="server" class="form-control select2"></asp:ListBox>
                            <asp:HiddenField ID="hfemployeeid" runat="server" />

                        </div>


                        <div class="col-xs-3">
                                    <label for="exampleInputPassword1">V-shape </label>
                                    <asp:CheckBox ID="cbvshape" Class="form-control" runat="server"></asp:CheckBox>
                                </div>
                                <div class="col-xs-3">
                                    <label for="exampleInputPassword1">Silai</label>
                                    <asp:CheckBox ID="cbsilai" Class="form-control" runat="server"></asp:CheckBox>
                                </div>

<%--                        <div class="col-xs-6">
                            <label for="exampleInputEmail1">V-shape / Silai</label>
                            <asp:RadioButton ID="rdvshape" runat="server" CssClass="form=control" GroupName="vshapesilai" Text="V-shape" />
                            <asp:RadioButton ID="rdsilai" runat="server" CssClass="form=control" GroupName="vshapesilai" Text="Silai" />

                        </div>--%>




                    </div>

                    <div class="form-group row">

                        
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Total Pairs</label>
                            <asp:TextBox ID="txttotalpairs" class="form-control" runat="server" Text="0"></asp:TextBox>


                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Factory Second</label>
                            <asp:TextBox ID="txtfactorysecond" class="form-control" runat="server" Text="0"></asp:TextBox>


                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Loose Pair</label>
                            <asp:TextBox ID="txtloosepair" class="form-control" runat="server" Text="0"></asp:TextBox>


                        </div>

                    </div>
                    <br />
                    <hr class="alert-success" style="height: 2px" />
                    <%--<div class="form-group row">

                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Range</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                <asp:ListItem Text="11 x 13" Value="1"></asp:ListItem>
                                <asp:ListItem Text="1 x 3" Value="2"></asp:ListItem>
                                <asp:ListItem Text="4 x 7" Value="3"></asp:ListItem>
                                <asp:ListItem Text="6 x 9" Value="4"></asp:ListItem>
                                <asp:ListItem Text="6 x 10" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6 x 8" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7 x 10" Value="7"></asp:ListItem>
                                <asp:ListItem Text="5 x 8" Value="8"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-3">
                            <label for="exampleInputEmail1">Quantity</label>
                            <asp:TextBox ID="txtquantity" class="form-control" runat="server" Text="0"></asp:TextBox>


                        </div>
                        <div class="col-xs-3 pad">
                            <label for="exampleInputEmail1"></label>
                            <br />
                            <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="ADD" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                        </div>
                    </div>--%>




                    <div class="col-xs-6">

                        <table class="table table-hover table-checkable order-column full-width" id="example4">
                            <thead>
                                <tr>

                                    <%--<th class="center">ACTION </th>
                                    <th class="center">PRODUCT NAME </th>
                                    <th class="center">QUANTITY</th>--%>
                                    <th class="center">Size Range</th>
                                    <th class="center">Quantity</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td class="center">
                                                <asp:Label ID="lblid" runat="server" visible="false" Text=' <%#Eval("cid")%>'></asp:Label>
                                                <asp:Label ID="lblsizerange" runat="server" Text=' <%#Eval("sizename")%>'></asp:Label>
                                            </td>
                                            <td class="center">
                                             <asp:TextBox ID="txtquantity" class="form-control" runat="server" Width="80" Text="0"></asp:TextBox>

                                            </td>
                                            <%--<td class="center">
                                                <asp:Button ID="Button2" runat="server" Text="REMOVE" OnClick="Remove_Product" CommandArgument='<%# Eval("SrNo") %>' /></td>
                                            <td class="center">
                                                <asp:Label ID="ProductName" runat="server" Text=' <%#Eval("ProductName")%>'></asp:Label></td>
                                            
                                            <td class="center">
                                                <asp:Label ID="Quantity" runat="server" Text=' <%#Eval("Quantity")%>'></asp:Label></td>--%>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
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

                </div>
                <!--/.col (left) -->
                <!-- right column -->

                <!--/.col (right) -->
            </div>
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


    <script type="text/javascript">

        $(document).ready(function () {

            initDropDowns();

        });

        function pageLoad() {
            // JS to execute during full and partial postbacks
            initDropDowns();


        }


        function initDropDowns() {

            $("#<%=lstemployee.ClientID%>").select2({

                allowClear: true

            }).on('change.select2', function () {
         //alert("Selected value is: "+$("#<%=lstemployee.ClientID%>").select2("val"));
                    $('[id*=hfemployeeid]').val($(this).val());
                });


           
        }


    </script>

    <script type="text/javascript">
        

    </script>


    <script type="text/javascript">
        

    </script>


</asp:Content>


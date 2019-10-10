<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="addWorksheet.aspx.cs" Inherits="addWorksheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-md-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">  <b id="spnMessgae" runat="server"></b></h3>
                    <%--<cc1:ToolkitScriptManager ID="toolScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%> 
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                   <%-- <div class="row">
                    
                  <div class="col-xs-4"> 
                    
                        <label for="exampleInputEmail1">Vendor Name </label>
                         
                    </div>
                        <div class="col-xs-4"> 
                    
                        <label for="exampleInputEmail1">Mobile</label>
                         
                    </div>
                        <div class="col-xs-4"> 
                    
                        <label for="exampleInputEmail1">Email </label>
                         
                    </div>
                    </div>--%>
                    <div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">Article</label>
                  <asp:DropDownList ID="ddlArticle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged" />
                  
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Date</label>
                        <asp:TextBox ID="txt_Date" runat="server" class="form-control" autocomplete="off" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_Date" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_Date" Format="yyyy/MM/dd"> </cc1:CalendarExtender>
                    </div>
<%--                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Email </label>
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>--%>
                    </div>
                    <div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">Color</label>
                  <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" />
                  
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Size</label>
                  <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control" />
                        <%--<label for="exampleInputEmail1">Size</label>
                        <asp:TextBox ID="txtSize" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSize" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                    </div>
                    </div>
                    
                    <div class="form-group row">
                    
                  <div class="col-xs-2">
                  <label for="exampleInputEmail1">Operation</label>
                  <asp:DropDownList ID="ddlOperation" runat="server" CssClass="form-control"/>
                  </div>
                <div class="col-xs-2"> 
                        <label for="exampleInputEmail1">Date</label>
                        <asp:TextBox ID="txtWorkDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txtWorkDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtWorkDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                  <div class="col-xs-2">
                  <label for="exampleInputEmail1">Employee</label>
                  <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" />
                  </div>
                  <div class="col-xs-2">
                        <label for="exampleInputEmail1">Quantity</label>
                      <asp:TextBox ID="txtquantity" class="form-control" runat="server" Text="0"></asp:TextBox>

                       
                    </div>
                <div class="col-xs-2">
                        <label for="exampleInputEmail1">Remark</label>
                      <asp:TextBox ID="txtRemark" class="form-control" runat="server"></asp:TextBox>

                       
                    </div>
                <div class="col-xs-2 pad">
                        <label for="exampleInputEmail1"></label>
                    <br />
                     <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="ADD" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                        <%--<br />--%>
                <%--<asp:Button ID="btnAdd" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect m-b-10 m-r-20 btn-pink" ValidationGroup="gg"  runat="server" Text="ADD" />   <%--onclick="Button1_Click"--%>

                </div>
                        </div>
                    

                    

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
                                              <th class="center">Remark</th>
                                              <%--  <th class="center"> test Name </th>                               
                                            	<th class="center"> BRAND  </th>
                                                <th class="center"> SIZE </th>
                                                  <th class="center">QUANTITY</th>--%>   
                                                 
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server"  >                  
                                             <ItemTemplate>
											<tr class="odd gradeX">
												
                                                <td class="center"><asp:Button ID="Button2" runat="server" Text="REMOVE"  OnClick ="Remove_Product" CommandArgument='<%# Eval("SrNo") %>'/></td>
												 <td class="center"><asp:Label ID="Operation" runat="server" Text=' <%#Eval("operation")%>'></asp:Label></td>
                                                 <td class="center"><asp:Label ID="Date" runat="server" Text=' <%#Eval("date")%>' > </asp:Label></td>
                                                 <td class="center"><asp:Label ID="Employee" runat="server" Text=' <%#Eval("employee")%>' ></asp:Label></td>
                                                 
                                                <td class="center"><asp:Label ID="Quantity" runat="server" Text=' <%#Eval("quantity")%>' ></asp:Label></td>
                                                <td class="center"><asp:Label ID="Remark" runat="server" Text=' <%#Eval("remark")%>' ></asp:Label></td>

											</tr>
                                                     </ItemTemplate>
                                                </asp:Repeater>
										
										</tbody>
                                    </table>
                      </div>
                        <div class="col-md-12">
            <div class="box-footer" style="text-align:center">
                     
                     <asp:Button ID="btnSave" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="Save" OnClick="btnSave_Click"  />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" CssClass="btn btn-info"  Text="Cancel" OnClick="btnCancel_Click" />
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
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="grn.aspx.cs" Inherits="grn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <!-- left column -->
        <div class="col-xs-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">  <b id="spnMessgae" runat="server"></b></h3>
                    <%--<div class="col-xs-6">
                        <label for="exampleInputEmail1">GRN (STOCK IN)</label>
                       
                    </div>--%>
                </div>
                <!-- /.box-header -->
                <!-- form start -->

                <div class="box-body">
                   
                    <div class="form-group row">
                        <div class="col-xs-4">
                    <table>
                                            <tr>
                                                <td><strong>PO No</strong></td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_ID" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                             <tr>
                                                <td><strong>Vendor</strong> </td>
                                                 <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_vendorName" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                             <%--<tr>
                                                <td>Contact </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_contactPerson" runat="server" Text="Label"></asp:Label></td>
                                            </tr>--%>
                                               <tr>
                                                <td><strong>Mobile</strong></td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_mobile" runat="server" Text="Label"></asp:Label></td>
                                               </tr> 
                                               <tr>
                                                <td><strong>Email</strong></td>
                                                 <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label></td>
                                               </tr>
                                                             
                                            
                        </table>

                        </div>
                        
                  <%--<div class="col-xs-4">
                        <label for="exampleInputEmail1">Vendor Name </label>
                        <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlVendor" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                       
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Mobile</label>
                        <asp:TextBox ID="txtMobile" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMobile" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Email </label>
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>--%>
                    </div>
                    
                  <%--  <div class="form-group row">
                    
                  <div class="col-xs-4">
                        <label for="exampleInputEmail1">City </label>
                      <asp:TextBox ID="txtCity" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCity" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   

                       
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">State</label>

                        <asp:TextBox ID="txtState" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtState" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                        <div class="col-xs-4"> 
                        <label for="exampleInputEmail1">Country </label>

                        <asp:TextBox ID="txtCountry"  class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCountry" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
                         
                    </div>
                    </div>
                    
                    <div class="form-group row">
                    
                  <div class="col-xs-6">
                        <label for="exampleInputEmail1">Address</label>
                      <asp:TextBox ID="txtAddress" class="form-control" TextMode="MultiLine" Height="70px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtAddress" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   

                       
                    </div>
                        </div>
                    
                    <div class="form-group row">
                    
                  <div class="col-xs-3">
                  <label for="exampleInputEmail1">Product Category</label>
                  <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" />
                  </div>
                  <div class="col-xs-3">
                  <label for="exampleInputEmail1">Product</label>
                  <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"/>
                  </div>
                  <div class="col-xs-3">
                        <label for="exampleInputEmail1">Quantity</label>
                      <asp:TextBox ID="txtquantity" class="form-control" runat="server"></asp:TextBox>

                       
                    </div>
                <div class="col-xs-3 pad">
                        <label for="exampleInputEmail1"></label>
                    <br />
                     <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="c1" Text="ADD" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                        
                </div>
                        </div>
                  --%>  

                    

                  <div class="col-xs-12">
                      <div class="table-scrollable" style="height:500px;overflow:scroll;">
                                    <table class="table table-bordered table-striped" id="example4">   <%--class="table table-hover table-checkable order-column full-width"--%>
                                        <thead>
                                            <tr> 
                                                <th class="center"> Product ID </th>
                                               <th class="center"> Product Name </th>                             
                                               <%--<th class="center"> Brand </th>                             
                                               <th class="center"> Size </th>                             
                                               --%><th class="center"> Pending </th>
                                               <th class="center"> Received </th>                             
                                               </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server"  >                  
                                             <ItemTemplate>
											<tr class="odd gradeX">										
                                                
                                                <td class="center">
                                                    <asp:Label ID="LabelProdId" runat="server" Text=' <%#Eval("ProdId")%>'></asp:Label>
                                                    <asp:Label ID="LabelPODId" runat="server" Text=' <%#Eval("PurchaseOrderDetailsId")%>' Visible="False"></asp:Label>
                                                </td>
												<td class="center"><asp:Label ID="Label4" runat="server" Text=' <%#Eval("ProdName")%>'></asp:Label></td>
                                               
                                                <td class="center"><asp:Label ID="LabelQuantity" runat="server" Text=' <%#Eval("Quantity")%>'></asp:Label></td>
												<td class="center">
                                                    <asp:TextBox id = "txt_receivedqty" class = "mdl-textfield__input" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txt_receivedqty" ValidationGroup="gg"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>   
												</td>

                                       
												
											</tr>
                                                     </ItemTemplate>
                                                </asp:Repeater>
										
										</tbody>
                                        <tfoot>
                                            <tr> 
                                                <th class="center"> Product ID </th>
                                               <th class="center"> Product Name </th>                             
                                               <%--<th class="center"> Brand </th>                             
                                               <th class="center"> Size </th>                             
                                               --%><th class="center"> Pending </th>
                                               <th class="center"> Received </th>                             
                                               </tr>
                                        </tfoot>
                                    </table>
                                        
                                    </div>
                     
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
    <!-- page script -->
    
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <%-- <style>
        .headerstyle {
            background-color: #00AAAC;
        }

        .headerstyle1 {
            background-color: #F2C81D;
        }

        .headerstyle2 {
            background-color: #01E677;
        }

        .headerstyle3 {
            background-color: #5E8FF9;
        }
        #areachartMorya{
  min-height: 250px;
}
    </style>--%>

    <%--<script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("yyyy-MM-dd")
            //calendarBehavior1.get_element().value = d.format("yyyy/MM/dd") + " " + now.format("HH:mm:ss")
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <!-- Small boxes (Stat box) -->
    <div class="row">
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lbl_AppOrder" runat="server" Text=""></asp:Label></h3>

                    <p>App Order</p>
                </div>
                <div class="icon">
                    <i class="ion ion-bag"></i>
                </div>
                <a href="manageCustomerOrder.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-green">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lbl_confirmedOrder" runat="server" Text=""></asp:Label><%--<sup style="font-size: 20px">%</sup>--%></h3>

                    <p>Confirmed Order</p>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="manageorders.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-yellow">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lbl_DealerRegi" runat="server" Text=""></asp:Label></h3>

                    <p>Dealer Registrations</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="managedealer.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-red">
                <div class="inner">
                    <h3>
                        <asp:Label ID="lbl_UserRegi" runat="server" Text=""></asp:Label></h3>

                    <p>Customer Registrations</p>
                </div>
                <div class="icon">
                    <i class="ion ion-pie-graph"></i>
                </div>
                <a href="manageusers.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
    </div>
    <!-- /.row -->
    <div class="row">
        <!-- Left col -->
        <section class="col-lg-6 connectedSortable">
            <!-- Custom tabs (Charts with tabs)-->
            <div class="nav-tabs-custom">
                <!-- Tabs within a box -->
                <ul class="nav nav-tabs pull-right">
                    <%-- <li class="active"><a href="#revenue-chart" data-toggle="tab">Area</a></li>
              <li><a href="#sales-chart" data-toggle="tab">Donut</a></li>--%>


                    <li class="pull-left header"><i class="fa fa-inbox"></i>Order Details</li>
                </ul>
                <div class="tab-content no-padding">
                    <br />
                    <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="false" Style="overflow: auto;" BorderWidth="1" CssClass="text-center table  table-hover" AllowPaging="true" PageSize="12" OnPageIndexChanging="gvOrderDetails_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <span id="spnSRNO" class="text-bold" runat="server"><%#Container.DataItemIndex+1 %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month - Year">
                                <ItemTemplate>
                                    <span id="spnMonthYear" class="text-bold" runat="server"><%# Eval("MonthName") + " - "+Eval("Year")  %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Count">
                                <ItemTemplate>
                                    <span id="spnOrderCount" class="text-bold" runat="server"><%# Eval("OrderCount") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="headerstyle" />
                    </asp:GridView>
                    <!-- Morris chart - Sales -->

                    <%--  <div class="chart tab-pane active" id="revenue-chart" style="position: relative; height: 300px;"></div>
              <div class="chart tab-pane" id="sales-chart" style="position: relative; height: 300px;"></div>--%>
                </div>
            </div>
            <!-- /.nav-tabs-custom -->

        </section>
        <!-- /.Left col -->
        <!-- right col (We are only adding the ID to make the widgets sortable)-->
       
        
        
         
         <section class="col-lg-6 connectedSortable">
            <div class="box box-info">
                <div class="box-header">
                    <%--<i class="fa fa-envelope"></i>--%>
                    <i class="fa fa-pie-chart"></i><%--<i class=""></i>--%>
                    <h3 class="box-title">Income / Expenses</h3>
                    <!-- tools box -->
                    <%--<div class="pull-right box-tools">
                <button type="button" class="btn btn-info btn-sm" data-widget="remove" data-toggle="tooltip"
                        title="Remove">
                  <i class="fa fa-times"></i></button>
              </div>--%>
                    <!-- /. tools -->
                </div>
                <div class="box-body">
                  <div class="form-group row">
                    
                        
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">From :</label>--%>
                        <asp:TextBox ID="txt_fromDate" runat="server" class="form-control" autocomplete="off" ReadOnly="false" AutoPostBack="true" placeholder="FROM" OnTextChanged="txt_fromDate_TextChanged" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_fromDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_fromDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">To :</label>--%>
                        <asp:TextBox ID="txt_toDate" runat="server" class="form-control" autocomplete="off" ReadOnly="false" AutoPostBack="true" placeholder="TO" OnTextChanged="txt_fromDate_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_toDate" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"   TargetControlID="txt_toDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    </div><br /><br /><br /><br />  


<%--                    <!-- Date range -->
              <div class="form-group">
                <label>Date range:</label>

                <div class="input-group">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                        

                  <input type="text" class="form-control pull-right" id="txtDateRange" />
                </div>
                <!-- /.input group -->
              </div>
              <!-- /.form group -->
--%>

                    <%--<div class="col-sm-6 col-sm-offset-3 text-center">
                        <label class="label label-success">Pie Chart</label>--%>
                        <div id="pie-chart"></div>
                    <%--</div>--%>

                    


                </div>
                <div class="box-footer clearfix"></div>
            </div>




        </section>
        
        
        
        <!-- right col -->


       <%-- <section class="col-lg-5 connectedSortable">
             <div class="box box-danger">
                <div class="box-header">
                    <i class="fa fa-envelope"></i>
                    <i class="fa fa-line-chart"></i><%--<i class=""></i>
                    <h3 class="box-title">Sales</h3>
                    
                </div>
                <div class="box-body">
                    <div class="text-center">
       <label class="label label-success">Line Chart</label>
      <div id="line-chart"></div>
    </div>
                </div>
                <div class="box-footer clearfix">
                    <%--<button type="button" class="pull-right btn btn-default" id="sendEmail">Send
                <i class="fa fa-arrow-circle-right"></i></button>
                </div>
            </div>


        </section>--%>

        <!-- Left col -->
        <section class="col-lg-12 connectedSortable">

            <!-- Map box -->
            <!-- solid sales graph -->
            <div class="box box-solid bg-teal-gradient">
                <div class="box-header">
                    <i class="fa fa-th"></i>

                    <h3 class="box-title">Sales Graph</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn bg-teal btn-sm" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn bg-teal btn-sm" data-widget="remove">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body border-radius-none">
                    <div class="chart" id="line-chart" style="height: 250px;"></div>
                </div>
                <!-- /.box-body -->
                <%--<div class="box-footer no-border">
                    <div class="row">
                        <div class="col-xs-4 text-center" style="border-right: 1px solid #f4f4f4">
                            <input type="text" class="knob" data-readonly="true" value="20" data-width="60" data-height="60"
                                data-fgcolor="#39CCCC" />

                            <div class="knob-label">Mail-Orders</div>
                        </div>
                        <!-- ./col -->
                        <div class="col-xs-4 text-center" style="border-right: 1px solid #f4f4f4">
                            <input type="text" class="knob" data-readonly="true" value="50" data-width="60" data-height="60"
                                data-fgcolor="#39CCCC" />

                            <div class="knob-label">Online</div>
                        </div>
                        <!-- ./col -->
                        <div class="col-xs-4 text-center">
                            <input type="text" class="knob" data-readonly="true" value="30" data-width="60" data-height="60"
                                data-fgcolor="#39CCCC" />

                            <div class="knob-label">In-Store</div>
                        </div>
                        <!-- ./col -->
                    </div>
                    <!-- /.row -->
                </div>--%>
                <!-- /.box-footer -->
            </div>
            <!-- /.box -->
            <!-- /.box -->



        </section>
       
        <!--/.Left col -->

        <!-- Left col -->

        <section class="col-lg-12 connectedSortable">
            <div class="box box-info">
                <div class="box-header">
                   
                    <i class="fa fa-area-chart"></i>
                    <h3 class="box-title">Morya Followup</h3>
                    <!-- tools box -->
                    <%--<div class="pull-right box-tools">
                <button type="button" class="btn btn-info btn-sm" data-widget="remove" data-toggle="tooltip"
                        title="Remove">
                  <i class="fa fa-times"></i></button>
              </div>--%>
                    <!-- /. tools -->
                </div>
                <div class="box-body">
                    <div class="form-group row">
                    
                  
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">From :</label>--%>
                        <asp:TextBox ID="txt_fromDate1" runat="server" class="form-control" autocomplete="off" AutoPostBack="true" ReadOnly="false" placeholder="FROM" OnTextChanged="txt_fromDate1_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_fromDate1" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_fromDate1" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    <div class="col-xs-3"> 
                        <%--<label for="exampleInputEmail1">To :</label>--%>
                        <asp:TextBox ID="txt_toDate1" runat="server" class="form-control" autocomplete="off" ReadOnly="false" AutoPostBack="true" placeholder="TO" OnTextChanged="txt_fromDate1_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Date" ControlToValidate="txt_toDate1" ValidationGroup="gg"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"   TargetControlID="txt_toDate1" Format="dd/MM/yyyy"> </cc1:CalendarExtender>
                    </div>
                    </div><br /><br /><br /><br />



                    <%--<div class="col-sm-6 col-sm-offset-3 text-center">--%>
                        <%--<label class="label label-success">Pie Chart</label>--%>

                        <div id="areaChartMorya"></div>
                    <%--</div>--%>
                </div>
                <div class="box-footer clearfix"></div>
            </div>




        </section>
        <!--/.Left col -->

    </div>
    <!-- Main row -->

    <!-- /.row (main row) -->


<%--</ContentTemplate>
    </asp:UpdatePanel>--%>
   






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
<!-- InputMask -->
<script src="Template/plugins/input-mask/jquery.inputmask.js"></script>
<script src="Template/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
<script src="Template/plugins/input-mask/jquery.inputmask.extensions.js"></script>
<!-- date-range-picker -->
<script src="Template/bower_components/moment/min/moment.min.js"></script>
<script src="Template/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
<!-- bootstrap datepicker -->
<script src="Template/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<!-- bootstrap color picker -->
<script src="Template/bower_components/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"></script>
<!-- bootstrap time picker -->
<script src="Template/plugins/timepicker/bootstrap-timepicker.min.js"></script>
<!-- iCheck 1.0.1 -->
<script src="Template/plugins/iCheck/icheck.min.js"></script>
<!-- Page script -->
<script>
        window.onload = function () {
           
            var chart = new CanvasJS.Chart("line-chart", {
                animationEnabled: true,
                theme: "light2",


                axisY: {
                    includeZero: false
                },
                axisX: {
                    includeZero: true
                },
                data: [{
                    type: "line",
                    lineColor: "yellow",
                    dataPoints: [
                        <%=markersLst%>
                    ]
                }]
        });

            chart.render();
            Morris.Donut({
                element: 'pie-chart',
                //data: [
                //  { label: "Friends", value: 30 },
                //  { label: "Allies", value: 15 },
                //  { label: "Enemies", value: 45 },
                //  { label: "Neutral", value: 10 }
                //]
                data: [
                  <%=markers%>
                ]
            });

        

            Morris.Bar({
                element: 'areaChartMorya',
                data: [<%=markersFollowup%>],
                xkey: 'UserName',
                ykeys: ['a'],
                labels: ['Followups', 'Total Outcome'],
                fillOpacity: 0.6,
                hideHover: 'auto',
                behaveLikeLine: true,
                resize: true,
                pointFillColors: ['#ffffff'],
                pointStrokeColors: ['black'],
                lineColors: ['red']

            });





        }

    </script>
  

</asp:Content>



<%--var data = [
        //{ UserName: 'komal chaure' , a: 4278} , 
        //{ UserName: 'dipali suplekar' , a: 1111} , 
        //{ UserName: 'divya bora' , a: 4853} , 
        //{ UserName: 'pooja patil' , a: 3573} , 
        //{ UserName: 'mohini magar' , a: 4501} , 
        //{ UserName: 'nikita thakare' , a: 563} , 
        //{ UserName: 'SuperAdmin' , a: 24}
      
    { y: 'komal chaure', a: 4278}, 
    { y: 'dipali suplekar', a: 1111}, 
    { y: 'divya bora', a: 4853}, 
    { y: 'pooja patil', a: 3573}, 
    { y: 'mohini magar', a: 4501}, 
    { y: 'nikita thakare', a: 563}, 
    { y: 'SuperAdmin', a: 24}


      { y: '2014', a: 50, b: 90},
      { y: '2015', a: 65,  b: 75},
      { y: '2016', a: 50,  b: 50},
      { y: '2017', a: 75,  b: 60},
      { y: '2018', a: 80,  b: 65},
      { y: '2019', a: 90,  b: 70},
      { y: '2020', a: 100, b: 75},
      { y: '2021', a: 115, b: 75},
      { y: '2022', a: 120, b: 85},
      { y: '2023', a: 145, b: 85},
      { y: '2024', a: 160, b: 95}
    ],
    config = {
      data: data,
      xkey: 'y',
      ykeys: ['a', 'b'],
      labels: ['Total Income', 'Total Outcome'],
      fillOpacity: 0.6,
      hideHover: 'auto',
      behaveLikeLine: true,
      resize: true,
      pointFillColors:['#ffffff'],
      pointStrokeColors: ['black'],
      lineColors:['gray','red']
  };
    
    

    config = {
                data: [<%=markersFollowup%>],
                xkey: 'UserName',
                ykeys: ['a'],
                //labels: ['Total Income', 'Total Outcome'],
                fillOpacity: 0.6,
                hideHover: 'auto',
                behaveLikeLine: true,
                resize: true,
                pointFillColors: ['#ffffff'],
                pointStrokeColors: ['black'],
                lineColors: ['gray', 'red']
            };
            config.element = 'areaChartMorya';
            Morris.Area(config);

    
    
    --%>
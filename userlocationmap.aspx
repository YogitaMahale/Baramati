<%@ Page Title="" Language="C#" MasterPageFile="~/morya.master" AutoEventWireup="true" CodeFile="userlocationmap.aspx.cs" Inherits="userlocationmap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                             

    <div class="row text-center">
        <div class="col-lg-6">
            <div class="col-lg-2">
                <asp:Image ID="imgU" runat="server" ImageUrl="~/images/u.png" />
                <b style="font-weight:bold;color:red"> User</b>
            </div>
            <div class="col-lg-3">
                <asp:Image ID="imgD" runat="server" ImageUrl="~/images/d.png" />
                <b style="font-weight:bold;color:green"> Dealer</b>
            </div>
        </div>
    </div>
    <br />
    <div id="dvMap" style="width: 100%; height: 650px">
    </div>

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
   <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBIU-crk70nNa6EUUto25X-QrAHSJGjo2g&sensor=false"></script>
    <script type="text/javascript">
        var markers = [<%=markersLst%>
        ];
        window.onload = function () {
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 2,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var infoWindow = new google.maps.InfoWindow();
            var latlngbounds = new google.maps.LatLngBounds();
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var i = 0;
            var interval = setInterval(function () {
                var data = markers[i]
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var icon = "";
                switch (data.type) {
                    case "d":
                        icon = "d";
                        break;
                    case "u":
                        icon = "u";
                        break;
                }
                icon = "http://moryaapp.moryatools.com/images/" + icon + ".png";
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title,
                    animation: google.maps.Animation.BOUNCE,
                    icon: new google.maps.MarkerImage(icon)
                });
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {
                        infoWindow.setContent(data.title);
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
                latlngbounds.extend(marker.position);
                i++;
                if (i == markers.length) {
                    clearInterval(interval);
                    var bounds = new google.maps.LatLngBounds();
                    map.setCenter(latlngbounds.getCenter());
                    map.fitBounds(latlngbounds);
                }
            }, 50);
        }
    </script>
</asp:Content>


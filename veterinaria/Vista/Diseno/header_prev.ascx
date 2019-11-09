<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header_prev.ascx.cs" Inherits="Vista_Diseno_header_prev" %>

<!-- header-section-starts -->
<div class="header">
    <div class="container">
        <!--AREA LOGO-->
        <div class="logo">
            <a href="../Inicio/home_previo.aspx"></a>
        </div>
        <!---->
        <!--------DATOS DE USUARIO-->
        <div class="tollfree">
            <ul>
                <!---icono de usuario-->
                <li><span class="glyphicon glyphicon-user icon_green"></span></li>
                <li>
                    <!--Nombre de Usuario--->
                    <p>
                        <asp:Label runat="server" ID="alblNombreUsuario"></asp:Label>
                    </p>
                    <!--Boton para cerrar sesión-->
                    <p class="call">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                    Cerrar Sesión
                        </asp:LinkButton>
                    </p>
                </li>
            </ul>
        </div>
        <!-----FIN DATOS DE USUARIO-->
        <div class="clearfix">
        </div>

        <!--AREA MENU-->
        <div class="navigation">
            <div class="navigation-bar">
                <span class="menu"></span>
                <div class="top-menu">
                    <ul id="hulMenu" class="ul_menu" runat="server">
                    </ul>
                </div>
                <!-- script for menu -->
                <script>
                    $("span.menu").click(function () {
                        $(".top-menu").slideToggle("slow", function () {
                            // Animation complete.
                        });
                    });
                </script>
                <!-- script for menu -->
                <div class="clearfix">
                </div>
            </div>
        </div>
        <!---->

    </div>
</div>
<!-- header-section-ends -->

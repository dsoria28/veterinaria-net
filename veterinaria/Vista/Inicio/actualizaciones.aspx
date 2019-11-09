<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Diseno/MasterPage.master" AutoEventWireup="true" CodeFile="actualizaciones.aspx.cs" Inherits="Vista_Inicio_actualizaciones" %>

<%@ Register Src="~/Vista/Diseno/brearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/CSS/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/cssEstilosVarios.css" rel="stylesheet" />
    <style type="text/css">
        #tb_list_Actualizacion > tbody > tr > td {
            text-align: center;
        }
    </style>
    <script src="../../Scripts/jsplugin/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Inicio/js_actualizaciones.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contend" runat="Server">

    <bread:breadCrumbs ID="breadCrum" runat="server" />

    <asp:HiddenField ID="ahddUser" runat="server" />
    <asp:HiddenField ID="ahdIdReg" runat="server" />
    <asp:HiddenField ID="ahdAction" runat="server" />
    <asp:HiddenField ID="ahddCtrl" runat="server" />
    <asp:HiddenField ID="atxtAct" runat="server" Value="1" />
    <div class="container-fluid">
        <!-- CONTENEDOR BOTÓN COLAPSAR/MAXIMIZAR-->
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding:8px;">
            <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5 row panel panel-default" style="border-radius: 5px; padding: 10px;">
                <span class="fa fa-info-circle txt-Azul" id=""></span>
                <span class="tooltipAzul ">Seleccione un registro del listado para ver el detalle.
                    <span class="tooltiptextAzul1 tooltip-bottom" id="hspVerInfo">Ver información.</span>
                </span>
            </div>
            <div class="col-xs-7 col-sm-7 col-md-7 col-lg-7 row form-group text-right">
                <button title="" class="shadow btn btn-info btn-sm input-sm" id="hbtnColapsar" onclick="javascript:fn_colapsar_tabla()" type="button">
                    <span class="fa fa-minus-circle" id="hspnColAct"></span>
                    <span id="hspnTextAct">Colapsar</span>

                </button>
            </div>
        </div>
        <br />
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <!--TABLA DE REGISTROS-->
            <div class="row form-group" id="hdvActualizaciones">
                <asp:Label runat="server" ID="alblListaActualizacion"></asp:Label>
            </div>
            <!--CONTENEDOR DETALLES Y PDF-->
            <div id="hdvContentAct" class="col-xs-8 col-sm-8 col-md-8 col-lg-8 hide" style="float: right;">
                <div class="row panel panel-default" style="border-radius: 10px; padding: 10px;">
                    <div>
                        <!-- Version -->
                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 text-center">
                            <span style="color: rgb(51, 122, 183);">Versión:</span>
                            <br />
                            <span id="hspVersion"></span>
                        </div>
                        <!-- Fecha de publicacion -->
                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 text-center">
                            <span style="color: rgb(51, 122, 183);">Fecha de publicación:</span>
                            <br />
                            <span id="hspFechaPu"></span>
                        </div>
                        <!-- Fecha inicio -->
                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 text-center">
                            <span style="color: rgb(51, 122, 183);">Fecha inicio:</span>
                            <br />
                            <span id="hspFechaIni"></span>
                        </div>
                        <!-- Fecha fin -->
                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 text-center">
                            <span style="color: rgb(51, 122, 183);">Fecha fin:</span>
                            <br />
                            <span id="hspFechaFin"></span>
                        </div>
                    </div>
                    <!-- Descripcion -->
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 70px;">
                        <span style="color: rgb(51, 122, 183);">Descripción:</span>

                        <span id="hspDescripcion"></span>
                    </div>
                </div>
                <!--CONTENEDOR PARA VISUALIZAR EL PDF-->
                <div id="hdvContentPdf" style="height: 650px">
                </div>
            </div>

        </div>
    </div>

</asp:Content>


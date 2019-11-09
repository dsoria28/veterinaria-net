<%@ Page Language="C#" AutoEventWireup="true" CodeFile="avisos_generales.aspx.cs" Inherits="Vista_Inicio_avisos_generales" MasterPageFile="~/Vista/Diseno/MasterPage.master" %>

<%@ Register Src="~/Vista/Diseno/brearCrumbs.ascx" TagName="breadCrumbs" TagPrefix="bread" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--------------------//////////INICIO CONTENIDO PARA ENCABEZADO///////////////////////--->
    <link href="../../Styles/CSS/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/cssEstilosVarios.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jsplugin/select2Plugin/select2.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/bootstrapValidator.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CSS/bootstrapValidator.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jsplugin/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/select2Plugin/select2.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/bootstrapValidator.js" type="text/javascript"></script>
    <script src="../../Scripts/jsplugin/bootstrapValidator.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Inicio/js_avisos_generales.js" type="text/javascript"></script>
    <style type="text/css">
        .heading-Azul
        {
            color: #333;
            background-color: #3f7b90 !important;
            border-color: #ddd;
        }
        .panel-default { background-color:#e6e8eb; border-color:transparent; }
        /*.panel-heading { background-color:#FFF; border: 1px solid #1d6688 !important; cursor:pointer; }*/
        .panel-title { color:#1d6688; }
        .panel-title:hover { cursor: pointer; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contend" runat="Server">
    <bread:breadCrumbs ID="breadCrum" runat="server" />
    <!--------------------//////////INICIO CONTENIDO PARA CONTENIDO///////////////////////--->
    <asp:HiddenField ID="ahddUser" runat="server" />
    <asp:HiddenField ID="ahddAction" runat="server" />
    <asp:HiddenField ID="ahddIdSubMenu" runat="server" />

    <div class="panel panel-default" style="border-radius:5px;">
        <div class="panel-heading" style="height: auto;border-radius:5px;">
            
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 form-group text-center" style="background-color: #154360; color: White; font-size: 15px; border-radius: 10px;">
                <label>Avisos de Actualizaciones</label>
            </div>

            <div class="row" runat="server" id="hdvAlertaAviso" visible="false" style="padding:0px 15px;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="background-color:#fcf8e3;height:24px;">
                <span class="glyphicon glyphicon-info-sign" style="color:#8a6d3b; font-size:12px;display:inline;"></span>
                <label style="color:#8a6d3b;display:inline;">No existen avisos disponibles para el usuario.</label>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 table-responsive">
                    <asp:Label runat="server" ID="alblListadoAvisos"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!--------------------///////////FIN CONTENIDO PARA CONTENIDO///////////////////////--->

    <!-- Inicio dialog resultados validación -->
    <div class="modal fade" id="hdvConfirmaRetornoCaptura" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <!---///INICIO CONTENIDO MODAL----->
                <div class="modal-header content_modal">
                    <!----///INI TITULO--->
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h5 class="modal-title">Confirmación</h5>
                </div>
                <!----///FIN TITULO--->
                <div class="modal-body">
                    <!---////INICIO CONTENIDO--->
                    <div class="row">
                        <!--Inicio row-->
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <p>¿Esta seguro de retornar el pedimento a captura?</p>
                        </div>
                    </div>
                    <!--Fin row-->
                </div>
                <!---////FIN CONTENIDO--->
                <div class="modal-footer">
                    <!---////INICIO FOOTER--->
                    <input type="button" value="Aceptar" class="btn btn-sm input-sm shadow btn-greenS" onclick="javascript: fn_retornar_pedimento_captura();" />
                </div>
                <!---////FIN FOOTER--->
            </div>
            <!---///FIN CONTENIDO MODAL----->
        </div>
    </div>
    <!-- Fin dialog resultados validación -->

</asp:Content>

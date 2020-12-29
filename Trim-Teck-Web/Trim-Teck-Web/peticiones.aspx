<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="peticiones.aspx.cs" Inherits="Trim_Teck_Web.peticiones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://fonts.googleapis.com/css2?family=PT+Sans&family=Source+Sans+Pro:ital,wght@0,600;1,400&disp<link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/bootstrap.js"></script>
    <link href="css/styles.css" rel="stylesheet" />
    <script src="js/jquery-3.1.1.min.js"></script>
    <title>Trim-Teck Periciones</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="row">
                <div class="col-12 text-center">
                    <h1 class="fuente-titulo">Trim-Teck Sistema de Peticiones</h1>
                </div>
            </div>
        </div>
        <div class="container mt-2">
            <div class="row">
                <div class="col-12 col-sm-6 pt-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="areaInpText">
                                Area
                            </span>
                        </div>
                        <asp:TextBox runat="server" CssClass="form-control" ID="area" TextMode="Number"/>
                    </div>
                </div>
                <div class="col-12 col-sm-6 pt-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="idClienteInpText">
                                Id Cliente
                            </span>
                        </div>
                        <asp:TextBox runat="server" CssClass="form-control" ID="idCliente" TextMode="Number"/>
                    </div>
                </div>
            </div>
            <div class="row mt-2 pt-2">
                <div class="col-12 col-sm-6 pt-2">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="fechaInpText">
                                Fecha
                            </span>
                        </div>
                        <asp:Calendar runat="server"  ID="fecha" SelectionMode="Day" ShowGridLines="false" AutoPostBack="false">
                            <SelectedDayStyle BackColor="#5c9396" ForeColor="Black"/>
                        </asp:Calendar>
                    </div>
                </div>
                <div class="col-12 col-sm-6 pt-2">
                    <div class="row">
                        <div class="col-12">   
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text" id="servicioInpText">
                                        Servicio
                                    </span>
                                </div>
                                <asp:DropDownList ID="servicio" runat="server" CssClass="dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <asp:Button runat="server" ID="limpiar" CssClass=" btn btn-light" Text="Limpiar" OnClick="limpiar_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-2 pt-4 centrado">
                <asp:RadioButtonList ID="solicitudes" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="text-center rblSty" OnSelectedIndexChanged="radio_SelectedIndexChanged">
                    <asp:ListItem Text="Petición" Value="peticion" Selected="True"/>
                    <asp:ListItem Text="Quejas" Value="quejas"/>
                    <asp:ListItem Text="Reclamo" Value="reclamo"/>
                    <asp:ListItem Text="Sugerencia" Value="sugerencia"/>
                    <asp:ListItem Text="Felicitación" Value="felicitacion"/>
                </asp:RadioButtonList>
            </div>
            <asp:Panel ID="espPeticion" runat="server" Visible="true">
                <div class="row mt-2 centrado">
                    <div class="col-12 text-center">
                        <h5>Peticiones</h5>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdPeticionInpText">
                                    Id Petición
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idPeticion" TextMode="Number"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdSuperInpText">
                                    Id Supervisor
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idSuper" TextMode="Number" Enabled="false"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="espQue" runat="server" Visible="false">
                <div class="row mt-2 centrado">
                    <div class="col-12 text-center">
                        <h5>Quejas</h5>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdQuejaInpText">
                                    Id Petición
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idQueja" TextMode="Number"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="tipoRemuText">
                                    Tipo de remuneración
                                </span>
                            </div>
                            <asp:DropDownList ID="tipoRemu" runat="server" CssClass="dropdown" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="espRec" runat="server" Visible="false">
                <div class="row mt-2 centrado">
                    <div class="col-12 text-center">
                        <h5>Reclamos</h5>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdRecInpText">
                                    Id Reclamo
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idReclamo" TextMode="Number"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="tipoReclText">
                                    Tipo de reclamo
                                </span>
                            </div>
                            <asp:DropDownList ID="tipoRecl" runat="server" CssClass="dropdown"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-12 col-sm-6 pt-2">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="costoInpText">
                                    Costo
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="costo" TextMode="Number" Enabled="false"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-2">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="tipoSolText">
                                    Tipo de Solución
                                </span>
                            </div>
                            <asp:DropDownList ID="tipoSol" runat="server" CssClass="dropdown" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="espSuge" runat="server" Visible="false">
                <div class="row mt-2 centrado">
                    <div class="col-12 text-center">
                        <h5>Sugerencias</h5>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdSugeInpText">
                                    Id Sugerencia
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idSuge" TextMode="Number"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="sugeText">
                                    Sugerencia
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="sugerencia" TextMode="MultiLine"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="espFeli" runat="server" Visible="false">
                <div class="row mt-2 centrado">
                    <div class="col-12 text-center">
                        <h5>Felicitaciones</h5>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="IdfeliInpText">
                                    Id felicitación
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="idFeli" TextMode="Number"/>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 pt-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="feliText">
                                    Felicitación
                                </span>
                            </div>
                            <asp:TextBox runat="server" CssClass="form-control" ID="felicitacion" TextMode="MultiLine"/>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row centrado mt-2">
                <div class="col-12 ">
                    <asp:Label ID="msje" runat="server" CssClass="alert alert-primary text-center" Text="" Width="100%" Visible="false"></asp:Label>
                </div>
                <div class="col-12 ">
                    <asp:Label ID="msjerr" runat="server" CssClass="alert alert-dancger text-center" Text="" Width="100%" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row centrado mt-3 mb-5">
                <div class="col-6 col-sm-3">
                    <asp:Button runat="server" ID="registrar" CssClass=" btn btn-success" Text="Registrar" OnClick="registrar_Click"/>
                </div>
                <div class="col-6 col-sm-3">
                    <asp:Button runat="server" ID="consultar" CssClass=" btn btn-primary" Text="Consultar" OnClick="consultar_Click"/>
                </div>
                <div class="col-6 col-sm-3">
                    <asp:Button runat="server" ID="modificar" CssClass=" btn btn-success" Text="Modificar" Enabled="false" OnClick="modificar_Click"/>
                </div>
                <div class="col-6 col-sm-3">
                    <asp:Button runat="server" ID="eliminar" CssClass=" btn btn-danger" Text="Eliminar" Enabled="false" OnClick="eliminar_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

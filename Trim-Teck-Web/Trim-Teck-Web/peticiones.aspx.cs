using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tiposSolicitudes;

namespace Trim_Teck_Web
{
    public partial class peticiones : System.Web.UI.Page
    {
        #region "Constantes"
        private const int UNO = 1;
        private const int DOS = 2;
        private const int CERO = 0;
        private const int TRES = 3;
        private const int CUATRO = 4;
        private const int CINCO = 5;
        private const string REGISTRADO = "Registrada correctamente";
        private const string MODIFICADO = "Registro modificado correctamente";
        private const string ELIMINADO = "Registro eliminado correctamente";
        private readonly string[] SERVICIOS = { "Seleccione un servicio", "Telefonía", "Internet", "Televisión", "Agua", "Luz", "Alcantarillado" };
        private readonly string[] TIPO_REMU = { "Seleccione", "Monetaria", "Aumento de capacidades", "Otras dádivas" };
        private readonly string[] TIPO_RECL = { "Seleccione", "Facturación", "Confuración", "Instalación" };
        private readonly string[] TIPO_SOLU = { "Seleccione", "Correción", "Aclaración", "Remuneración" };
        #endregion

        #region "Métodos privasos"
        private void cambiarPanel()
        {
            mostrarMensaje(string.Empty);
            ocultarPaneles();
            switch (this.solicitudes.SelectedIndex)
            {
                case 0:
                    this.espPeticion.Visible = true;
                    break;
                case 1:
                    this.espQue.Visible = true;
                    break;
                case 2:
                    this.espRec.Visible = true;
                    break;
                case 3:
                    this.espSuge.Visible = true;
                    break;
                case 4:
                    this.espFeli.Visible = true;
                    break;
                default:
                    goto case 1;
            }
            reiniElem();
        }

        private void reiniElem()
        {
            this.area.Text = string.Empty;
            this.idCliente.Text = string.Empty;
            this.fecha.SelectedDate = DateTime.Today;
            this.servicio.ClearSelection();
            this.idPeticion.Text = string.Empty;
            this.idSuper.Text = string.Empty;
            this.idPeticion.Text = string.Empty;
            this.tipoRemu.ClearSelection();
            this.idReclamo.Text = string.Empty;
            this.tipoRecl.ClearSelection();
            this.costo.Text = string.Empty;
            this.tipoSol.ClearSelection();
            this.idSuge.Text = string.Empty;
            this.sugerencia.Text = string.Empty;
            this.idFeli.Text = string.Empty;
            this.felicitacion.Text = string.Empty;
            this.msje.Text = string.Empty;
            this.msjerr.Text = string.Empty;
            mostrarMensaje(string.Empty);
            mostrarErr(string.Empty);
            this.modificar.Enabled = false;
            this.eliminar.Enabled = false;
            this.idSuper.Enabled = false;
            this.tipoRemu.Enabled = false;
        }

        private void ocultarPaneles()
        {
            this.espFeli.Visible = false;
            this.espPeticion.Visible = false;
            this.espQue.Visible = false;
            this.espRec.Visible = false;
            this.espSuge.Visible = false;
        }

        private void mostrarMensaje(string mensaje)
        {
            this.msje.Text = mensaje;
            if (mensaje == string.Empty)
            {
                this.msje.Visible = false;
                return;
            }
            this.msje.Visible = true;
        }
        private void mostrarErr(string mensaje)
        {
            this.msjerr.Text = mensaje;
            if (mensaje == string.Empty)
            {
                this.msjerr.Visible = false;
                return;
            }
            this.msjerr.Visible = true;
        }

        private void llenarServicios()
        {
            for (int i = CERO; i < SERVICIOS.Length; i++)
            {
                this.servicio.Items.Add(SERVICIOS[i]);
            }
        }
        private void llenarRemuneracion()
        {
            for (int i = CERO; i < TIPO_REMU.Length; i++)
            {
                this.tipoRemu.Items.Add(SERVICIOS[i]);
            }
        }
        private void llenarReclamo()
        {
            for (int i = CERO; i < TIPO_RECL.Length; i++)
            {
                this.tipoRecl.Items.Add(SERVICIOS[i]);
            }

            for (int i = CERO; i < TIPO_SOLU.Length; i++)
            {
                this.tipoSol.Items.Add(SERVICIOS[i]);
            }
        }

        private bool validarCom()
        {
            if (string.IsNullOrEmpty(this.area.Text))
            {
                const string AREA_VACIA = "El campo Area no puede estar vacio";
                mostrarErr(AREA_VACIA);
                return false;
            }
            if (string.IsNullOrEmpty(this.idCliente.Text))
            {
                const string ID_CLIENTE_VACIA = "El campo Id Cliente no puede estar vacio";
                mostrarErr(ID_CLIENTE_VACIA);
                return false;
            }
            if (this.servicio.SelectedIndex == CERO)
            {
                const string SERVICIO_NO_VALIDO = "Debe seleccionar un servicio";
                mostrarErr(SERVICIO_NO_VALIDO);
                return false;
            }
            return true;
        }
        private bool validarIngrRecl()
        {
            if (this.tipoRecl.SelectedIndex == CERO)
            {
                const string ERROR_SOLUCION = "Debe seleccionar un tipo de reclamo";
                mostrarErr(ERROR_SOLUCION);
                return false;
            }
            return true;
        }
        private bool validarInreSuge()
        {
            if (string.IsNullOrEmpty(this.sugerencia.Text))
            {
                const string ERROR_SUGERENCIA = "La sugerencia no puede estar vacia";
                mostrarErr(ERROR_SUGERENCIA);
                return false;
            }
            return true;
        }
        private bool validarIngrFeli()
        {
            if (string.IsNullOrEmpty(this.felicitacion.Text))
            {
                const string ERROR_FELICITACION = "La Felicitación no puede estar vacia";
                mostrarErr(ERROR_FELICITACION);
                return false;
            }
            return true;
        }
        private bool validarQueja()
        {
            if (this.tipoRemu.SelectedIndex == CERO)
            {
                const string ERROR_REMUNERACION = "Debe seleccionar un tipo de remuneración";
                mostrarErr(ERROR_REMUNERACION);
                return false;
            }
            return true;
        }
        private bool validarModiRecl()
        {
            if (this.tipoSol.SelectedIndex == CERO)
            {
                const string ERROR_SOLUCION = "Debe seleccionar un tipo de solución";
                mostrarErr(ERROR_SOLUCION);
                return false;
            }
            return true;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.fecha.SelectedDate = DateTime.Today;
                llenarServicios();
                this.servicio.SelectedIndex = CERO;
                llenarRemuneracion();
                this.tipoRemu.SelectedIndex = CERO;
                llenarReclamo();
                this.tipoRecl.SelectedIndex = CERO;
                this.tipoSol.SelectedIndex = CERO;
            }
        }

        protected void radio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cambiarPanel();
        }

        protected void limpiar_Click(object sender, EventArgs e)
        {
            reiniElem();
        }

        protected void registrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarCom())
                {
                    return;
                }
                switch (this.solicitudes.SelectedIndex)
                {
                    case 0:
                        Peticiones soliPeticion = new Peticiones();
                        soliPeticion.Area = Convert.ToInt32(this.area.Text);
                        soliPeticion.IdCliente = Convert.ToInt32(this.idCliente.Text);
                        soliPeticion.Servicio = this.servicio.SelectedIndex;
                        soliPeticion.Fecha = this.fecha.SelectedDate.ToString();
                        soliPeticion.TipoSoli = UNO;
                        if (soliPeticion.registrar())
                        {
                            this.idPeticion.Text = soliPeticion.IdPeticion.ToString();
                            soliPeticion = null;                            
                            mostrarMensaje(REGISTRADO);
                            break;
                        }
                        mostrarErr(soliPeticion.Error);
                        soliPeticion = null;
                        break;
                    case 1:
                        Quejas soliQuejas = new Quejas();
                        soliQuejas.Area = Convert.ToInt32(this.area.Text);
                        soliQuejas.IdCliente = Convert.ToInt32(this.idCliente.Text);
                        soliQuejas.Servicio = this.servicio.SelectedIndex;
                        soliQuejas.Fecha = this.fecha.SelectedDate.ToString();
                        soliQuejas.TipoSoli = DOS;
                        if (soliQuejas.registrar())
                        {
                            this.idQueja.Text = soliQuejas.IdQueja.ToString();
                            soliQuejas = null;
                            mostrarMensaje(REGISTRADO);
                            break;
                        }
                        mostrarErr(soliQuejas.Error);
                        soliQuejas = null;
                        break;
                    case 2:
                        if (validarIngrRecl())
                        {
                            Reclamos soliReclamo = new Reclamos();
                            soliReclamo.Area = Convert.ToInt32(this.area.Text);
                            soliReclamo.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliReclamo.Servicio = this.servicio.SelectedIndex;
                            soliReclamo.Fecha = this.fecha.SelectedDate.ToString();
                            soliReclamo.TipoSoli = TRES;
                            soliReclamo.IdTipoReclamo = this.tipoRecl.SelectedIndex;
                            if (soliReclamo.registrar())
                            {
                                this.idReclamo.Text = soliReclamo.IdTipoReclamo.ToString();
                                soliReclamo = null;
                                mostrarMensaje(REGISTRADO);
                                break;
                            }
                            mostrarErr(soliReclamo.Error);
                            soliReclamo = null;
                        }
                        break;
                    case 3:
                        if (validarInreSuge())
                        {
                            Sugerencias soliSugerencia = new Sugerencias();
                            soliSugerencia.Area = Convert.ToInt32(this.area.Text);
                            soliSugerencia.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliSugerencia.Servicio = this.servicio.SelectedIndex;
                            soliSugerencia.Fecha = this.fecha.SelectedDate.ToString();
                            soliSugerencia.TipoSoli = CUATRO;
                            soliSugerencia.Sugerencia = this.sugerencia.Text;
                            if (soliSugerencia.registrar())
                            {
                                this.idSuge.Text = soliSugerencia.IdSugerencia.ToString();
                                soliSugerencia = null;
                                mostrarMensaje(REGISTRADO);
                                break;
                            }
                            mostrarErr(soliSugerencia.Error);
                            soliSugerencia = null;
                        }
                        break;
                    case 4:
                        if (validarIngrFeli())
                        {
                            Felicitaciones soliFelicitacion = new Felicitaciones();
                            soliFelicitacion.Area = Convert.ToInt32(this.area.Text);
                            soliFelicitacion.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliFelicitacion.Servicio = this.servicio.SelectedIndex;
                            soliFelicitacion.Fecha = this.fecha.SelectedDate.ToString();
                            soliFelicitacion.TipoSoli = CINCO;
                            soliFelicitacion.Felicitacion = this.felicitacion.Text;
                            if (soliFelicitacion.registrar())
                            {
                                this.idFeli.Text = soliFelicitacion.IdFelicitacion.ToString();
                                soliFelicitacion = null;
                                mostrarMensaje(REGISTRADO);
                                break;
                            }
                            mostrarErr(soliFelicitacion.Error);
                            soliFelicitacion = null;
                        }
                        break;
                    default:
                        goto case 1;
                }
            }
            catch (Exception ex)
            {
                mostrarErr(ex.Message);
            }
        }

        protected void consultar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.solicitudes.SelectedIndex)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(this.idPeticion.Text))
                        {
                            Peticiones soliPeticion = new Peticiones();
                            soliPeticion.IdPeticion = Convert.ToInt32(this.idPeticion.Text);
                            if (soliPeticion.consultar())
                            {
                                this.area.Text = soliPeticion.Area.ToString();
                                this.idCliente.Text = soliPeticion.IdCliente.ToString();
                                this.fecha.SelectedDate = Convert.ToDateTime(soliPeticion.Fecha);
                                this.servicio.SelectedIndex = soliPeticion.Servicio;
                                this.idPeticion.Text = soliPeticion.IdPeticion.ToString();
                                this.idSuper.Text = soliPeticion.IdSupervisor.ToString();
                                soliPeticion = null;
                                this.modificar.Enabled = true;
                                this.eliminar.Enabled = true;
                                this.idSuper.Enabled = true;
                                break;
                            }
                            else
                            {
                                mostrarErr(soliPeticion.Error);
                                soliPeticion = null;
                                break;
                            }
                        }
                        else
                        {
                            const string ERROR_PETICION_ID = "Debe ingresar el id de la petición para consultar";
                            mostrarErr(ERROR_PETICION_ID);
                        }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(this.idQueja.Text))
                        {
                            Quejas soliQueja = new Quejas();
                            soliQueja.IdQueja = Convert.ToInt32(this.idQueja.Text);
                            if (soliQueja.consultar())
                            {
                                this.area.Text = soliQueja.Area.ToString();
                                this.idCliente.Text = soliQueja.IdCliente.ToString();
                                this.fecha.SelectedDate = Convert.ToDateTime(soliQueja.Fecha);
                                this.servicio.SelectedIndex = soliQueja.Servicio;
                                this.idQueja.Text = soliQueja.IdQueja.ToString();
                                this.tipoRemu.SelectedIndex = soliQueja.IdTipoRemuneracion;
                                soliQueja = null;
                                this.modificar.Enabled = true;
                                this.eliminar.Enabled = true;
                                this.tipoRemu.Enabled = true;
                                break;
                            }
                            else
                            {
                                mostrarErr(soliQueja.Error);
                                soliQueja = null;
                                break;
                            }
                        }
                        else
                        {
                            const string ERROR_PETICION_ID = "Debe ingresar el id de la queja para consultar";
                            mostrarErr(ERROR_PETICION_ID);
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(this.idReclamo.Text))
                        {
                            Reclamos soliReclamo = new Reclamos();
                            soliReclamo.IdReclamo = Convert.ToInt32(this.idReclamo.Text);
                            if (soliReclamo.consultar())
                            {
                                this.area.Text = soliReclamo.Area.ToString();
                                this.idCliente.Text = soliReclamo.IdCliente.ToString();
                                this.fecha.SelectedDate = Convert.ToDateTime(soliReclamo.Fecha);
                                this.servicio.SelectedIndex = soliReclamo.Servicio;
                                this.idReclamo.Text = soliReclamo.IdReclamo.ToString();
                                this.tipoRecl.SelectedIndex = soliReclamo.IdTipoReclamo;
                                this.costo.Text = soliReclamo.Costo.ToString();
                                this.tipoSol.SelectedIndex = soliReclamo.IdSolucion;
                                soliReclamo = null;
                                this.modificar.Enabled = true;
                                this.eliminar.Enabled = true;
                                this.tipoSol.Enabled = true;
                                this.costo.Enabled = true;
                                break;
                            }
                            else
                            {
                                mostrarErr(soliReclamo.Error);
                                soliReclamo = null;
                                break;
                            }
                        }
                        else
                        {
                            const string ERROR_PETICION_ID = "Debe ingresar el id del reclamo para consultar";
                            mostrarErr(ERROR_PETICION_ID);
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(this.idSuge.Text))
                        {
                            Sugerencias soliSugerencia = new Sugerencias();
                            soliSugerencia.IdSugerencia = Convert.ToInt32(this.idSuge.Text);
                            if (soliSugerencia.consultar())
                            {
                                this.area.Text = soliSugerencia.Area.ToString();
                                this.idCliente.Text = soliSugerencia.IdCliente.ToString();
                                this.fecha.SelectedDate = Convert.ToDateTime(soliSugerencia.Fecha);
                                this.servicio.SelectedIndex = soliSugerencia.Servicio;
                                this.idSuge.Text = soliSugerencia.IdSugerencia.ToString();
                                this.sugerencia.Text = soliSugerencia.Sugerencia;
                                soliSugerencia = null;
                                this.modificar.Enabled = true;
                                this.eliminar.Enabled = true;
                                break;
                            }
                            else
                            {
                                mostrarErr(soliSugerencia.Error);
                                soliSugerencia = null;
                                break;
                            }
                        }
                        else
                        {
                            const string ERROR_PETICION_ID = "Debe ingresar el id de la sugerencia para consultar";
                            mostrarErr(ERROR_PETICION_ID);
                        }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(this.idFeli.Text))
                        {
                            Felicitaciones soliFelicitacion = new Felicitaciones();
                            soliFelicitacion.IdFelicitacion = Convert.ToInt32(this.idFeli.Text);
                            if (soliFelicitacion.consultar())
                            {
                                this.area.Text = soliFelicitacion.Area.ToString();
                                this.idCliente.Text = soliFelicitacion.IdCliente.ToString();
                                this.fecha.SelectedDate = Convert.ToDateTime(soliFelicitacion.Fecha);
                                this.servicio.SelectedIndex = soliFelicitacion.Servicio;
                                this.idFeli.Text = soliFelicitacion.IdFelicitacion.ToString();
                                this.felicitacion.Text = soliFelicitacion.Felicitacion;
                                soliFelicitacion = null;
                                this.modificar.Enabled = true;
                                this.eliminar.Enabled = true;
                                break;
                            }
                            else
                            {
                                mostrarErr(soliFelicitacion.Error);
                                soliFelicitacion = null;
                                break;
                            }
                        }
                        else
                        {
                            const string ERROR_PETICION_ID = "Debe ingresar el id de la felicitación para consultar";
                            mostrarErr(ERROR_PETICION_ID);
                        }
                        break;
                    default:
                        goto case 0;
                }
            }
            catch (Exception ex)
            {
                mostrarErr(ex.Message);
            }
        }

        protected void modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarCom())
                {
                    return;
                }
                switch (this.solicitudes.SelectedIndex)
                {
                    case 0:
                        Peticiones soliPeticion = new Peticiones();
                        soliPeticion.Area = Convert.ToInt32(this.area.Text);
                        soliPeticion.IdCliente = Convert.ToInt32(this.idCliente.Text);
                        soliPeticion.Servicio = this.servicio.SelectedIndex;
                        soliPeticion.Fecha = this.fecha.SelectedDate.ToString();
                        soliPeticion.TipoSoli = UNO;
                        soliPeticion.IdSupervisor = Convert.ToInt32(this.idSuper.Text);
                        soliPeticion.IdPeticion = Convert.ToInt32(this.idPeticion.Text);
                        if (soliPeticion.modificar())
                        {
                            soliPeticion = null;
                            mostrarMensaje(MODIFICADO);
                            break;
                        }
                        mostrarErr(soliPeticion.Error);
                        soliPeticion = null;
                        break;
                    case 1:
                        if (validarQueja())
                        {
                            Quejas soliQuejas = new Quejas();
                            soliQuejas.Area = Convert.ToInt32(this.area.Text);
                            soliQuejas.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliQuejas.Servicio = this.servicio.SelectedIndex;
                            soliQuejas.Fecha = this.fecha.SelectedDate.ToString();
                            soliQuejas.TipoSoli = DOS;
                            soliQuejas.IdQueja = Convert.ToInt32(this.idQueja.Text);
                            soliQuejas.IdTipoRemuneracion = this.tipoRemu.SelectedIndex;
                            if (soliQuejas.modificar())
                            {
                                soliQuejas = null;
                                mostrarMensaje(MODIFICADO);
                                break;
                            }
                            mostrarErr(soliQuejas.Error);
                            soliQuejas = null;
                        }
                        break;
                    case 2:
                        if (validarIngrRecl() && validarModiRecl())
                        {
                            Reclamos soliReclamo = new Reclamos();
                            soliReclamo.Area = Convert.ToInt32(this.area.Text);
                            soliReclamo.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliReclamo.Servicio = this.servicio.SelectedIndex;
                            soliReclamo.Fecha = this.fecha.SelectedDate.ToString();
                            soliReclamo.TipoSoli = TRES;
                            soliReclamo.IdTipoReclamo = this.tipoRecl.SelectedIndex;
                            soliReclamo.IdReclamo = Convert.ToInt32(this.idReclamo.Text);
                            soliReclamo.IdSolucion = this.tipoSol.SelectedIndex;
                            soliReclamo.Costo = Convert.ToInt32(this.costo.Text);
                            if (soliReclamo.modificar())
                            {
                                soliReclamo = null;
                                mostrarMensaje(MODIFICADO);
                                break;
                            }
                            mostrarErr(soliReclamo.Error);
                            soliReclamo = null;
                        }
                        break;
                    case 3:
                        if (validarInreSuge())
                        {
                            Sugerencias soliSugerencia = new Sugerencias();
                            soliSugerencia.Area = Convert.ToInt32(this.area.Text);
                            soliSugerencia.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliSugerencia.Servicio = this.servicio.SelectedIndex;
                            soliSugerencia.Fecha = this.fecha.SelectedDate.ToString();
                            soliSugerencia.TipoSoli = CUATRO;
                            soliSugerencia.Sugerencia = this.sugerencia.Text;
                            soliSugerencia.IdSugerencia = Convert.ToInt32(this.idSuge.Text);
                            if (soliSugerencia.modificar())
                            {
                                soliSugerencia = null;
                                mostrarMensaje(MODIFICADO);
                                break;
                            }
                            mostrarErr(soliSugerencia.Error);
                            soliSugerencia = null;
                        }
                        break;
                    case 4:
                        if (validarIngrFeli())
                        {
                            Felicitaciones soliFelicitacion = new Felicitaciones();
                            soliFelicitacion.Area = Convert.ToInt32(this.area.Text);
                            soliFelicitacion.IdCliente = Convert.ToInt32(this.idCliente.Text);
                            soliFelicitacion.Servicio = this.servicio.SelectedIndex;
                            soliFelicitacion.Fecha = this.fecha.SelectedDate.ToString();
                            soliFelicitacion.TipoSoli = CINCO;
                            soliFelicitacion.Felicitacion = this.felicitacion.Text;
                            soliFelicitacion.IdFelicitacion = Convert.ToInt32(this.idFeli.Text);
                            if (soliFelicitacion.modificar())
                            {
                                soliFelicitacion = null;
                                mostrarMensaje(MODIFICADO);
                                break;
                            }
                            mostrarErr(soliFelicitacion.Error);
                            soliFelicitacion = null;
                        }
                        break;
                    default:
                        goto case 0;
                }
            }
            catch (Exception ex)
            {
                mostrarErr(ex.Message);
            }
        }

        protected void eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.solicitudes.SelectedIndex)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(this.idPeticion.Text))
                        {
                            Peticiones soliPeticion = new Peticiones();
                            soliPeticion.IdPeticion = Convert.ToInt32(this.idPeticion.Text);
                            if (soliPeticion.eliminar())
                            {
                                reiniElem();
                                mostrarMensaje(ELIMINADO);
                                soliPeticion = null;
                                break;
                            }
                            mostrarErr(soliPeticion.Error);
                            soliPeticion = null;
                            break;
                        }
                        const string ID_PETICIO_VACIO = "Debe ingresar el id de la petición a eliminar";
                        mostrarErr(ID_PETICIO_VACIO);
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(this.idQueja.Text))
                        {
                            Quejas soliQueja = new Quejas();
                            soliQueja.IdQueja = Convert.ToInt32(this.idQueja.Text);
                            if (soliQueja.eliminar())
                            {
                                reiniElem();
                                mostrarMensaje(ELIMINADO);
                                soliQueja = null;
                                break;
                            }
                            mostrarErr(soliQueja.Error);
                            soliQueja = null;
                            break;
                        }
                        const string ID_QUEJA_VACIO = "Debe ingresar el id de la queja a eliminar";
                        mostrarErr(ID_QUEJA_VACIO);
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(this.idReclamo.Text))
                        {
                            Reclamos soliReclamo = new Reclamos();
                            soliReclamo.IdReclamo = Convert.ToInt32(this.idReclamo.Text);
                            if (soliReclamo.eliminar())
                            {
                                reiniElem();
                                mostrarMensaje(ELIMINADO);
                                soliReclamo = null;
                                break;
                            }
                            mostrarErr(soliReclamo.Error);
                            soliReclamo = null;
                            break;
                        }
                        const string ID_RECLAMO_VACIO = "Debe ingresar el id del reclamo a eliminar";
                        mostrarErr(ID_RECLAMO_VACIO);
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(this.idSuge.Text))
                        {
                            Sugerencias soliSugerencia = new Sugerencias();
                            soliSugerencia.IdSugerencia = Convert.ToInt32(this.idSuge.Text);
                            if (soliSugerencia.eliminar())
                            {
                                reiniElem();
                                mostrarMensaje(ELIMINADO);
                                soliSugerencia = null;
                                break;
                            }
                            mostrarErr(soliSugerencia.Error);
                            soliSugerencia = null;
                            break;
                        }
                        const string ID_SUGERENCIA_VACIO = "Debe ingresar el id de la sugerencia a eliminar";
                        mostrarErr(ID_SUGERENCIA_VACIO);
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(this.idFeli.Text))
                        {
                            Felicitaciones solifelicitacion = new Felicitaciones();
                            solifelicitacion.IdFelicitacion = Convert.ToInt32(this.idFeli.Text);
                            if (solifelicitacion.eliminar())
                            {
                                reiniElem();
                                mostrarMensaje(ELIMINADO);
                                solifelicitacion = null;
                                break;
                            }
                            mostrarErr(solifelicitacion.Error);
                            solifelicitacion = null;
                            break;
                        }
                        const string ID_FELICITACION_VACIO = "Debe ingresar el id de la felicitación a eliminar";
                        mostrarErr(ID_FELICITACION_VACIO);
                        break;
                    default:
                        goto case 0;
                }
            }
            catch (Exception ex)
            {
                mostrarErr(ex.Message);
            }
        }
    }
}
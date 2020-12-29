using System;
using System.Windows.Forms;
using tiposSolicitudes;

namespace Trim_Teck
{
    public partial class TrimTeck : Form
    {
        #region "Constantes"
        private const int UNO = 1;
        private const int DOS = 2;
        private const int CERO = 0;
        private const int TRES = 3;
        private const int CUATRO = 4;
        private const int CINCO = 5;
        private const string VACIO = "";
        private const string ERROR = "ERROR";
        private const string INFORMATION = "Atención";
        private const string WARNING = "Advertencia";
        private const string EXCEPTION = "Exception";
        private readonly string[] SERVICIOS = { "Seleccione un servicio", "Telefonía", "Internet", "Televisión", "Agua", "Luz", "Alcantarillado" };
        private readonly string[] TIPO_REMU = { "Seleccione", "Monetaria", "Aumento de capacidades", "Otras dádivas" };
        private readonly string[] TIPO_RECL = { "Seleccione", "Facturación", "Confuración", "Instalación" };
        private readonly string[] TIPO_SOLU = { "Seleccione", "Correción", "Aclaración", "Remuneración" };
        #endregion
        public TrimTeck()
        {
            InitializeComponent();
        }

        #region "Métodos privados"
        private void TrimTeck_Load(object sender, EventArgs e)
        {
            llenarServicios();
            cbServicio.SelectedIndex = CERO;
            llenarRemuneracion();
            cbRemuneracion.SelectedIndex = CERO;
            llenarReclamo();
            cbReclamo.SelectedIndex = CERO;
            cbSolucion.SelectedIndex = CERO;
        }

        private void llenarServicios()
        {
            for (int i = CERO; i < SERVICIOS.Length; i++)
            {
                cbServicio.Items.Add(SERVICIOS[i]);
            }
        }

        private void limpiar()
        {
            txtArea.Text = VACIO;
            txtIdCliente.Text = VACIO;
            txtIdPeticion.Text = VACIO;
            txtIdSupervisor.Text = VACIO;
            cbServicio.SelectedIndex = CERO;
            cbRemuneracion.SelectedIndex = CERO;
            cbReclamo.SelectedIndex = CERO;
            cbSolucion.SelectedIndex = CERO;
            txtReclamo.Text = VACIO;
            txtCosto.Text = VACIO;
            txtIdQueja.Text = VACIO;
            txtSugerencia.Text = VACIO;
            txtFelicitacion.Text = VACIO;
            rtxtSugerencia.Text = VACIO;
            rtxtFelicitacion.Text = VACIO;
            dtpFecha.Value = DateTime.Now;
        }

        private void reiniciar()
        {
            modificarPeticion.Enabled = false;
            eliminarPeticion.Enabled = false;
            modificarQueja.Enabled = false;
            eliminarQueja.Enabled = false;
            txtIdSupervisor.Enabled = false;
            cbRemuneracion.Enabled = false;
            cbSolucion.Enabled = false;
            txtCosto.Enabled = false;
            modificarSugerencia.Enabled = false;
            eliminarPeticion.Enabled = false;
            modificarFelicitacion.Enabled = false;
            eliminarFelicitacion.Enabled = false;
        }

        private bool validar()
        {
            if (string.IsNullOrEmpty(txtArea.Text))
            {
                const string AREA_VACIA = "El campo Area no puede estar vacio";
                MessageBox.Show(AREA_VACIA, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtIdCliente.Text))
            {
                const string ID_CLIENTE_VACIA = "El campo Id Cliente no puede estar vacio";
                MessageBox.Show(ID_CLIENTE_VACIA, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbServicio.SelectedIndex == CERO)
            {
                const string SERVICIO_NO_VALIDO = "Debe seleccionar un servicio";
                MessageBox.Show(SERVICIO_NO_VALIDO, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            reiniciar();
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #region "Métodos para peticiones"
        private void consultarPeticion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdPeticion.Text))
                {
                    Peticiones soliPeticion = new Peticiones();
                    soliPeticion.IdPeticion = Convert.ToInt32(txtIdPeticion.Text);
                    if (soliPeticion.consultar())
                    {
                        txtArea.Text = soliPeticion.Area.ToString();
                        txtIdCliente.Text = soliPeticion.IdCliente.ToString();
                        dtpFecha.Value = Convert.ToDateTime(soliPeticion.Fecha);
                        cbServicio.SelectedIndex = soliPeticion.Servicio;
                        txtIdPeticion.Text = soliPeticion.IdPeticion.ToString();
                        txtIdSupervisor.Text = soliPeticion.IdSupervisor.ToString();
                        soliPeticion = null;
                        modificarPeticion.Enabled = true;
                        eliminarPeticion.Enabled = true;
                        txtIdSupervisor.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(soliPeticion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void registrarPeticion_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar())
                {
                    Peticiones soliPeticion = new Peticiones();
                    soliPeticion.Area = Convert.ToInt32(txtArea.Text);
                    soliPeticion.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliPeticion.Servicio = cbServicio.SelectedIndex;
                    soliPeticion.Fecha = dtpFecha.Value.ToString();
                    soliPeticion.TipoSoli = UNO;
                    if (soliPeticion.registrar())
                    {
                        txtIdPeticion.Text = soliPeticion.IdPeticion.ToString();
                        soliPeticion = null;
                        const string REGISTRADO = "Registrado correctamente";
                        MessageBox.Show(REGISTRADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliPeticion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarPeticion_Click(object sender, EventArgs e)
        {
            try
            {
                Peticiones soliPeticion = new Peticiones();
                soliPeticion.Area = Convert.ToInt32(txtArea.Text);
                soliPeticion.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                soliPeticion.Servicio = cbServicio.SelectedIndex;
                soliPeticion.Fecha = dtpFecha.Value.ToString();
                soliPeticion.TipoSoli = UNO;
                soliPeticion.IdSupervisor = Convert.ToInt32(txtIdSupervisor.Text);
                soliPeticion.IdPeticion = Convert.ToInt32(txtIdPeticion.Text);
                if (soliPeticion.modificar())
                {
                    soliPeticion = null;
                    const string MODIFICADO = "Registro modificado correctamente";
                    MessageBox.Show(MODIFICADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliPeticion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarPeticion_Click(object sender, EventArgs e)
        {
            try
            {
                
                Peticiones soliPeticion = new Peticiones();
                soliPeticion.IdPeticion = Convert.ToInt32(txtIdPeticion.Text);
                if (soliPeticion.eliminar())
                {
                    limpiar();
                    reiniciar();
                    const string ELIMINADO = "Registro eliminado correctamente";
                    MessageBox.Show(ELIMINADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliPeticion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtIdPeticion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtIdSupervisor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        } 
        #endregion



        #region "Métodos para quejas"
        private void txtIdQueja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void llenarRemuneracion()
        {
            for (int i = CERO; i < TIPO_REMU.Length; i++)
            {
                cbRemuneracion.Items.Add(TIPO_REMU[i]);
            }
        }

        private bool validarQueja()
        {
            if (cbRemuneracion.SelectedIndex == CERO)
            {
                const string ERROR_REMUNERACION = "Debe seleccionar un tipo de remuneración";
                MessageBox.Show(ERROR_REMUNERACION, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void registrarQueja_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar())
                {
                    Quejas soliQueja = new Quejas();
                    soliQueja.Area = Convert.ToInt32(txtArea.Text);
                    soliQueja.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliQueja.Servicio = cbServicio.SelectedIndex;
                    soliQueja.Fecha = dtpFecha.Value.ToString();
                    soliQueja.TipoSoli = DOS;
                    if (soliQueja.registrar())
                    {
                        txtIdQueja.Text = soliQueja.IdQueja.ToString();
                        soliQueja = null;
                        const string REGISTRADO = "Registrada correctamente";
                        MessageBox.Show(REGISTRADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliQueja.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultarQueja_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdQueja.Text))
                {
                    Quejas soliQueja = new Quejas();
                    soliQueja.IdQueja = Convert.ToInt32(txtIdQueja.Text);
                    if (soliQueja.consultar())
                    {
                        txtArea.Text = soliQueja.Area.ToString();
                        txtIdCliente.Text = soliQueja.IdCliente.ToString();
                        dtpFecha.Value = Convert.ToDateTime(soliQueja.Fecha);
                        cbServicio.SelectedIndex = soliQueja.Servicio;
                        txtIdQueja.Text = soliQueja.IdQueja.ToString();
                        cbRemuneracion.SelectedIndex = soliQueja.IdTipoRemuneracion;
                        soliQueja = null;
                        modificarQueja.Enabled = true;
                        eliminarQueja.Enabled = true;
                        cbRemuneracion.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(soliQueja.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarQueja_Click(object sender, EventArgs e)
        {
            try
            {
                if (validarQueja())
                {
                    Quejas soliQueja = new Quejas();
                    soliQueja.Area = Convert.ToInt32(txtArea.Text);
                    soliQueja.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliQueja.Servicio = cbServicio.SelectedIndex;
                    soliQueja.Fecha = dtpFecha.Value.ToString();
                    soliQueja.TipoSoli = DOS;
                    soliQueja.IdQueja = Convert.ToInt32(txtIdQueja.Text);
                    soliQueja.IdTipoRemuneracion = cbRemuneracion.SelectedIndex;
                    if (soliQueja.modificar())
                    {
                        soliQueja = null;
                        const string MODIFICADO = "Registro modificado correctamente";
                        MessageBox.Show(MODIFICADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliQueja.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarQueja_Click(object sender, EventArgs e)
        {
            try
            {
                Quejas soliQueja = new Quejas();
                soliQueja.IdQueja = Convert.ToInt32(txtIdQueja.Text);
                if (soliQueja.eliminar())
                {
                    limpiar();
                    reiniciar();
                    const string ELIMINADO = "Registro eliminado correctamente";
                    MessageBox.Show(ELIMINADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliQueja.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        #region "Métodos para Reclamos"
        private void txtReclamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void llenarReclamo()
        {
            for (int i = CERO; i < TIPO_RECL.Length; i++)
            {
                cbReclamo.Items.Add(TIPO_RECL[i]);
            }

            for (int i = CERO; i < TIPO_SOLU.Length; i++)
            {
                cbSolucion.Items.Add(TIPO_SOLU[i]);
            }
        }

        private bool validaringreRecl()
        {
            if (validar())
            {
                if (cbReclamo.SelectedIndex == 0)
                {
                    const string ERROR_RECLAMO = "Debe seleccionar un tipo de reclamo";
                    MessageBox.Show(ERROR_RECLAMO, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private bool validarmodiRecl()
        {
            if (cbSolucion.SelectedIndex == CERO)
            {
                const string ERROR_SOLUCION = "Debe seleccionar un tipo de solución";
                MessageBox.Show(ERROR_SOLUCION, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void registrarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaringreRecl())
                {
                    Reclamos solReclamo = new Reclamos();
                    solReclamo.Area = Convert.ToInt32(txtArea.Text);
                    solReclamo.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    solReclamo.Servicio = cbServicio.SelectedIndex;
                    solReclamo.Fecha = dtpFecha.Value.ToString();
                    solReclamo.TipoSoli = TRES;
                    solReclamo.IdTipoReclamo = cbReclamo.SelectedIndex;
                    if (solReclamo.registrar())
                    {
                        txtReclamo.Text = solReclamo.IdReclamo.ToString();
                        solReclamo = null;
                        const string REGISTRADO = "Registrado correctamente";
                        MessageBox.Show(REGISTRADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(solReclamo.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtReclamo.Text))
                {
                    Reclamos solReclamo = new Reclamos();
                    solReclamo.IdReclamo = Convert.ToInt32(txtReclamo.Text);
                    if (solReclamo.consultar())
                    {
                        txtArea.Text = solReclamo.Area.ToString();
                        txtIdCliente.Text = solReclamo.IdCliente.ToString();
                        dtpFecha.Value = Convert.ToDateTime(solReclamo.Fecha);
                        cbServicio.SelectedIndex = solReclamo.Servicio;
                        txtReclamo.Text = solReclamo.IdReclamo.ToString();
                        cbReclamo.SelectedIndex = solReclamo.IdTipoReclamo;
                        txtCosto.Text = solReclamo.Costo.ToString();
                        cbSolucion.SelectedIndex = solReclamo.IdSolucion;
                        solReclamo = null;
                        modificarReclamo.Enabled = true;
                        eliminarReclamo.Enabled = true;
                        cbSolucion.Enabled = true;
                        txtCosto.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(solReclamo.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                if (validaringreRecl() && validarmodiRecl())
                {
                    Reclamos soliReclamo = new Reclamos();
                    soliReclamo.Area = Convert.ToInt32(txtArea.Text);
                    soliReclamo.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliReclamo.Servicio = cbServicio.SelectedIndex;
                    soliReclamo.Fecha = dtpFecha.Value.ToString();
                    soliReclamo.TipoSoli = TRES;
                    soliReclamo.IdReclamo = Convert.ToInt32(txtReclamo.Text);
                    soliReclamo.IdTipoReclamo = cbReclamo.SelectedIndex;
                    soliReclamo.IdSolucion = cbSolucion.SelectedIndex;
                    soliReclamo.Costo = Convert.ToDouble(txtCosto.Text);
                    if (soliReclamo.modificar())
                    {
                        soliReclamo = null;
                        const string MODIFICADO = "Registro modificado correctamente";
                        MessageBox.Show(MODIFICADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliReclamo.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                Reclamos soliReclamo = new Reclamos();
                soliReclamo.IdReclamo = Convert.ToInt32(txtReclamo.Text);
                if (soliReclamo.eliminar())
                {
                    limpiar();
                    reiniciar();
                    const string ELIMINADO = "Registro eliminado correctamente";
                    MessageBox.Show(ELIMINADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliReclamo.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region "Métodos para Sugerencias"
        private void txtSugerencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool validarSugerencia()
        {
            if (string.IsNullOrEmpty(rtxtSugerencia.Text))
            {
                const string ERROR_SUGERENCIA = "La sugerencia no puede estar vacia";
                MessageBox.Show(ERROR_SUGERENCIA, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void registrarSugerencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar() && validarSugerencia())
                {
                    Sugerencias soliSugerencia = new Sugerencias(); 
                    soliSugerencia.Area = Convert.ToInt32(txtArea.Text);
                    soliSugerencia.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliSugerencia.Servicio = cbServicio.SelectedIndex;
                    soliSugerencia.Fecha = dtpFecha.Value.ToString();
                    soliSugerencia.TipoSoli = CUATRO;
                    soliSugerencia.Sugerencia = rtxtSugerencia.Text;
                    if (soliSugerencia.registrar())
                    {
                        txtSugerencia.Text = soliSugerencia.IdSugerencia.ToString();
                        soliSugerencia = null;
                        const string REGISTRADO = "Registrado correctamente";
                        MessageBox.Show(REGISTRADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliSugerencia.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultarSugerencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSugerencia.Text))
                {
                    Sugerencias soliSugerencia = new Sugerencias();
                    soliSugerencia.IdSugerencia = Convert.ToInt32(txtSugerencia.Text);
                    if (soliSugerencia.consultar())
                    {
                        txtArea.Text = soliSugerencia.Area.ToString();
                        txtIdCliente.Text = soliSugerencia.IdCliente.ToString();
                        dtpFecha.Value = Convert.ToDateTime(soliSugerencia.Fecha);
                        cbServicio.SelectedIndex = soliSugerencia.Servicio;
                        txtSugerencia.Text = soliSugerencia.IdSugerencia.ToString();
                        rtxtSugerencia.Text = soliSugerencia.Sugerencia;
                        soliSugerencia = null;
                        modificarSugerencia.Enabled = true;
                        eliminarSugerencia.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(soliSugerencia.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarSugerencia_Click(object sender, EventArgs e)
        {
            try
            {
                Sugerencias soliSugerencia = new Sugerencias();
                soliSugerencia.Area = Convert.ToInt32(txtArea.Text);
                soliSugerencia.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                soliSugerencia.Servicio = cbServicio.SelectedIndex;
                soliSugerencia.Fecha = dtpFecha.Value.ToString();
                soliSugerencia.TipoSoli = CUATRO;
                soliSugerencia.IdSugerencia = Convert.ToInt32(txtSugerencia.Text);
                soliSugerencia.Sugerencia = rtxtSugerencia.Text;
                if (soliSugerencia.modificar())
                {
                    soliSugerencia = null;
                    const string MODIFICADO = "Registro modificado correctamente";
                    MessageBox.Show(MODIFICADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliSugerencia.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarSugerencia_Click(object sender, EventArgs e)
        {
            try
            {

                Sugerencias soliSugerencia = new Sugerencias();
                soliSugerencia.IdSugerencia = Convert.ToInt32(txtSugerencia.Text);
                if (soliSugerencia.eliminar())
                {
                    limpiar();
                    reiniciar();
                    const string ELIMINADO = "Registro eliminado correctamente";
                    MessageBox.Show(ELIMINADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliSugerencia.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region "Métodos para Felicitaciones"
        private void txtFelicitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool validarFeli()
        {
            if (string.IsNullOrEmpty(rtxtFelicitacion.Text))
            {
                const string ERROR_FELICITACION = "La Felicitación no puede estar vacia";
                MessageBox.Show(ERROR_FELICITACION, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void registrarFelicitacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar() && validarFeli())
                {
                    Felicitaciones soliFelicitacion = new Felicitaciones();
                    soliFelicitacion.Area = Convert.ToInt32(txtArea.Text);
                    soliFelicitacion.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                    soliFelicitacion.Servicio = cbServicio.SelectedIndex;
                    soliFelicitacion.Fecha = dtpFecha.Value.ToString();
                    soliFelicitacion.TipoSoli = CINCO;
                    soliFelicitacion.Felicitacion = rtxtFelicitacion.Text;
                    if (soliFelicitacion.registrar())
                    {
                        txtFelicitacion.Text = soliFelicitacion.IdFelicitacion.ToString();
                        soliFelicitacion = null;
                        const string REGISTRADO = "Registrado correctamente";
                        MessageBox.Show(REGISTRADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(soliFelicitacion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultarFelicitacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFelicitacion.Text))
                {
                    Felicitaciones soliFelicitacion = new Felicitaciones();
                    soliFelicitacion.IdFelicitacion = Convert.ToInt32(txtFelicitacion.Text);
                    if (soliFelicitacion.consultar())
                    {
                        txtArea.Text = soliFelicitacion.Area.ToString();
                        txtIdCliente.Text = soliFelicitacion.IdCliente.ToString();
                        dtpFecha.Value = Convert.ToDateTime(soliFelicitacion.Fecha);
                        cbServicio.SelectedIndex = soliFelicitacion.Servicio;
                        txtFelicitacion.Text = soliFelicitacion.IdFelicitacion.ToString();
                        rtxtFelicitacion.Text = soliFelicitacion.Felicitacion;
                        soliFelicitacion = null;
                        modificarFelicitacion.Enabled = true;
                        eliminarFelicitacion.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(soliFelicitacion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarFelicitacion_Click(object sender, EventArgs e)
        {
            try
            {
                Felicitaciones soliFelicitacion = new Felicitaciones();
                soliFelicitacion.Area = Convert.ToInt32(txtArea.Text);
                soliFelicitacion.IdCliente = Convert.ToInt32(txtIdCliente.Text);
                soliFelicitacion.Servicio = cbServicio.SelectedIndex;
                soliFelicitacion.Fecha = dtpFecha.Value.ToString();
                soliFelicitacion.TipoSoli = CINCO;
                soliFelicitacion.IdFelicitacion = Convert.ToInt32(txtFelicitacion.Text);
                soliFelicitacion.Felicitacion = rtxtFelicitacion.Text;
                if (soliFelicitacion.modificar())
                {
                    soliFelicitacion = null;
                    const string MODIFICADO = "Registro modificado correctamente";
                    MessageBox.Show(MODIFICADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliFelicitacion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarFelicitacion_Click(object sender, EventArgs e)
        {
            try
            {
                Felicitaciones soliFelicitacion = new Felicitaciones();
                soliFelicitacion.IdFelicitacion = Convert.ToInt32(txtFelicitacion.Text);
                if (soliFelicitacion.eliminar())
                {
                    limpiar();
                    reiniciar();
                    const string ELIMINADO = "Registro eliminado correctamente";
                    MessageBox.Show(ELIMINADO, INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(soliFelicitacion.Error, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        #endregion
    }
}

namespace Trim_Teck_PQRSF
{
    public abstract class Solicitudes
    {
        #region "Atributos"
        protected int intArea;
        protected int intIdCliente;
        protected int intServicio;
        protected int intTipoSoli;
        protected string strFecha;
        protected string strError;
        #endregion

        #region "Propiedades"
        public int Area { get => intArea; set => intArea = value; }
        public int IdCliente { get => intIdCliente; set => intIdCliente = value; }
        public int Servicio { get => intServicio; set => intServicio = value; }
        public int TipoSoli { get => intTipoSoli; set => intTipoSoli = value; }
        public string Fecha { get => strFecha; set => strFecha = value; }
        public string Error { get => strError; set => strError = value; }
        #endregion

        #region "Métodos públicos"
        public abstract bool registrar();

        public abstract bool modificar();

        public abstract bool eliminar();

        public abstract bool consultar();
        #endregion
    }
}

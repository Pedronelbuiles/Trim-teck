using System;
using Trim_Teck_PQRSF;
using SpreadsheetLight;

namespace tiposSolicitudes
{
    public class Quejas : Solicitudes
    {
        #region "Constantes"
        private const int UNO = 1;
        private const int DOS = 2;
        private const int TRES = 3;
        private const int CERO = 0;
        private const int SEIS = 6;
        private const int CINCO = 5;
        private const int SIETE = 7;
        private const int CUATRO = 4;
        private const string VACIO = "";
        private const int MENOS_UNO = -1;
        private const int FILA_INICIAL = 2;
        private const int LIMITE_ID = 9999999;
        private const string NOMBRE_HOJA = "Quejas";
        private const string DOCUMENT_NAME = "Datos.xlsx";
        private const string MAS_DIGITOS = " ó tener más de 7 dígitos";
        private const string AREA = "Area";
        private const string ID_CLIENTE = "idCliente";
        private const string SERVICIO = "servicio";
        private const string FECHA = "fecha";
        private const string TIPO_SOLI = "tipoSoli";
        private const string ID_QUEJA = "IdQueja";
        private const string ID_TIPO_REMU = "idTipoRemuneracion";
        #endregion

        #region "Atributos"
        private int intIdQueja;
        private int intIdTipoRemuneracion;
        private string path;
        private int fila_actual;
        #endregion

        #region "Constructor"
        public Quejas()
        {
            intArea = CERO;
            strFecha = VACIO;
            strError = VACIO;
            intServicio = CERO;
            intTipoSoli = CERO;
            intIdCliente = CERO;
            intIdQueja = CERO;
            intIdTipoRemuneracion = CERO;
            path = AppDomain.CurrentDomain.BaseDirectory + DOCUMENT_NAME;
        }
        #endregion

        #region "Propiedades"
        public int IdQueja { get => intIdQueja; set => intIdQueja = value; }
        public int IdTipoRemuneracion { get => intIdTipoRemuneracion; set => intIdTipoRemuneracion = value; }
        #endregion


        #region "Métodos privados"
        private bool validar()
        {
            if (intArea <= CERO || intArea > LIMITE_ID)
            {
                const string ERROR_AREA = "El Código de area no puede ser menor a 0";
                strError = ERROR_AREA + MAS_DIGITOS;
                return false;
            }
            if (intIdCliente <= CERO || intIdCliente > LIMITE_ID)
            {
                const string ERROR_AREA = "El Código del cliente no puede ser menor a 0";
                strError = ERROR_AREA + MAS_DIGITOS;
                return false;
            }
            if (intIdQueja <= MENOS_UNO || intIdQueja > LIMITE_ID)
            {
                const string ERROR_ID_PETICION = "El Código de la queja no puede ser menor a 0";
                strError = ERROR_ID_PETICION + MAS_DIGITOS;
                return false;
            }
            if (intIdTipoRemuneracion <= MENOS_UNO || intIdTipoRemuneracion > LIMITE_ID)
            {
                const string ERROR_ID_SUPERVISION = "El Código del tipo de remuneración no puede ser menor a 0";
                strError = ERROR_ID_SUPERVISION + MAS_DIGITOS;
                return false;
            }
            if (intServicio <= CERO || intServicio > SEIS)
            {
                const string ERROR_SERVICIO = "El Código del servicio no puede ser menor a 0 ó mayor a 6";
                strError = ERROR_SERVICIO;
                return false;
            }
            if (intTipoSoli <= CERO || intTipoSoli > CINCO)
            {
                const string ERROR_TIPO_SOLI = "El Código del Tipo del servicio no puede ser menor a 0 ó mayor a 5";
                strError = ERROR_TIPO_SOLI;
                return false;
            }
            if (strFecha == VACIO)
            {
                const string ERROR_FECHA = "La fecha no puede estar vacia";
                strError = ERROR_FECHA;
                return false;
            }

            return true;
        }

        private bool encontrarFila()
        {
            SLDocument documento = new SLDocument(path, NOMBRE_HOJA);
            fila_actual = FILA_INICIAL;
            while (!string.IsNullOrEmpty(documento.GetCellValueAsString(fila_actual, UNO)))
            {
                if (intIdQueja == documento.GetCellValueAsInt32(fila_actual, SEIS))
                {
                    break;
                }
                fila_actual++;
            }
            return true;
        }

        private bool puedeConsultar()
        {
            if (intIdQueja == CERO)
            {
                const string NO_PUEDE_CONSULTAR = "Esta Queja no se puede ingresar o consultar";
                strError = NO_PUEDE_CONSULTAR;
                return false;
            }
            return true;
        }

        #endregion

        #region "Métodos publicos"
        public override bool consultar()
        {
            try
            {
                bool flag = false;
                if (!puedeConsultar())
                {
                    return false;
                }
                SLDocument documento = new SLDocument(path, NOMBRE_HOJA);
                fila_actual = FILA_INICIAL;
                while (!string.IsNullOrEmpty(documento.GetCellValueAsString(fila_actual, UNO)))
                {
                    if (intIdQueja == documento.GetCellValueAsInt32(fila_actual, SEIS))
                    {
                        intArea = documento.GetCellValueAsInt32(fila_actual, UNO);
                        intIdCliente = documento.GetCellValueAsInt32(fila_actual, DOS);
                        intServicio = documento.GetCellValueAsInt32(fila_actual, TRES);
                        intTipoSoli = documento.GetCellValueAsInt32(fila_actual, CUATRO);
                        strFecha = documento.GetCellValueAsString(fila_actual, CINCO);
                        intIdQueja = documento.GetCellValueAsInt32(fila_actual, SEIS);
                        intIdTipoRemuneracion = documento.GetCellValueAsInt32(fila_actual, SIETE);
                        flag = true;
                        break;
                    }
                    fila_actual++;
                }
                if (!flag)
                {
                    const string NO_ENCONTRADO = "La queja que se está consultando no esta registrada";
                    strError = NO_ENCONTRADO;
                    documento.CloseWithoutSaving();
                    documento = null;
                    return false;
                }
                documento.CloseWithoutSaving();
                documento = null;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool eliminar()
        {
            try
            {
                if (consultar())
                {
                    SLDocument documento = new SLDocument(path, NOMBRE_HOJA);
                    documento.DeleteRow(fila_actual, UNO);
                    documento.Save();
                    documento = null;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool modificar()
        {
            try
            {
                if (!validar())
                {
                    return false;
                }
                if (encontrarFila())
                {
                    SLDocument documento = new SLDocument(path, NOMBRE_HOJA);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    dataTable.Columns.Add(AREA, typeof(int));
                    dataTable.Columns.Add(ID_CLIENTE, typeof(int));
                    dataTable.Columns.Add(SERVICIO, typeof(int));
                    dataTable.Columns.Add(TIPO_SOLI, typeof(int));
                    dataTable.Columns.Add(FECHA, typeof(string));
                    dataTable.Columns.Add(ID_QUEJA, typeof(int));
                    dataTable.Columns.Add(ID_TIPO_REMU, typeof(int));
                    dataTable.Rows.Add(intArea, intIdCliente, intServicio, intTipoSoli, strFecha, intIdQueja, intIdTipoRemuneracion);
                    documento.ImportDataTable(fila_actual, UNO, dataTable, false);
                    documento.Save();
                    dataTable = null;
                    documento = null;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool registrar()
        {
            try
            {
                if (!validar())
                {
                    return false;
                }
                intIdQueja = UNO;
                if (!consultar())
                {
                    if (puedeConsultar())
                    {
                        SLDocument documento = new SLDocument(path, NOMBRE_HOJA);
                        fila_actual = FILA_INICIAL;
                        while (!string.IsNullOrEmpty(documento.GetCellValueAsString(fila_actual, UNO)))
                        {
                            fila_actual++;
                        }
                        intIdQueja = fila_actual;
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        dataTable.Columns.Add(AREA, typeof(int));
                        dataTable.Columns.Add(ID_CLIENTE, typeof(int));
                        dataTable.Columns.Add(SERVICIO, typeof(int));
                        dataTable.Columns.Add(TIPO_SOLI, typeof(int));
                        dataTable.Columns.Add(FECHA, typeof(string));
                        dataTable.Columns.Add(ID_QUEJA, typeof(int));
                        dataTable.Rows.Add(intArea, intIdCliente, intServicio, intTipoSoli, strFecha, intIdQueja);
                        documento.ImportDataTable(fila_actual, UNO, dataTable, false);
                        documento.Save();
                        dataTable = null;
                        documento = null;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}

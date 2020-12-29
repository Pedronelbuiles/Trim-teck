using System;
using Trim_Teck_PQRSF;
using SpreadsheetLight;

namespace tiposSolicitudes
{
    public class Sugerencias : Solicitudes
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
        private const string NOMBRE_HOJA = "Sugerencias";
        private const string DOCUMENT_NAME = "Datos.xlsx";
        private const string MAS_DIGITOS = " ó tener más de 7 dígitos";
        private const string AREA = "Area";
        private const string ID_CLIENTE = "idCliente";
        private const string SERVICIO = "servicio";
        private const string FECHA = "fecha";
        private const string TIPO_SOLI = "tipoSoli";
        private const string ID_SUGERENCIA = "idSugerencia";
        private const string SUGERENCIA = "Sugerencia";
        #endregion

        #region "Atributos"
        private int intIdSugerencia;
        private string strSugerencia;
        private string path;
        private int fila_actual;
        #endregion

        #region "Constructor"
        public Sugerencias()
        {
            intArea = CERO;
            strFecha = VACIO;
            strError = VACIO;
            intServicio = CERO;
            intTipoSoli = CERO;
            intIdCliente = CERO;
            intIdSugerencia = CERO;
            strSugerencia = VACIO;
            path = AppDomain.CurrentDomain.BaseDirectory + DOCUMENT_NAME;
        }
        #endregion

        #region "Propiedades"
        public int IdSugerencia { get => intIdSugerencia; set => intIdSugerencia = value; }
        public string Sugerencia { get => strSugerencia; set => strSugerencia = value; }
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
            if (intIdSugerencia <= MENOS_UNO || intIdSugerencia > LIMITE_ID)
            {
                const string ERROR_ID_PETICION = "El Código de la sugerencia no puede ser menor a 0";
                strError = ERROR_ID_PETICION + MAS_DIGITOS;
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
            if (strSugerencia == VACIO)
            {
                const string ERROR_FECHA = "La sugerencia no puede estar vacia";
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
                if (intIdSugerencia == documento.GetCellValueAsInt32(fila_actual, SEIS))
                {
                    break;
                }
                fila_actual++;
            }
            return true;
        }

        private bool puedeConsultar()
        {
            if (intIdSugerencia == CERO)
            {
                const string NO_PUEDE_CONSULTAR = "Esta Petición no se puede ingresar o consultar";
                strError = NO_PUEDE_CONSULTAR;
                return false;
            }
            return true;
        }
        #endregion

        #region "Métodos públicos"
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
                    if (intIdSugerencia == documento.GetCellValueAsInt32(fila_actual, SEIS))
                    {
                        intArea = documento.GetCellValueAsInt32(fila_actual, UNO);
                        intIdCliente = documento.GetCellValueAsInt32(fila_actual, DOS);
                        intServicio = documento.GetCellValueAsInt32(fila_actual, TRES);
                        intTipoSoli = documento.GetCellValueAsInt32(fila_actual, CUATRO);
                        strFecha = documento.GetCellValueAsString(fila_actual, CINCO);
                        intIdSugerencia = documento.GetCellValueAsInt32(fila_actual, SEIS);
                        strSugerencia = documento.GetCellValueAsString(fila_actual, SIETE);
                        flag = true;
                        break;
                    }
                    fila_actual++;
                }
                if (!flag)
                {
                    const string NO_ENCONTRADO = "La sugerencia que se está consultando no esta registrada";
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
                    dataTable.Columns.Add(ID_SUGERENCIA, typeof(int));
                    dataTable.Columns.Add(SUGERENCIA, typeof(string));
                    dataTable.Rows.Add(intArea, intIdCliente, intServicio, intTipoSoli, strFecha, intIdSugerencia, strSugerencia);
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
                intIdSugerencia = UNO;
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
                        intIdSugerencia = fila_actual;
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        dataTable.Columns.Add(AREA, typeof(int));
                        dataTable.Columns.Add(ID_CLIENTE, typeof(int));
                        dataTable.Columns.Add(SERVICIO, typeof(int));
                        dataTable.Columns.Add(TIPO_SOLI, typeof(int));
                        dataTable.Columns.Add(FECHA, typeof(string));
                        dataTable.Columns.Add(ID_SUGERENCIA, typeof(int));
                        dataTable.Columns.Add(SUGERENCIA, typeof(string));
                        dataTable.Rows.Add(intArea, intIdCliente, intServicio, intTipoSoli, strFecha, intIdSugerencia, strSugerencia);
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

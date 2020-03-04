using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleCodeGen
{
    /// <summary>
    /// Contenido de un template de tabla
    /// </summary>
    public class TemplateContent
    {
        public string NombreBaseDatos { get; set; }
        public string NombreTabla { get; set; }
        public string NombreClavePrimaria { get; set; }

        /// <summary>
        /// Sólo lectura. Quitamos el prefijo "db" de los nombres de Base de Datos
        /// </summary>
        public string NombreBaseDatosSinPrefijo
        {
            get
            {
                if (NombreBaseDatos != "")
                {
                    if (NombreBaseDatos.Substring(0, 2) == "db")
                    {
                        return NombreBaseDatos.Substring(2);
                    }
                    else return NombreBaseDatos;
                }
                else return "";
            }
        }

        /// <summary>
        /// Sólo lectura. Quitamos el prefijo XXX_ de los nombres de tabla
        /// </summary>
        public string NombreTablaSinPrefijo
        {
            get
            {
                if (NombreTabla != "")
                {
                    if (NombreTabla.Substring(3, 1) == "_")
                    {
                        return NombreTabla.Substring(4);
                    }
                    else return NombreTabla;
                }
                else return "";
            }
        }

        /// <summary>
        /// Sólo lectura. El nombre de la tabla en singular
        /// </summary>
        public string NombreClase
        {
            get { return ToSingular(this.NombreTabla); }
        }

        /// <summary>
        /// Colección de columnas de la tabla
        /// </summary>
        public List<ColumnItem> Columnas { get; set; }

        /// <summary>
        /// Sólo lectura. Los nombres de columnas unidos por comas:
        /// "Col1, Col2, Col3"
        /// </summary>
        public string ListaNombreColumnas
        {
            get
            {
                List<string> nombreCols = new List<string>();
                foreach (ColumnItem col in Columnas) { nombreCols.Add(col.NombreColumna); }
                return string.Join(", ", nombreCols);
            }
        }

        /// <summary>
        /// Sólo lectura. Los nombres de columnas unidos por comas y con el prefijo arroba:
        /// "@Col1, @Col2, @Col3"
        /// </summary>
        public string ListaNombreParametros
        {
            get
            {
                List<string> nombreCols = new List<string>();
                foreach (ColumnItem col in Columnas) { nombreCols.Add("@" + col.NombreColumna); }
                return string.Join(", ", nombreCols);
            }
        }

        /// <summary>
        /// Sólo lectura. Los nombres de columnas igualados a sus parámetros
        /// Col1 = @Col1, Col2 = @Col2, Col3 = @Col3
        /// </summary>
        public string ListaNombreColumnasParametros
        {
            get
            {
                List<string> nombreCols = new List<string>();
                foreach (ColumnItem col in Columnas) { nombreCols.Add(String.Format("{0} = @{0}", col.NombreColumna)); }
                return string.Join(", ", nombreCols);
            }
        }

        /// <summary>
        /// Sólo lectura. Número de columnas
        /// </summary>
        public int NumeroColumnas
        {
            get
            {
                if (Columnas != null) { return Columnas.Count; }
                else return 0;
            }
        }

        /// <summary>
        /// Sólo lectura. Fecha de generación de plantilla
        /// </summary>
        public DateTime FechaGeneracion { get; private set; }

        /// <summary>
        /// Ctor. Inicializa la fecha de generación
        /// </summary>
        public TemplateContent()
        {
            this.FechaGeneracion = DateTime.Today;
        }

        /// <summary>
        /// Función privada para convertir a singular el nombre de la tabla
        /// </summary>
        /// <param name="tableName">El nombre de la tabla</param>
        /// <returns>El nombre de la tabla en singular</returns>
        private static string ToSingular(string tableName)
        {
            //Quitamos el prefijo de la tabla
            tableName = tableName.Substring(tableName.IndexOf('_') + 1);
            //iremos construyendo un diccionario de excepciones para casos no cubiertos
            Dictionary<string, string> excepciones = new Dictionary<string, string> {
                { "Mensajes", "Mensaje" }, { "Cookies", "Cookie" },
                { "Clientes", "Cliente" }, { "Bases", "Base" }, { "Desgloses", "Desglose" }, //{ "", "" },
			};

            //Dividimos el nombre de tablas en nombres, basándonos en las mayúsculas
            string[] nombres = Regex.Replace(Regex.Replace(tableName, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2").Split(' ');

            /* ESTO ERA PARA SINGULARIZAR CADA NOMBRE, PERO EN LAS PLANTILLAS
             * ORIGINALES SOLO SE SINGULARIZA EL ULTIMO NOMBRE
            //Singularizamos cada nombre por separado
            for (int i = 0; i < nombres.Length; i++)
            {
                string actual = nombres[i];
                if (excepciones.ContainsKey(actual))
                {
                    nombres[i] = excepciones[actual];
                }
                else
                {
                    if (actual.Last() == 's')
                    {
                        actual = actual.Remove(actual.Length - 1);
                        if (actual.Last() == 'e')
                        {
                            actual = actual.Remove(actual.Length - 1);
                        }
                    }
                    nombres[i] = actual;
                }
            }
            */

            // Sólo singularizamos el último de los nombres
            // ProyectosActuales --> ProyectosActual
            string actual = nombres[nombres.Length - 1];
            if (excepciones.ContainsKey(actual))
            {
                nombres[nombres.Length - 1] = excepciones[actual];
            }
            else
            {
                if (actual.Last() == 's')
                {
                    actual = actual.Remove(actual.Length - 1);
                    if (actual.Last() == 'e')
                    {
                        actual = actual.Remove(actual.Length - 1);
                    }
                }
                nombres[nombres.Length - 1] = actual;
            }


            // Y lo devolvemos en cadena concatenada sin espacios
            return string.Join("", nombres);
        }
    }

    /// <summary>
    /// Cada una de las columnas de la tabla, 
    /// con nombre y tipo
    /// </summary>
    public class ColumnItem
    {
        public string NombreColumna { get; set; }
        public string TipoSql { get; set; }
        public string TipoClr
        {
            get { return GetClrType(TipoSql); }
        }

        /// <summary>
        /// Sólo lectura (private set). Devuelve si la columna es clave primaria
        /// </summary>
        public bool EsClavePrimaria
        {
            get { return false; }
            private set { }
        }

        public bool NotEsClavePrimaria
        {
            get { return !this.EsClavePrimaria; }
        }

        /// <summary>
        /// Ctor obligatorio con nombre de columna y tipo sql de columna
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        public ColumnItem(string nombre, string tipo, bool esClave)
        {
            this.NombreColumna = nombre;
            this.TipoSql = tipo;
            this.EsClavePrimaria = esClave;
        }

        /// <summary>
        /// Devuelve el equivalente Clr a un tipo Sql
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        private static string GetClrType(string sqlType)
        {
            switch (sqlType.ToUpper())
            {
                case "BIGINT":
                    return "Int64";

                case "BINARY":
                case "IMAGE":
                case "TIMESTAMP":
                case "VARBINARY":
                    return "Byte[]";

                case "BIT":
                    return "Boolean";

                case "CHAR":
                case "NCHAR":
                case "NTEXT":
                case "NVARCHAR":
                case "TEXT":
                case "VARCHAR":
                case "XML":
                    return "String";

                case "DATETIME":
                case "SMALLDATETIME":
                case "DATE":
                case "TIME":
                case "DATETIME2":
                    return "DateTime";

                case "DECIMAL":
                case "MONEY":
                case "SMALLMONEY":
                    return "Decimal";

                case "FLOAT":
                    return "Double";

                case "INT":
                    return "Int32";

                case "REAL":
                    return "Single";

                case "UNIQUEIDENTIFIER":
                    return "Guid";

                case "SMALLINT":
                    return "Short";

                case "TINYINT":
                    return "Byte";

                case "VARIANT":
                case "UDT":
                    return "Object";

                case "STRUCTURED":
                    return "DataTable";

                case "DATETIMEOFFSET":
                    return "DateTimeOffset";

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }
}

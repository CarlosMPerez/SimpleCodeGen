using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleCodeGen.CodeGenerator
{
    /// <summary>
    /// Content of a template
    /// </summary>
    public class TemplateContent
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyName { get; set; }

        public string ClassName { get; set; }


        /// <summary>
        /// Column collection of the table
        /// </summary>
        public List<ColumnItem> Columns { get; set; }

        /// <summary>
        /// Template generation date
        /// </summary>
        public DateTime GenDate { get; private set; }

        /// <summary>
        /// Ctor. 
        /// Initialises generation date
        /// </summary>
        public TemplateContent()
        {
            this.GenDate = DateTime.Today;
        }

        /// <summary>
        /// Converts to single and capitalizes the name of the table
        /// </summary>
        /// <param name="tableName">Table name (proposals)</param>
        /// <returns>Singular and capitalized table name (Proposal)</returns>
        private static string ToSingular(string tableName)
        {
            // Remove the table prefix
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
        private bool _isPrimaryKey;

        public string ColumnName { get; set; }

        public string FieldName {  get { return "FieldName"; } }
        public string SQLType { get; set; }
        public string CLRType
        {
            get { return GetClrType(SQLType); }
        }

        /// <summary>
        /// Sólo lectura (private set). Devuelve si la columna es clave primaria
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
        }

        public bool IsNotPrimaryKey
        {
            get { return !_isPrimaryKey; }
        }

        /// <summary>
        /// Ctor obligatorio con nombre de columna y tipo sql de columna
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        public ColumnItem(string nombre, string tipo, bool esClave)
        {
            this.ColumnName = nombre;
            this.SQLType = tipo;
            _isPrimaryKey = esClave;
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

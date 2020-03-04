using NLightTemplate;
using System.Collections.Generic;
using System.IO;

namespace SimpleCodeGen
{
    public static class TemplateEngine
    {
        public static void Generate(string database, string table, string rutaDestino, string rutaTemplate)
        {
            StreamReader sr = new StreamReader(rutaTemplate);
            string template = sr.ReadToEnd();
            sr.Close();
            TemplateContent content = GenerateContent(database, table);
            string renderedText = StringTemplate.Render(template, content);
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(rutaDestino))
            {
                outputFile.Write(renderedText);
            }
        }

        private static TemplateContent GenerateContent(string database, string table)
        {
            TemplateContent content = new TemplateContent();
            content.NombreBaseDatos = database;
            content.NombreTabla = table;
            content.NombreClavePrimaria = DBEngine.GetPrimaryKeyColumn(database, table);
            List<ColumnItem> cols = GetTableFields(database, table, content.NombreClavePrimaria);
            content.Columnas = cols;
            return content;
        }

        private static List<ColumnItem> GetTableFields(string database, string table, string nombreClavePrimaria)
        {
            List<ColumnItem> ret = new List<ColumnItem>();
            Dictionary<string, string> lista = DBEngine.GetColumnsWithCSharpTypes(database, table);
            foreach (var elemento in lista)
            {
                ret.Add(new ColumnItem(elemento.Key, elemento.Value, elemento.Key == nombreClavePrimaria));
            }

            return ret;
        }
    }
}

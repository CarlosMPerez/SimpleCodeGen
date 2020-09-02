using NLightTemplate;
using System.Collections.Generic;
using System.IO;

namespace SimpleCodeGen.CodeGenerator
{
    public class TemplateEngine
    {

        private DBEngine _engine;

        public TemplateEngine(DBEngine engine)
        {
            _engine = engine;
            //Configuración común de StringTemplate
            StringTemplate.Configure
                .OpenToken("<%")
                .CloseToken("%>")
                .IfToken("if")
                .ForeachToken("fe");
        }

        public void Generate(string database, string table, string className, string rutaDestino, string rutaTemplate)
        {
            StreamReader sr = new StreamReader(rutaTemplate);
            string template = sr.ReadToEnd();
            sr.Close();
            TemplateContent content = GenerateContent(database, table , className);
            string renderedText = StringTemplate.Render(template, content);
            // Write the string array to a new file
            using (StreamWriter outputFile = new StreamWriter(rutaDestino))
            {
                outputFile.Write(renderedText);
            }
        }

        private TemplateContent GenerateContent(string database, string table, string className)
        {
            TemplateContent content = new TemplateContent();
            content.DatabaseName = database;
            content.TableName = table;
            content.ClassName = className;
            content.PrimaryKeyName= _engine.GetPrimaryKeyColumn(database, table);
            List<ColumnItem> cols = GetTableFields(database, table, content.PrimaryKeyName);
            content.Columns = cols;
            return content;
        }

        private List<ColumnItem> GetTableFields(string database, string table, string nombreClavePrimaria)
        {
            List<ColumnItem> ret = new List<ColumnItem>();
            Dictionary<string, string> lista = _engine.GetColumnsWithDataTypes(database, table);
            foreach (var elemento in lista)
            {
                ret.Add(new ColumnItem(elemento.Key, elemento.Value, elemento.Key == nombreClavePrimaria));
            }

            return ret;
        }
    }
}

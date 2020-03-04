using NLightTemplate;
using System;
using System.Windows.Forms;

namespace SimpleCodeGen
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Configuración común de StringTemplate
            StringTemplate.Configure
                .OpenToken("<%")
                .CloseToken("%>")
                .IfToken("if")
                .ForeachToken("fe");
            Application.Run(new Main());
        }
    }
}
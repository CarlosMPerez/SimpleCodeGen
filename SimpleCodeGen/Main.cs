using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace SimpleCodeGen
{
    public partial class Main : Form
    {
        private DBEngine dbEngine;
        private TemplateEngine templateEngine;
        private List<string> connStrings;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadConnStrings();
            LoadTemplates();
            LoadDefaultGenPath();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadConnStrings()
        {
            connStrings = new List<string>();
            // Read all the keys from the config file
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var conns = configFile.AppSettings.Settings.AllKeys
                            .Where(key => key.StartsWith("conn"))
                            .Select(key => ConfigurationManager.AppSettings[key])
                            .ToArray();
            foreach (string conn in conns)
            {
                cmbConnString.Items.Add(conn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        private void UpdateConnStrings(string connString)
        {
            // Read all the keys from the config file
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string[] conns = configFile.AppSettings.Settings.AllKeys
                            .Where(key => key.StartsWith("conn"))
                            .Select(key => ConfigurationManager.AppSettings[key])
                            .ToArray();

            // Check if connString is already in collection
            if (conns.FirstOrDefault(x => x == cmbConnString.Text) == null)
            {
                string key = String.Format("conn{0}", conns.Count() + 1);
                configFile.AppSettings.Settings.Add(key, cmbConnString.Text);
                cmbConnString.Items.Add(cmbConnString.Text);
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
        }

        /// <summary>
        /// Cargamos el treeview con los 
        /// </summary>
        private void LoadTreeView()
        {
            foreach (string dbName in dbEngine.GetDatabases())
            {
                TreeNode parentNode = new TreeNode(dbName, 0, 0);
                foreach (string tableName in dbEngine.GetTables(dbName))
                {
                    TreeNode childNode = new TreeNode(tableName, 1, 1);
                    parentNode.Nodes.Add(childNode);
                }
                tvDBView.Nodes.Add(parentNode);
            }
        }

        /// <summary>
        /// Cargamos las plantillas, que DEBEN tener extensión GST y ubicarse en bin/templates
        /// </summary>
        private void LoadTemplates()
        {
            foreach (string fichero in Directory.EnumerateFiles("templates", "*.template.scg"))
            {
                lstTemplates.Items.Add(Path.GetFileNameWithoutExtension(fichero));
            }
        }

        private void LoadDefaultGenPath()
        {
            txtOutputPath.Text = AppContext.BaseDirectory.ToString();
        }

        /// <summary>
        /// Cargamos un path selector para seleccionar el path donde almacenar el código generado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            dlgOutputPath.SelectedPath = txtOutputPath.Text;
            if (dlgOutputPath.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = dlgOutputPath.SelectedPath;
            }
        }

        /// <summary>
        /// Go Power Rangers!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                Application.UseWaitCursor = true;
                List<Tuple<string, string>> tablasAProcesar = new List<Tuple<string, string>>();
                // Bloqueamos los controles mientras se procesa la cosa
                tvDBView.Enabled = false;
                lstTemplates.Enabled = false;
                btnGenerateCode.Enabled = false;

                // Vamos a ver qué tablas queremos procesar
                foreach (TreeNode parent in tvDBView.Nodes)
                {
                    if (parent.Nodes.Count > 0)
                    {
                        foreach (TreeNode son in parent.Nodes)
                        {
                            if (son.Checked)
                            {
                                tablasAProcesar.Add(new Tuple<string, string>(parent.Text, son.Text));
                            }
                        }
                    }
                }

                foreach (Tuple<string, string> tupla in tablasAProcesar)
                {
                    foreach (ListViewItem item in lstTemplates.Items)
                    {
                        if (item.Checked)
                        {
                            //Limpiamos el nombre de la tabla
                            string tName = tupla.Item2;
                            if (tName.Substring(3, 1) == "_") tName = tName.Substring(4);

                            txtResults.AppendText(String.Format("Creando {0}{1}.generado.vb ... ", tName, item.Text));

                            templateEngine.Generate(tupla.Item1, tupla.Item2,
                                Path.Combine(txtOutputPath.Text, String.Format("{0}{1}.generado.vb", tName, item.Text)),
                                String.Format(@"templates\{0}.scg", item.Text));

                            txtResults.AppendText("OK" + Environment.NewLine);
                        }
                        Application.DoEvents();
                    }
                }
                txtResults.AppendText("Proceso Terminado." + Environment.NewLine);
                Application.UseWaitCursor = false;
            }



            // Desbloqueamos los controles al terminar
            tvDBView.Enabled = true;
            lstTemplates.Enabled = true;
            btnGenerateCode.Enabled = true;
        }

        /// <summary>
        /// Validamos los valores introducidos en el form
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            bool someNodeIsChecked = false;
            foreach (TreeNode parent in tvDBView.Nodes)
            {
                if (parent.Checked)
                {
                    someNodeIsChecked = true;
                    break;
                }
                else
                {
                    foreach (TreeNode son in parent.Nodes)
                    {
                        if (son.Checked)
                        {
                            someNodeIsChecked = true;
                            break;
                        }
                    }
                    if (someNodeIsChecked) break;
                }
            }

            bool someTemplateIsChecked = false;
            foreach (ListViewItem item in lstTemplates.Items)
            {
                if (item.Checked)
                {
                    someTemplateIsChecked = true;
                    break;
                }
            }

            bool pathIsSet = false;
            if (txtOutputPath.Text != "")
            {
                if (Directory.Exists(txtOutputPath.Text))
                {
                    pathIsSet = true;
                }
            }


            if (!someNodeIsChecked)
            {
                MessageBox.Show("Debe seleccionar alguna tabla de la base de datos",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!someTemplateIsChecked)
            {
                MessageBox.Show("Debe seleccionar alguna template",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!pathIsSet)
            {
                MessageBox.Show("Debe seleccionar una ruta de destino correcta",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // si todo va ok
            return true;
        }

        /// <summary>
        /// Comprobamos si se ha checado u nodo padre y en ese caso checamos todos los hijos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDBView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode parent = e.Node;
            if (parent.Nodes.Count > 0) //es nodo padre
            {
                if (parent.Checked) parent.Expand();
                else parent.Collapse();
                foreach (TreeNode node in parent.Nodes)
                {
                    node.Checked = parent.Checked;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveConnString_Click(object sender, EventArgs e)
        {
            UpdateConnStrings(cmbConnString.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbConnString.Text != "")
            {
                dbEngine = new DBEngine(cmbConnString.Text);
                templateEngine = new TemplateEngine(dbEngine);
                LoadTreeView();
            }
        }
    }
}

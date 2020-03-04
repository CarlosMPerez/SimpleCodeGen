using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimpleCodeGen
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            LoadTemplates();
            LoadDefaultGenPath();
        }

        /// <summary>
        /// Cargamos el treeview con los 
        /// </summary>
        private void LoadTreeView()
        {
            foreach (string dbName in DBEngine.GetDatabases())
            {
                TreeNode parentNode = new TreeNode(dbName, 0, 0);
                foreach (string tableName in DBEngine.GetTables(dbName))
                {
                    TreeNode childNode = new TreeNode(tableName, 1, 1);
                    parentNode.Nodes.Add(childNode);
                }
                tvTablas.Nodes.Add(parentNode);
            }
        }

        /// <summary>
        /// Cargamos las plantillas, que DEBEN tener extensión GST y ubicarse en bin/templates
        /// </summary>
        private void LoadTemplates()
        {
            foreach (string fichero in Directory.EnumerateFiles("templates", "*.template.scg"))
            {
                lstPlantillas.Items.Add(Path.GetFileNameWithoutExtension(fichero));
            }
        }

        private void LoadDefaultGenPath()
        {
            txtRutaCodigoGen.Text = AppContext.BaseDirectory.ToString();
        }

        /// <summary>
        /// Cargamos un path selector para seleccionar el path donde almacenar el código generado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRutaCodigoGen_Click(object sender, EventArgs e)
        {
            dlgRutaCodigo.SelectedPath = txtRutaCodigoGen.Text;
            if (dlgRutaCodigo.ShowDialog() == DialogResult.OK)
            {
                txtRutaCodigoGen.Text = dlgRutaCodigo.SelectedPath;
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
                tvTablas.Enabled = false;
                lstPlantillas.Enabled = false;
                btnGenerateCode.Enabled = false;

                // Vamos a ver qué tablas queremos procesar
                foreach (TreeNode parent in tvTablas.Nodes)
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
                    foreach (ListViewItem item in lstPlantillas.Items)
                    {
                        if (item.Checked)
                        {
                            //Limpiamos el nombre de la tabla
                            string tName = tupla.Item2;
                            if (tName.Substring(3, 1) == "_") tName = tName.Substring(4);

                            txtResultados.AppendText(String.Format("Creando {0}{1}.generado.vb ... ", tName, item.Text));

                            TemplateEngine.Generate(tupla.Item1, tupla.Item2,
                                Path.Combine(txtRutaCodigoGen.Text, String.Format("{0}{1}.generado.vb", tName, item.Text)),
                                String.Format(@"templates\{0}.scg", item.Text));

                            txtResultados.AppendText("OK" + Environment.NewLine);
                        }
                        Application.DoEvents();
                    }
                }
                txtResultados.AppendText("Proceso Terminado." + Environment.NewLine);
                Application.UseWaitCursor = false;
            }



            // Desbloqueamos los controles al terminar
            tvTablas.Enabled = true;
            lstPlantillas.Enabled = true;
            btnGenerateCode.Enabled = true;
        }

        /// <summary>
        /// Validamos los valores introducidos en el form
        /// </summary>
        /// <returns></returns>
        private bool ValidateControls()
        {
            bool someNodeIsChecked = false;
            foreach (TreeNode parent in tvTablas.Nodes)
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
            foreach (ListViewItem item in lstPlantillas.Items)
            {
                if (item.Checked)
                {
                    someTemplateIsChecked = true;
                    break;
                }
            }

            bool pathIsSet = false;
            if (txtRutaCodigoGen.Text != "")
            {
                if (Directory.Exists(txtRutaCodigoGen.Text))
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
        private void tvTablas_AfterCheck(object sender, TreeViewEventArgs e)
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
    }
}

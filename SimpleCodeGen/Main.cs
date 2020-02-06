using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace GolfCodeGen
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
        }

        /// <summary>
        /// Cargamos el treeview con los 
        /// </summary>
        private void LoadTreeView()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnStr"]);
            conn.Open();
            DataTable dtBases = new DataTable();
            string sql = "SELECT name FROM master.sys.databases WHERE name LIKE ('db%') ORDER BY name";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comm;
            da.Fill(dtBases);

            foreach (DataRow row in dtBases.Rows)
            {
                string dbName = row["name"].ToString();
                TreeNode parentNode = new TreeNode(dbName, 0, 0);

                // Añadimos los hijos
                sql = String.Format("SELECT table_name FROM {0}.INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT IN ('sysdiagrams', 'dtproperties') ORDER BY TABLE_NAME", dbName);
                DataTable dtTables = new DataTable();
                comm.CommandText = sql;
                da.SelectCommand = comm;
                da.Fill(dtTables);
                foreach (DataRow subRow in dtTables.Rows)
                {
                    string tableName = subRow["table_name"].ToString();
                    TreeNode childNode = new TreeNode(tableName, 1, 1);
                    parentNode.Nodes.Add(childNode);
                }
                tvTablas.Nodes.Add(parentNode);
            }

            conn.Close();
        }

        /// <summary>
        /// Cargamos las plantillas, que DEBEN tener extensión GST y ubicarse en bin/templates
        /// </summary>
        private void LoadTemplates()
        {
            foreach(string fichero in Directory.EnumerateFiles("templates", "*.gst"))
            {
                lstPlantillas.Items.Add(Path.GetFileName(fichero));
            }
        }

        /// <summary>
        /// Cargamos un path selector para seleccionar el path donde almacenar el código generado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRutaCodigoGen_Click(object sender, EventArgs e)
        {
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

                foreach(Tuple<string, string> tupla in tablasAProcesar)
                {
                    foreach(ListViewItem item in lstPlantillas.Items)
                    {
                        if(item.Checked)
                        {
                            int posUnder = item.Text.IndexOf('_');
                            int posDot = item.Text.IndexOf('.');
                            string type = item.Text.Substring(0, posUnder);
                            string language = item.Text.Substring(posUnder + 1, (posDot - posUnder)-1);

                            strLabel.Text = String.Format("Generando el fichero {0}{1}{2}.generado.{3}", 
                                                            txtRutaCodigoGen.Text, 
                                                            tupla.Item2, type, language);
                            MessageBox.Show("A");
                            //TemplateEngine.Generate(tupla.Item1, tupla.Item2, type, language, txtRutaCodigoGen.Text);
                        }
                    }
                }

            }



            // Desbloqueamos los controles al terminar
            tvTablas.Enabled = true;
            lstPlantillas.Enabled = true;
            btnGenerateCode.Enabled = true;
            strLabel.Text = "";
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
                        if(son.Checked)
                        {
                            someNodeIsChecked = true;
                            break;
                        }
                    }
                    if (someNodeIsChecked) break;
                }
            }

            bool someTemplateIsChecked = false;
            foreach(ListViewItem item in lstPlantillas.Items)
            {
                if(item.Checked)
                {
                    someTemplateIsChecked = true;
                    break;
                }
            }

            bool pathIsSet = false;
            if(txtRutaCodigoGen.Text != "")
            {
                if(Directory.Exists(txtRutaCodigoGen.Text))
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
                foreach(TreeNode node in parent.Nodes)
                {
                    node.Checked = parent.Checked;
                }
            }
        }
    }
}

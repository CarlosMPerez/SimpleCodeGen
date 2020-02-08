namespace SimpleCodeGen
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tvTablas = new System.Windows.Forms.TreeView();
            this.lblTablas = new System.Windows.Forms.Label();
            this.lblPlantilla = new System.Windows.Forms.Label();
            this.lstPlantillas = new System.Windows.Forms.ListView();
            this.lblDestino = new System.Windows.Forms.Label();
            this.txtRutaCodigoGen = new System.Windows.Forms.TextBox();
            this.btnRutaCodigoGen = new System.Windows.Forms.Button();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.dlgRutaCodigo = new System.Windows.Forms.FolderBrowserDialog();
            this.strStatus = new System.Windows.Forms.StatusStrip();
            this.strLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgLista = new System.Windows.Forms.ImageList(this.components);
            this.strStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvTablas
            // 
            this.tvTablas.CheckBoxes = true;
            this.tvTablas.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvTablas.ImageIndex = 0;
            this.tvTablas.ImageList = this.imgLista;
            this.tvTablas.Location = new System.Drawing.Point(14, 42);
            this.tvTablas.Name = "tvTablas";
            this.tvTablas.SelectedImageIndex = 0;
            this.tvTablas.Size = new System.Drawing.Size(391, 498);
            this.tvTablas.TabIndex = 0;
            this.tvTablas.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTablas_AfterCheck);
            // 
            // lblTablas
            // 
            this.lblTablas.AutoSize = true;
            this.lblTablas.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTablas.Location = new System.Drawing.Point(11, 9);
            this.lblTablas.Name = "lblTablas";
            this.lblTablas.Size = new System.Drawing.Size(394, 17);
            this.lblTablas.TabIndex = 1;
            this.lblTablas.Text = "1.- Selecciona la(s) tabla(s) para generar código:";
            // 
            // lblPlantilla
            // 
            this.lblPlantilla.AutoSize = true;
            this.lblPlantilla.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlantilla.Location = new System.Drawing.Point(428, 9);
            this.lblPlantilla.Name = "lblPlantilla";
            this.lblPlantilla.Size = new System.Drawing.Size(276, 17);
            this.lblPlantilla.TabIndex = 2;
            this.lblPlantilla.Text = "2.- Selecciona la plantilla a utilizar:";
            // 
            // lstPlantillas
            // 
            this.lstPlantillas.CheckBoxes = true;
            this.lstPlantillas.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlantillas.FullRowSelect = true;
            this.lstPlantillas.GridLines = true;
            this.lstPlantillas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstPlantillas.HideSelection = false;
            this.lstPlantillas.Location = new System.Drawing.Point(424, 45);
            this.lstPlantillas.Name = "lstPlantillas";
            this.lstPlantillas.Size = new System.Drawing.Size(421, 189);
            this.lstPlantillas.TabIndex = 3;
            this.lstPlantillas.UseCompatibleStateImageBehavior = false;
            this.lstPlantillas.View = System.Windows.Forms.View.List;
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestino.Location = new System.Drawing.Point(428, 253);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(341, 17);
            this.lblDestino.TabIndex = 4;
            this.lblDestino.Text = "3.- Selecciona la ruta del código generado:";
            // 
            // txtRutaCodigoGen
            // 
            this.txtRutaCodigoGen.Location = new System.Drawing.Point(424, 289);
            this.txtRutaCodigoGen.Name = "txtRutaCodigoGen";
            this.txtRutaCodigoGen.Size = new System.Drawing.Size(378, 20);
            this.txtRutaCodigoGen.TabIndex = 5;
            // 
            // btnRutaCodigoGen
            // 
            this.btnRutaCodigoGen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRutaCodigoGen.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRutaCodigoGen.Location = new System.Drawing.Point(808, 287);
            this.btnRutaCodigoGen.Name = "btnRutaCodigoGen";
            this.btnRutaCodigoGen.Size = new System.Drawing.Size(37, 23);
            this.btnRutaCodigoGen.TabIndex = 6;
            this.btnRutaCodigoGen.Text = "...";
            this.btnRutaCodigoGen.UseVisualStyleBackColor = true;
            this.btnRutaCodigoGen.Click += new System.EventHandler(this.btnRutaCodigoGen_Click);
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerateCode.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateCode.Location = new System.Drawing.Point(529, 335);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(175, 58);
            this.btnGenerateCode.TabIndex = 7;
            this.btnGenerateCode.Text = "Generar Código";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // dlgRutaCodigo
            // 
            this.dlgRutaCodigo.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // strStatus
            // 
            this.strStatus.Font = new System.Drawing.Font("Verdana", 10F);
            this.strStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strLabel});
            this.strStatus.Location = new System.Drawing.Point(0, 550);
            this.strStatus.Name = "strStatus";
            this.strStatus.Size = new System.Drawing.Size(857, 22);
            this.strStatus.TabIndex = 8;
            // 
            // strLabel
            // 
            this.strLabel.Name = "strLabel";
            this.strLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // imgLista
            // 
            this.imgLista.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLista.ImageStream")));
            this.imgLista.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLista.Images.SetKeyName(0, "bdd.png");
            this.imgLista.Images.SetKeyName(1, "table.png");
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 572);
            this.Controls.Add(this.strStatus);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.btnRutaCodigoGen);
            this.Controls.Add(this.txtRutaCodigoGen);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.lstPlantillas);
            this.Controls.Add(this.lblPlantilla);
            this.Controls.Add(this.lblTablas);
            this.Controls.Add(this.tvTablas);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleCodeGen";
            this.Load += new System.EventHandler(this.Main_Load);
            this.strStatus.ResumeLayout(false);
            this.strStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvTablas;
        private System.Windows.Forms.Label lblTablas;
        private System.Windows.Forms.Label lblPlantilla;
        private System.Windows.Forms.ListView lstPlantillas;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.TextBox txtRutaCodigoGen;
        private System.Windows.Forms.Button btnRutaCodigoGen;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.FolderBrowserDialog dlgRutaCodigo;
        private System.Windows.Forms.StatusStrip strStatus;
        private System.Windows.Forms.ToolStripStatusLabel strLabel;
        private System.Windows.Forms.ImageList imgLista;
    }
}


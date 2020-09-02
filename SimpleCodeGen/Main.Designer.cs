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
            this.tvDBView = new System.Windows.Forms.TreeView();
            this.imgLista = new System.Windows.Forms.ImageList(this.components);
            this.lblDBView = new System.Windows.Forms.Label();
            this.lblTemplates = new System.Windows.Forms.Label();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.dlgOutputPath = new System.Windows.Forms.FolderBrowserDialog();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.cmbConnString = new System.Windows.Forms.ComboBox();
            this.btnSaveConnString = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.Tip = new System.Windows.Forms.ToolTip(this.components);
            this.tvTemplates = new System.Windows.Forms.TreeView();
            this.chkRemovePrefix = new System.Windows.Forms.CheckBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tvDBView
            // 
            this.tvDBView.CheckBoxes = true;
            this.tvDBView.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDBView.ImageIndex = 0;
            this.tvDBView.ImageList = this.imgLista;
            this.tvDBView.Location = new System.Drawing.Point(14, 128);
            this.tvDBView.Name = "tvDBView";
            this.tvDBView.SelectedImageIndex = 0;
            this.tvDBView.Size = new System.Drawing.Size(477, 431);
            this.tvDBView.TabIndex = 0;
            this.tvDBView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterCheck);
            // 
            // imgLista
            // 
            this.imgLista.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLista.ImageStream")));
            this.imgLista.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLista.Images.SetKeyName(0, "database-512.png");
            this.imgLista.Images.SetKeyName(1, "img_426719.png");
            this.imgLista.Images.SetKeyName(2, "iconfinder_save_2639912.png");
            this.imgLista.Images.SetKeyName(3, "iconfinder_database_run_103471.png");
            this.imgLista.Images.SetKeyName(4, "iconfinder_icon-folder_211608.png");
            this.imgLista.Images.SetKeyName(5, "iconfinder_file_227587.png");
            // 
            // lblDBView
            // 
            this.lblDBView.AutoSize = true;
            this.lblDBView.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBView.Location = new System.Drawing.Point(12, 105);
            this.lblDBView.Name = "lblDBView";
            this.lblDBView.Size = new System.Drawing.Size(424, 20);
            this.lblDBView.TabIndex = 1;
            this.lblDBView.Text = "2.- Pick the table(s) to generate code from:";
            // 
            // lblTemplates
            // 
            this.lblTemplates.AutoSize = true;
            this.lblTemplates.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplates.Location = new System.Drawing.Point(506, 105);
            this.lblTemplates.Name = "lblTemplates";
            this.lblTemplates.Size = new System.Drawing.Size(308, 20);
            this.lblTemplates.TabIndex = 2;
            this.lblTemplates.Text = "3.- Pick the template(s) to use:";
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputPath.Location = new System.Drawing.Point(516, 352);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(426, 20);
            this.lblOutputPath.TabIndex = 4;
            this.lblOutputPath.Text = "4.- Choose the path for the generated code:";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(520, 386);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(428, 24);
            this.txtOutputPath.TabIndex = 5;
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOutputPath.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputPath.Location = new System.Drawing.Point(954, 384);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(37, 26);
            this.btnOutputPath.TabIndex = 6;
            this.btnOutputPath.Text = "...";
            this.Tip.SetToolTip(this.btnOutputPath, "Choose Path");
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateCode.Location = new System.Drawing.Point(600, 416);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(273, 38);
            this.btnGenerateCode.TabIndex = 7;
            this.btnGenerateCode.Text = "Generate Code";
            this.Tip.SetToolTip(this.btnGenerateCode, "Generates code");
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // dlgOutputPath
            // 
            this.dlgOutputPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(520, 468);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(471, 161);
            this.txtResults.TabIndex = 9;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionString.Location = new System.Drawing.Point(12, 19);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(408, 20);
            this.lblConnectionString.TabIndex = 10;
            this.lblConnectionString.Text = "1.- Enter or choose the connection string:";
            // 
            // cmbConnString
            // 
            this.cmbConnString.FormattingEnabled = true;
            this.cmbConnString.Location = new System.Drawing.Point(16, 47);
            this.cmbConnString.Name = "cmbConnString";
            this.cmbConnString.Size = new System.Drawing.Size(836, 24);
            this.cmbConnString.TabIndex = 11;
            // 
            // btnSaveConnString
            // 
            this.btnSaveConnString.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnString.ImageIndex = 2;
            this.btnSaveConnString.ImageList = this.imgLista;
            this.btnSaveConnString.Location = new System.Drawing.Point(865, 31);
            this.btnSaveConnString.Name = "btnSaveConnString";
            this.btnSaveConnString.Size = new System.Drawing.Size(55, 50);
            this.btnSaveConnString.TabIndex = 12;
            this.Tip.SetToolTip(this.btnSaveConnString, "Save Connection String");
            this.btnSaveConnString.UseVisualStyleBackColor = true;
            this.btnSaveConnString.Click += new System.EventHandler(this.btnSaveConnString_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ImageIndex = 3;
            this.btnConnect.ImageList = this.imgLista;
            this.btnConnect.Location = new System.Drawing.Point(926, 31);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(55, 50);
            this.btnConnect.TabIndex = 13;
            this.Tip.SetToolTip(this.btnConnect, "Open connection");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tvTemplates
            // 
            this.tvTemplates.CheckBoxes = true;
            this.tvTemplates.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvTemplates.ImageIndex = 0;
            this.tvTemplates.ImageList = this.imgLista;
            this.tvTemplates.Location = new System.Drawing.Point(524, 128);
            this.tvTemplates.Name = "tvTemplates";
            this.tvTemplates.SelectedImageIndex = 0;
            this.tvTemplates.Size = new System.Drawing.Size(467, 221);
            this.tvTemplates.TabIndex = 14;
            this.tvTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewAfterCheck);
            // 
            // chkRemovePrefix
            // 
            this.chkRemovePrefix.AutoSize = true;
            this.chkRemovePrefix.Location = new System.Drawing.Point(13, 566);
            this.chkRemovePrefix.Name = "chkRemovePrefix";
            this.chkRemovePrefix.Size = new System.Drawing.Size(221, 21);
            this.chkRemovePrefix.TabIndex = 15;
            this.chkRemovePrefix.Text = "Remove table name prefix?";
            this.chkRemovePrefix.UseVisualStyleBackColor = true;
            this.chkRemovePrefix.CheckedChanged += new System.EventHandler(this.chkRemovePrefix_CheckedChanged);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Enabled = false;
            this.txtPrefix.Location = new System.Drawing.Point(240, 563);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(251, 24);
            this.txtPrefix.TabIndex = 16;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 641);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.chkRemovePrefix);
            this.Controls.Add(this.tvTemplates);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSaveConnString);
            this.Controls.Add(this.cmbConnString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.lblTemplates);
            this.Controls.Add(this.lblDBView);
            this.Controls.Add(this.tvDBView);
            this.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimpleCodeGen";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvDBView;
        private System.Windows.Forms.Label lblDBView;
        private System.Windows.Forms.Label lblTemplates;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.FolderBrowserDialog dlgOutputPath;
        private System.Windows.Forms.ImageList imgLista;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.ComboBox cmbConnString;
        private System.Windows.Forms.Button btnSaveConnString;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolTip Tip;
        private System.Windows.Forms.TreeView tvTemplates;
        private System.Windows.Forms.CheckBox chkRemovePrefix;
        private System.Windows.Forms.TextBox txtPrefix;
    }
}




namespace Et075.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.dataSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.printersComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.run = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox = new Et075.View.RichTextBoxEx();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.loadButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSplitContainer)).BeginInit();
            this.dataSplitContainer.Panel1.SuspendLayout();
            this.dataSplitContainer.Panel2.SuspendLayout();
            this.dataSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 624);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(861, 22);
            this.statusStrip.TabIndex = 4;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.dataSplitContainer);
            this.mainSplitContainer.Panel1MinSize = 280;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.richTextBox);
            this.mainSplitContainer.Panel2MinSize = 100;
            this.mainSplitContainer.Size = new System.Drawing.Size(861, 624);
            this.mainSplitContainer.SplitterDistance = 280;
            this.mainSplitContainer.TabIndex = 5;
            // 
            // dataSplitContainer
            // 
            this.dataSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSplitContainer.IsSplitterFixed = true;
            this.dataSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.dataSplitContainer.Name = "dataSplitContainer";
            this.dataSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // dataSplitContainer.Panel1
            // 
            this.dataSplitContainer.Panel1.Controls.Add(this.loadButton);
            this.dataSplitContainer.Panel1.Controls.Add(this.calculateButton);
            this.dataSplitContainer.Panel1.Controls.Add(this.printButton);
            this.dataSplitContainer.Panel1.Controls.Add(this.clearButton);
            this.dataSplitContainer.Panel1.Controls.Add(this.label1);
            this.dataSplitContainer.Panel1.Controls.Add(this.printersComboBox);
            this.dataSplitContainer.Panel1MinSize = 120;
            // 
            // dataSplitContainer.Panel2
            // 
            this.dataSplitContainer.Panel2.Controls.Add(this.dataGridView);
            this.dataSplitContainer.Panel2MinSize = 100;
            this.dataSplitContainer.Size = new System.Drawing.Size(280, 624);
            this.dataSplitContainer.SplitterDistance = 120;
            this.dataSplitContainer.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Поточний принтер:";
            // 
            // printersComboBox
            // 
            this.printersComboBox.FormattingEnabled = true;
            this.printersComboBox.Location = new System.Drawing.Point(58, 89);
            this.printersComboBox.Name = "printersComboBox";
            this.printersComboBox.Size = new System.Drawing.Size(214, 21);
            this.printersComboBox.TabIndex = 4;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.run,
            this.name});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 20;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.Size = new System.Drawing.Size(280, 500);
            this.dataGridView.TabIndex = 3;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.id.DefaultCellStyle = dataGridViewCellStyle1;
            this.id.FillWeight = 40F;
            this.id.HeaderText = "N";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 40;
            // 
            // run
            // 
            this.run.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.run.FillWeight = 120F;
            this.run.HeaderText = "Наклад";
            this.run.Name = "run";
            this.run.Width = 70;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.name.DefaultCellStyle = dataGridViewCellStyle2;
            this.name.FillWeight = 400F;
            this.name.HeaderText = "Назва";
            this.name.Name = "name";
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // richTextBox
            // 
            this.richTextBox.BackColor = System.Drawing.Color.White;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.DetectUrls = false;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.ForeColor = System.Drawing.Color.Black;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(577, 624);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.UseAntiAlias = true;
            this.printPreviewDialog1.Visible = false;
            // 
            // loadButton
            // 
            this.loadButton.BackgroundImage = global::Et075.Properties.Resources.table_add;
            this.loadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loadButton.Location = new System.Drawing.Point(4, 8);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(48, 48);
            this.loadButton.TabIndex = 0;
            this.loadButton.TabStop = false;
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // calculateButton
            // 
            this.calculateButton.AccessibleDescription = "";
            this.calculateButton.AccessibleName = "";
            this.calculateButton.BackgroundImage = global::Et075.Properties.Resources.Calculator_icon;
            this.calculateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.calculateButton.Location = new System.Drawing.Point(58, 8);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(48, 48);
            this.calculateButton.TabIndex = 1;
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // printButton
            // 
            this.printButton.BackgroundImage = global::Et075.Properties.Resources.Printer;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.printButton.Location = new System.Drawing.Point(4, 62);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(48, 48);
            this.printButton.TabIndex = 2;
            this.printButton.TabStop = false;
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.BackgroundImage = global::Et075.Properties.Resources.table_remove;
            this.clearButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clearButton.Location = new System.Drawing.Point(113, 8);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(48, 48);
            this.clearButton.TabIndex = 6;
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 646);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Et075 Run Analizer";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.dataSplitContainer.Panel1.ResumeLayout(false);
            this.dataSplitContainer.Panel1.PerformLayout();
            this.dataSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSplitContainer)).EndInit();
            this.dataSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox printersComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.SplitContainer dataSplitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn run;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private Et075.View.RichTextBoxEx richTextBox;

    }
}
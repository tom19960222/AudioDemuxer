namespace AudioDemuxer
{
    partial class form_MainWindow
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabpage_SingleFileDemuxWithChoose = new System.Windows.Forms.TabPage();
            this.gridview_Tracks = new System.Windows.Forms.DataGridView();
            this.txt_CommandLine = new System.Windows.Forms.TextBox();
            this.label_CommandLine = new System.Windows.Forms.Label();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.tabpage_BatchDemuxAll = new System.Windows.Forms.TabPage();
            this.OFD_InputFile = new System.Windows.Forms.OpenFileDialog();
            this.WantToDemux = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TrackNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BitDepth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Channels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabpage_SingleFileDemuxWithChoose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridview_Tracks)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabpage_SingleFileDemuxWithChoose);
            this.tabControl.Controls.Add(this.tabpage_BatchDemuxAll);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(701, 372);
            this.tabControl.TabIndex = 0;
            // 
            // tabpage_SingleFileDemuxWithChoose
            // 
            this.tabpage_SingleFileDemuxWithChoose.BackColor = System.Drawing.SystemColors.Control;
            this.tabpage_SingleFileDemuxWithChoose.Controls.Add(this.gridview_Tracks);
            this.tabpage_SingleFileDemuxWithChoose.Controls.Add(this.txt_CommandLine);
            this.tabpage_SingleFileDemuxWithChoose.Controls.Add(this.label_CommandLine);
            this.tabpage_SingleFileDemuxWithChoose.Controls.Add(this.btn_Start);
            this.tabpage_SingleFileDemuxWithChoose.Controls.Add(this.btn_Browse);
            this.tabpage_SingleFileDemuxWithChoose.Location = new System.Drawing.Point(4, 22);
            this.tabpage_SingleFileDemuxWithChoose.Name = "tabpage_SingleFileDemuxWithChoose";
            this.tabpage_SingleFileDemuxWithChoose.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_SingleFileDemuxWithChoose.Size = new System.Drawing.Size(693, 346);
            this.tabpage_SingleFileDemuxWithChoose.TabIndex = 0;
            this.tabpage_SingleFileDemuxWithChoose.Text = "單檔Demux";
            // 
            // gridview_Tracks
            // 
            this.gridview_Tracks.AllowUserToAddRows = false;
            this.gridview_Tracks.AllowUserToDeleteRows = false;
            this.gridview_Tracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridview_Tracks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridview_Tracks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridview_Tracks.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridview_Tracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridview_Tracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WantToDemux,
            this.TrackNumber,
            this.Format,
            this.BitDepth,
            this.Channels});
            this.gridview_Tracks.Location = new System.Drawing.Point(9, 35);
            this.gridview_Tracks.MultiSelect = false;
            this.gridview_Tracks.Name = "gridview_Tracks";
            this.gridview_Tracks.RowHeadersVisible = false;
            this.gridview_Tracks.RowTemplate.Height = 24;
            this.gridview_Tracks.Size = new System.Drawing.Size(674, 192);
            this.gridview_Tracks.TabIndex = 5;
            // 
            // txt_CommandLine
            // 
            this.txt_CommandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CommandLine.Location = new System.Drawing.Point(56, 233);
            this.txt_CommandLine.Name = "txt_CommandLine";
            this.txt_CommandLine.Size = new System.Drawing.Size(627, 22);
            this.txt_CommandLine.TabIndex = 4;
            // 
            // label_CommandLine
            // 
            this.label_CommandLine.AutoSize = true;
            this.label_CommandLine.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_CommandLine.Location = new System.Drawing.Point(9, 236);
            this.label_CommandLine.Name = "label_CommandLine";
            this.label_CommandLine.Size = new System.Drawing.Size(49, 14);
            this.label_CommandLine.TabIndex = 3;
            this.label_CommandLine.Text = "指令：";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(90, 6);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "開始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(9, 6);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.Text = "瀏覽";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // tabpage_BatchDemuxAll
            // 
            this.tabpage_BatchDemuxAll.Location = new System.Drawing.Point(4, 22);
            this.tabpage_BatchDemuxAll.Name = "tabpage_BatchDemuxAll";
            this.tabpage_BatchDemuxAll.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_BatchDemuxAll.Size = new System.Drawing.Size(693, 346);
            this.tabpage_BatchDemuxAll.TabIndex = 1;
            this.tabpage_BatchDemuxAll.Text = "批次全解";
            this.tabpage_BatchDemuxAll.UseVisualStyleBackColor = true;
            // 
            // OFD_InputFile
            // 
            this.OFD_InputFile.Filter = "所有支援格式 (*.mp4, *.mkv, *.m2ts, *.ts)|*.mp4;*.mkv;*.m2ts;*.ts|MP4檔 (*.mp4)|*.mp4|MK" +
    "V檔 (*.mkv)|*.mkv|M2TS / TS檔 (*.m2ts, *.ts)|*.m2ts;*.ts";
            this.OFD_InputFile.RestoreDirectory = true;
            this.OFD_InputFile.FileOk += new System.ComponentModel.CancelEventHandler(this.OFD_InputFile_FileOk);
            // 
            // WantToDemux
            // 
            this.WantToDemux.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.WantToDemux.HeaderText = "Demux?";
            this.WantToDemux.Name = "WantToDemux";
            this.WantToDemux.Width = 50;
            // 
            // TrackNumber
            // 
            this.TrackNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrackNumber.HeaderText = "Track No.";
            this.TrackNumber.Name = "TrackNumber";
            this.TrackNumber.ReadOnly = true;
            this.TrackNumber.Width = 77;
            // 
            // Format
            // 
            this.Format.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Format.HeaderText = "Format";
            this.Format.Name = "Format";
            this.Format.ReadOnly = true;
            this.Format.Width = 63;
            // 
            // BitDepth
            // 
            this.BitDepth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BitDepth.HeaderText = "Bit Depth";
            this.BitDepth.Name = "BitDepth";
            this.BitDepth.ReadOnly = true;
            this.BitDepth.Width = 75;
            // 
            // Channels
            // 
            this.Channels.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Channels.HeaderText = "Channels";
            this.Channels.Name = "Channels";
            this.Channels.ReadOnly = true;
            this.Channels.Width = 73;
            // 
            // form_MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 282);
            this.Controls.Add(this.tabControl);
            this.Name = "form_MainWindow";
            this.Text = "AudioDemuxer By 爆頭專家";
            this.tabControl.ResumeLayout(false);
            this.tabpage_SingleFileDemuxWithChoose.ResumeLayout(false);
            this.tabpage_SingleFileDemuxWithChoose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridview_Tracks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabpage_SingleFileDemuxWithChoose;
        private System.Windows.Forms.TabPage tabpage_BatchDemuxAll;
        private System.Windows.Forms.TextBox txt_CommandLine;
        private System.Windows.Forms.Label label_CommandLine;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.DataGridView gridview_Tracks;
        private System.Windows.Forms.OpenFileDialog OFD_InputFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn WantToDemux;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn BitDepth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Channels;

    }
}


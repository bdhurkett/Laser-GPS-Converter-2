namespace Laser_GPS_Converter_2
{
	partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.list_Tracks = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.n_Offset = new System.Windows.Forms.NumericUpDown();
            this.btn_Export = new System.Windows.Forms.Button();
            this.txt_Details = new System.Windows.Forms.TextBox();
            this.btn_Load = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_Offset)).BeginInit();
            this.SuspendLayout();
            // 
            // list_Tracks
            // 
            this.list_Tracks.FormattingEnabled = true;
            this.list_Tracks.IntegralHeight = false;
            this.list_Tracks.Location = new System.Drawing.Point(12, 41);
            this.list_Tracks.Name = "list_Tracks";
            this.list_Tracks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.list_Tracks.Size = new System.Drawing.Size(236, 250);
            this.list_Tracks.TabIndex = 0;
            this.list_Tracks.SelectedIndexChanged += new System.EventHandler(this.list_Tracks_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.n_Offset);
            this.groupBox1.Controls.Add(this.btn_Export);
            this.groupBox1.Controls.Add(this.txt_Details);
            this.groupBox1.Location = new System.Drawing.Point(254, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 275);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Track Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timezone offset (vs UTC)";
            // 
            // n_Offset
            // 
            this.n_Offset.DecimalPlaces = 1;
            this.n_Offset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.n_Offset.Location = new System.Drawing.Point(139, 249);
            this.n_Offset.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.n_Offset.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.n_Offset.Name = "n_Offset";
            this.n_Offset.Size = new System.Drawing.Size(45, 20);
            this.n_Offset.TabIndex = 2;
            this.n_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(249, 246);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 23);
            this.btn_Export.TabIndex = 1;
            this.btn_Export.Text = "Export...";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // txt_Details
            // 
            this.txt_Details.Font = new System.Drawing.Font("Courier New", 14F);
            this.txt_Details.Location = new System.Drawing.Point(7, 20);
            this.txt_Details.Multiline = true;
            this.txt_Details.Name = "txt_Details";
            this.txt_Details.ReadOnly = true;
            this.txt_Details.Size = new System.Drawing.Size(317, 223);
            this.txt_Details.TabIndex = 0;
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(12, 12);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(75, 23);
            this.btn_Load.TabIndex = 2;
            this.btn_Load.Text = "Load...";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "gpx";
            this.openFileDialog1.FileName = "BridgeMin.dll";
            this.openFileDialog1.Filter = "BridgeMin.dll|BridgeMin.dll|All files|*";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Laser GPS Database";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "gpx";
            this.saveFileDialog1.Filter = "GPX files|*.gpx";
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.Title = "Export GPX file...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 299);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.list_Tracks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Laser GPS Converter V2";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_Offset)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox list_Tracks;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown n_Offset;
		private System.Windows.Forms.Button btn_Export;
		private System.Windows.Forms.TextBox txt_Details;
		private System.Windows.Forms.Button btn_Load;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}


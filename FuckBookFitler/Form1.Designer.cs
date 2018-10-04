namespace FuckBookFitler
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnQueryByInput = new System.Windows.Forms.Button();
			this.btnQueryByWordlist = new System.Windows.Forms.Button();
			this.labelFileDrop = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(455, 98);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(15, 329);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(213, 20);
			this.textBox1.TabIndex = 1;
			// 
			// btnQueryByInput
			// 
			this.btnQueryByInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQueryByInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnQueryByInput.Location = new System.Drawing.Point(234, 326);
			this.btnQueryByInput.Name = "btnQueryByInput";
			this.btnQueryByInput.Size = new System.Drawing.Size(110, 23);
			this.btnQueryByInput.TabIndex = 2;
			this.btnQueryByInput.Text = "Custom Fitler";
			this.btnQueryByInput.UseVisualStyleBackColor = true;
			this.btnQueryByInput.Click += new System.EventHandler(this.btnQueryByInput_Click);
			// 
			// btnQueryByWordlist
			// 
			this.btnQueryByWordlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQueryByWordlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnQueryByWordlist.Location = new System.Drawing.Point(350, 326);
			this.btnQueryByWordlist.Name = "btnQueryByWordlist";
			this.btnQueryByWordlist.Size = new System.Drawing.Size(117, 23);
			this.btnQueryByWordlist.TabIndex = 3;
			this.btnQueryByWordlist.Text = "Fitler from Wordlist";
			this.btnQueryByWordlist.UseVisualStyleBackColor = true;
			this.btnQueryByWordlist.Click += new System.EventHandler(this.btnQueryByWordlist_Click);
			// 
			// labelFileDrop
			// 
			this.labelFileDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelFileDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelFileDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFileDrop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
			this.labelFileDrop.Location = new System.Drawing.Point(12, 107);
			this.labelFileDrop.Name = "labelFileDrop";
			this.labelFileDrop.Padding = new System.Windows.Forms.Padding(13);
			this.labelFileDrop.Size = new System.Drawing.Size(455, 207);
			this.labelFileDrop.TabIndex = 4;
			this.labelFileDrop.Text = "Drag files here...";
			this.labelFileDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 361);
			this.Controls.Add(this.labelFileDrop);
			this.Controls.Add(this.btnQueryByWordlist);
			this.Controls.Add(this.btnQueryByInput);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(495, 400);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FuckBookFitler ~ by Stelio Kontos";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnQueryByInput;
		private System.Windows.Forms.Button btnQueryByWordlist;
		private System.Windows.Forms.Label labelFileDrop;
	}
}


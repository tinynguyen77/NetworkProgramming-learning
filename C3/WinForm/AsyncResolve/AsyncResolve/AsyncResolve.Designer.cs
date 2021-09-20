
namespace AsyncResolve
{
    partial class DnsResolve
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
            this.lbAddr = new System.Windows.Forms.Label();
            this.lbResults = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbAddr
            // 
            this.lbAddr.AutoSize = true;
            this.lbAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAddr.Location = new System.Drawing.Point(51, 50);
            this.lbAddr.Name = "lbAddr";
            this.lbAddr.Size = new System.Drawing.Size(71, 25);
            this.lbAddr.TabIndex = 0;
            this.lbAddr.Text = "Địa chỉ";
            // 
            // lbResults
            // 
            this.lbResults.AutoSize = true;
            this.lbResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResults.Location = new System.Drawing.Point(51, 96);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(93, 25);
            this.lbResults.TabIndex = 1;
            this.lbResults.Text = "Phân giải";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(209, 53);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(360, 22);
            this.address.TabIndex = 2;
            // 
            // btnResolve
            // 
            this.btnResolve.AutoSize = true;
            this.btnResolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResolve.Location = new System.Drawing.Point(645, 53);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(92, 35);
            this.btnResolve.TabIndex = 4;
            this.btnResolve.Text = "Resolve";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // results
            // 
            this.results.FormattingEnabled = true;
            this.results.ItemHeight = 16;
            this.results.Location = new System.Drawing.Point(209, 96);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(360, 308);
            this.results.TabIndex = 5;
            // 
            // DnsResolve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.results);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.address);
            this.Controls.Add(this.lbResults);
            this.Controls.Add(this.lbAddr);
            this.Name = "DnsResolve";
            this.Text = "DnsResolve";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAddr;
        private System.Windows.Forms.Label lbResults;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.ListBox results;
    }
}
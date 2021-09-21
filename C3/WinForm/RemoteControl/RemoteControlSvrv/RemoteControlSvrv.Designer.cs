
namespace RemoteControlSvrv
{
    partial class Server
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
            this.lbPort = new System.Windows.Forms.Label();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.list_box = new System.Windows.Forms.ListBox();
            this.txt_Nhap = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPort.Location = new System.Drawing.Point(12, 26);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(40, 20);
            this.lbPort.TabIndex = 0;
            this.lbPort.Text = "Port";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(90, 26);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(411, 22);
            this.txt_Port.TabIndex = 1;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(540, 26);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(127, 23);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check Listen";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // list_box
            // 
            this.list_box.FormattingEnabled = true;
            this.list_box.ItemHeight = 16;
            this.list_box.Location = new System.Drawing.Point(12, 85);
            this.list_box.Name = "list_box";
            this.list_box.Size = new System.Drawing.Size(655, 212);
            this.list_box.TabIndex = 3;
            // 
            // txt_Nhap
            // 
            this.txt_Nhap.Location = new System.Drawing.Point(12, 315);
            this.txt_Nhap.Multiline = true;
            this.txt_Nhap.Name = "txt_Nhap";
            this.txt_Nhap.Size = new System.Drawing.Size(522, 82);
            this.txt_Nhap.TabIndex = 4;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(540, 315);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(127, 82);
            this.Send.TabIndex = 5;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 409);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.txt_Nhap);
            this.Controls.Add(this.list_box);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txt_Port);
            this.Controls.Add(this.lbPort);
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ListBox list_box;
        private System.Windows.Forms.TextBox txt_Nhap;
        private System.Windows.Forms.Button Send;
    }
}


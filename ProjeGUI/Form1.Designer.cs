namespace ProjeGUI
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.comPorts = new System.Windows.Forms.ComboBox();
            this.receivedList = new System.Windows.Forms.ListBox();
            this.decodedList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.slv3_data = new System.Windows.Forms.Label();
            this.slv2_data = new System.Windows.Forms.Label();
            this.slv1_data = new System.Windows.Forms.Label();
            this.slv3_rpm = new System.Windows.Forms.Label();
            this.slv2_rpm = new System.Windows.Forms.Label();
            this.slv1_rpm = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comPorts
            // 
            this.comPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPorts.FormattingEnabled = true;
            this.comPorts.Location = new System.Drawing.Point(16, 15);
            this.comPorts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comPorts.Name = "comPorts";
            this.comPorts.Size = new System.Drawing.Size(183, 24);
            this.comPorts.TabIndex = 0;
            this.comPorts.SelectedIndexChanged += new System.EventHandler(this.comPorts_SelectedIndexChanged);
            this.comPorts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comPorts_MouseClick);
            // 
            // receivedList
            // 
            this.receivedList.FormattingEnabled = true;
            this.receivedList.ItemHeight = 16;
            this.receivedList.Location = new System.Drawing.Point(16, 48);
            this.receivedList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.receivedList.Name = "receivedList";
            this.receivedList.Size = new System.Drawing.Size(183, 308);
            this.receivedList.TabIndex = 3;
            // 
            // decodedList
            // 
            this.decodedList.FormattingEnabled = true;
            this.decodedList.ItemHeight = 16;
            this.decodedList.Location = new System.Drawing.Point(227, 48);
            this.decodedList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.decodedList.Name = "decodedList";
            this.decodedList.Size = new System.Drawing.Size(358, 308);
            this.decodedList.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(50, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "SLAVE 1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slv3_data);
            this.groupBox1.Controls.Add(this.slv2_data);
            this.groupBox1.Controls.Add(this.slv1_data);
            this.groupBox1.Controls.Add(this.slv3_rpm);
            this.groupBox1.Controls.Add(this.slv2_rpm);
            this.groupBox1.Controls.Add(this.slv1_rpm);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 364);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(569, 123);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor Hız Durumu";
            // 
            // slv3_data
            // 
            this.slv3_data.AutoSize = true;
            this.slv3_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv3_data.Location = new System.Drawing.Point(438, 93);
            this.slv3_data.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv3_data.Name = "slv3_data";
            this.slv3_data.Size = new System.Drawing.Size(61, 18);
            this.slv3_data.TabIndex = 13;
            this.slv3_data.Text = "data = 1";
            // 
            // slv2_data
            // 
            this.slv2_data.AutoSize = true;
            this.slv2_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv2_data.Location = new System.Drawing.Point(253, 93);
            this.slv2_data.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv2_data.Name = "slv2_data";
            this.slv2_data.Size = new System.Drawing.Size(61, 18);
            this.slv2_data.TabIndex = 12;
            this.slv2_data.Text = "data = 1";
            // 
            // slv1_data
            // 
            this.slv1_data.AutoSize = true;
            this.slv1_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv1_data.Location = new System.Drawing.Point(72, 93);
            this.slv1_data.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv1_data.Name = "slv1_data";
            this.slv1_data.Size = new System.Drawing.Size(61, 18);
            this.slv1_data.TabIndex = 11;
            this.slv1_data.Text = "data = 1";
            // 
            // slv3_rpm
            // 
            this.slv3_rpm.AutoSize = true;
            this.slv3_rpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv3_rpm.Location = new System.Drawing.Point(433, 66);
            this.slv3_rpm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv3_rpm.Name = "slv3_rpm";
            this.slv3_rpm.Size = new System.Drawing.Size(78, 24);
            this.slv3_rpm.TabIndex = 10;
            this.slv3_rpm.Text = "125 rpm";
            // 
            // slv2_rpm
            // 
            this.slv2_rpm.AutoSize = true;
            this.slv2_rpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv2_rpm.Location = new System.Drawing.Point(248, 66);
            this.slv2_rpm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv2_rpm.Name = "slv2_rpm";
            this.slv2_rpm.Size = new System.Drawing.Size(78, 24);
            this.slv2_rpm.TabIndex = 9;
            this.slv2_rpm.Text = "125 rpm";
            // 
            // slv1_rpm
            // 
            this.slv1_rpm.AutoSize = true;
            this.slv1_rpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.slv1_rpm.Location = new System.Drawing.Point(62, 66);
            this.slv1_rpm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slv1_rpm.Name = "slv1_rpm";
            this.slv1_rpm.Size = new System.Drawing.Size(78, 24);
            this.slv1_rpm.TabIndex = 8;
            this.slv1_rpm.Text = "125 rpm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(418, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "SLAVE 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(234, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "SLAVE 2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.decodedList);
            this.Controls.Add(this.receivedList);
            this.Controls.Add(this.comPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proje GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comPorts;
        private System.Windows.Forms.ListBox receivedList;
        private System.Windows.Forms.ListBox decodedList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label slv3_data;
        private System.Windows.Forms.Label slv2_data;
        private System.Windows.Forms.Label slv1_data;
        private System.Windows.Forms.Label slv3_rpm;
        private System.Windows.Forms.Label slv2_rpm;
        private System.Windows.Forms.Label slv1_rpm;
    }
}


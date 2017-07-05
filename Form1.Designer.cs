namespace UValueCalc
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
            this.cmdAddLayer = new System.Windows.Forms.Button();
            this.cmdRemoveLayer = new System.Windows.Forms.Button();
            this.cmdAddMaterial = new System.Windows.Forms.Button();
            this.cmdRemoveMaterial = new System.Windows.Forms.Button();
            this.lisProperties = new System.Windows.Forms.ListView();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.txtProperty = new System.Windows.Forms.TextBox();
            this.comConstant = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUValue = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAddLayer
            // 
            this.cmdAddLayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdAddLayer.Location = new System.Drawing.Point(598, 12);
            this.cmdAddLayer.Name = "cmdAddLayer";
            this.cmdAddLayer.Size = new System.Drawing.Size(126, 34);
            this.cmdAddLayer.TabIndex = 3;
            this.cmdAddLayer.Text = "Add Layer";
            this.cmdAddLayer.UseVisualStyleBackColor = true;
            this.cmdAddLayer.Click += new System.EventHandler(this.cmdAddLayer_Click);
            // 
            // cmdRemoveLayer
            // 
            this.cmdRemoveLayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdRemoveLayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdRemoveLayer.Location = new System.Drawing.Point(730, 12);
            this.cmdRemoveLayer.Name = "cmdRemoveLayer";
            this.cmdRemoveLayer.Size = new System.Drawing.Size(126, 34);
            this.cmdRemoveLayer.TabIndex = 4;
            this.cmdRemoveLayer.Text = "Remove Layer";
            this.cmdRemoveLayer.UseVisualStyleBackColor = false;
            this.cmdRemoveLayer.Click += new System.EventHandler(this.cmdRemoveLayer_Click);
            // 
            // cmdAddMaterial
            // 
            this.cmdAddMaterial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdAddMaterial.Location = new System.Drawing.Point(598, 62);
            this.cmdAddMaterial.Name = "cmdAddMaterial";
            this.cmdAddMaterial.Size = new System.Drawing.Size(126, 34);
            this.cmdAddMaterial.TabIndex = 5;
            this.cmdAddMaterial.Text = "Add Material";
            this.cmdAddMaterial.UseVisualStyleBackColor = true;
            this.cmdAddMaterial.Click += new System.EventHandler(this.cmdAddMaterial_Click);
            // 
            // cmdRemoveMaterial
            // 
            this.cmdRemoveMaterial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmdRemoveMaterial.Location = new System.Drawing.Point(730, 62);
            this.cmdRemoveMaterial.Name = "cmdRemoveMaterial";
            this.cmdRemoveMaterial.Size = new System.Drawing.Size(126, 34);
            this.cmdRemoveMaterial.TabIndex = 6;
            this.cmdRemoveMaterial.Text = "Remove Material";
            this.cmdRemoveMaterial.UseVisualStyleBackColor = true;
            this.cmdRemoveMaterial.Click += new System.EventHandler(this.cmdRemoveMaterial_Click);
            // 
            // lisProperties
            // 
            this.lisProperties.Location = new System.Drawing.Point(598, 123);
            this.lisProperties.Name = "lisProperties";
            this.lisProperties.Size = new System.Drawing.Size(340, 280);
            this.lisProperties.TabIndex = 7;
            this.lisProperties.UseCompatibleStateImageBehavior = false;
            this.lisProperties.View = System.Windows.Forms.View.Details;
            this.lisProperties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lisProperties_MouseDown);
            this.lisProperties.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lisProperties_MouseUp);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(598, 426);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(126, 34);
            this.cmdEdit.TabIndex = 8;
            this.cmdEdit.Text = "Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // txtProperty
            // 
            this.txtProperty.Location = new System.Drawing.Point(543, 570);
            this.txtProperty.Name = "txtProperty";
            this.txtProperty.Size = new System.Drawing.Size(113, 25);
            this.txtProperty.TabIndex = 9;
            this.txtProperty.Visible = false;
            this.txtProperty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtProperty_KeyUp);
            this.txtProperty.Leave += new System.EventHandler(this.txtProperty_Leave);
            // 
            // comConstant
            // 
            this.comConstant.FormattingEnabled = true;
            this.comConstant.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comConstant.Location = new System.Drawing.Point(689, 570);
            this.comConstant.Name = "comConstant";
            this.comConstant.Size = new System.Drawing.Size(110, 25);
            this.comConstant.TabIndex = 10;
            this.comConstant.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(601, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "U-value:";
            // 
            // lblUValue
            // 
            this.lblUValue.AutoSize = true;
            this.lblUValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUValue.Location = new System.Drawing.Point(597, 505);
            this.lblUValue.Name = "lblUValue";
            this.lblUValue.Size = new System.Drawing.Size(59, 32);
            this.lblUValue.TabIndex = 12;
            this.lblUValue.Text = "0.00";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(436, 399);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(730, 426);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(126, 34);
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(989, 786);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comConstant);
            this.Controls.Add(this.txtProperty);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.lisProperties);
            this.Controls.Add(this.cmdRemoveMaterial);
            this.Controls.Add(this.cmdAddMaterial);
            this.Controls.Add(this.cmdRemoveLayer);
            this.Controls.Add(this.cmdAddLayer);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "U-value Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdAddLayer;
        private System.Windows.Forms.Button cmdRemoveLayer;
        private System.Windows.Forms.Button cmdAddMaterial;
        private System.Windows.Forms.Button cmdRemoveMaterial;
        private System.Windows.Forms.ListView lisProperties;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.TextBox txtProperty;
        private System.Windows.Forms.ComboBox comConstant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}


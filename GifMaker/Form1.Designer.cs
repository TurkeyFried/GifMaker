namespace GifMaker
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
            this.saveButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.previewButton = new System.Windows.Forms.Button();
            this.imageContainer = new System.Windows.Forms.Panel();
            this.imageLibrary = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.batchSaveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(293, 438);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(98, 33);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(443, 421);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(135, 50);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Add Images";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browse_Click);
            // 
            // previewBox
            // 
            this.previewBox.BackColor = System.Drawing.SystemColors.Window;
            this.previewBox.Location = new System.Drawing.Point(2, 214);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(285, 269);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previewBox.TabIndex = 3;
            this.previewBox.TabStop = false;
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(293, 355);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(98, 33);
            this.previewButton.TabIndex = 5;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = true;
            // 
            // imageContainer
            // 
            this.imageContainer.AllowDrop = true;
            this.imageContainer.AutoScroll = true;
            this.imageContainer.BackColor = System.Drawing.SystemColors.Window;
            this.imageContainer.Location = new System.Drawing.Point(2, 0);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(592, 100);
            this.imageContainer.TabIndex = 6;
            this.imageContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.imageContainer_DragDrop);
            this.imageContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.imageContainer_DragEnter);
            // 
            // imageLibrary
            // 
            this.imageLibrary.AllowDrop = true;
            this.imageLibrary.AutoScroll = true;
            this.imageLibrary.BackColor = System.Drawing.SystemColors.Window;
            this.imageLibrary.Dock = System.Windows.Forms.DockStyle.Right;
            this.imageLibrary.Location = new System.Drawing.Point(600, 0);
            this.imageLibrary.Name = "imageLibrary";
            this.imageLibrary.Size = new System.Drawing.Size(300, 483);
            this.imageLibrary.TabIndex = 7;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(293, 214);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // batchSaveButton
            // 
            this.batchSaveButton.Location = new System.Drawing.Point(443, 340);
            this.batchSaveButton.Name = "batchSaveButton";
            this.batchSaveButton.Size = new System.Drawing.Size(98, 33);
            this.batchSaveButton.TabIndex = 9;
            this.batchSaveButton.Text = "Batch Save";
            this.batchSaveButton.UseVisualStyleBackColor = true;
            this.batchSaveButton.Click += new System.EventHandler(this.batchSaveButton_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 483);
            this.Controls.Add(this.batchSaveButton);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.imageLibrary);
            this.Controls.Add(this.imageContainer);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.saveButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.DragLeave += new System.EventHandler(this.OnDragLeave);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Panel imageContainer;
        private System.Windows.Forms.Panel imageLibrary;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button batchSaveButton;
    }
}


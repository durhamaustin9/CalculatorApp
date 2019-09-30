namespace CalculatorApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Number1 = new System.Windows.Forms.TextBox();
            this.Number2 = new System.Windows.Forms.TextBox();
            this.Subtract = new System.Windows.Forms.Button();
            this.Divide = new System.Windows.Forms.Button();
            this.Multiply = new System.Windows.Forms.Button();
            this.Remainder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Input 2";
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(161, 11);
            this.Add.Margin = new System.Windows.Forms.Padding(2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(56, 19);
            this.Add.TabIndex = 2;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // Number1
            // 
            this.Number1.Location = new System.Drawing.Point(73, 11);
            this.Number1.Margin = new System.Windows.Forms.Padding(2);
            this.Number1.Name = "Number1";
            this.Number1.Size = new System.Drawing.Size(76, 20);
            this.Number1.TabIndex = 3;
            // 
            // Number2
            // 
            this.Number2.Location = new System.Drawing.Point(73, 40);
            this.Number2.Margin = new System.Windows.Forms.Padding(2);
            this.Number2.Name = "Number2";
            this.Number2.Size = new System.Drawing.Size(76, 20);
            this.Number2.TabIndex = 4;
            // 
            // Subtract
            // 
            this.Subtract.Location = new System.Drawing.Point(161, 34);
            this.Subtract.Margin = new System.Windows.Forms.Padding(2);
            this.Subtract.Name = "Subtract";
            this.Subtract.Size = new System.Drawing.Size(56, 19);
            this.Subtract.TabIndex = 5;
            this.Subtract.Text = "Subtract";
            this.Subtract.UseVisualStyleBackColor = true;
            this.Subtract.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // Divide
            // 
            this.Divide.Location = new System.Drawing.Point(161, 58);
            this.Divide.Margin = new System.Windows.Forms.Padding(2);
            this.Divide.Name = "Divide";
            this.Divide.Size = new System.Drawing.Size(56, 19);
            this.Divide.TabIndex = 6;
            this.Divide.Text = "Divide";
            this.Divide.UseVisualStyleBackColor = true;
            this.Divide.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // Multiply
            // 
            this.Multiply.Location = new System.Drawing.Point(161, 81);
            this.Multiply.Margin = new System.Windows.Forms.Padding(2);
            this.Multiply.Name = "Multiply";
            this.Multiply.Size = new System.Drawing.Size(56, 19);
            this.Multiply.TabIndex = 7;
            this.Multiply.Text = "Multiply";
            this.Multiply.UseVisualStyleBackColor = true;
            this.Multiply.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // Remainder
            // 
            this.Remainder.Location = new System.Drawing.Point(152, 105);
            this.Remainder.Name = "Remainder";
            this.Remainder.Size = new System.Drawing.Size(76, 19);
            this.Remainder.TabIndex = 8;
            this.Remainder.Text = "Remainder";
            this.Remainder.UseVisualStyleBackColor = true;
            this.Remainder.Click += new System.EventHandler(this.ActionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 134);
            this.Controls.Add(this.Remainder);
            this.Controls.Add(this.Multiply);
            this.Controls.Add(this.Divide);
            this.Controls.Add(this.Subtract);
            this.Controls.Add(this.Number2);
            this.Controls.Add(this.Number1);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.TextBox Number1;
        private System.Windows.Forms.TextBox Number2;
        private System.Windows.Forms.Button Subtract;
        private System.Windows.Forms.Button Divide;
        private System.Windows.Forms.Button Multiply;
        private System.Windows.Forms.Button Remainder;
    }
}


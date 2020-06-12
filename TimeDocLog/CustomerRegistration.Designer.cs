using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDocLog
{
    partial class CustomerRegistration
    {

        String customerName;
        String currency;
        String paymentMethod;
        double payRate;

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
            this.customerNameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.currencyText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.payRateText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.paymentMethodText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Name";
            // 
            // customerNameText
            // 
            this.customerNameText.Location = new System.Drawing.Point(251, 65);
            this.customerNameText.Name = "customerNameText";
            this.customerNameText.Size = new System.Drawing.Size(273, 20);
            this.customerNameText.TabIndex = 1;
            this.customerNameText.TextChanged += new System.EventHandler(this.customerName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.Text = "Currency";
            // 
            // currencyText
            // 
            this.currencyText.Location = new System.Drawing.Point(251, 133);
            this.currencyText.Name = "currencyText";
            this.currencyText.Size = new System.Drawing.Size(273, 20);
            this.currencyText.TabIndex = 2;
            this.currencyText.TextChanged += new System.EventHandler(this.currencyChange_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "PayRate";
            // 
            // payRateText
            // 
            this.payRateText.Location = new System.Drawing.Point(251, 203);
            this.payRateText.Name = "payRateText";
            this.payRateText.Size = new System.Drawing.Size(273, 20);
            this.payRateText.TabIndex = 3;
            this.payRateText.TextChanged += new System.EventHandler(this.payRateChange_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.Text = "Payment Method";
            // 
            // paymentMethodText
            this.paymentMethodText.Location = new System.Drawing.Point(251, 265);
            this.paymentMethodText.Name = "paymentMethodText";
            this.paymentMethodText.Size = new System.Drawing.Size(273, 20);
            this.paymentMethodText.TabIndex = 4;
            this.paymentMethodText.TextChanged += new System.EventHandler(this.paymentMethodChange_TextChanged);

            // 
            // button1
            this.button1.Location = new System.Drawing.Point(278, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Submit";
            this.button1.Click += new EventHandler(updateDB_event);
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CustomerRegistration
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.paymentMethodText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.payRateText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currencyText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customerNameText);
            this.Controls.Add(this.label1);
            this.Name = "CustomerRegistration";
            this.Text = "Customer Registration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=C:/Don/TimeDocLog.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception value " + ex.Message);
            }
            return sqlite_conn;
        }


        private void updateDB_event(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            InsertData(sqlite_conn);
        }

        private void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = "INSERT INTO CustomerTable(Name, Currency, Paymentmethod, Payrate) VALUES(\"" + customerName+ "\",\"" + currency + "\",\"" + paymentMethod + "\"," + payRate + "); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.Dispose();
            new CustomerTimeLog();
        }
        private void customerName_TextChanged(object sender, EventArgs e)
        {
            customerName = this.customerNameText.Text;
        }

        private void paymentMethodChange_TextChanged(object sender, EventArgs e)
        {
            paymentMethod = this.paymentMethodText.Text;
        }

        private void payRateChange_TextChanged(object sender, EventArgs e)
        {
            payRate = Convert.ToDouble(this.payRateText.Text);
        }

        private void currencyChange_TextChanged(object sender, EventArgs e)
        {
            currency = this.currencyText.Text;
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox customerNameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox currencyText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox payRateText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox paymentMethodText;
        private System.Windows.Forms.Button button1;
    }
}


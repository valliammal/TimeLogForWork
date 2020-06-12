using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeDocLog
{
    partial class CustomerTimeLog
    {
        DateTime t1;
        String customerName;
        String totalTime;
        String startTime;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void updateDB_event()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            ListData(sqlite_conn);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CustomerName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "End Time";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(639, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "TotalTime";
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(90, 101);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(625, 97);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // CustomerTimeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CustomerTimeLog";
            this.Text = "CustomerTimeLog";
            this.ResumeLayout(false);
            this.PerformLayout();
            updateDB_event();

        }

        void ListData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "select Name, Totaltime from CustomerTable";
            sqlite_cmd.ExecuteNonQuery();
            // Get the List of items. and keep in sequence
            // add the items in the MessageBox
            System.Data.IDataReader reader = sqlite_cmd.ExecuteReader();
            // There needs to be a button addition after CustomerName
            int index = 0;
            String firstCustomerName = "";
            String totalTime = "";

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("CustomerName", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("StartTime", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("EndTime", -2, HorizontalAlignment.Center);
            listView1.Columns.Add("TotalTime", -2, HorizontalAlignment.Right);
            ListViewItem[] rangeItems = new ListViewItem[3];
            
            while (reader.Read())
            {
                index++;
                if (index == 1) {
                    firstCustomerName = reader["Name"].ToString();
                    totalTime = reader["Totaltime"].ToString();
                }
                ListViewItem item1 = new ListViewItem(reader["Name"].ToString(), 0);
                item1.SubItems.Add(DateTime.Now.ToString()); 
                item1.SubItems.Add(DateTime.Now.ToString());
                item1.SubItems.Add(reader["Totaltime"].ToString());
                rangeItems.Append(item1);
                this.listView1.Items.Add(item1);
            }
            sqlite_cmd.Dispose();
            listView1.MultiSelect = true;
            listView1.MouseClick += listView_MouseClick;
            reader.Close();
        }
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            var info = listView1.HitTest(e.X, e.Y);
            var row = info.Item.Index;
            var col = info.Item.SubItems.IndexOf(info.SubItem);
            customerName = info.Item.SubItems[0].Text;
            totalTime = info.Item.SubItems[3].Text;
            startTime = info.Item.SubItems[0].Text;
            t1 = DateTime.Now;

            using (System.Diagnostics.Process myProcess = new System.Diagnostics.Process())
            {
                myProcess.StartInfo.FileName = "C:\\Don\\WebLaunchRecorder.exe";
                myProcess.EnableRaisingEvents = true;
                myProcess.Start();
                myProcess.WaitForExit();
                myProcess.Exited += new EventHandler(updateDBData);
            }
        }

        private void updateDBData(object sender, EventArgs e)
        {
            DateTime t2 = DateTime.Now;
            TimeSpan ts = t1.Subtract(t2);
            String totTime = ts.ToString();
            TimeSpan s2 = TimeSpan.Parse(totalTime);
            // With this , it needs to have addition.
            // Update the data in the database
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = CreateConnection().CreateCommand();
            sqlite_cmd.CommandText = "UPDATE CustomerTable Set Totaltime = " + (ts + s2).ToString() + " WHERE Name = " + customerName + "''"; 
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.Dispose();
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
            } catch (Exception ex) { 
                Console.WriteLine("Exception value " + ex.Message);
            }
            return sqlite_conn;
        }


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label4;
        private ListView listView1;
        private Label label5;
    }
}
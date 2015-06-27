using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace OpenMesh
{

    public partial class ErrorDialog : Form
    {

        private ErrorDialog()
        {
            this.InitializeComponent();

            this.Panel1Height = this.panel1.Height;
            this.Panel2Height = this.panel2.Height;
            this.tableLayoutPanel1.RowStyles[0].Height = this.Panel1Height;

            this.Collapse();
        }

        public void Expand()
        {
            this.ClientSize = new Size(this.ClientSize.Width, this.Panel1Height + this.Panel2Height + 24);
            this.tableLayoutPanel1.RowStyles[1].Height = this.Panel2Height;
            this.button1.Text = "Less ▲";
            this._IsExpanded = true;
        }

        public void Collapse()
        {
            this.ClientSize = new Size(this.ClientSize.Width, this.Panel1Height + 24);
            this.tableLayoutPanel1.RowStyles[1].Height = 0;
            this.button1.Text = "More ▼";
            this._IsExpanded = false;
        }

        private void ShowImpl(String Message, Exception Exception, StackTrace StackTrace)
        {
            var MoreInfo = new StringBuilder();
            var Bl = true;

            while (Exception != null)
            {
                if (Bl)
                {
                    MoreInfo.Append("Exception of type '")
                            .Append(Exception.GetType().FullName)
                            .Append("' occurred.")
                            .AppendLine();
                    Bl = false;
                }
                else
                {
                    MoreInfo.AppendLine()
                            .AppendLine();
                    MoreInfo.Append("Cause: Exception of type '")
                            .Append(Exception.GetType().FullName)
                            .Append("'.")
                            .AppendLine();
                }

                MoreInfo.Append("Message: ")
                        .Append(Exception.Message)
                        .AppendLine();
                MoreInfo.Append("Stack Trace:")
                        .AppendLine();
                MoreInfo.Append(Exception.StackTrace);

                if (StackTrace != null)
                {
                    MoreInfo.AppendLine()
                            .Append(StackTrace.ToString().TrimEnd());
                }

                Exception = Exception.InnerException;
                StackTrace = null;
            }

            this.textBox1.Text = Message;
            this.textBox2.Text = MoreInfo.ToString();

            this.ShowDialog();
        }

        public static void Show(String Message, Exception Exception = null, StackTrace StackTrace = null)
        {
            new ErrorDialog().ShowImpl(Message, Exception, StackTrace);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsExpanded = !this.IsExpanded;
        }

        private Boolean _IsExpanded;

        public Boolean IsExpanded
        {
            get
            {
                return this._IsExpanded;
            }
            set
            {
                if (this._IsExpanded != value)
                {
                    if (value)
                        this.Expand();
                    else
                        this.Collapse();
                }
            }
        }
        private readonly Int32 Panel1Height;
        private readonly Int32 Panel2Height;

    }

}

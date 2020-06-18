using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eq;

namespace QuEq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }
        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if((a.Text == "A") || (a.Text =="B") || (a.Text == "C"))
            {
                a.Text = "";
            }

            
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            if ((a.Text == "") || (a.Text == "") || (a.Text == ""))
            {
                a.Text = a.Tag.ToString();
            }

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {

            TextBox a = (TextBox)sender;
            List<char> chars = new List<char>(a.Text.ToCharArray());
            for (int i = 0; i < chars.Count; i++)
            {
                if (!char.IsDigit(chars[i]))
                {
                    if (chars[i] != '-')
                    {
                        chars.RemoveAt(i);
                    }
                }
            }
            a.SelectionStart = a.Text.Length;
            a.SelectionLength = 0;
            a.Text = string.Join("",chars.ToArray());
            
        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                string error = "";
                double[] solution = SolveEq.Solve(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), out error);
                foreach (double item in solution)
                {
                    
                    button2.Text += Math.Round(item, 4).ToString() + ";  ";
                }
                button2.Text += error;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You airball it and you hit LeBron James in the face. He gets mad at you and dunks it. Unfortunately You were directly under the hoop and it hits you darn hard. The End.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("He signs it. Choose more options.");
            Form5 form = new Form5();
            form.Show();
        }

    }
}

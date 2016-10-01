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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are just pure stupid to do dat. No need to go further, bubba. IQ!! aka: I Quit!!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You attempt slam dunking and succeed. LeBron James is at the same gym. Select another option. ");
            Form4 form = new Form4();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You hit it in the water and rage. You jump in to find your ball, but an aliggaiytter is there. Have fun!! :)");
        }
        }
    }


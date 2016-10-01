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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           MessageBox.Show("You fall off the tree and fall on a hornet's nest. You are seriously injured. Have a good day.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You eat the burger too fast and get a full on stomach ache for a year.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You win and get $1 million and you are drafted to the NBA. You have succeeded in my text adventure");
        }
    
    }
}

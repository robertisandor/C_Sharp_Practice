using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobertSandor_MadLibsWinForms
{
    public partial class Form1 : Form
    {
        // if I comment out the code, go to Form1.Designer and comment out the corresponding code
        public Form1()
        {
            InitializeComponent();
        }

        List<TextBox> tbs = new List<TextBox>();

        // if I were to create textboxes manually (instead of drag-and-drop), this is how I would add it to a list
        /*private void Form1_Load(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Size = new Size(20, 10);
            tb.Location = new Point(250, 250);
            tbs.Add(tb);
            Controls.Add(tb);
        }*/

        // how would I loop through the textboxes if I created the textboxes through drag-and-drop

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void TextBox_Click(object sender, EventArgs e)
        {
            // if textbox was clicked, remove the previous text, else if it's empty again, put back the previous text
            TextBox selectedTextbox = (TextBox)sender;
            
            // if the textbox is clicked and it equals the tag, clear it
            if (selectedTextbox.Text.Equals(selectedTextbox.Tag))
            {
                selectedTextbox.Clear();
            }
            // if you click/tab away from the textbox and the text is empty, the text from the tag comes back
        }

        // sender is what is sending the event, EventArgs is how the event was triggered
        private void button1_Click(object sender, EventArgs e)
        {
            // check if all of the textboxes were filled
            // compare the textbox's text to the textbox's tag, 
            // if they're not equal for all of the textboxes (use a loop), load the story
            // how do I loop through the textboxes? foreach loop?
            // if I created the textboxes manually, this is how I would loop through the list
            // Control is the parent
            //Controls.OfType<TextBox>()
            // this should loop through all of the children of Controls and check if the textbox text matches the textbox tag
            foreach (Control tb in Controls)
            {
                if(tb is TextBox)
                {
                    if (tb.Text.Equals(tb.Tag))
                    {

                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

    }
}

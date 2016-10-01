using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSandor___GuessingGame
{
    public partial class Form1 : Form
    {
        // make the textbox uneditable once # of attempts gets to 0?
        // MUST use button to submit number (instead of TextChanged)
        const int UPPER_END_OF_RANGE = 100;
        int numberOfAttempts = 5;
        int computerNumber;
        Random numberGenerator;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numberGenerator = new Random();
            computerNumber = numberGenerator.Next(1, UPPER_END_OF_RANGE);
        }

        // do I want it everytime it's clicked? No, I only want it when it's clicked & hasn't been changed
        private void UserInputTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            UserInputTextBox.Text = "";
        }

        // if the text is changed, validate that it's a number; parse the info (TryParse) and check that it's within the range
        // if it's not a number, make the background color red until it is a valid number
        // once it's valid input, change the background color back to white and decrement the counter
        // illustrate a counter that shows how many attempts left
        private void UserInputTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userGuess;
            String userInput = UserInputTextBox.Text;
            // will this allow a user to change it?
            if (!Int32.TryParse(userInput, out userGuess))
            {
                // make background color red
                UserInputTextBox.BackColor = Color.Red;
            }
            // once I confirm that it's a number, check to make sure that it's within the range
            if (userGuess.Range(1, UPPER_END_OF_RANGE))
            {
                BackColor = Color.White;
                numberOfAttempts--;
                label4.Text = String.Format("Number of attempts: {0}", numberOfAttempts);
                if (userGuess > computerNumber)
                {
                    HigherOrLower.Text = "Lower";
                }
                else if (userGuess < computerNumber)
                {
                    HigherOrLower.Text = "Higher";
                }
                else if (userGuess == computerNumber)
                {
                    HigherOrLower.Text = "You guessed correctly! You won! Congrats!";
                }
            }

            if (numberOfAttempts <= 0)
            {
                UserInputTextBox.Enabled = false;
                HigherOrLower.Text = "You lost! Try again.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numberOfAttempts = 5;
            HigherOrLower.Text = "I'll tell you to go higher or lower!";
            computerNumber = numberGenerator.Next(1, UPPER_END_OF_RANGE);
            label4.Text = String.Format("Number of attempts: {0}", numberOfAttempts);
            UserInputTextBox.Enabled = true;
        }



        // Anchor - absolute positioning
        // Dock - relative positioning
    }
}

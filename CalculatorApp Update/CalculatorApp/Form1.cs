using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            var input1 = Number1.Text;
            var input2 = Number2.Text;
            int answer = 0;

            string actionPressed = ((Button)sender).Name;

            switch(actionPressed)
            {
                case "Add":
                    if (input1.All(char.IsNumber) && input2.All(char.IsNumber))
                    {

                        answer = int.Parse(input1) + int.Parse(input2);

                        MessageBox.Show(answer.ToString());
                    }
                    else
                    {
                       inputCheck();
                    }
                    break;

                case "Subtract":
                    if (input1.All(char.IsNumber) && input2.All(char.IsNumber))
                    {

                        answer = int.Parse(input1) - int.Parse(input2);

                        MessageBox.Show(answer.ToString());
                    }
                    else
                    {
                        inputCheck();
                    }
                    break;

                case "Divide":
                    if (input1.All(char.IsNumber) && input2.All(char.IsNumber))
                    {

                        answer = int.Parse(input1) / int.Parse(input2);

                        MessageBox.Show(answer.ToString());
                    }
                    else
                    {
                        inputCheck();
                    }
                    break;

                case "Multiply":
                    if (input1.All(char.IsNumber) && input2.All(char.IsNumber))
                    {

                        answer = int.Parse(input1) * int.Parse(input2);

                        MessageBox.Show(answer.ToString());
                    }
                    else
                    {
                        inputCheck();
                    }
                    break;

                case "Remainder":
                    if (input1.All(char.IsNumber) && input2.All(char.IsNumber))
                    {

                        answer = int.Parse(input1) % int.Parse(input2);

                        MessageBox.Show(answer.ToString());
                    }
                    else
                    {
                        inputCheck();
                    }
                    break;
            }

            void inputCheck()
            {
                if (input1.All(char.IsLetter))
                {
                    MessageBox.Show("Input 1 Is Not a Valid Integer: " + input1);
                }
                else
                {
                    MessageBox.Show("Input 2 Is Not a Valid Integer: " + input2);
                }
            }

        }
    }
}

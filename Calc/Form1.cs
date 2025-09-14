using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace Calc
{
    public partial class Form1 : Form
    {
        string[] actions = new string[2];
        decimal? num1 = 0, num2;
        char action;
        bool toclear = false, cleared = true, canrewrite = false, canwrite = true, canclear;
        public Form1()
        {
            InitializeComponent();
        }
        void dii(decimal a)
        {
            if (toclear) { label1.Text = ""; toclear = false; cleared = true; canwrite = true; }
            if (canrewrite) { label2.Text = " "; num1 = null; canrewrite = false; canwrite = true; }
            label1.Text += a.ToString();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            dii(1);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            dii(2);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            dii(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dii(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dii(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dii(6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dii(7);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dii(8);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dii(9);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            dii(0);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (toclear) { label1.Text = ""; toclear = false; cleared = true; canwrite = true; }
            if (label1.Text == "") return;
            if (label1.Text.Split(',').Count() > 2) return;
            else label1.Text += ",";
        }
        decimal? LabToDec()
        {
            if (actions.Count() == 2) return (decimal.Parse($"{actions[0]},{actions[1]}"));
            else if (actions.Count() == 1) return decimal.Parse($"{actions[0]}");
            else return null;
        }
        void OperationDo()
        {
            if (label1.Text != "") actions = label1.Text.Split(',');
            if (num1 == null || label2.Text == "")
            {
                num1 = LabToDec();
                label2.Text = num1.ToString() + " " + action;
                toclear = true;
            }
            else if (cleared && canwrite && num1 != null && num2 == null)
            {
                num2 = LabToDec();
                label2.Text = (num1 + LabToDec()).ToString() + " " + action;
                toclear = true;
            }
            else if (cleared && canwrite && num1 != null && num2 != null)
            {
                num1 = ActionToDo(num1, LabToDec());
                label2.Text = (num1).ToString() + " " + action;
                toclear = true; canwrite = false;canclear = false;
            }
        }

        decimal? ActionToDo(decimal? a, decimal? b)
        {
            if (a == null) return null;

            switch (action)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return Func.Delit(a, b);
                case '1': return Func.Drob(a);
                case '√': return Func.Kor(a);
                default: return null;
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (label1.Text != "") actions = label1.Text.Split(',');
            if (num1 != null && num2 == null && label1.Text != "")
            {
                num2 = LabToDec();
                label2.Text = $"{num1} {action} {num2} =";
                num1 = ActionToDo(num1, num2);
                label1.Text = num1.ToString();
            }
            else if (num1 != null && num2 != null && label1.Text != "")
            {
                num1 = ActionToDo(num1, num2);
                label2.Text = $"{num1} {action} {num2} =";
                label1.Text = num1.ToString();
            }
            else if (num1 == null && num2 != null && label1.Text != "")
            {
                num1 = LabToDec();
                label2.Text = $"{num1} {action} {num2} =";
                num1 = ActionToDo(num1, num2);
                label1.Text = num1.ToString();
            }

            canrewrite = true; toclear = true;
        }//   ====
        private void button2_Click(object sender, EventArgs e)
        {
            action = '+';
            OperationDo();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            action = '-';
            OperationDo();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            action = '*';
            OperationDo();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            action = '/';
            OperationDo();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            num1 = Func.Kor(decimal.Parse(label1.Text));
            label1.Text = num1.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            num1 = decimal.Parse(label1.Text) * decimal.Parse(label1.Text);
            label1.Text = num1.ToString();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            num1 = Func.Drob(decimal.Parse(label1.Text));
            label1.Text = num1.ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            num1 = decimal.Parse(label1.Text) / 100;
            label1.Text = num1.ToString();
            toclear = true;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (canclear && canwrite)
            {
                if (label1.Text.Split().Count() > 0)
                {
                    string[] array = label1.Text.ToCharArray().Select(c => c.ToString()).ToArray();
                    label1.Text = ""; 
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        label1.Text += array[i].ToString();
                    }
                }
            }
        }
    }
}

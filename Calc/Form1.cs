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
        bool toclear = false, cleared = true, torewrite = false, canrewrite = false, canwrite = false;
        public Form1()
        {
            InitializeComponent();
        }
        void dii(decimal a) 
        {
            if (toclear) { label1.Text = ""; toclear = false; cleared = true; canwrite = true; }
            //if(canrewrite){label2.Text = " ";canrewrite = false;}
            label1.Text += a.ToString();
            //torewrite = true;
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
        void fornum1() {
            if (num1 == null)
            {
                if (actions.Count() == 2) num1 = (decimal.Parse($"{actions[0]},{actions[1]}"));
                else if (actions.Count() == 1) num2 = decimal.Parse($"{actions[0]}");
                label2.Text = num1.ToString() + " " + action;
            }
        }
        decimal? LabToDec()
        {
            if (actions.Count() == 2)  return(decimal.Parse($"{actions[0]},{actions[1]}"));
            if (actions.Count() == 1) return decimal.Parse($"{actions[0]}");
            else return null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            action = '+';
            if (label1.Text != "") actions = label1.Text.Split(',');
            if (label2.Text == "")
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
        }

        private void button8_Click(object sender, EventArgs e)
        {
            action = '-';
            if (label1.Text != "") actions = label1.Text.Split(',');
            if (num1 == null)
            {
                if (actions.Count() == 2) num1 = (decimal.Parse($"{actions[0]},{actions[1]}"));
                else if (actions.Count() == 1) num1 = decimal.Parse($"{actions[0]}");
                label2.Text = num1.ToString() + " " + action;
            }
            else if (num1 + num2 == LabToDec())
            {
                num1 = LabToDec();
                num2 = null;
                label2.Text = label1.Text + " " + action;
                torewrite = false;
            }
            else if (num2 != null)
            {
                num2 = LabToDec();
                num1 -= num2;
                label2.Text = num1.ToString() + " " + action;
                label1.Text = num1.ToString();
            }
            else
            {
                if (cleared) num1 -= LabToDec();
                label2.Text = num1.ToString() + " " + action;

                cleared = false;
            }
            toclear = true;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            
            //if ((cleared && num2 != null) || num2 == null)
            //{
            //    num2 = decimal.Parse(label1.Text);
            //    label2.Text = num1.ToString() + " " + action + " " + num2.ToString() + " =";
            //    label1.Text = (num1 + num2).ToString();
            //    toclear = true; cleared = false;
            //}
            //else if (torewrite)
            //{
            //    num1 = decimal.Parse(label1.Text) + num2;
            //    label2.Text = label1.Text + " " + action + " " + num2.ToString() + " =";
            //    label1.Text = num1.ToString();
            //    torewrite = false;
            //}
            //else
            //{
            //    num1 += num2;
            //    label2.Text = num1.ToString() + " " + action + " " + num2.ToString() + " =";
            //    label1.Text = (num1 + num2).ToString();
            //    toclear = true; cleared = false;
            //} canrewrite = true;
        }
    }
}

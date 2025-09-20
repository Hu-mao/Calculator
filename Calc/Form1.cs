using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.Design.AxImporter;
namespace Calc
{
    public partial class Form1 : Form
    {
        string[] actions = new string[2];
        List<KeyValuePair<string, decimal>> book = new List<KeyValuePair<string, decimal>>();
        List<decimal> memory = new List<decimal>();
        decimal? num1 = 0, num2;
        char? action;
        bool toclear = true, cleared = false, canrewrite = false, canwrite = true, resized = false, isbook = true;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(460, 660);
            string[] a = File.ReadAllText("memory.txt").Split(" ");
            foreach (string b in a)
            {
                if (!string.IsNullOrWhiteSpace(b)) memory.Add(decimal.Parse(b));
            }
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
                toclear = true; canwrite = false;
            }
        }
        void FeelBook()
        {
            Label[] keyLabels = { label8, label12, label14, label16, label11 };
            Label[] valueLabels = { label9, label13, label15, label17, label18 };

            for (int i = 0; i < keyLabels.Length; i++)
            {
                keyLabels[i].Text = "";
                valueLabels[i].Text = "";
            }

            int count = book.Count;
            for (int i = 0; i < Math.Min(5, count); i++)
            {
                var element = book[count - 1 - i];
                keyLabels[i].Text = element.Key;
                valueLabels[i].Text = element.Value.ToString();
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
            if (num1 != null && num2 == null && label1.Text != "" && action != null)
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
            if (!string.IsNullOrEmpty(label2.Text) && !string.IsNullOrEmpty(label1.Text))
            {
                book.Add(new KeyValuePair<string, decimal>(label2.Text, decimal.Parse(label1.Text)));
                if (isbook) FeelBook();
            }
            canrewrite = true; toclear = true;
        }
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
            if (canwrite)
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

        private void button14_Click(object sender, EventArgs e)
        {
            num1 = 0; num2 = null; label1.Text = "0"; label2.Text = ""; action = null;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            num1 = 0; label1.Text = "0";
            label2.Text = "";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (!resized) { Size = new Size(745, 660); resized = true; button21.Text = "←"; }
            else { Size = new Size(460, 660); resized = false; button21.Text = "→"; }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (!isbook)
            {
                isbook = true;
                label8.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label14.Visible = true;
                label16.Visible = true;
                label10.Font = new Font(label10.Font, FontStyle.Underline);
                label20.Font = new Font(label10.Font, FontStyle.Regular);
                changeShowForBtn();
                FeelBook();
            }
        }
        void feelMemoryText()
        {
            if (memory.Count == 0)
            {
                label9.Text = "";
                label13.Text = "";
                label15.Text = "";
                label17.Text = "";
                label18.Text = "";
            }
            else if (memory.Count() == 1)
            {
                label9.Text = memory[0].ToString();
                label13.Text = "";
                label15.Text = "";
                label17.Text = "";
                label18.Text = "";
            }
            else if (memory.Count() == 2)
            {
                label9.Text = memory[0].ToString();
                label13.Text = memory[1].ToString();
                label15.Text = "";
                label17.Text = "";
                label18.Text = "";
            }
            else if (memory.Count() == 3)
            {
                label9.Text = memory[0].ToString();
                label13.Text = memory[1].ToString();
                label15.Text = memory[2].ToString();
                label17.Text = "";
                label18.Text = "";
            }
            else if (memory.Count() == 4)
            {
                label9.Text = memory[0].ToString();
                label13.Text = memory[1].ToString();
                label15.Text = memory[2].ToString();
                label17.Text = memory[3].ToString();
                label18.Text = "";
            }
            else if (memory.Count() >= 5)
            {
                label9.Text = memory[0].ToString();
                label13.Text = memory[1].ToString();
                label15.Text = memory[2].ToString();
                label17.Text = memory[3].ToString();
                label18.Text = memory[4].ToString();
            }
        }
        private void label20_Click(object sender, EventArgs e)
        {
            if (isbook)
            {
                isbook = false;
                label8.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label14.Visible = false;
                label16.Visible = false;
                label20.Font = new Font(label10.Font, FontStyle.Underline);
                label10.Font = new Font(label10.Font, FontStyle.Regular);
                feelMemoryText();
            }
        }

        private void label9_TextChanged(object sender, EventArgs e)
        {
            if (label9.Text == "")
            {
                mr.ForeColor = Color.Gray;
                mc.ForeColor = Color.Gray;
                button27.Visible = false;
                button23.Visible = false;
                button22.Visible = false;
            }
            if (label9.Text != "")
            {
                mr.ForeColor = Color.White;
                mc.ForeColor = Color.White;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

            memory.Insert(0, decimal.Parse(label1.Text));
            if (!isbook)
            {
                feelMemoryText();
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (!isbook) memory.Clear();
            else { book.Clear(); }
            label8.Text = "";
            label9.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label11.Text = "";
            label18.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 1) { memory[0] -= decimal.Parse(label1.Text); label9.Text = memory[0].ToString(); }
            else { memory.Add(0 - decimal.Parse(label1.Text)); label9.Text = memory[0].ToString(); }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 1) { memory[0] += decimal.Parse(label1.Text); label9.Text = memory[0].ToString(); }
            else { memory.Add(decimal.Parse(label1.Text)); label9.Text = memory[0].ToString(); }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 1) memory.RemoveAt(0);
            feelMemoryText();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            label1.Text = (decimal.Parse(label1.Text)*(-1)).ToString();
        }

        private void mc2_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 2) memory.RemoveAt(1);
            feelMemoryText();
        }

        private void mc3_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 3) memory.RemoveAt(2);
            feelMemoryText();
        }

        private void mc4_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 4) memory.RemoveAt(3);
            feelMemoryText();
        }

        private void mc5_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 5) memory.RemoveAt(4);
            feelMemoryText();
        }

        private void Mp2_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 2) memory[1] += decimal.Parse(label1.Text);
            label13.Text = memory[1].ToString();
        }

        private void Mp3_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 3) memory[2] += decimal.Parse(label1.Text);
            label15.Text = memory[2].ToString();
        }

        private void Mp4_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 4) memory[3] += decimal.Parse(label1.Text);
            label17.Text = memory[3].ToString();
        }

        private void Mp5_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 5) memory[4] += decimal.Parse(label1.Text);
            label18.Text = memory[4].ToString();
        }

        private void Mm2_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 2) memory[1] -= decimal.Parse(label1.Text);
            label13.Text = memory[1].ToString();
        }

        private void Mm3_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 3) memory[2] -= decimal.Parse(label1.Text);
            label15.Text = memory[2].ToString();
        }

        private void Mm4_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 4) memory[3] -= decimal.Parse(label1.Text);
            label17.Text = memory[3].ToString();
        }

        private void Mm5_Click(object sender, EventArgs e)
        {
            if (memory.Count() >= 5) memory[4] -= decimal.Parse(label1.Text);
            label18.Text = memory[4].ToString();
        }

        private void label13_TextChanged(object sender, EventArgs e)
        {
            if (label13.Text == "")
            {
                mc2.Visible = false;
                Mm2.Visible = false;
                Mp2.Visible = false;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (label15.Text != "" && !isbook)
            {
                changeShowForBtn();
                Mp3.Visible = true;
                mc3.Visible = true;
                Mm3.Visible = true;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (label7.Text != "" && !isbook)
            {
                changeShowForBtn();
                Mp4.Visible = true;
                Mm4.Visible = true;
                mc4.Visible = true;
            }
        }

        private void label18_TextChanged(object sender, EventArgs e)
        {
            if (label18.Text != "")
            {
                mc5.Visible = false;
                Mm5.Visible = false;
                Mp5.Visible = false;
            }
        }
        void changeShowForBtn()
        {
            button27.Visible = false;
            button23.Visible = false;
            button22.Visible = false;
            Mp2.Visible = false;
            mc2.Visible = false;
            Mm2.Visible = false;
            Mp3.Visible = false;
            mc3.Visible = false;
            Mm3.Visible = false;
            Mp4.Visible = false;
            mc4.Visible = false;
            Mm4.Visible = false;
            Mp5.Visible = false;
            mc5.Visible = false;
            Mm5.Visible = false;
        }
        private void label9_Click(object sender, EventArgs e)
        {
            if (label9.Text != "" && !isbook)
            {
                changeShowForBtn();
                button27.Visible = true;
                button23.Visible = true;
                button22.Visible = true;

            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (label13.Text != "" && !isbook)
            {
                changeShowForBtn();
                Mp2.Visible = true;
                mc2.Visible = true;
                Mm2.Visible = true;
            }
        }

        private void label15_TextChanged(object sender, EventArgs e)
        {

            if (label15.Text == "")
            {
                mc3.Visible = false;
                Mm3.Visible = false;
                Mp3.Visible = false;
            }
        }

        private void label17_TextChanged(object sender, EventArgs e)
        {
            if (label17.Text == "")
            {
                mc4.Visible = false;
                Mm4.Visible = false;
                Mp4.Visible = false;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (label18.Text != "" && !isbook)
            {
                changeShowForBtn();
                Mp5.Visible = true;
                mc5.Visible = true;
                Mm5.Visible = true;
            }
        }

        private void label9_DoubleClick(object sender, EventArgs e)
        {
            if (label9.Text != "")
            {
                label1.Text = label9.Text;
            }
        }

        private void label13_DoubleClick(object sender, EventArgs e)
        {
            if (label13.Text != "")
            {
                label1.Text = label13.Text;
            }
        }

        private void label15_DoubleClick(object sender, EventArgs e)
        {
            if (label15.Text != "")
            {
                label1.Text = label15.Text;
            }
        }

        private void label17_DoubleClick(object sender, EventArgs e)
        {
            if (label17.Text != "")
            {
                label1.Text = label17.Text;
            }
        }

        private void label18_DoubleClick(object sender, EventArgs e)
        {
            if (label18.Text != "")
            {
                label1.Text = label18.Text;
            }
        }

        private void mr_Click(object sender, EventArgs e)
        {
            if (mr.ForeColor == Color.White)
            {
                label9_DoubleClick(sender, e);
            }
        }

        private void mc_Click(object sender, EventArgs e)
        {
            if (mr.ForeColor == Color.White)
            {
                memory.Clear();
                if (!isbook)
                {
                    label9.Text = "";
                    label13.Text = "";
                    label15.Text = "";
                    label17.Text = "";
                    label18.Text = "";
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = "memory.txt";
            File.WriteAllText(path, "");
            string b = "";
            foreach (decimal a in memory)
            {
                b += a.ToString() + " ";
            }
            File.WriteAllText(path, b);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '=':
                    button16_Click(sender, e);break;
                case '+':
                    button2_Click(sender, e);
                    break;
                case '-':
                    button8_Click(sender, e);
                    break;
                case '*':
                    button12_Click(sender, e); break;
                case '/':
                    button24_Click(sender, e);break;
                case '%':
                    button19_Click(sender, e);break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    dii(e.KeyChar);
                    break;
                default:
                    break;
            }

        }

    }
}

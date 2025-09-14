using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Calc
{
    internal class Func
    {
        public static decimal? Delit(decimal? a, decimal? b)
        {
            try
            {
                if (b == 0)
                {
                    MessageBox.Show("Помилка: ділення на нуль!", "Помилка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return a / b;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message, "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        public static decimal? Kor(decimal? a)
        {
            if (a < 0)
            {
                MessageBox.Show("Помилка: неможливо обчислити квадратний корінь із від’ємного числа!",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }

            return (decimal)Math.Sqrt((double)a);
        }
        public static decimal? Drob(decimal? a) {
            try
            {
                if (a == 0)
                {
                    MessageBox.Show("Помилка: ділення на нуль!", "Помилка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return 1 / a;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message, "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}

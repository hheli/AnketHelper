using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnketHelper
{
    public static class CheckerINN
    {
        // взято прям отсюда http://www.iu5bmstu.ru/index.php/%D0%9F%D1%80%D0%BE%D0%B2%D0%B5%D1%80%D0%BA%D0%B0_%D0%98%D0%9D%D0%9D,_%D0%9A%D0%9F%D0%9F_%D0%B8_%D0%9E%D0%93%D0%A0%D0%9D

        /// <summary>
        /// проверка ИНН по контрольной цифре
        /// </summary>
        /// <param name="INNstring">ИНН для проверки</param>
        /// <returns>true - если проходит проверку, false - если не проходит проверку</returns>
        public static bool check_INN(string INNstring)
        {
            // является ли вообще числом
            try { Int64.Parse(INNstring); } catch { return false; }

            // проверка на 10 и 12 цифр
            if (INNstring.Length != 10 && INNstring.Length != 12) { return false; }

            // проверка по контрольным цифрам
            if (INNstring.Length == 10) // для юридического лица
            {
                int dgt10 = 0;
                try
                {
                    dgt10 = (((2 * Int32.Parse(INNstring.Substring(0, 1))
                        + 4 * Int32.Parse(INNstring.Substring(1, 1))
                        + 10 * Int32.Parse(INNstring.Substring(2, 1))
                        + 3 * Int32.Parse(INNstring.Substring(3, 1))
                        + 5 * Int32.Parse(INNstring.Substring(4, 1))
                        + 9 * Int32.Parse(INNstring.Substring(5, 1))
                        + 4 * Int32.Parse(INNstring.Substring(6, 1))
                        + 6 * Int32.Parse(INNstring.Substring(7, 1))
                        + 8 * Int32.Parse(INNstring.Substring(8, 1))) % 11) % 10);
                }
                catch { return false; }

                if (Int32.Parse(INNstring.Substring(9, 1)) == dgt10) { return true; }
                else { return false; }
            }
            else // для физического лица
            {
                int dgt11 = 0, dgt12 = 0;
                try
                {
                    dgt11 = (((
                        7 * Int32.Parse(INNstring.Substring(0, 1))
                        + 2 * Int32.Parse(INNstring.Substring(1, 1))
                        + 4 * Int32.Parse(INNstring.Substring(2, 1))
                        + 10 * Int32.Parse(INNstring.Substring(3, 1))
                        + 3 * Int32.Parse(INNstring.Substring(4, 1))
                        + 5 * Int32.Parse(INNstring.Substring(5, 1))
                        + 9 * Int32.Parse(INNstring.Substring(6, 1))
                        + 4 * Int32.Parse(INNstring.Substring(7, 1))
                        + 6 * Int32.Parse(INNstring.Substring(8, 1))
                        + 8 * Int32.Parse(INNstring.Substring(9, 1))) % 11) % 10);
                    dgt12 = (((
                        3 * Int32.Parse(INNstring.Substring(0, 1))
                        + 7 * Int32.Parse(INNstring.Substring(1, 1))
                        + 2 * Int32.Parse(INNstring.Substring(2, 1))
                        + 4 * Int32.Parse(INNstring.Substring(3, 1))
                        + 10 * Int32.Parse(INNstring.Substring(4, 1))
                        + 3 * Int32.Parse(INNstring.Substring(5, 1))
                        + 5 * Int32.Parse(INNstring.Substring(6, 1))
                        + 9 * Int32.Parse(INNstring.Substring(7, 1))
                        + 4 * Int32.Parse(INNstring.Substring(8, 1))
                        + 6 * Int32.Parse(INNstring.Substring(9, 1))
                        + 8 * Int32.Parse(INNstring.Substring(10, 1))) % 11) % 10);
                }
                catch { return false; }
                if (Int32.Parse(INNstring.Substring(10, 1)) == dgt11
                    && Int32.Parse(INNstring.Substring(11, 1)) == dgt12) { return true; }
                else { return false; }
            }
        }
    }
}

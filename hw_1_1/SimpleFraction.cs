using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_1
{
    public sealed class SimpleFraction : IComparable
    {
        private long _numerator;
        private long _denominator;

        public long Numerator => _numerator;
        public long Denominator => _denominator;

        public SimpleFraction()
        {
            _numerator = 0;
            _denominator = 1;
        }

        public SimpleFraction(int denominator) : this()
        {
            if (denominator == 0)
                throw new DivideByZeroException("Попытка создать дробь с нулевым знаменателем!!!");
        }

        public SimpleFraction(int numerator, int denominator) : this()
        {
            if (denominator == 0)
                throw new DivideByZeroException("Попытка создать дробь с нулевым знаменателем!!!");
            else if (numerator != 0)
            {
                int divider = GetMaxCommonDivider(Math.Abs(numerator), Math.Abs(denominator));
                _numerator = (numerator / divider) * (denominator > 0 ? 1 : -1);
                _denominator = Math.Abs(denominator) / divider;
            }
        }

        private SimpleFraction(long numerator, long denominator) : this()
        {
            if (denominator == 0)
                throw new DivideByZeroException("Попытка создать дробь с нулевым знаменателем!!!");
            else if (numerator != 0)
            {
                long divider = GetMaxCommonDivider(Math.Abs(numerator), Math.Abs(denominator));
                _numerator = (numerator / divider) * (denominator > 0 ? 1 : -1);
                _denominator = Math.Abs(denominator) / divider;
            }
        }

        private static int GetMaxCommonDivider(int number1, int number2)
        {
            while (number1 != number2)
            {
                if (number1 > number2)
                    number1 -= number2;
                else
                    number2 -= number1;
            }
            return number1;
        }

        private static long GetMaxCommonDivider(long number1, long number2)
        {
            while (number1 != number2)
            {
                if (number1 > number2)
                    number1 -= number2;
                else
                    number2 -= number1;
            }
            return number1;
        }

        private static int GetMinCommonMultiple(int number1, int number2)
        {
            try
            {
                return (int)(((long)(number1 * number2)) / GetMaxCommonDivider(number1, number2));
            }
            catch(Exception)
            {
                throw new ArithmeticException("Ошибка в вычислениях!!!");
            }
        }

        private static long GetMinCommonMultiple(long number1, long number2)
        {
            try
            {
                return (number1 * number2) / GetMaxCommonDivider(number1, number2);
            }
            catch(Exception)
            {
                throw new ArithmeticException("Ошибка в вычислениях!!!");
            }
        }

        public static SimpleFraction GetSum(SimpleFraction fraction1, SimpleFraction fraction2)
        {
            try
            {
                SimpleFraction result;
                if (fraction1.Denominator == fraction2.Denominator)
                    result = new SimpleFraction(fraction1.Numerator + fraction2.Numerator, fraction1.Denominator);
                else
                {
                    long multiple = GetMinCommonMultiple(fraction1.Denominator, fraction2.Denominator);
                    long newNumerator1 = fraction1.Numerator * (multiple / fraction1.Denominator);
                    long newNumerator2 = fraction2.Numerator * (multiple / fraction2.Denominator);
                    result = new SimpleFraction(newNumerator1 + newNumerator2, multiple);
                }
                return result;
            }
            catch(ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Числитель и/или знаменатель выходит за границы диапазона для целых чисел!!!");
            }
            catch (ArithmeticException)
            {
                throw;
            }
            catch(Exception)
            {
                throw new Exception("Произошла какая-то ошибка!!!");
            }
        }

        public static SimpleFraction GetDifference(SimpleFraction fraction1, SimpleFraction fraction2)
        {
            try
            {
                var temp = new SimpleFraction(-fraction2.Numerator, fraction2.Denominator);
                return SimpleFraction.GetSum(fraction1, temp);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static SimpleFraction GetComposition(SimpleFraction fraction1, SimpleFraction fraction2)
        {
            try
            {
                SimpleFraction result;
                var temp1 = new SimpleFraction(fraction1.Numerator, fraction2.Denominator);
                var temp2 = new SimpleFraction(fraction2.Numerator, fraction1.Denominator);
                result = new SimpleFraction(temp1.Numerator * temp2.Numerator, temp1.Denominator * temp2.Denominator);
                return result;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Числитель и/или знаменатель выходит за границы диапазона для целых чисел!!!");
            }
            catch (ArithmeticException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Произошла какая-то ошибка!!!");
            }
        }

        public static SimpleFraction GetQuotient(SimpleFraction fraction1, SimpleFraction fraction2)
        {
            try
            {
                var temp = new SimpleFraction(fraction2.Denominator, fraction2.Numerator);
                return SimpleFraction.GetComposition(fraction1, temp);
            }
            catch (DivideByZeroException)
            {
               throw new DivideByZeroException("Попытка поделить на дробь с нулевым знаменателем!!!");
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int CompareTo(object obj)
        {
            try
            {
                if (obj == null)
                    return 1;
                var temp = obj as SimpleFraction;
                long newNumerator1, newNumerator2;
                if (temp.Denominator == this.Denominator)
                {
                    newNumerator1 = this.Numerator;
                    newNumerator2 = temp.Numerator;
                }
                else
                {
                    long multiple = GetMinCommonMultiple(this.Denominator, temp.Denominator);
                    newNumerator1 = this.Numerator * (multiple / this.Denominator);
                    newNumerator2 = temp.Numerator * (multiple / temp.Denominator);
                }
                if (newNumerator1 > newNumerator2)
                    return 1;
                else if (newNumerator1 < newNumerator2)
                    return -1;
                else
                    return 0;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Числитель и/или знаменатель выходит за границы диапазона для целых чисел!!!");
            }
            catch (ArithmeticException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Произошла какая-то ошибка!!!");
            }
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}", _numerator, _denominator);
        }
    }
}

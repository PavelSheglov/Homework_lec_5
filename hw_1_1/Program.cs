using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите числитель первой дроби:");
                var numerator1 = Console.ReadLine();
                Console.Write("Введите знаменатель первой дроби:");
                var denominator1 = Console.ReadLine();
                Console.Write("Введите числитель второй дроби:");
                var numerator2 = Console.ReadLine();
                Console.Write("Введите знаменатель второй дроби:");
                var denominator2 = Console.ReadLine();
                
                var n1 = new SimpleFraction(Convert.ToInt32(numerator1), Convert.ToInt32(denominator1));
                Console.WriteLine("Дробь №1:{0}", n1.ToString());
                var n2 = new SimpleFraction(Convert.ToInt32(numerator2), Convert.ToInt32(denominator2));
                Console.WriteLine("Дробь №2:{0}", n2.ToString());

                Console.WriteLine("Сумма дробей: {0}", SimpleFraction.GetSum(n1,n2));
                Console.WriteLine("Разность дробей: {0}", SimpleFraction.GetDifference(n1, n2));
                Console.WriteLine("Произведение дробей: {0}", SimpleFraction.GetComposition(n1, n2));
                Console.WriteLine("Частное дробей: {0}", SimpleFraction.GetQuotient(n1, n2));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}


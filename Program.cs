using System;
using System.Collections.Specialized;

namespace BaseTypesAndConstr
{
    struct MethodParams
    {
        public int maxParams;
        public int minParams;
        public int overloadsNumber;
        public MethodParams(int maxParams, int minParams, int overloadsNumber):this()
        {
            this.maxParams = maxParams;
            this.minParams = minParams;
            this.overloadsNumber = overloadsNumber;
        }
    }

    public partial class Program
    {
        //Выполнил Тускаев Александр, группа 3530203/00102, вариант 2
        public static void ChangeConsoleView()
        {
            Console.Clear();
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            while (true)
            {
                Console.WriteLine("В данном меню можно раскрасить консоль.\n" +
                             "Что хотите раскрасить?\n" +
                             "1 - Шрифт\n" +
                             "2 - Задний фон\n" +
                             "0 - Выход в главное меню\n");
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': 
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Выберите цвет для ракраски шрифта:");
                                for (int i = 0; i < colors.Length; i++)
                                {
                                    Console.WriteLine("{0} - {1}", i + 1, colors[i]);
                                }
                                Console.WriteLine("0 - Назад");
                                Console.Write(">>> ");
                                string key = Console.ReadLine();
                                if (key != "0")
                                {
                                    try
                                    {
                                        Console.ForegroundColor = colors[Int32.Parse(key) - 1];
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Такого цвета нет! Попробуйте еще раз.");
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }; 
                        break;
                    case '2': 
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Выберите цвет для ракраски фона:");
                                for (int i = 0; i < colors.Length; i++)
                                {
                                    Console.WriteLine("{0} - {1}", i + 1, colors[i]);
                                }
                                Console.WriteLine("0 - Назад");
                                Console.Write(">>> ");
                                string key = Console.ReadLine();
                                if (key != "0")
                                {
                                    try
                                    {
                                        Console.BackgroundColor = colors[Int32.Parse(key) - 1];
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Такого цвета нет! Попробуйте еще раз.");
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }; 
                        break;
                    default:
                        break;
                }
                Console.Clear();
                break;
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
               Console.WriteLine("Информация по типам:\n" +
                                  "1 - Общая информация по типам\n" +
                                  "2 - Выбрать тип из списка\n" +
                                  "3 - Параметры консоли\n" +
                                  "0 - Выход из программы");
                Console.WriteLine();
                char key = char.ToLower(Console.ReadKey(true).KeyChar);
                if ( key == '0')
                {
                    break;
                }
                else
                {
                    switch (key)
                    {
                        case '1': ShowAllTypeInfo()
                                ; break;
                        case '2': SelectTypeForInfo(); break;
                        case '3': ChangeConsoleView(); break;
                        default:
                            break;
                    }
                }
                Console.Clear();
                
            }
        }
    }
}

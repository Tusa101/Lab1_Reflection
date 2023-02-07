using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BaseTypesAndConstr
{
    struct MethodParams
    {
        private int _maxParams;
        private int _minParams;
        private int _overloadsNumber;
        public MethodParams(int maxParams, int minParams, int overloadsNumber):this()
        {
            this._maxParams = maxParams;
            this._minParams = minParams;
            this._overloadsNumber = overloadsNumber;
        }
        public void SetOverloads(int newOverload)
        {
            this._overloadsNumber = newOverload;
        }
        public void SetMaxParams(int newMaxParams)
        {
            this._maxParams = newMaxParams;
        }
        public void SetMinParams(int newMinParams)
        {
            this._minParams = newMinParams; 
        }
        public int GetOverloads()
        {
            return this._overloadsNumber;
        }
        public int GetMaxParams()
        {
            return this._maxParams;
        }
        public int GetMinParams()
        {
            return _minParams;
        }
    }

    internal class Program
    {
        //Выполнил Тускаев Александр, группа 3530203/00102
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
        public static void ShowAllTypeInfo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Информация по типам\n" +
                                  "Выберите тип:\n" +
                                  "----------------------------------------");
                string[] typesShorthandsArray = { "byte", "sbyte", "short", "int", "long", "ushort",
                                              "uint", "ulong", "float", "double", "bool", "char",
                                              "decimal", "IntPtr", "UIntPtr" };
                for (int i = 0; i < typesShorthandsArray.Length; i++)
                {
                    Console.WriteLine("\t{0} - {1}", i + 1, typesShorthandsArray[i]);
                }
                Console.WriteLine("\t0 - Выход в главное меню");
                Console.Write(">>> ");
                Type[] types = { typeof(byte), typeof(sbyte), typeof(short), typeof(int), typeof(long),
                             typeof(ushort), typeof(uint), typeof(ulong), typeof(float), typeof(double),
                             typeof(bool), typeof(char), typeof(decimal), typeof(IntPtr), typeof(UIntPtr)};
                string unformattedKey = Console.ReadLine();
                if (unformattedKey == "0")
                {
                    Console.Clear();
                    break;
                }
                int key = 0;
                if (Int32.TryParse(unformattedKey, out key))
                {
                    key = Int32.Parse(unformattedKey);
                    Console.WriteLine("Информация по типу: {0}", types[key - 1]);
                    Console.WriteLine("Значимый тип: {0}", types[key-1].IsValueType ? '+': '-');
                    Console.WriteLine("Пространство имен: {0}", types[key - 1].Namespace);
                    Console.WriteLine("Сборка: {0}", types[key - 1].Assembly.GetName().Name);
                    Console.WriteLine("Общее число элементов: {0}", types[key - 1].GetMethods().Length + 
                                                                    types[key - 1].GetProperties().Length + 
                                                                    types[key - 1].GetFields().Length);
                    Console.WriteLine("Число методов: {0}", types[key - 1].GetMethods().Length);
                    Console.WriteLine("Число свойств: {0}", types[key - 1].GetProperties().Length);
                    Console.WriteLine("Число полей: {0}", types[key - 1].GetFields().Length);

                    List<string> membersNames = new List<string>();
                    for (int i = 0; i < types[key-1].GetFields().Length; i++)
                    {
                        membersNames.Add(types[key - 1].GetFields()[i].Name);
                    }
                    Console.WriteLine("Список полей: {0}", String.Join(", ", membersNames)=="" ? "-" : String.Join(", ", membersNames));
                    membersNames.Clear();

                    for (int i = 0; i < types[key-1].GetProperties().Length; i++)
                    {
                        membersNames.Add(types[key - 1].GetProperties()[i].Name);
                    }
                    Console.WriteLine("Список свойств: {0}", String.Join(", ", membersNames) == "" ? "-" : String.Join(", ", membersNames));
                    Console.WriteLine("Нажмите 'M' для вывода дополнительной информации по методам");
                    Console.WriteLine("Нажмите '0' для выхода в главное меню");

                    if (char.ToUpper(Console.ReadKey(true).KeyChar) == 'M')
                    {
                        Console.Clear();
                        Dictionary<string, MethodParams> methods = new Dictionary<string, MethodParams>();
                        MethodInfo[] allTypesMethods = types[key - 1].GetMethods();
                        foreach (MethodInfo m in allTypesMethods) 
                        {
                            Console.WriteLine(m.Name);
                            if (methods.ContainsKey(m.Name))
                            {
                                int e = methods[m.Name].GetOverloads() + 1;
                                methods[m.Name].SetOverloads(e) ;
                            }
                            else
                            {
                                methods.Add(m.Name, new MethodParams(m.GetParameters().Length, m.GetParameters().Length, 1));
                            }
                        }
                        int maxLength = 0;
                        foreach (var item in methods)
                        {
                            if(item.Key.Length > maxLength)
                                maxLength = item.Key.Length;
                        }
                        var headerSB = new StringBuilder("Название:");
                        for (int i = headerSB.Length; i < maxLength+3; i++)
                        {
                            headerSB.Append(" ");
                        }
                        headerSB.Append("Число перегрузок");
                        Console.WriteLine(headerSB.ToString());
                        var rowMethod = new StringBuilder();
                        foreach (var item in methods)
                        {
                            rowMethod.Append(item.Key);
                            for (int i = item.Key.Length; i < maxLength + 3; i++)
                            {
                                rowMethod.Append(" ");
                            }
                            rowMethod.Append(item.Value.GetOverloads());
                            Console.WriteLine(rowMethod.ToString());
                            rowMethod.Clear();
                        }
                    }
                    if (char.ToLower(Console.ReadKey(true).KeyChar) == '0')
                        break;
                }
                else
                {
                    Console.WriteLine("Неверный номер, попробуйте снова.");
                    Console.Clear();
                }
            }
        }
        public static void SelectType()
        {
            Console.Clear();
            Console.WriteLine("Nothing found here.");
            Console.Clear();
        }
        public static void GetVariant()
        {
            Console.Clear();
            char F = 'T';
            char N = 'A';
            int V = (F + N) % 10;//9th variant
            Console.WriteLine("Тускаев Александр, группа 3530203/00102, вариант {0}",V);
            Console.Clear();
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
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowAllTypeInfo(); break;
                    case '2': SelectType(); break;
                    case '3': ChangeConsoleView(); break;
                    case 'v': GetVariant(); break;
                    default:
                        break;
                }
                if (char.ToLower(Console.ReadKey(true).KeyChar) == '0')
                {
                    break;
                }
            }
        }
    }
}

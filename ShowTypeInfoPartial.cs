using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace BaseTypesAndConstr
{
    public partial class Program
    {
        public static void SelectTypeForInfo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Информация по типам\n" +
                                  "Выберите тип:\n" +
                                  "----------------------------------------");
                string[] typesShorthandsArray = { "byte", "sbyte", "short", "int", "long", "ushort",
                                              "uint", "ulong", "float", "double", "bool", "char",
                                              "decimal", "IntPtr", "UIntPtr", "Matrix", "Vector" };
                for (int i = 0; i < typesShorthandsArray.Length; i++)
                {
                    Console.WriteLine("\t{0} - {1}", i + 1, typesShorthandsArray[i]);
                }
                Console.WriteLine("\t0 - Выход в главное меню");
                Console.Write(">>> ");
                Type[] types = { typeof(byte), typeof(sbyte), typeof(short), typeof(int), typeof(long),
                             typeof(ushort), typeof(uint), typeof(ulong), typeof(float), typeof(double),
                             typeof(bool), typeof(char), typeof(decimal), typeof(IntPtr), typeof(UIntPtr), typeof(Matrix), typeof(Vector)};
                string unformattedKey = Console.ReadLine();
                if (unformattedKey == "0")
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
                int key = 0;
                if (Int32.TryParse(unformattedKey, out key))
                {
                    key = Int32.Parse(unformattedKey);
                    if (ShowSelectedTypeInfo(types, key) == 0)
                        break;
                }
                else
                {
                    Console.WriteLine("Неверный номер, попробуйте снова.");
                    Console.Clear();
                }
                
            }
        }

        private static int ShowSelectedTypeInfo(Type[] types, int key)
        {
            Console.WriteLine("Информация по типу: {0}", types[key - 1]);
            Console.WriteLine("Значимый тип: {0}", types[key - 1].IsValueType ? '+' : '-');
            Console.WriteLine("Пространство имен: {0}", types[key - 1].Namespace);
            Console.WriteLine("Сборка: {0}", types[key - 1].Assembly.GetName().Name);
            Console.WriteLine("Общее число элементов: {0}", types[key - 1].GetMethods().Length +
                                                            types[key - 1].GetProperties().Length +
                                                            types[key - 1].GetFields().Length);
            Console.WriteLine("Число методов: {0}", types[key - 1].GetMethods().Length);
            Console.WriteLine("Число свойств: {0}", types[key - 1].GetProperties().Length);
            Console.WriteLine("Число полей: {0}", types[key - 1].GetFields().Length);

            List<string> membersNames = new List<string>();
            for (int i = 0; i < types[key - 1].GetFields().Length; i++)
            {
                membersNames.Add(types[key - 1].GetFields()[i].Name);
            }
            Console.WriteLine("Список полей: {0}", String.Join(", ", membersNames) == "" ? "-" : String.Join(", ", membersNames));
            membersNames.Clear();

            for (int i = 0; i < types[key - 1].GetProperties().Length; i++)
            {
                membersNames.Add(types[key - 1].GetProperties()[i].Name);
            }
            Console.WriteLine("Список свойств: {0}", String.Join(", ", membersNames) == "" ? "-" : String.Join(", ", membersNames));
            Console.WriteLine("Нажмите 'M' для вывода дополнительной информации по методам");
            Console.WriteLine("Нажмите '0' для выхода в главное меню");
            char keyNext = char.ToUpper(Console.ReadKey(true).KeyChar);
            if (keyNext == 'M')
            {
                Console.Clear();
                ShowMethodsInfo(types, key);
            }
            else if (keyNext == '0')
                return 0;
            return 1;
        }

        private static void ShowMethodsInfo(Type[] types, int key)
        {
            Dictionary<string, MethodParams> methods = new Dictionary<string, MethodParams>();
            MethodInfo[] allTypesMethods = types[key - 1].GetMethods();
            foreach (MethodInfo m in allTypesMethods)
            {
                if (methods.ContainsKey(m.Name))
                {
                    methods[m.Name] = new MethodParams
                    {
                        maxParams = methods[m.Name].maxParams,
                        minParams = methods[m.Name].minParams,
                        overloadsNumber = methods[m.Name].overloadsNumber + 1
                    };
                }
                else
                {
                    int maxParamsCnt = int.MinValue;
                    int minParamsCnt = int.MaxValue;
                    foreach (var item in allTypesMethods)
                    {
                        if (item.Name == m.Name)
                        {
                            if (item.GetParameters().Length > maxParamsCnt)
                                maxParamsCnt = item.GetParameters().Length;
                            if (item.GetParameters().Length < minParamsCnt)
                                minParamsCnt = item.GetParameters().Length;
                        }
                    }
                    methods.Add(m.Name, new MethodParams(maxParamsCnt, minParamsCnt, 1));
                }
            }
            Console.WriteLine($"Методы типа {types[key - 1]}"); ;
            int maxLength = 0;
            foreach (var item in methods)
            {
                if (item.Key.Length > maxLength)
                    maxLength = item.Key.Length;
            }
            StringBuilder headerSB = new StringBuilder("Название");
            headerSB.Append(' ', Math.Abs(maxLength - headerSB.Length + 3));
            int overloadsPos = headerSB.Length + 1;
            headerSB.Append("Число перегрузок");
            headerSB.Append(' ', 3);
            int paramsPos = headerSB.Length + 1;
            headerSB.Append("Число параметров");
            Console.WriteLine(headerSB.ToString());
            var rowMethod = new StringBuilder();
            int i = 0;
            foreach (var item in methods)
            {
                rowMethod.Append(i + ". ");
                rowMethod.Append(item.Key);
                rowMethod.Append(' ', overloadsPos - item.Key.Length);
                rowMethod.Append(item.Value.overloadsNumber);
                rowMethod.Append(' ', paramsPos - rowMethod.Length);
                if (item.Value.minParams != item.Value.maxParams)
                {
                    rowMethod.Append($"{item.Value.minParams}..{item.Value.maxParams}");
                }
                else
                {
                    rowMethod.Append($"{item.Value.maxParams}");
                }
                Console.WriteLine(rowMethod.ToString());
                rowMethod.Clear();
                i++;
            }
            int methodsAndOverloads = 0;
            int parameters = 0;
            foreach (var item in methods)
            {
                methodsAndOverloads += methods[item.Key].overloadsNumber;
            }
            foreach (var item in allTypesMethods)
            {
                parameters += item.GetParameters().Length;
            }
            Console.WriteLine($"Общее число методов с перегрузками: {methodsAndOverloads}");
            Console.WriteLine($"Общее число параметров: {parameters}");
            Console.WriteLine("Нажмите любую кнопку, чтобы вернуться...");
            Console.ReadKey();
        }
    }
}

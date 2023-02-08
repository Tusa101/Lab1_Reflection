using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BaseTypesAndConstr
{
     partial class Program
    {
        public static void ShowAllTypeInfo()
        {
//            Самое длинное название свойства
//            Тип с наибольшим числом полей
            Console.Clear();
            Console.WriteLine("Общая информация по типам");
            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            Console.WriteLine($"Подключенные сборки: {refAssemblies.Length}");
            int cnt = 0;
            foreach (Assembly assembly in refAssemblies)
            {
                var tempTypesArr = assembly.GetTypes();
                foreach (var item in tempTypesArr)
                {
                    cnt++;
                }
            }
            Console.WriteLine($"Всего типов по всем подключенным сборкам: {cnt}");
            cnt = 0;
            foreach (Assembly assembly in refAssemblies)
            {
                var tempTypesArr = assembly.GetTypes();
                foreach (var item in tempTypesArr)
                {
                    if (item.IsClass)
                        cnt++;
                }
            }
            Console.WriteLine($"Ссылочные типы (только классы): {cnt}");
            cnt = 0;
            foreach (Assembly assembly in refAssemblies)
            {
                var tempTypesArr = assembly.GetTypes();
                foreach (var item in tempTypesArr)
                {
                    if (item.IsValueType)
                        cnt++;
                }
            }
            int maxLength = int.MinValue;
            string maxName = string.Empty;
            foreach (Assembly assembly in refAssemblies)
            {
                var tempTypesArr = assembly.GetTypes();
                foreach (var item in tempTypesArr)
                {
                    var tempProps = item.GetProperties();
                    foreach (var property in tempProps)
                    {
                        if(property.Name.Length > maxLength)
                        {
                            maxLength = property.Name.Length;
                            maxName = property.Name;
                        }
                            
                    }
                }
            }
            Console.WriteLine($"Самое длинное назание свойства: {maxName}, длина: {maxLength}");
            Console.WriteLine("Десять свойств с длиной назвния, равной 30 и меньше:");
            int cutter = 0;
            foreach (Assembly assembly in refAssemblies)
            {
                while (cutter < 10)
                {
                    var tempTypesArr = assembly.GetTypes();
                    foreach (var item in tempTypesArr)
                    {
                        var tempProps = item.GetProperties();
                        foreach (var property in tempProps)
                        {
                            if (property.Name.Length < 31 && cutter < 10)
                            {
                                cutter++;
                                Console.WriteLine($"{cutter}. {property.Name}");
                            }
                        }
                    }
                }
            }


            cnt = int.MinValue;
            maxName = string.Empty;
            Type maxType = typeof(int);
            foreach (Assembly assembly in refAssemblies)
            {
                var tempTypesArr = assembly.GetTypes();
                foreach (var item in tempTypesArr)
                {
                    if(item.GetFields().Count() > cnt)
                    {
                         //1.первые 100 имен полей
                         //  2. свойства. КОТОРЫЕ ОГРАН 31 СИМВОЛОВ
                         //   Если много. ТО 10
                         //3. Число метожов плюс перегрузки
                         //   4. Число параметров для типа

                        cnt = item.GetFields().Count();
                        maxName = item.Name;
                        maxType = item;
                    }
                }
            }
            Console.WriteLine($"Тип с наибольшим числом полей: {    maxName}, количество: {cnt}");
            Console.WriteLine("Первые 100 полей самого большого типа: ");

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{i+1}. {maxType.GetFields()[i].Name}");
            }

            Console.WriteLine("Нажмите на любую клавишу, чтобы вернться в главное меню...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NCalc;

namespace DnDigital_1e.ViewModels
{
    public abstract class HandbooksElement(string name, string engName, string source)
    {
        public readonly string Name;
        public string MdName => $"#{Name}";

        public readonly string EngName;
        public string MdEngName => $"*{EngName}*";

        public readonly string Source;
        public string MdSource => $"_**Источник:**{Source}_";
    }
    public class Money(int cp, int sp, int ep, int gp, int pp) //Допилено
    {
        // Вложенный класс
        public class Coin
        {
            // Свойства
            private int value; // Значение
            private readonly string name; // Наименование
            public string Performance => $"{value} {name}"; // Представление монеты (пример: 10 зм)

            // Конструктор
            public Coin(int value, string name)
            {
                this.value = value;
                this.name = name;
            }

            // Методы
            internal void Take(int i) => value += i; // Получить монеты
            internal void Give(int i) => value -= i; // Отдать монеты
            
            // Преобразование типов
            public static implicit operator int(Coin coin) => coin.value; // При обращении к объекту класса, получим value, а не объект (не распространяется на методы и прочее)
        }

        // Свойства
        public Coin cp = new(cp, "мм"); // Медь (мм)     = 0.01 зм
        public Coin sp = new(sp, "см"); // Серебро (см)  = 0.1 зм
        public Coin ep = new(ep, "эм"); // Электрум (эм) = 0.5 зм
        public Coin gp = new(gp, "зм"); // Золото (зм)
        public Coin pp = new(pp, "пм"); // Платина (пм)  = 10 зм
        public double SumGP => cp / 100.0 + cp / 10.0 + ep / 2.0 + gp + 10.0 * pp;  // Сумма денег в золотых
        public string Performance => (new Coin[] { pp, gp, ep, sp, cp }).Where(x => x != 0)
                                                                        .Select(x => x.Performance)
                                                                        .Aggregate((a, b) => $"{a}, {b}"); // Представление стоимости товара (пример: 10 зм, 5 см)
    }
    public class Dice //Допилено
    {
        public class D20
        {
            private readonly int[] result; // Два броска
            public int Result => result[0]; // Первый
            public int Advantage => result.Max(); // Максимальный
            public int Disadvantage => result.Min(); // Минимальный
            public D20() => result = [rnd.Next(20) + 1, rnd.Next(20) + 1];
            public D20(int i) => result = [rnd.Next(20) + 1 + i, rnd.Next(20) + 1 + i];

            public static implicit operator int(D20 d20) => d20.Result;
        }
        public static D20 RollD20() => new(); // Бросок к20 (просто, с преимуществом, с помехой)
        public static D20 RollD20(int i) => new(i); // Бросок к20 с модификатором


        private static readonly Random rnd = new();

        private static readonly Predicate<string> isDice = (string str) => Regex.IsMatch(str, @"^\d+[кdКD]\d+$"); // Предикат, проверяющий удовлетворяет ли строка паттерну
        private static readonly Predicate<string> isMath = (string str) => Regex.IsMatch(str, @"^[\d\s\(\)\+\-\*\/]+$"); // Предикат, проверяющий удовлетворяет ли строка паттерну

        public static (string expression, int result) Roll(string str) // Решаем выражение с дайсами (пример: "1к8 кол. + 3к6 псих. + 4")
        {
            str = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(str, @"\++", "+"), @"\/+", "/"), @"\*+", "*"), @"\-+", "-");
            string[] arrayOfWords = str.Split(' '); // Разделяем строку на слова
            List<string> a = arrayOfWords.Select(x => isDice(x) ? PerformanceRoll(x) : x).ToList(); // Обозначение дайсов перевели в результаты бросков
            string b = string.Join(" ", a); // Представление броска
            string c = string.Join(" ", a.Where(x => isMath(x))); // Математическое выражение
            if (c.Trim() == "") return (b, 0);
            int d = (int)Math.Floor(Convert.ToDouble(new Expression(c).Evaluate())); // Подсчеты математического выражения
            return (b, d);
        }
        private static string PerformanceRoll(string str)
        {
            string[] numsString = str.Split('к');
            var (OfDices, OfFaces) = (int.Parse(numsString[0]), int.Parse(numsString[1]));
            return "(" + string.Join(" + ", Enumerable.Range(1, OfDices).Select(_ => rnd.Next(1, OfFaces + 1))) + ")";
        }

    }
    public class Equipments(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories) : HandbooksElement(Name, EngName, Source)
    {
        private Money _price { get; set; } = price;
        public string Price => _price.Performance;
        public string Weight { get; set; } = Weight;
        public string Description { get; set; } = Description;
        public List<string> Categories { get; set; } = Categories;
    }
    public class Armors(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories,
                  byte ArmorClass, string Type, bool DisadvantageToStealth, string PuttingOn, string TakingOff) : Equipments(Name, EngName, Source, price, Weight, Description, Categories)
    {
        public byte ArmorClass { get; set; } = ArmorClass;
        public string Type = Type;                                 // Легкий, средний, тяжелый
        public string ArmorPerformance => Type switch
        {
            "Легкий" => $"{ArmorClass} + ЛОВ",
            "Средний" => $"{ArmorClass} + ЛОВ (макс. 2)",
            "Тяжелый" => $"{ArmorClass}",   
            _ => throw new NotImplementedException()
        }; // Представление класса брони
        public bool DisadvantageToStealth { get; set; } = DisadvantageToStealth;
        public string PuttingOn { get; set; } = PuttingOn;
        public string TakingOff { get; set; } = TakingOff;
    }
    public class Arms(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories,
                string dices, List<(string, string)> features) : Equipments(Name, EngName, Source, price, Weight, Description, Categories)
    {
        private string _damageDices = dices; // Дайсы урона
        private List<(string name, string description)> _features = features; // Свойства оружия
        public string FeaturesNames => _features.Select(x => x.name).Aggregate((a, b) => a + ", " + b);
    }




    internal class Character
    {

    }
}

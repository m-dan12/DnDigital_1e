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
    public abstract class HandbooksElement
    {
        public readonly string Name;
        public string mdName => $"#{Name}";

        public readonly string EngName;
        public string mdEngName => $"*{EngName}*";

        public readonly string Source;
        public string mdSource => $"_**Источник:**{Source}_";
        public HandbooksElement(string name, string engName, string source)
        {
            Name = name;
            EngName = engName;
            Source = source;
        }
    }
    public class Money //Допилено
    {
        // Вложенный класс
        public class Coin
        {
            // Свойства
            private int value; // Значение
            private string name; // Наименование
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
        public Coin cp; // Медь (мм)     = 0.01 зм
        public Coin sp; // Серебро (см)  = 0.1 зм
        public Coin ep; // Электрум (эм) = 0.5 зм
        public Coin gp; // Золото (зм)
        public Coin pp; // Платина (пм)  = 10 зм
        public double SumGP => cp / 100.0 + cp / 10.0 + ep / 2.0 + gp + 10.0 * pp;  // Сумма денег в золотых
        public string Performance => (new Coin[] { pp, gp, ep, sp, cp }).Where(x => x != 0)
                                                                        .Select(x => x.Performance)
                                                                        .Aggregate((a, b) => $"{a}, {b}"); // Представление стоимости товара (пример: 10 зм, 5 см)
        // Конструкторы
        public Money(int cp, int sp, int ep, int gp, int pp)
        {
            this.cp = new Coin(cp, "мм");
            this.sp = new Coin(sp, "см");
            this.ep = new Coin(ep, "эм");
            this.gp = new Coin(gp, "зм");
            this.pp = new Coin(pp, "пм");
        }
    }
    public class Dice
    {
        public class D20
        {
            private readonly int[] result; // Два броска
            public int Result => result[0]; // Первый
            public int Advantage => result.Max(); // Максимальный
            public int Disadvantage => result.Min(); // Минимальный
            public D20() => result = new int[] { rnd.Next(20) + 1, rnd.Next(20) + 1 };
            public D20(int i) => result = new int[] { rnd.Next(20) + 1 + i, rnd.Next(20) + 1 + i };

            public static implicit operator int(D20 d20) => d20.Result;
        }
        public static D20 RollD20() => new(); // Бросок к20 (просто, с преимуществом, с помехой)
        public static D20 RollD20(int i) => new(i); // Бросок к20 с модификатором


        private static readonly Random rnd = new();

        private static readonly Predicate<string> isDice = (string str) => Regex.IsMatch(str, @"^\d+[кdКD]\d+$"); // Предикат, проверяющий удовлетворяет ли строка паттерну
        private static readonly Predicate<string> isMath = (string str) => Regex.IsMatch(str, @"^[\d\s\(\)\+\-\*\/]+$"); // Предикат, проверяющий удовлетворяет ли строка паттерну

        public static (string expression, int result) Roll(string str) // Решаем выражение с дайсами (пример: "1к8 кол. + 3к6 псих. + 4")
        {
            str = Regex.Replace(Regex.Replace(Regex.Replace(str, @"\++", "+"), @"\/+", "/"), @"\*+", "*");
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
            var num = (OfDices: int.Parse(numsString[0]), OfFaces: int.Parse(numsString[1]));
            return "(" + string.Join(" + ", Enumerable.Range(1, num.OfDices).Select(_ => rnd.Next(1, num.OfFaces + 1))) + ")";
        }

    }
    public class Equipments : HandbooksElement
    {
        private Money price { get; set; }        // Стоимость // Надо реализовать как класс или что-то подобное
        public string Price => price.Performance;
        public string Weight { get; set; }        // Вес
        public string Description { get; set; }   // Описание
        public List<string> Categories { get; set; }
        public Equipments(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories)
            : base(Name, EngName, Source)
        {
            this.price = price;
            this.Weight = Weight;
            this.Description = Description;
            this.Categories = Categories;
        }
    }
    public class Armors : Equipments
    {
        public byte ArmorClass { get; set; }                // Класс доспеха без модификаторов
        public string Type;                                 // Легкий, средний, тяжелый
        public string ArmorPerformance => Type switch
        {
            "Легкий" => $"{ArmorClass} + ЛОВ",
            "Средний" => $"{ArmorClass} + ЛОВ (макс. 2)",
            "Тяжелый" => $"{ArmorClass}",
            _ => throw new NotImplementedException()
        }; // Представление класса брони
        public bool DisadvantageToStealth { get; set; }     // Помеха на скрытность
        public string PuttingOn { get; set; }               // Время надевания
        public string TakingOff { get; set; }               // Время снятия
        public Armors(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories,
                      byte ArmorClass, string Type, bool DisadvantageToStealth, string PuttingOn, string TakingOff)
                      : base(Name, EngName, Source, price, Weight, Description, Categories)
        {
            this.ArmorClass = ArmorClass;
            this.Type = Type;
            this.DisadvantageToStealth = DisadvantageToStealth;
            this.PuttingOn = PuttingOn;
            this.TakingOff = TakingOff;
        }
    }
    public class Arms : Equipments
    {
        private string _damageDices; // Дайсы урона
        private List<(string name, string description)> _features; // Свойства оружия
        public string FeaturesNames => _features.Select(x => x.name).Aggregate((a, b) => a + ", " + b);
        public Arms(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories, 
                    string dices, List<(string, string)> features)
                    : base(Name, EngName, Source, price, Weight, Description, Categories)
        {
            _damageDices = dices;
            _features = features;
        }
    }
    
    
    
    
    internal class Character
    {

    }
    public struct Races { }
    public struct Classes
    {

    }
    public struct Skills
    {
        string name;
        byte numberOfBP; // либо 0, либо 1, либо 2
        int mod; // Характеристика + Количество бонусов мастерства * Бонус мастерства
    }
}

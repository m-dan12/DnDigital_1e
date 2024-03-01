using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Joins;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DnDigital_1e.ViewModels
{
    public class HandbooksElement
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
        // Статичные свойства и методы
        private static readonly Regex regex = new("\\d+(к|d|К|D)\\d+");
        public static int Roll(int numbersOfDices, int numbersOfFaces)
        {
            var rnd = new Random();
            int sum = 0;
            for (int i = 0; i < numbersOfDices; i++) sum += rnd.Next(numbersOfFaces) + 1;
            return sum;
        }

        public static string RollAllDiceInString(string str)
        {
            MatchCollection matches = regex.Matches(str);
            if (matches.Count == 0) return str;
            foreach(Match match in matches) str = str.Replace(match.Value, $"{match.Value} = {(new Dice(match.Value)).Roll()}");
            return str;
        }
        
        // Свойства
        private int numbersOfDices;
        private int numbersOfFaces;
        public string Performance => $"{numbersOfDices}к{numbersOfFaces}";

        // Конструкторы
        public Dice(int numbersOfDices, int numbersOfFaces)
        {
            this.numbersOfDices = numbersOfDices;
            this.numbersOfFaces = numbersOfFaces;
        }
        public Dice(string str)
        {
            String[] numArr = str.Split('к');
            numbersOfDices = int.Parse(numArr[0]);
            numbersOfFaces = int.Parse(numArr[1]);
        }
        
        // Методы
        public int Roll()
        {
            var rnd = new Random();
            int sum = 0;
            for (int i = 0; i < numbersOfDices; i++) sum += rnd.Next(numbersOfFaces) + 1;
            return sum;
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
        private Dice _damageDices; // Дайсы урона
        public string DicePerformance => _damageDices.Performance; // Представление дайсов урона
        private List<(string name, string description)> _features; // Свойства оружия
        public string FeaturesNames => _features.Select(x => x.name).Aggregate((a, b) => a + ", " + b);
        public Arms(string Name, string EngName, string Source, Money price, string Weight, string Description, List<string> Categories, 
                    Dice dices, List<(string, string)> features)
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

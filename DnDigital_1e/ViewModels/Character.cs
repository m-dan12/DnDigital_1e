using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDigital_1e.ViewModels
{
    public class Races { }
    public class Classes { }
    public class Backgroundes { }
    public class Skills { }
    public class Equipments { }
    public class Feats { }
    internal class Character
    {
        /* 1 Этап - Поля, относящиеся к концепции персонажа */
        //Концепция
        private string _concept;
        private string _conceptArt;

        /* 2 Этап - Поля, зависящие от происхождения персонажа */
        // Характеристики
        private int _strength;          // Сила
        private int _dexterity;        // Ловкость
        private int _сonstitution;    // Телосложение
        private int _intelligence;   // Интеллект
        private int _wisdom;        // Мудрость
        private int _charisma;     // Харизма
        // Раса                    \\
        private Races _race;        // Раса
        // Описательные моменты    \\
        private string _name;      // Имя
        private string _eyes;     // Глаза
        private string _skin;    // Кожа
        private string _hair;   // Волосы

        /* 3 Этап - То, чем персонаж занимался в течении жизни */
        private string _quenta;                 // Предыстория текстом
        private Backgroundes _background;      // Выбор предыстории
        private string _height;               // Рост
        private string _weight;              // Вес
        private string _age;                // Возраст
        private string _personalityTraits; // Черты характера
        private string _ideals;           // Иделы
        private string _bonds;           // Привязанности
        private string _flaws;          // Слабости

        /* 4 Этап - То, кем персонаж стал */
        private string _level;                   // Уровень
        private Classes _class;                 // Класс персонажа ?!Добавить возможность выбора опциональных умений
        private List<Skills> _skills;          // Навыки
        private List<Equipments> _equipments; // Снаряжение

        /* 5 Этап - Особые навыки персонажа */
        private List<Feats> feats; //черты
    }
}

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
        private int _strength;          //сила
        private int _dexterity;        //ловкость
        private int _сonstitution;    //телосложение
        private int _intelligence;   //интеллект
        private int _wisdom;        //мудрость
        private int _charisma;     //харизма
        // Раса                    \\
        private Races _race;        //раса
        // Описательные моменты    \\
        private string _name;      //имя
        private string _eyes;     //глаза
        private string _skin;    //кожа
        private string _hair;   //волосы

        /* 3 Этап - То, чем персонаж занимался в течении жизни */
        private string _quenta;                 //предыстория текстом
        private Backgroundes _background;        //выбор предыстории
        private string _height;               //рост
        private string _weight;              //вес
        private string _age;                //возраст
        private string _personalityTraits; //черты характера
        private string _ideals;           //иделы
        private string _bonds;           //привязанности
        private string _flaws;          //слабости

        /* 4 Этап - То, кем персонаж стал */
        private string _level; //уровень
        private Classes _class; //класс персонажа ?!Добавить возможность выбора опциональных умений
        private List<Skills> _skills; //навыки
        private List<Equipments> _equipments; //снаряжение

        /* 5 Этап - Особые навыки персонажа */
        private List<Feats> feats; //черты
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDigital_1e.ViewModels
{
    public class Races { }
    public class Classes { }
    public struct Skills
    {
        string name;
        byte numberOfBP; // либо 0, либо 1, либо 2
        int mod; // Характеристика + Количество бонусов мастерства * Бонус мастерства
    }
    internal class Character
    {
        /* Концепт */
        private string _theConcept;
        private string _conceptArt;

        /* То, кем персонаж был рожден */

        // Характеристики
        private int _strength;
        private int _dexterity;
        private int _сonstitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;

        // Раса
        private Races _class; // Это будет класс расы

        // Описательные моменты
        private string _name;
        private string _eyes;
        private string _skin;
        private string _hair;
        private Dictionary<string name, (int numbersOfBP, int mod)>

        /* То, чем персонаж занимался в течении жизни */






    }
}

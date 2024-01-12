using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {

            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            CurrentHealth = MaxHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}
        public bool HasPriorityEquipement { get; private set; }
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return _baseHealth;
            }
        }
        public void SetMaxHealth(int maxHealth)
        {
            if (maxHealth < 0) throw new ArgumentException();
            _baseHealth = maxHealth;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }


        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                return _baseAttack;
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                return _baseDefense;
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                return _baseSpeed;
            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            else
            {   
                if (s.Power - _baseDefense > 0)
                {
                    CurrentHealth -= s.Power - _baseDefense;
                    if (CurrentHealth <= 0)
                    {
                        CurrentStatus = null;
                        CurrentHealth = 0;
                    }
                }
            }
        }
        /// <summary>
        /// Soigne le personnage
        /// </summary>
        public void Heal(int heal)
        {
            if (heal < 0)
            {
                throw new ArgumentException();
            }
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            CurrentEquipment = newEquipment;
            _baseHealth += newEquipment.BonusHealth;
            _baseAttack += newEquipment.BonusAttack;
            _baseDefense += newEquipment.BonusDefense;
            _baseSpeed += newEquipment.BonusSpeed;
            if (newEquipment.IsPriority)
            {
                HasPriorityEquipement = true;
            }
            else
            {
                HasPriorityEquipement = false;
            }

        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            _baseHealth -= CurrentEquipment.BonusHealth;
            _baseAttack -= CurrentEquipment.BonusAttack;
            _baseDefense -= CurrentEquipment.BonusDefense;
            _baseSpeed -= CurrentEquipment.BonusSpeed;
            CurrentEquipment = null;
            HasPriorityEquipement = false;
        }

    }
}

using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
using UnityEditor;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type


        [Test]
        public void HealMoreThanMaxHP()
        {
            Character pikachu = new Character(100, 100, 100, 100, TYPE.NORMAL);
            pikachu.Heal(1000);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(pikachu.MaxHealth));
        }

        [Test]
        public void HealWithNegativeValue()
        {
            Character pikachu = new Character(100, 100, 100, 100, TYPE.NORMAL);
            Assert.Throws<ArgumentException>(() =>
            {
                pikachu.Heal(-20);
            });
        }

        [Test]
        public void ChangeMaxHealthWithNegativeValue()
        {
            Character pikachu = new Character(100, 100, 100, 100, TYPE.NORMAL);
            Assert.Throws<ArgumentException>(() =>
            {
                pikachu.SetMaxHealth(-20);
            });
        }
        [Test]
        public void ChangeMaxHealthWithSmallerValueThanCurrentHealth()
        {
            Character pikachu = new Character(100, 100, 100, 100, TYPE.NORMAL);
            pikachu.SetMaxHealth(50);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(pikachu.MaxHealth));
        }

        [Test]
        public void FightWithPriority()
        {
            Character pikachu = new Character(100, 1000, 20, 100, TYPE.NORMAL);
            Equipment e = new Equipment(100, 1000, 10, 100, true);
            pikachu.Equip(e); //pikachu a la priorité (200, 2000, 200, 200)

            Character salameche = new Character(100, 5000, 10, 2000, TYPE.NORMAL);
            Equipment f = new Equipment(100, 100, 0, 200, false);
            salameche.Equip(f); // salameche n'a pas la priorité (200, 5100, 200, 2200)

            Skill punch = new Punch();

            Fight fight = new Fight(pikachu, salameche);
            fight.ExecuteTurn(punch, punch);

            Assert.That(pikachu.MaxHealth, Is.EqualTo(200));
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(60));
            Assert.That(pikachu.IsAlive, Is.EqualTo(true));
            Assert.That(salameche.CurrentHealth, Is.EqualTo(40));

            fight.ExecuteTurn(punch, punch);
            Assert.That(salameche.CurrentHealth, Is.EqualTo(0));
            Assert.That(salameche.IsAlive, Is.EqualTo(false));

            Character salameche2 = new Character(100, 5000, 10, 2000, TYPE.NORMAL);
            pikachu.Unequip();
            Fight fight2 = new Fight(pikachu, salameche2);
            fight2.ExecuteTurn(punch, punch);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(10));
            Assert.That(pikachu.IsAlive, Is.EqualTo(true));
            fight2.ExecuteTurn(punch, punch);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(0));

        }

    }
}

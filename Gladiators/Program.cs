namespace Gladiators
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMainMenu();
        }
    }

    class Menu
    {
        private const string MenuChooseGladiators = "1";
        private const string MenuFight = "2";
        private const string MenuExit = "0";
        private const string MenuGladiator1 = "1";
        private const string MenuGladiator2 = "2";
        private const string MenuGladiator3 = "3";
        private const string MenuGladiator4 = "4";
        private const string MenuBack = "0";

        Arena arena = new Arena();
        Fighter fighter = new Fighter();
        Rouge rouge = new Rouge();
        Knight knight = new Knight();
        Cliric cliric = new Cliric();
        Gladiator gladiator1 = new Gladiator(null, 0, 0, 0);
        Gladiator gladiator2 = new Gladiator(null, 0, 0, 0);

        public void ShowMainMenu()
        {
            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine(MenuChooseGladiators + " - Выбор гладиаторов");
                Console.WriteLine(MenuFight + " - Бой!");
                Console.WriteLine(MenuExit + " - Выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuChooseGladiators:
                        ShowFightersMenu();
                        break;

                    case MenuFight:
                        arena.Fight();
                        break;

                    case MenuExit:
                        isExit = true;
                        break;
                }
            }
        }

        public void ShowFightersMenu()
        {
            bool isBack = false;
            string userInput;

            while (isBack == false)
            {
                Console.WriteLine("Список гладиаторов:");

                ShowAllGladiators();

                Console.WriteLine("\nВыберете гладиатора:");
                Console.WriteLine(MenuGladiator1 + " - " + fighter.Name);
                Console.WriteLine(MenuGladiator2 + " - " + rouge.Name);
                Console.WriteLine(MenuGladiator3 + " - " + knight.Name);
                Console.WriteLine(MenuGladiator4 + " - " + cliric.Name);
                Console.WriteLine(MenuBack + " - Назад");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuGladiator1:
                        Fighter fighter = new Fighter();
                        Console.WriteLine("Выбран " + fighter.Name);
                        AddGladiatorToArena(fighter);

                        break;

                    case MenuGladiator2:
                        Rouge rouge = new Rouge();
                        Console.WriteLine("Выбран " + rouge.Name);
                        AddGladiatorToArena(rouge);
                        break;

                    case MenuGladiator3:
                        Console.WriteLine("Выбран " + knight.Name);
                        AddGladiatorToArena(knight);
                        break;

                    case MenuGladiator4:
                        Console.WriteLine("Выбран " + cliric.Name);
                        AddGladiatorToArena(cliric);
                        break;

                    case MenuExit:
                        isBack = true;
                        break;
                }

                if (arena.Gladiators.Count == 2)
                {
                    Console.WriteLine($"Идущие на смерть {arena.Gladiators[0].Name} и {arena.Gladiators[1].Name} приветствуют тебя!");
                    isBack = true;
                }
            }
        }

        private void ShowAllGladiators()
        {
            fighter.ShowStats();
            rouge.ShowStats();
            knight.ShowStats();
            cliric.ShowStats();
        }

        private void AddGladiatorToArena(Gladiator gladiator)
        {
            if (arena.Gladiators.Count == 0)
            {
                gladiator1 = gladiator;
                arena.Gladiators.Add(gladiator1);
            }
            else if (arena.Gladiators.Count == 1)
            {
                gladiator2 = gladiator;
                arena.Gladiators.Add(gladiator2);
            }
        }
    }

    class Arena
    {
        public List<Gladiator> Gladiators = new List<Gladiator>(2);

        public void Fight()
        {
            if (Gladiators.Count == 2)
            {
                Gladiators[0].ShowStats();
                Gladiators[1].ShowStats();

                while (Convert.ToInt32(Gladiators[0].Health) > 0 && Convert.ToInt32(Gladiators[1].Health) > 0)
                {
                    Gladiators[0].TakeDamage(Gladiators[1].Damage());
                    Gladiators[1].TakeDamage(Gladiators[0].Damage());
                    Gladiators[0].ShowCurrentHealth();
                    Gladiators[1].ShowCurrentHealth();
                }
                if (Gladiators[0].Health <= 0 && Gladiators[1].Health > 0)
                {
                    Console.WriteLine(Gladiators[0].Name + " Повержен! Победил " + Gladiators[1].Name + "!");
                    Gladiators.Clear();
                }
                else if (Gladiators[0].Health > 0 && Gladiators[1].Health <= 0)
                {
                    Console.WriteLine(Gladiators[1].Name + " Повержен! Победил " + Gladiators[0].Name + "!");
                    Gladiators.Clear();
                }
                else if (Gladiators[0].Health <= 0 && Gladiators[1].Health <= 0)
                {
                    Console.WriteLine("Все мертвы! Нет победителя.");
                    Gladiators.Clear();
                }
            }
            else
            {
                Console.WriteLine("Для боя нужно 2 гладиатора!");
            }
        }
    }

    class Gladiator
    {
        public string Name { get; private set; }
        protected int _health;
        protected int _hitDamage;
        protected int _baseDamage = 10;
        protected int _armor;

        public Gladiator(string name, int health, int hitDamage, int armor)
        {
            Name = name;
            _health = health;
            _hitDamage = hitDamage;
            _armor = armor;
        }

        public int Health
        {
            get
            {
                return _health;
            }
        }

        public int Damage()
        {
            return _hitDamage;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Гладиатор: {Name}, Здоровье: {_health}, Урон: {_hitDamage}, Броня: {_armor}");
        }

        public void ShowCurrentHealth()
        {
            Console.WriteLine($"{Name} здоровье: {_health}");
        }

        public void TakeDamage(int damage)
        {
            int healthBeforeDamage = _health;

            if (damage < _armor)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health -= damage - _armor;
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }
    }

    class Fighter : Gladiator
    {
        public Fighter(string name = "Fighter Cris", int health = 1000, int hitDamage = 100, int armor = 30) : base(name, health, hitDamage, armor)
        {
        }
    }

    class Rouge : Gladiator
    {
        public Rouge(string name = "Rouge Karl", int health = 1000, int hitDamage = 100, int armor = 0) : base(name, health, hitDamage, armor)
        {
            hitDamage = CriticalHit();
        }

        private int CriticalHit()
        {
            Random random = new Random();
            int critChance = 25;
            double critMultiplier = 1.8;
            int baseDamage = _hitDamage;

            if (random.Next(0, 100) < critChance)
            {
                baseDamage = Convert.ToInt32(Math.Round(baseDamage * critMultiplier));
                Console.WriteLine($"Критический удар!");
            }

            return baseDamage;
        }

        public int Damage()
        {
            return CriticalHit();
        }
    }

    class Knight : Gladiator
    {
        public Knight(string name = "Knight Robert", int health = 1000, int hitDamage = 100, int armor = 70) : base(name, health, hitDamage, armor)
        {
        }

        private int RiseShield()
        {
            Random random = new Random();
            int getArmorChance = 25;
            int addArmor = 20;
            int extraArmor = 0;

            if (random.Next(0, 100) < getArmorChance)
            {
                extraArmor += addArmor;
                Console.WriteLine($"Поднят щит, добавлено {extraArmor} брони!");
            }

            return extraArmor;
        }

        public void TakeDamage(int damage)
        {
            int currentArmor = _armor;
            int healthBeforeDamage = _health;

            currentArmor += RiseShield();

            if (damage < currentArmor)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health -= damage - currentArmor;
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }
    }

    class Cliric : Gladiator
    {
        public Cliric(string name = "Cliric Flatis", int health = 1000, int hitDamage = 80, int armor = 0) : base(name, health, hitDamage, armor)
        {
        }

        private int Heal()
        {
            Random random = new Random();
            int getHealthChance = 10;
            int addHealth = 200;
            int extraHealth = 0;

            if (random.Next(0, 100) < getHealthChance)
            {
                extraHealth += addHealth;
                Console.WriteLine("Применено лечение, восстановлено: " + addHealth + " здоровья!");
            }

            return extraHealth;
        }

        public void TakeDamage(int damage)
        {
            _health += Heal();
            int healthBeforeDamage = _health;

            if (damage < _armor)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health -= damage - _armor;
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }
    }
}



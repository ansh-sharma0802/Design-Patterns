namespace Creational
{
    internal interface IEnemyPrototype
    {
        IEnemyPrototype Clone();
    }

    internal class Enemy : IEnemyPrototype
    {
        private readonly string Type;
        private int Health;
        private readonly double Speed;
        private readonly bool Armored;
        private readonly string Weapon;
        private readonly List<string> Inventory;

        public Enemy(string type,
            int health,
            double speed,
            bool armored,
            string weapon,
            List<string> inventory)
        {
            Type = type;
            Health = health;
            Speed = speed;
            Armored = armored;
            Weapon = weapon;
            //Inventory = new List<string>(inventory);
            Inventory = [.. inventory];
        }

        public IEnemyPrototype Clone()
            => new Enemy(Type, Health, Speed, Armored, Weapon, [.. Inventory]);

        public void SetHealth(int health)
            => Health = health;

        public void AddInventoryItem(string item)
            => Inventory.Add(item);

        public void PrintStats()
        {
            Console.WriteLine($"{Type} [Health: {Health}, Speed: {Speed}, " +
                              $"Armored: {Armored}, Weapon: {Weapon}, " +
                              $"Inventory: [{string.Join(", ", Inventory)}]]");
        }
    }

    internal class EnemyRegistry
    {
        private readonly Dictionary<string, Enemy> Prototypes = [];

        public void Register(string key, Enemy enemyPrototype)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(enemyPrototype);

            if (key.Length == 0)
            {
                throw new ArgumentException("Key cannot be empty.", nameof(key));
            }

            Prototypes.Add(key, enemyPrototype);
        }

        public Enemy Get(string key)
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!Prototypes.TryGetValue(key, out Enemy? prototype) || prototype is null)
            {
                throw new ArgumentException($"No prototype registered for: {key}");
            }

            return (Enemy)prototype.Clone();
        }
    }

    internal class Prototype
    {
        static void Main()
        {
            EnemyRegistry registry = new();

            registry.Register("flying", new Enemy("FlyingEnemy", 100, 12.0, false,
            "Laser", ["Speed Boost"]));

            registry.Register("armored", new Enemy("ArmoredEnemy", 300, 6.0, true,
            "Cannon", ["Shield", "Helmet"]));

            Enemy e1 = registry.Get("flying");
            Enemy e2 = registry.Get("flying");

            e2.SetHealth(80);
            e2.AddInventoryItem("Smoke Bomb");

            Enemy e3 = registry.Get("armored");

            e1.PrintStats();
            e2.PrintStats();
            e3.PrintStats();
        }
    }
}
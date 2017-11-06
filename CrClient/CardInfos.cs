using System.Collections.Generic;
using System.Linq;

namespace CrClient
{
    public class CardInfos
    {
        public static Dictionary<int, string> Cards = new Dictionary<int, string>()
        {
            //Cards...

            {26000000, "Knight" },
            {26000001, "Archers" },
            {26000002, "Goblins" },
            {26000003, "Giant" },
            {26000004, "P.E.K.K.A" },
            {26000005, "Minions" },
            {26000006, "Balloon" },
            {26000007, "Witch" },
            {26000008, "Skeletons" },
            {26000009, "Barbarians" },
            {26000010, "Golem" },
            {26000011, "Valkyrie" },
            {26000012, "Skeleton Army" },
            {26000013, "Bomber" },
            {26000014, "Musketeer" },
            {26000015, "Baby Dragon" },
            {26000016, "Prince" },
            {26000017, "Wizard" },
            {26000018, "Mini P.E.K.K.A" },
            {26000019, "Spear Goblins" },
            {26000020, "Giant Skeleton" },
            {26000021, "Hog Rider" },
            {26000022, "Minion Horde" },
            {26000023, "Ice Wizard" },
            {26000024, "Royal Giant" },
            {26000025, "Guards" },
            {26000026, "Princess" },
            {26000027, "Dark Prince" },
            {26000028, "Three Musketeers" },
            {26000029, "Lava Hound" },
            {26000030, "Ice Spirit" },
            {26000031, "Fire Spirits" },
            {26000032, "Miner" },
            {26000033, "Sparky" },
            {26000034, "Bowler" },
            {26000035, "Lumberjack" },
            {26000036, "Battle Ram" },
            {26000037, "Inferno Dragon" },
            {26000038, "Ice Golem" },
            {26000039, "Mega Minion" },
            {26000040, "Dart Goblin" },
            {26000041, "Goblin Gang" },
            {26000042, "Electro Wizard" },
            {26000043, "Elite Barbarians" },
            //{26000044, "" },
            {26000045, "Executioner" },
            {26000046, "Bandit" },
            //{26000047, "Unknown" },
            {26000048, "Night Witch" },
            {26000049, "Bats" },
            //{26000050, "Unknown" },
            //{26000051, "Unknown" },
            //{26000052, "Unknown" },
            //{26000053, "Unknown" },
            {26000054, "Cannon Cart" },
            {26000055, "Mega Knight" },

            //Buildings...

            {27000000, "Cannon" },
            {27000001, "Furnace" },
            {27000002, "Morter" },
            {27000003, "Inferno Tower" },
            {27000004, "Bomb Tower" },
            {27000005, "Barbarian Hut" },
            {27000006, "Hidden Tesla" },
            {27000007, "Elixir Collector" },
            {27000008, "X-Bow" },
            {27000009, "Tombstone" },
            {27000010, "Furnace" },

            //Spells

            {28000000, "Fireball" },
            {28000001, "Arrows" },
            {28000002, "Rage" },
            {28000003, "Rocket" },
            {28000004, "Goblin Barrel" },
            {28000005, "Freeze" },
            {28000006, "Mirror" },
            {28000007, "Lightning" },
            {28000008, "Zap" },
            {28000009, "Poison" },
            {28000010, "Graveyard" },
            {28000011, "The Log" },
            {28000012, "Tornado" },
            {28000013, "Clone" },
			{28000014, "Unknown" },
			{28000015, "Unknown" },
            {28000016, "Heal" }
        };

        public static string GetName(int id)
        {
            if (Cards.ContainsKey(id))
            {
                if(Cards[id] != "Unknown")
                {
                    return Cards[id];
                }
                else
                {
                    return id.ToString();
                }
            }
            else
            {
                return $"[{id}]Unknown";
            }
        }
        public static int GetID(string name)
        {
            if (Cards.ContainsValue(name))
            {
                return Cards.FirstOrDefault(x => x.Value == name).Key;
            }
            else
            {
                return 0;
            }
        }
    }
}

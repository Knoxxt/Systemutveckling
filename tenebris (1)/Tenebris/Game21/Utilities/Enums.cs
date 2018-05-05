using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Utilities
{
    public enum GameState // Created by Anton.
    {
        Menu,
        Game,
        Combat,
        Inventory
    }

    enum AiState // Different state of movement.
    {
        Chasing,
        Wander,
        Attack,
        Waiting
    }

    public enum PlayerClass
    {
        Warrior,
        Mage
    }
    
    public enum MonsterRace
    {
        Goblin,
        None,
        Devil
    }
    public class Enums
    {
        public GameState gState = new GameState();
        public PlayerClass pClass = new PlayerClass();
        public MonsterRace mRace = new MonsterRace();
    }
}

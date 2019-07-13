namespace Assets.Scripts
{
    public class Constant
    {
        public enum EnemyState
        {
            Idle, Move, Attack
        }

        public class EnemyStateId
        {
            public const string Idle = "Idle";
            public const string Move = "Move";
            public const string Attack = "Attack";
        }

        public class Event
        {
            public const string OnDamageTaken = "OnDamageTaken";
        }
    }
}

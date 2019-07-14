namespace Assets.Scripts
{
    public class Constant
    {
        public enum EnemyState
        {
            Idle, Move, Attack, Dead
        }

        public enum AmmoType
        {
            Rifle, Pistol, Shotgun
        }

        public class EnemyStateId
        {
            public const string Idle = "Idle";
            public const string Move = "Move";
            public const string Attack = "Attack";
            public const string Dead = "Dead";
        }

        public class WeaponStateParameter
        {
            public const string Zoomed = "IsZoomed";
        }

        public class Button
        {
            public const string MainFire = "Fire1";
            public const string SecondaryFire = "Fire2";
            public const string ScrollWheel = "Mouse ScrollWheel";
        }

        public class Event
        {
            public const string OnDamageTaken = "OnDamageTaken";
            public const string OnEnemyHealthDepleted = "OnEnemyHealthDepleted";
        }
    }
}

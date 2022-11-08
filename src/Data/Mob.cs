using System.Collections.Generic;

namespace KouCoCoa {
    internal struct Mob {
        internal int Id;
        internal string AegisName;
        internal string Name;
        internal string JapaneseName;
        internal int Level;
        internal int Hp;
        internal int Attack;
        internal int Attack2;
        internal int Defense;
        internal int MagicDefense;
        internal int Agi;
        internal int Vit;
        internal int Dex;
        internal int Luk;
        internal int AttackRange;
        internal int SkillRange;
        internal int ChaseRange;
        internal string Size;
        internal string Race;
        internal string Element;
        internal int ElementLevel;
        internal int WalkSpeed;
        internal int AttackDelay;
        internal int AttackMotion;
        internal int DamageMotion;
        internal int Ai;
        internal string Class;
        internal MobModes Modes;
        internal List<MobDrop> Drops;
    }

    internal struct MobModes {
        internal bool Mvp;
        internal bool Detector;
    }

    internal struct MobDrop {
        internal string Item;
        internal int Rate;
        internal bool StealProtected;
    }
}

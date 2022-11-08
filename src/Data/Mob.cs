using System.Collections.Generic;

namespace KouCoCoa {
    public struct Mob {
        public Mob() {
            Id = 0;
            AegisName = "Unknown";
            Name = "Unknown";
            JapaneseName = "Unknown";
            Level = 0;
            Hp = 0;
            Attack = 0;
            Attack2 = 0;
            Defense = 0;
            MagicDefense = 0;
            Agi = 0;
            Vit = 0;
            Dex = 0;
            Luk = 0;
            AttackRange = 0;
            SkillRange = 0;
            ChaseRange = 0;
            Size = "Small";
            Race = "Formless";
            Element = "Neutral";
            ElementLevel = 1;
            WalkSpeed = 0;
            AttackDelay = 0;
            AttackMotion = 0;
            DamageMotion = 0;
            Ai = 06.ToString("00");
            Class = "Normal";
            Modes = new();
            Drops = new();
        }
        public int Id;
        public string AegisName;
        public string Name;
        public string JapaneseName;
        public int Level;
        public int Hp;
        public int Attack;
        public int Attack2;
        public int Defense;
        public int MagicDefense;
        public int Agi;
        public int Vit;
        public int Dex;
        public int Luk;
        public int AttackRange;
        public int SkillRange;
        public int ChaseRange;
        public string Size;
        public string Race;
        public string Element;
        public int ElementLevel;
        public int WalkSpeed;
        public int AttackDelay;
        public int AttackMotion;
        public int DamageMotion;
        public string Ai;
        public string Class;
        public MobModes Modes;
        public List<MobDrop> Drops;
    }

    public struct MobModes {
        public MobModes() {
            CanMove = false;
            Looter = false;
            Aggressive = false;
            Assist = false;
            CastSensorIdle = false;
            NoRandomWalk = false;
            NoCast = false;
            CanAttack = false;
            CastSensorChase = false;
            ChangeChase = false;
            Angry = false;
            ChangeTargetMelee = false;
            ChangeTargetChase = false;
            TargetWeak = false;
            RandomTarget = false;
            IgnoreMelee = false;
            IgnoreMagic = false;
            IgnoreRanged = false;
            Mvp = false;
            IgnoreMisc = false;
            KnockbackImmune = false;
            TeleportBlock = false;
            FixedItemDrop = false;
            Detector = false;
            StatusImmune = false;
            SkillImmune = false;
        }

        public bool CanMove;
        public bool Looter;
        public bool Aggressive;
        public bool Assist;
        public bool CastSensorIdle;
        public bool NoRandomWalk;
        public bool NoCast;
        public bool CanAttack;
        public bool CastSensorChase;
        public bool ChangeChase;
        public bool Angry;
        public bool ChangeTargetMelee;
        public bool ChangeTargetChase;
        public bool TargetWeak;
        public bool RandomTarget;
        public bool IgnoreMelee;
        public bool IgnoreMagic;
        public bool IgnoreRanged;
        public bool Mvp;
        public bool IgnoreMisc;
        public bool KnockbackImmune;
        public bool TeleportBlock;
        public bool FixedItemDrop;
        public bool Detector;
        public bool StatusImmune;
        public bool SkillImmune;
    }

    public struct MobDrop {
        public MobDrop() {
            Item = "Apple";
            Rate = 1;
            StealProtected = true;
        }

        public string Item;
        public int Rate;
        public bool StealProtected;
    }
}

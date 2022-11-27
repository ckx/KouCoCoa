using System.CodeDom;
using System.Collections.Generic;

namespace KouCoCoa
{
    internal class Mob
    {
        #region Constructors
        #region Default Constructor
        public Mob()
        {
            Id = 0;
            AegisName = "UNKNOWN";
            Name = "Unknown";
            JapaneseName = Name;
            Level = 1;
            Hp = 1;
            Attack = 0;
            Attack2 = 0;
            Defense = 0;
            MagicDefense = 0;
            Str = 1;
            Agi = 1;
            Vit = 1;
            Int = 1;
            Dex = 1;
            Luk = 1;
            AttackRange = 0;
            SkillRange = 0;
            ChaseRange = 0;
            Size = "Small";
            Race = "Formless";
            Element = "Neutral";
            ElementLevel = 1;
            WalkSpeed = 125;
            AttackDelay = 0;
            AttackMotion = 0;
            DamageMotion = 0;
            Ai = 06.ToString("00");
            Class = "Normal";
            Modes = new();
            Drops = new();
            Skills = new();
        }
        #endregion

        #region Copy Constructor
        public Mob(Mob baseMob)
        {
            Id = baseMob.Id;
            AegisName = baseMob.AegisName;
            Name = baseMob.Name;
            JapaneseName = baseMob.JapaneseName;
            Level = baseMob.Level;
            Hp = baseMob.Hp;
            Attack = baseMob.Attack;
            Attack2 = baseMob.Attack2;
            Defense = baseMob.Defense;
            MagicDefense = baseMob.MagicDefense;
            Agi = baseMob.Agi;
            Vit = baseMob.Vit;
            Dex = baseMob.Dex;
            Luk = baseMob.Luk;
            AttackRange = baseMob.AttackRange;
            SkillRange = baseMob.SkillRange;
            ChaseRange = baseMob.ChaseRange;
            Size = baseMob.Size;
            Race = baseMob.Race;
            Element = baseMob.Element;
            ElementLevel = baseMob.ElementLevel;
            WalkSpeed = baseMob.WalkSpeed;
            AttackDelay = baseMob.AttackDelay;
            AttackMotion = baseMob.AttackMotion;
            DamageMotion = baseMob.DamageMotion;
            Ai = baseMob.Ai;
            Class = baseMob.Class;
            Modes = new MobModes(baseMob.Modes);
            Drops = new List<MobDrop>(baseMob.Drops);
            Skills = new List<MobSkill>(baseMob.Skills);
        }
        #endregion
        #endregion

        #region Properties
        public int Id { get; set; }
        public string AegisName { get; set; }
        public string Name { get; set; }
        public string JapaneseName { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Attack2 { get; set; }
        public int Defense { get; set; }
        public int MagicDefense { get; set; }
        public int Str { get; set; }
        public int Agi { get; set; }
        public int Vit { get; set; }
        public int Int { get; set; }
        public int Dex { get; set; }
        public int Luk { get; set; }
        public int AttackRange { get; set; }
        public int SkillRange { get; set; }
        public int ChaseRange { get; set; }
        public string Size { get; set; }
        public string Race { get; set; }
        public string Element { get; set; }
        public int ElementLevel { get; set; }
        public int WalkSpeed { get; set; }
        public int AttackDelay { get; set; }
        public int AttackMotion { get; set; }
        public int DamageMotion { get; set; }
        public string Ai { get; set; }
        public string Class { get; set; }
        public MobModes Modes { get; set; }
        public List<MobDrop> Drops { get; set; }
        public List<MobSkill> Skills { get; set; }
        #endregion
    }

    internal class MobModes
    {
        #region Constructor
        #region Default Constructor
        public MobModes()
        {
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
        #endregion

        #region Copy Constructor
        public MobModes(MobModes baseModes)
        {
            CanMove = baseModes.CanMove;
            Looter = baseModes.Looter;
            Aggressive = baseModes.Aggressive;
            Assist = baseModes.Assist;
            CastSensorIdle = baseModes.CastSensorIdle;
            NoRandomWalk = baseModes.NoRandomWalk;
            NoCast = baseModes.NoCast;
            CanAttack = baseModes.CanAttack;
            CastSensorChase = baseModes.CastSensorChase;
            ChangeChase = baseModes.ChangeChase;
            Angry = baseModes.Angry;
            ChangeTargetMelee = baseModes.ChangeTargetMelee;
            ChangeTargetChase = baseModes.ChangeTargetChase;
            TargetWeak = baseModes.TargetWeak;
            RandomTarget = baseModes.RandomTarget;
            IgnoreMelee = baseModes.IgnoreMelee;
            IgnoreMagic = baseModes.IgnoreMagic;
            IgnoreRanged = baseModes.IgnoreRanged;
            Mvp = baseModes.Mvp;
            IgnoreMisc = baseModes.IgnoreMisc;
            KnockbackImmune = baseModes.KnockbackImmune;
            TeleportBlock = baseModes.TeleportBlock;
            FixedItemDrop = baseModes.FixedItemDrop;
            Detector = baseModes.Detector;
            StatusImmune = baseModes.StatusImmune;
            SkillImmune = baseModes.SkillImmune;
        }
        #endregion
        #endregion

        #region Properties
        public bool CanMove { get; set; }
        public bool Looter { get; set; }
        public bool Aggressive { get; set; }
        public bool Assist { get; set; }
        public bool CastSensorIdle { get; set; }
        public bool NoRandomWalk { get; set; }
        public bool NoCast { get; set; }
        public bool CanAttack { get; set; }
        public bool CastSensorChase { get; set; }
        public bool ChangeChase { get; set; }
        public bool Angry { get; set; }
        public bool ChangeTargetMelee { get; set; }
        public bool ChangeTargetChase { get; set; }
        public bool TargetWeak { get; set; }
        public bool RandomTarget { get; set; }
        public bool IgnoreMelee { get; set; }
        public bool IgnoreMagic { get; set; }
        public bool IgnoreRanged { get; set; }
        public bool Mvp { get; set; }
        public bool IgnoreMisc { get; set; }
        public bool KnockbackImmune { get; set; }
        public bool TeleportBlock { get; set; }
        public bool FixedItemDrop { get; set; }
        public bool Detector { get; set; }
        public bool StatusImmune { get; set; }
        public bool SkillImmune { get; set; }
        #endregion
    }

    internal class MobDrop
    {
        #region Constructors
        #region Default Constructor
        public MobDrop()
        {
            Item = "Apple";
            Rate = 1;
            StealProtected = true;
        }
        #endregion

        #region Copy Constructor
        public MobDrop(MobDrop baseDrop)
        {
            Item = baseDrop.Item;
            Rate = baseDrop.Rate;
            StealProtected = baseDrop.StealProtected;
        }
        #endregion
        #endregion

        #region Properties
        public string Item { get; set; }
        public int Rate { get; set; }
        public bool StealProtected { get; set; }
        #endregion
    }

    internal class MobSkill
    {
        #region Constructors
        #region Default Constructor
        public MobSkill()
        {
            MobId = 0;
            MobName = "";
            SkillName = "";
            DummyValue = "";
            State = MobState.any;
            SkillId = 0;
            SkillLv = 0;
            Rate = 0;
            CastTime = 0;
            Delay = 0;
            Cancelable = MobSkillCancelable.yes;
            Target = MobSkillTarget.target; ;
            ConditionType = MobSkillConditionType.always;
            ConditionValue = "";
            Val1 = "";
            Val2 = "";
            Val3 = "";
            Val4 = "";
            Val5 = "";
            Emotion = "";
            Chat = "";
        }
        #endregion

        #region Copy Constructor
        public MobSkill(MobSkill baseSkill)
        {
            MobId = baseSkill.MobId;
            DummyValue = baseSkill.DummyValue;
            MobName = baseSkill.MobName;
            SkillName = baseSkill.SkillName;
            State = baseSkill.State;
            SkillId = baseSkill.SkillId;
            SkillLv = baseSkill.SkillLv;
            Rate = baseSkill.Rate;
            CastTime = baseSkill.CastTime;
            Delay = baseSkill.Delay;
            Cancelable = baseSkill.Cancelable;
            Target = baseSkill.Target;
            ConditionType = baseSkill.ConditionType;
            ConditionValue = baseSkill.ConditionValue;
            Val1 = baseSkill.Val1;
            Val2 = baseSkill.Val2;
            Val3 = baseSkill.Val3;
            Val4 = baseSkill.Val4;
            Val5 = baseSkill.Val5;
            Emotion = baseSkill.Emotion;
            Chat = baseSkill.Chat;
        }
        #endregion
        #endregion
        // Structure of Database:
        // MobID,Dummy value (info only),State,SkillID,SkillLv,Rate,CastTime,Delay,Cancelable,Target,Condition type,
        // Condition value,val1,val2,val3,val4,val5,Emotion,Chat
        //1299,Goblin Leader@NPC_SUMMONSLAVE,attack,196,5,10000,2000,60000,no,self,slavele,2,1122,1123,1124,1125,1126,27,
        #region Properties
        public int MobId { get; set; }
        public string DummyValue { get; set; }
        public string MobName { get; set; }
        public string SkillName { get; set; }
        public MobState State { get; set; }
        public int SkillId { get; set; }
        public int SkillLv { get; set; }
        /// <summary>
        /// RATE: the chance of the skill being casted when the condition is fulfilled (10000 = 100%)
        /// </summary>
        public int Rate { get; set; }
        public int CastTime { get; set; }

        /// <summary>
        /// DELAY: the time (in milliseconds) before attempting to recast the same skill.
        /// </summary>
        public int Delay { get; set; }

        public MobSkillCancelable Cancelable { get; set; }
        public MobSkillTarget Target { get; set; }
        public MobSkillConditionType ConditionType { get; set; }
        public string ConditionValue { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Val3 { get; set; }
        public string Val4 { get; set; }
        public string Val5 { get; set; }
        public string Emotion { get; set; }
        public string Chat { get; set; }
        #endregion
    }
}

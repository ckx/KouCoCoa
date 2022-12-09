namespace KouCoCoa 
{
    internal enum LogLevel 
    {
        Error,
        Warning,
        Info,
        Debug,
        DebugVerbose
    }

    internal enum RAthenaDbType 
    {
        UNSUPPORTED,
        MOB_DB,
        MOB_AVAIL_DB,
        ITEM_DB,
        MOB_SKILL_DB,
        NPC_IDENTITY
    }

    //	any (except dead) / idle (in standby) / walk (in movement) / dead (on killed) /
    //	loot /attack / angry (like attack, except player has not attacked mob yet) /
    //	chase (following target, after being attacked) / follow (following target,
    //	without being attacked) / anytarget (attack+angry+chase+follow)
    internal enum MobState
    {
        any,
        idle,
        walk,
        dead,
        loot,
        attack,
        angry,
        chase,
        follow,
        anytarget
    }

    internal enum MobSkillCancelable
    {
        yes,
        no
    }

    // TARGET:
    //	target (current target) / self / friend / master / randomtarget (any enemy within skill's range)
    //
    //	The following are for ground-skills, a random target tile is selected from the specified area:
    //	    around1 (3x3 area around self) / around2 (5x5 area around self) /
    //	    around3 (7x7 area around self) / around4 (9x9 area around self) /
    //	    around5 (3x3 area around target) / around6 (5x5 area around target) /
    //	    around7 (7x7 area around target) / around8 (9x9 area around target) /
    //	    around = around4
    //
    internal enum MobSkillTarget
    {
        target,
        self,
        friend,
        master,
        randomtarget,
        around,
        around1,
        around2,
        around3,
        around4,
        around5,
        around6,
        around7,
        around8
    }

    // CONDITION:
    //	always			Unconditional (no condition value).
    //	onspawn			When mob spawns/respawns (no condition value).
    //	myhpltmaxrate		When mob's HP drops to the specified %.
    //	myhpinrate		When mob's HP is in a certain % range (condition value = lower bound, val1 = upper bound).
    //	mystatuson		If mob has the specified abnormality in status.
    //	mystatusoff		If mob has ended the specified abnormality in status.
    //	friendhpltmaxrate	When mob's friend's HP drops to the specified %.
    //	friendhpinrate		When mob's friend's HP is in a certain % range (condition value = lower bound, val1 = upper bound).
    //	friendstatuson		If friend has the specified abnormality in status.
    //	friendstatusoff		If friend has ended the specified abnormality in status.
    //	attackpcgt		When attack PCs become greater than specified number.
    //	attackpcge		When attack PCs become greater than or equal to the specified number.
    //	slavelt			When number of slaves is less than the original specified number.
    //	slavele			When number of slaves is less than or equal to the original specified number.
    //	closedattacked		When close range melee attacked (no condition value).
    //	longrangeattacked	When long range attacked, ex. bows, guns, ranged skills (no condition value).
    //	skillused		When the specified skill is used on the mob.
    //	afterskill		After mob casts the specified skill.
    //	casttargeted		When a target is in cast range (no condition value).
    //	rudeattacked		When mob is rude attacked (no condition value).
    //
    //	Status abnormalities specified through the statuson/statusoff system:
    //	    anybad (any type of state change) / stone / freeze / stun / sleep /
    //	    poison / curse / silence / confusion / blind / hiding / sight (unhidden)
    internal enum MobSkillConditionType
    {
        always,
        onspawn,
        myhpltmaxrate,
        myhpinrate,
        mystatuson,
        mystatusoff,
        friendhpltmaxrate,
        friendhpinrate,
        friendstatuson,
        friendstatusoff,
        attackpcgt,
        attackpcge,
        slavelt,
        slavele,
        closedattacked,
        longrangeattacked,
        skillused,
        afterskill,
        chasetargeted,
        rudeattacked
    }

    internal enum MobSkillStatuses
    {
        anybad,
        stone,
        freeze,
        stun,
        sleep,
        poison,
        curse,
        silence,
        confusion,
        blind,
        hiding,
        sight
    }
}
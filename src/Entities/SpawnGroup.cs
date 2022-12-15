using System.Collections.Generic;

namespace KouCoCoa
{
    internal class SpawnGroup
    {
        #region Constructors
        public SpawnGroup()
        {
            Id = 0;
            Name = "Unnamed_SpawnGroup";
            Members = new();
        }

        public SpawnGroup(SpawnGroup baseSpawnGroup)
        {
            Id = baseSpawnGroup.Id;
            Name = new(baseSpawnGroup.Name);
            Members = new(baseSpawnGroup.Members);
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SpawnGroupMember> Members { get; set; }
        #endregion
    }

    /// <summary>
    /// A monster entry within a spawngroup
    /// </summary>
    internal class SpawnGroupMember
    {
        #region Constructors
        public SpawnGroupMember()
        {
            Id = 0;
            Name = "Unnamed_SpawnGroupMember";
            Count = 0;
            RewardMod = 0;
        }

        public SpawnGroupMember(SpawnGroupMember baseSpawnGroupMember)
        {
            Id = baseSpawnGroupMember.Id;
            Name = new(baseSpawnGroupMember.Name);
            Count = baseSpawnGroupMember.Count;
            RewardMod = baseSpawnGroupMember.RewardMod;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int RewardMod { get; set; }
        #endregion
    }
}

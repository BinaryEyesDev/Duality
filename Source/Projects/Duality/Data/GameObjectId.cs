namespace Duality.Data
{
    public readonly struct GameObjectId
    {
        public static readonly GameObjectId Invalid = new("", "", "");
        public readonly string Group;
        public readonly string SubGroup;
        public readonly string Name;
        public bool IsInvalid => string.IsNullOrEmpty(Group) || string.IsNullOrEmpty(SubGroup) || string.IsNullOrEmpty(Name);

        public GameObjectId(string group, string subGroup)
        {
            Group = group;
            SubGroup = subGroup;
            Name = "";
        }

        public GameObjectId(string group, string subGroup, string name)
        {
            Group = group;
            SubGroup = subGroup;
            Name = name;
        }
    }
}


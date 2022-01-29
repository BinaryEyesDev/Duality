using System;
using System.IO;

namespace Duality.Data
{
    public readonly struct GameElementTemplateInfo
    {
        public static GameElementTemplateInfo Invalid => new("", "", "", IntPtr.Zero);
        public readonly string GroupType;
        public readonly string SubGroupType;
        public readonly string TexturePath;
        public readonly IntPtr Pointer;
        public bool IsValid => Pointer != IntPtr.Zero;
        public string TextureName => Path.GetFileNameWithoutExtension(TexturePath);

        public GameElementTemplateInfo(
            string group,
            string subGroup, 
            string path, 
            IntPtr pointer)
        {
            GroupType = group;
            SubGroupType = subGroup;
            TexturePath = path;
            Pointer = pointer;
        }
    }
}
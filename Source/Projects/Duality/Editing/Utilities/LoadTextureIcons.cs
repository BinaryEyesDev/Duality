using System.Collections.Generic;
using Duality.Data;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.ImGui.Standard;

namespace Duality.Editing.Utilities
{
    using GroupingInfoMap = Dictionary<string, Dictionary<string, List<GameElementTemplateInfo>>>;
    using SubGroupInfoMap = Dictionary<string, List<GameElementTemplateInfo>>;

    public static class LoadTextureIcons
    {
        public static GroupingInfoMap Perform(GameDriver driver, ImGUIRenderer renderer)
        {
            var map = new GroupingInfoMap();
            foreach (var groupEntry in driver.TextureRegistry.Groupings)
            {
                var groupType = groupEntry.Key;
                var subGroupInfoMap = new SubGroupInfoMap();
                foreach (var subGroupEntry in groupEntry.Value)
                {
                    var subGroupInfoList = new List<GameElementTemplateInfo>();
                    var subGroupType = subGroupEntry.Key;
                    var subGroupList = subGroupEntry.Value;
                    foreach (var entry in subGroupList)
                    {
                        var elementInfo = new GameElementTemplateInfo(groupType, subGroupType, entry.Name, renderer.BindTexture(entry));
                        subGroupInfoList.Add(elementInfo);
                    }

                    subGroupInfoMap.Add(subGroupType, subGroupInfoList);
                }

                map.Add(groupType, subGroupInfoMap);
            }

            return map;
        }
    }
}

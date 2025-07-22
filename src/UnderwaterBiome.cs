using UnityEngine;
using Verse;

namespace BoUnderwater
{

    [StaticConstructorOnStartup]
    public class UnderwaterBiome : Mod
    {
        public static ModContentPack mcp;

        UnderwaterBiomeSettings Settings;

        public UnderwaterBiome(ModContentPack content) : base(content)
        {
            mcp = content;

            Settings = GetSettings<UnderwaterBiomeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            UnderwaterBiomeSettings.DoSettingsWindowContents(inRect);

            base.DoSettingsWindowContents(inRect);
        }


        public override string SettingsCategory()
        {
            return "Underwater Biome";
        }
    }
}

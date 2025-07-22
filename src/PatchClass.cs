using Verse;
using HarmonyLib;
using BiomesCore.DefModExtensions;

namespace BoUnderwater
{
    [StaticConstructorOnStartup]
    public static class PatchClass
    {
        public const string UID = "com.bo.underwater";

        static PatchClass()
        {
            var harmony = new Harmony(UID);
            harmony.PatchAll();
        }
    }
}

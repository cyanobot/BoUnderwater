using HarmonyLib;
using RimWorld.Planet;

namespace BoUnderwater
{
    [HarmonyPatch(typeof(Tile))]
    [HarmonyPatch("WaterCovered", MethodType.Getter)]
    public static class Patch_Tile_WaterCovered
    {
        [HarmonyPostfix]
        public static void Postfix(Tile __instance, ref bool __result)
        {
            if (__instance.PrimaryBiome == UnderWaterDefOf.UB_ShallowsTropical)
            {
                __result = true;
            }
        }
    }
}

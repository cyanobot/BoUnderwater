using Verse;
using HarmonyLib;
using RimWorld;

namespace BoUnderwater
{
    //override fertility calculation depending on maps biome, probably best way
    [HarmonyPatch(typeof(FertilityGrid), "CalculateFertilityAt")]
    public static class Patch_FertilityGrid_CalculateFertilityAt
    {
        public static void Postfix(IntVec3 loc, Map ___map, ref float __result)
        {
            if (___map.Biome == UnderWaterDefOf.UB_ShallowsTropical)
            {
                TerrainDef terrain = ___map.terrainGrid.TerrainAt(loc);

                var stonExtension = terrain.GetModExtension<NaturalStoneExtension>();
                if (stonExtension != null && stonExtension.IsValidBiome(___map.Biome))
                {
                    //Log.Message($"Conditionally changing terrain def fertility value to {stonExtension.StoneFertilityOverride} on {terrain.defName}, because it is specified in a Conditional FertilityExtension");
                    __result = stonExtension.StoneFertilityOverride;
                }
            }
        }
    }
}

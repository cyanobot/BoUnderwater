using Verse;
using HarmonyLib;
using RimWorld.Planet;
using System;

namespace BoUnderwater
{
    //I STILL DO NOT FULLY UNDERSTAND THIS, THIS IS JUST RANDOM NUMBERS, TODO.
    [HarmonyPatch(typeof(WorldGenStep_Terrain), "GenerateTileFor", new Type[] { typeof(PlanetTile), typeof(PlanetLayer) })]
    public static class Patch_WorldGenStep_Terrain_GenerateTileFor
    {
        [HarmonyPostfix]
        public static void Postfix(ref Tile __result, PlanetTile tile)
        {
            if (__result.PrimaryBiome == UnderWaterDefOf.UB_ShallowsTropical)
            {
                // Generate hilliness for underwater tiles
                float underwaterHillValue = Rand.Value;

                if (underwaterHillValue > 0.7f)
                {
                    __result.hilliness = Hilliness.SmallHills;
                }
                else if (underwaterHillValue > 0.9f)
                {
                    __result.hilliness = Hilliness.LargeHills;
                }

                __result.elevation = 1f;
            }
        }
    }
}

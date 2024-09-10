using Verse;
using HarmonyLib;
using RimWorld.Planet;
using System;
using RimWorld;
using System.Collections.Generic;

namespace BoUnderwater
{
    [StaticConstructorOnStartup]
    public static class PatchClass
    {
        static PatchClass()
        {
            var harmony = new Harmony("com.bo.underwater");
            harmony.PatchAll();
        }
    }


    [HarmonyPatch(typeof(WorldGenStep_Terrain), "GenerateTileFor", new Type[] { typeof(int) })]
    public static class Patch_WorldGenStep_Terrain_GenerateTileFor
    {
        [HarmonyPostfix]
        public static void Postfix(ref Tile __result, int tileID)
        {
            if (__result.biome == UnderWaterDefOf.UB_ShallowsTropical)
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
                // Ensure the tile remains water-covered
                /// __result.WaterCovered = true;
            }
        }
    }

    [HarmonyPatch(typeof(Tile))]
    [HarmonyPatch("WaterCovered", MethodType.Getter)]
    public static class Patch_Tile_WaterCovered
    {
        [HarmonyPostfix]
        public static void Postfix(Tile __instance, ref bool __result)
        {
            if (__instance.biome == UnderWaterDefOf.UB_ShallowsTropical)
            {
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(MapGenerator))]
    [HarmonyPatch("GenerateMap")]
    [HarmonyPatch(new Type[] { typeof(IntVec3), typeof(MapParent), typeof(MapGeneratorDef),
                               typeof(IEnumerable<GenStepWithParams>), typeof(Action<Map>), typeof(bool) })]
    public static class MapGenerator_GenerateMap_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Map __result, MapParent parent)
        {
            BiomeDef targetBiome = UnderWaterDefOf.UB_ShallowsTropical;
            GameConditionDef conditionToActivate = UnderWaterDefOf.UnderWaterEnvironment;

            if (parent != null && Find.WorldGrid[parent.Tile].biome == targetBiome)
            {
                Map map = __result;
                if (map != null)
                {
                    if (!map.gameConditionManager.ConditionIsActive(conditionToActivate))
                    {
                        GameCondition condition = GameConditionMaker.MakeConditionPermanent(conditionToActivate);
                        map.gameConditionManager.RegisterCondition(condition);

                        Log.Message($"Activated {conditionToActivate.defName} in {targetBiome.defName}");
                    }
                }
            }
        }
    }
}

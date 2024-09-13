using Verse;
using HarmonyLib;
using RimWorld.Planet;
using System;
using RimWorld;
using System.Collections.Generic;
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

                        //Log.Message($"Activated {conditionToActivate.defName} in {targetBiome.defName}");
                    }
                }
            }
        }
    }

    #region WORLD GEN HILLS AND WATER

    //I STILL DO NOT FULLY UNDERSTAND THIS, THIS IS JUST RANDOM NUMBERS, TODO.
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

    #endregion
}

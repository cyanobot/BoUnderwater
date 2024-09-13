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

                        Log.Message($"Activated {conditionToActivate.defName} in {targetBiome.defName}");
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

    #region TERRAIN


    //override can plant logic, if in biome that has mod extension, and terrain in question is natural stone, then check if plant has IGrowOnStone terrain tag.
    [HarmonyPatch(typeof(PlantUtility), nameof(PlantUtility.CanEverPlantAt),
    new[] { typeof(ThingDef), typeof(IntVec3), typeof(Map), typeof(Thing), typeof(bool), typeof(bool) },
    new[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal })]
    public static class Patch_PlantUtility_CanEverPlantAt
    {
        public static bool Prefix(ThingDef plantDef, IntVec3 c, Map map, ref Thing blockingThing, bool canWipePlantsExceptTree, bool writeNoReason, ref AcceptanceReport __result)
        {
            TerrainDef terrain = map.terrainGrid.TerrainAt(c);
            ConditionalFertilityExtension fertilityExt = map.Biome.GetModExtension<ConditionalFertilityExtension>();

            if (fertilityExt == null)
            {
                return true; 
            }

            //Log.Message("BoUnderwater: Biome has ConditionalFertilityExtension ModExtension.");

            bool isStoneOrUnderwaterTerrain = terrain != null && terrain.IsNaturalStone();

            if (isStoneOrUnderwaterTerrain)
            {
                Log.Message("BoUnderwater: isStoneOrUnderwaterTerrain");
                if (plantDef == UnderWaterDefOf.UB_Plant_Anemone)
                {
                    Log.Message($"BoUnderwater: IsAllowedUnderwaterPlant {plantDef.defName}");
                    __result = AcceptanceReport.WasAccepted;
                    return false;
                }
                else
                {
                    Log.Message($"BoUnderwater: cannt grow on stone {plantDef.defName}");
                    __result = new AcceptanceReport("CannotGrowOnStone".Translate());
                    return false;
                }
            }

            return true;
        }

        private static bool IsAllowedUnderwaterPlant(ThingDef plantDef)
        {
            Biomes_PlantControl plantControl = plantDef.GetModExtension<Biomes_PlantControl>();
            if (plantControl != null)
            {
                return plantControl.terrainTags != null && plantControl.terrainTags.Contains("IGrowOnStone");
            }
            return false;
        }

        //not good very bad
        public static bool IsNaturalStone(this TerrainDef thingDef)
        {
            return thingDef.defName.EndsWith("_Rough") || thingDef.defName.EndsWith("_Smooth") || thingDef.defName.EndsWith("_RoughHewn");
        }

    }

    //override fertility calculation depending on maps biome, probably best way
    [HarmonyPatch(typeof(FertilityGrid), "CalculateFertilityAt")]
    public static class Patch_FertilityGrid_CalculateFertilityAt
    {
        public static void Postfix(IntVec3 loc, Map ___map, ref float __result)
        {
            if (___map.Biome == UnderWaterDefOf.UB_ShallowsTropical)
            {
                TerrainDef terrain = ___map.terrainGrid.TerrainAt(loc);

                var fertilityExt = terrain.GetModExtension<ConditionalFertilityExtension>();
                if (fertilityExt != null && fertilityExt.applicableBiomes.Contains(___map.Biome))
                {
                    Log.Message($"Conditionally changing terrain def fertility value to {fertilityExt.stoneFertilityOverride} on {terrain.defName}, because it is specified in a Conditional FertilityExtension");
                    __result = fertilityExt.stoneFertilityOverride;
                }
            }
        }
    }

    //[HarmonyPatch(typeof(PlantUtility), "CanEverPlantAt",
    //        new[] { typeof(ThingDef), typeof(IntVec3), typeof(Map), typeof(Thing), typeof(bool), typeof(bool) },
    //        new[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal })]
    //public static class Patch_PlantUtility_CanEverPlantAt
    //{
    //    public static bool Prefix(ThingDef plantDef, IntVec3 c, Map map, ref Thing blockingThing, bool canWipePlantsExceptTree, bool writeNoReason, ref AcceptanceReport __result)
    //    {
    //        if (map.Biome != UnderWaterDefOf.UB_ShallowsTropical)
    //        {
    //            return true;
    //        }

    //        if (plantDef == UnderWaterDefOf.UB_Plant_Anemone)
    //        {
    //            TerrainDef terrain = map.terrainGrid.TerrainAt(c);
    //            if (IsSpecialStonePlant(plantDef))
    //            {
    //                if (terrain != null && (terrain.defName.EndsWith("_Rough") || terrain.defName.EndsWith("_Smooth") || terrain.defName.EndsWith("_RoughHewn")))
    //                {
    //                    // Check for ConditionalFertilityExtension and apply fertility if applicable
    //                    var fertilityExt = terrain.GetModExtension<ConditionalFertilityExtension>();
    //                    if (fertilityExt != null && fertilityExt.applicableBiomes.Contains(map.Biome))
    //                    {
    //                       // Log.Message($"BoUnderwater: Modifying terrain def fertility to {fertilityExt.fertility}");
    //                        //terrain.fertility = fertilityExt.fertility;
    //                    }

    //                    Log.Message($"Allowing {plantDef.defName} on {terrain.defName}");
    //                    __result = AcceptanceReport.WasAccepted;
    //                    return false;
    //                }
    //            }
    //        }
    //        return true;
    //    }

    //    private static bool IsSpecialStonePlant(ThingDef plantDef)
    //    {
    //        return plantDef == UnderWaterDefOf.UB_Plant_Anemone;
    //    }
    //}


    #endregion
}

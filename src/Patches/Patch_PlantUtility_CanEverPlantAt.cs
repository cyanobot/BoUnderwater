using Verse;
using HarmonyLib;
using RimWorld;

namespace BoUnderwater
{
    //override can plant logic, if in biome that has mod extension, and terrain in question is natural stone, then check if plant has IGrowOnStone terrain tag.
    [HarmonyPatch(typeof(PlantUtility), nameof(PlantUtility.CanEverPlantAt),
    new[] { typeof(ThingDef), typeof(IntVec3), typeof(Map), typeof(Thing), typeof(bool), typeof(bool) },
    new[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref, ArgumentType.Normal, ArgumentType.Normal })]
    public static class Patch_PlantUtility_CanEverPlantAt
    {
        public static bool Prefix(ThingDef plantDef, IntVec3 c, Map map, ref Thing blockingThing, bool canWipePlantsExceptTree, bool writeNoReason, ref AcceptanceReport __result)
        {
            TerrainDef terrain = map.terrainGrid.TerrainAt(c);
            PlantExtension plantExtension = plantDef.GetModExtension<PlantExtension>();
            bool IsNaturalStone = terrain != null && terrain.IsNaturalStone();


            if (plantExtension == null)
            {
                if (IsNaturalStone)
                {
                    return false;
                }

                return true; 
            }

            if (IsNaturalStone && plantExtension.IsValidBiome(map.Biome))
            {
                if (plantExtension.CanGrowOnStone)
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
        public static bool IsNaturalStone(this TerrainDef thingDef)
        {
            return thingDef.defName.EndsWith("_Rough") || thingDef.defName.EndsWith("_Smooth") || thingDef.defName.EndsWith("_RoughHewn");
        }

    }
}

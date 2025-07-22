using Verse;
using HarmonyLib;
using RimWorld.Planet;
using System;
using RimWorld;
using System.Collections.Generic;

namespace BoUnderwater
{
    [HarmonyPatch(typeof(MapGenerator))]
    [HarmonyPatch("GenerateMap")]
    [HarmonyPatch(new Type[] { typeof(IntVec3), typeof(MapParent), typeof(MapGeneratorDef),
                               typeof(IEnumerable<GenStepWithParams>), typeof(Action<Map>), 
                               typeof(bool), typeof(bool) })]
    public static class MapGenerator_GenerateMap_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Map __result, MapParent parent)
        {
            BiomeDef targetBiome = UnderWaterDefOf.UB_ShallowsTropical;
            GameConditionDef conditionToActivate = UnderWaterDefOf.UnderWaterEnvironment;

            if (parent != null && Find.WorldGrid[parent.Tile].PrimaryBiome == targetBiome)
            {
                Map map = __result;
                if (map != null)
                {
                    if (!map.gameConditionManager.ConditionIsActive(conditionToActivate))
                    {
                        GameCondition condition = GameConditionMaker.MakeConditionPermanent(conditionToActivate);
                        map.gameConditionManager.RegisterCondition(condition);
                    }
                }
            }
        }
    }
}

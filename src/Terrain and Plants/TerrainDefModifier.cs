using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using BiomesCore.DefModExtensions;

namespace BoUnderwater
{
    [StaticConstructorOnStartup]
    public static class TerrainDefModifier
    {
        static TerrainDefModifier()
        {
            LongEventHandler.QueueLongEvent(ModifyTerrainDefs, "ModifyingTerrainDefs", false, null);
        }

        private static void ModifyTerrainDefs()
        {
            Log.Message("BoUnderwater: Starting terrain def modifications from XML");

            foreach (string TerrainDefName in UnderWaterDefOf.UnderWaterTerrainModification.TargetTerrainDefNames)
            {
                TerrainDef TerrainDef = DefDatabase<TerrainDef>.GetNamed(TerrainDefName, false);
                if (TerrainDef != null)
                {
                    ApplyModifications(TerrainDef, UnderWaterDefOf.UnderWaterTerrainModification);
                }
                else
                {
                    //Log.Error($"BoUnderwater: TerrainDef {TerrainDefName} not found for modification");
                }
            }

            Log.Message("BoUnderwater: Finished terrain def modifications from XML");
        }

        private static void ApplyModifications(TerrainDef TerrainDef, TerrainModificationDef ModDef)
        {
            Log.Message($"BoUnderwater: Applying modifications to {TerrainDef.defName}");

            if (TerrainDef.modExtensions == null)
            {
                TerrainDef.modExtensions = new List<DefModExtension>();
            }

            var StoneExtension = TerrainDef.GetModExtension<NaturalStoneExtension>() ?? new NaturalStoneExtension();
            StoneExtension.StoneFertilityOverride = ModDef.StoneFertilityOverride;
            StoneExtension.ApplicableBiomes = ModDef.ApplicableBiomes;
            TerrainDef.modExtensions.Add(StoneExtension);
        }
    }

    public class TerrainModificationDef : Def
    {
        public List<string> TargetTerrainDefNames;
        public List<string> TerrainTags;
        public float StoneFertilityOverride;
        public List<BiomeDef> ApplicableBiomes;
    }

}

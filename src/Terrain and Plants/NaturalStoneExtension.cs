using Verse;
using RimWorld;
using System.Collections.Generic;

namespace BoUnderwater
{  
    public class NaturalStoneExtension : DefModExtension
    {
        public float StoneFertilityOverride = 0.1f;
        public List<BiomeDef> ApplicableBiomes = new List<BiomeDef>();

        public bool IsValidBiome(BiomeDef Biome)
        {
            return ApplicableBiomes.Contains(Biome);
        }
    }
}

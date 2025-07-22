using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace BoUnderwater
{
    public class PlantExtension : DefModExtension
    {
        public bool CanGrowOnStone = false;
        public List<BiomeDef> ApplicableBiomes = new List<BiomeDef>();

        public bool IsValidBiome(BiomeDef Biome)
        {
            return ApplicableBiomes.Contains(Biome);
        }
    }
}

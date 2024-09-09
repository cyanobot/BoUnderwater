using RimWorld;

namespace BoUnderwater
{
    [DefOf]
    internal class UnderWaterDefOf
    {

        public static BiomeDef UB_ShallowsTropical;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        static UnderWaterDefOf()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(UnderWaterDefOf));
        }
    }
}

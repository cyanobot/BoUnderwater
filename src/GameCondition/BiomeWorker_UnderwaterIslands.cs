using RimWorld;
using RimWorld.Planet;

namespace BoUnderwater
{
    //    <BoUnderwater.UnderWaterGameConditionDef>
    //      <defName>UnderWaterEnvironment</defName>
    //      <label>Underwater Environment</label>
    //      <description>An underwater environment with unique visual effects.</description>
    //      <conditionClass>BoUnderwater.GameCondition_UnderWaterCondition</conditionClass>
    //      <SkyColor>(0.1, 0.1, 0.1)</SkyColor>
    //      <SkyColorNight>(0.4, 0.4, 0.5)</SkyColorNight>
    //      <ShadowColor>(1, 0, 0)</ShadowColor>
    //      <OverlayColor>(1, 1, 1, 1)</OverlayColor>
    //      <SkyColorSaturation>0.8</SkyColorSaturation>
    //      <OverallGlowIntensityMultiplier>1</OverallGlowIntensityMultiplier>
    //      <letterDef>NegativeEvent</letterDef>
    //      <weatherDef>Clear</weatherDef>
    //      <canBePermanent>true</canBePermanent>
    //      <natural>false</natural>
    //      <allowUnderground>true</allowUnderground>
    //    </BoUnderwater.UnderWaterGameConditionDef>


    public class BiomeWorker_UnderwaterIslands : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            float Score = 0f;

            //// Start with no score
            if (tile.WaterCovered)
            {
                Score += 2;
            }


            //if (tile.hilliness == Hilliness.LargeHills || tile.hilliness == Hilliness.Mountainous)
            //{
            //    Score += 1f;
            //}
            //else
            //{
            //    Score =- 1f;
            //}

            return Score;
        }
    }

   
}

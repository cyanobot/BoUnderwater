using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class UnderWaterGameConditionDef : GameConditionDef
    {
        public Color SkyColor;
        public Color SkyColorNight;
        public Color ShadowColor;
        public Color OverlayColor;

        public float SkyColorSaturation;
        public float OverallGlowIntensityMultiplier;

        public UnderWaterGameConditionDef()
        {
            conditionClass = typeof(GameCondition_UnderWaterCondition);
        }
    }

        
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
}

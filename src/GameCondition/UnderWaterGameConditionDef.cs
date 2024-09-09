using UnityEngine;
using Verse;
using BiomesCore.WorldMap;
using BiomesCore.Planet;
using HarmonyLib;
using System;

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

   
}

using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BoUnderwater
{
    //pixelwizardy
    public class GameCondition_UnderWaterCondition : GameCondition
    {
        private Color SkyColor => UnderwaterBiomeSettings.Sky_ColorDay; // color of the sky
        private Color SkyColorNight => UnderwaterBiomeSettings.Sky_ColorNight;
        private Color ShadowColor => UnderwaterBiomeSettings.Sky_ColorShadowDay; // color of any shadows
        private Color ShadowColorNight => UnderwaterBiomeSettings.Sky_ColorShadowNight;        
        private Color OverlayColor => UnderwaterBiomeSettings.Sky_ColorOverlay; // strength (opacity) of the sky color
        private float Saturation => UnderwaterBiomeSettings.Sky_Saturation; // saturation of the sky color (0 = grayscale)
        private float Glow => UnderwaterBiomeSettings.Sky_Glow; // strength of any actual light in each cell on the map

        private CausticsOverlay CausticsOverlay;
        private MurkOverlay MurkOverlay;

        private List<SkyOverlay> ConditionOverlays = new List<SkyOverlay>();

        public override int TransitionTicks => 1; // how quickly the sky changes color (120 ticks = 2 seconds)
        public override void Init()
        {
            base.Init();
        }
        public override void GameConditionTick()
        {
            base.GameConditionTick();
            List<Map> affectedMaps = base.AffectedMaps;
            foreach (var map in affectedMaps)
            {
                foreach (var item in SkyOverlays(map))
                {
                    item.TickOverlay(map,1f);
                }
            }
        }

        public override void GameConditionDraw(Map map)
        {
            base.GameConditionDraw(map);

            if (map == null)
            {
                return;
            }

            foreach (var item in this.SkyOverlays(map))
            {
                item.DrawOverlay(map);

                if (item is CausticsOverlay causticsOverlay)
                {
                    causticsOverlay.UpdateZoom();
                }
            }
        }

        public override List<SkyOverlay> SkyOverlays(Map map)
        {
            return new List<SkyOverlay>() { 
                new CausticsOverlay() ,
                new MurkOverlay()
            };
        }

        public override float SkyTargetLerpFactor(Map map)
        {
            return GameConditionUtility.LerpInOutValue(this, TransitionTicks);
        }

        public SkyColorSet SkyColors
        {
            get
            {
                float dayPercent = GenCelestial.CurCelestialSunGlow(Find.CurrentMap);
                Color lerpedSkyColor = Color.Lerp(SkyColorNight, SkyColor, dayPercent);
                Color lerpedShadowColor = Color.Lerp(ShadowColorNight, ShadowColor, dayPercent);

                //additionally, adjust shadow color by murk intensity, so that shadows disappear as murk increases
                Color murkShadowColor = UnderwaterBiomeSettings.Murk_Color;
                float murkOpacity = murkShadowColor.a;
                murkShadowColor.a = 1 - murkOpacity;
                lerpedShadowColor = Color.Lerp(lerpedShadowColor, murkShadowColor, murkOpacity);

                return new SkyColorSet(lerpedSkyColor, lerpedShadowColor, OverlayColor, Saturation);
            }
        }

        public override SkyTarget? SkyTarget(Map map)
        {
            return new SkyTarget(Glow, SkyColors, 1f, 1f);
        }
    }
}

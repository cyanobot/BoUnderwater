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
        private Color SkyColor = new Color(1f, 0.953f, 0.773f); // color of the sky
        private Color SkyColorNight = Color.white;
        private Color ShadowColor = new Color(0.596f, 0.902f, 1f); // color of any shadows
        private Color OverlayColor = new Color(0f, 0f, 0f); // strength (opacity) of the sky color
        private float Saturation = 1f; // saturation of the sky color (0 = grayscale)
        private float Glow = 0.7f; // strength of any actual light in each cell on the map

        private CausticsOverlay CausticsOverlay;

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
                    //causticsOverlay.UpdateMaterial();
                }
            }
        }

        public override List<SkyOverlay> SkyOverlays(Map map)
        {
            return new List<SkyOverlay>() { new CausticsOverlay() };
        }

        public override float SkyTargetLerpFactor(Map map)
        {
            return GameConditionUtility.LerpInOutValue(this, TransitionTicks);
        }

        public SkyColorSet TestSkyColors
        {
            get
            {
                float dayPercent = GenCelestial.CurCelestialSunGlow(Find.CurrentMap);
                Color lerpedColor = Color.Lerp(SkyColorNight, SkyColor, dayPercent);
                return new SkyColorSet(lerpedColor, ShadowColor, OverlayColor, Saturation);
            }
        }

        public override SkyTarget? SkyTarget(Map map)
        {
            return new SkyTarget(Glow, TestSkyColors, 1f, 1f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class MurkOverlay : SkyOverlay
    {
        public Material Material;

        public MurkOverlay()
        {
            this.Material = AssetLoader.MurkMaterial;
            AssetLoader.UpdateMurkMaterial();
        }

        public override void TickOverlay(Map map, float lerpFactor)
        {

        }

        public override void DrawOverlay(Map map)
        {
            SkyOverlay.DrawWorldOverlay(map, Material);
        }

        public override void SetOverlayColor(Color color)
        {

        }
    }
}

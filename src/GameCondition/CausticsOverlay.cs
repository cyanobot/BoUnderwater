using UnityEngine;
using Verse;
using System;

namespace BoUnderwater
{
    public class CausticsOverlay : SkyOverlay
    {
        public Material Material;

        private UnderwaterBiomeSettings _Settings;
        private UnderwaterBiomeSettings Settings
        {
            get
            {
                if (_Settings == null)
                {
                    _Settings = LoadedModManager.GetMod<UnderwaterBiome>().GetSettings<UnderwaterBiomeSettings>();
                }

                return _Settings;
            }
        }
        public const string CausticShaderAssetName = "causticsshader";

        public CausticsOverlay()
        {
            this.Material = BundleLoader.CausticsMaterial;
            BundleLoader.UpdateCausticsMaterial();
        }

        public void UpdateZoom()
        {

            float scaleAtMaxZoom = 10f;
            float baseScale = 4f;
    

            // Get the current zoom value
  
            // Calculate the final zoom scale
            float zoomScale = Mathf.Lerp(baseScale, scaleAtMaxZoom, GetZoom());

            this.Material.SetFloat("_ZoomScale", zoomScale);
            this.Material.SetFloat("_GlobalScale", UnderwaterBiomeSettings.Caustics_GlobalScale * GetZoom());
        }

        private float GetZoom()
        {
            float minZoom = Find.CameraDriver.ZoomRootSize * (float)CameraZoomRange.Closest;
            float maxZoom = Find.CameraDriver.ZoomRootSize * (float)CameraZoomRange.Furthest;

            float currentZoom = Find.CameraDriver.ZoomRootSize * (float)Find.CameraDriver.CurrentZoom;



            var Settings = LoadedModManager.GetMod<UnderwaterBiome>().GetSettings<UnderwaterBiomeSettings>();
            float normalizedZoom = Mathf.Lerp(UnderwaterBiomeSettings.Caustics_ScaleAtMinHeight, UnderwaterBiomeSettings.Caustics_ScaleAtMaxHeight, Mathf.Clamp01(currentZoom / maxZoom));
            return normalizedZoom;
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

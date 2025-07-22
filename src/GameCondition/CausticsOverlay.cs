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
            this.Material.SetFloat("_GlobalScale", UnderwaterBiomeSettings.GlobalScale * GetZoom());
        }

        private float GetZoom()
        {
            float minZoom = Find.CameraDriver.ZoomRootSize * (float)CameraZoomRange.Closest;
            float maxZoom = Find.CameraDriver.ZoomRootSize * (float)CameraZoomRange.Furthest;

            float currentZoom = Find.CameraDriver.ZoomRootSize * (float)Find.CameraDriver.CurrentZoom;



            var Settings = LoadedModManager.GetMod<UnderwaterBiome>().GetSettings<UnderwaterBiomeSettings>();
            float normalizedZoom = Mathf.Lerp(UnderwaterBiomeSettings.ScaleAtMinHeight, UnderwaterBiomeSettings.ScaleAtMaxHeight, Mathf.Clamp01(currentZoom / maxZoom));
            return normalizedZoom;
        }


      
        public void UpdateMaterial()
        {
            // General settings
            this.Material.SetFloat("_Opacity", UnderwaterBiomeSettings.Opacity);
            this.Material.SetColor("_Color", UnderwaterBiomeSettings.Color);
            this.Material.SetColor("_ColorTwo", UnderwaterBiomeSettings.Color2);


            // Layer One settings
            this.Material.SetFloat("_LayerOneScrollSpeedX", UnderwaterBiomeSettings.LayerOneScrollX);
            this.Material.SetFloat("_LayerOneScrollSpeedY", UnderwaterBiomeSettings.LayerOneScrollY);
            this.Material.SetFloat("_LayerOneZoomScale", UnderwaterBiomeSettings.LayerOneZoomScale);

            // Layer Two settings
            this.Material.SetFloat("_LayerTwoSpeedX", UnderwaterBiomeSettings.LayerTwoScrollX);
            this.Material.SetFloat("_LayerTwoSpeedY", UnderwaterBiomeSettings.LayerTwiScrollY);
            this.Material.SetFloat("_LayerTwoZoomScale", UnderwaterBiomeSettings.LayerTwiZoomScale);

            // Voronoi settings
            this.Material.SetFloat("_VoronoiCellDensity", UnderwaterBiomeSettings.VoronoiCellDensity);
            this.Material.SetFloat("_VoronoiSpeed", UnderwaterBiomeSettings.VoronoiSpeed);
            this.Material.SetColor("_VoronoiColor1", UnderwaterBiomeSettings.VoronoiColorOne);
            this.Material.SetColor("_VoronoiColor2", UnderwaterBiomeSettings.VoronoiColorTwo);
            this.Material.SetFloat("_VoronoiMax", UnderwaterBiomeSettings.VoronoiMax);

            // Distortion settings
            this.Material.SetFloat("_EnableDistortion", UnderwaterBiomeSettings.EnableDistortion ? 1f : 0f);
            this.Material.SetFloat("_DistortionScale", UnderwaterBiomeSettings.DistortionScale);
            this.Material.SetFloat("_DistortionSpeedX", UnderwaterBiomeSettings.DistortionSpeedX);
            this.Material.SetFloat("_DistortionSpeedY", UnderwaterBiomeSettings.DistortionSpeedY);
            this.Material.SetFloat("_DistortionStrR", UnderwaterBiomeSettings.DistortionStrR);
            this.Material.SetFloat("_DistortionStrG", UnderwaterBiomeSettings.DistortionStrG);
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

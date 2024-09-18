using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class CausticsOverlay : SkyOverlay
    {
        public Shader Shader;
        public Texture2D MainTex;
        public Texture2D SecondTex;
        public Texture2D DistortTex;
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
            this.MainTex = ContentFinder<Texture2D>.Get("Layer1");
            this.SecondTex = ContentFinder<Texture2D>.Get("Layer2");
            this.DistortTex = ContentFinder<Texture2D>.Get("DistortionNoise4");
            this.Shader = LoadedModManager.GetMod<UnderwaterBiome>().GetShaderFromAssets(CausticShaderAssetName);

            if (this.Shader == null)
            {
                Log.Error($"Could not find shader {CausticShaderAssetName} in assets.");
                return;
            }

            this.Material = new Material(this.Shader);
            this.Material.SetTexture("_MainTex", this.MainTex);
            this.Material.SetTexture("_LayerTwo", this.SecondTex);
            this.Material.SetTexture("_DistortMap", this.DistortTex);


            this.Material.SetFloat("_Opacity", 0.14f);
            this.Material.SetFloat("_ScrollSpeed", 0.3f);
      
            this.Material.SetFloat("_DistortionSpeed", 0.04f);
            this.Material.SetFloat("_DistortionStrR", 0.06f);
            this.Material.SetFloat("_DistortionStrG", 0.06f);


            this.Material.SetColor("_Color", new Color(1, 1, 1));
            this.Material.SetColor("_Color2", new Color(1, 1,1));


            this.worldOverlayMat = this.Material;
        }

        public void UpdateZoom()
        {

            float scaleAtMaxZoom = 10f;
            float baseScale = 4f;
    

            // Get the current zoom value
  
            // Calculate the final zoom scale
            float zoomScale = Mathf.Lerp(baseScale, scaleAtMaxZoom, GetZoom());

            this.Material.SetFloat("_ZoomScale", zoomScale);
        }

        private float GetZoom()
        {
            float minZoom = 10f;
            float maxZoom = 60f;
            float currentZoom = Find.CameraDriver.ZoomRootSize;



            var Settings = LoadedModManager.GetMod<UnderwaterBiome>().GetSettings<UnderwaterBiomeSettings>();
            float normalizedZoom = Mathf.InverseLerp(Settings.MinScale, Settings.ScaleAtMaxZoom, currentZoom);
            return normalizedZoom;
        }


      
        public void UpdateMaterial()
        {
            // General settings
            this.Material.SetFloat("_Opacity", Settings.Opacity);
            this.Material.SetColor("_Color", Settings.Color);
            this.Material.SetColor("_ColorTwo", Settings.Color2);

            this.Material.SetFloat("_GlobalScale", Settings.GlobalScale);

            // Layer One settings
            this.Material.SetFloat("_LayerOneScrollX", Settings.LayerOneScrollX);
            this.Material.SetFloat("_LayerOneScrollY", Settings.LayerOneScrollY);
            this.Material.SetFloat("_LayerOneZoomScale", Settings.LayerOneZoomScale);

            // Layer Two settings
            this.Material.SetFloat("_LayerTwoSpeedX", Settings.LayerTwoScrollX);
            this.Material.SetFloat("_LayerTwoSpeedY", Settings.LayerTwiScrollY);
            this.Material.SetFloat("_LayerTwoZoomScale", Settings.LayerTwiZoomScale);

            // Voronoi settings
            this.Material.SetFloat("_VoronoiCellDensity", Settings.VoronoiCellDensity);
            this.Material.SetFloat("_VoronoiSpeed", Settings.VoronoiSpeed);
            this.Material.SetColor("_VoronoiColor1", Settings.VoronoiColorOne);
            this.Material.SetColor("_VoronoiColor2", Settings.VoronoiColorTwo);
            this.Material.SetFloat("_VoronoiMax", Settings.VoronoiMax);

            // Distortion settings
            this.Material.SetFloat("_EnableDistortion", Settings.EnableDistortion ? 1f : 0f);
            this.Material.SetFloat("_DistortionScale", Settings.DistortionScale);
            this.Material.SetFloat("_DistortionSpeedX", Settings.DistortionSpeedX);
            this.Material.SetFloat("_DistortionSpeedY", Settings.DistortionSpeedY);
            this.Material.SetFloat("_DistortionStrR", Settings.DistortionStrR);
            this.Material.SetFloat("_DistortionStrG", Settings.DistortionStrG);

            // Scale and zoom settings
            this.Material.SetFloat("_BaseScale", Settings.BaseScale);
            this.Material.SetFloat("_MinZoom", Settings.MinZoom);
            this.Material.SetFloat("_MaxZoom", Settings.MaxZoom);
            this.Material.SetFloat("_ScaleAtMaxZoom", Settings.ScaleAtMaxZoom);
            this.Material.SetFloat("_MinScale", Settings.MinScale);
        }
    }
}

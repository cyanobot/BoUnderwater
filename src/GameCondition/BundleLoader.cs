using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Verse;
using BiomesCore.ModSettings;

namespace BoUnderwater
{
    [StaticConstructorOnStartup]
    public static class BundleLoader
    {
        public const string CausticShaderAssetName = "causticsshader";

        public static Shader CausticsShader = GetShaderFromAssets(CausticShaderAssetName);
        public static Material CausticsMaterial = new Material(CausticsShader);

        public static Texture2D CausticsMainTex = ContentFinder<Texture2D>.Get("Layer1");
        public static Texture2D CausticsSecondTex = ContentFinder<Texture2D>.Get("Layer2");
        public static Texture2D CausticsDistortTex = ContentFinder<Texture2D>.Get("DistortionNoise");

        static BundleLoader()
        {
            if (CausticsShader == null)
            {
                Log.Error($"Could not find shader {CausticShaderAssetName} in assets.");
                return;
            }

            CausticsMaterial.SetTexture("_MainTex", CausticsMainTex);
            CausticsMaterial.SetTexture("_LayerTwo", CausticsSecondTex);
            CausticsMaterial.SetTexture("_DistortMap", CausticsDistortTex);


            CausticsMaterial.SetFloat("_Opacity", 0.14f);
            CausticsMaterial.SetFloat("_ScrollSpeed", 0.3f);

            CausticsMaterial.SetFloat("_DistortionSpeed", 0.04f);
            CausticsMaterial.SetFloat("_DistortionStrR", 0.06f);
            CausticsMaterial.SetFloat("_DistortionStrG", 0.06f);


            CausticsMaterial.SetColor("_Color", new Color(1, 1, 1));
            CausticsMaterial.SetColor("_Color2", new Color(1, 1, 1));
        }

        public static Shader GetShaderFromAssets(string shaderAssetName)
        {
            foreach (var bundle in UnderwaterBiome.mcp.assetBundles.loadedAssetBundles)
            {
                Shader shader = bundle.LoadAsset<Shader>(shaderAssetName);
                if (shader != null)
                {
                    return shader;
                }
            }

            Shader fallbackShader = Shader.Find(shaderAssetName);
            if (fallbackShader != null)
            {
                return fallbackShader;
            }


            Debug.LogWarning($"Shader '{shaderAssetName}' not found in any asset bundle or by name.");
            return ShaderDatabase.DefaultShader;
        }

        public static void UpdateCausticsMaterial()
        {
            // General settings
            CausticsMaterial.SetFloat("_Opacity", UnderwaterBiomeSettings.Caustics_Opacity);
            CausticsMaterial.SetColor("_Color", UnderwaterBiomeSettings.Caustics_Color);
            CausticsMaterial.SetColor("_ColorTwo", UnderwaterBiomeSettings.Caustics_Color2);

            // Layer One settings
            CausticsMaterial.SetFloat("_LayerOneScrollSpeedX", UnderwaterBiomeSettings.Caustics_LayerOneScrollX);
            CausticsMaterial.SetFloat("_LayerOneScrollSpeedY", UnderwaterBiomeSettings.Caustics_LayerOneScrollY);
            CausticsMaterial.SetFloat("_LayerOneZoomScale", UnderwaterBiomeSettings.Caustics_LayerOneZoomScale);

            // Layer Two settings
            CausticsMaterial.SetFloat("_LayerTwoSpeedX", UnderwaterBiomeSettings.Caustics_LayerTwoScrollX);
            CausticsMaterial.SetFloat("_LayerTwoSpeedY", UnderwaterBiomeSettings.Caustics_LayerTwoScrollY);
            CausticsMaterial.SetFloat("_LayerTwoZoomScale", UnderwaterBiomeSettings.Caustics_LayerTwoZoomScale);

            // Voronoi settings
            CausticsMaterial.SetFloat("_VoronoiCellDensity", UnderwaterBiomeSettings.Caustics_VoronoiCellDensity);
            CausticsMaterial.SetFloat("_VoronoiSpeed", UnderwaterBiomeSettings.Caustics_VoronoiSpeed);
            CausticsMaterial.SetColor("_VoronoiColor1", UnderwaterBiomeSettings.Caustics_VoronoiColorOne);
            CausticsMaterial.SetColor("_VoronoiColor2", UnderwaterBiomeSettings.Caustics_VoronoiColorTwo);
            CausticsMaterial.SetFloat("_VoronoiMax", UnderwaterBiomeSettings.Caustics_VoronoiMax);

            // Distortion settings
            CausticsMaterial.SetFloat("_EnableDistortion", UnderwaterBiomeSettings.Caustics_EnableDistortion ? 1f : 0f);
            CausticsMaterial.SetFloat("_DistortionScale", UnderwaterBiomeSettings.Caustics_DistortionScale);
            CausticsMaterial.SetFloat("_DistortionSpeedX", UnderwaterBiomeSettings.Caustics_DistortionSpeedX);
            CausticsMaterial.SetFloat("_DistortionSpeedY", UnderwaterBiomeSettings.Caustics_DistortionSpeedY);
            CausticsMaterial.SetFloat("_DistortionStrR", UnderwaterBiomeSettings.Caustics_DistortionStrR);
            CausticsMaterial.SetFloat("_DistortionStrG", UnderwaterBiomeSettings.Caustics_DistortionStrG);

        }
    }
}
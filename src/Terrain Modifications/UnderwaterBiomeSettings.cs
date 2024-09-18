using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class UnderwaterBiomeSettings : ModSettings
    {
        public float Opacity = 0.14f;
        public Color Color = Color.white;
        public Color Color2 = Color.white;
        public float GlobalScale = 1f;

        public float LayerOneScrollX = 0.06f;
        public float LayerOneScrollY = 0.02f;
        public float LayerOneZoomScale = -2.5f;
        public float LayerTwoScrollX = 0.02f;
        public float LayerTwiScrollY = -0.05f;
        public float LayerTwiZoomScale = 0.07f;
        public float VoronoiCellDensity = 10f;
        public float VoronoiSpeed = -0.04f;
        public Color VoronoiColorOne = Color.white;
        public Color VoronoiColorTwo = Color.white;
        public float VoronoiMax = 1f;
        public bool EnableDistortion = false;
        public float DistortionSpeedX = 0.01f;
        public float DistortionSpeedY = 0.01f;
        public float DistortionStrR = 0.06f;
        public float DistortionStrG = 0.06f;
        public float DistortionScale = 0.1f;
        public float BaseScale = 4f;
        public float MinZoom = 10f;
        public float MaxZoom = 60f;
        public float ScaleAtMaxZoom = 15f;
        public float MinScale = 4f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref Opacity, "Opacity", 0.14f);
            Scribe_Values.Look(ref Color, "Color", Color.white);
            Scribe_Values.Look(ref Color2, "Color2", Color.white);
            Scribe_Values.Look(ref GlobalScale, "GlobalScale", 1f);
            Scribe_Values.Look(ref LayerOneScrollX, "LayerOneScrollX", 0.06f);
            Scribe_Values.Look(ref LayerOneScrollY, "LayerOneScrollY", 0.02f);
            Scribe_Values.Look(ref LayerOneZoomScale, "LayerOneZoomScale", -2.5f);
            Scribe_Values.Look(ref LayerTwoScrollX, "LayerTwoScrollX", 0.02f);
            Scribe_Values.Look(ref LayerTwiScrollY, "LayerTwiScrollY", -0.05f);
            Scribe_Values.Look(ref LayerTwiZoomScale, "LayerTwiZoomScale", 0.07f);
            Scribe_Values.Look(ref VoronoiCellDensity, "VoronoiCellDensity", 10f);
            Scribe_Values.Look(ref VoronoiSpeed, "VoronoiSpeed", -0.04f);
            Scribe_Values.Look(ref VoronoiColorOne, "VoronoiColorOne", Color.white);
            Scribe_Values.Look(ref VoronoiColorTwo, "VoronoiColorTwo", Color.white);
            Scribe_Values.Look(ref VoronoiMax, "VoronoiMax", 1f);
            Scribe_Values.Look(ref EnableDistortion, "EnableDistortion", false);
            Scribe_Values.Look(ref DistortionScale, "DistortionScale", 0.1f);
            Scribe_Values.Look(ref DistortionSpeedX, "DistortionSpeedX", 0.01f);
            Scribe_Values.Look(ref DistortionSpeedY, "DistortionSpeedY", 0.01f);
            Scribe_Values.Look(ref DistortionStrR, "DistortionStrR", 0.06f);
            Scribe_Values.Look(ref DistortionStrG, "DistortionStrG", 0.06f);
            Scribe_Values.Look(ref BaseScale, "BaseScale", 4f);
            Scribe_Values.Look(ref MinZoom, "MinZoom", 10f);
            Scribe_Values.Look(ref MaxZoom, "MaxZoom", 60f);
            Scribe_Values.Look(ref ScaleAtMaxZoom, "ScaleAtMaxZoom", 15f);
            Scribe_Values.Look(ref MinScale, "MinScale", 4f);
            base.ExposeData();
        }
    }
}

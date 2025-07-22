using BiomesCore.ModSettings;
using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class UnderwaterBiomeSettings : ModSettings
    {
        //Caustics settings

        public static float Opacity = 0.14f;
        public static Color Color = Color.white;
        public static Color Color2 = Color.white;
        public static float GlobalScale = 1f;

        public static float LayerOneScrollX = 0.06f;
        public static float LayerOneScrollY = 0.02f;
        public static float LayerOneZoomScale = -2.5f;
        public static float LayerTwoScrollX = 0.02f;
        public static float LayerTwiScrollY = -0.05f;
        public static float LayerTwiZoomScale = 0.07f;
        public static float VoronoiCellDensity = 10f;
        public static float VoronoiSpeed = -0.04f;
        public static Color VoronoiColorOne = Color.white;
        public static Color VoronoiColorTwo = Color.white;
        public static float VoronoiMax = 1f;
        public static bool EnableDistortion = false;
        public static float DistortionSpeedX = 0.01f;
        public static float DistortionSpeedY = 0.01f;
        public static float DistortionStrR = 0.06f;
        public static float DistortionStrG = 0.06f;
        public static float DistortionScale = 0.1f;

        public static float ScaleAtMinHeight = 1f;
        public static float ScaleAtMaxHeight = 1f;

        //Caustics helpers
        private static Vector2 scrollPosition = Vector2.zero;
        private static RGBColorPicker colorPicker;
        private static RGBColorPicker colorPicker2;
        private static RGBColorPicker voronoiColorOnePicker;
        private static RGBColorPicker voronoiColorTwoPicker;


        public override void ExposeData()
        {

            //Caustics settings

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

            Scribe_Values.Look(ref ScaleAtMinHeight, "ScaleAtMinHeight",1f);
            Scribe_Values.Look(ref ScaleAtMaxHeight, "ScaleAtMaxHeight", 1f);

            base.ExposeData();
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect outRect = inRect;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, inRect.height * 4f); // Increased height multiplier
            colorPicker = new RGBColorPicker(Color);
            colorPicker2 = new RGBColorPicker(Color2);
            voronoiColorOnePicker = new RGBColorPicker(VoronoiColorOne);
            voronoiColorTwoPicker = new RGBColorPicker(VoronoiColorTwo);

            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(viewRect);

            listingStandard.GapLine();

            listingStandard.Label("Caustics Settings");

            // General settings
            listingStandard.Label("Opacity: " + Opacity.ToString("F2"));
            Opacity = listingStandard.Slider(Opacity, 0f, 1f);

            listingStandard.Label("Global Scale: " + GlobalScale.ToString("F2"));
            GlobalScale = listingStandard.Slider(GlobalScale, 0f, 1f);

            // Layer One settings
            listingStandard.Label("Layer One Scroll X: " + LayerOneScrollX.ToString("F2"));
            LayerOneScrollX = listingStandard.Slider(LayerOneScrollX, -1f, 1f);
            listingStandard.Label("Layer One Scroll Y: " + LayerOneScrollY.ToString("F2"));
            LayerOneScrollY = listingStandard.Slider(LayerOneScrollY, -1f, 1f);
            listingStandard.Label("Layer One Zoom Scale: " + LayerOneZoomScale.ToString("F2"));
            LayerOneZoomScale = listingStandard.Slider(LayerOneZoomScale, -5f, 5f);

            // Layer Two settings
            listingStandard.Label("Layer Two Scroll X: " + LayerTwoScrollX.ToString("F2"));
            LayerTwoScrollX = listingStandard.Slider(LayerTwoScrollX, -1f, 1f);
            listingStandard.Label("Layer Two Scroll Y: " + LayerTwiScrollY.ToString("F2"));
            LayerTwiScrollY = listingStandard.Slider(LayerTwiScrollY, -1f, 1f);
            listingStandard.Label("Layer Two Zoom Scale: " + LayerTwiZoomScale.ToString("F2"));
            LayerTwiZoomScale = listingStandard.Slider(LayerTwiZoomScale, -5f, 5f);

            // Voronoi settings
            listingStandard.Label("Voronoi Cell Density: " + VoronoiCellDensity.ToString("F2"));
            VoronoiCellDensity = listingStandard.Slider(VoronoiCellDensity, 1f, 50f);
            listingStandard.Label("Voronoi Speed: " + VoronoiSpeed.ToString("F2"));
            VoronoiSpeed = listingStandard.Slider(VoronoiSpeed, -1f, 1f);
            listingStandard.Label("Voronoi Max: " + VoronoiMax.ToString("F2"));
            VoronoiMax = listingStandard.Slider(VoronoiMax, 0f, 2f);

            // Distortion settings
            listingStandard.CheckboxLabeled("Enable Distortion", ref EnableDistortion);

            if (EnableDistortion)
            {
                listingStandard.Label("Distortion Speed X: " + DistortionSpeedX.ToString("F2"));
                DistortionSpeedX = listingStandard.Slider(DistortionSpeedX, -1f, 1f);

                listingStandard.Label("Distortion Speed Y: " + DistortionSpeedY.ToString("F2"));
                DistortionSpeedY = listingStandard.Slider(DistortionSpeedY, -1f, 1f);

                listingStandard.Label("Distortion Strength R: " + DistortionStrR.ToString("F2"));
                DistortionStrR = listingStandard.Slider(DistortionStrR, 0f, 01f);

                listingStandard.Label("Distortion Strength G: " + DistortionStrG.ToString("F2"));
                DistortionStrG = listingStandard.Slider(DistortionStrG, 0f, 1f);

                listingStandard.Label("Distortion Scale : " + DistortionScale.ToString("F2"));
                DistortionScale = listingStandard.Slider(DistortionScale, -1f, 1f);
            }

            // Scale and zoom settings
            listingStandard.Label("Scale At Min Height: " + ScaleAtMinHeight.ToString("F2"));
            ScaleAtMinHeight = listingStandard.Slider(ScaleAtMinHeight, 0f, 10f);
            listingStandard.Label("Scale At Max Height: " + ScaleAtMaxHeight.ToString("F2"));
            ScaleAtMaxHeight = listingStandard.Slider(ScaleAtMaxHeight, 0f, 10f);


            // Color pickers
            listingStandard.Gap();
            listingStandard.Label("Color");
            Rect colorRect = listingStandard.GetRect(30f);
            Color = colorPicker.Draw(colorRect);

            listingStandard.Gap();
            listingStandard.Label("Color 2");
            Rect colorRect2 = listingStandard.GetRect(30f);
            Color2 = colorPicker2.Draw(colorRect2);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color One");
            Rect voronoiColorOneRect = listingStandard.GetRect(30f);
            VoronoiColorOne = voronoiColorOnePicker.Draw(voronoiColorOneRect);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color Two");
            Rect voronoiColorTwoRect = listingStandard.GetRect(30f);
            VoronoiColorTwo = voronoiColorTwoPicker.Draw(voronoiColorTwoRect);

            listingStandard.End();
            Widgets.EndScrollView();

            BundleLoader.UpdateCausticsMaterial();
        }
    }
}

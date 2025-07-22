using BiomesCore.ModSettings;
using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class UnderwaterBiomeSettings : ModSettings
    {
        //Sky settings

        public static Color Sky_ColorDay = new Color(1f, 0.953f, 0.773f);   //visual 'lighting' for map
        public static Color Sky_ColorNight = new Color(0.8f, 0.9f, 1f);
        public static Color Sky_ColorShadowDay = new Color(0.596f, 0.902f, 1f);
        public static Color Sky_ColorShadowNight = new Color(0.6f, 0.75f, 1f);
        public static Color Sky_ColorOverlay = Color.white;     //color modifier on all weather/gamecondition overlays - not currently using this but may need later
        public static float Sky_Saturation = 1f;    //reduces saturation of sky color, probaby leave at 1 and adjust color directly
        public static float Sky_Glow = 0.7f;        //mechanical light level 

        //Sky helpers
        private static RGBColorPicker Sky_colorPicker_Day;
        private static RGBColorPicker Sky_colorPicker_Night;
        private static RGBColorPicker Sky_colorPicker_ShadowDay;
        private static RGBColorPicker Sky_colorPicker_ShadowNight;
        private static RGBColorPicker Sky_colorPicker_Overlay;

        //Caustics settings

        public static float Caustics_Opacity = 0.14f;
        public static Color Caustics_Color = Color.white;
        public static Color Caustics_Color2 = Color.white;
        public static float Caustics_GlobalScale = 1f;

        public static float Caustics_LayerOneScrollX = 0.06f;
        public static float Caustics_LayerOneScrollY = 0.02f;
        public static float Caustics_LayerOneZoomScale = -2.5f;
        public static float Caustics_LayerTwoScrollX = 0.02f;
        public static float Caustics_LayerTwoScrollY = -0.05f;
        public static float Caustics_LayerTwoZoomScale = 0.07f;
        public static float Caustics_VoronoiCellDensity = 10f;
        public static float Caustics_VoronoiSpeed = -0.04f;
        public static Color Caustics_VoronoiColorOne = Color.white;
        public static Color Caustics_VoronoiColorTwo = Color.white;
        public static float Caustics_VoronoiMax = 1f;
        public static bool Caustics_EnableDistortion = false;
        public static float Caustics_DistortionSpeedX = 0.01f;
        public static float Caustics_DistortionSpeedY = 0.01f;
        public static float Caustics_DistortionStrR = 0.06f;
        public static float Caustics_DistortionStrG = 0.06f;
        public static float Caustics_DistortionScale = 0.1f;

        public static float Caustics_ScaleAtMinHeight = 1f;
        public static float Caustics_ScaleAtMaxHeight = 1f;

        //Caustics helpers
        private static RGBColorPicker Caustics_colorPicker;
        private static RGBColorPicker Caustics_colorPicker2;
        private static RGBColorPicker Caustics_voronoiColorOnePicker;
        private static RGBColorPicker Caustics_voronoiColorTwoPicker;

        //Generic helpers
        private static Vector2 scrollPosition = Vector2.zero;


        public override void ExposeData()
        {
            //Sky settings

            Scribe_Values.Look(ref Sky_ColorDay, "Sky_ColorDay", Sky_ColorDay, true);
            Scribe_Values.Look(ref Sky_ColorNight, "Sky_ColorNight", Sky_ColorNight, true);
            Scribe_Values.Look(ref Sky_ColorShadowDay, "Sky_ColorShadowDay", Sky_ColorShadowDay, true);
            Scribe_Values.Look(ref Sky_ColorShadowNight, "Sky_ColorShadowNight", Sky_ColorShadowNight, true);
            Scribe_Values.Look(ref Sky_ColorOverlay, "Sky_ColorOverlay", Sky_ColorOverlay, true);
            Scribe_Values.Look(ref Sky_Saturation, "Sky_Saturation", Sky_Saturation, true);
            Scribe_Values.Look(ref Sky_Glow, "Sky_Glow", Sky_Glow);

            //Caustics settings

            Scribe_Values.Look(ref Caustics_Opacity, "Caustics_Opacity", Caustics_Opacity, true);
            Scribe_Values.Look(ref Caustics_Color, "Caustics_Color", Caustics_Color, true);
            Scribe_Values.Look(ref Caustics_Color2, "Caustics_Color2", Caustics_Color2, true);
            Scribe_Values.Look(ref Caustics_GlobalScale, "Caustics_GlobalScale", Caustics_GlobalScale, true);
            Scribe_Values.Look(ref Caustics_LayerOneScrollX, "Caustics_LayerOneScrollX", Caustics_LayerOneScrollX, true);
            Scribe_Values.Look(ref Caustics_LayerOneScrollY, "Caustics_LayerOneScrollY", Caustics_LayerOneScrollY, true);
            Scribe_Values.Look(ref Caustics_LayerOneZoomScale, "Caustics_LayerOneZoomScale", Caustics_LayerOneZoomScale, true);
            Scribe_Values.Look(ref Caustics_LayerTwoScrollX, "Caustics_LayerTwoScrollX", Caustics_LayerTwoScrollX, true);
            Scribe_Values.Look(ref Caustics_LayerTwoScrollY, "Caustics_LayerTwoScrollY", Caustics_LayerTwoScrollY, true);
            Scribe_Values.Look(ref Caustics_LayerTwoZoomScale, "Caustics_LayerTwoZoomScale", Caustics_LayerTwoZoomScale, true);
            Scribe_Values.Look(ref Caustics_VoronoiCellDensity, "Caustics_VoronoiCellDensity", Caustics_VoronoiCellDensity, true);
            Scribe_Values.Look(ref Caustics_VoronoiSpeed, "Caustics_VoronoiSpeed", Caustics_VoronoiSpeed, true);
            Scribe_Values.Look(ref Caustics_VoronoiColorOne, "Caustics_VoronoiColorOne", Caustics_VoronoiColorOne, true);
            Scribe_Values.Look(ref Caustics_VoronoiColorTwo, "Caustics_VoronoiColorTwo", Caustics_VoronoiColorTwo, true);
            Scribe_Values.Look(ref Caustics_VoronoiMax, "Caustics_VoronoiMax", Caustics_VoronoiMax, true);
            Scribe_Values.Look(ref Caustics_EnableDistortion, "Caustics_EnableDistortion", Caustics_EnableDistortion, true);
            Scribe_Values.Look(ref Caustics_DistortionSpeedX, "Caustics_DistortionSpeedX", Caustics_DistortionSpeedX, true);
            Scribe_Values.Look(ref Caustics_DistortionSpeedY, "Caustics_DistortionSpeedY", Caustics_DistortionSpeedY, true);
            Scribe_Values.Look(ref Caustics_DistortionStrR, "Caustics_DistortionStrR", Caustics_DistortionStrR, true);
            Scribe_Values.Look(ref Caustics_DistortionStrG, "Caustics_DistortionStrG", Caustics_DistortionStrG, true);
            Scribe_Values.Look(ref Caustics_DistortionScale, "Caustics_DistortionScale", Caustics_DistortionScale, true);

            Scribe_Values.Look(ref Caustics_ScaleAtMinHeight, "Caustics_ScaleAtMinHeight", Caustics_ScaleAtMinHeight, true);
            Scribe_Values.Look(ref Caustics_ScaleAtMaxHeight, "Caustics_ScaleAtMaxHeight", Caustics_ScaleAtMaxHeight, true);

            base.ExposeData();
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect outRect = inRect;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, inRect.height * 4f); // Increased height multiplier

            //colorpickers
            Sky_colorPicker_Day = new RGBColorPicker(Sky_ColorDay);
            Sky_colorPicker_Night = new RGBColorPicker(Sky_ColorNight);
            Sky_colorPicker_ShadowDay = new RGBColorPicker(Sky_ColorShadowDay);
            Sky_colorPicker_ShadowNight = new RGBColorPicker(Sky_ColorShadowNight);
            Sky_colorPicker_Overlay = new RGBColorPicker(Sky_ColorOverlay);

            Caustics_colorPicker = new RGBColorPicker(Caustics_Color);
            Caustics_colorPicker2 = new RGBColorPicker(Caustics_Color2);
            Caustics_voronoiColorOnePicker = new RGBColorPicker(Caustics_VoronoiColorOne);
            Caustics_voronoiColorTwoPicker = new RGBColorPicker(Caustics_VoronoiColorTwo);

            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(viewRect);

            //Sky settings

            listingStandard.GapLine();
            listingStandard.Label("'Sky' Settings");
            listingStandard.GapLine();

            listingStandard.Gap();
            listingStandard.Label("Sky Color Day");
            Rect colorRect_SkyDay = listingStandard.GetRect(30f);
            Sky_ColorDay = Sky_colorPicker_Day.Draw(colorRect_SkyDay);

            listingStandard.Gap();
            listingStandard.Label("Sky Color Night");
            Rect colorRect_SkyNight = listingStandard.GetRect(30f);
            Sky_ColorNight = Sky_colorPicker_Night.Draw(colorRect_SkyNight);

            listingStandard.Gap();
            listingStandard.Label("Shadow Color Day");
            Rect colorRect_SkyShadowDay = listingStandard.GetRect(30f);
            Sky_ColorShadowDay = Sky_colorPicker_ShadowDay.Draw(colorRect_SkyShadowDay);

            listingStandard.Gap();
            listingStandard.Label("Shadow Color Night");
            Rect colorRect_SkyShadowNight = listingStandard.GetRect(30f);
            Sky_ColorShadowNight = Sky_colorPicker_ShadowNight.Draw(colorRect_SkyShadowNight);

            listingStandard.Gap();
            listingStandard.Label("Overlay Color");
            Rect colorRect_SkyOverlay = listingStandard.GetRect(30f);
            Sky_ColorOverlay = Sky_colorPicker_Overlay.Draw(colorRect_SkyOverlay);

            listingStandard.Label("Saturation: " + Sky_Saturation.ToString("F2"));
            Sky_Saturation = listingStandard.Slider(Sky_Saturation, 0f, 1f);

            listingStandard.Label("Sky Glow: " + Sky_Glow.ToString("F2"));
            Sky_Glow = listingStandard.Slider(Sky_Glow, 0f, 1f);

            //Caustics settings

            listingStandard.GapLine();
            listingStandard.Label("Caustics Settings");
            listingStandard.GapLine();

            // General settings
            listingStandard.Label("Opacity: " + Caustics_Opacity.ToString("F2"));
            Caustics_Opacity = listingStandard.Slider(Caustics_Opacity, 0f, 1f);

            listingStandard.Label("Global Scale: " + Caustics_GlobalScale.ToString("F2"));
            Caustics_GlobalScale = listingStandard.Slider(Caustics_GlobalScale, 0f, 1f);

            // Layer One settings
            listingStandard.Label("Layer One Scroll X: " + Caustics_LayerOneScrollX.ToString("F2"));
            Caustics_LayerOneScrollX = listingStandard.Slider(Caustics_LayerOneScrollX, -1f, 1f);
            listingStandard.Label("Layer One Scroll Y: " + Caustics_LayerOneScrollY.ToString("F2"));
            Caustics_LayerOneScrollY = listingStandard.Slider(Caustics_LayerOneScrollY, -1f, 1f);
            listingStandard.Label("Layer One Zoom Scale: " + Caustics_LayerOneZoomScale.ToString("F2"));
            Caustics_LayerOneZoomScale = listingStandard.Slider(Caustics_LayerOneZoomScale, -5f, 5f);

            // Layer Two settings
            listingStandard.Label("Layer Two Scroll X: " + Caustics_LayerTwoScrollX.ToString("F2"));
            Caustics_LayerTwoScrollX = listingStandard.Slider(Caustics_LayerTwoScrollX, -1f, 1f);
            listingStandard.Label("Layer Two Scroll Y: " + Caustics_LayerTwoScrollY.ToString("F2"));
            Caustics_LayerTwoScrollY = listingStandard.Slider(Caustics_LayerTwoScrollY, -1f, 1f);
            listingStandard.Label("Layer Two Zoom Scale: " + Caustics_LayerTwoZoomScale.ToString("F2"));
            Caustics_LayerTwoZoomScale = listingStandard.Slider(Caustics_LayerTwoZoomScale, -5f, 5f);

            // Voronoi settings
            listingStandard.Label("Voronoi Cell Density: " + Caustics_VoronoiCellDensity.ToString("F2"));
            Caustics_VoronoiCellDensity = listingStandard.Slider(Caustics_VoronoiCellDensity, 1f, 50f);
            listingStandard.Label("Voronoi Speed: " + Caustics_VoronoiSpeed.ToString("F2"));
            Caustics_VoronoiSpeed = listingStandard.Slider(Caustics_VoronoiSpeed, -1f, 1f);
            listingStandard.Label("Voronoi Max: " + Caustics_VoronoiMax.ToString("F2"));
            Caustics_VoronoiMax = listingStandard.Slider(Caustics_VoronoiMax, 0f, 2f);

            // Distortion settings
            listingStandard.CheckboxLabeled("Enable Distortion", ref Caustics_EnableDistortion);

            if (Caustics_EnableDistortion)
            {
                listingStandard.Label("Distortion Speed X: " + Caustics_DistortionSpeedX.ToString("F2"));
                Caustics_DistortionSpeedX = listingStandard.Slider(Caustics_DistortionSpeedX, -1f, 1f);

                listingStandard.Label("Distortion Speed Y: " + Caustics_DistortionSpeedY.ToString("F2"));
                Caustics_DistortionSpeedY = listingStandard.Slider(Caustics_DistortionSpeedY, -1f, 1f);

                listingStandard.Label("Distortion Strength R: " + Caustics_DistortionStrR.ToString("F2"));
                Caustics_DistortionStrR = listingStandard.Slider(Caustics_DistortionStrR, 0f, 01f);

                listingStandard.Label("Distortion Strength G: " + Caustics_DistortionStrG.ToString("F2"));
                Caustics_DistortionStrG = listingStandard.Slider(Caustics_DistortionStrG, 0f, 1f);

                listingStandard.Label("Distortion Scale : " + Caustics_DistortionScale.ToString("F2"));
                Caustics_DistortionScale = listingStandard.Slider(Caustics_DistortionScale, -1f, 1f);
            }

            // Scale and zoom settings
            listingStandard.Label("Scale At Min Height: " + Caustics_ScaleAtMinHeight.ToString("F2"));
            Caustics_ScaleAtMinHeight = listingStandard.Slider(Caustics_ScaleAtMinHeight, 0f, 10f);
            listingStandard.Label("Scale At Max Height: " + Caustics_ScaleAtMaxHeight.ToString("F2"));
            Caustics_ScaleAtMaxHeight = listingStandard.Slider(Caustics_ScaleAtMaxHeight, 0f, 10f);


            // Color pickers
            listingStandard.Gap();
            listingStandard.Label("Color");
            Rect colorRect_caustics1 = listingStandard.GetRect(30f);
            Caustics_Color = Caustics_colorPicker.Draw(colorRect_caustics1);

            listingStandard.Gap();
            listingStandard.Label("Color 2");
            Rect colorRect_caustics2 = listingStandard.GetRect(30f);
            Caustics_Color2 = Caustics_colorPicker2.Draw(colorRect_caustics2);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color One");
            Rect colorRect_causticsVoronoi1 = listingStandard.GetRect(30f);
            Caustics_VoronoiColorOne = Caustics_voronoiColorOnePicker.Draw(colorRect_causticsVoronoi1);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color Two");
            Rect colorRect_causticsVoronoi2 = listingStandard.GetRect(30f);
            Caustics_VoronoiColorTwo = Caustics_voronoiColorTwoPicker.Draw(colorRect_causticsVoronoi2);

            listingStandard.End();
            Widgets.EndScrollView();

            BundleLoader.UpdateCausticsMaterial();
        }
    }
}

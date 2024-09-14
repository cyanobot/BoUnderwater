using UnityEngine;
using Verse;

namespace BoUnderwater
{

    [StaticConstructorOnStartup]
    public class UnderwaterBiome : Mod
    {
        UnderwaterBiomeSettings Settings;
        private Vector2 scrollPosition = Vector2.zero;
        private RGBColorPicker colorPicker;
        private RGBColorPicker colorPicker2;

        private RGBColorPicker voronoiColorOnePicker;
        private RGBColorPicker voronoiColorTwoPicker;

        public UnderwaterBiome(ModContentPack content) : base(content)
        {
            Settings = GetSettings<UnderwaterBiomeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect outRect = inRect;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, inRect.height * 4f); // Increased height multiplier
            colorPicker = new RGBColorPicker(Settings.Color);
            colorPicker2 = new RGBColorPicker(Settings.Color2);
            voronoiColorOnePicker = new RGBColorPicker(Settings.VoronoiColorOne);
            voronoiColorTwoPicker = new RGBColorPicker(Settings.VoronoiColorTwo);

            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(viewRect);

            // General settings
            listingStandard.Label("Opacity: " + Settings.Opacity.ToString("F2"));
            Settings.Opacity = listingStandard.Slider(Settings.Opacity, 0f, 1f);

            // Layer One settings
            listingStandard.Label("Layer One Scroll X: " + Settings.LayerOneScrollX.ToString("F2"));
            Settings.LayerOneScrollX = listingStandard.Slider(Settings.LayerOneScrollX, -1f, 1f);
            listingStandard.Label("Layer One Scroll Y: " + Settings.LayerOneScrollY.ToString("F2"));
            Settings.LayerOneScrollY = listingStandard.Slider(Settings.LayerOneScrollY, -1f, 1f);
            listingStandard.Label("Layer One Zoom Scale: " + Settings.LayerOneZoomScale.ToString("F2"));
            Settings.LayerOneZoomScale = listingStandard.Slider(Settings.LayerOneZoomScale, -5f, 5f);

            // Layer Two settings
            listingStandard.Label("Layer Two Scroll X: " + Settings.LayerTwoScrollX.ToString("F2"));
            Settings.LayerTwoScrollX = listingStandard.Slider(Settings.LayerTwoScrollX, -1f, 1f);
            listingStandard.Label("Layer Two Scroll Y: " + Settings.LayerTwiScrollY.ToString("F2"));
            Settings.LayerTwiScrollY = listingStandard.Slider(Settings.LayerTwiScrollY, -1f, 1f);
            listingStandard.Label("Layer Two Zoom Scale: " + Settings.LayerTwiZoomScale.ToString("F2"));
            Settings.LayerTwiZoomScale = listingStandard.Slider(Settings.LayerTwiZoomScale, -5f, 5f);

            // Voronoi settings
            listingStandard.Label("Voronoi Cell Density: " + Settings.VoronoiCellDensity.ToString("F2"));
            Settings.VoronoiCellDensity = listingStandard.Slider(Settings.VoronoiCellDensity, 1f, 50f);
            listingStandard.Label("Voronoi Speed: " + Settings.VoronoiSpeed.ToString("F2"));
            Settings.VoronoiSpeed = listingStandard.Slider(Settings.VoronoiSpeed, -1f, 1f);
            listingStandard.Label("Voronoi Max: " + Settings.VoronoiMax.ToString("F2"));
            Settings.VoronoiMax = listingStandard.Slider(Settings.VoronoiMax, 0f, 2f);

            // Distortion settings
            listingStandard.CheckboxLabeled("Enable Distortion", ref Settings.EnableDistortion);

            if (Settings.EnableDistortion)
            {
                listingStandard.Label("Distortion Speed X: " + Settings.DistortionSpeedX.ToString("F2"));
                Settings.DistortionSpeedX = listingStandard.Slider(Settings.DistortionSpeedX, -0.5f, 0.5f);
                listingStandard.Label("Distortion Speed Y: " + Settings.DistortionSpeedY.ToString("F2"));
                Settings.DistortionSpeedY = listingStandard.Slider(Settings.DistortionSpeedY, -0.5f, 0.5f);
                listingStandard.Label("Distortion Strength R: " + Settings.DistortionStrR.ToString("F2"));
                Settings.DistortionStrR = listingStandard.Slider(Settings.DistortionStrR, 0f, 0.5f);
                listingStandard.Label("Distortion Strength G: " + Settings.DistortionStrG.ToString("F2"));
                Settings.DistortionStrG = listingStandard.Slider(Settings.DistortionStrG, 0f, 0.5f);
            }

            // Scale and zoom settings
            listingStandard.Label("Base Scale: " + Settings.BaseScale.ToString("F2"));
            Settings.BaseScale = listingStandard.Slider(Settings.BaseScale, 1f, 10f);
            listingStandard.Label("Min Zoom: " + Settings.MinZoom.ToString("F2"));
            Settings.MinZoom = listingStandard.Slider(Settings.MinZoom, 1f, 30f);
            listingStandard.Label("Max Zoom: " + Settings.MaxZoom.ToString("F2"));
            Settings.MaxZoom = listingStandard.Slider(Settings.MaxZoom, 31f, 100f);
            listingStandard.Label("Scale At Max Zoom: " + Settings.ScaleAtMaxZoom.ToString("F2"));
            Settings.ScaleAtMaxZoom = listingStandard.Slider(Settings.ScaleAtMaxZoom, 0f, 100f);
            listingStandard.Label("Min Scale: " + Settings.MinScale.ToString("F2"));
            Settings.MinScale = listingStandard.Slider(Settings.MinScale, 0f, 100f);

            // Color pickers
            listingStandard.Gap();
            listingStandard.Label("Color");
            Rect colorRect = listingStandard.GetRect(30f);
            Settings.Color = colorPicker.Draw(colorRect);

            listingStandard.Gap();
            listingStandard.Label("Color 2");
            Rect colorRect2 = listingStandard.GetRect(30f);
            Settings.Color2 = colorPicker2.Draw(colorRect2);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color One");
            Rect voronoiColorOneRect = listingStandard.GetRect(30f);
            Settings.VoronoiColorOne = voronoiColorOnePicker.Draw(voronoiColorOneRect);

            listingStandard.Gap();
            listingStandard.Label("Voronoi Color Two");
            Rect voronoiColorTwoRect = listingStandard.GetRect(30f);
            Settings.VoronoiColorTwo = voronoiColorTwoPicker.Draw(voronoiColorTwoRect);

            listingStandard.End();
            Widgets.EndScrollView();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Underwater Biome";
        }
    }
}

using UnityEngine;
using Verse;

namespace BoUnderwater
{

    public class UnderwaterBiomeSettings : ModSettings
    {
        public float Opacity = 0.14f;
        public float ScrollSpeed = 0.3f;
        public float DistortionSpeed = 0.04f;
        public float DistortionStrR = 0.06f;
        public float DistortionStrG = 0.06f;
        public Color Color = Color.white;
        public Color Node9748Color = Color.white;
        public float BaseScale = 4f;
        public float MinZoom = 10f;
        public float MaxZoom = 60f;

        public float ScaleAtMaxZoom = 15f;
        public float MinScale = 4f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref Opacity, "Opacity", 0.14f);
            Scribe_Values.Look(ref ScrollSpeed, "ScrollSpeed", 0.3f);
            Scribe_Values.Look(ref DistortionSpeed, "DistortionSpeed", 0.04f);
            Scribe_Values.Look(ref DistortionStrR, "DistortionStrR", 0.06f);
            Scribe_Values.Look(ref DistortionStrG, "DistortionStrG", 0.06f);
            Scribe_Values.Look(ref Color, "Color", Color.white);
            Scribe_Values.Look(ref Node9748Color, "Node9748Color", Color.white);
            Scribe_Values.Look(ref BaseScale, "BaseScale", 4f);
            Scribe_Values.Look(ref MinZoom, "MinZoom", 10f);
            Scribe_Values.Look(ref MaxZoom, "MaxZoom", 60f);

            Scribe_Values.Look(ref ScaleAtMaxZoom, "ScaleAtMaxZoom", 15f);
            Scribe_Values.Look(ref MinScale, "MinScale",4f);
            base.ExposeData();
        }
    }

    [StaticConstructorOnStartup]
    public class UnderwaterBiome : Mod
    {
        UnderwaterBiomeSettings Settings;
        private Vector2 scrollPosition = Vector2.zero;
        private RGBColorPicker colorPicker;
        private RGBColorPicker node9748ColorPicker;
        public UnderwaterBiome(ModContentPack content) : base(content)
        {
            Settings = GetSettings<UnderwaterBiomeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect outRect = inRect;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, inRect.height * 1.5f);
            colorPicker = new RGBColorPicker(Settings.Color);
            node9748ColorPicker = new RGBColorPicker(Settings.Node9748Color);
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);

            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(viewRect);

            listingStandard.Label("Opacity: " + Settings.Opacity.ToString("F2"));
            Settings.Opacity = listingStandard.Slider(Settings.Opacity, 0f, 1f);
            listingStandard.Label("Scroll Speed: " + Settings.ScrollSpeed.ToString("F2"));
            Settings.ScrollSpeed = listingStandard.Slider(Settings.ScrollSpeed, 0f, 1f);
            listingStandard.Label("Distortion Speed: " + Settings.DistortionSpeed.ToString("F2"));
            Settings.DistortionSpeed = listingStandard.Slider(Settings.DistortionSpeed, 0f, 0.5f);
            listingStandard.Label("Distortion Strength R: " + Settings.DistortionStrR.ToString("F2"));
            Settings.DistortionStrR = listingStandard.Slider(Settings.DistortionStrR, 0f, 0.5f);
            listingStandard.Label("Distortion Strength G: " + Settings.DistortionStrG.ToString("F2"));
            Settings.DistortionStrG = listingStandard.Slider(Settings.DistortionStrG, 0f, 0.5f);

            listingStandard.Gap();
            listingStandard.Label("Color");
            Rect colorRect = listingStandard.GetRect(30f);
            Settings.Color = colorPicker.Draw(colorRect);

            listingStandard.Gap();
            listingStandard.Label("Node 9748 Color");
            Rect node9748ColorRect = listingStandard.GetRect(30f);
            Settings.Node9748Color = node9748ColorPicker.Draw(node9748ColorRect);

            listingStandard.Label("Base Scale: " + Settings.BaseScale.ToString("F2"));
            Settings.BaseScale = listingStandard.Slider(Settings.BaseScale, 1f, 10f);
            listingStandard.Label("Min Zoom: " + Settings.MinZoom.ToString("F2"));
            Settings.MinZoom = listingStandard.Slider(Settings.MinZoom, 1f, 30f);
            listingStandard.Label("Max Zoom: " + Settings.MaxZoom.ToString("F2"));
            Settings.MaxZoom = listingStandard.Slider(Settings.MaxZoom, 31f, 100f);


            listingStandard.Label("Overlay Scale at close range: " + Settings.MinScale.ToString("F2"));
            Settings.MinScale = listingStandard.Slider(Settings.MinScale, 0f, 100);

            listingStandard.Label("ScaleAtMaxZoom: " + Settings.ScaleAtMaxZoom.ToString("F2"));
            Settings.ScaleAtMaxZoom = listingStandard.Slider(Settings.ScaleAtMaxZoom, 0f, 100f);

            listingStandard.End();
            Widgets.EndScrollView();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Underwater Biome";
        }
    }

    public class RGBColorPicker
    {
        private Color currentColor;
        private Texture2D colorTexture;

        public RGBColorPicker(Color initialColor)
        {
            currentColor = initialColor;
            colorTexture = new Texture2D(64, 64);
        }

        public Color Draw(Rect rect)
        {
            const float columnWidth = 0.23f;
            const float textureWidth = 0.31f;

            Rect redRect = new Rect(rect.x, rect.y, rect.width * columnWidth, rect.height);
            Rect greenRect = new Rect(rect.x + rect.width * columnWidth, rect.y, rect.width * columnWidth, rect.height);
            Rect blueRect = new Rect(rect.x + rect.width * columnWidth * 2, rect.y, rect.width * columnWidth, rect.height);
            Rect textureRect = new Rect(rect.x + rect.width * columnWidth * 3, rect.y, rect.width * textureWidth, rect.height);

            currentColor.r = Widgets.HorizontalSlider(redRect, currentColor.r, 0f, 1f, false, "R: " + currentColor.r.ToString("F2"));
            currentColor.g = Widgets.HorizontalSlider(greenRect, currentColor.g, 0f, 1f, false, "G: " + currentColor.g.ToString("F2"));
            currentColor.b = Widgets.HorizontalSlider(blueRect, currentColor.b, 0f, 1f, false, "B: " + currentColor.b.ToString("F2"));

            UpdateColorTexture();
            GUI.DrawTexture(textureRect, colorTexture);

            return currentColor;
        }

        private void UpdateColorTexture()
        {
            for (int y = 0; y < colorTexture.height; y++)
            {
                for (int x = 0; x < colorTexture.width; x++)
                {
                    colorTexture.SetPixel(x, y, currentColor);
                }
            }
            colorTexture.Apply();
        }
    }
}

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
            base.ExposeData();
        }
    }

    [StaticConstructorOnStartup]
    public class UnderwaterBiome : Mod
    {
        UnderwaterBiomeSettings Settings;

        public UnderwaterBiome(ModContentPack content) : base(content)
        {
            Settings = GetSettings<UnderwaterBiomeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
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

            // Color sliders
            listingStandard.Label("Color");
            float r = Settings.Color.r;
            float g = Settings.Color.g;
            float b = Settings.Color.b;
            float a = Settings.Color.a;

            listingStandard.Label("R: " + r.ToString("F2"));
            r = listingStandard.Slider(r, 0f, 1f);
            listingStandard.Label("G: " + g.ToString("F2"));
            g = listingStandard.Slider(g, 0f, 1f);
            listingStandard.Label("B: " + b.ToString("F2"));
            b = listingStandard.Slider(b, 0f, 1f);
            listingStandard.Label("A: " + a.ToString("F2"));
            a = listingStandard.Slider(a, 0f, 1f);

            Settings.Color = new Color(r, g, b, a);

            // Node9748Color sliders
            listingStandard.Label("Node 9748 Color");
            float nr = Settings.Node9748Color.r;
            float ng = Settings.Node9748Color.g;
            float nb = Settings.Node9748Color.b;
            float na = Settings.Node9748Color.a;

            listingStandard.Label("R: " + nr.ToString("F2"));
            nr = listingStandard.Slider(nr, 0f, 1f);
            listingStandard.Label("G: " + ng.ToString("F2"));
            ng = listingStandard.Slider(ng, 0f, 1f);
            listingStandard.Label("B: " + nb.ToString("F2"));
            nb = listingStandard.Slider(nb, 0f, 1f);
            listingStandard.Label("A: " + na.ToString("F2"));
            na = listingStandard.Slider(na, 0f, 1f);

            Settings.Node9748Color = new Color(nr, ng, nb, na);

            listingStandard.Label("Base Scale: " + Settings.BaseScale.ToString("F2"));
            Settings.BaseScale = listingStandard.Slider(Settings.BaseScale, 1f, 10f);
            listingStandard.Label("Min Zoom: " + Settings.MinZoom.ToString("F2"));
            Settings.MinZoom = listingStandard.Slider(Settings.MinZoom, 1f, 30f);
            listingStandard.Label("Max Zoom: " + Settings.MaxZoom.ToString("F2"));
            Settings.MaxZoom = listingStandard.Slider(Settings.MaxZoom, 31f, 100f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Underwater Biome";
        }
    }
}

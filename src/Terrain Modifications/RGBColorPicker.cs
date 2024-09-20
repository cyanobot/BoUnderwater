using UnityEngine;
using Verse;

namespace BoUnderwater
{
    public class RGBColorPicker
    {
        private Color currentColor;
        private Texture2D colorTexture;
        const float columnWidth = 0.23f;
        const float textureWidth = 0.31f;

        public RGBColorPicker(Color initialColor)
        {
            currentColor = initialColor;
            colorTexture = new Texture2D(64, 64);
        }

        public Color Draw(Rect rect)
        {
            Rect redRect = new Rect(rect.x, rect.y, rect.width * columnWidth, rect.height);
            Rect greenRect = new Rect(rect.x + rect.width * columnWidth, rect.y, rect.width * columnWidth, rect.height);
            Rect blueRect = new Rect(rect.x + rect.width * columnWidth * 2, rect.y, rect.width * columnWidth, rect.height);
            Rect alphaRect = new Rect(rect.x + rect.width * columnWidth * 3, rect.y, rect.width * columnWidth, rect.height);
            Rect textureRect = new Rect(rect.x + rect.width * columnWidth * 4, rect.y, rect.width * textureWidth, rect.height);

            currentColor.r = Widgets.HorizontalSlider(redRect, currentColor.r, 0f, 1f, false, "R: " + currentColor.r.ToString("F2"));
            currentColor.g = Widgets.HorizontalSlider(greenRect, currentColor.g, 0f, 1f, false, "G: " + currentColor.g.ToString("F2"));
            currentColor.b = Widgets.HorizontalSlider(blueRect, currentColor.b, 0f, 1f, false, "B: " + currentColor.b.ToString("F2"));
            currentColor.a = Widgets.HorizontalSlider(alphaRect, currentColor.a, 0f, 1f, false, "A: " + currentColor.a.ToString("F2"));

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

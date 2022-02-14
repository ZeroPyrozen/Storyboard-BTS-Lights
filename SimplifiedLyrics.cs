using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System.Drawing;

namespace StorybrewScripts
{
    // Simple and clean Lyrics script by -Tochi
    // Text is generated as a whole sentence so you cannot adjust the parameters for each letter
    // Fore help, join our Discord server: https://discord.gg/QZjD3yb

    public class SimplifiedLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public string fontName = "Cocogoose Pro";

        [Configurable]
        public float opacity = 1f;
        
        [Configurable]
        public int fontScale = 30;

        [Configurable]
        public float spriteScale = 1f;

        [Configurable]
        public float fadeStartDelay = 1000;

        [Configurable]
        public float fadeEndDelay = 1000;

        [Configurable]
        public FontStyle fontStyle = FontStyle.Regular;

        [Configurable]
        public bool enableColor = false;

        [Configurable]
        public Color4 textColor = Color4.IndianRed;

        [Configurable]
        public bool enableGlow = false;

        [Configurable]
        public int glowRadius = 30;

        [Configurable]
        public Color4 glowColor = Color4.White;

        [Configurable]
        public int outlineThickness = 0;

        [Configurable]
        public Color4 outlineColor = Color4.Black;

        [Configurable]
        public int shadowThickness = 0;

        [Configurable]
        public Color4 shadowColor = Color4.Black;

        private FontGenerator font;

        public override void Generate()
        {
            font = FontGenerator("sb/lyrics");
            // You should write the lyrics in this function

            // The 'CreateText' function's parameters should be in this order (for reference):
            // CreateText(startTime, endTime, position, origin, text)
            var position = new Vector2(220,240);
            var origin = OsbOrigin.CentreLeft;
            fadeStartDelay = fadeEndDelay = (float)Beatmap.GetTimingPointAt(0).BeatDuration;
            // CreateText(0, 5012, position, origin, "電話や LINE を返せない日曜");
            // CreateText(5315, 9557, position, origin, "何も手につけれないよ");
            // CreateText(10088, 14557, position, origin, "たまにだけど嫌になるよ");
            // CreateText(15012, 18951, position, origin, "今が少し怖くなるんだ");

            // CreateText(19179, 23648, position, origin, "でも誰かを救える気もするんだ");
            // CreateText(24406, 27891, position, origin, "君の声は聞こえてる");
            // CreateText(28951, 33497, position, origin, "喧騒の中で時が止まる");
            // CreateText(33800, 39557, position, origin, "君と音で繋がる whoa");
        }

        FontGenerator FontGenerator(string output)
        {
            var font = LoadFont(output, new FontDescription()
            {
                FontPath = fontName,
                FontSize = fontScale,
                Color = Color4.White,
                Padding = Vector2.Zero,
                FontStyle = fontStyle,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow()
            {
                Radius = !enableGlow ? 0 : glowRadius,
                Color = glowColor,
            },
            new FontOutline()
            {
                Thickness = outlineThickness,
                Color = outlineColor,
            },
            new FontShadow()
            {
                Thickness = shadowThickness,
                Color = shadowColor,
            });

            return font;
        }

        // lyrics code
        public void CreateText(int startTime, int endTime, Vector2 position, OsbOrigin origin, string text)
        {
            var texture = font.GetTexture(text);
            var sprite = GetLayer("Lyrics").CreateSprite(texture.Path, origin, position);

            sprite.Scale(startTime, 0.5f * spriteScale);
            sprite.Fade(startTime, startTime + fadeStartDelay, 0, opacity);
            sprite.Fade(endTime, endTime + fadeEndDelay, opacity, 0);
            if (enableColor)
            sprite.Color(startTime, textColor);
        }
    }
}


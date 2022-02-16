using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class LightsBG : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            string BGPath = "sb/w.png";
		    var bgWhite = GetLayer("Background").CreateSprite(BGPath);
            var bgBitmap = GetMapsetBitmap(BGPath);
            bgWhite.Scale(0, Math.Max(854.0 / bgBitmap.Width, 480.0 / bgBitmap.Height));
            bgWhite.Fade(0,1);
            bgWhite.Fade(292853,0);
            string vPath = "sb/v.png";
		    var vWhite = GetLayer("Vignette").CreateSprite(vPath);
            var vBitmap = GetMapsetBitmap(vPath);
            vWhite.Scale(0, Math.Max(0.7,Math.Max(854.0 / vBitmap.Width, 480.0 / vBitmap.Height)));
            vWhite.Fade(0,1);
            vWhite.Fade(292853,0);

            var GreyColor = new Color4(76,72,72,1);
            var OrangeColor = new Color4(251, 176, 5,1);
            bgWhite.Color(0,Color4.White);
            SwipeTransition(10163,GreyColor);
            SwipeTransition(19254,GreyColor);
            SwipeTransition(28951,GreyColor);
            TileTransition(38042, 39709, OrangeColor, true);
            bgWhite.Color(39406,OrangeColor);
            TileTransition(48042, 49860, Color4.White, true);
            TileTransition(57739, 59557, Color4.White, true);
            SwipeTransition(68800,Color4.White);
            bgWhite.Color(78345, 78951, bgWhite.ColorAt(78345), Color4.White);
            SwipeTransition(80769, OrangeColor);
            SwipeTransition(98951, OrangeColor);

            SwipeTransition(108648, GreyColor);
            TileTransition(118042, 120163, OrangeColor, true);
            bgWhite.Color(119709, 119860, bgWhite.ColorAt(119709), OrangeColor);
            TileTransition(127891, 129860, Color4.White, true);
            TileTransition(137739, 139406, Color4.White, true);
            SwipeTransition(148800, Color4.White);
            bgWhite.Color(158345, 158951, bgWhite.ColorAt(158345), Color4.White);
            SwipeTransition(169103, OrangeColor);
            SwipeTransition(186224, GreyColor);
            SwipeTransition(205921, OrangeColor);
            SwipeTransition(215315, GreyColor);
            TileTransition(225012, 226527, OrangeColor, true);
            bgWhite.Color(225618, 226224, bgWhite.ColorAt(225618), OrangeColor);
            TileTransition(234709, 236376, Color4.White, true);
            TileTransition(244406, 246073, Color4.White, true);
            SwipeTransition(255315,Color4.White);
            bgWhite.Color(265012, 265618, bgWhite.ColorAt(265012), Color4.White);
            
        }
        private void SwipeTransition(int time, Color4 customColor)
        {
            string SpritePath = "sb/p/p.png";
            Color4 Color = customColor;
            var sprite = GetLayer("Transition").CreateSprite(SpritePath, OsbOrigin.Centre, new Vector2(320, 240));
            sprite.ScaleVec(OsbEasing.None, time - 800, time, 900, 700, 900, 700);
            sprite.Rotate(OsbEasing.None, time - 800, time, 0.2, 0.2);
            sprite.Move(OsbEasing.InExpo, time - 800, time, 1270, 240, 320, 240);
            sprite.Color(time, Color);  

            var sprite2 = GetLayer("Transition").CreateSprite(SpritePath, OsbOrigin.Centre, new Vector2(320, 240));
            sprite2.ScaleVec(OsbEasing.None, time, time + 800, 900, 700, 900, 700);
            sprite2.Rotate(OsbEasing.None, time, time + 800, 0.2, 0.2);
            sprite2.Move(OsbEasing.OutExpo, time, time + 800, 320, 240, -630, 240);
            sprite2.Color(time, Color);
        }

        private void SquareTileTransition(int startTime, int endTime, Color4 Color, Color4 NextColor, OsbEasing Easing = OsbEasing.None, double SquareScale = 100, int Distance = 100)
        {
            int transitionDuration = (int)Beatmap.GetTimingPointAt(0).BeatDuration;
            string SpritePath = "sb/p/p.png";
            for (int x = -120; x < 900; x += Distance)
            {
                for (int y = -120; y < 900; y += Distance)
                {
                    var sprite = GetLayer("TransitionSquares").CreateSprite(SpritePath, OsbOrigin.Centre, new Vector2(x, y));
                    sprite.ScaleVec(Easing, startTime, startTime + transitionDuration, 0, 0, SquareScale, SquareScale);
                    sprite.Fade(startTime, endTime, 1, 1);
                    sprite.Rotate(Easing, startTime, startTime + transitionDuration, 0.8, 0);
                    sprite.Color(startTime, Color);
                    sprite.Fade(endTime, endTime+transitionDuration,1,0);
                    sprite.Color(startTime, endTime,Color,NextColor);
                }
            }
        }

        private void TileTransition(int START_TIME, int END_TIME, Color4 squareColor, bool isFadeOut = false)
        {
            int gridSize = 18;
            var layer = GetLayer("TileBackground");
            for (int x = -120; x < 760; x += gridSize) {
                for (int y = 0; y < 480; y += gridSize) {
                    double distance = Math.Sqrt(Math.Pow((x-320),2.0) + Math.Pow((y-240),2.0));
                    double startTime = START_TIME + distance * 2;
                    var tile = layer.CreateSprite("sb\\square.png");
                    double duration = 100;
                    tile.Move(0, startTime, startTime + duration, x - 20, y, x, y);
                    tile.Rotate(0, startTime, startTime + duration, -1, 0);
                    tile.Scale(0, startTime, startTime + duration, 0, 1);
                    if(isFadeOut == true)
                        tile.Fade(0, END_TIME-299, END_TIME, 1, 0);
                    else
                        tile.Fade(0, END_TIME, END_TIME, 1, 0);
                    tile.Color(startTime, squareColor);
                }
            }
        }
    }
}

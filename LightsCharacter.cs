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
    public class LightsCharacter : StoryboardObjectGenerator
    {
        public StoryboardLayer charaLayer;
        public double BeatDuration = 0.0;

        public override void Generate()
        {
		    charaLayer = GetLayer("Character");
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            SpawnCharacter("JK", 0, 9860);
            SpawnCharacter("V", 10163, 18951);
            SpawnCharacter("JM", 19254, 28648);
            SpawnCharacter("JN", 28951, 38345);

            SpawnCharacter("JK", 39557, 49103);
            SpawnCharacter("V", 49557, 58951);

            SpawnCharacter("JN", 59103, 68648);
            SpawnCharacter("JM", 68800, 78042);

            SpawnCharacter("JH", 80769, 98951);
            
            SpawnCharacter("JK", 99103, 108042);
            SpawnCharacter("V", 108648, 119557);

            SpawnCharacter("JN", 119860, 129254);
            SpawnCharacter("JM", 129557, 138951);

            SpawnCharacter("JK", 139103, 148648);
            SpawnCharacter("V", 148951, 158648);

            SpawnCharacter("SG", 168951, 185618);
            SpawnCharacter("RM", 186224, 205618);

            SpawnCharacter("JM", 205921, 215163);
            SpawnCharacter("JK", 215315, 225921);

            SpawnCharacter("JN", 226224, 235921);
            SpawnCharacter("V", 235921, 245466);

            SpawnCharacter("JK", 245618, 255315);
            SpawnCharacter("JM", 255315, 265012);

        }

        void SpawnCharacter(string charaInitial, int startTime, int endTime, double customSection=1, double customSplit=3.0)
        {
            string characterPath = "sb/characters/"+charaInitial+".png";
            //Log(characterPath);
            customSection = Math.Min(customSplit, customSection);
            var origin = OsbOrigin.Centre;
            var xPosition = (854.0/customSplit-107.0)*customSection;
            if(customSection == 1)
            {
                origin = OsbOrigin.CentreRight;
            }
            else if(customSection == customSplit)
            {
                xPosition = 854.0-854.0/customSplit-107.0;
                origin = OsbOrigin.CentreLeft;
            }
            Log(854.0/customSplit-107.0);
            var charaSprite = charaLayer.CreateSprite(characterPath, origin);
            var charaBitmap = GetMapsetBitmap(characterPath);
            charaSprite.MoveX(startTime, xPosition);
            charaSprite.Scale(startTime, 0.35);
            charaSprite.Fade(startTime, startTime+BeatDuration, 0, 1);
            charaSprite.Fade(OsbEasing.Out, endTime, endTime+BeatDuration, 1, 0);
        }
    }
}

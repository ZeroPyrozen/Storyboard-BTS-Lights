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
    public class Loading : StoryboardObjectGenerator
    {
        public Dictionary<string, List<Vector2>> spritePositionCollections = new Dictionary<string, List<Vector2>>();
        public int BeatDuration = 0;
        [Configurable]
        public Vector2 LeftOffset = Vector2.Zero;
        [Configurable]
        public Vector2 RightOffset = Vector2.Zero;
        [Configurable]
        public double Speed = 1.0;
        public override void Generate()
        {
            BeatDuration = (int)Beatmap.GetTimingPointAt(0).BeatDuration;
            List<Vector2> JKPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(321, 57),
                new Vector2(186, 273),
                new Vector2(324, 191)
            };
            List<Vector2> VPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(319, 56),
                new Vector2(186, 255),
                new Vector2(305, 215)
            };
            List<Vector2> JMPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(316, 56),
                new Vector2(186, 228),
                new Vector2(260, 181)
            };
            List<Vector2> JNPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(319, 56),
                new Vector2(185, 240),
                new Vector2(270, 198)
            };
            List<Vector2> JHPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(291, 57),
                new Vector2(186, 269),
                new Vector2(306, 213)
            };
            List<Vector2> SGPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(300, 55),
                new Vector2(187, 260),
                new Vector2(274, 302)
            };
            List<Vector2> RMPositions = new List<Vector2>
            {
                new Vector2(186, 56),
                new Vector2(318, 56),
                new Vector2(186, 254),
                new Vector2(330, 195)
            };
            spritePositionCollections.Add("JK", JKPositions);
            spritePositionCollections.Add("V", VPositions);
            spritePositionCollections.Add("JM", JMPositions);
            spritePositionCollections.Add("JN", JNPositions);
            spritePositionCollections.Add("JH", JHPositions);
            spritePositionCollections.Add("SG", SGPositions);
            spritePositionCollections.Add("RM", RMPositions);
            double startTime = 265618;
            int i=1;
            foreach(var item in spritePositionCollections.Keys)
            {
		        SpawnLoading(item, startTime, startTime+4*(BeatDuration*Speed), i%2);
                    startTime += 4*(BeatDuration*Speed)+(BeatDuration*Speed);
                i++;
            }
            

        }

        void SpawnLoading(string characterInitial, double startTime, double endTime, int anchor)
        {
            Log(startTime + " " + endTime);
            int spriteCount = 4;
            var spritePosition = spritePositionCollections[characterInitial.ToUpper()];
            var currTime = startTime;
            for(int i=1; i<=spriteCount; i++)
            {
                string spritePath = "sb/loading/" + characterInitial + i.ToString() + ".png";
                Vector2 position = spritePosition[i-1];
                if(anchor == 1)
                    position += LeftOffset;
                else
                    position += RightOffset;
                var sprite = GetLayer("Loading").CreateSprite(spritePath, OsbOrigin.TopLeft, position);
                sprite.Scale(startTime, 0.35);
                sprite.Fade(currTime, currTime+BeatDuration*Speed,0, 1);
                sprite.Fade(endTime, endTime+BeatDuration*Speed, 1, 0);
                currTime += BeatDuration*Speed;
            }
        }
    }
}

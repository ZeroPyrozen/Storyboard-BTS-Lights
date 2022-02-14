using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class HighlightRing : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int FadeTime = 200;

        [Configurable]
        public string SpritePath = "sb/glow.png";

        [Configurable]
        public double SpriteScale = 1;

        [Configurable]
        public Color4 Color = new Color4(1, 1, 1, 0.6f);

        public override void Generate()
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var hSprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                hSprite.Scale(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + FadeTime, 0.4, 0.8);
                hSprite.Fade(OsbEasing.In, hitobject.StartTime, hitobject.StartTime + FadeTime, 0.4, 0);
                hSprite.Color(hitobject.StartTime, Color);

    
                }
            }
        }
    }


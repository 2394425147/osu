﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Modes.Objects;
using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Modes.Osu.Objects.Drawables;

namespace osu.Game.Modes.Osu.Objects
{
    public abstract class OsuHitObject : HitObject
    {
        public const double HITTABLE_RANGE = 300;
        public const double HIT_WINDOW_50 = 150;
        public const double HIT_WINDOW_100 = 80;
        public const double HIT_WINDOW_300 = 30;
        public const double OBJECT_RADIUS = 64;

        public Vector2 Position { get; set; }

        public Vector2 StackedPosition => Position + StackOffset;

        public virtual Vector2 EndPosition => Position;

        public Vector2 StackedEndPosition => EndPosition + StackOffset;

        public virtual int StackHeight { get; set; }

        public Vector2 StackOffset => new Vector2(StackHeight * Scale * -6.4f);

        public float Scale { get; set; } = 1;

        public abstract HitObjectType Type { get; }

        public double HitWindowFor(OsuScoreResult result)
        {
            switch (result)
            {
                default:
                    return 300;
                case OsuScoreResult.Hit50:
                    return 150;
                case OsuScoreResult.Hit100:
                    return 80;
                case OsuScoreResult.Hit300:
                    return 30;
            }
        }

        public override void SetDefaultsFromBeatmap(Beatmap beatmap)
        {
            base.SetDefaultsFromBeatmap(beatmap);

            Scale = (1.0f - 0.7f * (beatmap.BeatmapInfo.BaseDifficulty.CircleSize - 5) / 5) / 2;
        }
    }

    [Flags]
    public enum HitObjectType
    {
        Circle = 1 << 0,
        Slider = 1 << 1,
        NewCombo = 1 << 2,
        Spinner = 1 << 3,
        ColourHax = 122,
        Hold = 1 << 7,
        SliderTick = 1 << 8,
    }
}

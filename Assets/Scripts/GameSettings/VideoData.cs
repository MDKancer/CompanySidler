using System;
using Enums;

namespace GameSettings
{
    [Serializable]
    public class VideoData
    {
        public bool fullScreen = true;
        public Resolution resolution = Resolution._1920x1080;
        public bool antiAliasing = true;
        public float gamma = 0.8f;
        public Details textureQuality = Details.Medium;
        public Details shadowQuality = Details.Medium;
    }
}
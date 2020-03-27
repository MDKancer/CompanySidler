using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSettings
{
    [Serializable]
    public class VideoData
    {
        public bool fullScreen = true;
        public ScreenResolution screenResolution = ScreenResolution._1920x1080;
        public bool antiAliasing = true;
        public float gamma = 0.8f;
        public float brightness = 0.8f;
        public Details textureQuality = Details.Medium;
        public ShadowResolution shadowQuality = ShadowResolution.Medium;
    }
}
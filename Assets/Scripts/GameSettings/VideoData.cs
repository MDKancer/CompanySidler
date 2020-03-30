using System;
using Enums;
using UnityEngine;

namespace GameSettings
{
    /// <summary>
    /// all video settings
    /// </summary>
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
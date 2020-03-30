using Container;
using Enums;
using GameSettings;
using UnityEngine;
using Zenject;

namespace VideoManager
{
    public class VideoController
    {
        private SignalBus signalBus;
        private Cloud cloud;
        private VideoData videoData;
        [Inject]
        private void Init(SignalBus signalBus, Cloud cloud)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
        }

        public VideoData VideoData => videoData;

        public void SetImportData()
        {
            this.videoData = cloud.VideosData;
            SetFullScreen(videoData.fullScreen);
            SetResolution(videoData.screenResolution);
            SetAntiAliasing(videoData.antiAliasing);
            SetAmbientIntensity(videoData.gamma);
            SetBrightness(videoData.brightness);
            SetTextureQuality(videoData.textureQuality);
            SetShadowQuality(videoData.shadowQuality);
        }

        public void SetResolution(ScreenResolution resolution)
        {
            videoData.screenResolution = resolution;
            var res = GetResolution(videoData.screenResolution);
            Screen.SetResolution(res.width,res.height,videoData.fullScreen);
        }

        public void SetFullScreen(bool value)
        {
            videoData.fullScreen = value;
        }
        public void SetAntiAliasing(bool status)
        {
            videoData.antiAliasing = status;
            if (videoData.antiAliasing)
            {
                QualitySettings.antiAliasing = 2;
            }
            else
            {
                QualitySettings.antiAliasing = 0;
            }
        }

        public void SetAmbientIntensity(float gamma)
        {
            videoData.gamma = gamma;
            RenderSettings.ambientIntensity = videoData.gamma;
        }

        public void SetBrightness(float brightness)
        {
            videoData.brightness = brightness;
            Screen.brightness = videoData.brightness;
        }

        public void SetTextureQuality(Details details)
        {
            videoData.textureQuality = details;
            QualitySettings.masterTextureLimit = (int) videoData.textureQuality;
        }

        public void SetShadowQuality(ShadowResolution shadowQuality)
        {
            videoData.shadowQuality = shadowQuality;
            QualitySettings.shadowResolution = videoData.shadowQuality;
        }
        public void Reset()
        {
            cloud.SettingsDataReset();
        }
        
        private (int width, int height) GetResolution(ScreenResolution screenResolution)
        {
            switch (screenResolution)
            {
                case ScreenResolution._800x600:
                    return (800, 600);
                case ScreenResolution._1024x768:
                    return (1024, 768);
                case ScreenResolution._1280x720:
                    return (1280, 720);
                case ScreenResolution._1920x1080:
                    return (1920, 1080);
                case ScreenResolution._1920x1200:
                    return (1920, 1200);
                case ScreenResolution._2560x1440:
                    return (2560, 1440);
                case ScreenResolution._3840x2160:
                    return (3840, 2160);
            }
            return (800, 600);
        }
    }
}
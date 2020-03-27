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

        public void SetImportData()
        {
            this.videoData = cloud.VideosData;

            var resolution = GetResolution(videoData.screenResolution);
            Screen.SetResolution(resolution.width,resolution.height,videoData.fullScreen);

            if (videoData.antiAliasing)
            {
                QualitySettings.antiAliasing = 2;
            }
            else
            {
                QualitySettings.antiAliasing = 0;
            }
            RenderSettings.ambientIntensity = videoData.gamma;
            Screen.brightness = videoData.brightness;
            QualitySettings.masterTextureLimit = (int) videoData.textureQuality;
            QualitySettings.shadowResolution = videoData.shadowQuality;
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
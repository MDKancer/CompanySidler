
using Constants;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public class SceneManager
    {
        private Scenes lastScene;
        private Scenes currentScene;
        
        public void GoTo(Scenes scenes)
        {
            CurrentScene = scenes;
            UnityEngine.SceneManagement.SceneManager.LoadScene(scenes.ToString(),LoadSceneMode.Single);
        }

        public Scenes CurrentScene
        {
            get => currentScene;
            private set
            {
                lastScene = currentScene;
                currentScene = value;
            }
        }

        public Scenes LastScene => lastScene;

        public void SwitchToLastScene()
        {
            
            Scenes temp = currentScene;
            currentScene = lastScene;
            lastScene = temp;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.ToString(),LoadSceneMode.Single);
        }
    }
}
using UnityEngine;
using Zenject;

namespace Zenject_Initializer
{
    [CreateAssetMenu(fileName = "SceneBindings", menuName = "Installers/SceneBindings")]
    public class SceneBindings : ScriptableObjectInstaller<SceneBindings>
    {
        public override void InstallBindings()
        {
            // here will be only the Scene Bindings

        }
    }
}
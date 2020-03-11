using System;
using Sirenix.OdinInspector;
using Zenject;

namespace Zenject_Initializer
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        [ReadOnly,InfoBox(
                "ProjectContext Prefab should be in Resource Folder")]
        private readonly String message = "Assets/Resources/ProjectContext.asset";
        /// <summary>
        /// <remarks>ProjectContext Prefab should be in Resource Folder</remarks>
        /// <example>Assets/Resources/ProjectContext.asset</example>
        /// </summary>
        public override void InstallBindings()
        {
            ProjectSignalModule.Install(Container);
        }
    }
}
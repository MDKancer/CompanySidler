using Constants;
using GameCloud;
using StateMachine;
using UnityEngine;

namespace BootManager
{
    public class Boot : MonoBehaviour
    {
        public static Boot boot_Instance { get; private set; }

        public static MonoBehaviour monobehaviour;
        public static Container container;
        public static StateController<GameState> gameStateController;
        public static StateController<RuntimeState> runtimeStateController;

        void Awake()
        {
            if (boot_Instance == null)
            {
                boot_Instance = this;
                monobehaviour = this;
                container = new Container();
                gameStateController = new StateController<GameState>();
                runtimeStateController = new StateController<RuntimeState>();
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Booting();
            
        }
        void Start()
        {
            AllBegin();
        }
        public void Booting()
        {
          container.LoadAllResources();
        }
        public void AllBegin()
        {
            
        }
    }
}
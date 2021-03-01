using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Systems _systems;

    void Start()
    {
        // get a reference to the contexts
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("Systems").Add(new InputEmitterSystem(contexts)).Add(new PlayerControlledSpawnerSystem(contexts)).Add(new EntitySpawnerSystem(contexts)).Add(new LogSystem(contexts)).Add(new CameraMoverSystem(contexts));
        
        // call Initialize() on all of the IInitializeSystems
        _systems.Initialize();
    }

    void Update()
    {
        // call Execute() on all the IExecuteSystems and 
        // ReactiveSystems that were triggered last frame
        _systems.Execute();
        // call cleanup() on all the ICleanupSystems
        _systems.Cleanup();
    }
}
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerControlledSpawnerSystem : ReactiveSystem<InputEntity> {

    private GameContext _gameContext; 

    public PlayerControlledSpawnerSystem(Contexts context) : base(context.input){
        _gameContext = context.game;
    }
    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context){
        return context.CreateCollector(InputMatcher.MouseDown);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseDown && entity.isInput;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        GameObject prefab = Resources.Load<GameObject>("Sphere");

        foreach (var e in entities)
        {
            GameEntity spawned = _gameContext.CreateEntity();
            spawned.AddView(prefab);
            spawned.AddPosition(e.mouseDown.mousePosition);
        }
    }
}
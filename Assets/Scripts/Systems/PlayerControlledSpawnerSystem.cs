using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerControlledSpawnerSystem : ReactiveSystem<InputEntity> {

    private GameContext _gameContext; 
    private GameObject _prefab;

    public PlayerControlledSpawnerSystem(Contexts context) : base(context.input){
        _gameContext = context.game;
        _prefab = Resources.Load<GameObject>("Sphere");
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
        foreach (var e in entities)
        {
            GameEntity spawned = _gameContext.CreateEntity();
            spawned.AddView(_prefab);
            spawned.AddPosition(e.mouseDown.mousePosition);
            spawned.isGridElement = true;
        }
    }
}
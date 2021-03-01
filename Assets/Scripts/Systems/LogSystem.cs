using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class LogSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{

    readonly IGroup<GameEntity> _debugs;
    public LogSystem(Contexts context) : base(context.game){
        _debugs = context.game.GetGroup(GameMatcher.Debug);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.debug.message);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDebug;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Debug);
    }

    public void Cleanup(){
        foreach (var e in _debugs.GetEntities())
        {
            e.Destroy();
        }
    }
}

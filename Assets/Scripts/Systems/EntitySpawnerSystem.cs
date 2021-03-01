using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EntitySpawnerSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    public EntitySpawnerSystem(Contexts context) : base(context.game)
    {
        _context = context.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            GameObject toInstantiate = e.view.gameObject;
            Object.Instantiate(toInstantiate, e.position.value, Quaternion.identity);
            _context.CreateEntity().AddDebug("Spawned entity at "+e.position.value.ToString());
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasPosition;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.View);
    }
}
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EntitySpawnerSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private GameContext _context;
    private const float GRIDSIZE = 2.0f;
    public EntitySpawnerSystem(Contexts context) : base(context.game)
    {
        _context = context.game;
    }

    public void Initialize(){
        GameObject cellMarker = Resources.Load<GameObject>("CellMarker");

        for(int i = -51; i<49; i+=2){
            for(int j = -51; j<49; j+=2){
                Vector3 position = Vector3.zero;

                position.x = i;
                position.z = j;

                Object.Instantiate(cellMarker, position, Quaternion.identity);
            }
        }
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            GameObject toInstantiate = e.view.gameObject;


            if(e.isGridElement){
                e.position.value = PlaceOnGrid(e.position.value);
            }

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

    //Place the vector at the center of the cell
    private Vector3 PlaceOnGrid(Vector3 origin){
        Vector3 result = Vector3.zero;

        _context.CreateEntity().AddDebug("Origin position : "+origin.ToString());

        float mod = (origin.x%GRIDSIZE + GRIDSIZE)%GRIDSIZE;
        result.x = origin.x-mod+GRIDSIZE/2.0f;

        result.y = origin.y;

        mod = (origin.z%GRIDSIZE + GRIDSIZE)%GRIDSIZE;
        result.z = origin.z-mod+GRIDSIZE/2.0f;

        _context.CreateEntity().AddDebug("Result position : "+result.ToString());


        return result;
    }
}
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CameraMoverSystem : ReactiveSystem<InputEntity>
{

    private Camera _camera;

    private const float CAMERA_SPEED = 2f;
    public CameraMoverSystem(Contexts context): base(context.input){
        _camera = Camera.main;
    }
    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {

            //We want a faster camera the farther away we are from the ground
            if(e.hasHorizontalPosition){
                float sign = (e.horizontalPosition.isUpperBound?1.0f:-1.0f);
                _camera.transform.position += new Vector3(sign*Time.deltaTime*CAMERA_SPEED*_camera.transform.position.y, 0, 0);
            }

            if(e.hasVerticalPosition){
                float sign = (e.verticalPosition.isUpperBound?1.0f:-1.0f);
                _camera.transform.position += new Vector3(0, 0, sign*Time.deltaTime*CAMERA_SPEED*_camera.transform.position.y);
            }

            if(e.hasScroll){
                if((_camera.transform.position.y > 2 || e.scroll.delta < 0) && (_camera.transform.position.y < 50 || e.scroll.delta > 0)){
                    _camera.transform.position += _camera.transform.TransformDirection(Vector3.forward*e.scroll.delta);
                }
            }
        }
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasVerticalPosition || entity.hasHorizontalPosition || entity.hasScroll;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Input);
    }

}
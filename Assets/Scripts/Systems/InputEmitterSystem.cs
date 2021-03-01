using Entitas;
using UnityEngine;

public class InputEmitterSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem {

    readonly InputContext _context;
    readonly IGroup<InputEntity> _inputs;

    private const float SCREEN_BORDER_PERCENTAGE = 0.1f;

    private Plane _plane;
    public InputEmitterSystem(Contexts context){
        _context = context.input;
        _inputs = _context.GetGroup(InputMatcher.Input);
    }

    public void Initialize(){
        _plane = new Plane(Vector3.up, new Vector3(0, 0, 0));
    }

    public void Execute(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (_plane.Raycast(ray, out enter))
        {
            //Get the point on the plane corresponding to mouse position
            Vector3 hitPoint = ray.GetPoint(enter);

            InputEntity entity = _context.CreateEntity();

            entity.AddMousePosition(hitPoint);
            entity.isInput = true;


            if(Input.GetMouseButtonDown(0))
                entity.AddMouseDown(hitPoint);

            if(Input.GetMouseButton(0))
                entity.AddMousePressed(hitPoint);

            if(Input.GetMouseButtonUp(0))
                entity.AddMouseUp(hitPoint);
        }

        InputEntity screenBorderEntity = _context.CreateEntity();
        screenBorderEntity.isInput = true;

        if(Input.mousePosition.x < Screen.width*SCREEN_BORDER_PERCENTAGE){//Left of the screen
            screenBorderEntity.AddHorizontalPosition(false);
        } else if(Input.mousePosition.x > Screen.width - Screen.width*SCREEN_BORDER_PERCENTAGE){//Right of the screen
            screenBorderEntity.AddHorizontalPosition(true);
        }

        if(Input.mousePosition.y < Screen.height*SCREEN_BORDER_PERCENTAGE){//Bottom of the screen
            screenBorderEntity.AddVerticalPosition(false);
        } else if(Input.mousePosition.y > Screen.height - Screen.height*SCREEN_BORDER_PERCENTAGE){//Top of the screen
            screenBorderEntity.AddVerticalPosition(true);
        }

        InputEntity scrollEntity = _context.CreateEntity();
        scrollEntity.isInput = true;
        scrollEntity.AddScroll(Input.mouseScrollDelta.y);
    }

    public void Cleanup(){
        foreach (var e in _inputs.GetEntities())
        {
            e.Destroy();            
        }
    }
}
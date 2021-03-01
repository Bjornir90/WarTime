using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;


[Input]
public class MouseDownComponent : IComponent{
    public Vector3 mousePosition;
}

[Input]
public class MousePressedComponent : IComponent{
    public Vector3 mousePosition;
}

[Input]
public class MouseUpComponent : IComponent{
    public Vector3 mousePosition;
}

[Input]
public class MousePositionComponent : IComponent{
    public Vector3 mousePosition;
}

[Input]
public class InputComponent : IComponent{

}

[Input]
public class ScrollComponent : IComponent{
    public float delta;
}
using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Game]
public class ViewComponent : IComponent{
    public GameObject gameObject;
}

[Game]
public class PositionComponent : IComponent{
    public Vector3 value;
}
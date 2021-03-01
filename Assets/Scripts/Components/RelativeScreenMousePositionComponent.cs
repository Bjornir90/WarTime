using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input]
public class VerticalPositionComponent : IComponent{
    public bool isUpperBound;//Is top ?
}

[Input]
public class HorizontalPositionComponent : IComponent{
    public bool isUpperBound;//Is right ?
}
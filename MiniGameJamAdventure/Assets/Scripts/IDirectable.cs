using UnityEngine;

public interface IDirectable
{
    Vector2 Direction { get; }       
    Transform FoundTarget { get; }
}
using UnityEngine;

public interface IGraberable
{
    Rigidbody2D body { get; }
    bool isFixed { get; }
    bool Grab(GameObject graber);
    bool Drop(GameObject graber);
}
using UnityEngine;

public interface IBlock
{
    void SetName();
    void SetTitle();
    void Actuate();
    void ToggleEditMenu();
    void Upscale();
    void Downscale();
    void Closer();
    void Farther();
    void RotateLeft();
    void RotateRight();
    void RotateAllLeft();
    void RotateAllRight();
    void Setup();
    void ToSphere();
    void ToCube();
    void ToCylinder();
    void ToTable();
}

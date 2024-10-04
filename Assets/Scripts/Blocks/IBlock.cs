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
    void Setup();
}

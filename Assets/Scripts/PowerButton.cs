using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public PowerType powerType;
}

public enum PowerType
{
    Blade,
    Area,
    Bomb,
    ClearScreen,
    MaxHealth
}
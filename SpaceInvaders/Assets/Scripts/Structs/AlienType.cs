using UnityEngine;

public struct AlienType
{
    public readonly int lifes;
    public readonly int typeId;
    public readonly Color color;

    public AlienType(int typeId, int lifes, Color color)
    {
        this.typeId = typeId;
        this.lifes = lifes;
        this.color = color;
    }
}
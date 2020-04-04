using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTypeGetter
{
    AlienType[] alienTypes;

    public AlienTypeGetter()
    {
        alienTypes = new AlienType[4];
        alienTypes[0] = new AlienType(1, 2, Color.red);
        alienTypes[1] = new AlienType(2, 2, Color.yellow);
        alienTypes[2] = new AlienType(3, 1, Color.blue);
        alienTypes[3] = new AlienType(4, 1, Color.green);
    }

    public AlienType GetRandomAlienType()
    {
        return alienTypes[UnityEngine.Random.Range(0, alienTypes.Length)];
    }

}
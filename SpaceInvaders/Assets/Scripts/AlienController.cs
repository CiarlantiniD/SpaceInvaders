using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType
{
    public readonly Color color;
    public readonly int lifes;

    public AlienType(Color color, int lifes)
    {
        this.color = color;
        this.lifes = lifes;
    }
}

public class AlienTypeGetter
{
    AlienType[] alienTypes;

    public AlienTypeGetter()
    {
        alienTypes = new AlienType[4];
        alienTypes[0] = new AlienType(Color.red, 2);
        alienTypes[1] = new AlienType(Color.yellow, 2);
        alienTypes[2] = new AlienType(Color.blue, 1);
        alienTypes[3] = new AlienType(Color.green, 1);
    }

    public AlienType GetRandomAlienType()
    {
        return alienTypes[Random.Range(0, alienTypes.Length)];
    }

}


public class AlienController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private int lifes;
    private Color color;
    
    
    void Start()
    {
        AlienTypeGetter alienTypeGetter = new AlienTypeGetter();
        AlienType myAlienType = alienTypeGetter.GetRandomAlienType();

        lifes = myAlienType.lifes;
        color = myAlienType.color;

        sprite.color = color;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision ALIEN");
        
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}

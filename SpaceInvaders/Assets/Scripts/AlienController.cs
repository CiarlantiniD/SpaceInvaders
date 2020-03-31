using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        return alienTypes[UnityEngine.Random.Range(0, alienTypes.Length)];
    }

}


public class AlienController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    public float Width { get { return sprite.size.x; } }
    public float Height { get { return sprite.size.y; } }

    private int lifes;
    private Color color;

    public Vector2 PositioninMatrix { get; private set; }

    public Action<Vector2> OnDestroy;
    
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

    public void SetPositionInMatrix(int column, int row)
    {
        PositioninMatrix = new Vector2(column, row);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            Hit();
    }

    public void Hit()
    {
        if (lifes == 0)
            return;

        --lifes;

        if(lifes == 0)
        {
            StartCoroutine(DestroyAnimation());
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    IEnumerator DestroyAnimation()
    {
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.1f);
        OnDestroy?.Invoke(PositioninMatrix);
        Destroy(gameObject);
    }

}

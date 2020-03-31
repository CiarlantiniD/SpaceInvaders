using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlienType
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

public class AlienTypeGetter
{
    AlienType[] alienTypes;

    public AlienTypeGetter()
    {
        alienTypes = new AlienType[4];
        alienTypes[0] = new AlienType(1,2,Color.red);
        alienTypes[1] = new AlienType(2,2,Color.yellow);
        alienTypes[2] = new AlienType(3,1,Color.blue);
        alienTypes[3] = new AlienType(4,1,Color.green);
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
    [SerializeField] private Transform alienBullet;

    public float Width { get { return sprite.size.x; } }
    public float Height { get { return sprite.size.y; } }

    public int TypeID { get; private set; }
    public int Lifes { get; private set; }
    public Color Color { get; private set; }
    public bool IsDead { get; private set; }

    public Vector2 PositioninMatrix { get; private set; } = default;

    public Action<Vector2> OnDestroy;
    
    void Start()
    {
        AlienTypeGetter alienTypeGetter = new AlienTypeGetter();
        AlienType alienType = alienTypeGetter.GetRandomAlienType();

        TypeID = alienType.typeId;
        Lifes = alienType.lifes;
        Color = alienType.color;

        sprite.color = Color;
    }

    void Update()
    {
        
    }



    public void Shoot()
    {
        Instantiate(alienBullet, transform.localPosition, Quaternion.identity);
    }






    public void SetPositionInMatrix(int column, int row)
    {
        if (PositioninMatrix.Equals(default) == false)
            return;
        
        PositioninMatrix = new Vector2(column, row);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            Hit();
    }

    public void Hit()
    {
        if (Lifes == 0)
            return;

        --Lifes;

        if(Lifes == 0)
        {
            Destoy();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public void Destoy()
    {
        if (IsDead)
            return;

        IsDead = true;
        StartCoroutine(DestroyAnimation());
    }

    IEnumerator DestroyAnimation()
    {
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.1f);
        OnDestroy?.Invoke(PositioninMatrix);
        Destroy(gameObject);
    }

}

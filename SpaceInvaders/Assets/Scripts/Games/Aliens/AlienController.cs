using System.Collections;
using UnityEngine;
using System;

public class AlienController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform alienBullet = null;

    [SerializeField] private BoxCollider2D boxCollider = null;

    public float Width { get { return sprite.size.x; } }
    public float Height { get { return sprite.size.y; } }

    public int TypeID { get; private set; }
    public int Lifes { get; private set; }
    public Color Color { get; private set; }
    public bool IsAlive { get; private set; }

    private bool isPaused;

    public Vector2 PositioninMatrix { get; private set; } = default;

    public Action<Vector2> OnDestroyAlien;

    private AlienTypeGetter alienTypeGetter;


    void Start()
    {
        alienTypeGetter = new AlienTypeGetter();

        GameManager.OnPause += PauseAlienController;
        GameManager.OnUnpause += UnpauseAlienController;

        SetAlienType();
        IsAlive = true;
    }

    private void OnDestroy()
    {
        GameManager.OnPause -= PauseAlienController;
        GameManager.OnUnpause -= UnpauseAlienController;
    }


    public void OnReset()
    {
        SetAlienType();
        IsAlive = true;
        boxCollider.enabled = true;
        animator.SetTrigger("Reset");
    }


    private void SetAlienType()
    {
        AlienType alienType = alienTypeGetter.GetRandomAlienType();

        TypeID = alienType.typeId;
        Lifes = alienType.lifes;
        Color = alienType.color;

        sprite.color = Color;
    }




    public void Shoot()
    {
        Instantiate(alienBullet, transform.position, Quaternion.identity);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            Hit();

        if (collision.gameObject.tag == "Player")
            Destoy();
    }


    public void Hit()
    {
        if (Lifes == 0)
            return;

        --Lifes;

        if(Lifes == 0)
            Destoy();
        else
            animator.SetTrigger("Hit");
    }


    public void Destoy()
    {
        if (IsAlive == false)
            return;

        IsAlive = false;
        StartCoroutine(DestroyAnimation());
    }

    IEnumerator DestroyAnimation()
    {
        boxCollider.enabled = false;
        AudioManager.Instance.PlaySound(AudioManager.Sounds.ExplotionAlien);
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.1f);
        OnDestroyAlien?.Invoke(PositioninMatrix);
    }



    public void SetPositionInMatrix(int column, int row)
    {
        if (PositioninMatrix.Equals(default) == false)
            return;

        PositioninMatrix = new Vector2(column, row);
    }


    void PauseAlienController()
    {
        isPaused = true;
        animator.speed = 0;
    }

    void UnpauseAlienController()
    {
        isPaused = false;
        animator.speed = 1;
    }

}

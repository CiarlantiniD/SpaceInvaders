using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTranslation : MonoBehaviour
{
    [SerializeField] private int lateralMove = 18;
    [SerializeField] private float unidades = 0.05f;
    [SerializeField] private float startTimeIntervale = 1f;

    private Vector3 initialPosition;
    private bool isActive;    
    
    private float timeIntervale;

    private IEnumerator MoveAliensCoroutine;


    void Start()
    {
        initialPosition = transform.position;
    }


    public void StartTranslate()
    {
        timeIntervale = startTimeIntervale;
        isActive = true;
        MoveAliensCoroutine = MoveAliens();
        StartCoroutine(MoveAliensCoroutine);
    }

    public void StopTranslate()
    {
        isActive = false;
        timeIntervale = startTimeIntervale;
        StopCoroutine(MoveAliensCoroutine);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }



    private void Update()
    {
        if (isActive == false)
            return;

        if(timeIntervale > 0.04f)
            timeIntervale -= Time.deltaTime * 0.02f;
    }


    IEnumerator MoveAliens()
    {
        float down = -unidades * 5;

        for (int i = 0; i < lateralMove / 2; i++)
        {
            transform.Translate(unidades, 0, 0);
            yield return new WaitForSeconds(timeIntervale);
        }

        transform.Translate(0, down, 0);
        yield return new WaitForSeconds(timeIntervale);

        while (true)
        {
            for (int i = 0; i < lateralMove; i++)
            {
                transform.Translate(-unidades, 0, 0);
                yield return new WaitForSeconds(timeIntervale);
            }

            transform.Translate(0, down, 0);
            yield return new WaitForSeconds(timeIntervale);

            for (int i = 0; i < lateralMove; i++)
            {
                transform.Translate(unidades, 0, 0);
                yield return new WaitForSeconds(timeIntervale);
            }

            transform.Translate(0, down, 0);
            yield return new WaitForSeconds(timeIntervale);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTranslation : MonoBehaviour
{
    private Vector3 initialPosition;
    private float inicitalIntervale;
    private bool isActive;

    private int lateralMove = 18;
    private float unidades = 0.05f;
    private float timeIntervale = 1f;

    private IEnumerator MoveAliensCoroutine;


    void Start()
    {
        initialPosition = transform.position;
        inicitalIntervale = timeIntervale;
    }


    public void StartTranslate()
    {
        isActive = true;
        MoveAliensCoroutine = MoveAliens();
        StartCoroutine(MoveAliensCoroutine);
    }

    public void StopTranslate()
    {
        isActive = false;
        timeIntervale = inicitalIntervale;
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

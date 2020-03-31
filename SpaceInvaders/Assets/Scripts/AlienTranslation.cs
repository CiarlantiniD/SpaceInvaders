using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTranslation : MonoBehaviour
{

    private int lateralMove = 14;

    private float unidades = 0.05f;

    private float timeIntervale = 1f;

    void Start()
    {
        StartCoroutine(MoveAliens());
    }


    private void Update()
    {
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

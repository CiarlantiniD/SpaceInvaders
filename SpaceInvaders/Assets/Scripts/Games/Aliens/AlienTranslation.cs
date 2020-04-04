using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienTranslation : MonoBehaviour
{
    [SerializeField] private int lateralMove = 18;
    [SerializeField] private float unidades = 0.05f;
    [SerializeField] private float startTimeIntervale = 1f;
    [Space]
    [SerializeField] private AnimationCurve animationCurve;

    private Vector3 initialPosition;
    private bool isActive;
    private bool isPaused;

    private float timeIntervale;
    private float factor = 0;

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

    public void PauseMove()
    {
        isPaused = true;
    }

    public void UnpauseMove()
    {
        isPaused = false;
    }


    private void Update()
    {
        if (isActive == false)
            return;

        factor += Time.deltaTime * 0.02f;
        timeIntervale = Mathf.Lerp(0.04f, startTimeIntervale, animationCurve.Evaluate(factor));
    }


    IEnumerator MoveAliens()
    {
        float down = -unidades * 5;

        for (int i = 0; i < lateralMove / 2; i++)
        {
            transform.Translate(unidades, 0, 0);
            yield return new WaitForSeconds(timeIntervale);
            while (isPaused) { yield return null; }
        }

        transform.Translate(0, down, 0);
        yield return new WaitForSeconds(timeIntervale);
        while (isPaused) { yield return null; }

        while (true)
        {
            for (int i = 0; i < lateralMove; i++)
            {
                transform.Translate(-unidades, 0, 0);
                yield return new WaitForSeconds(timeIntervale);
                while (isPaused) { yield return null; }
            }

            transform.Translate(0, down, 0);
            yield return new WaitForSeconds(timeIntervale);
            while (isPaused) { yield return null; }

            for (int i = 0; i < lateralMove; i++)
            {
                transform.Translate(unidades, 0, 0);
                yield return new WaitForSeconds(timeIntervale);
                while (isPaused) { yield return null; }
            }

            transform.Translate(0, down, 0);
            yield return new WaitForSeconds(timeIntervale);
            while (isPaused) { yield return null; }
        }
    }

}

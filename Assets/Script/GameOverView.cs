using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    public AnimationCurve GameOvercurve;
    public Transform victoryView;
    public Transform defeatView;

    IEnumerator Anim;

    public void VictoryView()
    {
        Anim = VictoryAnim();
        StartCoroutine(Anim);
    }

    public void DefeatView()
    {
        Anim = DefectAnim();
        StartCoroutine(Anim);
    }

    IEnumerator VictoryAnim()
    {
        float timer = 0;
        while (timer <= 1)
        {
            victoryView.localScale = new Vector3(GameOvercurve.Evaluate(timer), GameOvercurve.Evaluate(timer), 0);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DefectAnim()
    {
        float timer = 0;
        while (timer <= 1)
        {
            defeatView.localScale = new Vector3(GameOvercurve.Evaluate(timer), GameOvercurve.Evaluate(timer), 0);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void OnBthChick()
    {
        SceneManager.LoadScene(0);
    }
}

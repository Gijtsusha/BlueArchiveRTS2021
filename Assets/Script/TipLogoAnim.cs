using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipLogoAnim : MonoBehaviour
{
    public RectTransform tipLogo;

    public AnimationCurve animLine;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        tipLogo.position = new Vector3(860, 580 + animLine.Evaluate(timer)*10, 0);
        timer += Time.deltaTime;

    }

}

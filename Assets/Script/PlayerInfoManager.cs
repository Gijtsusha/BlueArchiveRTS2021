using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    public Sprite[] Icons;
    public Image IconUI;

    public int Gem;
    public int Gold;

    public TMP_Text GemText;
    public TMP_Text APText;
    IEnumerator GetG;

    // Start is called before the first frame update
    void Start()
    {
        IconUI.sprite = Icons[PlayerType.playerType];
        Gem = 120;
        Gold = 0;       //ÔÝÎÞ×÷ÓÃ
        GetG = GetGem();
        StartCoroutine(GetG);
    }

    // Update is called once per frame
    void Update()
    {
        GemText.text = Gem.ToString();
        APText.text = Gold.ToString();
    }

    IEnumerator GetGem()
    {
        float timer = 0;
        while(true)
        {
            if (timer >= 1)
            {
                timer -= 1;
                Gem += 10;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        
    }
}

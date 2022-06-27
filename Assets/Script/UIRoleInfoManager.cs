using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRoleInfoManager : MonoBehaviour
{
    public Image image;
    public TMP_Text nameText;
    public TMP_Text dataText;
    public TMP_Text data2Text;

    public TextAsset InfoFile;
    public List<GameObject> students = new List<GameObject>();
    public Dictionary<GameObject, string> InfoDic = new Dictionary<GameObject, string>();
    protected string RoleID;

    public string[] infoRows;
    protected Actor actor;

    private void Awake()
    {
        InfoDic[students[0]] = "T01";
        InfoDic[students[1]] = "T02";
        InfoDic[students[2]] = "T03";
        InfoDic[students[3]] = "T04";
        InfoDic[students[4]] = "T05";
        InfoDic[students[5]] = "G01";
        InfoDic[students[6]] = "G02";
        InfoDic[students[7]] = "G03";
        InfoDic[students[8]] = "G04";
        InfoDic[students[9]] = "M01";
        InfoDic[students[10]] = "M02";
        InfoDic[students[11]] = "M03";
        InfoDic[students[12]] = "M04";
        InfoDic[students[13]] = "M05";

    }


    public void GetID(GameObject _role)
    {
        actor = _role.GetComponent<Actor>();
        RoleID = InfoDic[_role];
        GetInfo(InfoFile);
    }

    void GetInfo(TextAsset _textAsset)
    {
        string[] allRoleInfoRows = _textAsset.text.Split('\n');
        for (int i = 0; i < allRoleInfoRows.Length; i++)
        {
            string[] cells = allRoleInfoRows[i].Split(',');
            if (cells[0] == RoleID)
            {
                infoRows = cells;
                break;
            }
        }
        if (infoRows != null)
        {
            SetInfo();
        }
    }

    void SetInfo()
    {
        image.sprite = actor.roleImage;
        nameText.text = infoRows[1];
        dataText.text = "ÉËº¦:" + infoRows[2] + "¹¥»÷¾àÀë:" + infoRows[3] + "ÉúÃüÖµ:" + infoRows[4] + "ÒÆËÙ:" + infoRows[5];
        data2Text.text = "";
        string[] cells = infoRows[6].Split(' ');
        foreach(string cell in cells)
        {
            data2Text.text += cell + '\n';
        }
    }
}

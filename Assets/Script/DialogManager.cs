using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public TextAsset dialogDataFile;

    public Transform spriteLeft;
    public Transform spriteMid;
    public Transform spriteRight;
    [SerializeField] string LeftName;
    [SerializeField] string MidName;
    [SerializeField] string RightName;

    public TMP_Text nameText;
    public TMP_Text dialogText;

    public List<GameObject> students = new List<GameObject>();

    Dictionary<string, GameObject> imageDic = new Dictionary<string, GameObject>();

    public int dialogIndex;
    public string[] dialogRows;

    public GameObject optionButton;
    public Transform buttonGroup;


    private void Awake()
    {
        imageDic["“¡¬¿≤®"] = students[0];
        imageDic["«ÁƒŒ"] = students[1];
        imageDic["∑„œ„"] = students[2];
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadText(dialogDataFile);
        ShowDialogRow();
        //UpdateText("“¡¬¿≤®", "∏Ò∫’ƒ»—ß‘∫");
        // UpdateImage("«ÁƒŒ", "R");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buttonGroup.childCount == 0)
        {
            ShowDialogRow();
        }
    }

    void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    void UpdateImage(string _name, string _Pos, int _faceIndex)
    {
        if (_Pos == "L")
        {
            ClearOld(_name);
            GameObject role = Instantiate(imageDic[_name], spriteLeft);
            LeftName = _name;
            ChangeFace(role, spriteLeft, _faceIndex);
        }
        else if (_Pos == "M")
        {
            ClearOld(_name);
            GameObject role = Instantiate(imageDic[_name], spriteMid);
            MidName = _name;
            ChangeFace(role, spriteMid, _faceIndex);
        }
        else if (_Pos == "R")
        {
            ClearOld(_name);
            GameObject role = Instantiate(imageDic[_name], spriteRight);
            RightName = _name;
            ChangeFace(role, spriteRight, _faceIndex);
        }
    }

    void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        Debug.Log("readed");
    }

    void ShowDialogRow()
    {
        for(int i=0; i<dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split('\t');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[5]);
                UpdateImage(cells[2], cells[4], int.Parse(cells[3]));

                dialogIndex = int.Parse(cells[6]);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                GenerateOption(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                int _index = SceneManager.GetActiveScene().buildIndex;
                if (_index == 1 || _index == 2)
                {
                    SceneManager.LoadScene(3);
                }
                else if (_index == 3)
                {
                    SceneManager.LoadScene(4);
                }
                else if(_index == 5 || _index == 6)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split('\t');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[5];
            button.GetComponent<Button>().onClick.AddListener(delegate()
            {
                OnOptionChick(int.Parse(cells[6]));
            });
            GenerateOption(_index + 1);
        }
        
    }

    void OnOptionChick(int _id)
    {
        int length = buttonGroup.childCount;
        for (int i = 0; i < length; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
        dialogIndex = _id;
        ShowDialogRow();
    }

    void ChangeFace(GameObject role, Transform _parent, int _faceIndex)
    {
        GameObject face = role.GetComponentInChildren<FaceManager>().gameObject;
        face.GetComponent<SpriteRenderer>().sprite = face.GetComponent<FaceManager>().FindFace(_faceIndex);
    }

    void ClearOld(string _name)
    {
        if (LeftName == _name)
        {
            DelectImage(spriteLeft);
            LeftName = null;
        }
        if(MidName == _name)
        {
            DelectImage(spriteMid);
            MidName = null;
        }
        if(RightName == _name)
        {
            DelectImage(spriteRight);
            RightName = null;
        }
        
    }

    void DelectImage(Transform Pos)
    {
        int length = Pos.childCount;
        for (int i = 0; i < length; i++)
        {
            Destroy(Pos.GetChild(i).gameObject);
        }

    }
}

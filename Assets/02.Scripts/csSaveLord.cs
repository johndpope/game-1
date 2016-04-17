using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csSaveLord : MonoBehaviour
{  
    //아이템 구입시 인벤토리 셋팅 오브젝트
    public GameObject itemUseGrid;  //아이템 그리드
    public GameObject skillUseGrid; //스킬 그리드
    public GameObject magicUseGrid; //마법 그리드
    public GameObject buffUseGrid;  //버프 그리드
    //아이템 인벤토리 셋팅
    public GameObject itemUseSetObj;
    public GameObject itemUseName;
    public GameObject itemUseExplain;
    public GameObject itemUseImage;

    GameObject itemUse;

    Text itemUseNameText;
    Text itemUseExplainText;

    void Start()
    {
        itemUseNameText = itemUseName.GetComponent<Text>();
        itemUseExplainText = itemUseExplain.GetComponent<Text>();

        Debug.Log(StateManager.Instance.potionNum.Length);
        for (int i = 0; i < StateManager.Instance.potionNum.Length; i++)
        {
            StateManager.Instance.potionNum[i] = PlayerPrefs.GetInt("potion" + i.ToString());            
            PotionItem item = (PotionItem)StateManager.Instance.potionItems[i];
            if (StateManager.Instance.potionNum[i] > 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                GameObject itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(itemUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Potion" + i;

                StateManager.Instance.potionItemBag[i] = itemUseSet;
                GameObject.Find("Potion" + i).transform.FindChild("Scrollcnt").GetComponent<Text>().text = "보 유 갯 수:" + StateManager.Instance.potionNum[i] + " 개";
                StateManager.Instance.potionItemBag[i].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.potionNum[i] + " 개";
            }
        }

        for (int i = 0; i < StateManager.Instance.SkscrollNum.Length; i++)
        {
            PlayerPrefs.GetInt("skill" + i.ToString(), StateManager.Instance.SkscrollNum[i]);
        }

        for (int i = 0; i < StateManager.Instance.MgscrollNum.Length; i++)
        {
            PlayerPrefs.GetInt("magic" + i.ToString(), StateManager.Instance.MgscrollNum[i]);
        }

        for (int i = 0; i < StateManager.Instance.BufscrollNum.Length; i++)
        {
            PlayerPrefs.GetInt("potion" + i.ToString(), StateManager.Instance.BufscrollNum[i]);
        }
    }


    public void SaveData()
    {
        for (int i = 0; i < StateManager.Instance.potionNum.Length; i++)
        {
            Debug.Log(StateManager.Instance.potionNum[i]);
            PlayerPrefs.SetInt("potion" + i.ToString(), StateManager.Instance.potionNum[i]);
        }

        for (int i = 0; i < StateManager.Instance.SkscrollNum.Length; i++)
        {
            PlayerPrefs.SetInt("skill" + i.ToString(), StateManager.Instance.SkscrollNum[i]);
        }

        for (int i = 0; i < StateManager.Instance.MgscrollNum.Length; i++)
        {
            PlayerPrefs.SetInt("magic" + i.ToString(), StateManager.Instance.MgscrollNum[i]);
        }

        for (int i = 0; i < StateManager.Instance.BufscrollNum.Length; i++)
        {
            PlayerPrefs.SetInt("potion" + i.ToString(), StateManager.Instance.BufscrollNum[i]);
        }
    }
}

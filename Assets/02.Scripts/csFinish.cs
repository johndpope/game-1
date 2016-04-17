using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csFinish : MonoBehaviour
{
    public GameObject finishGold;
    public GameObject finishItem;
    public GameObject finushItemImage;

    void OnEnable()
    {
        finushItemImage.GetComponent<Image>().sprite = (Sprite)Resources.Load("Base1", typeof(Sprite));
        finishGold.GetComponent<Text>().text = "획득 골드\n:";
        finishItem.GetComponent<Text>().text = "획득 아이템\n:";
    }
}

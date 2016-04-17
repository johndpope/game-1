using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using itemPool;


public class csdungeonManager : MonoBehaviour
{
    public GameObject levelgrid;
    public GameObject floor;
    public GameObject label;
    Text floorText;

    public TextAsset textAsset;

    GameObject[] floorPoolSet = new GameObject[30];


    private ArrayList nLevel;
    ArrayList itemInfoLevel;
    void LoadAssetfromJson()
    {
        nLevel = new ArrayList();
        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
             itemInfoLevel = (ArrayList)itemTable["Level"];
            
            foreach (Hashtable itemInfo in itemInfoLevel)
            {

                String levelName = (String)itemInfo["level"];
                String rocks = (String)itemInfo["rock"];
                String boxs = (String)itemInfo["boxs"];

                Level levelValue = new Level();
                levelValue.Name = levelName;
                levelValue.Rock = Int32.Parse(rocks);
                levelValue.Boxs = Int32.Parse(boxs);

                nLevel.Add(levelValue);
            }
        }
    }
    void Start ()
    {
        LoadAssetfromJson();
        floorText = label.GetComponent<Text>();

        for (int i = 0; i < itemInfoLevel.Count; i++)
        {
            _setupFloor(i);
        }
    }
	
	void Update ()
    {

    }

    public void Floor(int level)
    {

        StateManager.Instance.dungeonMap= level;
        StateManager.Instance.dungeonLevel = level;
        Application.LoadLevel(1);
    }

    private void _setupFloor(int levelNum)
    {
        Level level = (Level)nLevel[levelNum];
        floorText.text = "" + level.Name;
        floorPoolSet[levelNum] = Instantiate(floor) as GameObject;
        floorPoolSet[levelNum].transform.SetParent(levelgrid.transform);
        floorPoolSet[levelNum].transform.localScale = new Vector3(1, 1, 1);

        floorPoolSet[levelNum].GetComponent<Button>().onClick.AddListener(delegate { Floor(levelNum); });

        floorPoolSet[levelNum].SetActive(false);
    }

    public void firstFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(true);
            //floorPoolSet[i+9].SetActive(false);
            //floorPoolSet[i+18].SetActive(false);
        }
        
    }
    public void secondFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(false);
            floorPoolSet[i + 9].SetActive(true);
            floorPoolSet[i + 18].SetActive(false);
        }
    }
    public void thirdFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(false);
            floorPoolSet[i + 9].SetActive(false);
            floorPoolSet[i + 18].SetActive(true);
        }
    }
}

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using itemPool;

class Maps : HMItem
{
    private int mWidthRoad;
    private int mHeightRoad;
    private int mRotationRoad;
    private int mCrossRoad;
    private int mTcrossRoad;
    private int mRoom;
    
    public int WidthRoad
    {
        get { return mWidthRoad; }
        set { mWidthRoad = value; }
    }
    public int HeightRoad
    {
        get { return mHeightRoad; }
        set { mHeightRoad = value; }
    }
    public int RotationRoad
    {
        get { return mRotationRoad; }
        set { mRotationRoad = value; }
    }
    public int CrossRoad
    {
        get { return mCrossRoad; }
        set { mCrossRoad = value; }
    }
    public int TcrossRoad
    {
        get { return mTcrossRoad; }
        set { mTcrossRoad = value; }
    }

    public int Room
    {
        get { return mRoom; }
        set { mRoom = value; }
    }
}

class Level : HMItem
{
    private int mRock;
    private int mBoxs;

    public int Rock
    {
        get { return mRock; }
        set { mRock = value; }
    }
    public int Boxs
    {
        get { return mBoxs; }
        set { mBoxs = value; }
    }
}

public class csDungeon : MonoBehaviour
{

    private ArrayList nMaps;
    private ArrayList nLevel;
    public TextAsset textAsset;

    GameObject map;
    int levelRock;
    public GameObject rock;

    public Transform[] map1 = new Transform[16];

    void LoadAssetfromJson()
    {
        nMaps = new ArrayList();
        nLevel = new ArrayList();
        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            ArrayList itemInfoMaps = (ArrayList)itemTable["map"];
            ArrayList itemInfoLevel = (ArrayList)itemTable["Level"];

            foreach (Hashtable itemInfo in itemInfoMaps)
            {
                String mapName = (String)itemInfo["mapName"];
                String widthRoad = (String)itemInfo["wRoad_"];
                String heightRoad = (String)itemInfo["hRoad_"];
                String rotationRoad = (String)itemInfo["rRoad_"];
                String room = (String)itemInfo["room_"];
                String tcrossRoad = (String)itemInfo["tRoad_"];
                String crossRoad = (String)itemInfo["cRoad_"];

                Maps mapValue = new Maps();
                mapValue.Name = mapName;
                mapValue.WidthRoad = Int32.Parse(widthRoad);
                mapValue.HeightRoad = Int32.Parse(heightRoad);
                mapValue.RotationRoad = Int32.Parse(rotationRoad);
                mapValue.CrossRoad = Int32.Parse(crossRoad);
                mapValue.TcrossRoad = Int32.Parse(tcrossRoad);
                mapValue.Room = Int32.Parse(room);

                nMaps.Add(mapValue);
            }

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

    // Use this for initialization
    void Start ()
    {
        LoadAssetfromJson();
        Maps maps = (Maps)nMaps[0];
        Level level = (Level)nLevel[0];
        levelRock = level.Rock;
        
        map = GameObject.Find(maps.Name);


        int j=1;
        for (int i = 0; i < map.transform.childCount; ++i)
        {
            //if (map.transform.GetChild(i).name == "rRoad_" + j)
            //{
               // map1[j-1] = map.transform.GetChild(i);
            map1[i] = map.transform.GetChild(i);
            // j++;
            //}
        }

        for (int i = 0; i < map.transform.childCount; i++)
        {
            int f = UnityEngine.Random.Range(1, (maps.RotationRoad + 1));

            Debug.Log("랜덤 값"+f);
            Debug.Log("i 값" + i);

            Rocks(i, f);

        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Rocks(int num, int random)
    {
        if (map1[random+2].name == "rRoad_" + random)
        {
            if (levelRock > 0)
            {
                GameObject gameObj = Instantiate(rock) as GameObject;
                gameObj.transform.parent = map1[random + 2];
                gameObj.transform.position = map1[random + 2].transform.position;
                Debug.Log("들어옴");
                levelRock--; Debug.Log("남은 돌 갯수" + levelRock);
            }
        }
    }
}

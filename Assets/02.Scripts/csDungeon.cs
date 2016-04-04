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

    public GameObject player;

    private ArrayList nMaps;
    private ArrayList nLevel;

    public TextAsset textAsset;

    //현제 맵
    GameObject map;
    //래벨의 돌겟수를 저장하는 integer
    int levelRock;
    //생성할 돌 GameObject
    public GameObject rock;
    //래벨의 보물 겟수를 저장하는 integer
    int levelTreasure;
    //생성할 보물 GameObject
    public GameObject treasure;

    //rRoad의 위치 값을 저장하는 배열
    public Transform[] rRoad;
    int rRoadPool;

    //wRoad의 위치 값을 저장하는 배열
    public Transform[] wRoad;
    int wRoadPool;

    //hRoad의 위치 값을 저장하는 배열
    public Transform[] hRoad;
    int hRoadPool;

    //tRoad의 위치 값을 저장하는 배열
    public Transform[] tRoad;
    int tRoadPool;

    //cRoad의 위치 값을 저장하는 배열
    public Transform[] cRoad;
    int cRoadPool;

    //room의 위치 값을 저장하는 배열
    public Transform[] room;
    int roomPool;

    Maps mapObj;
    Level level;

    int randomSetRock = 0;
    int randomSetTreasure = 0;

    ArrayList itemInfoMaps;
    ArrayList itemInfoLevel;

    void LoadAssetfromJson()
    {
        nMaps = new ArrayList();
        nLevel = new ArrayList();
        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            itemInfoMaps = (ArrayList)itemTable["map"];
            itemInfoLevel = (ArrayList)itemTable["Level"];

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
        
        if (Application.loadedLevel == 0)
        {
            return;
        }
     
        mapObj = (Maps)nMaps[StateManager.Instance.dungeonMap];
        //mapObj = (Maps)nMaps[0];
        //level = (Level)nLevel[StateManager.Instance.dungeonLevel];
        level = (Level)nLevel[0];

        //래벨에 따른 돌의 겟수를 저장한다.
        levelRock = level.Rock;
        Debug.Log(levelRock);
        //래벨에 따른 보물의 겟수를 저장한다.
        levelTreasure = level.Boxs;
        Debug.Log(levelTreasure);
        //json데이터의 RotationRoad의 게수를 가져와서 pool의 겟수를 정한다.
        rRoadPool = mapObj.RotationRoad;
        rRoad = new Transform[rRoadPool];
        Debug.Log(rRoadPool);
        //json데이터의 WidthRoad의 게수를 가져와서 pool의 겟수를 정한다.
        wRoadPool = mapObj.WidthRoad;
        wRoad = new Transform[wRoadPool];

        //json데이터의 HeightRoad의 게수를 가져와서 pool의 겟수를 정한다.
        hRoadPool = mapObj.HeightRoad;
        hRoad = new Transform[hRoadPool];

        //json데이터의 TcrossRoad의 게수를 가져와서 pool의 겟수를 정한다.
        tRoadPool = mapObj.TcrossRoad;
        tRoad = new Transform[tRoadPool];

        //json데이터의 CrossRoad의 게수를 가져와서 pool의 겟수를 정한다.
        cRoadPool = mapObj.CrossRoad;
        cRoad = new Transform[cRoadPool];

        //json데이터의 Room의 게수를 가져와서 pool의 겟수를 정한다.
        roomPool = mapObj.Room;
        room = new Transform[roomPool];

        //json데이터의 맵이름을 가져와서 찾는다.
        //map = GameObject.Find("4x4_1");

        //원래사용
        map = (GameObject)Resources.Load(mapObj.Name, typeof(GameObject));
        GameObject mapCap = Instantiate(map) as GameObject;
        mapCap.transform.position = new Vector3(0, 0, 0);

        int r = 1;
        int w = 1;
        int h = 1;
        int c = 1;
        int t = 1;
        int rm = 1;

        for (int i = 0; i < map.transform.childCount; ++i)
        {
            Debug.Log(map.transform.childCount);
            if (map.transform.GetChild(i).tag == "Start")
            {
                Transform point = map.transform.GetChild(i).transform.FindChild("startPoint");
                player.transform.position = point.position;
                player.transform.rotation = point.rotation;
            }

            if (map.transform.GetChild(i).name == "rRoad_" + r && mapObj.RotationRoad !=0)
            {
                Debug.Log("들어옴 플레이어");
                rRoad[r - 1] = map.transform.GetChild(i);
                r++;
            }

            else if (map.transform.GetChild(i).name == "wRoad_" + w && mapObj.WidthRoad != 0)
            {
                Debug.Log("들어옴 플레이어");
                wRoad[w - 1] = map.transform.GetChild(i);
                w++;
            }

            else if (map.transform.GetChild(i).name == "hRoad_" + h && mapObj.HeightRoad != 0)
            {
                Debug.Log("들어옴 플레이어");
                hRoad[h - 1] = map.transform.GetChild(i);
                h++;
            }

            else if (map.transform.GetChild(i).name == "tRoad_" + t && mapObj.TcrossRoad != 0)
            {
                tRoad[t - 1] = map.transform.GetChild(i);
                t++;
            }

            else if (map.transform.GetChild(i).name == "cRoad_" + c && mapObj.CrossRoad != 0)
            {
                cRoad[c - 1] = map.transform.GetChild(i);
                c++;
            }

            else if (map.transform.GetChild(i).name == "room_" + rm && mapObj.Room != 0)
            {
                room[rm - 1] = map.transform.GetChild(i);
                rm++;
            }

           
        }


        
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Application.loadedLevel == 0)
        {
            return;
        }

        if (levelTreasure > 0)
        {
            int roomNum = UnityEngine.Random.Range(1, (mapObj.Room + 1));
            for (int i = 0; i < room.Length; i++)
            {
                if (room.Length == 0)
                {
                    return;
                }
                Treasure(room, i, roomNum, "room_");
            }
        }


        if (levelRock > 0)
        {
            int num = UnityEngine.Random.Range(0, 4);
            
            switch(num)
            {
                case 0:
                    int rRoadNum = UnityEngine.Random.Range(1, (mapObj.RotationRoad + 1));
                    for (int i = 0; i < rRoad.Length; i++)
                    {
                        if (rRoad.Length == 0)
                        {
                            return;
                        }
                        Rocks(rRoad, i, rRoadNum, "rRoad_");
                    }
                    break;

                case 1:
                    int wRoadNum = UnityEngine.Random.Range(1, (mapObj.WidthRoad + 1));
                    for (int i = 0; i < wRoad.Length; i++)
                    {
                        if (wRoad.Length == 0)
                        {
                            return;
                        }
                        Rocks(wRoad, i, wRoadNum, "wRoad_");
                    }
                    break;

                case 2:
                    int hRoadNum = UnityEngine.Random.Range(1, (mapObj.HeightRoad + 1));
                    for (int i = 0; i < hRoad.Length; i++)
                    {
                        if (hRoad.Length == 0)
                        {
                            return;
                        }
                        Rocks(hRoad, i, hRoadNum, "hRoad_");
                    }
                    break;
                case 3:
                    int cRoadNum = UnityEngine.Random.Range(1, (mapObj.CrossRoad + 1));
                    for (int i = 0; i < cRoad.Length; i++)
                    {
                        if (cRoad.Length == 0)
                        {
                            return;
                        }
                        Rocks(cRoad, i, cRoadNum, "cRoad_");
                    }
                    break;
                case 4:
                    int tRoadNum = UnityEngine.Random.Range(1, (mapObj.TcrossRoad + 1));
                    for (int i = 0; i < tRoad.Length; i++)
                    {
                        if (tRoad.Length == 0)
                        {
                            return;
                        }
                        Rocks(tRoad, i, tRoadNum, "tRoad_");
                    }
                    break;
            }
        }
    }

    public void Rocks(Transform[] road,int num, int random, string roadName)
    {
       
        if (road[num].name == roadName + (random) && randomSetRock!= random && road[num].tag != "Start")
        {
            randomSetRock = random;

            if (levelRock > 0)
            {
                GameObject gameObj = Instantiate(rock) as GameObject;
                gameObj.transform.localPosition = road[num].transform.localPosition;
                Debug.Log("들어옴");
                levelRock--;
                Debug.Log("남은 돌 갯수" + levelRock);
            }
        }
    }

    public void Treasure(Transform[] road, int num, int random, string roadName)
    {

        if (road[num].name == roadName + (random) && randomSetTreasure != random)
        {
            randomSetTreasure = random;

            if (levelTreasure > 0)
            {
                GameObject gameObj = Instantiate(treasure) as GameObject;
                gameObj.transform.localPosition = road[num].transform.localPosition;
                gameObj.transform.rotation = road[num].transform.rotation;
                Debug.Log("들어옴");
                levelTreasure--;
                Debug.Log("남은 보물 갯수" + levelTreasure);
            }
        }
    }
}

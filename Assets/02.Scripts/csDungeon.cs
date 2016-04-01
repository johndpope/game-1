using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using itemPool;

class Maps
{
    private string mName;
    private int mWidthRoad;
    private int mHeightRoad;
    private int mRotationRoad;
    private int mCrossRoad;
    private int mTcrossRoad;
    private int mRoom;
    
    public string Name
    {
        get { return mName; }
        set { mName = value; }
    }

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

public class csDungeon : MonoBehaviour
{

    private ArrayList nMaps;
    public TextAsset textAsset;

    GameObject map;

    public GameObject rock;

    public Transform[] map1 = new Transform[16];

    void LoadAssetfromJson()
    {
        nMaps = new ArrayList();
      
        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            ArrayList itemInfoMaps = (ArrayList)itemTable["map"];

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
          
        }
    }

    // Use this for initialization
    void Start ()
    {
        LoadAssetfromJson();
        Maps maps = (Maps)nMaps[0];
        map = GameObject.Find(maps.Name);

        int f = UnityEngine.Random.Range(1, maps.RotationRoad);
        Debug.Log(f);
        for (int i = 0; i < map.transform.childCount; i++)
        {
            map1[i] = map.transform.GetChild(i);
            if (map1[i].name == "rRoad_" + f)
            {
                rock.transform.parent = map1[i];
                rock.transform.position = map1[i].transform.position;
                Debug.Log("들어옴");
            }

        }
        
        //Debug.Log(f);

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}

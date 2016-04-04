using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csdungeonManager : MonoBehaviour
{
    public GameObject levelgrid;
    public GameObject floor;

    public GameObject[] floorPoolSet = new GameObject[30];
    private ArrayList nMaps;
    private ArrayList nLevel;
    void Start ()
    {
        nMaps = StateManager.Instance.dungeonMapList;
        nLevel = StateManager.Instance.dungeonLevelList;

        for (int i = 0; i < nMaps.Count; i++)
        {
            _setupFloor(i,i);
        }
    }
	
	void Update ()
    {

        
    }

    public void Floor(int floorNum, int level)
    {
        StateManager.Instance.dungeonMap= floorNum;
        StateManager.Instance.dungeonLevel = level;
        Application.LoadLevel(1);
    }

    private void _setupFloor(int mapNum, int levelNum)
    {
        Debug.Log("들어옴");
        GameObject gameObj = Instantiate(floor) as GameObject;
        gameObj.transform.SetParent(levelgrid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);

        gameObj.GetComponent<Button>().onClick.AddListener(delegate { Floor(mapNum, levelNum); });
    }
}

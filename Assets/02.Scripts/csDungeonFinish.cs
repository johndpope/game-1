using UnityEngine;
using System.Collections;

public class csDungeonFinish : MonoBehaviour
{
    public GameObject finishPop;
    public GameObject state;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            //StartCoroutine("clear");
            village();
        }
    }

    IEnumerator clear()
    {
        //clear 라는 오브젝트 찾아서 켜기?
        GameObject.Find("Clear").SetActive(true);
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Clear").SetActive(false);
    }

    public void nextLaval()
    {
        StateManager.Instance.dungeonMap++;
        StateManager.Instance.dungeonLevel++;
        GameObject.Find("Manager").GetComponent<csSaveLord>().SaveData();
        finishPop.SetActive(false);
        Application.LoadLevel(1);
    }

    public void village()
    {
        //finishPop.SetActive(false);
        GameObject.Find("Manager").GetComponent<csSaveLord>().SaveData();
        StateManager.Instance.dunFinish = true;
        Application.LoadLevel(0);
    }
}

using UnityEngine;
using System.Collections;

public class csDungeonFinish : MonoBehaviour
{
    public GameObject finishPop;
    public GameObject cleras;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "door")
        {            
            StartCoroutine("clear");
            //village();
        }
    }

    IEnumerator clear()
    {
        //clear 라는 오브젝트 찾아서 켜기?
        cleras.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        cleras.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        village();
    }

    public void nextLaval()
    {
        StateManager.Instance.dungeonMap++;
        StateManager.Instance.dungeonLevel++;
        GameObject.Find("Manager").GetComponent<csSaveLord>().SaveData();
        //finishPop.SetActive(false);
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

using UnityEngine;
using System.Collections;

public class csAtkDown : MonoBehaviour
{
    int turn;
    int atkDownEnemy;
    float monsterAtk;

    void Start ()
    {
        turn = csBattle.turn;
        atkDownEnemy = csBattle.atkDownEnemy;
        monsterAtk = csBattle.monsterAtk;
        StartCoroutine(Down());
    }
	
	void Update () {
	
	}

    IEnumerator Down()
    {
        yield return new WaitForSeconds(20.0f);
        StateManager.Instance.monsterSpd[atkDownEnemy] = monsterAtk;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<csAtkDown>().enabled = false;
    }
}

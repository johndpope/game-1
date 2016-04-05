using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csObject : MonoBehaviour
{

    public GameObject findText;

    Text fText;

	void Start ()
    {
        fText = findText.GetComponent<Text>();
	}
	
	void Update ()
    {
	
	}
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock1f")
        {
            StartCoroutine("findObj");
        }

        if (collision.gameObject.tag == "Rock")
        {
            StartCoroutine("findObj");
        }

    }

    IEnumerator findObj()
    {
        findText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        findText.SetActive(false);
    }
}

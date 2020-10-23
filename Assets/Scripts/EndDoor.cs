using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(MainScreen());
        }
    }

    IEnumerator MainScreen()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        GameObject.Find("Transition").GetComponent<Animator>().SetBool("SceneChanging", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(0);
    }
}

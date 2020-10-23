using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireDamage : MonoBehaviour
{
    private GameObject fires;
    private bool isDoused = false;
    // Start is called before the first frame update
    void Start()
    {
        fires = GameObject.FindGameObjectWithTag("Fires");
    }

    // Update is called once per frame
    void Update()
    {
        if (fires.GetComponent<FiresScript>().dousing && !isDoused)
        {
            GetComponent<Animator>().SetBool("Douse", true);
            isDoused = true;
        }
        else if (isDoused)
        {
            GetComponent<Animator>().SetBool("Douse", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        GameObject.Find("Transition").GetComponent<Animator>().SetBool("SceneChanging", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Trigger1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject level;
    private GameObject[] events;
    public int eventNum = 0;
    public bool inRoutine = false;
    public UnityEngine.Vector3 playerVelocity = UnityEngine.Vector3.zero;
    private bool alreadyTriggered;
    // Start is called before the first frame update
    void Start()
    {
        eventNum = GameObject.Find("EventNumerator").GetComponent<EventNumerator>().eventNum;
        if(eventNum == 0)
        {
            StartCoroutine(IntroSequence());
            alreadyTriggered = false;
        }
        else if (eventNum == 1)
        {
            player.GetComponent<Animator>().SetBool("Run_01", true);
            canvas.SetActive(false);
            level.GetComponent<LevelController>().gameState = LevelController.State.Normal;
        }
        else if (eventNum == 2)
        {
            alreadyTriggered = true;
            level.GetComponent<LevelController>().canRewind = true;
            player.GetComponent<Animator>().SetBool("Run_01", true);
            canvas.SetActive(false);
            level.GetComponent<LevelController>().gameState = LevelController.State.Normal;
        }
        else if (eventNum == 3)
        {
            GetComponents<BoxCollider2D>()[0].enabled = false;
            level.GetComponent<LevelController>().canRewind = true;
            level.GetComponent<LevelController>().canForward = true;
            player.GetComponent<Animator>().SetBool("Run_01", true);
            canvas.SetActive(false);
            level.GetComponent<LevelController>().gameState = LevelController.State.Normal;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        eventNum = GameObject.Find("EventNumerator").GetComponent<EventNumerator>().eventNum;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && eventNum == 1)
        {
            StartCoroutine(FirstTrigger());
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 2 && alreadyTriggered)
        {
            alreadyTriggered = false;
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 2)
        {
            StartCoroutine(SecondTrigger());
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 3)
        {
            StartCoroutine(ThirdTrigger());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && eventNum == 1)
        {
            GameObject.Find("EventNumerator").GetComponent<EventNumerator>().eventNum = 2;
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 2)
        {
            GameObject.Find("EventNumerator").GetComponent<EventNumerator>().eventNum = 3;
            playerVelocity = UnityEngine.Vector3.zero;
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 3)
        {
            this.gameObject.SetActive(false); 
            playerVelocity = UnityEngine.Vector3.zero;
        }
    }

    IEnumerator IntroSequence()
    {
        level.GetComponent<LevelController>().gameState = LevelController.State.Pause;
        canvas.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        canvas.SetActive(true);
        text.GetComponent<Text>().text = "Where am I?";
        yield return new WaitForSeconds(2.0f);
        text.GetComponent<Text>().text = "Looks like I've been watching too much TV....";
        yield return new WaitForSeconds(3.0f);
        level.GetComponent<LevelController>().staticMove = true;
        yield return new WaitForSeconds(1.0f);
        text.GetComponent<Text>().text = "AHHHH!!! I don't want to find out what that does!";
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<Animator>().SetBool("Run_01", true);
        canvas.SetActive(false);
        level.GetComponent<LevelController>().gameState = LevelController.State.Normal;
        GameObject.Find("EventNumerator").GetComponent<EventNumerator>().eventNum = 1;
    }

    IEnumerator FirstTrigger()
    {
        level.GetComponent<LevelController>().canForward = false;
        level.GetComponent<LevelController>().canRewind = false;
        level.GetComponent<LevelController>().canPause = false;
        inRoutine = true;
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        player.GetComponent<Rigidbody2D>().velocity = new UnityEngine.Vector3(0, 0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        player.GetComponent<Animator>().enabled = false;
        level.GetComponent<LevelController>().gameState = LevelController.State.Pause;
        canvas.SetActive(true);
        text.GetComponent<Text>().text = "What was I thinking?!?!";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "I can't make this jump.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "There's got to be something I can do.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Press '←' to rewind.";
        level.GetComponent<LevelController>().canRewind = true;
        inRoutine = false;
    }

    IEnumerator SecondTrigger()
    {
        level.GetComponent<LevelController>().canForward = false;
        level.GetComponent<LevelController>().canRewind = false;
        level.GetComponent<LevelController>().canPause = false;
        inRoutine = true;
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        player.GetComponent<Rigidbody2D>().velocity = new UnityEngine.Vector3(0, 0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        player.GetComponent<Animator>().enabled = false;
        level.GetComponent<LevelController>().gameState = LevelController.State.Pause;
        canvas.SetActive(true);
        text.GetComponent<Text>().text = "Well that helps a bit.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Still too long of a jump.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Any other bright ideas?";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Press '→' to fast forward.";
        level.GetComponent<LevelController>().canForward = true;
        GetComponents<BoxCollider2D>()[0].enabled = false;
        inRoutine = false;
    }

    IEnumerator ThirdTrigger()
    {
        level.GetComponent<LevelController>().canForward = false;
        level.GetComponent<LevelController>().canRewind = false;
        level.GetComponent<LevelController>().canPause = false;
        inRoutine = true;
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        player.GetComponent<Rigidbody2D>().velocity = new UnityEngine.Vector3(0, 0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        player.GetComponent<Animator>().enabled = false;
        level.GetComponent<LevelController>().gameState = LevelController.State.Pause;
        canvas.SetActive(true);
        text.GetComponent<Text>().text = "Uh, fire is bad.";
        yield return new WaitForSeconds(2.5f); 
        text.GetComponent<Text>().text = "Hope you haven't run out of buttons yet.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Maybe we can wait until the fire is out.";
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<Text>().text = "Press '↓' to pause.";
        level.GetComponent<LevelController>().canPause = true;
        inRoutine = false;
    }
}

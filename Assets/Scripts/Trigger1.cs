using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Trigger1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject level;
    public int eventNum = 1;
    public bool inRoutine = false;
    public UnityEngine.Vector3 playerVelocity = UnityEngine.Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && eventNum == 1)
        {
            StartCoroutine(FirstTrigger());
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
            eventNum = 2;
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 2)
        {
            eventNum = 3;
            playerVelocity = UnityEngine.Vector3.zero;
        }
        else if (collision.gameObject.tag == "Player" && eventNum == 3)
        {
            this.gameObject.SetActive(false); 
            playerVelocity = UnityEngine.Vector3.zero;
        }
    }


    IEnumerator FirstTrigger()
    {
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
        inRoutine = false;
    }

    IEnumerator ThirdTrigger()
    {
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
        text.GetComponent<Text>().text = "Press '↓' to pause.";
        level.GetComponent<LevelController>().canPause = true;
        inRoutine = false;
    }
}

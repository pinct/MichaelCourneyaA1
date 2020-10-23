using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] public State gameState = State.Pause;
    [SerializeField] public bool canRewind = false;
    [SerializeField] public bool canPause = false;
    [SerializeField] public bool canForward = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject staticField;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject text;
    [SerializeField] private bool staticMove = false;
    public enum State { Reverse, Pause, Forward, Normal };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroSequence());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameState == State.Normal)
        {
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
        else if (gameState == State.Reverse)
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        else if (gameState == State.Forward)
        {
            transform.position = new Vector2(transform.position.x - 2*speed, transform.position.y);
        }

        if(Input.GetAxis("Horizontal") > 0 && canForward)
        {
            gameState = State.Forward;
            canvas.SetActive(false);
            StartCoroutine(TimeCooldown(0.5f));
        }
        else if (Input.GetAxis("Horizontal") < 0 && canRewind)
        {
            gameState = State.Reverse;
            canvas.SetActive(false);
            StartCoroutine(TimeCooldown(1.5f));
        }
        else if (Input.GetAxis("Vertical") < 0 && canPause)
        {
            gameState = State.Pause;
            canvas.SetActive(false);
            if(GameObject.Find("Fires") != null && !GameObject.Find("Fires").GetComponent<FiresScript>().dousing)
            {
                GameObject.Find("Fires").GetComponent<FiresScript>().dousing = true;
            }
            StartCoroutine(TimeCooldown(2.0f));
        }
        if (staticMove && staticField.transform.position.x < -13)
        {
            staticField.transform.position = new Vector2(staticField.transform.position.x + 0.1f, staticField.transform.position.y);
        }
        else
        {
            staticMove = false;
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

    IEnumerator IntroSequence()
    {
        gameState = State.Pause;
        canvas.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        canvas.SetActive(true);
        text.GetComponent<Text>().text = "Where am I?";
        yield return new WaitForSeconds(2.0f);
        text.GetComponent<Text>().text = "Looks like I've been watching too much tv....";
        yield return new WaitForSeconds(3.0f);
        staticMove = true;
        yield return new WaitForSeconds(1.0f);
        text.GetComponent<Text>().text = "AHHHH!!! I don't want to find out what that does!";
        yield return new WaitForSeconds(2.0f);
        player.GetComponent<Animator>().SetBool("Run_01", true);
        canvas.SetActive(false);
        gameState = State.Normal;
    }

    IEnumerator TimeCooldown(float time)
    {
        canRewind = false;
        canPause = false;
        canForward = false;
        Vector3 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        player.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(time);
        gameState = State.Normal;
        player.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
        if(GameObject.Find("Event1") != null && GameObject.Find("Event1").GetComponent<Trigger1>().playerVelocity != Vector3.zero)
        {
            player.GetComponent<Rigidbody2D>().velocity = GameObject.Find("Event1").GetComponent<Trigger1>().playerVelocity;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = playerVelocity;
        }
        player.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        if(GameObject.Find("Event1") != null && GameObject.Find("Event1").GetComponent<Trigger1>().eventNum == 1)
        {
            canRewind = true;
        }
        else if (GameObject.Find("Event1") != null && GameObject.Find("Event1").GetComponent<Trigger1>().eventNum == 2)
        {
            canRewind = true;
            canForward = true;
        }
        else if (GameObject.Find("Event1") == null || GameObject.Find("Event1").GetComponent<Trigger1>().eventNum == 3)
        {
            canRewind = true;
            canPause = true;
            canForward = true;
        }
    }
}

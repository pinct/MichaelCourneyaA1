using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button powerButton;
    // Start is called before the first frame update
    void Start()
    {
        powerButton.onClick.AddListener(SwitchScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SwitchScene()
    {
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Transition").GetComponent<Animator>().SetBool("SceneChanging", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}

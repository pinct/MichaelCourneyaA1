using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiresScript : MonoBehaviour
{
    public bool dousing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dousing)
        {
            StartCoroutine(Burnout());
        }
    }

    IEnumerator Burnout()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}

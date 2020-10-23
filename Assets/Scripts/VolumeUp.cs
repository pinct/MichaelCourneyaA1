using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUp : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button upButton;
    [SerializeField] private UnityEngine.UI.Button downButton;
    [SerializeField] private UnityEngine.UI.Button muteButton;
    // Start is called before the first frame update
    void Start()
    {
        upButton.onClick.AddListener(TurnVolumeUp);
        muteButton.onClick.AddListener(MuteVolume);
        downButton.onClick.AddListener(TurnVolumeDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TurnVolumeUp()
    {
        AudioListener.volume += 0.1f;
    }
    void MuteVolume()
    {
        if (AudioListener.volume == 0.0f)
        {
            AudioListener.volume += 0.1f;
        }
        else
        {
            AudioListener.volume = 0.0f;
        }
    }
    void TurnVolumeDown()
    {
        AudioListener.volume -= 0.1f;
    }
}

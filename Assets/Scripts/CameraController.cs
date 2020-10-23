using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Range(1.0f, 10.0f)] [SerializeField] private float cameraOffsetY = 5.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.Find("Event1") != null && GameObject.Find("Event1").GetComponent<Trigger1>().inRoutine)
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, new UnityEngine.Vector3(player.position.x, player.position.y + 0.7f, transform.position.z), 0.1f);
            if (GetComponent<Camera>().orthographicSize > 4)
            {
                GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 0.1f;
            }
        }
        else if (GameObject.Find("Event1") != null && !GameObject.Find("Event1").GetComponent<Trigger1>().inRoutine)
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, new UnityEngine.Vector3(0.0f, 0.0f, transform.position.z), 0.1f);
            if (GetComponent<Camera>().orthographicSize < 5)
            {
                GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 0.1f;
            }
        }
        if (player.position.y < transform.position.y - (0.5f * cameraOffsetY) && transform.position.y > 0.0f)
        {
            transform.position = new UnityEngine.Vector3(
                transform.position.x,
                player.position.y + (0.5f * cameraOffsetY),
                transform.position.z);
        }
        else if (player.position.y > transform.position.y + (0.5f * cameraOffsetY))
        {
            transform.position = new UnityEngine.Vector3(
                transform.position.x,
                player.position.y - (0.5f * cameraOffsetY),
                transform.position.z);
        }
        else if (transform.position.y < 0.0f)
        {
            transform.position = new UnityEngine.Vector3(
                transform.position.x,
                0.0f,
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new UnityEngine.Vector3(0.0f, cameraOffsetY, 0.0f));
    }
}

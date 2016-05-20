using UnityEngine;
using System.Collections;

public class TeleportTo : MonoBehaviour
{
    public GameObject teleportPos;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.transform.position = teleportPos.transform.position;
    }
}

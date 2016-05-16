using UnityEngine;
using System.Collections;

public class ToDownstairs : MonoBehaviour
{
    Vector3 downStairsPos;

    void Start()
    {
        downStairsPos = GameObject.Find("BottomOfStairs").transform.position;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.transform.position = downStairsPos;
    }
}

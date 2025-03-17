using System;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    public GameObject obstacle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle Hit");
        }
    }
}

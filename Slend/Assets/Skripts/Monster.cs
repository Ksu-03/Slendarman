using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  Transform player;
    Vector3 playerCorrectPosition = Vector3.zero;
    Vector3 newPos = Vector3.zero;
    float teleportTimer = 15;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        playerCorrectPosition = player.position;
        playerCorrectPosition.y = transform.position.y;
        transform.LookAt(playerCorrectPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerCorrectPosition, Time.deltaTime * 7);
        teleportTimer -= Time.deltaTime;
        if(teleportTimer < 0)
        {
            teleportTimer = Random.Range(15, 40);
            Teleport();
        }
    }
    public void Teleport()
    {
        newPos = playerCorrectPosition;
        newPos.y = transform.position.y;
        do
        {
            newPos.x += Random.Range(-80, -80);
            newPos.z += Random.Range(-80, -80);
        }
        while (Vector3.Distance(newPos, playerCorrectPosition) < 20);
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}

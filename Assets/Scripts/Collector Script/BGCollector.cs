using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{

    private GameObject[] backgrounds;
    private GameObject[] grounds;

    private float lastBGX;
    private float lastGroundX;

    // Start is called before the first frame update
    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        for (int i = 1; i < backgrounds.Length; i++) {
            if (lastBGX < backgrounds[i].transform.position.x) {
                lastBGX = backgrounds[i].transform.position.x;
            }
        }

        for (int i = 1; i < grounds.Length; i++) {
            if (lastGroundX < grounds[i].transform.position.x) {
                lastGroundX = grounds[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Background") {
            Vector3 temp = collision.transform.position;
            float width = ((BoxCollider2D)collision).size.x;

            temp.x = lastBGX + width;

            collision.transform.position = temp;

            lastBGX = temp.x;
        } else if (collision.tag == "Ground") {

            Vector3 temp = collision.transform.position;
            float width = ((BoxCollider2D)collision).size.x;

            temp.x = lastGroundX + width;

            collision.transform.position = temp;

            lastGroundX = temp.x;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ConveyerBelt" && transform.parent == null)
        {
            ConveyerBelt cb = collision.gameObject.GetComponent<ConveyerBelt>();
            Vector3 newPos = new Vector3(transform.position.x + (cb.direction * Time.deltaTime * cb.speed), transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}

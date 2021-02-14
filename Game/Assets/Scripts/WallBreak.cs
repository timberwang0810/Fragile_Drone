using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float v = collision.relativeVelocity.magnitude;
        if (v > 20)
        {
            explode();
        }
    }

    public void explode()
    {
        SoundManager.S.breakWood();
        animator.SetTrigger("broken");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    [Space]
    public float movementForce;
    public float maxFallVelocity;
    public float maxFallVelocityIncrease;
    private Rigidbody rigidB;
    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RegisterSwipe.SwipeLeft)
        {
            if(rigidB.velocity.x > 0)
                rigidB.velocity = new Vector3(0, rigidB.velocity.y, 0);
            rigidB.AddForce(Vector3.left * movementForce);
        }
        else if(RegisterSwipe.SwipeRight)
        {
            if (rigidB.velocity.x < 0)
                rigidB.velocity = new Vector3(0, rigidB.velocity.y, 0);
            rigidB.AddForce(Vector3.right * movementForce);
        }
        //else if(RegisterSwipe.SwipeDown)
        //{
        //    maxFallVelocity += maxFallVelocityIncrease;
        //}
       
        rigidB.velocity = new Vector3(rigidB.velocity.x, Mathf.Clamp(rigidB.velocity.y, -maxFallVelocity, 0), 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            rigidB.velocity = new Vector3(rigidB.velocity.x, -4, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("End"))
        {
            PlayerPrefs.SetInt("Number", gm.currentLevel + 1);
            SceneManager.LoadScene(0);
        }
    }

}

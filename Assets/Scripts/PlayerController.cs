using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public LevelGenerator lg;
    [Space]
    public float movementForce;
    public float maxFallVelocity;
    private float maxEndFallVelocity;
    public float maxFallVelocityIncrease;
    private Rigidbody rigidB;
    [Space]
    public GameObject waterSplashes;
    [Space]
    public Animator anim;
    [Space]
    public GameObject blood;
    public GameObject deathSmoke;

    [Space]
    public GameObject victoryParticles;
    private bool died;
    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        Invoke("IncreaseVelocityAtStart", 0.01f);
    }
    private void IncreaseVelocityAtStart()
    {
        maxFallVelocity *= 1 + lg.currentLevel * 0.05f;
        maxEndFallVelocity = maxFallVelocity + 0.05f;
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

        transform.rotation = Quaternion.Euler(0, 180, -rigidB.velocity.x * 4);

        maxFallVelocity = Mathf.Lerp(maxFallVelocity, maxEndFallVelocity, Mathf.Abs(transform.position.y / (lg.currentLevel * 0.5f + 29)));
        maxFallVelocity = Mathf.Clamp(maxFallVelocity, 4, 8);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (died)
            return;

        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Boiler"))
        {
            if (collision.gameObject.CompareTag("Boiler"))
                Instantiate(deathSmoke, collision.contacts[0].point, Quaternion.Euler(-90,0,0));
            else
                Instantiate(blood, collision.contacts[0].point, Quaternion.identity);

            anim.SetBool("Death", true);
            died = true;
            Invoke("ReloadScene", 0.5f);
            Destroy(GetComponent<CapsuleCollider>());
            rigidB.useGravity = false;
            rigidB.velocity = Vector3.zero;
            movementForce = 0;
        }
        else
        {
            rigidB.velocity = new Vector3(rigidB.velocity.x, -4, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (died)
            return;

        if(other.CompareTag("End"))
        {
            PlayerPrefs.SetInt("level", lg.currentLevel + 1);
            Instantiate(waterSplashes, transform.position + Vector3.up * 0.4f, Quaternion.Euler(-90, 0, 0));
            Instantiate(victoryParticles, new Vector3(-2, transform.position.y + 2.6f, -1), Quaternion.identity);
            Instantiate(victoryParticles, new Vector3(2, transform.position.y + 2.5f, -1), Quaternion.Euler(0, 180, 0));
            Invoke("ReloadScene", 2f);
            PlayerPrefs.SetString("trapsOnLevels", "");
        }
        else if(other.CompareTag("Obstacle") || other.gameObject.CompareTag("Boiler"))
        {
            if (other.gameObject.CompareTag("Boiler"))
            {
                Instantiate(deathSmoke, transform.position, Quaternion.Euler(-90, 0, 0));
            }

            anim.SetBool("Death", true);
            died = true;
            Invoke("ReloadScene", 0.5f);
            Destroy(GetComponent<CapsuleCollider>());
            movementForce = 0;
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

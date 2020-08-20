using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 20.0f;
    public GameObject projectilePrefab;
    public string endGame = "EndGame";
    public AudioClip projectilesound;
    public static PlayerController instance;
    public bool isDead = false;




    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }else
        {
            Debug.LogError("Already an Instance of a player controller class");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);

            }
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioSource.PlayOneShot(projectilesound);
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            }
            if (horizontalInput == 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        { 
            animator.SetTrigger("Death Trigger");
            Debug.Log("Game Over");
            isDead = true;
            Instantiate(smokeEffect.gameObject,transform.position,transform.rotation);
            StartCoroutine(LoadEndGame());
        }
    }
    private IEnumerator LoadEndGame()
    {
        yield return new WaitForSeconds(5);
        ScoreManager.instance.StopGame();
        SceneManager.LoadScene(endGame);
    }
}


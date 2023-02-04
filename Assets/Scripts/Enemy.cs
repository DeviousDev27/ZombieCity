using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;


    [SerializeField]
    public float moveSpeed;

    public int maxHealth = 50;
    public float currentHealth;

    [SerializeField]
    public int attackDamge;

    [SerializeField] private AudioSource zombieDied;


    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }
public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;
        if (currentHealth <= 0)
        {
            //keep score 
            Score.scoreValue += 100;

            //dead
            zombieDied.Play();
            Destroy(gameObject, .8f);
        
        }        

    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            if (hitInfo.GetComponent<PlayerMovement>() != null)
            {
                hitInfo.GetComponent<PlayerMovement>().TakeDamage(attackDamge);
            }
        }
    }

}

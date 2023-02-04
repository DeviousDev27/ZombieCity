using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthManager healthBar;

    public float moveSpeed;
    float moveHorizontal;
    float moveVertical;
    private Vector2 move;
    private Vector2 mousePos;
    private float angle;

    public Transform weapon;
    public Transform firePoint;

    public GameObject bullet;
    public float shootSpeed;
    public float bulletSpeed;
    private Rigidbody2D rb;
    private bool canShoot = true;

    bool facingRight = true;

    [SerializeField] private AudioSource shootSoundEffect;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canShoot = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //determined if I can shoot or not
        if(Input.GetButton("Fire1") && canShoot)
        {
            shootSoundEffect.Play();
            StartCoroutine(ShootGun());
        }

    }

    private void FixedUpdate()
    {
        weapon.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        

        if(moveHorizontal !=0 || moveVertical !=0)
        {
            move = new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.deltaTime;
            rb.AddForce(move);
        }

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }

        if(moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }



    //shooting the weapon
    public IEnumerator ShootGun()
    {
        canShoot = false;
        GameObject bulletCreated = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bulletCreated.GetComponent<Rigidbody2D>();

        bulletRb.AddForce(firePoint.right * bulletSpeed);

        
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

   public void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            //dead
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();

        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

 
}

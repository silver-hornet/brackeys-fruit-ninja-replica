using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] GameObject fruitSlicedPrefab;
    Rigidbody2D rb;
    [SerializeField] float startForce = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blade"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
            Destroy(slicedFruit, 3f);
            Destroy(gameObject);
        }
    }
}
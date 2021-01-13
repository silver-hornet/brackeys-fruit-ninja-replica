using UnityEngine;

public class Blade : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;
    [SerializeField] GameObject bladeTrailPrefab;
    GameObject currentBladeTrail;
    CircleCollider2D circleCollider;
    bool isCutting = false;
    Vector2 previousPosition;
    [SerializeField] float minCuttingVelocity = 0.001f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        // We are calculating our own velocity, rather than using rb.velocity, since we can't access that using a kinematic rigidbody
        if (velocity > minCuttingVelocity)
            circleCollider.enabled = true;
        else
            circleCollider.enabled = false;

        previousPosition = newPosition;
    }

    void StartCutting()
    {
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }

    void StopCutting()
    {
        isCutting = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 2f);
        circleCollider.enabled = false;
    }
}
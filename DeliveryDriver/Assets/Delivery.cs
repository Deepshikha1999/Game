using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
    [SerializeField] Color32 noPackageColor = new Color32(2, 2, 2, 2);

    bool hasPackage; // by default false
    [SerializeField] float destroyDelay = 0.5f;

    SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I m colliding");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up fro delivery.....");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(collision.gameObject, destroyDelay);
            
        }

        if(collision.tag == "Customer" && hasPackage)
        {
            hasPackage = false;
            Debug.Log("Here is the customer to deliver the package....");
            spriteRenderer.color = noPackageColor;
        }
    }
}

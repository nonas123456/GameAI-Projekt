using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject CurrentTile;
    private RaycastHit hit;
    public Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position + new Vector3(2f, 0f, 0f), transform.TransformDirection(Vector3.down), out hit);
        CurrentTile = hit.collider.gameObject;


        if (Input.GetKey(KeyCode.W))
        {
            print("Pressed W");
            rb.AddForce(transform.TransformDirection(Vector3.forward) * 10, ForceMode.Force);
            print("Added Force");
        }

        if (Input.GetKey(KeyCode.S))
        {
            print("Pressed S");
            rb.AddForce(transform.TransformDirection(Vector3.back) * 10, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("Pressed A");
            rb.AddForce(transform.TransformDirection(Vector3.left) * 10, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("Pressed D");
            rb.AddForce(transform.TransformDirection(Vector3.right) * 10, ForceMode.Force);
        }


    }
}

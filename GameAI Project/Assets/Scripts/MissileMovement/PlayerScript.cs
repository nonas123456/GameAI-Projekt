using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject CurrentTile;
    private RaycastHit hit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit);
        CurrentTile = hit.collider.gameObject;
    }
}

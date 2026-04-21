using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour
{
    [SerializeField] private GameObject currentTile;
    private RaycastHit hit;
    public PlayerScript PlayerScript;
    [SerializeField] private GameObject CurrentPlayerTile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit);
        currentTile = hit.collider.gameObject;
        CurrentPlayerTile = PlayerScript.CurrentTile;   

            
    }

    private void LateUpdate()
    {
        
    }

    
    
    
}

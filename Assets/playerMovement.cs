using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField]
    private Tilemap groundTileMap;
    
    [SerializeField]
    private Tilemap collisionTileMap;

    [SerializeField]
    private Tilemap showPhoto;

    [SerializeField]
    private GameObject Button;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        // input
        movement.x =Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate(){
        //Movement
        Move(movement);
        ShowCard(movement);
    }

    private void Move(Vector2 movement){
        if (canMove(movement))
            rb.MovePosition(rb.position + movement * moveSpeed* Time.fixedDeltaTime);
    }

    private void ShowCard(Vector2 movement){
        if(shouldPhoto(movement)){
            Button.SetActive(true);
        }
        else
        Button.SetActive(false);
            
    }

    private bool shouldPhoto(Vector2 movement){
        Vector3Int gridPosition = showPhoto.WorldToCell(transform.position + (Vector3)movement );
        if(!showPhoto.HasTile(gridPosition))
            return false;
        return true;
    }

    private bool canMove(Vector2 movement){
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)movement );
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition))
            return false;
        return true;
    }

}

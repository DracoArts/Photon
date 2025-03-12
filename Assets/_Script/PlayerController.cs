using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public float speed = 5f;
    
      public Camera playerCamera; 
   void Start()
    {
        if (photonView.IsMine) 
        {
          
           playerCamera.gameObject.SetActive(true);
            
        }
        else
        {
    
            playerCamera.gameObject.SetActive(false);
            
        }
    }    void Update()
    {
        if (photonView.IsMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
            transform.Translate(move);
        }
    }
}

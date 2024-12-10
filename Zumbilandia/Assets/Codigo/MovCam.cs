using UnityEngine;

public class Movcam : MonoBehaviour
{
    public Transform Personagem;
    [SerializeField]public const float vel = 0.1f;
    
      private void FixedUpdate() 
      {
       transform.position = Vector3.Lerp(transform.position, Personagem.position, vel);
      }
}
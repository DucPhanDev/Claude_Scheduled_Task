using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float delayDestroy;
    private void Start()
    {
        Invoke("DestroyGameObject",delayDestroy);
    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
}

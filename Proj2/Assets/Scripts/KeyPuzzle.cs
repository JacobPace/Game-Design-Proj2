using UnityEngine;

public class KeyPuzzle : MonoBehaviour, IInteractable
{
    // set in inspector
    public GameObject key;
    public bool grabbed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabbed = false;
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPuzzle()
    {
        key.SetActive(true);
    }

    public void Interact()
    {

    }
}

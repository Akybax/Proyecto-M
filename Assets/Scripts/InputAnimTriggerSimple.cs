
using UnityEngine;

public class InputAnimTriggerSimple : MonoBehaviour
{

    public string triggerParameterName ="Attack";

    public KeyCode activationKey = KeyCode.Space;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetKeyDown(activationKey))
         {
            _animator.SetTrigger(triggerParameterName);
         }
        
    }
}

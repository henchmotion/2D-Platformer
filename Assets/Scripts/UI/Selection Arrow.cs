using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; // the siund we play when we move the arrow up and down.
    [SerializeField] private AudioClip interactSound; // The sound  we play when we select an option.
    private RectTransform rect;
    private int currentPosition;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Change position of the selection arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //Interact with options
         if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
        
    }
    private void ChangePosition(int _change)
    { 
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length -1)
        currentPosition = 0;


        // Assign the Y position of the current option to the arrow (basically moving it up/down)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //Access the button component on each and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}


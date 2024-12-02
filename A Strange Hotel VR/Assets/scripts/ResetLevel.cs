using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    //Whole class is just for testing purposes
    [SerializeField] private InputActionProperty resetButton;

    void Update()
    {
        if (resetButton.action.ReadValue<float>() == 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}

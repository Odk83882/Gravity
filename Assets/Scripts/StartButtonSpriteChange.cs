using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonSpriteChange : MonoBehaviour
{
    [SerializeField]
    private SpriteState spriteState;

    [SerializeField]
    private Button button;

    public void SpriteChange()
    {
        button.spriteState = spriteState;
    }
}

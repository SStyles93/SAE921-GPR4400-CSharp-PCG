using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonPrompt", menuName = "ScriptableObject/UI/UIButtons", order = 4)]
public class UIButtonsSO : ScriptableObject
{
    [Tooltip("UI Sprite for Keyboard")]
    public Sprite keyboardSprite;
    [Tooltip("UI Sprite for Gamepad")]
    public Sprite gamepadSprite;
}

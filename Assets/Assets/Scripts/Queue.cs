using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour {

    public int maxActions;
    public List<string> acciones = new List<string>();
    public List<Button> buttons = new List<Button>();
    public Image imageForward;
    public Image imageLeft;
    public Image imageRight;
    public Image imageJump;
    public Image imageAttck;
    public Image imageCombo1;
    public Image imageCombo2;
}

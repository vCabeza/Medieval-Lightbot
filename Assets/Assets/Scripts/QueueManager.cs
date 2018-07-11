using UnityEngine;

public class QueueManager : MonoBehaviour {

    public GameObject mainQueue;
    public GameObject combo1Queue;
    public GameObject combo2Queue;

    private Queue currentQueue;

    private void Start() {
        selectMain();
    }

    public void selectMain() {
        currentQueue = mainQueue.GetComponent<Queue>();
    }

    public void selectCombo1() {
        currentQueue = combo1Queue.GetComponent<Queue>();
    }

    public void selectCombo2() {
        currentQueue = combo2Queue.GetComponent<Queue>();
    }

    public void addForward() {
        if (currentQueue.acciones.Count < currentQueue.maxActions) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageForward.sprite;
            currentQueue.acciones.Add("F");
        }
    }
    public void addLeft() {
        if (currentQueue.acciones.Count < currentQueue.maxActions) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageLeft.sprite;
            currentQueue.acciones.Add("L");
        }
    }
    public void addRight() {
        if (currentQueue.acciones.Count < currentQueue.maxActions) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageRight.sprite;
            currentQueue.acciones.Add("R");
        }
    }
    public void addJump() {
        if (currentQueue.acciones.Count < currentQueue.maxActions) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageJump.sprite;
            currentQueue.acciones.Add("J");
        }
    }
    public void addAtck() {
        if (currentQueue.acciones.Count < currentQueue.maxActions) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageAttck.sprite;
            currentQueue.acciones.Add("A");
        }
    }
    public void addCombo1() {
        if (currentQueue.acciones.Count < currentQueue.maxActions && isComboAvailable()) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageCombo1.sprite;
            currentQueue.acciones.Add("1");
        }
    }
    public void addCombo2() {
        if (currentQueue.acciones.Count < currentQueue.maxActions && isComboAvailable()) {
            currentQueue.buttons[currentQueue.acciones.Count].image.sprite = currentQueue.imageCombo2.sprite;
            currentQueue.acciones.Add("2");
        }
    }

    private bool isComboAvailable() {
        bool isAvailable = true;

        if(currentQueue.maxActions == 8 && currentQueue.acciones.Count == 0) {
            isAvailable = false;
        }

        return isAvailable;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharMovement: MonoBehaviour {

    public GameObject finishMenu;
    public int alturaActual = 1;
    public Text score;

    RaycastHit[] hits;
    bool isMoving;
    GameObject[] enemies;
    int currentPoints;
    int playerPoints;
    int finalPoints;

    public GameObject main;
    public GameObject combo1;
    public GameObject combo2;

    Queue mainQueue;
    Queue combo1Queue;
    Queue combo2Queue;

    List<string> accionesMain = new List<string>();
    List<string> accionesCombo1 = new List<string>();
    List<string> accionesCombo2 = new List<string>();

    public GameObject characterPrefab;
    Animator anim;

    public void start() {
        StartCoroutine(coroutineActions(accionesMain));
    }

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void goForward() {
        hits = Physics.RaycastAll(transform.position, transform.forward, 1);

        if (hits.Length > 0 && hits[0].collider.gameObject.tag == "Escenario") {
            TileScript tileScript = hits[0].collider.gameObject.GetComponent<TileScript>();

            if (tileScript.altura == alturaActual) {
                if (hits.Length > 1) {
                    if (hits[1].collider.gameObject.tag != "Enemy") {
                        StartCoroutine(stepForward());
                    }
                } else {
                    StartCoroutine(stepForward());
                }
            }
        }
    }

    void jump() {
        hits = Physics.RaycastAll(transform.position, transform.forward, 1);

        if (hits.Length > 0 && hits[0].collider.gameObject.tag == "Escenario") {
            TileScript tileScript = hits[0].collider.gameObject.GetComponent<TileScript>();

            if (tileScript.altura == alturaActual + 1 || tileScript.altura == alturaActual - 1) {
                if (hits.Length > 1) {
                    if (hits[1].collider.gameObject.tag != "Enemy") {
                        StartCoroutine(jumpForward(tileScript.altura));
                        alturaActual = tileScript.altura;
                    }
                } else {
                    StartCoroutine(jumpForward(tileScript.altura));
                    alturaActual = tileScript.altura;
                }
            }
        }
    }

    IEnumerator coroutineActions(List<string> actionsQueue) {
        for (int i = 0; i < actionsQueue.Count; i++) {
            if (actionsQueue[i].Equals("F")) {
                goForward();
            } else if (actionsQueue[i].Equals("R")) {
                StartCoroutine(turn(90));
            } else if (actionsQueue[i].Equals("L")) {
                StartCoroutine(turn(-90));
            } else if (actionsQueue[i].Equals("J")) {
                jump();
            } else if (actionsQueue[i].Equals("A")) {
                StartCoroutine(attack());
            } else if (actionsQueue[i].Equals("1")) {
                StartCoroutine(coroutineActions(accionesCombo1));
            } else if (actionsQueue[i].Equals("2")) {
                StartCoroutine(coroutineActions(accionesCombo2));
            }

            currentPoints -= 1;

            yield return new WaitUntil(() => isMoving == false);
        }

        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator stepForward() {
        isMoving = true;
        anim.SetBool("isWalking", true);

        Vector3 moveDirection = transform.position + transform.forward;
        while (moveDirection != transform.position) {            
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, 1.5f * Time.deltaTime);

            yield return null;
        }

        anim.SetBool("isWalking", false);
        yield return new WaitForSeconds(0.5f);

        isMoving = false;
    }

    IEnumerator jumpForward(int altura) {
        isMoving = true;
        anim.SetBool("isJumping", true);

        Vector3 moveDirection = transform.position + transform.forward;
        if (alturaActual < altura) {
            moveDirection += new Vector3(0, 0.5f, 0);
        }
        while (moveDirection != transform.position) {
            if (moveDirection.y > transform.position.y) {
                transform.Translate(Vector3.up * 3f * Time.deltaTime, Space.World);
            }
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, 1.5f * Time.deltaTime);

            yield return null;
        }

        anim.SetBool("isJumping", false);
        transform.position = moveDirection;
        yield return new WaitForSeconds(0.5f);

        isMoving = false;
    }

    IEnumerator turn(int direction) {
        isMoving = true;
        anim.SetBool("isRotating", true);

        Quaternion targetRotation = transform.rotation;
        targetRotation *= Quaternion.AngleAxis(direction, Vector3.up);

        while (targetRotation != transform.rotation) {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * 1f * Time.deltaTime);

            yield return null;
        }

        anim.SetBool("isRotating", false);
        yield return new WaitForSeconds(0.5f);

        isMoving = false;
    }

    IEnumerator attack() {
        isMoving = true;
        anim.SetBool("isAttacking", true);

        hits = Physics.RaycastAll(transform.position, transform.forward, 1);

        if (hits[0].collider.gameObject.tag == "Escenario") {
            if (hits.Length > 1) {
                if (hits[1].collider.gameObject.tag == "Enemy") {
                    Destroy(hits[1].collider.gameObject);
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }

        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.7f);

        isMoving = false;
    }

    void Start() {
        mainQueue = main.GetComponent<Queue>();
        combo1Queue = combo1.GetComponent<Queue>();
        combo2Queue = combo2.GetComponent<Queue>();

        anim = characterPrefab.GetComponent<Animator>();

        currentPoints = 100;
        playerPoints = PlayerPrefs.GetInt("playerPoints");
    }

    void Update() {
        accionesMain = mainQueue.acciones;
        accionesCombo1 = combo1Queue.acciones;
        accionesCombo2 = combo2Queue.acciones;

        score.text = playerPoints.ToString();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) {
            finishMenu.SetActive(true);

            finalPoints = currentPoints + playerPoints;
            PlayerPrefs.SetInt("playerPoints", finalPoints);
        }
    }
}

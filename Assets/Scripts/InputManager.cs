using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    private TempoManager tempoManager;
    [HideInInspector] public int direction = 0;
    [HideInInspector] public int charge = 0;
    [SerializeField] private int maxCharge = 3;
    [HideInInspector] public bool attackTriggered = false;
    [HideInInspector] public bool failed = false;
    private bool alreadyActed = false;
    [SerializeField] private TextMeshProUGUI chargeText;

    private Vector2 startTouchPosition, endtouchPosition;
    private float doubleTapTime = 0.3f;
    private float LastTapTime = 0f;

    private void Start()
    {
        tempoManager = GameObject.FindGameObjectWithTag("Tempo").GetComponent<TempoManager>();
    }

    private void Update()
    {
        failed = false;
        bool attemptedInput = false;
        chargeText.text = "" + charge;

#if UNITY_EDITOR || UNITY_STANDALONE
        attemptedInput = Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.S) ||
                     Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.Space) ||
                     Input.GetKeyDown(KeyCode.F);
#else
    attemptedInput = Input.touchCount > 0;
#endif

        if (!tempoManager.tap)
        {
            direction = 0;
            attackTriggered = false;
            alreadyActed = false;
            if (attemptedInput)
            {
                failed = true;
            }

            return;
        }

        if (alreadyActed) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        PCInput();
#else
        ControlsforMobile();
#endif
    }

    private void PCInput()
    {
        bool acted = false;

        if (Input.GetKeyDown(KeyCode.W)) { direction = 1; acted = true; }
        else if (Input.GetKeyDown(KeyCode.D)) { direction = 2; acted = true; }
        else if (Input.GetKeyDown(KeyCode.S)) { direction = 3; acted = true; }
        else if (Input.GetKeyDown(KeyCode.A)) { direction = 4; acted = true; }

        if (Input.GetKeyDown(KeyCode.Space) && charge < maxCharge)
        {
            charge++;
            acted = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            attackTriggered = true;
            acted = true;
        }

        if (acted)
        {
            alreadyActed = true;
        }
    }

    private void ControlsforMobile()
    {
        direction = 0;
        attackTriggered = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;

                    if (Time.time - LastTapTime < doubleTapTime)
                    {
                        attackTriggered = true;
                        LastTapTime = 0f;
                    }
                    else if (charge <= maxCharge)
                    {
                        LastTapTime = Time.time;
                        charge++;
                    }
                    break;

                case TouchPhase.Ended:
                    endtouchPosition = touch.position;
                    Vector2 swipe = endtouchPosition - startTouchPosition;

                    if (swipe.magnitude > 50)
                    {
                        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                        {
                            direction = swipe.x > 0 ? 2 : 4;
                        }
                        else
                        {
                            direction = swipe.y > 0 ? 1 : 3;
                        }
                    }
                    break;
            }
        }

        alreadyActed = true;
    }
}

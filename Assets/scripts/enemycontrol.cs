//using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class enemycontrol : MonoBehaviour
{
    /*public void move()
    {
        transform.position += Vector3.up * 1;
    }*/

    public float vel, dzone;
    private float dz, dz2, fTemp1, fTemp2, prevvalue;
    private bool hasChanged;
    public InputAction movement;
    InputActionPhase prevphase;
    //public InputAction playermovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dz = dzone / 2;
        dz2 = dz / 2;
        hasChanged = false;
        prevphase = movement.phase;
        fTemp1 = dzone;
        fTemp2 = dz2;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        dz = dzone; // 2;
        dz2 = dz; // 2;
        /* if ()
         {
             gameObject.transform.localPosition += new Vector3(1, 1, 0);
         }*/

        /*if (Input.GetKey(KeyCode.UpArrow))
        {
           transform.position += delta * vel * Vector3.up;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += delta * vel * Vector3.down;
        }*/

        //Debug.Log(movement.phase);
        if (movement.phase != prevphase)
        {
            hasChanged = true;
        }

        if (hasChanged && (movement.phase == InputActionPhase.Waiting))
        {
            fTemp1 = dzone;
            if (fTemp2 <= 0)
            {
                fTemp2 = dz2;
                hasChanged = false;
            }
            else
            {
                transform.position += prevvalue * vel * delta * Vector3.up;
                fTemp2 -= delta;
            }
        }
        else if (hasChanged && (movement.phase == InputActionPhase.Started))
        {
            if (fTemp1 <= 0)
            {
                prevvalue = movement.ReadValue<float>();
                transform.position += movement.ReadValue<float>() * vel * delta * Vector3.up;
                //hasChanged = false;
            }
            else
            {
                fTemp1 -= delta;
            }
        }
        else
        {
            prevvalue = movement.ReadValue<float>();
        }

        if (transform.position.y < -20)
        {
            transform.position = new Vector3(transform.position.x, -19.7f, transform.position.z);
        }
        if (transform.position.y > 20)
        {
            transform.position = new Vector3(transform.position.x, 19.7f, transform.position.z);
        }
        //transform.position += movement.ReadValue<float>() * vel * delta * Vector3.up;

        prevphase = movement.phase;
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    public void Reset()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        fTemp1 = 0;
        fTemp2 = 0;
    }
}

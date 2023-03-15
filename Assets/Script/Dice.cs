using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public int diceValue;

    public int currentPoint;

    public DiceSide[] diceSides;

    private void Start()
    {
        currentPoint = 1;
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
        if (rb.IsSleeping() && !hasLanded && thrown) // rb.issleep la khi dice da roll va dang nam tren ground
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            GameObject go = GameObject.FindGameObjectWithTag("Player");
            var component = go.gameObject.GetComponent<GamblerCat>();
            currentPoint += GetSideValue();
            component.MoveToPoint(currentPoint);
        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollAgain();
        }
    }

    private void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            ResetDice();
        }
    }

    private void ResetDice()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    private void RollAgain()
    {
        ResetDice();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    private int GetSideValue()
    {
        diceValue = 0;
        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                // Debug.Log(diceValue + " has been rolled");
                print(diceValue + "!!!!!!");
                return diceValue;
            }
        }
        return diceValue;
    }
}

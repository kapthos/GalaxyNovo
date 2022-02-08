using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnimation();
    }

    public void MovementAnimation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _anim.SetBool("LeftTurn", true);
            _anim.SetBool("RightTurn", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", false);
        }
    }
}

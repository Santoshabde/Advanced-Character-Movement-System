using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    private const string MOVE = "Run";

    [SerializeField] private Animator characterAnimator;
    [SerializeField] private CharacterLocomotion characterLocomotion;

    private void Awake()
    {
        GetRequiredComponents();
    }

    private void Update()
    {
        characterAnimator.SetFloat(MOVE, characterLocomotion.MoveInput.magnitude);
    }

    private void GetRequiredComponents()
    {
        if (characterLocomotion == null)
            characterLocomotion = GetComponent<CharacterLocomotion>();

        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();
    }
}

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
        if (characterLocomotion.IsCharacterGrounded())
        {
            characterAnimator.SetFloat(MOVE, characterLocomotion.MoveInput.magnitude);
            characterAnimator.SetBool("onGround", true);
        }
        else
        {
            //Character in the air!!
            characterAnimator.SetFloat(MOVE, 0);
            characterAnimator.SetBool("onGround", false);
        }
    }

    private void GetRequiredComponents()
    {
        if (characterLocomotion == null)
            characterLocomotion = GetComponent<CharacterLocomotion>();

        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();
    }

    public void PlayParkourAnimation(string parkourAnimationName)
    {
        characterAnimator.Play(parkourAnimationName);
       
    }

    public void AnimationMatchTarget(Vector3 targetPoint, Quaternion rotation, AvatarTarget avtarTarget, Vector3 positionXYZWeight, float rotationWight, float startNormalizedTime, float targetNormalizedTime)
    {
        characterAnimator.MatchTarget(targetPoint, rotation, avtarTarget, new MatchTargetWeightMask(positionXYZWeight, rotationWight), startNormalizedTime, targetNormalizedTime);
    }

    public AnimatorStateInfo GetCurrentStateInfo(int layer) => characterAnimator.GetCurrentAnimatorStateInfo(layer);
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ParkourController : MonoBehaviour
{
    //Required Components
    [SerializeField] private CharacterAnimatorController animatorController;
    [SerializeField] private CharacterLocomotion characterLocomotion;
    [SerializeField] private List<ParkourActionData> parkourActionsData;

    [SerializeField] private Vector3 forwardScannerOffset;
    [SerializeField] private LayerMask scannerLayerMask;
    [SerializeField] private float forwardRayLenght;
    [SerializeField] private float heightRayLength;

    private EnvironmentScanner environmentScanner;
    private IInputSystem inputSystem;
    private bool performingAction;

    private void Awake()
    {
        environmentScanner = new EnvironmentScanner(this.transform, forwardScannerOffset, scannerLayerMask, forwardRayLenght, heightRayLength);
        inputSystem = new KeyboardAndMouseInputSystem();
    }

    private Vector3 test;
    private void Update()
    {
        if (inputSystem.OnJump() && !performingAction)
        {
            (bool hasForwardHit, RaycastHit forwardHitInfo, bool hasHeightHit, RaycastHit heightHitinfo) = environmentScanner.ObstrucleCheck();
            if (hasForwardHit)
            {
                test = heightHitinfo.point;
                ParkourActionData parkourData = PickAParkourAnimation(heightHitinfo.point);
                animatorController.PlayParkourAnimation(parkourData.ActionName);
                StartCoroutine(PerformParkourActions(heightHitinfo, parkourData));
            }
        }
    }

    IEnumerator PerformParkourActions(RaycastHit heightHit, ParkourActionData parkourData)
    {
        performingAction = true;

        //We have to wait a frame after playing an animation. Cz animation is played in next frame!!
        yield return null;

        //Target matching??
        if (parkourData.shouldTargetMatch)
            animatorController.AnimationMatchTarget(heightHit.point, transform.rotation, parkourData.avtarTarget, parkourData.positionXYZWeight, 0, parkourData.startTime, parkourData.endTime);

        characterLocomotion.SetControl(false);

        var animState = animatorController.GetCurrentStateInfo(0);
        yield return new WaitForSeconds(animState.length);

        characterLocomotion.SetControl(true);
        performingAction = false;
    }

    private ParkourActionData PickAParkourAnimation(Vector3 hitPoint)
    {
        foreach (var item in parkourActionsData)
        {
            if (item.CheckForPossibleAction(hitPoint, transform))
                return item;
        }

        Debug.LogError("Non Configured Animation Picked");
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(test, 0.2f);
    }
}

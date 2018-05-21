using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bottle Config", menuName = "Bottle/Configuration")]
public class BottleMovementConfiguration : ScriptableObject {

    [Header("Boost")]
    #region Boost

    public float BoostAcceleration;

    #endregion

    [Header("Jump")]
    #region Jump

    public float JumpHeight;

    #endregion

    [Header("Air Movement")]
    #region Air Movement

    public float MaxAirVelocity;
    public float AirAcceleration;

    public float YawSpeed;
    public float RollSpeed;
    public float PitchSpeed;

    public float AirMaxAngularVelocity;
    public float AirAngularDampening;

    #endregion

    [Header("Upright Movement")]
    #region Upright Movement

    public float MaxUprightVelocity;
    public float UprightAcceleration;

    public float TurningSpeed;

    public float UprightMaxAngularVelocity;
    public float UprightAngularDampening;

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCarInfo : MonoBehaviour
{

    //------------------------------------------------UNITY INPUTS--------------------------------------------------------------
    [Header("Basic input variables")]    
    [SerializeField]
    private float braking_distance = 1f;
    [SerializeField]
    private float horizontal_inputScalar = 1f;
    [SerializeField]
    private float verical_inputScalar = 1f;
    [SerializeField]
    private AnimationCurve node_distance_decelerationCurve;
    [SerializeField]
    private float node_distance_deceletationLimit = 20f;

    [Header("Random variance variables")]
    [SerializeField]
    private float max_node_distVariance  = 5f;
    [SerializeField]
    private float max_horizontalVariance  = 0.1f;
    [SerializeField]
    private float max_verticalVariance  = 0.1f;


    //------------------------------------------------LOCAL VARIABLES--------------------------------------------------------------

    //Raycast infornt of the car. if the line hits the wall within braking distance then brake
    public float BrakingDistance { get; set; }


    //Node related variables
    public AnimationCurve NodeDistanceDecelerationCurve { get; set; }
    public  float NodeDistanceDecelerationLimit { get; set; } = 20f;


    //input scalars
    public float HorizontalInputScalar { get; set; }
    public float VerticalInputScalar { get; set; }


    //Random Variance variables
    public float MaxNodeDistVariance { get; set; }
    public float MaxHorizontalVariance { get; set; }
    public float MaxVerticalVariance { get; set; }



    //------------------------------------------------LOCAL FUNCTIONS--------------------------------------------------------------

    //Start function if initalized through unity
    private void Start()
    {
        BrakingDistance = braking_distance;
        HorizontalInputScalar = horizontal_inputScalar;
        VerticalInputScalar = verical_inputScalar;

        NodeDistanceDecelerationCurve = node_distance_decelerationCurve;
        NodeDistanceDecelerationLimit = node_distance_deceletationLimit;

        MaxNodeDistVariance = max_node_distVariance;
        MaxHorizontalVariance = max_horizontalVariance;
        MaxVerticalVariance = max_verticalVariance;
    }

    //constructor if constructed soemwhere else
    public BotCarInfo(float _braking_distance, float _horizontal_inputScalar, float _vertical_inputScalar)
    {
        BrakingDistance = _braking_distance;
        HorizontalInputScalar = _horizontal_inputScalar;
        VerticalInputScalar = _vertical_inputScalar;
    }
}

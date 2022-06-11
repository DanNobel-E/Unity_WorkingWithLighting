using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum MinMaxMode { Constant, RNDBetween2Constants, Curve, RNDBetween2Curves}

public class PSOps : MonoBehaviour {
    public MinMaxMode CurrMinMaxMode;
    public float multiplier = 5.0f;
    [Range(0, 1)]
    public float evalNormTime = 0;
    MinMaxMode pastCurrMinMaxMode;
    AnimationCurve curveA, curveB;
    ParticleSystem.EmissionModule emissionModule;
    public ParticleSystem.MinMaxCurve CustomMinMaxCurve;
    public ParticleSystem.MinMaxGradient CustomMinMaxGradient;
    public float EvaluatedMinMaxValue;
    public Color EvaluatedMinMaxGradient;

    void Start () {
        pastCurrMinMaxMode = CurrMinMaxMode;
        // Get the emission module
        emissionModule = GetComponent<ParticleSystem>().emission;
        // Enable it and set a value (simple way)
        emissionModule.enabled = true;
        //emissionModule.rateOverTime = 15;
        curveA = new AnimationCurve();
        curveB = new AnimationCurve();
    }

    private void Update()
    {
        if(pastCurrMinMaxMode != CurrMinMaxMode)
        {
            pastCurrMinMaxMode = CurrMinMaxMode;
            changeMinMaxMode();
        }
        float pickedVal = emissionModule.rateOverTime.Evaluate(evalNormTime, 0.5f);
        Debug.Log("At time "+ evalNormTime + " emissionModule.rateOverTime value is: "+ pickedVal);
        //Evaluate the value between (0.5f lerping) the two curves
        EvaluatedMinMaxValue = CustomMinMaxCurve.Evaluate(evalNormTime, 0.5f);
        Debug.Log("At time " + evalNormTime + " CustomMinMaxCurve.Evaluate(evalNormTime, 0.5f): " + EvaluatedMinMaxValue);
        //Evaluate the value between (0.5f lerping) the two curves
        EvaluatedMinMaxGradient = CustomMinMaxGradient.Evaluate(evalNormTime, 0.5f);
        Debug.Log("At time " + evalNormTime + " CustomMinMaxGradient.Evaluate(evalNormTime, 0.5f): " + EvaluatedMinMaxGradient);
    }

    void changeMinMaxMode()
    {
        //--- MinMaxCurve modes ---
        switch (CurrMinMaxMode)
        {
            case (MinMaxMode.Constant):
                //* Constant
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(10.0f);
                break;
            case (MinMaxMode.RNDBetween2Constants):
                //* Random between two constants
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(10.0f, 20.0f);
                break;
            case (MinMaxMode.Curve):
                //* Curve
                //Read only: you can store your own curve and applying this to the module
                //Create simple linear curve
                curveA.AddKey(0.0f, 0.5f);
                curveA.AddKey(1.0f, 1.0f);
                //Apply the curve. The curve will go from 0.5*multiplier value to 1*multiplier value 
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(multiplier, curveA);
                break;
            case (MinMaxMode.RNDBetween2Curves):
                //* Random between two curves
                //Create simple horizontal straight line with value 0.5*multiplier value
                curveB.AddKey(0.0f, 0.5f);
                curveB.AddKey(1.0f, 0.5f);
                emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(multiplier, curveA, curveB);
                break;
        }
    }
}

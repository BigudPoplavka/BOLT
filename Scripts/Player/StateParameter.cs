using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class StateParameter
{
    public UnityEvent minReached = new UnityEvent();
    public UnityEvent maxReached = new UnityEvent();
    public UnityEvent<float> valueChanged = new UnityEvent<float>();

    [SerializeField] public float Value { get; private set; }
    [SerializeField] public float Min { get; private set; }
    [SerializeField] public float Max { get; private set; }

    public StateParameter(float value, float min, float max)
    {
        Value = value;
        Min = min;
        Max = max;
    }

    public StateParameter()
    {
        Value = 100;
        Min = 0;
        Max = 100;
    }

    public void IncreaseValue(float value)
    {
        if(Value + value < Max)
        {
            Value += value;
        }
        else
        {
            Value = Max;
            maxReached?.Invoke();
        }
        
        valueChanged?.Invoke(Value);
    }

    public void DecreaseValue(float value)
    {
        if (Value - value > Min)
        {
            Value -= value;
        }
        else
        {
            Value = Min;
            minReached?.Invoke();
        }

        valueChanged?.Invoke(Value);
    }

    public void ExpandMax(float value)
    {
        Max += value;
        valueChanged?.Invoke(Value);
    }

}

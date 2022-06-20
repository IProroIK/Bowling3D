using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _doorText;
    [SerializeField] private int _value;
    [SerializeField] private float _rightValue;
    [SerializeField] private operetions _operation;
    [SerializeField] private Collider _secondDoor;
    [SerializeField] private bool _isDouble;

    private void Awake()
    {
        switch(_operation)
        {
            case operetions.add:
                _doorText.text = "+ " + _value;
                break;
            case operetions.multipliy:
                _doorText.text = "x " + _value;
                break;
            case operetions.divide:
                _doorText.text = "% " + _value;
                break;
            case operetions.sub:
                _doorText.text = "- " + _value;
                break;
        }
    }

    private enum operetions
    {
        add,
        sub,
        multipliy,
        divide
    }


    public int CheckOperetion(int curentNumber)
    {
        int resualt;
        switch (_operation)
        {
            case operetions.add:
                resualt = curentNumber + _value;
                return resualt;
                break;
            case operetions.multipliy:
                resualt = curentNumber * _value;
                resualt -= curentNumber;
                return resualt;
                break;
            case operetions.divide:
                resualt = curentNumber / _value;
                return resualt;
                break;
            case operetions.sub:
                resualt = curentNumber - _value;
                return resualt;
                break;
        }
        return 0;
    }

    public void DeactivateSecondDoor()
    {
        if(_isDouble)
        {
            _secondDoor.enabled = false;
        }
    }
}

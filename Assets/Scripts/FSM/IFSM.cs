using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM
{
    void ChangeState(State newState);
}

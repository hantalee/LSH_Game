using System.Collections;
using System.Collections.Generic;

public interface IBuff
{
    void Init(string buffName);
    void Execute();
    IEnumerator Activation();
    void DeActivation();
}

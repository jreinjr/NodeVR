using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableReference<T0, T1> 
    where T1 : Variable<T0>
{
    public bool UseConstant = true;
    public T0 ConstantValue;
    public T1 Variable;

    public T0 Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

}

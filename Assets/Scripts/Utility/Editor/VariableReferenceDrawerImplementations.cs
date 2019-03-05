using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : VariableReferenceDrawer
{
}

[CustomPropertyDrawer(typeof(IntReference))]
public class IntReferenceDrawer : VariableReferenceDrawer
{
}

[CustomPropertyDrawer(typeof(BoolReference))]
public class BoolReferenceDrawer : VariableReferenceDrawer
{
}

using UnityEditor;
using UnityEngine;

public enum VariableReferenceOption { Constant, Variable };

public abstract class VariableReferenceDrawer : PropertyDrawer
{
    private static string k_UseConstant = "UseConstant";
    private static string k_ConstantValue = "ConstantValue";
    private static string k_Variable = "Variable";

    private SerializedProperty p_UseConstant;
    private SerializedProperty p_ConstantValue;
    private SerializedProperty p_Variable;

    private VariableReferenceOption constantOrVariableOption;

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        p_UseConstant = property.FindPropertyRelative(k_UseConstant);
        p_ConstantValue = property.FindPropertyRelative(k_ConstantValue);
        p_Variable = property.FindPropertyRelative(k_Variable);

        constantOrVariableOption = p_UseConstant.boolValue ? VariableReferenceOption.Constant : VariableReferenceOption.Variable;

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var optionPosition = new Rect(position.x - 10, position.y, 10, 10);

        EditorGUI.BeginChangeCheck();

        constantOrVariableOption = (VariableReferenceOption)EditorGUI.EnumPopup(optionPosition, constantOrVariableOption);
        p_UseConstant.boolValue = (constantOrVariableOption == VariableReferenceOption.Constant);

        EditorGUI.EndChangeCheck();
        
        if (p_UseConstant.boolValue)
        {
            EditorGUI.PropertyField(position, p_ConstantValue, GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(position, p_Variable, GUIContent.none);
        }


        EditorGUI.EndProperty();


        property.serializedObject.ApplyModifiedProperties();

    }
}

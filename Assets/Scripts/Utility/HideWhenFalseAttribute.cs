﻿using UnityEngine;
using System.Collections;

public class HideWhenFalseAttribute : PropertyAttribute
{
    public readonly string hideBoolean;

    public HideWhenFalseAttribute(string booleanName)
    {
        this.hideBoolean = booleanName;
    }
}
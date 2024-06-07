using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a Extention method class for Animator to get common fuctionality to every animator on the fly
/// </summary>
public static class AnimatorExtentionClass
{
    /// <summary>
    /// Will Reset all the Parameters in target animator
    /// </summary>
    /// <param name="animator"></param>
    public static void ResetAnimator(this Animator animator , bool canResetTrigers = true, bool canResetBool = true, bool canResetInt = true, bool canResetFloat = true)
    {
        foreach (var trigger in animator.parameters)
        {
            switch (trigger.type)
            {
                case AnimatorControllerParameterType.Float:
                    if(canResetFloat)
                        animator.SetFloat(trigger.name , 0);
                    break;
                case AnimatorControllerParameterType.Int:
                    if (canResetInt)
                        animator.SetInteger(trigger.name, 0);
                    break;
                case AnimatorControllerParameterType.Bool:
                    if (canResetBool)
                        animator.SetBool(trigger.name, false);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if(canResetTrigers)
                    animator.ResetTrigger(trigger.name);
                    break;
            }
        }
    }


    /// <summary>
    /// Used to make animation trasition based on triger paramerter seted up in animator
    /// </summary>
    /// <param name="a_trigerKey">Trigger Parameter Key</param>
    public static void TrigerAnimation<TEnum>(this Animator animator , TEnum a_trigerKey) where TEnum : System.Enum
    {
        animator.SetTrigger(a_trigerKey.ToString());
    }

    /// <summary>
    /// Will set the Int parameter value based on enum
    /// </summary>
    /// <param name="a_trigerKey"></param>
    /// <param name="value"></param>
    public static void SetAnimatorIntKey<TEnum>(this Animator animator, TEnum a_trigerKey, int value) where TEnum : System.Enum
    {
        animator.SetInteger(a_trigerKey.ToString(), value);
    }


    /// <summary>
    /// Will set the float parameter value based on enum
    /// </summary>
    /// <param name="a_trigerKey"></param>
    /// <param name="value"></param>
    public static void SetAnimatorFloatKey<TEnum>(this Animator animator, TEnum a_trigerKey, float value) where TEnum : System.Enum
    {
        animator.SetFloat(a_trigerKey.ToString(), value);
    }


    /// <summary>
    /// Will set the bool parameter value based on enum
    /// </summary>
    /// <param name="a_trigerKey"></param>
    /// <param name="value"></param>
    public static void SetAnimatorBoolKey<TEnum>(this Animator animator, TEnum a_trigerKey, bool isActive) where TEnum : System.Enum
    {
        animator.SetBool(a_trigerKey.ToString(), isActive);
    }

}

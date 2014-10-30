using UnityEngine;
using System;
using System.Collections;

namespace UnityConsole.Internal
{
    internal static class TypeSafeInvokeMonoBehaviourExtensions
    {
        public static void Invoke(this MonoBehaviour mb, Action action, float delay)
        {
            if (delay == 0f)
                action();
            else
                mb.StartCoroutine(DelayedInvoke(action, delay));
        }

        public static void Invoke<T>(this MonoBehaviour mb, Action<T> action, T arg, float delay)
        {
            if (delay == 0f)
                action(arg);
            else
                mb.StartCoroutine(DelayedInvoke(action, arg, delay));
        }

        public static void Invoke<T1, T2>(this MonoBehaviour mb, Action<T1, T2> action, T1 arg1, T2 arg2, float delay)
        {
            if (delay == 0f)
                action(arg1, arg2);
            else
                mb.StartCoroutine(DelayedInvoke(action, arg1, arg2, delay));
        }

        public static void Invoke<T1, T2, T3>(this MonoBehaviour mb, Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, float delay)
        {
            if (delay == 0f)
                action(arg1, arg2, arg3);
            else
                mb.StartCoroutine(DelayedInvoke(action, arg1, arg2, arg3, delay));
        }

        public static void Invoke<T1, T2, T3, T4>(this MonoBehaviour mb, Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, float delay)
        {
            if (delay == 0f)
                action(arg1, arg2, arg3, arg4);
            else
                mb.StartCoroutine(DelayedInvoke(action, arg1, arg2, arg3, arg4, delay));
        }

        private static IEnumerator DelayedInvoke(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        private static IEnumerator DelayedInvoke<T>(Action<T> action, T arg, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(arg);
        }

        private static IEnumerator DelayedInvoke<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(arg1, arg2);
        }

        private static IEnumerator DelayedInvoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(arg1, arg2, arg3);
        }

        private static IEnumerator DelayedInvoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(arg1, arg2, arg3, arg4);
        }
    }
}

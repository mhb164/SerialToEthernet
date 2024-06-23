using System;
using System.ComponentModel;
using System.Windows.Forms;

//https://github.com/mhb164/Boilerplates
namespace System.Windows.Forms
{
    public static partial class UIExtensions
    {
        public static void InvokeIfRequired(this ISynchronizeInvoke item, MethodInvoker action)
        {
            if (!item.InvokeRequired)
            {
                action();
                return;
            }

            item.Invoke(action, null);
        }

        public static void InvokeIfRequired(this ISynchronizeInvoke item, Action action)
        {
            if (!item.InvokeRequired)
            {
                action();
                return;
            }

            item.Invoke(action, null);
        }

        public static T InvokeIfRequired<T>(this ISynchronizeInvoke item, Func<T> function)
        {
            if (!item.InvokeRequired)
            {
                return function();
            }

            return (T)item.Invoke(function, null);
        }

        public static void InvokeIfNecessary(this Form item, MethodInvoker action)
            => InvokeIfRequired(item as ISynchronizeInvoke, action);

        public static void InvokeIfNecessary(this Control item, MethodInvoker action)
            => InvokeIfRequired(item as ISynchronizeInvoke, action);

        public static void InvokeIfNecessary(this UserControl item, MethodInvoker action)
            => InvokeIfRequired(item as ISynchronizeInvoke, action);

    }
}
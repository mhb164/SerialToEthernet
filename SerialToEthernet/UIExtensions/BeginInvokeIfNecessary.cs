using System;
using System.ComponentModel;
using System.Windows.Forms;

//https://github.com/mhb164/Boilerplates
namespace System.Windows.Forms
{
    public static partial class UIExtensions
    {
        public static void BeginInvokeIfRequired(this ISynchronizeInvoke item, MethodInvoker action)
        {
            if (!item.InvokeRequired)
            {
                action();
                return;
            }

            item.BeginInvoke(action, null);
        }

        public static void BeginInvokeIfRequired(this ISynchronizeInvoke item, Action action)
        {
            if (!item.InvokeRequired)
            {
                action();
                return;
            }

            item.BeginInvoke(action, null);
        }

        public static T BeginInvokeIfRequired<T>(this ISynchronizeInvoke item, Func<T> function)
        {
            if (!item.InvokeRequired)
            {
                return function();
            }

            return (T)item.BeginInvoke(function, null);
        }

        public static void BeginInvokeIfNecessary(this Form item, MethodInvoker action)
            => BeginInvokeIfRequired(item as ISynchronizeInvoke, action);

        public static void BeginInvokeIfNecessary(this Control item, MethodInvoker action)
            => BeginInvokeIfRequired(item as ISynchronizeInvoke, action);

        public static void BeginInvokeIfNecessary(this UserControl item, MethodInvoker action)
            => BeginInvokeIfRequired(item as ISynchronizeInvoke, action);

    }
}
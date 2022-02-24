using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MisFrame.Core.Common
{
    public class AsyncHelper
    {
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (method.ReturnType == typeof(Task) || method.ReturnType.BaseType == typeof(Task));
        }

        public static T WaitAsyncMethodForFinally<T>(Func<Task<T>> func,Action<Exception> funcEx) where T : class
        {

            System.Threading.Tasks.TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            
            Task.Run(async () =>
            {
                try
                {
                    var t = await func.Invoke();
                    tcs.SetResult(t);
                }catch(Exception ex)
                {
                    if (funcEx != null)
                        funcEx(ex);
                }
            });

            return tcs.Task.Result;
        }
    }
}

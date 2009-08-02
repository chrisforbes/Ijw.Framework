using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IjwFramework.Types
{
    public class Future<T>
    {
        Func<T> f;
        IAsyncResult async;
        T value;
        bool hasValue = false;

        public Future(Func<T> f) { this.f = f; async = f.BeginInvoke(null, null); }

        T Value
        {
            get
            {
                if (!hasValue)
                {
                    if (!async.IsCompleted) async.AsyncWaitHandle.WaitOne();
                    value = f.EndInvoke(async);
                    hasValue = true;
                }
                return value;
            }
        }

        public bool HasValue { get { return hasValue || async.IsCompleted; } }
        public static implicit operator T(Future<T> f) { return f.Value; }
    }

    public static class Future
    {
        public static Future<T> New<T>(Func<T> f) { return new Future<T>(f); }
    }
}

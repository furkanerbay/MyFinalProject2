using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { } //Öncesinde
        protected virtual void OnAfter(IInvocation invocation) { } //Sonrasında
        protected virtual void OnException(IInvocation invocation, System.Exception e) { } //Hata aldıgında
        protected virtual void OnSuccess(IInvocation invocation) { } //Başarılı oldugunuda.
        public override void Intercept(IInvocation invocation) //Az önce boş olanı eziyoruz. Nerede calısacaklarını yazıyoruz.
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}

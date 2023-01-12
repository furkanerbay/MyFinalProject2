using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute> //Sınıf attributelerını listele.
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name) //Method atributelerini bul.
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes); 

            return classAttributes.OrderBy(x => x.Priority).ToArray(); //Sırayla sıralaa.
        }
    }
}

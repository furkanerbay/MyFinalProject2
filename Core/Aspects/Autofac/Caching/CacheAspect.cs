using Autofac.Core;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //Reflected Type -> Yani yol.
            var arguments = invocation.Arguments.ToList(); //Parametreler = Methodun parametrelerini listeye çevir. 
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //varsa soldakini yoksa sağdakini ekle.
            //Sonra methodun eğer ki ilgili parametre değerleri var ise ona göre o parametre değerini ekliyoruz.
            //Parametre yoksa null.
            if (_cacheManager.IsAdd(key)) //Bellekte var mı ?
            {
                invocation.ReturnValue = _cacheManager.Get(key); //Sen methodu çalıştırmadan geri dön.CacheManagerdan çalıştır.
                return;
            }
            invocation.Proceed(); //Yoksa inovation'ı methodu calistir.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //Methodu cache'e ekle.
        }

    }
}

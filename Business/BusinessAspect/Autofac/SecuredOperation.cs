using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //JWT'ı da göndererek istek yapıyoruz.Her bir istek için 1 HTTP Context oluşur. Thread

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();//Servis mimarisine ulaş Interface'i new'let.
            //ServiceTool injection alt yapımızı okuyabilmemize yarayan araç.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) //claimlerin içerisinde ilgili rol varsa
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);//Rol yoksa yetkin yok.
        }
    }
}

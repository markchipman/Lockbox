﻿using System.Security.Claims;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Security;

namespace Lockbox.Api.Modules
{
    public abstract class ModuleBase : NancyModule
    {
        protected ModuleBase(string modulePath = "") : base(modulePath)
        {
        }

        protected void RequiresAdmin()
        {
            this.RequiresAuthentication();
            this.RequiresClaims(x => x.Type == ClaimTypes.Role && x.Value == "admin");
        }

        protected T BindRequest<T>() => this.Bind<T>();

        protected string CurrentUsername => Context.CurrentUser.Identity.Name;

        protected Negotiator Created(string endpoint)
            => Negotiate.WithHeader("Location", $"{Request.Url.SiteBase}/{endpoint}")
                .WithStatusCode(HttpStatusCode.Created);
    }
}
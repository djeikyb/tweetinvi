﻿using Tweetinvi.AspNet.Core.Logic;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi.Models;

namespace Tweetinvi.AspNet.Modules
{
    public class TweetinviAspNetModule : ITweetinviModule
    {
        public void Initialize(ITweetinviContainer container)
        {
            container.RegisterType<IWebhooksHelper, WebhooksHelper>(RegistrationLifetime.InstancePerApplication);
            container.RegisterType<IWebhooksRoutes, WebhooksRoutes>(RegistrationLifetime.InstancePerApplication);
            container.RegisterType<IWebhookRouter, WebhookRouter>();
            container.RegisterType<IAccountActivityRequestHandler, AccountActivityRequestHandler>();
        }
    }
}

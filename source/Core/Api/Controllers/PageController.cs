﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Net.Http;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Thinktecture.IdentityManager.Api.Filters;
using Thinktecture.IdentityManager.Assets;

namespace Thinktecture.IdentityManager.Api.Controllers
{
    [NoCache]
    [SecurityHeaders]
    public class PageController : ApiController
    {
        IdentityManagerConfiguration idmConfig;
        public PageController(IdentityManagerConfiguration idmConfig)
        {
            if (idmConfig == null) throw new ArgumentNullException("idmConfig");

            this.idmConfig = idmConfig;
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Index()
        {
            if (idmConfig.SecurityMode == SecurityMode.LocalMachine &&
                (User == null || User.Identity == null || User.Identity.IsAuthenticated == false))
            {
                return new EmbeddedHtmlResult(Request, "Thinktecture.IdentityManager.Assets.Templates.accessdenied.html");
            }

            return new EmbeddedHtmlResult(Request, "Thinktecture.IdentityManager.Assets.Templates.index.html", idmConfig.OAuth2Configuration);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Frame()
        {
            if (idmConfig.SecurityMode != SecurityMode.OAuth2)
            {
                return NotFound();
            }

            return new EmbeddedHtmlResult(Request, "Thinktecture.IdentityManager.Assets.Templates.frame.html");
        }
    }
}

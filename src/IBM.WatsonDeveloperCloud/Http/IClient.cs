/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using IBM.WatsonDeveloperCloud.Http.Filters;
using System.Threading;

namespace IBM.WatsonDeveloperCloud.Http
{
    public interface IClient : IDisposable
    {
        HttpClient BaseClient { get; }

        MediaTypeFormatterCollection Formatters { get; }

        List<IHttpFilter> Filters { get; }

        IClient WithAuthentication(string userName, string password);

        IRequest Delete(string resource, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Get(string resource, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Post(string resource, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Post<TBody>(string resource, TBody body, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Put(string resource, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Put<TBody>(string resource, TBody body, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Send(HttpMethod method, string resource, CancellationToken cancellationToken = default(CancellationToken));

        IRequest Send(HttpRequestMessage message, CancellationToken cancellationToken = default(CancellationToken));
    }
}
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
using System.Net.Http.Headers;
using System.Text;
using IBM.WatsonDeveloperCloud.Http.Filters;
using System.Threading;

namespace IBM.WatsonDeveloperCloud.Http
{
    public class WatsonHttpClient : IClient
    {
        private bool IsDisposed;

        public List<IHttpFilter> Filters { get; private set; }

        public HttpClient BaseClient { get; private set; }

        public MediaTypeFormatterCollection Formatters { get; protected set; }

        public WatsonHttpClient(string baseUri)
        {
            this.Filters = new List<IHttpFilter> { new ErrorFilter() };
            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);

            this.Formatters = new MediaTypeFormatterCollection();
        }

        public WatsonHttpClient(string baseUri, string userName, string password)
        {
            this.BaseClient = new HttpClient();

            this.Filters = new List<IHttpFilter> { new ErrorFilter() };

            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);

            this.Formatters = new MediaTypeFormatterCollection();

            this.WithAuthentication(userName, password);
        }

        public WatsonHttpClient(string baseUri, string userName, string password, HttpClient client)
        {
            this.BaseClient = client;
            this.Filters = new List<IHttpFilter> { new ErrorFilter() };
            if (baseUri != null)
                this.BaseClient.BaseAddress = new Uri(baseUri);
            this.Formatters = new MediaTypeFormatterCollection();

            this.WithAuthentication(userName, password);
        }

        public IClient WithAuthentication(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                string auth = string.Format("{0}:{1}", userName, password);
                string auth64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));

                this.BaseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth64);
            }

            return this;
        }

        public IRequest Delete(string resource, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Send(HttpMethod.Delete, resource, cancellationToken);
        }

        public IRequest Get(string resource, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Send(HttpMethod.Get, resource, cancellationToken);
        }

        public IRequest Post(string resource, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Send(HttpMethod.Post, resource, cancellationToken);
        }

        public IRequest Post<TBody>(string resource, TBody body, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Post(resource, cancellationToken).WithBody(body);
        }

        public IRequest Put(string resource, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Send(HttpMethod.Put, resource, cancellationToken);
        }

        public IRequest Put<TBody>(string resource, TBody body, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Put(resource, cancellationToken).WithBody(body);
        }

        public virtual IRequest Send(HttpMethod method, string resource, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.AssertNotDisposed();

            Uri uri = new Uri(this.BaseClient.BaseAddress, resource);
            HttpRequestMessage message = HttpFactory.GetRequestMessage(method, uri, this.Formatters);
            return this.Send(message, cancellationToken);
        }

        public virtual IRequest Send(HttpRequestMessage message, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.AssertNotDisposed();
            return new Request(message, this.Formatters, request => this.BaseClient.SendAsync(request.Message, cancellationToken), this.Filters.ToArray());
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void AssertNotDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(nameof(WatsonHttpClient));
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.IsDisposed)
                return;

            if (isDisposing)
                this.BaseClient.Dispose();

            this.IsDisposed = true;
        }

        ~WatsonHttpClient()
        {
            Dispose(false);
        }
    }
}
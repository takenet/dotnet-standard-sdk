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

using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public class ConversationService : WatsonService, IConversationService
    {
        const string SERVICE_NAME = "conversation";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        /** The Constant CONVERSATION_VERSION_DATE_2017_05_26. */
        public static string CONVERSATION_VERSION_DATE_2017_05_26 = "2017-05-26";

        public ConversationService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public ConversationService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            VersionDate = versionDate;
        }

        public ConversationService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public async Task<ExampleResponse> CreateCounterexampleAsync(string workspaceId, CreateExample body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateExample>(body)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteCounterexampleAsync(string workspaceId, string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ExampleResponse> GetCounterexampleAsync(string workspaceId, string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<CounterexampleCollectionResponse> ListCounterexamplesAsync(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            CounterexampleCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<CounterexampleCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ExampleResponse> UpdateCounterexampleAsync(string workspaceId, string text, UpdateExample body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/counterexamples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateExample>(body)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<EntityResponse> CreateEntityAsync(string workspaceId, CreateEntity body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateEntity>(body)
                                .As<EntityResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteEntityAsync(string workspaceId, string entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<EntityExportResponse> GetEntityAsync(string workspaceId, string entity, bool? export = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityExportResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<EntityExportResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<EntityCollectionResponse> ListEntitiesAsync(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<EntityCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<EntityResponse> UpdateEntityAsync(string workspaceId, string entity, UpdateEntity body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            EntityResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateEntity>(body)
                                .As<EntityResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<ExampleResponse> CreateExampleAsync(string workspaceId, string intent, CreateExample body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateExample>(body)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteExampleAsync(string workspaceId, string intent, string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ExampleResponse> GetExampleAsync(string workspaceId, string intent, string text, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ExampleCollectionResponse> ListExamplesAsync(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<ExampleCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ExampleResponse> UpdateExampleAsync(string workspaceId, string intent, string text, UpdateExample body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ExampleResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}/examples/{text}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateExample>(body)
                                .As<ExampleResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<IntentResponse> CreateIntentAsync(string workspaceId, CreateIntent body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateIntent>(body)
                                .As<IntentResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteIntentAsync(string workspaceId, string intent, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<IntentExportResponse> GetIntentAsync(string workspaceId, string intent, bool? export = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentExportResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<IntentExportResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<IntentCollectionResponse> ListIntentsAsync(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<IntentCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<IntentResponse> UpdateIntentAsync(string workspaceId, string intent, UpdateIntent body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(intent))
                throw new ArgumentNullException(nameof(intent));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            IntentResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/intents/{intent}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateIntent>(body)
                                .As<IntentResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<LogCollectionResponse> ListLogsAsync(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            LogCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/logs", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("sort", sort)
                                .WithArgument("filter", filter)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("cursor", cursor)
                                .As<LogCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<MessageResponse> MessageAsync(string workspaceId, MessageRequest body = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            MessageResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/message", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<MessageRequest>(body)
                                .As<MessageResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<SynonymResponse> CreateSynonymAsync(string workspaceId, string entity, string value, CreateSynonym body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateSynonym>(body)
                                .As<SynonymResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteSynonymAsync(string workspaceId, string entity, string value, string synonym, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<SynonymResponse> GetSynonymAsync(string workspaceId, string entity, string value, string synonym, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<SynonymResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<SynonymCollectionResponse> ListSynonymsAsync(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<SynonymCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<SynonymResponse> UpdateSynonymAsync(string workspaceId, string entity, string value, string synonym, UpdateSynonym body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(synonym))
                throw new ArgumentNullException(nameof(synonym));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            SynonymResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateSynonym>(body)
                                .As<SynonymResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<ValueResponse> CreateValueAsync(string workspaceId, string entity, CreateValue body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateValue>(body)
                                .As<ValueResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteValueAsync(string workspaceId, string entity, string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ValueExportResponse> GetValueAsync(string workspaceId, string entity, string value, bool? export = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueExportResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<ValueExportResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ValueCollectionResponse> ListValuesAsync(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<ValueCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<ValueResponse> UpdateValueAsync(string workspaceId, string entity, string value, UpdateValue body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));
            if (string.IsNullOrEmpty(entity))
                throw new ArgumentNullException(nameof(entity));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            ValueResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}/entities/{entity}/values/{value}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateValue>(body)
                                .As<ValueResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public async Task<WorkspaceResponse> CreateWorkspaceAsync(CreateWorkspace body = null, CancellationToken cancellationToken = default(CancellationToken))
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateWorkspace>(body)
                                .As<WorkspaceResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<object> DeleteWorkspaceAsync(string workspaceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            object result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Delete($"{this.Endpoint}/v1/workspaces/{workspaceId}")
                                .WithArgument("version", VersionDate)
                                .As<object>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<WorkspaceExportResponse> GetWorkspaceAsync(string workspaceId, bool? export = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceExportResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces/{workspaceId}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("export", export)
                                .As<WorkspaceExportResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<WorkspaceCollectionResponse> ListWorkspacesAsync(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken))
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceCollectionResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Get($"{this.Endpoint}/v1/workspaces", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithArgument("page_limit", pageLimit)
                                .WithArgument("include_count", includeCount)
                                .WithArgument("sort", sort)
                                .WithArgument("cursor", cursor)
                                .As<WorkspaceCollectionResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public async Task<WorkspaceResponse> UpdateWorkspaceAsync(string workspaceId, UpdateWorkspace body = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(workspaceId))
                throw new ArgumentNullException(nameof(workspaceId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'CONVERSATION_VERSION_DATE_2017_05_26'");

            WorkspaceResponse result = null;

            try
            {
                result = await this.Client.WithAuthentication(this.UserName, this.Password)
                                .Post($"{this.Endpoint}/v1/workspaces/{workspaceId}", cancellationToken)
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateWorkspace>(body)
                                .As<WorkspaceResponse>();
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}

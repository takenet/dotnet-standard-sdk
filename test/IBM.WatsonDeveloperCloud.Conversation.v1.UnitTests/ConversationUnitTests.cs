﻿/**
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.UnitTests
{
    [TestClass]
    public class ConversationUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            ConversationService service =
                new ConversationService(null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            ConversationService service =
                new ConversationService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            ConversationService service =
                new ConversationService(null, "password", ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            ConversationService service =
                new ConversationService("username", null, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            ConversationService service =
                new ConversationService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            ConversationService service =
                new ConversationService("username", "password", ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            ConversationService service =
                new ConversationService();

            Assert.IsNotNull(service);
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                    .Returns(client);

            return client;
        }
        #endregion

        #region Counter Examples
        #region Create Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            service.CreateCounterexample(null, example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.CreateCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            service.CreateCounterexample("workspaceId", example);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateCounterExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateCounterexample("workspaceId", example);
        }

        [TestMethod]
        public void CreateCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateExample>(example)
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateCounterexample("workspaceId", example);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
        }
        #endregion

        #region Delete Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteCounterexample(null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteCounterexample("workspaceId", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteCounterExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteCounterexample("workspaceId", "example");
        }

        [TestMethod]
        public void DeleteCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteCounterexample("workspaceId", "example");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetCounterexample(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetCounterexample("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetCounterExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetCounterexample("workspaceId", "text");
        }

        [TestMethod]
        public void GetCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetCounterexample("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
        }
        #endregion

        #region List Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCounterExamples_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListCounterexamples(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCounterExamples_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListCounterexamples("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListCounterExamples_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListCounterexamples("workspaceId");
        }

        [TestMethod]
        public void ListCounterExamples_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            CounterexampleCollectionResponse response = new CounterexampleCollectionResponse()
            {
                Counterexamples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Text = "text",
                        Updated = DateTime.Now,
                        Created = DateTime.Now
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<CounterexampleCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListCounterexamples("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Counterexamples);
            Assert.IsTrue(result.Counterexamples.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Counterexamples[0].Text));
            Assert.IsNotNull(result.Counterexamples[0].Created);
            Assert.IsNotNull(result.Counterexamples[0].Updated);
            Assert.IsTrue(result.Counterexamples[0].Text == "text");
        }
        #endregion

        #region Update Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateCounterexample(null, "text", example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_Text()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateCounterexample("workspaceId", null, example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateCounterexample("workspaceId", "text", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateCounterexample("workspaceId", "text", example);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateCounterExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateCounterexample("workspaceId", "text", example);
        }

        [TestMethod]
        public void UpdateCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateExample>(example)
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateCounterexample("workspaceId", "text", example);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
        }
        #endregion
        #endregion

        #region Entities
        #region Create Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateEntity entity = new CreateEntity()
            {
                Entity = "entity",
                Description = "description"
            };

            service.CreateEntity(null, entity);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");

            service.CreateEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateEntity entity = new CreateEntity()
            {
                Entity = "entity",
                Description = "description"
            };

            service.CreateEntity("workspaceId", entity);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateEntity_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateEntity entity = new CreateEntity()
            {
                Entity = "entity",
                Description = "description"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateEntity("workspaceId", entity);
        }

        [TestMethod]
        public void CreateEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            EntityResponse response = new EntityResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Entity = "entity",
                Description = "description"
            };
            #endregion

            CreateEntity entity = new CreateEntity()
            {
                Entity = "entity",
                Description = "description"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateEntity>(entity)
                .Returns(request);
            request.As<EntityResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateEntity("workspaceId", entity);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Entity);
            Assert.IsTrue(result.Entity == "entity");
            Assert.IsNotNull(result.Description);
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region Delete Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteEntity(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteEntity("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteEntity_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteEntity("workspaceId", "entity");
        }

        [TestMethod]
        public void DeleteEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteEntity("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetEntity(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetEntity("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetEntity_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetEntity("workspaceId", "entity");
        }

        [TestMethod]
        public void GetEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            EntityExportResponse response = new EntityExportResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Entity = "entity",
                Description = "description"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<EntityExportResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetEntity("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Entity);
            Assert.IsTrue(result.Entity == "entity");
            Assert.IsNotNull(result.Description);
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region List Entities
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListEntities_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListEntities(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListEntities_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListEntities("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListEntities_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListEntities("workspaceId");
        }

        [TestMethod]
        public void ListEntities_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            EntityCollectionResponse response = new EntityCollectionResponse()
            {
                Entities = new List<EntityExportResponse>()
                {
                    new EntityExportResponse()
                    {
                        Entity = "entity",
                        Description = "description",
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        Metadata = new object() { },
                        FuzzyMatch = true,
                        Values = new List<ValueExportResponse>()
                        {
                            new ValueExportResponse()
                            {
                                Value = "value",
                                Metadata = new object() { },
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Synonyms = new List<string>()
                                {
                                    "synonym"
                                }
                            }
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    RefreshUrl = "refreshUrl",
                    NextUrl = "nextUrl",
                    Total = 1,
                    Matched = 1
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<EntityCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListEntities("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Entities);
            Assert.IsTrue(result.Entities.Count > 0);
            Assert.IsNotNull(result.Entities[0].Created);
            Assert.IsNotNull(result.Entities[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Entities[0].Entity));
            Assert.IsTrue(result.Entities[0].Entity == "entity");
            Assert.IsTrue(!string.IsNullOrEmpty(result.Entities[0].Description));
            Assert.IsTrue(result.Entities[0].Description == "description");
            Assert.IsNotNull(result.Entities[0].Metadata);
            Assert.IsTrue((bool)result.Entities[0].FuzzyMatch);
            Assert.IsNotNull(result.Entities[0].Values);
            Assert.IsTrue(result.Entities[0].Values.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Entities[0].Values[0].Value));
            Assert.IsTrue(result.Entities[0].Values[0].Value == "value");
            Assert.IsNotNull(result.Entities[0].Values[0].Metadata);
            Assert.IsNotNull(result.Entities[0].Values[0].Created);
            Assert.IsNotNull(result.Entities[0].Values[0].Updated);
            Assert.IsNotNull(result.Entities[0].Values[0].Synonyms);
            Assert.IsTrue(result.Entities[0].Values[0].Synonyms.Count > 0);
            Assert.IsTrue(result.Entities[0].Values[0].Synonyms[0] == "synonym");
            Assert.IsTrue(result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Pagination.Total == 1);
            Assert.IsTrue(result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateEntity entity = new UpdateEntity()
            {
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true,
                Values = new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                }
            };

            service.UpdateEntity(null, "entity", entity);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_Text()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateEntity entity = new UpdateEntity()
            {
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true,
                Values = new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                }
            };

            service.UpdateEntity("workspaceId", null, entity);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateEntity("workspaceId", "text", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateEntity entity = new UpdateEntity()
            {
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true,
                Values = new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                }
            };

            service.UpdateEntity("workspaceId", "text", entity);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateEntity_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateEntity entity = new UpdateEntity()
            {
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true,
                Values = new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateEntity("workspaceId", "text", entity);
        }

        [TestMethod]
        public void UpdateEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            EntityResponse response = new EntityResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true
            };
            #endregion

            UpdateEntity entity = new UpdateEntity()
            {
                Entity = "entity",
                Description = "description",
                Metadata = new object() { },
                FuzzyMatch = true,
                Values = new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateEntity>(entity)
                .Returns(request);
            request.As<EntityResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateEntity("workspaceId", "text", entity);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Entity));
            Assert.IsTrue(result.Entity == "entity");
            Assert.IsTrue(!string.IsNullOrEmpty(result.Description));
            Assert.IsTrue(result.Description == "description");
            Assert.IsNotNull(result.Metadata);
            Assert.IsTrue((bool)result.FuzzyMatch);
        }
        #endregion
        #endregion

        #region Examples
        #region Create Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            service.CreateExample(null, "intent", example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            service.CreateExample("workspaceId", null, example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.CreateExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            service.CreateExample("workspaceId", "intent", example);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateExample("workspaceId", "intent", example);
        }

        [TestMethod]
        public void CreateExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            CreateExample example = new CreateExample()
            {
                Text = "text"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateExample>(example)
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateExample("workspaceId", "intent", example);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion

        #region Delete Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteExample(null, "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteExample("workspaceId", null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");

            service.DeleteExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteExample("workspaceId", "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteExample("workspaceId", "intent", "example");
        }

        [TestMethod]
        public void DeleteExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteExample("workspaceId", "intent", "example");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetExample(null, "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetExample("workspaceId", null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_Example()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetExample("workspaceId", "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetExample("workspaceId", "intent", "example");
        }

        [TestMethod]
        public void GetExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetExample("workspaceId", "intent", "example");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
        }
        #endregion

        #region List Examples
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListExamples(null, "intent");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListExamples("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListExamples("workspaceId", "intent");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListExamples_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListExamples("workspaceId", "intent");
        }

        [TestMethod]
        public void ListExamples_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleCollectionResponse response = new ExampleCollectionResponse()
            {
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Text = "text",
                        Updated = DateTime.Now,
                        Created = DateTime.Now
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ExampleCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListExamples("workspaceId", "intent");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Examples);
            Assert.IsTrue(result.Examples.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Examples[0].Text));
            Assert.IsNotNull(result.Examples[0].Created);
            Assert.IsNotNull(result.Examples[0].Updated);
            Assert.IsTrue(result.Examples[0].Text == "text");
        }
        #endregion

        #region Update Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateExample(null, "intent", "text", example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateExample("workspaceId", null, "text", example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_Text()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateExample("workspaceId", "intent", null, example);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");

            service.UpdateExample("workspaceId", "intent", "text", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            service.UpdateExample("workspaceId", "intent", "text", example);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateExample("workspaceId", "intent", "text", example);
        }

        [TestMethod]
        public void UpdateExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ExampleResponse response = new ExampleResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Text = "text"
            };
            #endregion

            UpdateExample example = new UpdateExample()
            {
                Text = "text"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateExample>(example)
                .Returns(request);
            request.As<ExampleResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateExample("workspaceId", "intent", "text", example);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Text == "text");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion
        #endregion

        #region Intents
        #region Create Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateIntent intent = new CreateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "example"
                    }
                }
            };

            service.CreateIntent(null, intent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.CreateIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateIntent intent = new CreateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "example"
                    }
                }
            };

            service.CreateIntent("workspaceId", intent);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateIntent_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateIntent intent = new CreateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "example"
                    }
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateIntent("workspaceId", intent);
        }

        [TestMethod]
        public void CreateIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            IntentResponse response = new IntentResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Intent = "intent",
                Description = "description"
            };
            #endregion

            CreateIntent Intent = new CreateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "example"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateIntent>(Intent)
                .Returns(request);
            request.As<IntentResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateIntent("workspaceId", Intent);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Intent == "intent");
            Assert.IsTrue(result.Description == "description");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion

        #region Delete Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteIntent(null, "intent");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteIntent("workspaceId", "intent");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteIntent_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteIntent("workspaceId", "intent");
        }

        [TestMethod]
        public void DeleteIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteIntent("workspaceId", "intent");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetIntent(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_Intent()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetIntent("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetIntent_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetIntent("workspaceId", "text");
        }

        [TestMethod]
        public void GetIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            IntentExportResponse response = new IntentExportResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Intent = "intent",
                Description = "description",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Text = "text",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.As<IntentExportResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetIntent("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsTrue(result.Intent == "intent");
            Assert.IsTrue(result.Description == "description");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Examples);
            Assert.IsTrue(result.Examples.Count > 0);
            Assert.IsNotNull(result.Examples[0].Created);
            Assert.IsNotNull(result.Examples[0].Updated);
            Assert.IsTrue(result.Examples[0].Text == "text");
        }
        #endregion

        #region List Intents
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListIntents_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListIntents(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListIntents_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListIntents("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListIntents_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListIntents("workspaceId");
        }

        [TestMethod]
        public void ListIntents_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            IntentCollectionResponse response = new IntentCollectionResponse()
            {
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Intent = "intent",
                        Description = "description",
                        Updated = DateTime.Now,
                        Created = DateTime.Now,
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Text = "example",
                                Created = DateTime.Now,
                                Updated = DateTime.Now
                            }
                        }
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<IntentCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListIntents("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Intents);
            Assert.IsTrue(result.Intents.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Intents[0].Intent));
            Assert.IsTrue(result.Intents[0].Intent == "intent");
            Assert.IsNotNull(result.Intents[0].Created);
            Assert.IsNotNull(result.Intents[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Intents[0].Description));
            Assert.IsTrue(result.Intents[0].Description == "description");
            Assert.IsNotNull(result.Intents[0].Examples);
            Assert.IsTrue(result.Intents[0].Examples.Count > 0);
            Assert.IsTrue(result.Intents[0].Examples[0].Text == "example");
            Assert.IsNotNull(result.Intents[0].Examples[0].Created);
            Assert.IsNotNull(result.Intents[0].Examples[0].Updated);
        }
        #endregion

        #region Update Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateIntent intent = new UpdateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };

            service.UpdateIntent(null, "text", intent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_Text()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateIntent intent = new UpdateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };

            service.UpdateIntent("workspaceId", null, intent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateIntent("workspaceId", "text", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateIntent intent = new UpdateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };

            service.UpdateIntent("workspaceId", "text", intent);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateIntent_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateIntent intent = new UpdateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateIntent("workspaceId", "text", intent);
        }

        [TestMethod]
        public void UpdateIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            IntentResponse response = new IntentResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Intent = "intent",
                Description = "description"
            };
            #endregion

            UpdateIntent intent = new UpdateIntent()
            {
                Intent = "intent",
                Description = "description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateIntent>(intent)
                .Returns(request);
            request.As<IntentResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateIntent("workspaceId", "text", intent);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Intent == "intent");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion
        #endregion

        #region List Logs
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListLogs_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListLogs(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListLogs_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListLogs("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListLogs_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListLogs("workspaceId");
        }

        [TestMethod]
        public void ListLogs_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            LogCollectionResponse response = new LogCollectionResponse()
            {
                Logs = new List<LogExportResponse>()
                {
                    new LogExportResponse()
                    {
                        Request = new MessageRequest()
                        {
                            Input = new InputData()
                            {
                                Text = "inputData"
                            },
                            AlternateIntents = true,
                            Context = new Context()
                            {
                                ConversationId = "conversationId",
                                System = new SystemResponse()
                                {
                                    SystemResponseObject = new object() { }
                                }
                            },
                            Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                            Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                            Output = new OutputData()
                            {
                                LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = LogMessageResponse.LevelEnum.INFO,
                                        Msg = "msg"
                                    }
                                },
                                Text = new List<string>()
                                {
                                    "text"
                                },
                                NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                            }
                        },
                        Response = new MessageResponse()
                        {
                            Input = new MessageInput()
                            {
                                Text = "text"
                            },
                            Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                            Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                            AlternateIntents = true,
                            Context = new RuntimeContext()
                            {
                                ConversationId = "conversationId",
                                System = new RuntimeSystemContext()
                                {
                                    RuntimeSystemContextObject = new object() { }
                                }
                            },
                            Output = new RuntimeOutput()
                            {
                                LogMessages = new List<RuntimeLogMessage>()
                                {
                                    new RuntimeLogMessage()
                                    {
                                        Level = "info",
                                        Msg = "msg"
                                    }
                                },
                                Text = new List<string>()
                                {
                                    "text"
                                },
                                NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                            }
                        },
                        LogId = "logId",
                        RequestTimestamp = "requestTimestamp",
                        ResponseTimestamp = "responseTimestamp"
                    }
                },
                Pagination = new LogPaginationResponse()
                {
                    NextUrl = "nextUrl",
                    Matched = 1
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<LogCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListLogs("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Logs);
            Assert.IsTrue(result.Logs.Count > 0);
            Assert.IsNotNull(result.Pagination);
            Assert.IsTrue(result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Pagination.Matched == 1);
            Assert.IsTrue(result.Logs[0].LogId == "logId");
            Assert.IsTrue(result.Logs[0].RequestTimestamp == "requestTimestamp");
            Assert.IsTrue(result.Logs[0].ResponseTimestamp == "responseTimestamp");
        }
        #endregion

        #region Message
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");

            #region MessageRequest
            MessageRequest message = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "inputData"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "conversationId",
                    System = new SystemResponse()
                    {
                        SystemResponseObject = new object() { }
                    }
                },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = LogMessageResponse.LevelEnum.INFO,
                                        Msg = "msg"
                                    }
                                },
                    Text = new List<string>()
                                {
                                    "text"
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };
            #endregion

            service.Message(null, message);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            #region MessageRequest
            MessageRequest message = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "inputData"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "conversationId",
                    System = new SystemResponse()
                    {
                        SystemResponseObject = new object() { }
                    }
                },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = LogMessageResponse.LevelEnum.INFO,
                                        Msg = "msg"
                                    }
                                },
                    Text = new List<string>()
                                {
                                    "text"
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };
            #endregion

            service.Message("workspaceId", message);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Message_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            #region MessageRequest
            MessageRequest message = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "inputData"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "conversationId",
                    System = new SystemResponse()
                    {
                        SystemResponseObject = new object() { }
                    }
                },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = LogMessageResponse.LevelEnum.INFO,
                                        Msg = "msg"
                                    }
                                },
                    Text = new List<string>()
                                {
                                    "text"
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };
            #endregion
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.Message("workspaceId", message);
        }

        [TestMethod]
        public void Message_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            MessageResponse response = new MessageResponse()
            {
                Input = new MessageInput()
                {
                    Text = "text"
                },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                AlternateIntents = true,
                Context = new RuntimeContext()
                {
                    ConversationId = "conversationId",
                    System = new RuntimeSystemContext()
                    {
                        RuntimeSystemContextObject = new object() { }
                    }
                },
                Output = new RuntimeOutput()
                {
                    LogMessages = new List<RuntimeLogMessage>()
                                {
                                    new RuntimeLogMessage()
                                    {
                                        Level = "info",
                                        Msg = "msg"
                                    }
                                },
                    Text = new List<string>()
                                {
                                    "text"
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };
            #endregion

            #region MessageRequest
            MessageRequest message = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "inputData"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "conversationId",
                    System = new SystemResponse()
                    {
                        SystemResponseObject = new object() { }
                    }
                },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = LogMessageResponse.LevelEnum.INFO,
                                        Msg = "msg"
                                    }
                                },
                    Text = new List<string>()
                                {
                                    "text"
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<MessageRequest>(message)
                .Returns(request);
            request.As<MessageResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.Message("workspaceId", message);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Input);
            Assert.IsNotNull(result.Intents);
            Assert.IsNotNull(result.Entities);
            Assert.IsNotNull(result.AlternateIntents);
            Assert.IsNotNull(result.Context);
            Assert.IsNotNull(result.Output);
        }
        #endregion

        #region Synonyms
        #region Create Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };

            service.CreateSynonym(null, "entity", "value", synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };

            service.CreateSynonym("workspaceId", null, "value", synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };

            service.CreateSynonym("workspaceId", "entity", null, synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.CreateSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };

            service.CreateSynonym("workspaceId", "entity", "value", synonym);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateSynonym_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateSynonym("workspaceId", "entity", "value", synonym);
        }

        [TestMethod]
        public void CreateSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            SynonymResponse response = new SynonymResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Synonym = "synonym"
            };
            #endregion

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = "synonym"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateSynonym>(synonym)
                .Returns(request);
            request.As<SynonymResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateSynonym("workspaceId", "entity", "value", synonym);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Synonym);
            Assert.IsTrue(result.Synonym == "synonym");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion

        #region Delete Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Synonym()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteSynonym_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod]
        public void DeleteSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Synonym()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetSynonym_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod]
        public void GetSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            SynonymResponse response = new SynonymResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Synonym = "synonym"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<SynonymResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Synonym);
            Assert.IsTrue(result.Synonym == "synonym");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion

        #region List Synonyms
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListSynonyms(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListSynonyms("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListSynonyms("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListSynonyms("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListSynonyms_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListSynonyms("workspaceId", "entity", "value");
        }

        [TestMethod]
        public void ListSynonyms_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            SynonymCollectionResponse response = new SynonymCollectionResponse()
            {
                Synonyms = new List<SynonymResponse>()
                {
                    new SynonymResponse()
                    {
                        Synonym = "synonym",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                },
                Pagination = new PaginationResponse()
                {
                    RefreshUrl = "refreshUrl",
                    NextUrl = "nextUrl",
                    Total = 1,
                    Matched = 1
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<SynonymCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListSynonyms("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Synonyms);
            Assert.IsTrue(result.Synonyms.Count > 0);
            Assert.IsNotNull(result.Synonyms[0].Created);
            Assert.IsNotNull(result.Synonyms[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Synonyms[0].Synonym));
            Assert.IsTrue(result.Synonyms[0].Synonym == "synonym");
            Assert.IsTrue(result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Pagination.Total == 1);
            Assert.IsTrue(result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            service.UpdateSynonym(null, "entity", "value", "synonym", synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            service.UpdateSynonym("workspaceId", null, "value", "synonym", synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            service.UpdateSynonym("workspaceId", "entity", null, "synonym", synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Synonym()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            service.UpdateSynonym("workspaceId", "entity", "value", null, synonym);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Body()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateSynonym("workspaceId", "entity", "value", "synonym", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            service.UpdateSynonym("workspaceId", "entity", "value", "synonym", synonym);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateSynonym_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateSynonym("workspaceId", "entity", "value", "synonym", synonym);
        }

        [TestMethod]
        public void UpdateSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            SynonymResponse response = new SynonymResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Synonym = "synonym"
            };
            #endregion

            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = "synonym"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateSynonym>(synonym)
                .Returns(request);
            request.As<SynonymResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateSynonym("workspaceId", "entity", "value", "synonym", synonym);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsTrue(result.Synonym == "synonym");
        }
        #endregion
        #endregion

        #region Values
        #region Create Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateValue value = new CreateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.CreateValue(null, "entity", value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            CreateValue value = new CreateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.CreateValue("workspaceId", null, value);
        }
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_Bpdu()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.CreateValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateValue value = new CreateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.CreateValue("workspaceId", "entity", value);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateValue_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateValue value = new CreateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateValue("workspaceId", "entity", value);
        }

        [TestMethod]
        public void CreateValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ValueResponse response = new ValueResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Value = "value",
                Metadata = new object() { }
            };
            #endregion

            CreateValue value = new CreateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateValue>(value)
                .Returns(request);
            request.As<ValueResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateValue("workspaceId", "entity", value);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Value == "value");
            Assert.IsNotNull(result.Metadata);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion

        #region Delete Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteValue("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteValue_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteValue("workspaceId", "entity", "example");
        }

        [TestMethod]
        public void DeleteValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteValue("workspaceId", "entity", "example");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetValue("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetValue_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetValue("workspaceId","entity", "value");
        }

        [TestMethod]
        public void GetValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ValueExportResponse response = new ValueExportResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ValueExportResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetValue("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.Metadata);
            Assert.IsNotNull(result.Synonyms);
            Assert.IsTrue(result.Value == "value");
            Assert.IsTrue(result.Synonyms.Count > 0);
            Assert.IsTrue(result.Synonyms[0] == "synonym");
        }
        #endregion

        #region List Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListValues(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.ListValues("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListValues("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListValues_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListValues("workspaceId", "entity");
        }

        [TestMethod]
        public void ListValues_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ValueCollectionResponse response = new ValueCollectionResponse()
            {
                Values = new List<ValueExportResponse>()
                {
                    new ValueExportResponse()
                    {
                        Value = "value",
                        Metadata = new object() { },
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Synonyms = new List<string>()
                        {
                            "synonym"
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    RefreshUrl = "refreshUrl",
                    NextUrl = "nextUrl",
                    Total = 1,
                    Matched = 1
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ValueCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListValues("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Values);
            Assert.IsTrue(result.Values.Count > 0);
            Assert.IsTrue(result.Values[0].Value == "value");
            Assert.IsNotNull(result.Values[0].Metadata);
            Assert.IsNotNull(result.Values[0].Created);
            Assert.IsNotNull(result.Values[0].Updated);
            Assert.IsNotNull(result.Values[0].Synonyms);
            Assert.IsTrue(result.Values[0].Synonyms.Count > 0);
            Assert.IsTrue(result.Values[0].Synonyms[0] == "synonym");
            Assert.IsTrue(result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Pagination.Total == 1);
            Assert.IsTrue(result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.UpdateValue(null, "entity", "value", value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_Entity()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.UpdateValue("workspaceId", null, "value", value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_Value()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.UpdateValue("workspaceId", "entity", null, value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_UpdateValue()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateValue("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            service.UpdateValue("workspaceId", "entity", "value", value);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateValue_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateValue("workspaceId", "entity", "value", value);
        }

        [TestMethod]
        public void UpdateValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            ValueResponse response = new ValueResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Value = "value",
                Metadata = new object() { }
            };
            #endregion

            UpdateValue value = new UpdateValue()
            {
                Value = "value",
                Metadata = new object() { },
                Synonyms = new List<string>()
                {
                    "synonym"
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateValue>(value)
                .Returns(request);
            request.As<ValueResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateValue("workspaceId", "entity", "value", value);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value == "value");
            Assert.IsNotNull(result.Metadata);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
        }
        #endregion
        #endregion

        #region Workspaces
        #region Create Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkspace_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = "name",
                Description = "description",
                Language = "en",
                Metadata = new object()
            };

            service.CreateWorkspace(workspace);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateWorkspace_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = "name",
                Description = "description",
                Language = "en",
                Metadata = new object() { },
                DialogNodes = new List<CreateDialogNode>()
                {
                    new CreateDialogNode() { }
                }
            };
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.CreateWorkspace(workspace);
        }

        [TestMethod]
        public void CreateWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = "name",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "workspaceId",
                Description = "description"
            };
            #endregion

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = "name",
                Description = "description",
                Language = "en",
                Metadata = new object()
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateWorkspace>(workspace)
                .Returns(request);
            request.As<WorkspaceResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateWorkspace(workspace);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Metadata);
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Language == "en");
            Assert.IsTrue(result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region Delete Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWorkspace_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.DeleteWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWorkspace_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteWorkspace_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.DeleteWorkspace("workspaceId");
        }

        [TestMethod]
        public void DeleteWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() { };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteWorkspace("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region Get Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetWorkspace_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.GetWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetWorkspace_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetWorkspace_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.GetWorkspace("workspaceId");
        }

        [TestMethod]
        public void GetWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            WorkspaceExportResponse response = new WorkspaceExportResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = "name",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "workspaceId",
                Description = "description",
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Intent = "intent",
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Description = "description",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Text = "text"
                            }
                        }
                    }
                },
                Entities = new List<EntityExportResponse>()
                {
                    new EntityExportResponse()
                    {
                        Entity = "entity",
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Description = "description",
                        Metadata = new object() { },
                        FuzzyMatch = true,
                        Values = new List<ValueExportResponse>()
                        {
                            new ValueExportResponse()
                            {
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                Value = "value",
                                Metadata = new object() { },
                                Synonyms = new List<string>()
                                {
                                    "synonym"
                                }
                            }
                        }
                    }
                },
                Counterexamples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Text = "text"
                    }
                },
                DialogNodes = new List<DialogNodeResponse>()
                {
                    new DialogNodeResponse() { }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.As<WorkspaceExportResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.GetWorkspace("workspaceId");

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Metadata);
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Language == "en");
            Assert.IsTrue(result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region List Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListWorkspaces_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListWorkspaces();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListWorkspaces_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;
            service.ListWorkspaces();
        }

        [TestMethod]
        public void ListWorkspaces_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            WorkspaceCollectionResponse response = new WorkspaceCollectionResponse()
            {
                Workspaces = new List<WorkspaceResponse>()
                {
                    new WorkspaceResponse()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Name = "name",
                        Language = "en",
                        Metadata = new object() { },
                        WorkspaceId = "workspaceId",
                        Description = "description"
                    }
                },
                Pagination = new PaginationResponse()
                {
                    RefreshUrl = "refreshUrl",
                    NextUrl = "nextUrl",
                    Total = 1,
                    Matched = 1
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<WorkspaceCollectionResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.ListWorkspaces();

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsTrue(result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Pagination.Total == 1);
            Assert.IsTrue(result.Pagination.Matched == 1);
            Assert.IsNotNull(result.Workspaces[0].Created);
            Assert.IsNotNull(result.Workspaces[0].Created);
            Assert.IsNotNull(result.Workspaces[0].Metadata);
            Assert.IsTrue(result.Workspaces[0].Name == "name");
            Assert.IsTrue(result.Workspaces[0].Description == "description");
            Assert.IsTrue(result.Workspaces[0].Language == "en");
            Assert.IsTrue(result.Workspaces[0].WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Workspaces[0].Description == "description");
        }
        #endregion

        #region Update Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_No_WorkspaceId()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.UpdateWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_No_VersionDate()
        {
            ConversationService service = new ConversationService("username", "password", "versionDate");
            service.VersionDate = null;

            service.UpdateWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateWorkspace_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });
            
            ConversationService service = new ConversationService(client);
            service.VersionDate = ConversationService.CONVERSATION_VERSION_DATE_2017_05_26;

            service.UpdateWorkspace("workspaceId");
        }

        [TestMethod]
        public void UpdateWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = "name",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "workspaceId",
                Description = "description"
            };
            #endregion

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = "name",
                Language = "en",
                Metadata = new object() { },
                Description = "description",
                DialogNodes = new List<CreateDialogNode>()
                {
                    new CreateDialogNode() { }
                },
                Intents = new List<CreateIntent>()
                {
                    new CreateIntent()
                    {
                        Intent = "intent",
                        Description = "description",
                        Examples = new List<CreateExample>()
                        {
                            new CreateExample()
                            {
                                Text = "text"
                            }
                        }
                    }
                },
                Entities = new List<CreateEntity>()
                {
                    new CreateEntity()
                    {
                        Entity = "entity",
                        Description = "description",
                        Metadata = new object() { },
                        FuzzyMatch = true,
                        Values = new List<CreateValue>()
                        {
                            new CreateValue()
                            {
                                Value = "value",
                                Metadata = new object() { },
                                Synonyms = new List<string>()
                                {
                                    "synonym"
                                }
                            }
                        }
                    }
                },
                Counterexamples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "text"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateWorkspace>(workspace)
                .Returns(request);
            request.As<WorkspaceResponse>()
                .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateWorkspace("workspaceId", workspace);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Metadata);
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Language == "en");
            Assert.IsTrue(result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion
        #endregion
    }
}

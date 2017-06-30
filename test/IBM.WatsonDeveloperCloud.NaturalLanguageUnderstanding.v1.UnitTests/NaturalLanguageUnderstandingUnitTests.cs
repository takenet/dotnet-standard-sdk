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
using IBM.WatsonDeveloperCloud.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using System.Net;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.UnitTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(null, "password", NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService("username", null, NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService("username", "password", NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService();

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

        #region Analyze
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_No_VersionDate()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("username", "password", "versionDate");
            service.VersionDate = null;

            service.Analyze();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Analyze_Catch_Exception()
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

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27;

            service.Analyze();
        }

        [TestMethod]
        public void Analyze_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                .Returns(request);

            #region Response
            AnalysisResults response = new AnalysisResults()
            {
                Language = "en",
                AnalyzedText = "testText",
                RetrievedUrl = "retrivedUrl",
                Usage = new Usage()
                {
                    Features = 1
                },
                Concepts = new List<ConceptsResult>()
                {
                    new ConceptsResult()
                    {
                        Text = "text",
                        Relevance = 1.0f,
                        DbpediaResource = "dbpediaResouce"
                    }
                },
                Entities = new List<EntitiesResult>()
                {
                    new EntitiesResult()
                    {
                        Type = "type",
                        Relevance = 1.0f,
                        Count = 1,
                        Text = "text",
                        Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                        Sentiment = new FeatureSentimentResults() { Score = 1.0f },
                        Disambiguation = new DisambiguationResult()
                        {
                            Name = "name",
                            DbpediaResource = "dbpediaResource",
                            Subtype = new List<string>()
                            {
                                "subtype"
                            }
                        }
                    }
                },
                Keywords = new List<KeywordsResult>()
                {
                    new KeywordsResult()
                    {
                        Relevance = 1.0f,
                        Text = "text",
                        Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                        Sentiment = new FeatureSentimentResults() { Score = 1.0f }
                    }
                },
                Categories = new List<CategoriesResult>()
                {
                    new CategoriesResult()
                    {
                        Label = "label",
                        Score = 1.0f
                    }
                },
                Emotion = new EmotionResult()
                {
                    Document = new DocumentEmotionResults()
                    {
                        Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                    },
                    Targets = new List<TargetedEmotionResults>()
                    {
                        new TargetedEmotionResults()
                        {
                            Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                            Text = "text"
                        }
                    }
                },
                Metadata = new MetadataResult()
                {
                    Authors = new List<Author>()
                    {
                        new Author()
                        {
                            Name = "name"
                        }
                    },
                    PublicationDate = "publicationDate",
                    Title = "title"
                },
                Relations = new List<RelationsResult>()
                {
                    new RelationsResult()
                    {
                        Score = 1.0f,
                        Sentence = "sentence",
                        Type = "type",
                        Arguments = new List<RelationArgument>()
                        {
                            new RelationArgument()
                            {
                                Entities = new List<RelationEntity>()
                                {
                                    new RelationEntity()
                                    {
                                        Text = "text",
                                        Type = "type"
                                    }
                                },
                                Text = "text"
                            }
                        }
                    }
                },
                SemanticRoles = new List<SemanticRolesResult>()
                {
                    new SemanticRolesResult()
                    {
                        Sentence = "sentence",
                        Subject = new SemanticRolesSubject()
                        {
                            Text = "text",
                            Entities = new List<SemanticRolesEntity>()
                            {
                                new SemanticRolesEntity()
                                {
                                    Text = "text",
                                    Type = "type"
                                }
                            },
                            Keywords = new List<SemanticRolesKeyword>()
                            {
                                new SemanticRolesKeyword()
                                {
                                    Text = "text"
                                }
                            }
                        },
                        Action = new SemanticRolesAction()
                        {
                            Normalized = "normalized",
                            Text = "text",
                            Verb = new SemanticRolesVerb()
                            {
                                Text = "text",
                                Tense = "tense"
                            }
                        },
                        _Object = new SemanticRolesObject()
                        {
                            Text = "text",
                            Keywords = new List<SemanticRolesKeyword>()
                            {
                                new SemanticRolesKeyword()
                                {
                                    Text = "text"
                                }
                            }
                        }
                    }
                },
                Sentiment = new SentimentResult()
                {
                    Document = new DocumentSentimentResults()
                    {
                        Score = 1.0f
                    },
                    Targets = new List<TargetedSentimentResults>()
                    {
                        new TargetedSentimentResults()
                        {
                            Score = 1.0f,
                            Text = "text"
                        }
                    }
                }
            };
            #endregion

            #region parameters
            Parameters parameters = new Parameters()
            {
                Text = "text",
                Html = "html",
                Url = "url",
                Features = new Features()
                {
                    Concepts = new ConceptsOptions()
                    {
                        Limit = 1
                    },
                    Emotion = new EmotionOptions()
                    {
                        Document = true,
                        Targets = new List<string>()
                        {
                            "target"
                        }
                    },
                    Entities = new EntitiesOptions()
                    {
                        Model = "model",
                        Sentiment = true,
                        Emotion = true,
                        Limit = 1
                    },
                    Keywords = new KeywordsOptions()
                    {
                        Limit = 1,
                        Sentiment = true,
                        Emotion = true
                    },
                    Metadata = new MetadataOptions() {},
                    Relations = new RelationsOptions()
                    {
                        Model = "model"
                    },
                    SemanticRoles = new SemanticRolesOptions()
                    {
                        Limit = 1,
                        Keywords = true,
                        Entities = true
                    },
                    Sentiment = new SentimentOptions()
                    {
                        Document = true,
                        Targets = new List<string>()
                        {
                            "target"
                        }
                    },
                    Categories = new CategoriesOptions() {}
                },
                Clean = true,
                Xpath = "xpath",
                FallbackToRaw = true,
                ReturnAnalyzedText = true,
                Language = "en"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<Parameters>(parameters)
                .Returns(request);
            request.As<AnalysisResults>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.Analyze(parameters);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.Language== "en");
            Assert.IsTrue(result.AnalyzedText == "testText");
        }
        #endregion

        #region Delete Model
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteModel_No_Model_Id()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("username", "password", "versionDate");
            service.DeleteModel(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteModel_No_VersionDate()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("username", "password", "versionDate");
            service.VersionDate = null;

            service.DeleteModel("modelId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteModel_Catch_Exception()
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

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27;

            service.DeleteModel("modelID");
        }

        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Delete(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object() {};
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteModel("modelId");

            Assert.IsNotNull(result);
            client.Received().Delete(Arg.Any<string>());
        }
        #endregion

        #region List Models
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListModels_No_VersionDate()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("username", "password", "versionDate");
            service.VersionDate = null;

            service.GetModels();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListModels_Catch_Exception()
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

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27;

            service.GetModels();
        }

        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Get(Arg.Any<string>())
                .Returns(request);

            #region Response
            ListModelsResults response = new ListModelsResults()
            {
                Models = new List<CustomModel>()
                {
                    new CustomModel()
                    {
                        Status = "status",
                        ModelId = "modelId",
                        Language = "language",
                        Description = "description"
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListModelsResults>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.GetModels();

            Assert.IsNotNull(result);
            client.Received().Get(Arg.Any<string>());
            Assert.IsTrue(result.Models[0].Status == "status");
            Assert.IsTrue(result.Models[0].ModelId == "modelId");
            Assert.IsTrue(result.Models[0].Language == "language");
            Assert.IsTrue(result.Models[0].Description == "description");
        }
        #endregion
    }
}

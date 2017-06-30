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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.UnitTests
{
    [TestClass]
    public class ToneAnalyzerServiceUnitTests
    {
        private string versionDate = "2016-05-19";

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(null, "password", versionDate);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", null, versionDate);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", "password", versionDate);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService();

            Assert.IsNotNull(service);
        }

        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                    .Returns(client);

            return client;
        }

        [TestMethod]
        public void Tone_Success_With_ToneInput()
        {
            IClient client = CreateClient();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentAnalysis()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            IRequest request = Substitute.For<IRequest>();

            client.Post(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                   .Returns(request);
            request.WithBody<ToneInput>(Arg.Any<ToneInput>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);
            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = "2016-05-19";

            ToneInput toneInput = new ToneInput()
            {
                Text = Arg.Any<string>()
            };

            var analyzeTone = service.Tone(toneInput, Arg.Any<string>(), Arg.Any<bool>());

            Assert.IsNotNull(analyzeTone);
            client.Received().Post(Arg.Any<string>());
            Assert.IsNotNull(analyzeTone.DocumentTone);
            Assert.IsNotNull(analyzeTone.DocumentTone.ToneCategories);
            Assert.IsTrue(analyzeTone.DocumentTone.ToneCategories.Count >= 1);
            Assert.IsNotNull(analyzeTone.SentencesTone);
            Assert.IsTrue(analyzeTone.SentencesTone.Count >= 1);
            Assert.IsNotNull(analyzeTone.SentencesTone[0].ToneCategories);
            Assert.IsTrue(analyzeTone.SentencesTone[0].SentenceId == 0);
            Assert.IsTrue(analyzeTone.SentencesTone[0].Text == "string");
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories.Count >= 1);
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].CategoryId == "string");
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].CategoryName == "string");
            Assert.IsNotNull(analyzeTone.SentencesTone[0].ToneCategories[0].Tones);
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].Tones.Count >= 1);
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].Tones[0].ToneName == "string");
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].Tones[0].ToneId == "string");
            Assert.IsTrue(analyzeTone.SentencesTone[0].ToneCategories[0].Tones[0].Score == 0);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Tone_Catch_Exception()
        {
            #region Mock IClient
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });
            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            ToneInput toneInput = new ToneInput()
            {
                Text = "test"
            };

            service.Tone(toneInput);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Tone_ToneInputEmpty()
        {
            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentAnalysis()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            var analyzeTone = service.Tone(null, "tones", true);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Tone_Empty_Version()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            service.VersionDate = null;

            ToneInput toneInput = new ToneInput()
            {
                Text = Arg.Any<string>()
            };

            var analyzeTone = service.Tone(toneInput, Arg.Any<string>(), Arg.Any<bool>());
        }

        [TestMethod]
        public void ToneChat_Success_With_ToneChatInput()
        {
            IClient client = CreateClient();

            #region response
            UtteranceAnalyses response = new UtteranceAnalyses()
            {
                UtterancesTone = new List<UtteranceAnalysis>()
                {
                    new UtteranceAnalysis()
                    {
                        UtteranceId = "utteranceID",
                        UtteranceText = "utteranceText",
                        Tones = new List<ToneChatScore>()
                        {
                            new ToneChatScore()
                            {
                                ToneName = "string",
                                ToneId = "string",
                                Score = 0
                            }
                        }
                    }
                }
            };
            #endregion

            ToneChatInput toneChatInput = new ToneChatInput()
            {
                Utterances = new List<Utterance>()
                {
                    new Utterance()
                    {
                        Text = "utteranceText",
                        User = "utteranceUser"
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();

            client.Post(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithBody(Arg.Any<ToneChatInput>())
                   .Returns(request);
            request.As<UtteranceAnalyses>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            var result = service.ToneChat(toneChatInput);

            Assert.IsNotNull(result);
            client.Received().Post(Arg.Any<string>());
            Assert.IsTrue(result.UtterancesTone.Count >= 1);
            Assert.IsTrue(result.UtterancesTone[0].Tones.Count >= 1);
            Assert.IsTrue(result.UtterancesTone[0].Tones[0].ToneName == "string");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToneChat_ToneChatInputEmpty()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            var result = service.ToneChat(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ToneChat_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                    .Returns(client);

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.Post(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            ToneChatInput toneChatInput = new ToneChatInput()
            {
                Utterances = new List<Utterance>()
                {
                    new Utterance()
                    {
                        Text = "utteranceText",
                        User = "utteranceUser"
                    }
                }
            };
            service.ToneChat(toneChatInput);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToneChat_Empty_Version()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            service.VersionDate = null;

            ToneChatInput toneChatInput = new ToneChatInput()
            {
                Utterances = new List<Utterance>()
                {
                    new Utterance()
                    {
                        Text = "utteranceText",
                        User = "utteranceUser"
                    }
                }
            };

            service.ToneChat(toneChatInput);
        }
    }
}

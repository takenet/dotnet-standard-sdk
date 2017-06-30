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

using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Runtime.ExceptionServices;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public interface IConversationService
    {
        /// <summary>
        /// Create counterexample. Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> CreateCounterexampleAsync(string workspaceId, CreateExample body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete counterexample. Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param> 
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteCounterexampleAsync(string workspaceId, string text, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get counterexample. Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> GetCounterexampleAsync(string workspaceId, string text, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List counterexamples. List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="CounterexampleCollectionResponse" />CounterexampleCollectionResponse</returns>
        Task<CounterexampleCollectionResponse> ListCounterexamplesAsync(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update counterexample. Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">An UpdateExample object defining the new text for the counterexample.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> UpdateCounterexampleAsync(string workspaceId, string text, UpdateExample body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create entity. Create a new entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateEntity object defining the content of the new entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        Task<EntityResponse> CreateEntityAsync(string workspaceId, CreateEntity body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity. Delete an entity from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteEntityAsync(string workspaceId, string entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entity. Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned
        /// data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param> 
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="EntityExportResponse" />EntityExportResponse</returns>
        Task<EntityExportResponse> GetEntityAsync(string workspaceId, string entity, bool? export = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List entities. List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="EntityCollectionResponse" />EntityCollectionResponse</returns>
        Task<EntityCollectionResponse> ListEntitiesAsync(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update entity. Update an existing entity with new or modified data.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">An UpdateEntity object defining the updated content of the entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        Task<EntityResponse> UpdateEntityAsync(string workspaceId, string entity, UpdateEntity body, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Create user input example. Add a new user input example to an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> CreateExampleAsync(string workspaceId, string intent, CreateExample body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete user input example. Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteExampleAsync(string workspaceId, string intent, string text, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get user input example. Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> GetExampleAsync(string workspaceId, string intent, string text, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List user input examples. List the user input examples for an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ExampleCollectionResponse" />ExampleCollectionResponse</returns>
        Task<ExampleCollectionResponse> ListExamplesAsync(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update user input example. Update the text of a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">An UpdateExample object defining the new text for the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        Task<ExampleResponse> UpdateExampleAsync(string workspaceId, string intent, string text, UpdateExample body, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Create intent. Create a new intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateIntent object defining the content of the new intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        Task<IntentResponse> CreateIntentAsync(string workspaceId, CreateIntent body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete intent. Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteIntentAsync(string workspaceId, string intent, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get intent. Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="IntentExportResponse" />IntentExportResponse</returns>
        Task<IntentExportResponse> GetIntentAsync(string workspaceId, string intent, bool? export = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List intents. List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="IntentCollectionResponse" />IntentCollectionResponse</returns>
        Task<IntentCollectionResponse> ListIntentsAsync(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update intent. Update an existing intent with new or modified data. You must provide data defining the content of the updated intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">An UpdateIntent object defining the updated content of the intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        Task<IntentResponse> UpdateIntentAsync(string workspaceId, string intent, UpdateIntent body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List log events. 
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="LogCollectionResponse" />LogCollectionResponse</returns>
        Task<LogCollectionResponse> ListLogsAsync(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a response to a user's input. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The user's input, with optional intents, entities, and other properties from the response. (optional)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        Task<MessageResponse> MessageAsync(string workspaceId, MessageRequest body = null, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Add entity value synonym. Add a new synonym to an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">A CreateSynonym object defining the new synonym for the entity value.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        Task<SynonymResponse> CreateSynonymAsync(string workspaceId, string entity, string value, CreateSynonym body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity value synonym. Delete a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteSynonymAsync(string workspaceId, string entity, string value, string synonym, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entity value synonym. Get information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        Task<SynonymResponse> GetSynonymAsync(string workspaceId, string entity, string value, string synonym, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List entity value synonyms. List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="SynonymCollectionResponse" />SynonymCollectionResponse</returns>
        Task<SynonymCollectionResponse> ListSynonymsAsync(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update entity value synonym. Update the information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">An UpdateSynonym object defining the new information for an entity value synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        Task<SynonymResponse> UpdateSynonymAsync(string workspaceId, string entity, string value, string synonym, UpdateSynonym body, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Add entity value. Create a new value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">A CreateValue object defining the content of the new value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        Task<ValueResponse> CreateValueAsync(string workspaceId, string entity, CreateValue body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete entity value. Delete a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteValueAsync(string workspaceId, string entity, string value, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entity value. Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="ValueExportResponse" />ValueExportResponse</returns>
        Task<ValueExportResponse> GetValueAsync(string workspaceId, string entity, string value, bool? export = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List entity values. List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ValueCollectionResponse" />ValueCollectionResponse</returns>
        Task<ValueCollectionResponse> ListValuesAsync(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update entity value. Update the content of a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">An UpdateValue object defining the new content for value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        Task<ValueResponse> UpdateValueAsync(string workspaceId, string entity, string value, UpdateValue body, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Create workspace. Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.
        /// </summary>
        /// <param name="body">Valid data defining the content of the new workspace. (optional)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        Task<WorkspaceResponse> CreateWorkspaceAsync(CreateWorkspace body = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete workspace. Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <returns><see cref="object" />object</returns>
        Task<object> DeleteWorkspaceAsync(string workspaceId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get information about a workspace. Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="WorkspaceExportResponse" />WorkspaceExportResponse</returns>
        Task<WorkspaceExportResponse> GetWorkspaceAsync(string workspaceId, bool? export = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List workspaces. List the workspaces associated with a Conversation service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="WorkspaceCollectionResponse" />WorkspaceCollectionResponse</returns>
        Task<WorkspaceCollectionResponse> ListWorkspacesAsync(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update workspace. Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. (optional)</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        Task<WorkspaceResponse> UpdateWorkspaceAsync(string workspaceId, UpdateWorkspace body = null, CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// Extensions for <see cref="IConversationService"/> interface to keep the compability with older implementations.
    /// </summary>
    public static class ConversationServiceExtensions
    {
        /// <summary>
        /// Create counterexample. Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse CreateCounterexample(this IConversationService conversationService, string workspaceId, CreateExample body)
        {
            try
            {
                return conversationService.CreateCounterexampleAsync(workspaceId, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete counterexample. Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteCounterexample(this IConversationService conversationService, string workspaceId, string text)
        {
            try
            {
                return conversationService.DeleteCounterexampleAsync(workspaceId, text, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get counterexample. Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse GetCounterexample(this IConversationService conversationService, string workspaceId, string text)
        {
            try
            {
                return conversationService.GetCounterexampleAsync(workspaceId, text, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List counterexamples. List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="CounterexampleCollectionResponse" />CounterexampleCollectionResponse</returns>
        public static CounterexampleCollectionResponse ListCounterexamples(this IConversationService conversationService, string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListCounterexamplesAsync(workspaceId, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update counterexample. Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">An UpdateExample object defining the new text for the counterexample.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse UpdateCounterexample(this IConversationService conversationService, string workspaceId, string text, UpdateExample body)
        {
            try
            {
                return conversationService.UpdateCounterexampleAsync(workspaceId, text, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Create entity. Create a new entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateEntity object defining the content of the new entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        public static EntityResponse CreateEntity(this IConversationService conversationService, string workspaceId, CreateEntity body)
        {
            try
            {
                return conversationService.CreateEntityAsync(workspaceId, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete entity. Delete an entity from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteEntity(this IConversationService conversationService, string workspaceId, string entity)
        {
            try
            {
                return conversationService.DeleteEntityAsync(workspaceId, entity, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get entity. Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="EntityExportResponse" />EntityExportResponse</returns>
        public static EntityExportResponse GetEntity(this IConversationService conversationService, string workspaceId, string entity, bool? export = null)
        {
            try
            {
                return conversationService.GetEntityAsync(workspaceId, entity, export, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List entities. List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="EntityCollectionResponse" />EntityCollectionResponse</returns>
        public static EntityCollectionResponse ListEntities(this IConversationService conversationService, string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListEntitiesAsync(workspaceId, export, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update entity. Update an existing entity with new or modified data.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">An UpdateEntity object defining the updated content of the entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        public static EntityResponse UpdateEntity(this IConversationService conversationService, string workspaceId, string entity, UpdateEntity body)
        {
            try
            {
                return conversationService.UpdateEntityAsync(workspaceId, entity, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
        /// <summary>
        /// Create user input example. Add a new user input example to an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse CreateExample(this IConversationService conversationService, string workspaceId, string intent, CreateExample body)
        {
            try
            {
                return conversationService.CreateExampleAsync(workspaceId, intent, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete user input example. Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteExample(this IConversationService conversationService, string workspaceId, string intent, string text)
        {
            try
            {
                return conversationService.DeleteExampleAsync(workspaceId, intent, text, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get user input example. Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse GetExample(this IConversationService conversationService, string workspaceId, string intent, string text)
        {
            try
            {
                return conversationService.GetExampleAsync(workspaceId, intent, text, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List user input examples. List the user input examples for an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ExampleCollectionResponse" />ExampleCollectionResponse</returns>
        public static ExampleCollectionResponse ListExamples(this IConversationService conversationService, string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListExamplesAsync(workspaceId, intent, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update user input example. Update the text of a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">An UpdateExample object defining the new text for the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        public static ExampleResponse UpdateExample(this IConversationService conversationService, string workspaceId, string intent, string text, UpdateExample body)
        {
            try
            {
                return conversationService.UpdateExampleAsync(workspaceId, intent, text, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
        /// <summary>
        /// Create intent. Create a new intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateIntent object defining the content of the new intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        public static IntentResponse CreateIntent(this IConversationService conversationService, string workspaceId, CreateIntent body)
        {
            try
            {
                return conversationService.CreateIntentAsync(workspaceId, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete intent. Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteIntent(this IConversationService conversationService, string workspaceId, string intent)
        {
            try
            {
                return conversationService.DeleteIntentAsync(workspaceId, intent, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get intent. Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="IntentExportResponse" />IntentExportResponse</returns>
        public static IntentExportResponse GetIntent(this IConversationService conversationService, string workspaceId, string intent, bool? export = null)
        {
            try
            {
                return conversationService.GetIntentAsync(workspaceId, intent, export, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List intents. List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="IntentCollectionResponse" />IntentCollectionResponse</returns>
        public static IntentCollectionResponse ListIntents(this IConversationService conversationService, string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListIntentsAsync(workspaceId, export, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update intent. Update an existing intent with new or modified data. You must provide data defining the content of the updated intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">An UpdateIntent object defining the updated content of the intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        public static IntentResponse UpdateIntent(this IConversationService conversationService, string workspaceId, string intent, UpdateIntent body)
        {
            try
            {
                return conversationService.UpdateIntentAsync(workspaceId, intent, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
        /// <summary>
        /// List log events. 
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="LogCollectionResponse" />LogCollectionResponse</returns>
        public static LogCollectionResponse ListLogs(this IConversationService conversationService, string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            try
            {
                return conversationService.ListLogsAsync(workspaceId, sort, filter, pageLimit, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
        /// <summary>
        /// Get a response to a user's input. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The user's input, with optional intents, entities, and other properties from the response. (optional)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        public static MessageResponse Message(this IConversationService conversationService, string workspaceId, MessageRequest body = null)
        {
            try
            {
                return conversationService.MessageAsync(workspaceId, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
        /// <summary>
        /// Add entity value synonym. Add a new synonym to an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">A CreateSynonym object defining the new synonym for the entity value.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        public static SynonymResponse CreateSynonym(this IConversationService conversationService, string workspaceId, string entity, string value, CreateSynonym body)
        {
            try
            {
                return conversationService.CreateSynonymAsync(workspaceId, entity, value, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete entity value synonym. Delete a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteSynonym(this IConversationService conversationService, string workspaceId, string entity, string value, string synonym)
        {
            try
            {
                return conversationService.DeleteSynonymAsync(workspaceId, entity, value, synonym, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get entity value synonym. Get information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        public static SynonymResponse GetSynonym(this IConversationService conversationService, string workspaceId, string entity, string value, string synonym)
        {
            try
            {
                return conversationService.GetSynonymAsync(workspaceId, entity, value, synonym, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List entity value synonyms. List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="SynonymCollectionResponse" />SynonymCollectionResponse</returns>
        public static SynonymCollectionResponse ListSynonyms(this IConversationService conversationService, string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListSynonymsAsync(workspaceId, entity, value, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update entity value synonym. Update the information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">An UpdateSynonym object defining the new information for an entity value synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        public static SynonymResponse UpdateSynonym(this IConversationService conversationService, string workspaceId, string entity, string value, string synonym, UpdateSynonym body)
        {
            try
            {
                return conversationService.UpdateSynonymAsync(workspaceId, entity, value, synonym, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Add entity value. Create a new value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">A CreateValue object defining the content of the new value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        public static ValueResponse CreateValue(this IConversationService conversationService, string workspaceId, string entity, CreateValue body)
        {
            try
            {
                return conversationService.CreateValueAsync(workspaceId, entity, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete entity value. Delete a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteValue(this IConversationService conversationService, string workspaceId, string entity, string value)
        {
            try
            {
                return conversationService.DeleteValueAsync(workspaceId, entity, value, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get entity value. Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="ValueExportResponse" />ValueExportResponse</returns>
        public static ValueExportResponse GetValue(this IConversationService conversationService, string workspaceId, string entity, string value, bool? export = null)
        {
            try
            {
                return conversationService.GetValueAsync(workspaceId, entity, value, export, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List entity values. List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ValueCollectionResponse" />ValueCollectionResponse</returns>
        public static ValueCollectionResponse ListValues(this IConversationService conversationService, string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListValuesAsync(workspaceId, entity, export, pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update entity value. Update the content of a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">An UpdateValue object defining the new content for value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        public static ValueResponse UpdateValue(this IConversationService conversationService, string workspaceId, string entity, string value, UpdateValue body)
        {
            try
            {
                return conversationService.UpdateValueAsync(workspaceId, entity, value, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Create workspace. Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.
        /// </summary>
        /// <param name="body">Valid data defining the content of the new workspace. (optional)</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        public static WorkspaceResponse CreateWorkspace(this IConversationService conversationService, CreateWorkspace body = null)
        {
            try
            {
                return conversationService.CreateWorkspaceAsync(body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Delete workspace. Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <returns><see cref="object" />object</returns>
        public static object DeleteWorkspace(this IConversationService conversationService, string workspaceId)
        {
            try
            {
                return conversationService.DeleteWorkspaceAsync(workspaceId, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Get information about a workspace. Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceExportResponse" />WorkspaceExportResponse</returns>
        public static WorkspaceExportResponse GetWorkspace(this IConversationService conversationService, string workspaceId, bool? export = null)
        {
            try
            {
                return conversationService.GetWorkspaceAsync(workspaceId, export, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// List workspaces. List the workspaces associated with a Conversation service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="WorkspaceCollectionResponse" />WorkspaceCollectionResponse</returns>
        public static WorkspaceCollectionResponse ListWorkspaces(this IConversationService conversationService, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null)
        {
            try
            {
                return conversationService.ListWorkspacesAsync(pageLimit, includeCount, sort, cursor, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        /// <summary>
        /// Update workspace. Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. (optional)</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        public static WorkspaceResponse UpdateWorkspace(this IConversationService conversationService, string workspaceId, UpdateWorkspace body = null)
        {
            try
            {
                return conversationService.UpdateWorkspaceAsync(workspaceId, body, CancellationToken.None).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }
    }
}

// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.TournamentsApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using IO.Swagger.Client;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class TournamentsApi : ITournamentsApi
  {
    public TournamentsApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public TournamentsApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RTournamentsApi<long?> GetCashPrize()
    {
      string path = "/Tournaments/GetCashPrize".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetCashPrize: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetCashPrize: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RTournamentsApi<long?>(restResponse, data);
    }

    public RTournamentsApi<TournamentRound> MatchList(long? tournamentId)
    {
      if (!tournamentId.HasValue)
        throw new ApiException(400, "Missing required parameter 'tournamentId' when calling MatchList");
      string path = "/Tournaments/MatchList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (tournamentId.HasValue)
        formParams.Add("tournament_id", this.ApiClient.ParameterToString((object) tournamentId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MatchList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MatchList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      TournamentRound data = (TournamentRound) this.ApiClient.Deserialize(restResponse.Content, typeof (TournamentRound), restResponse.Headers);
      return new RTournamentsApi<TournamentRound>(restResponse, data);
    }

    public RTournamentsApi<TournamentMatchResult> MatchResult(
      long? tournamentId,
      long? matchId,
      long? winnerTeam,
      long? isForfait,
      long? endTurn)
    {
      if (!tournamentId.HasValue)
        throw new ApiException(400, "Missing required parameter 'tournamentId' when calling MatchResult");
      if (!matchId.HasValue)
        throw new ApiException(400, "Missing required parameter 'matchId' when calling MatchResult");
      if (!winnerTeam.HasValue)
        throw new ApiException(400, "Missing required parameter 'winnerTeam' when calling MatchResult");
      if (!isForfait.HasValue)
        throw new ApiException(400, "Missing required parameter 'isForfait' when calling MatchResult");
      if (!endTurn.HasValue)
        throw new ApiException(400, "Missing required parameter 'endTurn' when calling MatchResult");
      string path = "/Tournaments/MatchResult".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (tournamentId.HasValue)
        formParams.Add("tournament_id", this.ApiClient.ParameterToString((object) tournamentId));
      if (matchId.HasValue)
        formParams.Add("match_id", this.ApiClient.ParameterToString((object) matchId));
      if (winnerTeam.HasValue)
        formParams.Add("winner_team", this.ApiClient.ParameterToString((object) winnerTeam));
      if (isForfait.HasValue)
        formParams.Add("is_forfait", this.ApiClient.ParameterToString((object) isForfait));
      if (endTurn.HasValue)
        formParams.Add("end_turn", this.ApiClient.ParameterToString((object) endTurn));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MatchResult: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MatchResult: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      TournamentMatchResult data = (TournamentMatchResult) this.ApiClient.Deserialize(restResponse.Content, typeof (TournamentMatchResult), restResponse.Headers);
      return new RTournamentsApi<TournamentMatchResult>(restResponse, data);
    }
  }
}

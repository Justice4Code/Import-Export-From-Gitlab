using Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Export_Import_Issues_attachments_From_Gitlab
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Welcome Message
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Greetings! Please put the variables correctly and we get your issues up and ready in your own gitlab :)");

                //GETTING PROJECT ID FROM GITLAB 
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter the project id from Gitlab.com");
                Console.ResetColor();
                var projectIdGitlab = Console.ReadLine();

                //GETTING PRIVATE TOKEN FROM GITLAB 
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter the PRIVATE-TOKEN from Gitlab.com");
                Console.ResetColor();
                var privateTokenGitlab = Console.ReadLine();

                //GETTING TOTAL ISSUE COUNT FROM GITLAB 
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter the total issues count from Gitlab.com");
                Console.ResetColor();
                var totalIssueCount = Convert.ToInt32(Console.ReadLine());

                //GETTING PROJECT ID FROM YOUR OWN GITLAB
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter the project id from your own gitlab");
                Console.ResetColor();
                var projectIdOwn = Console.ReadLine();

                //getting PRIVATE TOKEN FROM YOUR OWN GITLAB
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please enter the PRIVATE-TOKEN your own gitlab");
                Console.ResetColor();
                var privateTokenOwn = Console.ReadLine();


                //Creating the request. 
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Now sit back and relax this may take some time to complete!");
                Console.ResetColor();

                //Getting the issues. 
                var ret = GetExportListOfIssues(totalIssueCount, projectIdGitlab, privateTokenGitlab);

                if (ret != null)
                {
                    var issueList = new List<ExportIssueResponse>();
                    //Get Issue notes
                    foreach (var item in ret)
                    {
                        //request notes
                        var returned = GetExportListOfNotes(item, projectIdGitlab, privateTokenGitlab);
                        //Initialize list of export response
                        item.Notes = new List<ExportNoteResponse>();

                        if (returned != null)
                            item.Notes.AddRange(returned);

                        issueList.Add(item);
                    }

                    if(issueList != null && issueList.Count > 0)
                    {
                        //getting PRIVATE TOKEN FROM YOUR OWN GITLAB
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Success! I've fetched everything from gitlab and I am ready to put it on your own gitlab!");
                        Console.WriteLine("But before to do so! do you wish to add these issues to a milestone? add the id of your milestone here! if not type NO and we move forward");
                        Console.ResetColor();
                        var mileStoneId = Console.ReadLine();
                        if (mileStoneId.ToLower() == "no")
                            mileStoneId = string.Empty; 

                        //Create Issue and put their note 
                        foreach (var item in issueList)
                        {
                            var responseIssue = CreateNewIssue(item,projectIdOwn,privateTokenOwn,mileStoneId);

                            if (responseIssue != null)
                            {
                                foreach (var child in item.Notes)
                                {
                                    var newId = responseIssue.iid?.ToString();
                                    CreateNewNotes(child,projectIdOwn, privateTokenOwn, newId);
                                }
                            }

                        }
                    }
                }
                Console.ResetColor();
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        private static List<ExportIssueResponse> GetExportListOfIssues(int totalIssueCount, string projectId, string token)
        {
            try
            {
                var processResponse = new List<ExportIssueResponse>();
                var pageCount = Convert.ToInt32(Math.Round(Convert.ToDecimal(totalIssueCount / 100), MidpointRounding.AwayFromZero));
                for (int i = 0; i < pageCount; i++)
                {
                    var newObject = new Entities.ExportIssueRequest(projectId, token);
                    newObject.PrivateToken = token;
                    var client = new RestClient(newObject.BaseUrl);
                    // setting the url of the request
                    var request = new RestRequest(newObject.BaseUrl + newObject.FragmentUrl, Method.GET);
                    // adding the private key
                    request.AddHeader("PRIVATE-TOKEN", newObject.PrivateToken);
                    request.AddParameter("id", newObject.ProjectId);
                    request.AddParameter("page", i);


                    // execute the request
                    var response = client.Get(request);
                    // raw content as string
                    var content = response.Content;
                    // deserialize
                    processResponse.AddRange(JsonConvert.DeserializeObject<List<ExportIssueResponse>>(content));
                }
                return processResponse;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();

                return null;
            }

        }
        private static List<ExportNoteResponse> GetExportListOfNotes(ExportIssueResponse item, string projectId, string token)
        {
            try
            {
                var noteObj = new ExportNoteRequest(projectId, token, item.iid?.ToString());
                var client = new RestClient(noteObj.BaseUrl);
                var noteRequest = new RestRequest(noteObj.BaseUrl + noteObj.FragmentUrl, Method.GET);

                noteRequest.AddHeader("PRIVATE-TOKEN", noteObj.PrivateToken);
                //noteRequest.AddParameter("id", noteObj.ProjectId);
                //noteRequest.AddParameter("issue_iid", noteObj.IssueId);


                // execute the request
                var res = client.Get(noteRequest);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // raw content as string
                    var contentNote = res.Content;
                    // deserialize
                    return JsonConvert.DeserializeObject<List<ExportNoteResponse>>(contentNote);

                }
                return null;
            }
            catch (Exception ex )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();

                return null;
            }

        }
        private static ImportCreateIssueResponse CreateNewIssue(ExportIssueResponse item, string projectId, string token, string mileStone)
        {
            try
            {
                var issue = new ImportCreateIssueRequest(projectId, token, mileStone);
                issue.Title = item.title;
                issue.Description = item.description;

                //Initialize new client
                var newClient = new RestClient(issue.BaseUrl);

                var issueRequest = new RestRequest(issue.BaseUrl + issue.FragmentUrl, Method.POST);
                issueRequest.AddHeader("PRIVATE-TOKEN", issue.PrivateToken);
                issueRequest.AddParameter("id", issue.ProjectId);
                issueRequest.AddParameter("title", issue.Title);
                issueRequest.AddParameter("description", issue.Description);
                issueRequest.AddParameter("milestone_id", Convert.ToInt32(issue.MilestoneId));


                var res = newClient.Execute<ImportCreateIssueResponse>(issueRequest);
                if (res.Content != null)
                {
                    return JsonConvert.DeserializeObject<ImportCreateIssueResponse>(res.Content);
                }
                return null;
            }
            catch (Exception ex )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();

                return null;
            }

        }
        private static void CreateNewNotes(ExportNoteResponse item, string projectId, string token, string issueId)
        {
            try
            {
                var issue = new ImportCreateNoteRequest(projectId, token, issueId);
                issue.Body = item.body;

                //Initialize new client
                var newClient = new RestClient(issue.BaseUrl);

                var issueRequest = new RestRequest(issue.BaseUrl + issue.FragmentUrl, Method.POST);
                issueRequest.AddHeader("PRIVATE-TOKEN", issue.PrivateToken);
                issueRequest.AddParameter("id", issue.ProjectId);
                issueRequest.AddParameter("issue_iid", issue.IssueId);
                issueRequest.AddParameter("body", issue.Body);


                var res = newClient.Execute<ImportCreateIssueResponse>(issueRequest);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Success!!");
                    Console.ResetColor();

                }
                else if (res.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Success!!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Success!!");
                    Console.ResetColor();
                }
            }
            catch (Exception ex )
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.ReadKey();

            }
        }

    }
}

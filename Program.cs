using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using System;
using PureCloudPlatform.Client.V2.Extensions;
using PureCloudPlatform.Client.V2.Model;
using System.Threading.Tasks;

namespace NewOutboundEmailApp
{
    class Program
    {
        static async Task Main(string[] args)
        {           
            var QueueId = "";
            var ClientId = "";
            var ClientSecret = "";
            var Environment = "mypurecloud.ie";

            var _apiClient = new ApiClient($"https://api.{Environment}");
            var _configuration = new Configuration(_apiClient);
            _apiClient.Configuration = _configuration;
            
            var _conversationsApi = new ConversationsApi(_configuration);
            var accessToken = "";

            try
            {
                var authTokenInfo = _apiClient.PostToken(ClientId, ClientSecret);
                accessToken = authTokenInfo.AccessToken;
                Console.WriteLine("Token received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }            

            _configuration.AccessToken = accessToken;

            try
            {
                var emailConv = await _conversationsApi.PostConversationsEmailsAsync(new CreateEmailRequest
                {
                    Direction = CreateEmailRequest.DirectionEnum.Outbound,
                    QueueId = QueueId,
                    Priority = 5,
                    ToAddress = "taras@noralogix.com",
                    ToName = "Taras Buha",
                    Subject = $"Re: Test Subj",
                    HtmlBody = ">>API outbound email.",
                    Provider = "PureCloud Email"
                });
                Console.WriteLine($"New email conv {emailConv.Id} was created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}

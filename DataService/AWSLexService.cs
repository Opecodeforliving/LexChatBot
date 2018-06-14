using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lex.Model;
using Amazon.Lex;
using Microsoft.Extensions.Options;
using Amazon.CognitoIdentity;

namespace LexWebChatBox.DataService
{
    public class AWSLexService : IAWSLexService
    {
        public AWSLexService(IOptions<AWSOptions> awsOptions)
        {
            _awsOptions = awsOptions.Value;
            //InitLex service
            InitializeLex();
        }

        protected void InitializeLex()
        {
            Amazon.RegionEndpoint svcRegionEndpoint;

            //Grab region for Lex Bot services
            svcRegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsOptions.BotRegion);

            //Get credentials from Cognito
            awsCredentials = new CognitoAWSCredentials(
                                _awsOptions.CognitoPoolID, // Identity pool ID
                                svcRegionEndpoint); // Region

            //Instantiate Lex Client with Region
            awsLexClient = new AmazonLexClient(awsCredentials, svcRegionEndpoint);
        }


        public async Task<PostTextResponse> ChatByTextToLex(string lexSessionID, string messageToSend)
        {
            PostTextResponse lexTextResponse;
            PostTextRequest lexTextRequest = new PostTextRequest()
            {
                BotAlias = _awsOptions.LexBotAlias,
                BotName = _awsOptions.LexBotName,
                UserId = lexSessionID,
                InputText = messageToSend,
                SessionAttributes = _lexSessionAttribs
            };

            try
            {
                lexTextResponse = await awsLexClient.PostTextAsync(lexTextRequest);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex);
            }

            return lexTextResponse;
        }

        public async Task<PostTextResponse> ChatByTextToLexAsync(string lexSessionID, Dictionary<string, string> lexSessionAttributes, string messageToSend)
        {
            _lexSessionAttribs = lexSessionAttributes;
            return await ChatByTextToLex(lexSessionID, messageToSend);
        }

        public Task<PostTextResponse> ChatByVoiceToLex(string lexSessionID, Stream voicemessageToSend)
        {
            throw new NotImplementedException();
        }

        public Task<PostTextResponse> ChatByVoiceToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, Stream voicemessageToSend)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        private AWSOptions _awsOptions;
        private Dictionary<string, string> _lexSessionAttribs;
        private AmazonLexClient awsLexClient;
        private CognitoAWSCredentials awsCredentials;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AWSLexService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

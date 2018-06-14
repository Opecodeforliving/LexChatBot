using Amazon.Lex.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LexWebChatBox.DataService
{
    public interface IAWSLexService : IDisposable
    {
        Task<PostTextResponse> ChatByTextToLex(string lexSessionID, string messageToSend);

        Task<PostTextResponse> ChatByTextToLexAsync(string lexSessionID, Dictionary<string, string> lexSessionAttributes, string messageToSend);

        Task<PostTextResponse> ChatByVoiceToLex(string lexSessionID, Stream voicemessageToSend);

        Task<PostTextResponse> ChatByVoiceToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, Stream voicemessageToSend);
    }
}

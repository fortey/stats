using Epic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using System.Web.Script.Serialization;

namespace Epic.Domain.Concrete
{
    public class IntelligenceRepository
    {
        FirebaseClient firebase = new FirebaseClient("");
       
        public async Task<FirebaseObject<string>> SavePlayedDeck(PlayedDeck playedDeck, int playerId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonDeck = serializer.Serialize(playedDeck);
            var result = await firebase.Child(playerId.ToString()).PostAsync(jsonDeck);
            return result;
        }

        public Task<IReadOnlyCollection<FirebaseObject<PlayedDeck>>> PlayedDeckes(int playerId)
        {         
            return firebase.Child(playerId.ToString()).OnceAsync<PlayedDeck>();
        }
    }
}

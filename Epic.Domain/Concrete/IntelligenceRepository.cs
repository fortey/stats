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
        public async Task<FirebaseObject<string>> SavePlayedDeck(PlayedDeck playedDeck, int playerId)
        {
            var firebase = FirebaseClient();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonDeck = serializer.Serialize(playedDeck);
            var result = await firebase.Child(playerId.ToString()).PostAsync(jsonDeck);
            return result;
        }

        public Task<IReadOnlyCollection<FirebaseObject<PlayedDeck>>> PlayedDeckes(int playerId)
        {
            var firebase = FirebaseClient();
            return firebase.Child(playerId.ToString()).OnceAsync<PlayedDeck>();
        }

        public FirebaseClient FirebaseClient()
        {       
           return new FirebaseClient("https://stats-ba8.firebaseio.com", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult("y3xp2EYqBiVCmcEkx5DmZcq4C8KfWLfEDePV7N3D") });
        }
    }
}

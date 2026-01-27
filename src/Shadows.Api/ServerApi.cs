using Shadows.Api.WebApi;
using Shadows.Data.Model;
using System.ServiceModel;

namespace Shadows.Api
{
    public class ServerApi
    {
        #region Common
        private static AndroidClientSoapClient GetClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 5000000;
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            EndpointAddress address = new EndpointAddress("https://mkopecky.dev/drd/AndroidClient.asmx");

            var client = new AndroidClientSoapClient(binding, address);

            return client;
        }

        private static string GetToken(CharacterData character)
        {
            var text = $"{character.UserName}:{character.Password}";

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        public static async Task<bool> LoginAsync(CharacterData character)
        {
            var client = GetClient();

            var result = await client.LoginAsync(GetToken(character));

            return result.Body.LoginResult;
        }

        public async static Task<string> MessageAsync(CharacterData character)
        {
            var client = GetClient();
            MessageResponse message = await client.MessageAsync(GetToken(character));

            return message.Body.MessageResult;
        }

        public static async Task<List<ListObject>> SkillListAsync(CharacterData character)
        {
            var client = GetClient();

            var list = await client.SkillListAsync(GetToken(character));

            List<ListObject> result = new List<ListObject>();

            foreach (CharacterSkillLevel s in list.Body.SkillListResult)
            {
                if (s.IdSkill > 0)
                {
                    result.Add(new ListObject(s.IdSkill, s.Skill));
                }
                else
                {
                    result.Add(new ListObject(0, "Všechny dovednosti"));
                }
            }

            return result;
        }

        #endregion

        #region Item
        public static async Task<List<Item>> ItemSearchSearchAsync(string searchText, int idSkill, CharacterData character)
        {
            var client = GetClient();
            var result = await client.ItemSearchAsync(searchText, idSkill, GetToken(character));

            return result.Body.ItemSearchResult;
        }
        public static async Task<ItemData> ItemDetailAsync(int id, CharacterData character)
        {
            var client = GetClient();
            var result = await client.ItemDetailAsync(id, GetToken(character));
            return result.Body.ItemDetailResult;
        }

        public static async Task SaveItemNoteAsync(int id, string note, CharacterData character)
        {
            var client = GetClient();
            await client.SaveItemNoteAsync(id, note, GetToken(character));
        }
        #endregion

        #region SpellbookItem
        public static async Task<List<SpellbookItem>> SpellbookItemSearchSearchAsync(string searchText, CharacterData character)
        {
            var client = GetClient();

            var result = await client.SpellbookItemSearchAsync(searchText, GetToken(character));

            return result.Body.SpellbookItemSearchResult;
        }
        public static async Task<SpellbookItemData> SpellbookItemDetailAsync(int id, CharacterData character)
        {
            var client = GetClient();

            var data = await client.SpellbookItemDetailAsync(id, GetToken(character));
            return data.Body.SpellbookItemDetailResult;
        }

        public static async Task SaveSpellbookItemNoteAsync(int id, string note, CharacterData character)
        {
            var client = GetClient();
            await client.SaveSpellbookItemNoteAsync(id, note, GetToken(character));
        }
        #endregion

        #region CustomCounter
        public static async Task<List<CustomCounter>> GetCustomCounterListAsync(CharacterData character)
        {
            var client = GetClient();
            var result = await client.GetCustomCounterListAsync(GetToken(character));

            return result.Body.GetCustomCounterListResult;
        }
        public static async Task SaveCustomCounterAsync(CustomCounter data, CharacterData character)
        {
            var client = GetClient();
            await client.SaveCustomCounterAsync(data, GetToken(character));
        }

        public static async Task DeleteCustomCounterAsync(CustomCounter data, CharacterData character)
        {
            var client = GetClient();
            await client.DeleteCustomCounterAsync(data.Id, GetToken(character));
        }

        #endregion

        #region FightNumber
        public static async Task<List<FightNumber>> GetFightNumberListAsync(CharacterData character)
        {
            var client = GetClient();
            var result = await client.GetFightNumberListAsync(GetToken(character));

            return result.Body.GetFightNumberListResult;
        }
        public static async Task SaveFightNumberAsync(FightNumber data, CharacterData character)
        {
            var client = GetClient();
            await client.SaveFightNumberAsync(data, GetToken(character));
        }

        public static async Task DeleteFightNumberAsync(FightNumber data, CharacterData character)
        {
            var client = GetClient();
            await client.DeleteFightNumberAsync(data.Id, GetToken(character));
        }

        public static async Task SaveFightNumberBonusSelectedAsync(FightNumberBonus data, CharacterData character)
        {
            var client = GetClient();
            await client.SaveFightNumberBonusSelectedAsync(data.Id, data.IsSelected, GetToken(character));
        }

        #endregion

        #region CharacterActivity

        public static async Task<List<CharacterActivity>> GetCharacterActivityListAsync(CharacterData character)
        {
            var client = GetClient();
            var result = await client.GetCharacterActivityListAsync(GetToken(character));

            return result.Body.GetCharacterActivityListResult;
        }

        public static async Task SaveCharacterActivityAsync(CharacterActivity data, CharacterData character)
        {
            var client = GetClient();
            await client.SaveCharacterActivityAsync(data, GetToken(character));
        }

        public static async Task DeleteCharacterActivityAsync(CharacterActivity data, CharacterData character)
        {
            var client = GetClient();
            await client.DeleteCharacterActivityAsync(data.Id, GetToken(character));
        }

        #endregion
    }
}

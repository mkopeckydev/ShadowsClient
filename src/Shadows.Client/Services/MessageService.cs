using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using Shadows.Api;
using Shadows.Data.Facade;
using Shadows.Data.Model;

namespace Shadows.Client.Services
{
    public class MessageService
    {
        public MessageService()
        { }

        public async void Show(CharacterData characterData)
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            if (await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                try
                {
                    string message = String.Empty;

                    message = await ServerApi.MessageAsync(characterData);

                    if (!String.IsNullOrEmpty(message))
                    {
                        var notification = new NotificationRequest
                        {
                            CategoryType = NotificationCategoryType.Alarm,
                            NotificationId = 1,
                            Title = $"Zpráva pro {characterData.DisplayName}",
                            Description = message,
                            BadgeNumber = 1,
                            Android =
                        {
                            IconSmallName =
                            {
                                  ResourceName = "mail",
                            }
                        }
                        };
                        await LocalNotificationCenter.Current.Show(notification);
                    }
                }
                catch (Exception e)
                {
                    LogFacade.Add("MessageService.Show", e.Message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PoulpApp.Services
{
    public class MessageService
    {
        public void Subscribe<T>(string messageType, Action<T> action)
        {
            MessagingCenter.Subscribe<MessageService, T>(this, messageType, (sender, message) => action(message));
        }
        
        public void Subscribe(string messageType, Action action)
        {
            MessagingCenter.Subscribe<MessageService>(this, messageType, (sender) => action());
        }

        //public void Send(string messageType, object message)
        //{
        //    MessagingCenter.Send(this, messageType, message);
        //}
        
        //public void Send(string messageType)
        //{
        //    MessagingCenter.Send(this, messageType);
        //}
    }
}

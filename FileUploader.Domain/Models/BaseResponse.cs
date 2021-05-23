using System;
using System.Collections.Generic;

namespace FileUploader.Domain.Models
{
    public class BaseResponse<T> where T: class, new()
    {
        public string Status { get; set; }
        public List<Message> Messages { get; private set; }
        public T Data { get; set; }

        public BaseResponse()
        {
            Data = new T();
            Messages = new List<Message>();
        }
        public void SetSuccess(string messageText = "Completed Successfully", string messageCode = "200")
        {
            addMessage(messageCode, messageText);
            Status = "200";
        }
        public void SetWarning(string messageText = "Caution", string messageCode = "100")
        {
            addMessage(messageCode, messageText);
            Status = "100";
        }
        public void SetError(string messageText = "Error", Exception ex = null, string messageCode = "300")
        {
            addMessage(messageCode, messageText);
            if(ex != null)
                addMessage(messageCode, ex.Message);
            Status = "300";
        }

        private void addMessage(string messageCode, string messageText)
        {
            var message = new Message { MessageCode = messageCode, MessageText = messageText };
            Messages.Add(message);
        }
    }

    public class Message
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
    }

}

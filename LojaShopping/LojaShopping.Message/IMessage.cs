﻿namespace LojaShopping.Message
{
    public interface IMessage
    {
        Task PublicMessage(BaseMessage baseMessage, string topicName);

    }
}
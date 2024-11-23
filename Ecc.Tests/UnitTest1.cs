using Xunit;
using ECC_APP_2.Models;
using System;
using System.Collections.Generic;

namespace ECC_APP_2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Mentor_Constructor_WithEmailAndPassword_SetsPropertiesCorrectly()
        {
            // Arrange
            var expectedEmail = "smith@gmail.com";
            var expectedPassword = "feedfell92";

            // Act
            var mentor = new Mentor(expectedEmail, expectedPassword);

            // Assert
            Assert.Equal(expectedEmail, mentor.Email);
            Assert.Equal(expectedPassword, mentor.Password);
        }

        [Fact]
        public void Message_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var messageId = 1;
            var senderEmail = "sender@gmail.com";
            var receiverEmail = "receiver@gmail.com";
            var content = "This is a test message.";
            var sentDate = DateTime.Now;
            var isRead = false;
            var parentMessageId = 2;

            var responses = new List<MessageResponses>
            {
                new MessageResponses { SenderEmail = "jess@gmail.com", ResponseContent = "This is a response." }
            };

      


        // Act
        var message = new Message
            {
                MessageId = messageId,
                SenderEmail = senderEmail,
                ReceiverEmail = receiverEmail,
                Content = content,
                SentDate = sentDate,
                IsRead = isRead,
                ParentMessageId = parentMessageId,
                Responses = responses
            };

            // Assert
            Assert.Equal(messageId, message.MessageId);
            Assert.Equal(senderEmail, message.SenderEmail);
            Assert.Equal(receiverEmail, message.ReceiverEmail);
            Assert.Equal(content, message.Content);
            Assert.Equal(sentDate, message.SentDate);
            Assert.Equal(isRead, message.IsRead);
            Assert.Equal(parentMessageId, message.ParentMessageId);
            Assert.Equal(responses, message.Responses);
        }

        [Fact]
        public void Message_DefaultConstructor_InitializesResponsesList()
        {
            // Act
            var message = new Message();

            // Assert
            Assert.NotNull(message.Responses);
            Assert.Empty(message.Responses); // Ensure the list is empty by default
        }
    }
}

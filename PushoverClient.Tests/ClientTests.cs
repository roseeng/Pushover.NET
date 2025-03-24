using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PushoverClient.Tests
{
    [TestClass]
    public class ClientTests
    {
        private const string TEST_APP_KEY = "av7687c73hw87ou75mwd8c9bb9iwt1"; //  "YOURAPPKEY";    
        private const string TEST_USER_KEY = "untPRNMPAdJ822kSkqiBxKFDdP2wC3"; // "YOURUSERKEY";

        [TestMethod]
        public void PushWithValidParms_ReturnsSuccessful()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";

            //  Act
            var pclient = new Pushover(TEST_APP_KEY);
            var response = pclient.Push(title, message, TEST_USER_KEY);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        public async Task PushAsyncWithValidParms_ReturnsSuccessful()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";

            //  Act
            var pclient = new Pushover(TEST_APP_KEY);
            var response = await pclient.PushAsync(title, message, TEST_USER_KEY);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PushWithNoKey_ReturnsError()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";

            //  Act
            var pclient = new Pushover(TEST_APP_KEY);
            var response = await pclient.PushAsync(title, message);

            //  Assert - above code should error before this
            Assert.Fail();
        }

        [TestMethod]
        public async Task PushWithDefaultKey_ReturnsSuccessful()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";

            //  Act
            var pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            var response = await pclient.PushAsync(title, message);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        public async Task PushWithHighPriority_ReturnsSuccessful()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";
            var priority = Priority.High;

            //  Act
            var pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            var response = await pclient.PushAsync(title, message, priority: priority);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        public async Task PushWithEmergencyPriority_ReturnsSuccessful()
        {
            // Arrange
            var title = "Test title";
            var message = "This is a test push notification message";
            var priority = Priority.Emergency;

            //  Act
            var pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            var response = await pclient.PushAsync(title, message, priority: priority);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        public async Task PushWithSound_ReturnsSuccessful()
        {
            //  Arrange
            var title = "Test title";
            var message = "This is a test push notification message";

            //  Act
            var pclient = new Pushover(TEST_APP_KEY) { DefaultUserGroupSendKey = TEST_USER_KEY };
            var response = await pclient.PushAsync(title, message, notificationSound: NotificationSound.Alien);

            //  Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
        }

        [TestMethod]
        public void TestJsonDeserialization()
        {
            string buf = "{\"status\":1,\"request\":\"822b0ffb-ac63-4c53-97a8-546647d8504c\"}";

            // Compatibility with Newtonsoft.Json
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = System.Text.Json.JsonSerializer.Deserialize<PushResponse>(buf, serializeOptions);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Status);
            Assert.AreEqual("822b0ffb-ac63-4c53-97a8-546647d8504c", response.Request);
        }
    }
}

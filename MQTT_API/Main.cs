using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT_API
{
    public class Main
    {
        MqttClient client;
        string clientId;

        public  void Teste()
        {
            string BrokerAddress = "test.mosquitto.org";

            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();

            client.Connect(clientId);
        }


        // this code runs when the button "Subscribe" is clicked
        private void btnSubscribe_Click(string Text)
        {
            if (Text != "")
            {
                // whole topic
                string Topic = "/ElektorMyJourneyIoT/" + Text + "/test";

                // subscribe to the topic with QoS 2
                client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
                Text = "";
            }
            else
            {
                "You have to enter a topic to subscribe!";
            }
        }


        // this code runs when the button "Publish" is clicked
        private void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (txtTopicPublish.Text != "")
            {
                // whole topic
                string Topic = "/ElektorMyJourneyIoT/" + txtTopicPublish.Text + "/test";

                // publish a message with QoS 2
                client.Publish(Topic, Encoding.UTF8.GetBytes(txtPublish.Text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
            else
            {
                System.Windows.MessageBox.Show("You have to enter a topic to publish!");
            }
        }


        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            Dispatcher.Invoke(delegate
            {              // we need this construction because the receiving code in the library and the UI with textbox run on different threads
                txtReceived.Text = ReceivedMessage;
            });
        }
    }
}
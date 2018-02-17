using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT_MvcAPI.Controllers
{
    public class HomeController : Controller
    {
        private string MQTT_BROKER_ADDRESS = "iot.eclipse.org";
        private MqttClient client = new MqttClient("iot.eclipse.org");
        public ActionResult Index()
        {
            // create client instance 
            //MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            // subscribe to the topic "/home/temperature" with QoS 2 
            var ret = client.Subscribe(new string[] { "/29asoMqttLampInput" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

            return View();
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // handle message received 
            Debug.WriteLine(e.Message);
        }

        public void InscreveTopicoLampada()
        {
            // create client instance 
            MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // subscribe to the topic "/home/temperature" with QoS 2 
            client.Subscribe(new string[] { "/29asoMqttLampOutput" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
        }

        public void InscreveTopicoPresenca()
        {
            // create client instance 
            MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // subscribe to the topic "/home/temperature" with QoS 2 
            client.Subscribe(new string[] { "/29asoMqttPresencaOutput" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }


        public void EscrevePresenca(double value)
        {
            // create client instance 
            MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            string strValue = Convert.ToString(value);

            // publish a message on "/home/temperature" topic with QoS 2 
            client.Publish("/29asoMqttLampOutput", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); 
        }

        public void EscreveLampada(double value)
        {
            // create client instance 
            //MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string strValue = value == 10 ? "D" : "L";// Convert.ToString(value);

            // publish a message on "/home/temperature" topic with QoS 2 
            //var ret = client.Publish("/29asoMqttLampOutput", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            //var ret2 = client.Publish("/home/29asoMqttLampOutput", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            var ret3 = client.Publish("29asoMqttLampOutput", Encoding.UTF8.GetBytes(strValue), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }
}

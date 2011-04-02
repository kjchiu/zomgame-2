using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame
{
	class MessageHandler
	{
		List<string> messageList;

		private static MessageHandler instance;

		public MessageHandler()
		{
			messageList = new List<string>();
		}

		/// <summary>
		/// Takes in a list of params (either strings or GameObjects) and converts that into a message with color tags. 
		/// Then the message is added to the list.
		/// </summary>
		/// <param name="message">List of message inputs</param>
		public void AddMessage(params object[] message)
		{
			string newMessage = "";
			foreach (object o in message){
				if (o is string){
					newMessage += " " + ((string)o).Trim() + " ";

				}
				if (o is Player){
					newMessage += "[p]" + ((Player)o).Name + "[/p]";
				}
				if (o is Zombie){
						newMessage += "a [g]zombie[/g]";
				}
				if (o is Item){
					newMessage += "[o]" + ((Item)o).Name + "[/o]";
				}
			}
			//capitalize the first word
			//
			if (newMessage[0].Equals('['))
			{
				newMessage = newMessage.Substring(0, 3) + char.ToUpper(newMessage[3]) + newMessage.Substring(4);
			}
			else
			{
				newMessage = char.ToUpper(newMessage[0]) + newMessage.Substring(1);
			}
			messageList.Add(newMessage);


		}

		public List<string> MessageList
		{
			get { return messageList; }
		}

		public static MessageHandler Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MessageHandler();
				}
				return instance;
			}
		}

		
	}
}

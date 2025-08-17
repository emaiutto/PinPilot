using System;
using System.Collections.Generic;
using System.Text;

namespace FSUIPC;

public class TextMenu
{
	private int changedTime;

	private int previouschangedTime;

	private int dataLength;

	private byte classInstance;

	private object lockObject = new object();

	public float Duration { get; private set; }

	public bool IsMenu => MessageType == SimConnectMessageType.Menu;

	public bool Changed => changedTime != previouschangedTime;

	public string[] MenuItems { get; private set; }

	public int MenuItemCount => MenuItems.Length;

	public string MenuTitleText { get; private set; }

	public string MenuPromptText { get; private set; }

	public string Message { get; private set; }

	public SimConnectMessageType MessageType { get; private set; }

	public SimConnectMessageColor MessageColor { get; private set; }

	public int ID { get; private set; }

	public TextMenu()
		: this(0)
	{
	}

	public TextMenu(byte ClassInstance)
	{
		classInstance = ClassInstance;
		MenuItems = new string[0];
	}

	public void RefreshData()
	{
		lock (lockObject)
		{
			if (FSUIPCConnection.IsConnectionOpenForClass(classInstance))
			{
				string text = classInstance + "~~SimConnectTextDisplay~~";
				Offset<byte[]> offset = new Offset<byte[]>(text, 45056, 2048);
				FSUIPCConnection.Process(classInstance, text);
				byte[] value = offset.Value;
				previouschangedTime = changedTime;
				changedTime = BitConverter.ToInt32(value, 0);
				MessageType = (SimConnectMessageType)value[5];
				MessageColor = SimConnectMessageColor.NotApplicable;
				if (MessageType == SimConnectMessageType.Scrolling || MessageType == SimConnectMessageType.Static)
				{
					MessageColor = (SimConnectMessageColor)value[4];
				}
				Duration = BitConverter.ToSingle(value, 8);
				ID = BitConverter.ToInt32(value, 12);
				dataLength = BitConverter.ToInt32(value, 16);
				int i = 20;
				List<string> list = new List<string>();
				if (IsMenu)
				{
					MenuPromptText = "";
					MenuTitleText = "";
					MenuItems = new string[0];
				}
				else
				{
					Message = "";
				}
				int num = 0;
				string text2 = "";
				for (; i < dataLength + 20; i++)
				{
					char c = (char)value[i];
					if (c.Equals('\0'))
					{
						switch (num)
						{
						case 0:
							if (IsMenu)
							{
								MenuTitleText = text2;
							}
							else
							{
								Message = text2;
							}
							break;
						case 1:
							MenuPromptText = text2;
							break;
						default:
							list.Add(text2);
							break;
						}
						num++;
						text2 = "";
					}
					else
					{
						text2 += c;
					}
				}
				if (IsMenu)
				{
					MenuItems = list.ToArray();
				}
				FSUIPCConnection.DeleteGroup(text);
				return;
			}
			if (classInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + classInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}

	public override string ToString()
	{
		return ToString("\r\n", IncludeMenuTitle: true, IncludeMenuPrompt: true, AddNumbersToMenuItem: true);
	}

	public string ToString(string RowDelimiter, bool IncludeMenuTitle, bool IncludeMenuPrompt, bool AddNumbersToMenuItem)
	{
		if (IsMenu)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (IncludeMenuTitle)
			{
				stringBuilder.Append(MenuTitleText);
				stringBuilder.Append(RowDelimiter);
			}
			if (IncludeMenuPrompt)
			{
				stringBuilder.Append(MenuPromptText);
				stringBuilder.Append(RowDelimiter);
			}
			for (int i = 0; i < MenuItemCount; i++)
			{
				stringBuilder.Append((AddNumbersToMenuItem ? (i + 1 + " - ") : "") + MenuItems[i].ToString());
				stringBuilder.Append(RowDelimiter);
			}
			return stringBuilder.ToString();
		}
		return Message;
	}
}

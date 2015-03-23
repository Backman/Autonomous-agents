using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpSteer
{
	public partial class Menu : Form
	{
		//public delegate void Callback();
		//public delegate void Callback<T>(T t);
		//public delegate void Callback<T, U>(T t, U u);
		//public delegate void Callback<T, U, V>(T t, U u, V v);

		//public delegate void Listener();

		//public Dictionary<string, Delegate> _callbacks = new Dictionary<string, Delegate>();
		
		//private Dictionary<string, Listener> _listeners = new Dictionary<string, Listener>();

		public Menu()
		{
			InitializeComponent();
		}

		//public void RegisterCallback(string key, Callback callback)
		//{
		//	OnCallbackRegistering(key, callback);
		//	_callbacks[key] = (Callback)_callbacks[key] + callback;
		//}

		//public void RegisterCallback<T>(string key, Callback<T> callback)
		//{
		//	OnCallbackRegistering(key, callback);
		//	_callbacks[key] = (Callback<T>)_callbacks[key] + callback;
		//}

		//public void RegisterCallback<T, U>(string key, Callback<T, U> callback)
		//{
		//	OnCallbackRegistering(key, callback);
		//	_callbacks[key] = (Callback<T, U>)_callbacks[key] + callback;
		//}

		//public void RegisterCallback<T, U, V>(string key, Callback<T, U, V> callback)
		//{
		//	OnCallbackRegistering(key, callback);
		//	_callbacks[key] = (Callback<T, U, V>)_callbacks[key] + callback;
		//}

		//private void OnCallbackRegistering(string key, Delegate callback)
		//{
		//	Delegate d;
		//	if (!_callbacks.TryGetValue(key, out d))
		//	{
		//		d = null;
		//		_callbacks.Add(key, d);
		//	}

		//	if (d != null && d.GetType() != callback.GetType())
		//	{
		//		throw new Exception(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", key, d.GetType().Name, callback.GetType().Name));
		//	}
		//}

		//private Listener GetListener(string key)
		//{
		//	Listener listener;
		//	if (_listeners.TryGetValue(key, out listener))
		//	{
		//		return listener;
		//	}

		//	return null;
		//}

		private void mapPathField_TextChanged(object sender, EventArgs e)
		{

		}

		private void loadMapPath_Click(object sender, EventArgs e)
		{

		}

		private void loadMapButton_Click(object sender, EventArgs e)
		{
			Messenger.Broadcast<string>("OnLoadMapButton", mapPathField.Text);
		}

		private void pathFollowButton_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void flowFieldFollowButton_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void showGridToggle_CheckedChanged(object sender, EventArgs e)
		{
			Messenger.Broadcast<bool>("OnShowGridToggle", showGridToggle.Checked);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Messenger.Broadcast("OnExit");
		}
	}
}

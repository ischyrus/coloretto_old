using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Coloretto.Player
{
    public enum ConnectionState
    {
        Disconnected,
        Connected,
        Limited
    }

    [Serializable]
    public class ValueChangedEventArgs<T> : EventArgs
    {
		private T _newValue;
		
        public T NewValue 
		{ 
			get{ return _newValue; }
			set{ _newValue = value; }
		}

        public ValueChangedEventArgs(T value)
        {
            NewValue = value;
        }
    }

    /// <summary>
    /// Class indicating the player's status
    /// </summary>
    [Serializable]
    public class PlayerStatus
    {
        private ConnectionState _connectionState;

        /// <summary>
        /// Get or set the connection state
        /// </summary>
        public ConnectionState ConnectionState
        {
            get { return _connectionState; }
            set
            {
                if (_connectionState == value)
                    return;

                _connectionState = value;
                OnConnectionStateChnaged(value);
            }
        }

        private void OnConnectionStateChnaged(ConnectionState value)
        {
            if (ConnectionStateChanged != null)
                ConnectionStateChanged(this, new ValueChangedEventArgs<ConnectionState>(value));
        }

        public event EventHandler<ValueChangedEventArgs<ConnectionState>> ConnectionStateChanged;
    }
}

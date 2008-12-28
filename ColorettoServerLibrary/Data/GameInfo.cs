using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ColorettoServerLibrary;

namespace Coloretto.Services.Data
{
	[DataContract]
	public class GameInfo
	{
		[DataMember]
		public DateTime Creation { get; set; }

		[DataMember]
		public Guid GameId { get; set; }

		[DataMember]
		public string Owner { get; set; }

		[DataMember]
		public List<string> Players { get; set; }

		[DataMember]
		public GameStates State { get; set; }
	}
}

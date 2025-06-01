using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Action
	{
		public Guid Id { get; set; }
		public Guid MasterId { get; set; }
		public Guid SlaveId { get; set; }
		public string Name { get; set; }

		public Action() { }
		public Action(Guid id, Guid masterId, Guid slaveId, string name)
		{
			Id = id;
			MasterId = masterId;
			SlaveId = slaveId;
			Name = name;
		}
	}
}

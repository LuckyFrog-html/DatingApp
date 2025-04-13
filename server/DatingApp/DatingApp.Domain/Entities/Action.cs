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
		public string MasterId { get; set; }
		public string SlaveId { get; set; }
		public string Name { get; set; }

		public Action() { }
		public Action(Guid id, string masterId, string slaveId, string name)
		{
			Id = id;
			MasterId = masterId;
			SlaveId = slaveId;
			Name = name;
		}
	}
}

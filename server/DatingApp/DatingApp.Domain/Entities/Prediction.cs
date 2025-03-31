using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
	public class Prediction
	{
		public Guid Id { get; set; }
		public Guid ownerId { get; set; }
		public Guid predictedId { get; set; }

		public Prediction() { }
		public Prediction(Guid id, Guid ownerId, Guid predictedId)
		{
			Id = id;
			this.ownerId = ownerId;
			this.predictedId = predictedId;
		}
	}
}

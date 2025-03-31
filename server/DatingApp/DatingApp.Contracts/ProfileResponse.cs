using System.Numerics;

namespace DatingApp.Contracts
{
	public class ProfileResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Age { get; set; }
		public string Town { get; set; }
		public BigInteger Balance { get; set; }

		public ProfileResponse(Profile) { }
	}
}

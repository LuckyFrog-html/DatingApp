using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DatingApp.Api.Conventions
{
	public class RoutePrefixConvention : IControllerModelConvention
	{
		private readonly AttributeRouteModel _routePrefix;

		public RoutePrefixConvention(IRouteTemplateProvider route)
		{
			_routePrefix = new AttributeRouteModel(route);
		}

		public void Apply(ControllerModel controller)
		{
			foreach (var selector in controller.Selectors)
			{
				selector.AttributeRouteModel = selector.AttributeRouteModel != null
					? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
					: _routePrefix;
			}
		}
	}
}

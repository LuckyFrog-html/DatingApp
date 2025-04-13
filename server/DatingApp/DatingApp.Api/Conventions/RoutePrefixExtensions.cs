using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DatingApp.Api.Conventions
{
	public static class RoutePrefixExtensions
	{
		public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
		{
			var convention = new RoutePrefixConvention(routeAttribute);
			opts.Conventions.Add(new RoutePrefixConventionWrapper(convention));
		}

		private class RoutePrefixConventionWrapper : IApplicationModelConvention
		{
			private readonly IControllerModelConvention _convention;

			public RoutePrefixConventionWrapper(IControllerModelConvention convention)
			{
				_convention = convention;
			}

			public void Apply(ApplicationModel application)
			{
				foreach (var controller in application.Controllers)
				{
					_convention.Apply(controller);
				}
			}
		}
	}
}

using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions.AddAuthHeaderParam
{
	/// <summary>
	/// Add field for auth token input.
	/// </summary>
	public class AddAuthHeaderParam : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

			operation.Parameters
					 .Add(new OpenApiParameter
					 {
						 Name = "Authorization",
						 In =ParameterLocation.Header,
						 Description = "e.g.: bearer [token]",
						 
					 });
		}
	}
}

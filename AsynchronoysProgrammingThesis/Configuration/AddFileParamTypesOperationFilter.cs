using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blitz.API.Configuration
{
    public class AddFileParamTypesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var formFileParams = context.ApiDescription.ActionDescriptor.Parameters
                .Where(p => p.ParameterType == typeof(IFormFile))
                .Select(p => new OpenApiParameter
                {
                    Name = p.Name,
                    Schema = new OpenApiSchema { Type = "file" },
                    In = ParameterLocation.Query,
                    Required = true
                });

            foreach (var param in formFileParams)
            {
                operation.Parameters.Add(param);
            }
        }
    }
}
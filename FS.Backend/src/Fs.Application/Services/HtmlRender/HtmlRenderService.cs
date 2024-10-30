using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Fs.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Fs.Application.Services.HtmlRender
{
    public sealed class HtmlRenderService : IHtmlRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public HtmlRenderService(IRazorViewEngine razorViewEngine, IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderAsync(string templateName, object model = null, string cultureName = null)
        {
            try
            {
                return await RenderViewAsync(templateName, model, new Dictionary<string, object>
                {
                    {"CurrentCulture", new CultureInfo(string.IsNullOrWhiteSpace(cultureName) ? "en" : cultureName)}
                });
            }
            catch (Exception exc)
            {
                throw new HtmlRenderingException("HTML rendering has failed.", exc);
            }
        }

        private async Task<string> RenderViewAsync(string viewName, object model, Dictionary<string, object> viewBag)
        {
            var actionContext = new ActionContext(new DefaultHttpContext
                {
                    RequestServices = _serviceProvider
                },
                new Microsoft.AspNetCore.Routing.RouteData(),
                new ActionDescriptor());

            await using var sw = new StringWriter();
            var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
                throw new Exception("HTML view could not be found.");

            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            foreach (var data in viewBag)
                viewDataDictionary.Add(data);

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDataDictionary,
                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }
}

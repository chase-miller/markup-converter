using MarkupConverterWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkupConverterServiceApi;
using Ninject;

namespace MarkupConverterWeb.Controllers
{
    public class MarkupConverterController : Controller
    {
        private IMarkupConverterService markupConverterService;

        [Inject]
        public MarkupConverterController(IMarkupConverterService markupConverterService)
        {
            this.markupConverterService = markupConverterService;
        }

        // GET: MarkupConverter
        public ActionResult Index()
        {
            MarkupConverterModel model = new MarkupConverterModel();
            this.markupConverterService.GetLanguages().ToList<string>().ForEach(x => model.Languages.Add(x));
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult ResultPartial(string markupLanguage, string markupText)
        {
            MarkupConverterResultModel model = new MarkupConverterResultModel();

            string result = this.markupConverterService.ParseMarkup(markupLanguage, markupText);

            model.Result = result;

            return PartialView(model);
        }
    }
}
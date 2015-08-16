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

        [HttpPost]
        public ActionResult ConvertMarkup(MarkupConverterModel model)
        {
            string result = this.markupConverterService.ParseMarkup(model.MarkupLanguage, model.MarkupText);

            model.ConvertedMarkup = result;

            return PartialView("ResultPartial", model);
        }
    }
}
using System;
using System.Web.Mvc;
using log4net;
using PinkTravel.Localization;

namespace PinkTravel.Controllers
{
    public abstract class LocalizedController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            LocalizationControllerHelper.OnBeginExecuteCore(this);

            return base.BeginExecuteCore(callback, state);
        }
    }
}
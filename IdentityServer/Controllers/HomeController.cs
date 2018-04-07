using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
     [SecurityHeaders]
     [Authorize]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _identityserverInteraction;

        public HomeController(IIdentityServerInteractionService identityServerInteraction)
        {
            _identityserverInteraction = identityServerInteraction;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
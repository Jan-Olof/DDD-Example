﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    /// <summary>
    /// TODO: Remove this example controller.
    /// </summary>
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        /// <summary>
        /// Test that you get an answer.
        /// </summary>
        [HttpGet]
        public string Get()
            => "This is OK.";

        /// <summary>
        /// Test is not implemented error.
        /// </summary>
        [HttpGet("NotImplemented")]
        public IEnumerable<string> GetNotImplemented()
            => throw new NotImplementedException();
    }
}
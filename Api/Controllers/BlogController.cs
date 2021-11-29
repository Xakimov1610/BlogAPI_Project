using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entity;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _ctx;

        public BlogController(BlogContext ctx)
        {
            _ctx = ctx;
        }

    }
}
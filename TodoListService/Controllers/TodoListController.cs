﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TodoListService.Models;

namespace TodoListService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        static ConcurrentBag<TodoItem> todoStore = new ConcurrentBag<TodoItem>();

        // GET: api/todolist
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            // Please note: use of "Context.User", instead of the standard ClaimsPrincipal.Current, is due to a bug in this release
            string owner = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return todoStore.Where(t => t.Owner == owner).ToList();
        }
    
        // POST api/todolist
        [HttpPost]
        public void Post(string Title)
        {
            // Please note: use of "Context.User", instead of the standard ClaimsPrincipal.Current, is due to a bug in this release
            string owner = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            todoStore.Add(new TodoItem { Owner = owner, Title = Title });
        }
    }
}

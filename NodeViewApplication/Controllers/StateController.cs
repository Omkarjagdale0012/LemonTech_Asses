using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodeViewApplication.Data;
using NodeViewApplication.Models;

namespace NodeViewApplication.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            // Loop and add the Parent Nodes.
            foreach (State state in _context.State)
            {
                nodes.Add(new TreeViewNode { id = state.Id.ToString(), parent = "#", text = state.Title });
            }

            // Loop and add the Child Nodes.
            foreach (City city in _context.City)
            {
                nodes.Add(new TreeViewNode { id = $"{city.StateId}-{city.Id}", parent = city.StateId.ToString(), text = city.Name });
            }

            // Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);

            // Do something with the selected items if needed.

            return RedirectToAction("Index");
        }
    }
}
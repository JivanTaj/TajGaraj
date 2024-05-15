using Microsoft.AspNetCore.Mvc;
using TajGaraj.Models;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TajGaraj.Controllers
{
    public class PartsController : Controller
    {
        // Path to the JSON file for part data 
        private string _path = "Data/partData/parts.json";

        //------------------------------------------------------------------------------------//
        // Index homepage 
        //------------------------------------------------------------------------------------//
        public async Task<ActionResult> Index()
        {
            // reads part data file into list to display tables in index view
            var parts = new List<Part>();
            if (System.IO.File.Exists(_path))
            {
                var json = await System.IO.File.ReadAllTextAsync(_path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    parts = JsonSerializer.Deserialize<List<Part>>(json);
                }
            }
            // index view #parts/index
            return View(parts);
        }

        //------------------------------------------------------------------------------------//
        // Create Methods for parts 
        //------------------------------------------------------------------------------------//
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost] // post method for parts, fills off a form in #parts/create
        public async Task<ActionResult> Create(Part part)
        {
            if (ModelState.IsValid)
            {
                // Load existing parts from the file
                var parts = new List<Part>();
                // if the file exists read it and append 
                if (System.IO.File.Exists(_path))
                {
                    var json = await System.IO.File.ReadAllTextAsync(_path);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        parts = JsonSerializer.Deserialize<List<Part>>(json);
                    }
                }
                // Add the new part
                parts.Add(part);
                // Save all parts back to the file
                var newJson = JsonSerializer.Serialize(parts);
                await System.IO.File.WriteAllTextAsync(_path, newJson);
                return RedirectToAction("Index"); // redirect #parts/index
            }

            //create view #parts/create
            return View(part);
        }

        //------------------------------------------------------------------------------------//
        // Edit Methods for parts
        //------------------------------------------------------------------------------------//
        public async Task<ActionResult> Edit(int id)
        {
            var parts = new List<Part>();
            if (System.IO.File.Exists(_path))
            {
                var json = await System.IO.File.ReadAllTextAsync(_path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    parts = JsonSerializer.Deserialize<List<Part>>(json);
                }
            }

            var part = parts.FirstOrDefault(p => p.ID == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Part part)
        {
            if (ModelState.IsValid)
            {
                var parts = new List<Part>();
                if (System.IO.File.Exists(_path))
                {
                    var json = await System.IO.File.ReadAllTextAsync(_path);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        parts = JsonSerializer.Deserialize<List<Part>>(json);
                    }
                }

                var existingPart = parts.FirstOrDefault(p => p.ID == part.ID);
                if (existingPart != null)
                {
                    // Update the existing part
                    existingPart.Name = part.Name;
                    existingPart.Description = part.Description;
                    existingPart.Location = part.Location;

                    // Save all parts back to the file
                    var newJson = JsonSerializer.Serialize(parts);
                    await System.IO.File.WriteAllTextAsync(_path, newJson);

                    return RedirectToAction("Index");
                }
            }

            return View(part);
        }
    }
}

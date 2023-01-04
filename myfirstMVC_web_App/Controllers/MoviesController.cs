using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using myfirstMVC_web_App.Models;
using myfirstMVC_web_App.viewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace myfirstMVC_web_App.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var AllMovies = await _context.Movies.ToListAsync();

            return View(AllMovies);
        }
        public async Task<IActionResult> Create()
        {
            FilmsModelsViews MovieModel = new FilmsModelsViews
            {

                Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync()
            };

            return View(MovieModel);
        }
        // create after post :: 
        [HttpPost]
        public async Task<IActionResult> Create(FilmsModelsViews MovieModel)
        {   //check if the model is valid :: 
            if (!ModelState.IsValid)
            {
                MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
                return View(MovieModel);
            }
            // catch my file : : : : : 
            var files = Request.Form.Files;
            if (!files.Any())
            {
                ModelState.AddModelError("Poster", "You should select image !!... ");
                MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
                return View(MovieModel);
            }
            var poster = files.FirstOrDefault();
            //check the length of the poster : 
            if (poster.Length > 1048576)
            {
                ModelState.AddModelError("Poster", "size of image can't be more than 1MegaByte");
                MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
                return View(MovieModel);
            }
            //Convert to binary::
            using var imageinbinary = new MemoryStream();
            await poster.CopyToAsync(imageinbinary);
            MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
            Movie movie = new Movie
            {
                Title = MovieModel.Title,
                CategoryId = MovieModel.CategoryId,
                Year = MovieModel.Year,
                Story = MovieModel.Story,
                Rate = MovieModel.Rate,
                Poster = imageinbinary.ToArray()
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mov = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (mov != null)
            {
                FilmsModelsViews MovieModel = new FilmsModelsViews
                {   Id= mov.Id,
                    Title = mov.Title,
                    Year = mov.Year,
                    Story = mov.Story,
                    Rate = mov.Rate,
                    Poster = mov.Poster,
                    CategoryId = mov.CategoryId,
                    Categs = await _context.Categs.ToListAsync(),
                };
                return View(MovieModel);


            }

            return Content("a7a");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FilmsModelsViews MovieModel)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(b => b.Id == MovieModel.Id);
            if (!ModelState.IsValid)
            {
                MovieModel.Poster = movie.Poster;
                MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
               
                return View(MovieModel);

            }
            
            var files = Request.Form.Files ;  
            if(files.Any() )
            {
                var poster = files.FirstOrDefault(); 
                
               
                using var imageinbinary = new MemoryStream();
                await poster.CopyToAsync(imageinbinary);

                if (poster.Length >  1048576) 
                {
                    MovieModel.Poster = movie.Poster;
                    MovieModel.Categs = await _context.Categs.OrderBy(m => m.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "the length should be less than 1MGBYTE") ; 
                    return View(MovieModel) ; 
                }

                movie.Title = MovieModel.Title;
                movie.CategoryId = MovieModel.CategoryId;
                movie.Year = MovieModel.Year;
                movie.Story = MovieModel.Story;
                movie.Rate = MovieModel.Rate;
                movie.Poster = imageinbinary.ToArray();
                

            }
            else
            {
                movie.Title = MovieModel.Title;
                movie.CategoryId = MovieModel.CategoryId;          
                movie.Year = MovieModel.Year;
                movie.Story = MovieModel.Story;
                movie.Rate = MovieModel.Rate;
                //movie.Poster = MovieModel.Poster;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");    
        }
        public async Task<IActionResult> Detail(int id)  
        {
            var mov = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (mov != null)
            {
                var categ_of_film = await _context.Categs.FirstOrDefaultAsync(b=>b.Id==mov.CategoryId); 
                FilmsModelsViews MovieModel = new FilmsModelsViews
                {   
                    Id = mov.Id,
                    Title = mov.Title,
                    Year = mov.Year,
                    Story = mov.Story,
                    Rate = mov.Rate,
                    Poster = mov.Poster,
                    CategoryId = mov.CategoryId,
                   Categoffilm=categ_of_film 
                };
                return View(MovieModel);


            }

            return Content("a7a");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) 
                return BadRequest();
            var mov = await _context.Movies.FirstOrDefaultAsync(b => b.Id == id);
            if (mov == null)
                return NotFound();

             _context.Movies.Remove(mov);
             _context.SaveChanges();
            return Ok();
               
        }

    }  

}

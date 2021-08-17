using AutoMapper;
using BookStore.API.Dtos.Book;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers

{
    [Route("api/[controller]")]
    public class BooksController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();

            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));

        }

         [HttpGet("{id:int}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookResultDto>(book));
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);
            if (!books.Any()) return NotFound();
            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books));
        }
        [HttpPost]
        public async Task<IActionResult> Add(BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDto);

            var bookResult =await  _bookService.Add(book);

            if (bookResult == null) return BadRequest();
            return Ok(_mapper.Map<BookResultDto>(bookResult));

        }

        [HttpPut("{id:int}")]
       

        public async Task<IActionResult> Update(int id,BookEditDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();

            await _bookService.Update(_mapper.Map<Book>(bookDto));
            return Ok(bookDto);

        }

        [HttpDelete("{id:int}")]
        

        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return BadRequest();

            await _bookService.Remove(book);
            return Ok();

        }
        [Route("search-Book-with-category/{searchedValue}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategory(searchedValue));
            if (!books.Any()) return NotFound("No book was found");
            return Ok(_mapper.Map<IEnumerable<BookResultDto>>(books)); 
        }
    }
}

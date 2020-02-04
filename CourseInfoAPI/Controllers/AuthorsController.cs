using System;
using System.Collections.Generic;
using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;
using CourseInfoAPI.ResourceParameters;
using CourseInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseInfoAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    //[Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var authors = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId:guid}", Name = "GetAuthor")]
        public  ActionResult<AuthorDto> GetAuthorById(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (null == author)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            /* Automatically checked because of ApiController annotation */
            //if(null == authorCreateDto)
            //{
            //    return BadRequest();
            //}
            var author = _mapper.Map<Author>(authorCreateDto);
            _courseLibraryRepository.AddAuthor(author);
            _courseLibraryRepository.Save();
            var authorForReturn = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute("GetAuthor",
                new { authorId = authorForReturn.Id },
                authorForReturn);
        }

        [HttpOptions]
        public ActionResult AuthorOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS,HEAD");
            return Ok();
        }
    }
}

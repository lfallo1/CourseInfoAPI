using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;
using CourseInfoAPI.ResourceParameters;
using CourseInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<AuthorDto>> getAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var authors = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId:guid}")]
        public  ActionResult<AuthorDto> getAuthorById(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (null == author)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }
    }
}

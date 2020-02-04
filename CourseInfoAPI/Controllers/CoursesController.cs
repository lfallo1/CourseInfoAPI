using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;
using CourseInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseInfoAPI.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId:guid}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> getCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courses = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId:guid}", Name="GetCourseForAuthor")]
        public ActionResult<CourseDto> getCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if(null == course)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public ActionResult<CourseDto> createCourseForAuthor(Guid authorId, CourseCreateDto courseCreateDto)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(courseCreateDto);

            _courseLibraryRepository.AddCourse(authorId, courseEntity);

            _courseLibraryRepository.Save();

            var courseForReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId = authorId, courseId = courseEntity.Id },
                courseForReturn);
        }

        [HttpPut("{courseId}")]
        public ActionResult<CourseDto> createCourseForAuthor(Guid authorId, Guid courseId, CourseUpdateDto courseUpdateDto)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (null == courseFromRepo)
            {
                return NotFound();
            }

            _mapper.Map(courseUpdateDto, courseFromRepo);

            //not implemented in repo since entity auto updated during map - but future proof in case repo implementation changes
            _courseLibraryRepository.UpdateCourse(courseFromRepo);

            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}

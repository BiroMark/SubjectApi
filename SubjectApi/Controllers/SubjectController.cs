using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubjectApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SubjectApi.Controllers
{
    [Route("subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Subject> Post(CreateSubjectDto createSubjectDto)
        {
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                SubjectName = createSubjectDto.SubjectName,
                NumberOfHours = createSubjectDto.NumberOfHours,
                Description = createSubjectDto.Description,
                CreatedTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,

            };
            if (subject != null)
            {
                using (var context = new SubjectDbContext())
                {
                    context.Subjects.Add(subject);
                    context.SaveChanges();
                    return StatusCode(201, subject);
                }
            }
            return BadRequest();

        }
        
        [HttpGet]
        public ActionResult<Subject> Get()
        {
            using var context = new SubjectDbContext();
            return Ok(context.Subjects.ToList());

        }
        [HttpGet("{id}")]
        public ActionResult<Subject> GetById(Guid id)
        {
            using (var context = new SubjectDbContext())
            {
                var subject = context.Subjects.SingleOrDefault(x => x.Id == id);
                if (subject != null)
                {
                    return Ok(subject);
                }
                return NotFound();
            }
        }
        [HttpPut]
        public ActionResult<Subject> Put(Guid id, UpdateSubjectDto updateSubjectdto)
        {
            using (var context = new SubjectDbContext())
            {

                var subject = context.Subjects.FirstOrDefault(x => x.Id == id);
                if (subject != null)
                {
                    subject.SubjectName = updateSubjectdto.SubjectName;
                    subject.NumberOfHours = updateSubjectdto.NumberOfHours;
                    subject.Description = updateSubjectdto.Description;


                    context.Subjects.Update(subject);
                    context.SaveChanges();
                    return StatusCode(200, subject);
                }
                return NotFound();
            }

        }



    }
}

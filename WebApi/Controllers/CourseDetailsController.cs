using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDetailsController : ControllerBase
    {
        #region Properties
        private readonly AppDbContext _context;
        #endregion Properties

        #region Constructors
        public CourseDetailsController(AppDbContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region Methods

        //Get all CourseDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDetails>>> GetCourseDetails()
        {
            return await _context.CourseDetails.ToListAsync();
        }

        //Get a specific CourseDetail by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetails>> GetCourseDetail(int id)
        {
            var courseDetail = await _context.CourseDetails.FindAsync(id);

            if (courseDetail == null)
            {
                return NotFound();
            }

            return courseDetail;
        }

        //Create a new CourseDetail
        [HttpPost]
        public async Task<ActionResult<CourseDetails>> CreateCourseDetail(CourseDetails courseDetail)
        {
            _context.CourseDetails.Add(courseDetail);
            await _context.SaveChangesAsync();

            return Ok(courseDetail);
        }

        //Update an existing CourseDetail
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseDetail(int id, CourseDetails courseDetail)
        {
            courseDetail = await _context.CourseDetails.FindAsync(id);

            try
            {
                if (courseDetail != null)
                {
                    _context.Update(courseDetail);
                    _context.SaveChanges();

                    return (IActionResult)courseDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        //Delete a CourseDetail
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseDetail(int id)
        {
            var courseDetail = await _context.CourseDetails.FindAsync(id);

            if (courseDetail == null)
            {
                return NotFound();
            }

            _context.CourseDetails.Remove(courseDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Get all CourseDetails for a specific User
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CourseDetails>>> GetCourseDetailsByUser(int userId)
        {
            return await _context.CourseDetails.Where(cd => cd.UserId == userId).ToListAsync();
        }

        //Add a User to a CourseDetail
        [HttpPost("{id}/user/{userId}")]
        public async Task<IActionResult> AddUserToCourse(int id, int userId)
        {
            var courseDetail = await _context.CourseDetails.FindAsync(id);
            if (courseDetail == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            courseDetail.UserId = userId;
            courseDetail.User = user;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion Methods
    }
}

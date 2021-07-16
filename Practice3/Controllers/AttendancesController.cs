using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Practice3.Models;

namespace Practice3.Controllers
{
    public class AttendancesController : ApiController
    {
        public BigShoolContext db = new BigShoolContext();

        // POST: api/Attendances
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult PostAttendance(Course course)
        {
            var attendance = new Attendance() { CourseId = course.Id, Attendee = User.Identity.GetUserId() };
            db.Attendances.Add(attendance);
            try
            {
                
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AttendanceExists(attendance))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(attendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(Attendance attendance)
        {
            return db.Attendances.Any(e => e.CourseId == attendance.CourseId && e.Attendee == attendance.Attendee);
        }
    }
}
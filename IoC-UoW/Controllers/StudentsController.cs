using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Entity.Models;
using Repository.UnitOfWork;
using UnitOfWork;

namespace IoC_UoW.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController()
        {
            _unitOfWork = new UnitOfWorkInjector().GetUnitOfWorkInstance();
        }
        // GET: Students
        public async Task<ActionResult> Index()
        {
            return View(await _unitOfWork.GetRepository<Student>().GetAll().ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = await _unitOfWork.GetRepository<Student>().FindAsync(id);
            if (student == null)
                return HttpNotFound();
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Age,CreatedBy,CreatedAt,LastModifiedBy,LastModifiedAt")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                _unitOfWork.GetRepository<Student>().Insert(student);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _unitOfWork.GetRepository<Student>().FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Age,CreatedBy,CreatedAt,LastModifiedBy,LastModifiedAt")] Student student)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepository<Student>().Update(student);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _unitOfWork.GetRepository<Student>().FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Student student = await _unitOfWork.GetRepository<Student>().FindAsync(id);
            _unitOfWork.GetRepository<Student>().Delete(student);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using LibraryAngular.Models;
using MusicStream.Models;
using MusicStream.Repositories;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MusicStream.Controllers
{
    public class HomeController : Controller
    {
        private SongsRepository _repo;
        public HomeController()
        {
            _repo = new SongsRepository();
        }

        public ActionResult Index()
        {
            var songs = _repo.GetSongs();
            return View(songs);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("/songs/"), fileName);
                file.SaveAs(path);
                Song song = new Song() { FilePath = Globals.resolveVirtual(path), SongName = file.FileName };
                _repo.Add(song);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddPlaylist()
        {
            return View("Playlist");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            DeleteFromServer(id);
            _repo.Delete(id);

            return RedirectToAction("Index");
        }

        public void DeleteFromServer(int id)
        {
            Song song = _repo.Get(id);
            var path = Path.Combine(Server.MapPath("/songs/"), song.SongName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task<ActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Server.MapPath("/songs/"), filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, Path.GetExtension(path), Path.GetFileName(path));
        }

    }
}
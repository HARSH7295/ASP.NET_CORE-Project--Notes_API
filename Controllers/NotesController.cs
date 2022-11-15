using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI___with_Repository_Pattern_and_Dtos.DBControl;
using NotesAPI___with_Repository_Pattern_and_Dtos.DTOs;
using NotesAPI___with_Repository_Pattern_and_Dtos.Models;
using NotesAPI___with_Repository_Pattern_and_Dtos.Repositories;

namespace NotesAPI___with_Repository_Pattern_and_Dtos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INotesRepository _repo;
        public NotesController(IMapper mapper,INotesRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        // POST -- api/notes/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(NoteForCreateDTO dto)
        {
            if(await _repo.NoteExists(dto.Title))
            {
                return StatusCode(403, "Note with this title already exists..Try different one.");
            }
            else
            {
                var noteToCreate = _mapper.Map<Note>(dto);
                var note = await _repo.Create(noteToCreate);
                if(note == null)
                {
                    return StatusCode(500, "Error occured during creation of note.");
                }
                else
                {
                    var noteToReturn = _mapper.Map<NoteForReturnDTO>(note);
                    return StatusCode(200, noteToReturn);
                }
            }
        }

        // GET -- api/notes/get/{id}
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var note = await _repo.Get(id);
            if(note == null)
            {
                return StatusCode(404, "No note with this id.");
            }
            else
            {
                var noteToReturn = _mapper.Map<NoteForReturnDTO>(note);
                return StatusCode(200, noteToReturn);
            }
        }

        // GET -- api/notes/get/all
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _repo.GetAll();
            if (notes.Count() != 0)
            {
                var notesToReturn = _mapper.Map<IEnumerable<NoteForReturnDTO>>(notes);
                return StatusCode(200, notesToReturn);
            }
            else
            {
                return StatusCode(204, "No notes");
            }
        }

        // PATCH -- api/notes/update/{id}
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(int id, NoteForCreateDTO dto)
        {
            var note = await _repo.Get(id);
            if (note == null)
            {
                return StatusCode(404, "No note with this id");
            }
            else
            {
                var noteFromRepo = await _repo.Update(dto, id);
                if(noteFromRepo == null)
                {
                    return StatusCode(500, "Error occured during updating note");
                }
                else
                {
                    var noteToReturn = _mapper.Map<NoteForReturnDTO>(noteFromRepo);
                    return StatusCode(200, noteToReturn);
                }
            }
        }

        // DELETE -- api/notes/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _repo.Get(id);
            if (note == null)
            {
                return StatusCode(400, "No note with this id");
            }
            else
            {
                var ans = await _repo.Delete(id);
                if (ans == "Deleted")
                {
                    return StatusCode(200,"Note Deleted.!!");
                }
                else
                {
                    return StatusCode(500,"Error occured during deletion of note.!!");
                }
            }
        }
    }
}

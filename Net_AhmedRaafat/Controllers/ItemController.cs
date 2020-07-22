using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net_AhmedRaafat.Manager;
using Net_AhmedRaafat_Entities;
using Net_AhmedRaafat_Repository;
using System;

namespace Net_AhmedRaafat.Controllers
{
    [Authorize]
    [Route("api/Item")]
    public class ItemController : Controller
    {
        private IBaseRepository<PersonalDiary> _personalDiaryRepository;
        private IBaseRepository<ToDo> _toDoRepository;
        private readonly IMapper _mapper;


        private ConfigurationsManager _configurationsManager;
        private readonly ItemManager _itemManager;

        public ItemController(IBaseRepository<PersonalDiary> personalDiaryRepository, IBaseRepository<ToDo> toDoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _personalDiaryRepository = personalDiaryRepository;
            _toDoRepository = toDoRepository;
            _itemManager = new ItemManager(_personalDiaryRepository, _toDoRepository);
        }


        [HttpPost("AddItem/{isDiary}")]
        public IActionResult AddItem([FromBody]PersonalDiary item, bool isDiary)
        {

            if (isDiary)
            {
                try
                {
                    //var diary = _mapper.Map<PersonalDiary>(item);
                    var res = _itemManager.InsertDiary(item);
                    if (res)
                        return Ok(true);

                    return NotFound();
                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                try
                {
                    var toDO = _mapper.Map<ToDo>(item);
                    var res = _itemManager.InsertToDo(toDO);

                    if (res)
                        return Ok(true);

                    return NotFound();

                }
                catch
                {
                    return NotFound();
                }
            }

        }

        [HttpPut("editItem/{id}/{isDiary}")]
        public IActionResult EditItem([FromBody]PersonalDiary item, Guid id, bool isDiary)
        {

            if (isDiary)
            {
                try
                {
                    //var diary = _mapper.Map<PersonalDiary>(item);
                    var res = _itemManager.editDiary(id, item);

                    if (res)
                        return Ok(true);

                    return NotFound();

                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                try
                {
                    var toDO = _mapper.Map<ToDo>(item);
                    var res = _itemManager.editToDo(id, toDO);

                    if (res)
                        return Ok(true);

                    return NotFound();

                }
                catch
                {
                    return NotFound();
                }
            }

        }

        [HttpGet("GetAll/{userId}/{isDiary?}")]
        public IActionResult GetAll(Guid userId , bool isDiary = true)
        {

            if (isDiary)
            {
                try
                {
                    //var diaries = _personalDiaryRepository.GetAll().Result;
                    var diaries = _itemManager.GetAllDiaries(userId);
                    return Ok(diaries);

                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                try
                {
                    //var toDO = _toDoRepository.GetAll().Result;
                    var toDO = _itemManager.GetAllToDo(userId);
                    return Ok(toDO);

                }
                catch
                {
                    return NotFound();
                }
            }

        }

        [HttpGet("GetSpecificItem/{id}/{isDiary?}")]
        public IActionResult GetSpecificItem(Guid id, bool isDiary = true)
        {

            if (isDiary)
            {
                try
                {

                    var diary = _itemManager.GetDiaryById(id);
                    var d= diary.CreatedDate.ToString("MM/dd/yyyy hh:mm tt");
                    diary.CreatedDate= Convert.ToDateTime(d);
                    if (diary != null)
                        return Ok(diary);

                    return NotFound();
                }
                catch
                {
                    return NotFound();
                }

            }
            else
            {
                try
                {
                    var toDO = _itemManager.GetTODOById(id);
                    var d = toDO.CreatedDate.ToString("MM/dd/yyyy hh:mm tt");
                    toDO.CreatedDate = Convert.ToDateTime(d);

                    if (toDO != null)
                        return Ok(toDO);

                    return NotFound();


                }
                catch
                {
                    return NotFound();
                }
            }

        }
    }
}

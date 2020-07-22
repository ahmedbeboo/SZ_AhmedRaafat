using Net_AhmedRaafat_Entities;
using Net_AhmedRaafat_Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Net_AhmedRaafat.Manager
{
    public class ItemManager
    {
        private IBaseRepository<PersonalDiary> _personalDiaryRepository;
        private IBaseRepository<ToDo> _toDoRepository;


        public ItemManager(IBaseRepository<PersonalDiary> personalDiaryRepository, IBaseRepository<ToDo> toDoRepository)
        {
            _personalDiaryRepository = personalDiaryRepository;
            _toDoRepository = toDoRepository;

        }



        //PersonalDiary Operations 

        public List<PersonalDiary> GetAllDiaries(Guid userId)
        {
            try
            {
                var res = _personalDiaryRepository.GetAll().Result.Where(p=>p.userId== userId).OrderByDescending(p=>p.CreatedDate).ToList();

                if (res != null && res.Count > 0)
                    return res;

                return null;
            }
            catch
            {
                return null;
            }

        }

        public PersonalDiary GetDiaryById(Guid id)
        {
            try
            {
                var res = _personalDiaryRepository.GetById(id).Result;

                if (res != null)
                    return res;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool InsertDiary(PersonalDiary personalDiary)
        {
            personalDiary.Id = Guid.NewGuid();
            //personalDiary.CreatedDate = DateTime.Now;

            try
            {
                _personalDiaryRepository.Insert(personalDiary);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool editDiary(Guid id,PersonalDiary personalDiary)
        {
            try
            {
                personalDiary.Id = id;
                _personalDiaryRepository.Update(personalDiary);
                return true;
            }
            catch
            {
                return false;
            }
        }

        

        //ToDo Operations 

        public List<ToDo> GetAllToDo(Guid userId)
        {
            try
            {
                var res = _toDoRepository.GetAll().Result.Where(p => p.userId == userId).OrderByDescending(p => p.CreatedDate).ToList();

                if (res != null && res.Count > 0)
                    return res;

                return null;
            }
            catch
            {
                return null;
            }

        }

        public ToDo GetTODOById(Guid id)
        {
            try
            {
                var res = _toDoRepository.GetById(id).Result;

                if (res != null)
                    return res;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool InsertToDo(ToDo toDo)
        {
            toDo.Id = Guid.NewGuid();
            //toDo.CreatedDate = DateTime.Now;

            try
            {
                _toDoRepository.Insert(toDo);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool editToDo(Guid id, ToDo toDo)
        {
            try
            {
                toDo.Id = id;
                _toDoRepository.Update(toDo);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

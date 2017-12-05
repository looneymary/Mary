using DataAccess.Models;
using System;
using WorkerViewer.ViewModels;

namespace WorkerViewer.Infrastructure
{
    public class Mapper
    {
        /// <summary>
        /// To convert from object of class Worker to object of class BaseWorkerViewModel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>object of class BaseWorkerViewModel</returns>
        public static BaseWorkerViewModel MapEntityToModel(Worker entity)
        {
            if (entity is Developer devEntity)
            {
                return new DeveloperViewModel()
                {
                    Id = devEntity._id,
                    FirstName = devEntity.FirstName,
                    LastName = devEntity.LastName,
                    Sex = devEntity.Sex,
                    Appointment = devEntity.Appointment,
                    Date = devEntity.Date,
                    Salary = devEntity.Salary,
                    DevLang = devEntity.DevLang,
                    Experience = devEntity.Experience,
                    Level = devEntity.Level,
                    Type = devEntity.Type
                };
            }
            else if(entity is OfficeWorker)
            {
                var officeEntity = (OfficeWorker)entity;
                return new OfficeWorkerViewModel()
                {
                    Id = officeEntity._id,
                    FirstName = officeEntity.FirstName,
                    LastName = officeEntity.LastName,
                    Sex = officeEntity.Sex,
                    Appointment = officeEntity.Appointment,
                    Date = officeEntity.Date,
                    Salary = officeEntity.Salary,
                    YearsInService = officeEntity.YearsInService,
                    Type = officeEntity.Type
                };
            }
            else
            {
                throw new ArgumentException(string.Format("unknown type of worker: {0}", entity.GetType().Name));
            }
        }

        /// <summary>
        /// To convert fron object of class BaseWorkerViweModel to object of class Worker
        /// </summary>
        /// <param name="model"></param>
        /// <returns>object of class Worker</returns>
        public static Worker MapModelToEntity(BaseWorkerViewModel model)
        {
            if (model is DeveloperViewModel)
            {
                var developer = (DeveloperViewModel)model;
                return new Developer()
                {
                    _id = developer.Id,
                    FirstName = developer.FirstName,
                    LastName = developer.LastName,
                    Sex = developer.Sex,
                    Appointment = developer.Appointment,
                    Date = developer.Date,
                    Salary = developer.Salary,
                    DevLang = developer.DevLang,
                    Experience = developer.Experience,
                    Level = developer.Level,
                    Type = developer.Type
                };
            }

            else if (model is OfficeWorkerViewModel office)
            {
                return new OfficeWorker()
                {
                    _id = office.Id,
                    FirstName = office.FirstName,
                    LastName = office.LastName,
                    Sex = office.Sex,
                    Appointment = office.Appointment,
                    Date = office.Date,
                    Salary = office.Salary,
                    YearsInService = office.YearsInService,
                    Type = office.Type
                };
            }

            else
            {
                throw new ArgumentException(string.Format("unknown model: {0}", model.GetType().Name));
            }
        }
    }
}

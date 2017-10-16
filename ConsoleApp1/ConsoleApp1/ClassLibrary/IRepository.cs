using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataAccess
{
    public interface IRepository
    {
        /// <summary>
        /// Get list of workers according to certain filter
        /// </summary>
        /// <param name="filter">Filter for search</param>
        /// <returns>List of workers what found</returns>
        IEnumerable<Worker> Get(string filter, string xmlPath, string xsdPath);

        /// <summary>
        /// Add new worker in xml-document
        /// </summary>
        /// <param name="worker">Object that need to add in xml-document</param>
        void Create(Worker worker, string xmlPath, string xsdPath);

        /// <summary>
        /// Update element in xml-document
        /// </summary>
        /// <param name="worker">object "Worker" for update</param>
        void Update(Worker worker, string xmlPath, string xsdPath);

        /// <summary>
        /// Find anf remove worker by id from xml
        /// </summary>
        /// <param name="id">Worker's id</param>
        void Delete(Guid id, string xmlPath, string xsdPath);
    }
}

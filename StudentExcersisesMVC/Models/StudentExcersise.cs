using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExcersisesMVC.Models
{
    public class StudentExcersise
    {
        public int ExcersiseId { get; set; }
        public int StudentId { get; set; }
        public List<Excersise> AssignedExcersise { get; set; } = new List <Excersise>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_Hamza_API.Models
{
    // DTO to map incoming JSON request body
    public class TrackRCInstStateRequestDto
    {
        public int FormNo { get; set; }
        public List<int> SelectedJobs { get; set; }
        public List<string> SelectedJobNames { get; set; }
    }

    // Entity class to map to database table
    public class TrackRCInstState2
    {
        public int Id { get; set; }
        public int FormNo { get; set; }
        public Nullable<int> StateId { get; set; }
        public string SelectedJobNames { get; set; }
        public bool IsBooked { get; set; }
    }
}
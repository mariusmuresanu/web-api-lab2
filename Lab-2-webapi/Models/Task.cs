﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2_webapi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       // public DateTime Added { get; set; }
      //  public DateTime Deadline { get; set; }
      //  [EnumDataType(typeof(Importance))]
       // public Importance Imp { get; set; }
       // [EnumDataType(typeof(State))]
      //  public State Status { get; set; }
       // public DateTime ClosedAt { get; set; }
       // public List<Comment> Comments { get; set; }
    }
}
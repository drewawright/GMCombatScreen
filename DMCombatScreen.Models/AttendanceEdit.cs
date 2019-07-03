﻿using DMCombatScreen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMCombatScreen.Models
{
    public class AttendanceEdit
    {
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public int CombatID { get; set; }
        public string CombatName { get; set; }
        public int? CurrentHP { get; set; }
    }
}

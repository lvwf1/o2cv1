﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O2V1Web.Models.EFModels
{
    [Table("Feeds", Schema = "mgt")]
    public partial class Feed
    {
        [Key]
        [Column("FeedId", Order = 0)]
        public Guid FeedId { get; set; }

        [Key]
        [Column("BackBoneId", Order = 1)]
        public Guid BackBoneId { get; set; }
    }
}

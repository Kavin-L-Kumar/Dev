﻿namespace Dev.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Description{ get; set; }
        public string Name{ get; set; }
        public double DistanceInKm { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //navigation prop
        public Difficulty Difficulty {  get; set; }
        public Region Region { get; set; }



    }
}

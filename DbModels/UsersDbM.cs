using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Seido.Utilities.SeedGenerator;
using Models;
using models;
using Microsoft.Identity.Client;

namespace DbModels
{
    public class UsersDbM : Users, ISeed<UsersDbM>
    {
        [Key]
        public override Guid UserId { get; set; }

        public override string FullName { get; set; }

        public override string Email { get; set; }

        public override IAddress Address { get; set; }

        public UsersDbM() { }

        public new UsersDbM Seed(SeedGenerator seeder)
        {
            base.Seed(seeder);
            return this;
        }

    }
    
}
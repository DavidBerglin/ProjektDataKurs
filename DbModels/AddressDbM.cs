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
    public class AddressDbM : Address, ISeed<AddressDbM>
    {
        [Key]
        public override Guid AddressId { get; set; }
        public override string StreetAddress { get; set; }

        public override string City { get; set; }

        public override string Country { get; set; }
        public override int ZipCode { get; set; }

        [NotMapped]
        public override List<IUsers> Users { get => UsersDbM?.ToList<IUsers>(); set => new NotImplementedException(); }


        public List<UsersDbM> UsersDbM { get; set; } = null;


        public AddressDbM() { }
        public new AddressDbM Seed(SeedGenerator seeder)
        {
            base.Seed(seeder);
            return this;
        }
    }

}
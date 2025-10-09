using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Seido.Utilities.SeedGenerator;
using Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DbModels
{
    public class CommentDbM : Comment, ISeed<CommentDbM>
    {
        [Key]
        public override Guid CommentId { get; set; }
        public Guid AttractionId { get; set; }
        public Guid UserId { get; set; }
        public override string Text { get; set; }

        [NotMapped]
        public override IAttraction Attraction { get => AttractionDbM; set => new NotImplementedException(); }
        public AttractionDbM AttractionDbM { get; set; }

        [NotMapped]
        public override IUsers User { get => UsersDbM; set => new NotImplementedException(); }
        public UsersDbM UsersDbM { get; set; }

        public CommentDbM() { }

        public new CommentDbM Seed(SeedGenerator seeder)
        {
            base.Seed(seeder);
            return this;
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Seminar5.Models
{
    public class ChatContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        public ChatContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host = localhost; UserName=postgres; Password=example; Database=Seminar5")
            .LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);
                entity.ToTable("users");
                entity.Property(user => user.Id)
                    .HasColumnName("user_id");
                entity.Property(user => user.Name).HasColumnName("name")
                    .HasMaxLength(255);

                entity.HasMany(user => user.RecievedMessage).WithOne(message => message.Consumer)
                    .HasForeignKey(message => message.ConsumerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(message => message.Content).HasColumnName("text");

                entity.HasOne(message => message.Author)
                    .WithMany(user => user.SendedMessage)
                    .HasForeignKey(message => message.AutorId)
                    .HasConstraintName("mesages_from_user_id_fk_author_id");


            });

            base.OnModelCreating(modelBuilder);

        }

    }
}

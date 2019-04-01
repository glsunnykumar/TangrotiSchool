using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TangrotiSchool.Models.DB
{
    public partial class SchoolTangrotiContext : DbContext
    {
        public SchoolTangrotiContext()
        {
        }

        public SchoolTangrotiContext(DbContextOptions<SchoolTangrotiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Login { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=SchoolTangroti;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            //modelBuilder.Entity<Login>(entity =>
            //{
            //    entity.Property(e => e.Id).HasColumnName("id");

            //    entity.Property(e => e.Password)
            //        .IsRequired()
            //        .HasColumnName("password")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Username)
            //        .IsRequired()
            //        .HasColumnName("username")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);
            //});

            modelBuilder.Query<LoginByUsernamePassword>();
        }

        public async Task<List<LoginByUsernamePassword>> LoginByUsernamePasswords(string userName,string Password)
        {
            List<LoginByUsernamePassword> lst = new List<LoginByUsernamePassword>();
            try
            {
                SqlParameter userParameter = new SqlParameter("@username", userName);
                SqlParameter pwdParameter = new SqlParameter("@password", Password);
                string sqlQuery = "EXEC [dbo].[LoginByUserNamePassword]" + "@username,@password";
                lst = await this.Query<LoginByUsernamePassword>().FromSql(sqlQuery, userName, Password).ToListAsync();

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return lst;
        }
    }
}

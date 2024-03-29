﻿using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=OnlineExamination;Trusted_Connection=true");

            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-U0NS9N8\LOCALDB#9F7D4565;Database=OnlineExam;Trusted_Connection=true");
        }
        //hangi veritabanı hangisine bağlı onu gösteriyoruz.
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Exam> Exam { get; set; }
        //public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<ExamResult> ExamResult { get; set; }
        
        public DbSet<Question> Questions { get; set; }
        //public DbSet<QuestionOption> QuestionOptions { get; set; }
        
        public DbSet<StudentCourse> StudentCourses { get; set; }
      

    }
}

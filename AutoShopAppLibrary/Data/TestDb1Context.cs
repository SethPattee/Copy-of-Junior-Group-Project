using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace AutoShopAppLibrary.Data;

public partial class TestDb1Context : DbContext
{
    public TestDb1Context()
    {
    }

    public TestDb1Context(DbContextOptions<TestDb1Context> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=../../../testDB1.db3");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
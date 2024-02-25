﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StaffHub.Entities;

#nullable disable

namespace StaffHub.Entities.Migrations
{
    [DbContext(typeof(EmployeesDbContext))]
    partial class EmployeesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("StaffHub.Entities.Department", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmenName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department", (string)null);

                    b.HasData(
                        new
                        {
                            DepartmentId = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            DepartmenName = "IT"
                        },
                        new
                        {
                            DepartmentId = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            DepartmenName = "HR"
                        },
                        new
                        {
                            DepartmentId = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            DepartmenName = "Finance"
                        },
                        new
                        {
                            DepartmentId = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            DepartmenName = "Marketing"
                        },
                        new
                        {
                            DepartmentId = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            DepartmenName = "Sales"
                        });
                });

            modelBuilder.Entity("StaffHub.Entities.Employee", b =>
                {
                    b.Property<Guid>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DepartmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmployeeName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees", (string)null);

                    b.HasData(
                        new
                        {
                            EmployeeID = new Guid("9b32b1ba-3586-40d0-8e76-8500e0a84689"),
                            DateOfBirth = new DateTime(1989, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("ec51293b-52de-43b5-8765-49a5d7325dc7"),
                            Email = "mwebsdale0@people.com.cn",
                            EmployeeName = "Marguerite",
                            Gender = "Female",
                            IsActive = false,
                            Role = "Recruitment Specialist"
                        },
                        new
                        {
                            EmployeeID = new Guid("85324c3e-7041-4ab1-b7c0-aa43fffec6d6"),
                            DateOfBirth = new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            Email = "ushears1@globo.com",
                            EmployeeName = "Ursa",
                            Gender = "Female",
                            IsActive = false,
                            Role = "Developer"
                        },
                        new
                        {
                            EmployeeID = new Guid("1a35c0c8-ac6f-49fd-9740-fca801441b79"),
                            DateOfBirth = new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("14629847-905a-4a0e-9abe-80b61655c5cb"),
                            Email = "fbowsher2@howstuffworks.com",
                            EmployeeName = "Franchot",
                            Gender = "Male",
                            IsActive = true,
                            Role = "Developer"
                        },
                        new
                        {
                            EmployeeID = new Guid("5c041832-af60-4048-b2eb-a309514752be"),
                            DateOfBirth = new DateTime(1987, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            Email = "asarvar3@dropbox.com",
                            EmployeeName = "Angie",
                            Gender = "Male",
                            IsActive = true,
                            Role = "Accountant"
                        },
                        new
                        {
                            EmployeeID = new Guid("02dfa2c0-27c2-49a4-a52f-99107bdf8f93"),
                            DateOfBirth = new DateTime(1995, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                            Email = "ttregona4@stumbleupon.com",
                            EmployeeName = "Tani",
                            Gender = "Gender",
                            IsActive = false,
                            Role = "HR Manager"
                        },
                        new
                        {
                            EmployeeID = new Guid("4662b347-1057-4d0f-98b5-4ecfb5c77edc"),
                            DateOfBirth = new DateTime(1988, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            Email = "mlingfoot5@netvibes.com",
                            EmployeeName = "Mitchael",
                            Gender = "Male",
                            IsActive = false,
                            Role = "Content Creator"
                        },
                        new
                        {
                            EmployeeID = new Guid("ca065236-a934-4e19-8e2f-cdfeeb187998"),
                            DateOfBirth = new DateTime(1983, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            Email = "mjarrell6@wisc.edu",
                            EmployeeName = "Maddy",
                            Gender = "Male",
                            IsActive = true,
                            Role = "Financial Analyst"
                        },
                        new
                        {
                            EmployeeID = new Guid("c10089c8-e14c-4e70-8003-c2baede7cf95"),
                            DateOfBirth = new DateTime(1998, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            Email = "pretchford7@virginia.edu",
                            EmployeeName = "Pegeen",
                            Gender = "Female",
                            IsActive = true,
                            Role = "Auditor"
                        },
                        new
                        {
                            EmployeeID = new Guid("788075ef-ce02-4f88-8be0-bd70e67e80ae"),
                            DateOfBirth = new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("12e15727-d369-49a9-8b13-bc22e9362179"),
                            Email = "hmosco8@tripod.com",
                            EmployeeName = "Hansiain",
                            Gender = "Male",
                            IsActive = true,
                            Role = "Accountant"
                        },
                        new
                        {
                            EmployeeID = new Guid("97c269b2-a785-48cb-a2d6-bbbdda264d94"),
                            DateOfBirth = new DateTime(1997, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                            Email = "lwoodwing9@wix.com",
                            EmployeeName = "Lombard",
                            Gender = "Male",
                            IsActive = false,
                            Role = "Content Creator"
                        },
                        new
                        {
                            EmployeeID = new Guid("05df0d9f-8431-4a4d-b349-7d6fae774150"),
                            DateOfBirth = new DateTime(1990, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            Email = "mconachya@va.gov",
                            EmployeeName = "Minta",
                            Gender = "Female",
                            IsActive = true,
                            Role = "Sales Manager"
                        },
                        new
                        {
                            EmployeeID = new Guid("75b97179-89dc-4830-bf72-6f854dc4d71a"),
                            DateOfBirth = new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentID = new Guid("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                            Email = "vklussb@nationalgeographic.com",
                            EmployeeName = "Verene",
                            Gender = "Female",
                            IsActive = true,
                            Role = "Sales Representative"
                        });
                });

            modelBuilder.Entity("StaffHub.Entities.IdentityEntities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("StaffHub.Entities.IdentityEntities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StaffHub.Entities.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StaffHub.Entities.Employee", b =>
                {
                    b.HasOne("StaffHub.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("StaffHub.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
